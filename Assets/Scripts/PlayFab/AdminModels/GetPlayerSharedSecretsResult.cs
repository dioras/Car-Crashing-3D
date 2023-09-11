using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetPlayerSharedSecretsResult : PlayFabResultCommon
	{
		public List<SharedSecret> SharedSecrets;
	}
}
