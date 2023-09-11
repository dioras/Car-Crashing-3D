using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetLeaderboardAroundCharacterRequest : PlayFabRequestCommon
	{
		public string CharacterId;

		public string CharacterType;

		public int MaxResultsCount;

		public string PlayFabId;

		public string StatisticName;
	}
}
