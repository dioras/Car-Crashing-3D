using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetPlayersInSegmentResult : PlayFabResultCommon
	{
		public string ContinuationToken;

		public List<PlayerProfile> PlayerProfiles;

		public int ProfilesInSegment;
	}
}
