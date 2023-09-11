using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class AwardSteamAchievementResult : PlayFabResultCommon
	{
		public List<AwardSteamAchievementItem> AchievementResults;
	}
}
