using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class UpdatePlayerStatisticsRequest : PlayFabRequestCommon
	{
		public bool? ForceUpdate;

		public string PlayFabId;

		public List<StatisticUpdate> Statistics;
	}
}
