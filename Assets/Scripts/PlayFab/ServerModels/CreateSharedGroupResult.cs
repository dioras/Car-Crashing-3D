using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class CreateSharedGroupResult : PlayFabResultCommon
	{
		public string SharedGroupId;
	}
}
