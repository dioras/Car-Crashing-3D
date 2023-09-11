using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetUserAccountInfoResult : PlayFabResultCommon
	{
		public UserAccountInfo UserInfo;
	}
}
