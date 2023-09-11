using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class AuthenticateSessionTicketRequest : PlayFabRequestCommon
	{
		public string SessionTicket;
	}
}
