using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetFriendsListResult : PlayFabResultCommon
	{
		public List<FriendInfo> Friends;
	}
}
