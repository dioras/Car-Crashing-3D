using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class AuthenticateSessionTicketResult : PlayFabResultCommon
	{
		public UserAccountInfo UserInfo;
	}
}
