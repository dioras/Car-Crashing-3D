using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class ModifyCharacterVirtualCurrencyResult : PlayFabResultCommon
	{
		public int Balance;

		public string VirtualCurrency;
	}
}
