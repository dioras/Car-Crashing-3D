using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class RevokeBansResult : PlayFabResultCommon
	{
		public List<BanInfo> BanData;
	}
}
