using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetContentUploadUrlRequest : PlayFabRequestCommon
	{
		public string ContentType;

		public string Key;
	}
}
