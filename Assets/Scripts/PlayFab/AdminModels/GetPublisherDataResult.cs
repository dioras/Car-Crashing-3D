using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetPublisherDataResult : PlayFabResultCommon
	{
		public Dictionary<string, string> Data;
	}
}
