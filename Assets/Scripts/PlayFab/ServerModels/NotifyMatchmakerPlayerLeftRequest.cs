using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class NotifyMatchmakerPlayerLeftRequest : PlayFabRequestCommon
	{
		public string LobbyId;

		public string PlayFabId;
	}
}
