using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class UpdateCharacterDataRequest : PlayFabRequestCommon
	{
		public string CharacterId;

		public Dictionary<string, string> Data;

		public List<string> KeysToRemove;

		public UserDataPermission? Permission;

		public string PlayFabId;
	}
}
