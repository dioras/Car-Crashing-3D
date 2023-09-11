using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class SetupPushNotificationResult : PlayFabResultCommon
	{
		public string ARN;
	}
}
