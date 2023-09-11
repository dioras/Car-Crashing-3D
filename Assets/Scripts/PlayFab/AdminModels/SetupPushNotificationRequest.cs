using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class SetupPushNotificationRequest : PlayFabRequestCommon
	{
		public string Credential;

		public string Key;

		public string Name;

		public bool OverwriteOldARN;

		public PushSetupPlatform Platform;
	}
}
