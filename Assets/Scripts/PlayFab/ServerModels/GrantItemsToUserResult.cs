using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GrantItemsToUserResult : PlayFabResultCommon
	{
		public List<GrantedItemInstance> ItemGrantResults;
	}
}
