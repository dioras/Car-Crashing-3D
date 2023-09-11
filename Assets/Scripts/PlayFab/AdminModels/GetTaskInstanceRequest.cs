using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetTaskInstanceRequest : PlayFabRequestCommon
	{
		public string TaskInstanceId;
	}
}
