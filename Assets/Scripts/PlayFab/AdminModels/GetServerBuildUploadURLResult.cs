using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetServerBuildUploadURLResult : PlayFabResultCommon
	{
		public string URL;
	}
}
