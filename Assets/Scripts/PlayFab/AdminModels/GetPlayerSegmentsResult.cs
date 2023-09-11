using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetPlayerSegmentsResult : PlayFabResultCommon
	{
		public List<GetSegmentResult> Segments;
	}
}
