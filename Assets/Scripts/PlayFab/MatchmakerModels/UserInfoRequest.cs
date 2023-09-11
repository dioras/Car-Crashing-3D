using System;
using PlayFab.SharedModels;

namespace PlayFab.MatchmakerModels
{
	[Serializable]
	public class UserInfoRequest : PlayFabRequestCommon
	{
		public int MinCatalogVersion;

		public string PlayFabId;
	}
}
