using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetRandomResultTablesRequest : PlayFabRequestCommon
	{
		public string CatalogVersion;

		public List<string> TableIDs;
	}
}
