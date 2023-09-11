using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class CreatePlayerSharedSecretRequest : PlayFabRequestCommon
	{
		public string FriendlyName;
	}
}
