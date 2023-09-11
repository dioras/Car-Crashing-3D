using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class DeregisterGameRequest : PlayFabRequestCommon
	{
		public string LobbyId;
	}
}
