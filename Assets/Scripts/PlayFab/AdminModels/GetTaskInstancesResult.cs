using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetTaskInstancesResult : PlayFabResultCommon
	{
		public List<TaskInstanceBasicSummary> Summaries;
	}
}
