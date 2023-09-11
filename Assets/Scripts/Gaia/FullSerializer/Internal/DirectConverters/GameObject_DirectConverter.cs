using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gaia.FullSerializer.Internal.DirectConverters
{
	public class GameObject_DirectConverter : fsDirectConverter<GameObject>
	{
		protected override fsResult DoSerialize(GameObject model, Dictionary<string, fsData> serialized)
		{
			fsResult fsResult = fsResult.Success;
			if (model == null)
			{
				fsResult += base.SerializeMember<bool>(serialized, "present", false);
			}
			else
			{
				fsResult += base.SerializeMember<bool>(serialized, "present", true);
				fsResult += base.SerializeMember<string>(serialized, "name", model.name);
				fsResult += base.SerializeMember<string>(serialized, "path", string.Empty);
			}
			return fsResult;
		}

		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref GameObject model)
		{
			fsResult fsResult = fsResult.Success;
			bool flag = false;
			fsResult += base.DeserializeMember<bool>(data, "present", out flag);
			if (flag)
			{
				string name = model.name;
				fsResult += base.DeserializeMember<string>(data, "name", out name);
				model.name = name;
				string empty = string.Empty;
				fsResult += base.DeserializeMember<string>(data, "path", out empty);
				if (!string.IsNullOrEmpty(empty))
				{
				}
			}
			return fsResult;
		}

		public override object CreateInstance(fsData data, Type storageType)
		{
			return new Texture2D(1024, 1024);
		}
	}
}
