using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class DeletePlayerSharedSecretRequest : PlayFabRequestCommon
	{
		public string SecretKey;
	}
}
