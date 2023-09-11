using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetPlayerTagsRequest : PlayFabRequestCommon
	{
		public string Namespace;

		public string PlayFabId;
	}
}
