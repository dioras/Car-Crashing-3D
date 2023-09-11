using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetCloudScriptVersionsResult : PlayFabResultCommon
	{
		public List<CloudScriptVersionStatus> Versions;
	}
}
