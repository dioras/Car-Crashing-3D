using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GrantCharacterToUserResult : PlayFabResultCommon
	{
		public string CharacterId;
	}
}
