using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetPlayerStatisticDefinitionsResult : PlayFabResultCommon
	{
		public List<PlayerStatisticDefinition> Statistics;
	}
}
