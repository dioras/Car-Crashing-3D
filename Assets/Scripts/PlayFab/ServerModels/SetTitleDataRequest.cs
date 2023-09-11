using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class SetTitleDataRequest : PlayFabRequestCommon
	{
		public string Key;

		public string Value;
	}
}
