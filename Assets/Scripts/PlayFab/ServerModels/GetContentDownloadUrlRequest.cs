using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetContentDownloadUrlRequest : PlayFabRequestCommon
	{
		public string HttpMethod;

		public string Key;

		public bool? ThruCDN;
	}
}
