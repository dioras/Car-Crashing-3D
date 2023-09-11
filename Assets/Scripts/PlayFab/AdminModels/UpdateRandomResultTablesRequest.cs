using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class UpdateRandomResultTablesRequest : PlayFabRequestCommon
	{
		public string CatalogVersion;

		public List<RandomResultTable> Tables;
	}
}
