using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gaia.FullSerializer.Internal.DirectConverters
{
	public class Keyframe_DirectConverter : fsDirectConverter<Keyframe>
	{
		protected override fsResult DoSerialize(Keyframe model, Dictionary<string, fsData> serialized)
		{
			fsResult a = fsResult.Success;
			a += base.SerializeMember<float>(serialized, "time", model.time);
			a += base.SerializeMember<float>(serialized, "value", model.value);
			a += base.SerializeMember<int>(serialized, "tangentMode", model.tangentMode);
			a += base.SerializeMember<float>(serialized, "inTangent", model.inTangent);
			return a + base.SerializeMember<float>(serialized, "outTangent", model.outTangent);
		}

		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref Keyframe model)
		{
			fsResult fsResult = fsResult.Success;
			float time = model.time;
			fsResult += base.DeserializeMember<float>(data, "time", out time);
			model.time = time;
			float value = model.value;
			fsResult += base.DeserializeMember<float>(data, "value", out value);
			model.value = value;
			int tangentMode = model.tangentMode;
			fsResult += base.DeserializeMember<int>(data, "tangentMode", out tangentMode);
			model.tangentMode = tangentMode;
			float inTangent = model.inTangent;
			fsResult += base.DeserializeMember<float>(data, "inTangent", out inTangent);
			model.inTangent = inTangent;
			float outTangent = model.outTangent;
			fsResult += base.DeserializeMember<float>(data, "outTangent", out outTangent);
			model.outTangent = outTangent;
			return fsResult;
		}

		public override object CreateInstance(fsData data, Type storageType)
		{
			return default(Keyframe);
		}
	}
}
