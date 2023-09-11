using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class UpdateStoreItemsRequest : PlayFabRequestCommon
	{
		public string CatalogVersion;

		public StoreMarketingModel MarketingData;

		public List<StoreItem> Store;

		public string StoreId;
	}
}
