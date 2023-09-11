using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class AbortTaskInstanceRequest : PlayFabRequestCommon
	{
		public string TaskInstanceId;
	}
}
