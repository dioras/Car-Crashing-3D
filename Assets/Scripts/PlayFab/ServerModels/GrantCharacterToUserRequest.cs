using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GrantCharacterToUserRequest : PlayFabRequestCommon
	{
		public string CharacterName;

		public string CharacterType;

		public string PlayFabId;
	}
}
