using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class SetGameServerInstanceStateRequest : PlayFabRequestCommon
	{
		public string LobbyId;

		public GameInstanceState State;
	}
}
