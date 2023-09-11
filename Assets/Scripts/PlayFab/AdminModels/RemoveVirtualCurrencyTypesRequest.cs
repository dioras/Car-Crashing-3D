using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class RemoveVirtualCurrencyTypesRequest : PlayFabRequestCommon
	{
		public List<VirtualCurrencyData> VirtualCurrencies;
	}
}
