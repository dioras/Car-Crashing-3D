using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class GetCharacterInventoryRequest : PlayFabRequestCommon
	{
		public string CatalogVersion;

		public string CharacterId;

		public string PlayFabId;
	}
}
