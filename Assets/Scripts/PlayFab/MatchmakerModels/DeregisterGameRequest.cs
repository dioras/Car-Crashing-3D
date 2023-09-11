using System;
using PlayFab.SharedModels;

namespace PlayFab.MatchmakerModels
{
	[Serializable]
	public class DeregisterGameRequest : PlayFabRequestCommon
	{
		public string LobbyId;
	}
}
