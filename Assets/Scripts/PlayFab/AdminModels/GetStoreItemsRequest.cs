using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetStoreItemsRequest : PlayFabRequestCommon
	{
		public string CatalogVersion;

		public string StoreId;
	}
}
