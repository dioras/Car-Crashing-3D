using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetPlayerStatisticsResult : PlayFabResultCommon
	{
		public string PlayFabId;

		public List<StatisticValue> Statistics;
	}
}
