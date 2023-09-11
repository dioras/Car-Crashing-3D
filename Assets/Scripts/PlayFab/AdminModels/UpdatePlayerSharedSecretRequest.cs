using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class UpdatePlayerSharedSecretRequest : PlayFabRequestCommon
	{
		public bool Disabled;

		public string FriendlyName;

		public string SecretKey;
	}
}
