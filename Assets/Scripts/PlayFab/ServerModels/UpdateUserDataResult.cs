using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class UpdateUserDataResult : PlayFabResultCommon
	{
		public uint DataVersion;
	}
}
