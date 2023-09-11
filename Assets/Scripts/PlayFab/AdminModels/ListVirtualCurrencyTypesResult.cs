using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class ListVirtualCurrencyTypesResult : PlayFabResultCommon
	{
		public List<VirtualCurrencyData> VirtualCurrencies;
	}
}
