using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetPlayerProfileRequest : PlayFabRequestCommon
	{
		public string PlayFabId;

		public PlayerProfileViewConstraints ProfileConstraints;
	}
}
