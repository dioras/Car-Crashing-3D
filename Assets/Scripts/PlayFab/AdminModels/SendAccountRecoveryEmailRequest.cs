using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class SendAccountRecoveryEmailRequest : PlayFabRequestCommon
	{
		public string Email;
	}
}
