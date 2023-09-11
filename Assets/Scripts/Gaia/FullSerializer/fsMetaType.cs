using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Gaia.FullSerializer.Internal;
using UnityEngine;

namespace Gaia.FullSerializer
{
	public class fsMetaType
	{
		private fsMetaType(Type reflectedType)
		{
			this.ReflectedType = reflectedType;
			List<fsMetaProperty> list = new List<fsMetaProperty>();
			fsMetaType.CollectProperties(list, reflectedType);
			this.Properties = list.ToArray();
		}

		public static fsMetaType Get(Type type)
		{
			fsMetaType fsMetaType;
			if (!fsMetaType._metaTypes.TryGetValue(type, out fsMetaType))
			{
				fsMetaType = new fsMetaType(type);
				fsMetaType._metaTypes[type] = fsMetaType;
			}
			return fsMetaType;
		}

		public static void ClearCache()
		{
			fsMetaType._metaTypes = new Dictionary<Type, fsMetaType>();
		}

		private static void CollectProperties(List<fsMetaProperty> properties, Type reflectedType)
		{
			bool flag = fsConfig.DefaultMemberSerialization == fsMemberSerialization.OptIn;
			bool flag2 = fsConfig.DefaultMemberSerialization == fsMemberSerialization.OptOut;
			fsObjectAttribute attribute = fsPortableReflection.GetAttribute<fsObjectAttribute>(reflectedType);
			if (attribute != null)
			{
				flag = (attribute.MemberSerialization == fsMemberSerialization.OptIn);
				flag2 = (attribute.MemberSerialization == fsMemberSerialization.OptOut);
			}
			MemberInfo[] declaredMembers = reflectedType.GetDeclaredMembers();
			MemberInfo[] array = declaredMembers;
			for (int i = 0; i < array.Length; i++)
			{
				MemberInfo member = array[i];
				if (!fsConfig.IgnoreSerializeAttributes.Any((Type t) => fsPortableReflection.HasAttribute(member, t)))
				{
					PropertyInfo propertyInfo = member as PropertyInfo;
					FieldInfo fieldInfo = member as FieldInfo;
					if (!flag || fsConfig.SerializeAttributes.Any((Type t) => fsPortableReflection.HasAttribute(member, t)))
					{
						if (!flag2 || !fsConfig.IgnoreSerializeAttributes.Any((Type t) => fsPortableReflection.HasAttribute(member, t)))
						{
							if (propertyInfo != null)
							{
								if (fsMetaType.CanSerializeProperty(propertyInfo, declaredMembers, flag2))
								{
									properties.Add(new fsMetaProperty(propertyInfo));
								}
							}
							else if (fieldInfo != null && fsMetaType.CanSerializeField(fieldInfo, flag2))
							{
								properties.Add(new fsMetaProperty(fieldInfo));
							}
						}
					}
				}
			}
			if (reflectedType.Resolve().BaseType != null)
			{
				fsMetaType.CollectProperties(properties, reflectedType.Resolve().BaseType);
			}
		}

		private static bool IsAutoProperty(PropertyInfo property, MemberInfo[] members)
		{
			if (!property.CanWrite || !property.CanRead)
			{
				return false;
			}
			string b = "<" + property.Name + ">k__BackingField";
			for (int i = 0; i < members.Length; i++)
			{
				if (members[i].Name == b)
				{
					return true;
				}
			}
			return false;
		}

		private static bool CanSerializeProperty(PropertyInfo property, MemberInfo[] members, bool annotationFreeValue)
		{
			if (typeof(Delegate).IsAssignableFrom(property.PropertyType))
			{
				return false;
			}
			MethodInfo getMethod = property.GetGetMethod(false);
			MethodInfo setMethod = property.GetSetMethod(false);
			return (getMethod == null || !getMethod.IsStatic) && (setMethod == null || !setMethod.IsStatic) && (fsConfig.SerializeAttributes.Any((Type t) => fsPortableReflection.HasAttribute(property, t)) || (property.CanRead && property.CanWrite && (((fsConfig.SerializeNonAutoProperties || fsMetaType.IsAutoProperty(property, members)) && getMethod != null && (fsConfig.SerializeNonPublicSetProperties || setMethod != null)) || annotationFreeValue)));
		}

		private static bool CanSerializeField(FieldInfo field, bool annotationFreeValue)
		{
			return !typeof(Delegate).IsAssignableFrom(field.FieldType) && !field.IsDefined(typeof(CompilerGeneratedAttribute), false) && !field.IsStatic && (fsConfig.SerializeAttributes.Any((Type t) => fsPortableReflection.HasAttribute(field, t)) || annotationFreeValue || field.IsPublic);
		}

		public bool EmitAotData()
		{
			if (this._hasEmittedAotData)
			{
				return false;
			}
			this._hasEmittedAotData = true;
			for (int i = 0; i < this.Properties.Length; i++)
			{
				if (!this.Properties[i].IsPublic)
				{
					return false;
				}
			}
			if (!this.HasDefaultConstructor)
			{
				return false;
			}
			fsAotCompilationManager.AddAotCompilation(this.ReflectedType, this.Properties, this._isDefaultConstructorPublic);
			return true;
		}

		public fsMetaProperty[] Properties { get; private set; }

		public bool HasDefaultConstructor
		{
			get
			{
				if (this._hasDefaultConstructorCache == null)
				{
					if (this.ReflectedType.Resolve().IsArray)
					{
						this._hasDefaultConstructorCache = new bool?(true);
						this._isDefaultConstructorPublic = true;
					}
					else if (this.ReflectedType.Resolve().IsValueType)
					{
						this._hasDefaultConstructorCache = new bool?(true);
						this._isDefaultConstructorPublic = true;
					}
					else
					{
						ConstructorInfo declaredConstructor = this.ReflectedType.GetDeclaredConstructor(fsPortableReflection.EmptyTypes);
						this._hasDefaultConstructorCache = new bool?(declaredConstructor != null);
						if (declaredConstructor != null)
						{
							this._isDefaultConstructorPublic = declaredConstructor.IsPublic;
						}
					}
				}
				return this._hasDefaultConstructorCache.Value;
			}
		}

		public object CreateInstance()
		{
			if (this.ReflectedType.Resolve().IsInterface || this.ReflectedType.Resolve().IsAbstract)
			{
				throw new Exception("Cannot create an instance of an interface or abstract type for " + this.ReflectedType);
			}
			if (typeof(ScriptableObject).IsAssignableFrom(this.ReflectedType))
			{
				return ScriptableObject.CreateInstance(this.ReflectedType);
			}
			if (typeof(string) == this.ReflectedType)
			{
				return string.Empty;
			}
			if (!this.HasDefaultConstructor)
			{
				return FormatterServices.GetSafeUninitializedObject(this.ReflectedType);
			}
			if (this.ReflectedType.Resolve().IsArray)
			{
				return Array.CreateInstance(this.ReflectedType.GetElementType(), 0);
			}
			object result;
			try
			{
				result = Activator.CreateInstance(this.ReflectedType, true);
			}
			catch (MissingMethodException innerException)
			{
				throw new InvalidOperationException("Unable to create instance of " + this.ReflectedType + "; there is no default constructor", innerException);
			}
			catch (TargetInvocationException innerException2)
			{
				throw new InvalidOperationException("Constructor of " + this.ReflectedType + " threw an exception when creating an instance", innerException2);
			}
			catch (MemberAccessException innerException3)
			{
				throw new InvalidOperationException("Unable to access constructor of " + this.ReflectedType, innerException3);
			}
			return result;
		}

		private static Dictionary<Type, fsMetaType> _metaTypes = new Dictionary<Type, fsMetaType>();

		public Type ReflectedType;

		private bool _hasEmittedAotData;

		private bool? _hasDefaultConstructorCache;

		private bool _isDefaultConstructorPublic;
	}
}
