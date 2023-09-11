using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class RevokeAllBansForUserRequest : PlayFabRequestCommon
	{
		public string PlayFabId;
	}
}
