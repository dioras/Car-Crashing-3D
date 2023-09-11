using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class UpdateBansResult : PlayFabResultCommon
	{
		public List<BanInfo> BanData;
	}
}
