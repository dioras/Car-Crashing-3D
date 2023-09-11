using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class UpdateBansRequest : PlayFabRequestCommon
	{
		public List<UpdateBanRequest> Bans;
	}
}
