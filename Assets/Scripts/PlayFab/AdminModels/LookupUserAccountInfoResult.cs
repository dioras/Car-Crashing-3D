using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class LookupUserAccountInfoResult : PlayFabResultCommon
	{
		public UserAccountInfo UserInfo;
	}
}
