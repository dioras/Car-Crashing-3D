using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class DeleteUsersRequest : PlayFabRequestCommon
	{
		public List<string> PlayFabIds;

		public string TitleId;
	}
}
