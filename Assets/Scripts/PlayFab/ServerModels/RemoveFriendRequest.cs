using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class RemoveFriendRequest : PlayFabRequestCommon
	{
		public string FriendPlayFabId;

		public string PlayFabId;
	}
}
