using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class NotifyMatchmakerPlayerLeftResult : PlayFabResultCommon
	{
		public PlayerConnectionState? PlayerState;
	}
}
