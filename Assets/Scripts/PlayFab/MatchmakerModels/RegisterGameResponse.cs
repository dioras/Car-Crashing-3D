using System;
using PlayFab.SharedModels;

namespace PlayFab.MatchmakerModels
{
	[Serializable]
	public class RegisterGameResponse : PlayFabResultCommon
	{
		public string LobbyId;
	}
}
