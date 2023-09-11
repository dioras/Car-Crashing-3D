using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class WriteEventResponse : PlayFabResultCommon
	{
		public string EventId;
	}
}
