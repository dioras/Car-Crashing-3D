using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class UpdatePlayerStatisticDefinitionRequest : PlayFabRequestCommon
	{
		public StatisticAggregationMethod? AggregationMethod;

		public string StatisticName;

		public StatisticResetIntervalOption? VersionChangeInterval;
	}
}
