using System;
using UnityEngine;

namespace Gaia.FullSerializer
{
	public static class fsConfig
	{
		public static fsMemberSerialization DefaultMemberSerialization
		{
			get
			{
				return fsConfig._defaultMemberSerialization;
			}
			set
			{
				fsConfig._defaultMemberSerialization = value;
				fsMetaType.ClearCache();
			}
		}

		public static Type[] SerializeAttributes = new Type[]
		{
			typeof(SerializeField),
			typeof(fsPropertyAttribute)
		};

		public static Type[] IgnoreSerializeAttributes = new Type[]
		{
			typeof(NonSerializedAttribute),
			typeof(fsIgnoreAttribute)
		};

		private static fsMemberSerialization _defaultMemberSerialization = fsMemberSerialization.Default;

		public static bool SerializeNonAutoProperties = false;

		public static bool SerializeNonPublicSetProperties = true;

		public static bool IsCaseSensitive = true;

		public static string CustomDateTimeFormatString = null;

		public static bool Serialize64BitIntegerAsString = false;

		public static bool SerializeEnumsAsInteger = false;
	}
}
