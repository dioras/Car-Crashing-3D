using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetTasksRequest : PlayFabRequestCommon
	{
		public NameIdentifier Identifier;
	}
}
