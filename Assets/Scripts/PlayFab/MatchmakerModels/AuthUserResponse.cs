using System;
using PlayFab.SharedModels;

namespace PlayFab.MatchmakerModels
{
	[Serializable]
	public class AuthUserResponse : PlayFabResultCommon
	{
		public bool Authorized;

		public string PlayFabId;
	}
}
