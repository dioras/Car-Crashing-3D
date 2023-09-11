using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetCatalogItemsRequest : PlayFabRequestCommon
	{
		public string CatalogVersion;
	}
}
