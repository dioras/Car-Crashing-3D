using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class SetPublisherDataRequest : PlayFabRequestCommon
	{
		public string Key;

		public string Value;
	}
}
