using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class AddPlayerTagRequest : PlayFabRequestCommon
	{
		public string PlayFabId;

		public string TagName;
	}
}
