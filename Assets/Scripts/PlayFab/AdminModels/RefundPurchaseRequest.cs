using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class RefundPurchaseRequest : PlayFabRequestCommon
	{
		public string OrderId;

		public string PlayFabId;

		public string Reason;
	}
}
