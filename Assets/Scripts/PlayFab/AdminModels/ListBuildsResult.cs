using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class ListBuildsResult : PlayFabResultCommon
	{
		public List<GetServerBuildInfoResult> Builds;
	}
}
