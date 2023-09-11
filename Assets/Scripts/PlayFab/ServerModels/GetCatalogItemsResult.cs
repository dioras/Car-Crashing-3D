using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetCatalogItemsResult : PlayFabResultCommon
	{
		public List<CatalogItem> Catalog;
	}
}
