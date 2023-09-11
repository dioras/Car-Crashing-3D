using System;
using System.Runtime.CompilerServices;
using PlayFab.ClientModels;
using PlayFab.SharedModels;
using UnityEngine;

namespace PlayFab.Internal
{
	public static class PlayFabDeviceUtil
	{
		private static void DoAttributeInstall()
		{
			if (!PlayFabDeviceUtil._needsAttribution || PlayFabSettings.DisableAdvertising)
			{
				return;
			}
			AttributeInstallRequest attributeInstallRequest = new AttributeInstallRequest();
			string advertisingIdType = PlayFabSettings.AdvertisingIdType;
			if (advertisingIdType != null)
			{
				if (!(advertisingIdType == "Adid"))
				{
					if (advertisingIdType == "Idfa")
					{
						attributeInstallRequest.Idfa = PlayFabSettings.AdvertisingIdValue;
					}
				}
				else
				{
					attributeInstallRequest.Adid = PlayFabSettings.AdvertisingIdValue;
				}
			}
			AttributeInstallRequest request = attributeInstallRequest;
			if (PlayFabDeviceUtil._003C_003Ef__mg_0024cache0 == null)
			{
				PlayFabDeviceUtil._003C_003Ef__mg_0024cache0 = new Action<AttributeInstallResult>(PlayFabDeviceUtil.OnAttributeInstall);
			}
			PlayFabClientAPI.AttributeInstall(request, PlayFabDeviceUtil._003C_003Ef__mg_0024cache0, null, null, null);
		}

		private static void OnAttributeInstall(AttributeInstallResult result)
		{
			PlayFabSettings.AdvertisingIdType += "_Successful";
		}

		private static void SendDeviceInfoToPlayFab()
		{
			if (PlayFabSettings.DisableDeviceInfo || !PlayFabDeviceUtil._gatherInfo)
			{
				return;
			}
			PlayFabDeviceUtil.DeviceInfoRequest deviceInfoRequest = new PlayFabDeviceUtil.DeviceInfoRequest
			{
				Info = new PlayFabDataGatherer()
			};
			string apiEndpoint = "/Client/ReportDeviceInfo";
			PlayFabRequestCommon request = deviceInfoRequest;
			AuthType authType = AuthType.LoginSession;
			if (PlayFabDeviceUtil._003C_003Ef__mg_0024cache1 == null)
			{
				PlayFabDeviceUtil._003C_003Ef__mg_0024cache1 = new Action<EmptyResult>(PlayFabDeviceUtil.OnGatherSuccess);
			}
			Action<EmptyResult> resultCallback = PlayFabDeviceUtil._003C_003Ef__mg_0024cache1;
			if (PlayFabDeviceUtil._003C_003Ef__mg_0024cache2 == null)
			{
				PlayFabDeviceUtil._003C_003Ef__mg_0024cache2 = new Action<PlayFabError>(PlayFabDeviceUtil.OnGatherFail);
			}
			PlayFabHttp.MakeApiCall<EmptyResult>(apiEndpoint, request, authType, resultCallback, PlayFabDeviceUtil._003C_003Ef__mg_0024cache2, null, null, false);
		}

		private static void OnGatherSuccess(EmptyResult result)
		{
			UnityEngine.Debug.Log("OnGatherSuccess");
		}

		private static void OnGatherFail(PlayFabError error)
		{
			UnityEngine.Debug.Log("OnGatherFail: " + error.GenerateErrorReport());
		}

		public static void OnPlayFabLogin(PlayFabResultCommon result)
		{
			LoginResult loginResult = result as LoginResult;
			RegisterPlayFabUserResult registerPlayFabUserResult = result as RegisterPlayFabUserResult;
			if (loginResult == null && registerPlayFabUserResult == null)
			{
				return;
			}
			PlayFabDeviceUtil._needsAttribution = false;
			PlayFabDeviceUtil._gatherInfo = false;
			if (loginResult != null && loginResult.SettingsForUser != null)
			{
				PlayFabDeviceUtil._needsAttribution = loginResult.SettingsForUser.NeedsAttribution;
			}
			else if (registerPlayFabUserResult != null && registerPlayFabUserResult.SettingsForUser != null)
			{
				PlayFabDeviceUtil._needsAttribution = registerPlayFabUserResult.SettingsForUser.NeedsAttribution;
			}
			if (loginResult != null && loginResult.SettingsForUser != null)
			{
				PlayFabDeviceUtil._gatherInfo = loginResult.SettingsForUser.GatherDeviceInfo;
			}
			else if (registerPlayFabUserResult != null && registerPlayFabUserResult.SettingsForUser != null)
			{
				PlayFabDeviceUtil._gatherInfo = registerPlayFabUserResult.SettingsForUser.GatherDeviceInfo;
			}
			if (PlayFabSettings.AdvertisingIdType != null && PlayFabSettings.AdvertisingIdValue != null)
			{
				PlayFabDeviceUtil.DoAttributeInstall();
			}
			else
			{
				PlayFabDeviceUtil.GetAdvertIdFromUnity();
			}
			PlayFabDeviceUtil.SendDeviceInfoToPlayFab();
		}

		private static void GetAdvertIdFromUnity()
		{
			Application.RequestAdvertisingIdentifierAsync(delegate(string advertisingId, bool trackingEnabled, string error)
			{
				PlayFabSettings.DisableAdvertising = !trackingEnabled;
				if (!trackingEnabled)
				{
					return;
				}
				PlayFabSettings.AdvertisingIdType = "Adid";
				PlayFabSettings.AdvertisingIdValue = advertisingId;
				PlayFabDeviceUtil.DoAttributeInstall();
			});
		}

		private static bool _needsAttribution;

		private static bool _gatherInfo;

		[CompilerGenerated]
		private static Action<AttributeInstallResult> _003C_003Ef__mg_0024cache0;

		[CompilerGenerated]
		private static Action<EmptyResult> _003C_003Ef__mg_0024cache1;

		[CompilerGenerated]
		private static Action<PlayFabError> _003C_003Ef__mg_0024cache2;

		private class DeviceInfoRequest : PlayFabRequestCommon
		{
			public PlayFabDataGatherer Info;
		}
	}
}
