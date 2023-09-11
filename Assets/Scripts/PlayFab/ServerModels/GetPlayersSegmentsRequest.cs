using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetPlayersSegmentsRequest : PlayFabRequestCommon
	{
		public string PlayFabId;
	}
}
