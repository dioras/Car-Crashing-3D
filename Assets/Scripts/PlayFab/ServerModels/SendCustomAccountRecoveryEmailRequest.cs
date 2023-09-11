using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class SendCustomAccountRecoveryEmailRequest : PlayFabRequestCommon
	{
		public string Email;

		public string EmailTemplateId;

		public string Username;
	}
}
