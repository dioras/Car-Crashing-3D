using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class UpdateBanRequest : PlayFabRequestCommon
	{
		public bool? Active;

		public string BanId;

		public DateTime? Expires;

		public string IPAddress;

		public string MACAddress;

		public bool? Permanent;

		public string Reason;
	}
}
