using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class AddCharacterVirtualCurrencyRequest : PlayFabRequestCommon
	{
		public int Amount;

		public string CharacterId;

		public string PlayFabId;

		public string VirtualCurrency;
	}
}
