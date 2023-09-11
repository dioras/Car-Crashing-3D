using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetPlayerStatisticVersionsResult : PlayFabResultCommon
	{
		public List<PlayerStatisticVersion> StatisticVersions;
	}
}
