using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetContentUploadUrlResult : PlayFabResultCommon
	{
		public string URL;
	}
}
