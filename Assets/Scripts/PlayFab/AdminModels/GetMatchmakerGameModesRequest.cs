using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetMatchmakerGameModesRequest : PlayFabRequestCommon
	{
		public string BuildVersion;
	}
}
