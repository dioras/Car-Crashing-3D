using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class DeleteSharedGroupRequest : PlayFabRequestCommon
	{
		public string SharedGroupId;
	}
}
