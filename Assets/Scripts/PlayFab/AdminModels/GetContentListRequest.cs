using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetContentListRequest : PlayFabRequestCommon
	{
		public string Prefix;
	}
}
