using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class ResolvePurchaseDisputeRequest : PlayFabRequestCommon
	{
		public string OrderId;

		public ResolutionOutcome Outcome;

		public string PlayFabId;

		public string Reason;
	}
}
