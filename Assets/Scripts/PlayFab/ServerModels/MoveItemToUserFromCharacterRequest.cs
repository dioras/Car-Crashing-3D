using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class MoveItemToUserFromCharacterRequest : PlayFabRequestCommon
	{
		public string CharacterId;

		public string ItemInstanceId;

		public string PlayFabId;
	}
}
