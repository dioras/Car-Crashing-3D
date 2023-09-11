using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class MoveItemToCharacterFromCharacterRequest : PlayFabRequestCommon
	{
		public string GivingCharacterId;

		public string ItemInstanceId;

		public string PlayFabId;

		public string ReceivingCharacterId;
	}
}
