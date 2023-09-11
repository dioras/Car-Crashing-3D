using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetPlayersSegmentsRequest : PlayFabRequestCommon
	{
		public string PlayFabId;
	}
}
