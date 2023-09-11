using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetCatalogItemsResult : PlayFabResultCommon
	{
		public List<CatalogItem> Catalog;
	}
}
