using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetRandomResultTablesResult : PlayFabResultCommon
	{
		public Dictionary<string, RandomResultTableListing> Tables;
	}
}
