using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class RevokeAllBansForUserResult : PlayFabResultCommon
	{
		public List<BanInfo> BanData;
	}
}
