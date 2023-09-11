using System;
using PlayFab.SharedModels;

namespace PlayFab.MatchmakerModels
{
	[Serializable]
	public class StartGameResponse : PlayFabResultCommon
	{
		public string GameID;

		public string ServerHostname;

		public string ServerIPV6Address;

		public uint ServerPort;
	}
}
