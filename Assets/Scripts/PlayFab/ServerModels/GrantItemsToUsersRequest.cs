using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GrantItemsToUsersRequest : PlayFabRequestCommon
	{
		public string CatalogVersion;

		public List<ItemGrant> ItemGrants;
	}
}
