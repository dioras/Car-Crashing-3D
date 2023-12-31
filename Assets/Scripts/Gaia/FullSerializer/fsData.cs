using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Gaia.FullSerializer
{
	public sealed class fsData
	{
		public fsData()
		{
			this._value = null;
		}

		public fsData(bool boolean)
		{
			this._value = boolean;
		}

		public fsData(double f)
		{
			this._value = f;
		}

		public fsData(long i)
		{
			this._value = i;
		}

		public fsData(ulong i)
		{
			this._value = i;
		}

		public fsData(string str)
		{
			this._value = str;
		}

		public fsData(Dictionary<string, fsData> dict)
		{
			this._value = dict;
		}

		public fsData(List<fsData> list)
		{
			this._value = list;
		}

		public static fsData CreateDictionary()
		{
			return new fsData(new Dictionary<string, fsData>((!fsConfig.IsCaseSensitive) ? StringComparer.CurrentCultureIgnoreCase : StringComparer.CurrentCulture));
		}

		public static fsData CreateList()
		{
			return new fsData(new List<fsData>());
		}

		public static fsData CreateList(int capacity)
		{
			return new fsData(new List<fsData>(capacity));
		}

		internal void BecomeDictionary()
		{
			this._value = new Dictionary<string, fsData>();
		}

		internal fsData Clone()
		{
			return new fsData
			{
				_value = this._value
			};
		}

		public fsDataType Type
		{
			get
			{
				if (this._value == null)
				{
					return fsDataType.Null;
				}
				if (this._value is double)
				{
					return fsDataType.Double;
				}
				if (this._value is long)
				{
					return fsDataType.Int64;
				}
				if (this._value is ulong)
				{
					return fsDataType.UInt64;
				}
				if (this._value is bool)
				{
					return fsDataType.Boolean;
				}
				if (this._value is string)
				{
					return fsDataType.String;
				}
				if (this._value is Dictionary<string, fsData>)
				{
					return fsDataType.Object;
				}
				if (this._value is List<fsData>)
				{
					return fsDataType.Array;
				}
				throw new InvalidOperationException("unknown JSON data type");
			}
		}

		public bool IsNull
		{
			get
			{
				return this._value == null;
			}
		}

		public bool IsDouble
		{
			get
			{
				return this._value is double;
			}
		}

		public bool IsInt64
		{
			get
			{
				return this._value is long;
			}
		}

		public bool IsUInt64
		{
			get
			{
				return this._value is ulong;
			}
		}

		public bool IsBool
		{
			get
			{
				return this._value is bool;
			}
		}

		public bool IsString
		{
			get
			{
				return this._value is string;
			}
		}

		public bool IsDictionary
		{
			get
			{
				return this._value is Dictionary<string, fsData>;
			}
		}

		public bool IsList
		{
			get
			{
				return this._value is List<fsData>;
			}
		}

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public double AsDouble
		{
			get
			{
				return this.Cast<double>();
			}
		}

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public long AsInt64
		{
			get
			{
				return this.Cast<long>();
			}
		}

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public ulong AsUInt64
		{
			get
			{
				return this.Cast<ulong>();
			}
		}

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool AsBool
		{
			get
			{
				return this.Cast<bool>();
			}
		}

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public string AsString
		{
			get
			{
				return this.Cast<string>();
			}
		}

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public Dictionary<string, fsData> AsDictionary
		{
			get
			{
				return this.Cast<Dictionary<string, fsData>>();
			}
		}

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public List<fsData> AsList
		{
			get
			{
				return this.Cast<List<fsData>>();
			}
		}

		private T Cast<T>()
		{
			if (this._value is T)
			{
				return (T)((object)this._value);
			}
			throw new InvalidCastException(string.Concat(new object[]
			{
				"Unable to cast <",
				this,
				"> (with type = ",
				this._value.GetType(),
				") to type ",
				typeof(T)
			}));
		}

		public override string ToString()
		{
			return fsJsonPrinter.CompressedJson(this);
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as fsData);
		}

		public bool Equals(fsData other)
		{
			if (other == null || this.Type != other.Type)
			{
				return false;
			}
			switch (this.Type)
			{
			case fsDataType.Array:
			{
				List<fsData> asList = this.AsList;
				List<fsData> asList2 = other.AsList;
				if (asList.Count != asList2.Count)
				{
					return false;
				}
				for (int i = 0; i < asList.Count; i++)
				{
					if (!asList[i].Equals(asList2[i]))
					{
						return false;
					}
				}
				return true;
			}
			case fsDataType.Object:
			{
				Dictionary<string, fsData> asDictionary = this.AsDictionary;
				Dictionary<string, fsData> asDictionary2 = other.AsDictionary;
				if (asDictionary.Count != asDictionary2.Count)
				{
					return false;
				}
				foreach (string key in asDictionary.Keys)
				{
					if (!asDictionary2.ContainsKey(key))
					{
						return false;
					}
					if (!asDictionary[key].Equals(asDictionary2[key]))
					{
						return false;
					}
				}
				return true;
			}
			case fsDataType.Double:
				return this.AsDouble == other.AsDouble || Math.Abs(this.AsDouble - other.AsDouble) < double.Epsilon;
			case fsDataType.Int64:
				return this.AsInt64 == other.AsInt64;
			case fsDataType.UInt64:
				return this.AsUInt64 == other.AsUInt64;
			case fsDataType.Boolean:
				return this.AsBool == other.AsBool;
			case fsDataType.String:
				return this.AsString == other.AsString;
			case fsDataType.Null:
				return true;
			default:
				throw new Exception("Unknown data type");
			}
		}

		public static bool operator ==(fsData a, fsData b)
		{
			if (object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (a.IsDouble && b.IsDouble)
			{
				return Math.Abs(a.AsDouble - b.AsDouble) < double.Epsilon;
			}
			return a.Equals(b);
		}

		public static bool operator !=(fsData a, fsData b)
		{
			return !(a == b);
		}

		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		private object _value;

		public static readonly fsData True = new fsData(true);

		public static readonly fsData False = new fsData(false);

		public static readonly fsData Null = new fsData();
	}
}
