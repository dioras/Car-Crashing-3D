using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetPlayFabIDsFromFacebookIDsRequest : PlayFabRequestCommon
	{
		public List<string> FacebookIDs;
	}
}
