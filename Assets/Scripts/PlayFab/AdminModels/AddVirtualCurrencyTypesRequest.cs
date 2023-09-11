using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class AddVirtualCurrencyTypesRequest : PlayFabRequestCommon
	{
		public List<VirtualCurrencyData> VirtualCurrencies;
	}
}
