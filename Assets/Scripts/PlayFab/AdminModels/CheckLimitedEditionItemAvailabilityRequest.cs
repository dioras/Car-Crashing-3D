using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class CheckLimitedEditionItemAvailabilityRequest : PlayFabRequestCommon
	{
		public string CatalogVersion;

		public string ItemId;
	}
}
