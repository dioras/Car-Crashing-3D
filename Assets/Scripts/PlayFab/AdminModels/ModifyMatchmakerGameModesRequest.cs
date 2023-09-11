using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class ModifyMatchmakerGameModesRequest : PlayFabRequestCommon
	{
		public string BuildVersion;

		public List<GameModeInfo> GameModes;
	}
}
