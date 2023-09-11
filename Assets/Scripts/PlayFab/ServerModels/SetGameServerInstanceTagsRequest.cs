using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class SetGameServerInstanceTagsRequest : PlayFabRequestCommon
	{
		public string LobbyId;

		public Dictionary<string, string> Tags;
	}
}
