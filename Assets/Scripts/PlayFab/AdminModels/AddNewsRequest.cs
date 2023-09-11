using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class AddNewsRequest : PlayFabRequestCommon
	{
		public string Body;

		public DateTime? Timestamp;

		public string Title;
	}
}
