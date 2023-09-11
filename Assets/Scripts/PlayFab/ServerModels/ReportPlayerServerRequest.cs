using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class ReportPlayerServerRequest : PlayFabRequestCommon
	{
		public string Comment;

		public string ReporteeId;

		public string ReporterId;
	}
}
