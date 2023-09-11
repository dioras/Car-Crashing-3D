using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class CharacterResult : PlayFabResultCommon
	{
		public string CharacterId;

		public string CharacterName;

		public string CharacterType;
	}
}
