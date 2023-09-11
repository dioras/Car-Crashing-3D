using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class SetTitleDataRequest : PlayFabRequestCommon
	{
		public string Key;

		public string Value;
	}
}
