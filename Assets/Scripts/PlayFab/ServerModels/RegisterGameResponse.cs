using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class RegisterGameResponse : PlayFabResultCommon
	{
		public string LobbyId;
	}
}
