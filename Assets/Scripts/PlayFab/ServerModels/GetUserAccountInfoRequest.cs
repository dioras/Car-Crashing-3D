using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetUserAccountInfoRequest : PlayFabRequestCommon
	{
		public string PlayFabId;
	}
}
