using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class WriteServerCharacterEventRequest : PlayFabRequestCommon
	{
		public Dictionary<string, object> Body;

		public string CharacterId;

		public string EventName;

		public string PlayFabId;

		public DateTime? Timestamp;
	}
}
