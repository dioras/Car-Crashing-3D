using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetUserBansRequest : PlayFabRequestCommon
	{
		public string PlayFabId;
	}
}
