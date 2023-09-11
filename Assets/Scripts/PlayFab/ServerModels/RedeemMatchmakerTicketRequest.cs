using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class RedeemMatchmakerTicketRequest : PlayFabRequestCommon
	{
		public string LobbyId;

		public string Ticket;
	}
}
