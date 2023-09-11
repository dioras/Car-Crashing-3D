using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GrantItemsToUsersRequest : PlayFabRequestCommon
	{
		public string CatalogVersion;

		public List<ItemGrant> ItemGrants;
	}
}
