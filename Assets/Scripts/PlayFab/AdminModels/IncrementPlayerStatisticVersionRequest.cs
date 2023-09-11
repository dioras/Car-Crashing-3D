using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class IncrementPlayerStatisticVersionRequest : PlayFabRequestCommon
	{
		public string StatisticName;
	}
}
