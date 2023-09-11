using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gaia.FullSerializer.Internal.DirectConverters
{
	public class Rect_DirectConverter : fsDirectConverter<Rect>
	{
		protected override fsResult DoSerialize(Rect model, Dictionary<string, fsData> serialized)
		{
			fsResult a = fsResult.Success;
			a += base.SerializeMember<float>(serialized, "xMin", model.xMin);
			a += base.SerializeMember<float>(serialized, "yMin", model.yMin);
			a += base.SerializeMember<float>(serialized, "xMax", model.xMax);
			return a + base.SerializeMember<float>(serialized, "yMax", model.yMax);
		}

		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref Rect model)
		{
			fsResult fsResult = fsResult.Success;
			float xMin = model.xMin;
			fsResult += base.DeserializeMember<float>(data, "xMin", out xMin);
			model.xMin = xMin;
			float yMin = model.yMin;
			fsResult += base.DeserializeMember<float>(data, "yMin", out yMin);
			model.yMin = yMin;
			float xMax = model.xMax;
			fsResult += base.DeserializeMember<float>(data, "xMax", out xMax);
			model.xMax = xMax;
			float yMax = model.yMax;
			fsResult += base.DeserializeMember<float>(data, "yMax", out yMax);
			model.yMax = yMax;
			return fsResult;
		}

		public override object CreateInstance(fsData data, Type storageType)
		{
			return default(Rect);
		}
	}
}
