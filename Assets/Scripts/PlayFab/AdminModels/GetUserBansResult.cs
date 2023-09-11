using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetUserBansResult : PlayFabResultCommon
	{
		public List<BanInfo> BanData;
	}
}
