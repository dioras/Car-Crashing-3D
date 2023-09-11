using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetPlayerProfileRequest : PlayFabRequestCommon
	{
		public string PlayFabId;

		public PlayerProfileViewConstraints ProfileConstraints;
	}
}
