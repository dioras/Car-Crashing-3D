using System;
using System.Collections.Generic;
using Gaia.FullSerializer.Internal;

namespace Gaia.FullSerializer
{
	public class fsSerializer
	{
		public fsSerializer()
		{
			this._cachedConverters = new Dictionary<Type, fsBaseConverter>();
			this._cachedProcessors = new Dictionary<Type, List<fsObjectProcessor>>();
			this._references = new fsCyclicReferenceManager();
			this._lazyReferenceWriter = new fsSerializer.fsLazyCycleDefinitionWriter();
			this._availableConverters = new List<fsConverter>
			{
				new fsNullableConverter
				{
					Serializer = this
				},
				new fsGuidConverter
				{
					Serializer = this
				},
				new fsTypeConverter
				{
					Serializer = this
				},
				new fsDateConverter
				{
					Serializer = this
				},
				new fsEnumConverter
				{
					Serializer = this
				},
				new fsPrimitiveConverter
				{
					Serializer = this
				},
				new fsArrayConverter
				{
					Serializer = this
				},
				new fsDictionaryConverter
				{
					Serializer = this
				},
				new fsIEnumerableConverter
				{
					Serializer = this
				},
				new fsKeyValuePairConverter
				{
					Serializer = this
				},
				new fsWeakReferenceConverter
				{
					Serializer = this
				},
				new fsReflectedConverter
				{
					Serializer = this
				}
			};
			this._availableDirectConverters = new Dictionary<Type, fsDirectConverter>();
			this._processors = new List<fsObjectProcessor>
			{
				new fsSerializationCallbackProcessor()
			};
			this._processors.Add(new fsSerializationCallbackReceiverProcessor());
			this.Context = new fsContext();
			foreach (Type type in fsConverterRegistrar.Converters)
			{
				this.AddConverter((fsBaseConverter)Activator.CreateInstance(type));
			}
		}

		public static bool IsReservedKeyword(string key)
		{
			return fsSerializer._reservedKeywords.Contains(key);
		}

		private static bool IsObjectReference(fsData data)
		{
			return data.IsDictionary && data.AsDictionary.ContainsKey("$ref");
		}

		private static bool IsObjectDefinition(fsData data)
		{
			return data.IsDictionary && data.AsDictionary.ContainsKey("$id");
		}

		private static bool IsVersioned(fsData data)
		{
			return data.IsDictionary && data.AsDictionary.ContainsKey("$version");
		}

		private static bool IsTypeSpecified(fsData data)
		{
			return data.IsDictionary && data.AsDictionary.ContainsKey("$type");
		}

		private static bool IsWrappedData(fsData data)
		{
			return data.IsDictionary && data.AsDictionary.ContainsKey("$content");
		}

		public static void StripDeserializationMetadata(ref fsData data)
		{
			if (data.IsDictionary && data.AsDictionary.ContainsKey("$content"))
			{
				data = data.AsDictionary["$content"];
			}
			if (data.IsDictionary)
			{
				Dictionary<string, fsData> asDictionary = data.AsDictionary;
				asDictionary.Remove("$ref");
				asDictionary.Remove("$id");
				asDictionary.Remove("$type");
				asDictionary.Remove("$version");
			}
		}

		private static void ConvertLegacyData(ref fsData data)
		{
			if (!data.IsDictionary)
			{
				return;
			}
			Dictionary<string, fsData> asDictionary = data.AsDictionary;
			if (asDictionary.Count > 2)
			{
				return;
			}
			string key = "ReferenceId";
			string key2 = "SourceId";
			string key3 = "Data";
			string key4 = "Type";
			string key5 = "Data";
			if (asDictionary.Count == 2 && asDictionary.ContainsKey(key4) && asDictionary.ContainsKey(key5))
			{
				data = asDictionary[key5];
				fsSerializer.EnsureDictionary(data);
				fsSerializer.ConvertLegacyData(ref data);
				data.AsDictionary["$type"] = asDictionary[key4];
			}
			else if (asDictionary.Count == 2 && asDictionary.ContainsKey(key2) && asDictionary.ContainsKey(key3))
			{
				data = asDictionary[key3];
				fsSerializer.EnsureDictionary(data);
				fsSerializer.ConvertLegacyData(ref data);
				data.AsDictionary["$id"] = asDictionary[key2];
			}
			else if (asDictionary.Count == 1 && asDictionary.ContainsKey(key))
			{
				data = fsData.CreateDictionary();
				data.AsDictionary["$ref"] = asDictionary[key];
			}
		}

		private static void Invoke_OnBeforeSerialize(List<fsObjectProcessor> processors, Type storageType, object instance)
		{
			for (int i = 0; i < processors.Count; i++)
			{
				processors[i].OnBeforeSerialize(storageType, instance);
			}
		}

		private static void Invoke_OnAfterSerialize(List<fsObjectProcessor> processors, Type storageType, object instance, ref fsData data)
		{
			for (int i = processors.Count - 1; i >= 0; i--)
			{
				processors[i].OnAfterSerialize(storageType, instance, ref data);
			}
		}

		private static void Invoke_OnBeforeDeserialize(List<fsObjectProcessor> processors, Type storageType, ref fsData data)
		{
			for (int i = 0; i < processors.Count; i++)
			{
				processors[i].OnBeforeDeserialize(storageType, ref data);
			}
		}

		private static void Invoke_OnBeforeDeserializeAfterInstanceCreation(List<fsObjectProcessor> processors, Type storageType, object instance, ref fsData data)
		{
			for (int i = 0; i < processors.Count; i++)
			{
				processors[i].OnBeforeDeserializeAfterInstanceCreation(storageType, instance, ref data);
			}
		}

		private static void Invoke_OnAfterDeserialize(List<fsObjectProcessor> processors, Type storageType, object instance)
		{
			for (int i = processors.Count - 1; i >= 0; i--)
			{
				processors[i].OnAfterDeserialize(storageType, instance);
			}
		}

		private static void EnsureDictionary(fsData data)
		{
			if (!data.IsDictionary)
			{
				fsData value = data.Clone();
				data.BecomeDictionary();
				data.AsDictionary["$content"] = value;
			}
		}

		public void AddProcessor(fsObjectProcessor processor)
		{
			this._processors.Add(processor);
			this._cachedProcessors = new Dictionary<Type, List<fsObjectProcessor>>();
		}

		private List<fsObjectProcessor> GetProcessors(Type type)
		{
			fsObjectAttribute attribute = fsPortableReflection.GetAttribute<fsObjectAttribute>(type);
			List<fsObjectProcessor> list;
			if (attribute != null && attribute.Processor != null)
			{
				fsObjectProcessor item = (fsObjectProcessor)Activator.CreateInstance(attribute.Processor);
				list = new List<fsObjectProcessor>();
				list.Add(item);
				this._cachedProcessors[type] = list;
			}
			else if (!this._cachedProcessors.TryGetValue(type, out list))
			{
				list = new List<fsObjectProcessor>();
				for (int i = 0; i < this._processors.Count; i++)
				{
					fsObjectProcessor fsObjectProcessor = this._processors[i];
					if (fsObjectProcessor.CanProcess(type))
					{
						list.Add(fsObjectProcessor);
					}
				}
				this._cachedProcessors[type] = list;
			}
			return list;
		}

		public void AddConverter(fsBaseConverter converter)
		{
			if (converter.Serializer != null)
			{
				throw new InvalidOperationException("Cannot add a single converter instance to multiple fsConverters -- please construct a new instance for " + converter);
			}
			if (converter is fsDirectConverter)
			{
				fsDirectConverter fsDirectConverter = (fsDirectConverter)converter;
				this._availableDirectConverters[fsDirectConverter.ModelType] = fsDirectConverter;
			}
			else
			{
				if (!(converter is fsConverter))
				{
					throw new InvalidOperationException("Unable to add converter " + converter + "; the type association strategy is unknown. Please use either fsDirectConverter or fsConverter as your base type.");
				}
				this._availableConverters.Insert(0, (fsConverter)converter);
			}
			converter.Serializer = this;
			this._cachedConverters = new Dictionary<Type, fsBaseConverter>();
		}

		private fsBaseConverter GetConverter(Type type)
		{
			fsBaseConverter fsBaseConverter;
			if (this._cachedConverters.TryGetValue(type, out fsBaseConverter))
			{
				return fsBaseConverter;
			}
			fsObjectAttribute attribute = fsPortableReflection.GetAttribute<fsObjectAttribute>(type);
			if (attribute != null && attribute.Converter != null)
			{
				fsBaseConverter = (fsBaseConverter)Activator.CreateInstance(attribute.Converter);
				fsBaseConverter.Serializer = this;
				fsBaseConverter fsBaseConverter2 = fsBaseConverter;
				this._cachedConverters[type] = fsBaseConverter2;
				return fsBaseConverter2;
			}
			fsForwardAttribute attribute2 = fsPortableReflection.GetAttribute<fsForwardAttribute>(type);
			if (attribute2 != null)
			{
				fsBaseConverter = new fsForwardConverter(attribute2);
				fsBaseConverter.Serializer = this;
				fsBaseConverter fsBaseConverter2 = fsBaseConverter;
				this._cachedConverters[type] = fsBaseConverter2;
				return fsBaseConverter2;
			}
			if (!this._cachedConverters.TryGetValue(type, out fsBaseConverter))
			{
				if (this._availableDirectConverters.ContainsKey(type))
				{
					fsBaseConverter = this._availableDirectConverters[type];
					fsBaseConverter fsBaseConverter2 = fsBaseConverter;
					this._cachedConverters[type] = fsBaseConverter2;
					return fsBaseConverter2;
				}
				for (int i = 0; i < this._availableConverters.Count; i++)
				{
					if (this._availableConverters[i].CanProcess(type))
					{
						fsBaseConverter = this._availableConverters[i];
						fsBaseConverter fsBaseConverter2 = fsBaseConverter;
						this._cachedConverters[type] = fsBaseConverter2;
						return fsBaseConverter2;
					}
				}
			}
			throw new InvalidOperationException("Internal error -- could not find a converter for " + type);
		}

		public fsResult TrySerialize<T>(T instance, out fsData data)
		{
			return this.TrySerialize(typeof(T), instance, out data);
		}

		public fsResult TryDeserialize<T>(fsData data, ref T instance)
		{
			object obj = instance;
			fsResult result = this.TryDeserialize(data, typeof(T), ref obj);
			if (result.Succeeded)
			{
				instance = (T)((object)obj);
			}
			return result;
		}

		public fsResult TrySerialize(Type storageType, object instance, out fsData data)
		{
			List<fsObjectProcessor> processors = this.GetProcessors((instance != null) ? instance.GetType() : storageType);
			fsSerializer.Invoke_OnBeforeSerialize(processors, storageType, instance);
			if (object.ReferenceEquals(instance, null))
			{
				data = new fsData();
				fsSerializer.Invoke_OnAfterSerialize(processors, storageType, instance, ref data);
				return fsResult.Success;
			}
			fsResult result = this.InternalSerialize_1_ProcessCycles(storageType, instance, out data);
			fsSerializer.Invoke_OnAfterSerialize(processors, storageType, instance, ref data);
			return result;
		}

		private fsResult InternalSerialize_1_ProcessCycles(Type storageType, object instance, out fsData data)
		{
			fsResult result;
			try
			{
				this._references.Enter();
				if (!this.GetConverter(instance.GetType()).RequestCycleSupport(instance.GetType()))
				{
					result = this.InternalSerialize_2_Inheritance(storageType, instance, out data);
				}
				else if (this._references.IsReference(instance))
				{
					data = fsData.CreateDictionary();
					this._lazyReferenceWriter.WriteReference(this._references.GetReferenceId(instance), data.AsDictionary);
					result = fsResult.Success;
				}
				else
				{
					this._references.MarkSerialized(instance);
					fsResult fsResult = this.InternalSerialize_2_Inheritance(storageType, instance, out data);
					if (fsResult.Failed)
					{
						result = fsResult;
					}
					else
					{
						this._lazyReferenceWriter.WriteDefinition(this._references.GetReferenceId(instance), data);
						result = fsResult;
					}
				}
			}
			finally
			{
				if (this._references.Exit())
				{
					this._lazyReferenceWriter.Clear();
				}
			}
			return result;
		}

		private fsResult InternalSerialize_2_Inheritance(Type storageType, object instance, out fsData data)
		{
			fsResult result = this.InternalSerialize_3_ProcessVersioning(instance, out data);
			if (result.Failed)
			{
				return result;
			}
			if (storageType != instance.GetType() && this.GetConverter(storageType).RequestInheritanceSupport(storageType))
			{
				fsSerializer.EnsureDictionary(data);
				data.AsDictionary["$type"] = new fsData(instance.GetType().FullName);
			}
			return result;
		}

		private fsResult InternalSerialize_3_ProcessVersioning(object instance, out fsData data)
		{
			fsOption<fsVersionedType> versionedType = fsVersionManager.GetVersionedType(instance.GetType());
			if (!versionedType.HasValue)
			{
				return this.InternalSerialize_4_Converter(instance, out data);
			}
			fsVersionedType value = versionedType.Value;
			fsResult result = this.InternalSerialize_4_Converter(instance, out data);
			if (result.Failed)
			{
				return result;
			}
			fsSerializer.EnsureDictionary(data);
			data.AsDictionary["$version"] = new fsData(value.VersionString);
			return result;
		}

		private fsResult InternalSerialize_4_Converter(object instance, out fsData data)
		{
			Type type = instance.GetType();
			return this.GetConverter(type).TrySerialize(instance, out data, type);
		}

		public fsResult TryDeserialize(fsData data, Type storageType, ref object result)
		{
			if (data.IsNull)
			{
				result = null;
				List<fsObjectProcessor> processors = this.GetProcessors(storageType);
				fsSerializer.Invoke_OnBeforeDeserialize(processors, storageType, ref data);
				fsSerializer.Invoke_OnAfterDeserialize(processors, storageType, null);
				return fsResult.Success;
			}
			fsSerializer.ConvertLegacyData(ref data);
			fsResult result2;
			try
			{
				this._references.Enter();
				List<fsObjectProcessor> processors2;
				fsResult fsResult = this.InternalDeserialize_1_CycleReference(data, storageType, ref result, out processors2);
				if (fsResult.Succeeded)
				{
					fsSerializer.Invoke_OnAfterDeserialize(processors2, storageType, result);
				}
				result2 = fsResult;
			}
			finally
			{
				this._references.Exit();
			}
			return result2;
		}

		private fsResult InternalDeserialize_1_CycleReference(fsData data, Type storageType, ref object result, out List<fsObjectProcessor> processors)
		{
			if (fsSerializer.IsObjectReference(data))
			{
				int id = int.Parse(data.AsDictionary["$ref"].AsString);
				result = this._references.GetReferenceObject(id);
				processors = this.GetProcessors(result.GetType());
				return fsResult.Success;
			}
			return this.InternalDeserialize_2_Version(data, storageType, ref result, out processors);
		}

		private fsResult InternalDeserialize_2_Version(fsData data, Type storageType, ref object result, out List<fsObjectProcessor> processors)
		{
			if (fsSerializer.IsVersioned(data))
			{
				string asString = data.AsDictionary["$version"].AsString;
				fsOption<fsVersionedType> versionedType = fsVersionManager.GetVersionedType(storageType);
				if (versionedType.HasValue && versionedType.Value.VersionString != asString)
				{
					fsResult fsResult = fsResult.Success;
					List<fsVersionedType> list;
					fsResult += fsVersionManager.GetVersionImportPath(asString, versionedType.Value, out list);
					if (fsResult.Failed)
					{
						processors = this.GetProcessors(storageType);
						return fsResult;
					}
					fsResult += this.InternalDeserialize_3_Inheritance(data, list[0].ModelType, ref result, out processors);
					if (fsResult.Failed)
					{
						return fsResult;
					}
					for (int i = 1; i < list.Count; i++)
					{
						result = list[i].Migrate(result);
					}
					processors = this.GetProcessors(fsResult.GetType());
					return fsResult;
				}
			}
			return this.InternalDeserialize_3_Inheritance(data, storageType, ref result, out processors);
		}

		private fsResult InternalDeserialize_3_Inheritance(fsData data, Type storageType, ref object result, out List<fsObjectProcessor> processors)
		{
			fsResult success = fsResult.Success;
			processors = this.GetProcessors(storageType);
			fsSerializer.Invoke_OnBeforeDeserialize(processors, storageType, ref data);
			Type type = storageType;
			if (fsSerializer.IsTypeSpecified(data))
			{
				fsData fsData = data.AsDictionary["$type"];
				if (!fsData.IsString)
				{
					success.AddMessage("$type value must be a string (in " + data + ")");
				}
				else
				{
					string asString = fsData.AsString;
					Type type2 = fsTypeLookup.GetType(asString);
					if (type2 == null)
					{
						success.AddMessage("Unable to locate specified type \"" + asString + "\"");
					}
					else if (!storageType.IsAssignableFrom(type2))
					{
						success.AddMessage(string.Concat(new object[]
						{
							"Ignoring type specifier; a field/property of type ",
							storageType,
							" cannot hold an instance of ",
							type2
						}));
					}
					else
					{
						type = type2;
					}
				}
			}
			if (object.ReferenceEquals(result, null) || result.GetType() != type)
			{
				result = this.GetConverter(type).CreateInstance(data, type);
			}
			fsSerializer.Invoke_OnBeforeDeserializeAfterInstanceCreation(processors, storageType, result, ref data);
			return success + this.InternalDeserialize_4_Cycles(data, type, ref result);
		}

		private fsResult InternalDeserialize_4_Cycles(fsData data, Type resultType, ref object result)
		{
			if (fsSerializer.IsObjectDefinition(data))
			{
				int id = int.Parse(data.AsDictionary["$id"].AsString);
				this._references.AddReferenceWithId(id, result);
			}
			return this.InternalDeserialize_5_Converter(data, resultType, ref result);
		}

		private fsResult InternalDeserialize_5_Converter(fsData data, Type resultType, ref object result)
		{
			if (fsSerializer.IsWrappedData(data))
			{
				data = data.AsDictionary["$content"];
			}
			return this.GetConverter(resultType).TryDeserialize(data, ref result, resultType);
		}

		private static HashSet<string> _reservedKeywords = new HashSet<string>
		{
			"$ref",
			"$id",
			"$type",
			"$version",
			"$content"
		};

		private const string Key_ObjectReference = "$ref";

		private const string Key_ObjectDefinition = "$id";

		private const string Key_InstanceType = "$type";

		private const string Key_Version = "$version";

		private const string Key_Content = "$content";

		private Dictionary<Type, fsBaseConverter> _cachedConverters;

		private Dictionary<Type, List<fsObjectProcessor>> _cachedProcessors;

		private readonly List<fsConverter> _availableConverters;

		private readonly Dictionary<Type, fsDirectConverter> _availableDirectConverters;

		private readonly List<fsObjectProcessor> _processors;

		private readonly fsCyclicReferenceManager _references;

		private readonly fsSerializer.fsLazyCycleDefinitionWriter _lazyReferenceWriter;

		public fsContext Context;

		internal class fsLazyCycleDefinitionWriter
		{
			public void WriteDefinition(int id, fsData data)
			{
				if (this._references.Contains(id))
				{
					fsSerializer.EnsureDictionary(data);
					data.AsDictionary["$id"] = new fsData(id.ToString());
				}
				else
				{
					this._pendingDefinitions[id] = data;
				}
			}

			public void WriteReference(int id, Dictionary<string, fsData> dict)
			{
				if (this._pendingDefinitions.ContainsKey(id))
				{
					fsData fsData = this._pendingDefinitions[id];
					fsSerializer.EnsureDictionary(fsData);
					fsData.AsDictionary["$id"] = new fsData(id.ToString());
					this._pendingDefinitions.Remove(id);
				}
				else
				{
					this._references.Add(id);
				}
				dict["$ref"] = new fsData(id.ToString());
			}

			public void Clear()
			{
				this._pendingDefinitions.Clear();
			}

			private Dictionary<int, fsData> _pendingDefinitions = new Dictionary<int, fsData>();

			private HashSet<int> _references = new HashSet<int>();
		}
	}
}
