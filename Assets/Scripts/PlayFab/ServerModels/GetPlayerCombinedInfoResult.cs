using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetPlayerCombinedInfoResult : PlayFabResultCommon
	{
		public GetPlayerCombinedInfoResultPayload InfoResultPayload;

		public string PlayFabId;
	}
}
