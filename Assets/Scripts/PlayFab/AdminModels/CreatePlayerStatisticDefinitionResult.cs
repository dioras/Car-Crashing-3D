using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class CreatePlayerStatisticDefinitionResult : PlayFabResultCommon
	{
		public PlayerStatisticDefinition Statistic;
	}
}
