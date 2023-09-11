using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetStoreItemsResult : PlayFabResultCommon
	{
		public string CatalogVersion;

		public StoreMarketingModel MarketingData;

		public SourceType? Source;

		public List<StoreItem> Store;

		public string StoreId;
	}
}
