using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetCloudScriptTaskInstanceResult : PlayFabResultCommon
	{
		public CloudScriptTaskParameter Parameter;

		public CloudScriptTaskSummary Summary;
	}
}
