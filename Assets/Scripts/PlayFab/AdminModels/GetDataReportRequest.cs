using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetDataReportRequest : PlayFabRequestCommon
	{
		public int Day;

		public int Month;

		public string ReportName;

		public int Year;
	}
}
