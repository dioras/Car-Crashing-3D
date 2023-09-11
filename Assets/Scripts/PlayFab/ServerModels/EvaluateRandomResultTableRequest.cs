using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class EvaluateRandomResultTableRequest : PlayFabRequestCommon
	{
		public string CatalogVersion;

		public string TableId;
	}
}
