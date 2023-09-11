using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetRandomResultTablesResult : PlayFabResultCommon
	{
		public Dictionary<string, RandomResultTableListing> Tables;
	}
}
