using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetContentDownloadUrlResult : PlayFabResultCommon
	{
		public string URL;
	}
}
