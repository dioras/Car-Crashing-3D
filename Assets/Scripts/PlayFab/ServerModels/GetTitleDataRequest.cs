using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetTitleDataRequest : PlayFabRequestCommon
	{
		public List<string> Keys;
	}
}
