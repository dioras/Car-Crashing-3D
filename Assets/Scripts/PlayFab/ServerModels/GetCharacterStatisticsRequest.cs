using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetCharacterStatisticsRequest : PlayFabRequestCommon
	{
		public string CharacterId;

		public string PlayFabId;
	}
}
