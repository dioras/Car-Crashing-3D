using System;
using PlayFab.SharedModels;

namespace PlayFab.MatchmakerModels
{
	[Serializable]
	public class PlayerLeftRequest : PlayFabRequestCommon
	{
		public string LobbyId;

		public string PlayFabId;
	}
}
