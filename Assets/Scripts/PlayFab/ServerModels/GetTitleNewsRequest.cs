using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetTitleNewsRequest : PlayFabRequestCommon
	{
		public int? Count;
	}
}
