using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetTimeResult : PlayFabResultCommon
	{
		public DateTime Time;
	}
}
