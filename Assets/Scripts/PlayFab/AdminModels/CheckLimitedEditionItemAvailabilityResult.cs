using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class CheckLimitedEditionItemAvailabilityResult : PlayFabResultCommon
	{
		public int Amount;
	}
}
