using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class DeletePlayerRequest : PlayFabRequestCommon
	{
		public string PlayFabId;
	}
}
