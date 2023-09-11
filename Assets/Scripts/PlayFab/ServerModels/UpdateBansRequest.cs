using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class UpdateBansRequest : PlayFabRequestCommon
	{
		public List<UpdateBanRequest> Bans;
	}
}
