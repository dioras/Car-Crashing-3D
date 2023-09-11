using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class SetFriendTagsRequest : PlayFabRequestCommon
	{
		public string FriendPlayFabId;

		public string PlayFabId;

		public List<string> Tags;
	}
}
