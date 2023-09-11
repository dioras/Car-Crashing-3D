using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class ResetPasswordRequest : PlayFabRequestCommon
	{
		public string Password;

		public string Token;
	}
}
