using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class BanUsersRequest : PlayFabRequestCommon
	{
		public List<BanRequest> Bans;
	}
}
