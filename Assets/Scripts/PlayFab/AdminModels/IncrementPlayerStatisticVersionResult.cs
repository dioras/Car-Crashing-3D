using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class IncrementPlayerStatisticVersionResult : PlayFabResultCommon
	{
		public PlayerStatisticVersion StatisticVersion;
	}
}
