using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class ModifyItemUsesRequest : PlayFabRequestCommon
	{
		public string ItemInstanceId;

		public string PlayFabId;

		public int UsesToAdd;
	}
}
