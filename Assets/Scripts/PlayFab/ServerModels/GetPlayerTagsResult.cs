using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetPlayerTagsResult : PlayFabResultCommon
	{
		public string PlayFabId;

		public List<string> Tags;
	}
}
