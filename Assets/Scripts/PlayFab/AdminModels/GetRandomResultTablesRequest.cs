using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetRandomResultTablesRequest : PlayFabRequestCommon
	{
		public string CatalogVersion;
	}
}
