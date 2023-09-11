using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetServerBuildUploadURLRequest : PlayFabRequestCommon
	{
		public string BuildId;
	}
}
