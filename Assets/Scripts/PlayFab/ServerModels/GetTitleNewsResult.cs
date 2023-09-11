using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetTitleNewsResult : PlayFabResultCommon
	{
		public List<TitleNewsItem> News;
	}
}
