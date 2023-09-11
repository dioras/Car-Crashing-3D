using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class RedeemCouponRequest : PlayFabRequestCommon
	{
		public string CatalogVersion;

		public string CharacterId;

		public string CouponCode;

		public string PlayFabId;
	}
}
