using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetServerBuildInfoRequest : PlayFabRequestCommon
	{
		public string BuildId;
	}
}
