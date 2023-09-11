using System;
using PlayFab.SharedModels;

namespace PlayFab.MatchmakerModels
{
	[Serializable]
	public class AuthUserRequest : PlayFabRequestCommon
	{
		public string AuthorizationTicket;
	}
}
