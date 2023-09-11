using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetUserInventoryRequest : PlayFabRequestCommon
	{
		public string PlayFabId;
	}
}
