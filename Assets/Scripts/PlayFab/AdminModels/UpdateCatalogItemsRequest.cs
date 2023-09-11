using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class UpdateCatalogItemsRequest : PlayFabRequestCommon
	{
		public List<CatalogItem> Catalog;

		public string CatalogVersion;

		public bool? SetAsDefaultCatalog;
	}
}
