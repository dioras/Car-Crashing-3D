using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class CreateSharedGroupRequest : PlayFabRequestCommon
	{
		public string SharedGroupId;
	}
}
