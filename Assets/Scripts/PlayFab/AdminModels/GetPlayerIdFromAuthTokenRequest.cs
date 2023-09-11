using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetPlayerIdFromAuthTokenRequest : PlayFabRequestCommon
	{
		public string Token;

		public AuthTokenType TokenType;
	}
}
