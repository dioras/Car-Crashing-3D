using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class AwardSteamAchievementRequest : PlayFabRequestCommon
	{
		public List<AwardSteamAchievementItem> Achievements;
	}
}
