using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class UpdateUserTitleDisplayNameResult : PlayFabResultCommon
	{
		public string DisplayName;
	}
}
