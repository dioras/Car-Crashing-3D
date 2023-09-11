using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetCharacterLeaderboardResult : PlayFabResultCommon
	{
		public List<CharacterLeaderboardEntry> Leaderboard;
	}
}
