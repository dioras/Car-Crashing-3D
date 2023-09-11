using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class MoveItemToCharacterFromUserRequest : PlayFabRequestCommon
	{
		public string CharacterId;

		public string ItemInstanceId;

		public string PlayFabId;
	}
}
