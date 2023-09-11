using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class UpdateUserInternalDataRequest : PlayFabRequestCommon
	{
		public Dictionary<string, string> Data;

		public List<string> KeysToRemove;

		public string PlayFabId;
	}
}
