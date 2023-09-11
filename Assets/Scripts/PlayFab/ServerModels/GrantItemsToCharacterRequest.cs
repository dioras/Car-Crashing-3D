using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GrantItemsToCharacterRequest : PlayFabRequestCommon
	{
		public string Annotation;

		public string CatalogVersion;

		public string CharacterId;

		public List<string> ItemIds;

		public string PlayFabId;
	}
}
