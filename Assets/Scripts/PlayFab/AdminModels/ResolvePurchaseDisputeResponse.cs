using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class ResolvePurchaseDisputeResponse : PlayFabResultCommon
	{
		public string PurchaseStatus;
	}
}
