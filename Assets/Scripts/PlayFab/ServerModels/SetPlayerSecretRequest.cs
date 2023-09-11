using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class SetPlayerSecretRequest : PlayFabRequestCommon
	{
		public string PlayerSecret;

		public string PlayFabId;
	}
}
