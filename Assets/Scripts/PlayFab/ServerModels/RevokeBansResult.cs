using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class RevokeBansResult : PlayFabResultCommon
	{
		public List<BanInfo> BanData;
	}
}
