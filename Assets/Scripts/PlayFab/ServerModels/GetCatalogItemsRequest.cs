using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetCatalogItemsRequest : PlayFabRequestCommon
	{
		public string CatalogVersion;
	}
}
