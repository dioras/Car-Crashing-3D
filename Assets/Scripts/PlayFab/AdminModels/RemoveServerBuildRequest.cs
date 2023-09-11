using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class RemoveServerBuildRequest : PlayFabRequestCommon
	{
		public string BuildId;
	}
}
