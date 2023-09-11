using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class ModifyUserVirtualCurrencyResult : PlayFabResultCommon
	{
		public int Balance;

		public int BalanceChange;

		public string PlayFabId;

		public string VirtualCurrency;
	}
}
