using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetMatchmakerGameInfoRequest : PlayFabRequestCommon
	{
		public string LobbyId;
	}
}
