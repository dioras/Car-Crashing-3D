using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class ReportPlayerServerResult : PlayFabResultCommon
	{
		public int SubmissionsRemaining;
	}
}
