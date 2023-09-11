using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class ModifyItemUsesResult : PlayFabResultCommon
	{
		public string ItemInstanceId;

		public int RemainingUses;
	}
}
