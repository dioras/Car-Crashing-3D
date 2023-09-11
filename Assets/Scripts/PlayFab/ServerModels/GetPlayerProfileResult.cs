using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetPlayerProfileResult : PlayFabResultCommon
	{
		public PlayerProfileModel PlayerProfile;
	}
}
