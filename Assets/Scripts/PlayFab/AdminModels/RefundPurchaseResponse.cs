using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class RefundPurchaseResponse : PlayFabResultCommon
	{
		public string PurchaseStatus;
	}
}
