using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetUserInventoryRequest : PlayFabRequestCommon
	{
		public string PlayFabId;
	}
}
