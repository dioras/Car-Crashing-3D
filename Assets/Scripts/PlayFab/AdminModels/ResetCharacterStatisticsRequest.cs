using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class ResetCharacterStatisticsRequest : PlayFabRequestCommon
	{
		public string CharacterId;

		public string PlayFabId;
	}
}
