using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetTitleDataRequest : PlayFabRequestCommon
	{
		public List<string> Keys;
	}
}
