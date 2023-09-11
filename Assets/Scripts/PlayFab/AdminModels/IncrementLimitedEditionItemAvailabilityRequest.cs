using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class IncrementLimitedEditionItemAvailabilityRequest : PlayFabRequestCommon
	{
		public int Amount;

		public string CatalogVersion;

		public string ItemId;
	}
}
