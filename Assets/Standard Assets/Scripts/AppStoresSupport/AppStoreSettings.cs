using System;
using UnityEngine;
using UnityEngine.Store;

namespace AppStoresSupport
{
	[Serializable]
	public class AppStoreSettings : ScriptableObject
	{
		public AppInfo getAppInfo()
		{
			return new AppInfo
			{
				clientId = this.UnityClientID,
				clientKey = this.UnityClientKey,
				appId = this.XiaomiAppStoreSetting.AppID,
				appKey = this.XiaomiAppStoreSetting.AppKey,
				debug = this.XiaomiAppStoreSetting.IsTestMode
			};
		}

		public string UnityClientID = string.Empty;

		public string UnityClientKey = string.Empty;

		public string UnityClientRSAPublicKey = string.Empty;

		public AppStoreSetting XiaomiAppStoreSetting = new AppStoreSetting();
	}
}
