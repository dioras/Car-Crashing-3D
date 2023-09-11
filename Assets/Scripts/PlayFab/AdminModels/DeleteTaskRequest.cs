using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class DeleteTaskRequest : PlayFabRequestCommon
	{
		public NameIdentifier Identifier;
	}
}
