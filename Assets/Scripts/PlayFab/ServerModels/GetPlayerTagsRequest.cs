using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetPlayerTagsRequest : PlayFabRequestCommon
	{
		public string Namespace;

		public string PlayFabId;
	}
}
