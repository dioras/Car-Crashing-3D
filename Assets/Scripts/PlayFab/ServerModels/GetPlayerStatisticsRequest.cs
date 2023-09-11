using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetPlayerStatisticsRequest : PlayFabRequestCommon
	{
		public string PlayFabId;

		public List<string> StatisticNames;

		public List<StatisticNameVersion> StatisticNameVersions;
	}
}
