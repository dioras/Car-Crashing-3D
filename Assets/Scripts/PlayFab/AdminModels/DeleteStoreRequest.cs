using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class DeleteStoreRequest : PlayFabRequestCommon
	{
		public string CatalogVersion;

		public string StoreId;
	}
}
