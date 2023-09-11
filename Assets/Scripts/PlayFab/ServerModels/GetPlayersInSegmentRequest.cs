using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetPlayersInSegmentRequest : PlayFabRequestCommon
	{
		public string ContinuationToken;

		public uint? MaxBatchSize;

		public uint? SecondsToLive;

		public string SegmentId;
	}
}
