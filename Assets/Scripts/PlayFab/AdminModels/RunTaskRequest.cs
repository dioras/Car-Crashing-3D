using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class RunTaskRequest : PlayFabRequestCommon
	{
		public NameIdentifier Identifier;
	}
}
