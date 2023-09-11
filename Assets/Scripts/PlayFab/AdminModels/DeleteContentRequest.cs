using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class DeleteContentRequest : PlayFabRequestCommon
	{
		public string Key;
	}
}
