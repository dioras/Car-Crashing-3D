using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetLeaderboardForUsersCharactersRequest : PlayFabRequestCommon
	{
		public int MaxResultsCount;

		public string PlayFabId;

		public string StatisticName;
	}
}
