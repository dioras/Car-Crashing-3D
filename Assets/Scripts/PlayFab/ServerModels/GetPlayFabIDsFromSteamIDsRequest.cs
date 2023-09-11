using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetPlayFabIDsFromSteamIDsRequest : PlayFabRequestCommon
	{
		public List<string> SteamStringIDs;
	}
}
