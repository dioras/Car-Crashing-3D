using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class SendEmailFromTemplateRequest : PlayFabRequestCommon
	{
		public string EmailTemplateId;

		public string PlayFabId;
	}
}
