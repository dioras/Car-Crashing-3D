using System;
using System.Reflection;

namespace Gaia.FullSerializer.Internal
{
	public class fsMetaProperty
	{
		internal fsMetaProperty(FieldInfo field)
		{
			this._memberInfo = field;
			this.StorageType = field.FieldType;
			this.JsonName = fsMetaProperty.GetJsonName(field);
			this.MemberName = field.Name;
			this.IsPublic = field.IsPublic;
			this.CanRead = true;
			this.CanWrite = true;
		}

		internal fsMetaProperty(PropertyInfo property)
		{
			this._memberInfo = property;
			this.StorageType = property.PropertyType;
			this.JsonName = fsMetaProperty.GetJsonName(property);
			this.MemberName = property.Name;
			this.IsPublic = (property.GetGetMethod() != null && property.GetGetMethod().IsPublic && property.GetSetMethod() != null && property.GetSetMethod().IsPublic);
			this.CanRead = property.CanRead;
			this.CanWrite = property.CanWrite;
		}

		public Type StorageType { get; private set; }

		public bool CanRead { get; private set; }

		public bool CanWrite { get; private set; }

		public string JsonName { get; private set; }

		public string MemberName { get; private set; }

		public bool IsPublic { get; private set; }

		public void Write(object context, object value)
		{
			FieldInfo fieldInfo = this._memberInfo as FieldInfo;
			PropertyInfo propertyInfo = this._memberInfo as PropertyInfo;
			if (fieldInfo != null)
			{
				fieldInfo.SetValue(context, value);
			}
			else if (propertyInfo != null)
			{
				MethodInfo setMethod = propertyInfo.GetSetMethod(true);
				if (setMethod != null)
				{
					setMethod.Invoke(context, new object[]
					{
						value
					});
				}
			}
		}

		public object Read(object context)
		{
			if (this._memberInfo is PropertyInfo)
			{
				return ((PropertyInfo)this._memberInfo).GetValue(context, new object[0]);
			}
			return ((FieldInfo)this._memberInfo).GetValue(context);
		}

		private static string GetJsonName(MemberInfo member)
		{
			fsPropertyAttribute attribute = fsPortableReflection.GetAttribute<fsPropertyAttribute>(member);
			if (attribute != null && !string.IsNullOrEmpty(attribute.Name))
			{
				return attribute.Name;
			}
			return member.Name;
		}

		private MemberInfo _memberInfo;
	}
}
