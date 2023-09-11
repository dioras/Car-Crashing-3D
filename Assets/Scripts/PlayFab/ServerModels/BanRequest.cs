using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class BanRequest : PlayFabRequestCommon
	{
		public uint? DurationInHours;

		public string IPAddress;

		public string MACAddress;

		public string PlayFabId;

		public string Reason;
	}
}
