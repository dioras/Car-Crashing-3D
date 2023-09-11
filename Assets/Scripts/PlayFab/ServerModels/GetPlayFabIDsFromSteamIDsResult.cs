using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetPlayFabIDsFromSteamIDsResult : PlayFabResultCommon
	{
		public List<SteamPlayFabIdPair> Data;
	}
}
