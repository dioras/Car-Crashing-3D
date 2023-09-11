using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class ListUsersCharactersRequest : PlayFabRequestCommon
	{
		public string PlayFabId;
	}
}
