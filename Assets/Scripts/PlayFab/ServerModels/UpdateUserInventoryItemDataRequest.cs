using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class UpdateUserInventoryItemDataRequest : PlayFabRequestCommon
	{
		public string CharacterId;

		public Dictionary<string, string> Data;

		public string ItemInstanceId;

		public List<string> KeysToRemove;

		public string PlayFabId;
	}
}
