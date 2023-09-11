using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class ListUsersCharactersResult : PlayFabResultCommon
	{
		public List<CharacterResult> Characters;
	}
}
