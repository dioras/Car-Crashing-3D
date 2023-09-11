using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetSharedGroupDataResult : PlayFabResultCommon
	{
		public Dictionary<string, SharedGroupDataRecord> Data;

		public List<string> Members;
	}
}
