using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class UpdatePlayerStatisticDefinitionResult : PlayFabResultCommon
	{
		public PlayerStatisticDefinition Statistic;
	}
}
