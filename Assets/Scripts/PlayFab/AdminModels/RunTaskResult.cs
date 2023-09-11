using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class RunTaskResult : PlayFabResultCommon
	{
		public string TaskInstanceId;
	}
}
