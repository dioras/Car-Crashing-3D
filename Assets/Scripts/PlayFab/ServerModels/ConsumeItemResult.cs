using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class ConsumeItemResult : PlayFabResultCommon
	{
		public string ItemInstanceId;

		public int RemainingUses;
	}
}
