using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class SetGameServerInstanceDataRequest : PlayFabRequestCommon
	{
		public string GameServerData;

		public string LobbyId;
	}
}
