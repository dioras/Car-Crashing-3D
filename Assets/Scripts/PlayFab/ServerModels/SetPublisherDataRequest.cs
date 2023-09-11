using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class SetPublisherDataRequest : PlayFabRequestCommon
	{
		public string Key;

		public string Value;
	}
}
