using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class DeleteCharacterFromUserRequest : PlayFabRequestCommon
	{
		public string CharacterId;

		public string PlayFabId;

		public bool SaveCharacterInventory;
	}
}
