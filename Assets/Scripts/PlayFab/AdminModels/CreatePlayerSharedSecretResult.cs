using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class CreatePlayerSharedSecretResult : PlayFabResultCommon
	{
		public string SecretKey;
	}
}
