using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class UpdateAvatarUrlRequest : PlayFabRequestCommon
	{
		public string ImageUrl;

		public string PlayFabId;
	}
}
