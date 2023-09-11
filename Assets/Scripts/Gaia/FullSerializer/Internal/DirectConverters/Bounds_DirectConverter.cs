using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gaia.FullSerializer.Internal.DirectConverters
{
	public class Bounds_DirectConverter : fsDirectConverter<Bounds>
	{
		protected override fsResult DoSerialize(Bounds model, Dictionary<string, fsData> serialized)
		{
			fsResult a = fsResult.Success;
			a += base.SerializeMember<Vector3>(serialized, "center", model.center);
			return a + base.SerializeMember<Vector3>(serialized, "size", model.size);
		}

		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref Bounds model)
		{
			fsResult fsResult = fsResult.Success;
			Vector3 center = model.center;
			fsResult += base.DeserializeMember<Vector3>(data, "center", out center);
			model.center = center;
			Vector3 size = model.size;
			fsResult += base.DeserializeMember<Vector3>(data, "size", out size);
			model.size = size;
			return fsResult;
		}

		public override object CreateInstance(fsData data, Type storageType)
		{
			return default(Bounds);
		}
	}
}
