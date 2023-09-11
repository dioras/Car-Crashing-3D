using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class UpdateUserTitleDisplayNameRequest : PlayFabRequestCommon
	{
		public string DisplayName;

		public string PlayFabId;
	}
}
