using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class UnlockContainerItemRequest : PlayFabRequestCommon
	{
		public string CatalogVersion;

		public string CharacterId;

		public string ContainerItemId;

		public string PlayFabId;
	}
}
