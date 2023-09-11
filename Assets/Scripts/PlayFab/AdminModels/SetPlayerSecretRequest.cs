using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class SetPlayerSecretRequest : PlayFabRequestCommon
	{
		public string PlayerSecret;

		public string PlayFabId;
	}
}
