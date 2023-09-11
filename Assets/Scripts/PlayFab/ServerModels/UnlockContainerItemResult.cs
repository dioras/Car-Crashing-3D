using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class UnlockContainerItemResult : PlayFabResultCommon
	{
		public List<ItemInstance> GrantedItems;

		public string UnlockedItemInstanceId;

		public string UnlockedWithItemInstanceId;

		public Dictionary<string, uint> VirtualCurrency;
	}
}
