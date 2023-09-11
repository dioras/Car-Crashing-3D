using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class RefreshGameServerInstanceHeartbeatRequest : PlayFabRequestCommon
	{
		public string LobbyId;
	}
}
