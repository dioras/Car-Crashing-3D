using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class RevokeAllBansForUserRequest : PlayFabRequestCommon
	{
		public string PlayFabId;
	}
}
