using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class LookupUserAccountInfoRequest : PlayFabRequestCommon
	{
		public string Email;

		public string PlayFabId;

		public string TitleDisplayName;

		public string Username;
	}
}
