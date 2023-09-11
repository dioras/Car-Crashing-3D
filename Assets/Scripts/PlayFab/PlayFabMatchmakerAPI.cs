using System;
using System.Collections.Generic;
using PlayFab.Internal;
using PlayFab.MatchmakerModels;

namespace PlayFab
{
	public static class PlayFabMatchmakerAPI
	{
		public static void ForgetAllCredentials()
		{
			PlayFabHttp.ForgetAllCredentials();
		}

		public static void AuthUser(AuthUserRequest request, Action<AuthUserResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<AuthUserResponse>("/Matchmaker/AuthUser", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void PlayerJoined(PlayerJoinedRequest request, Action<PlayerJoinedResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<PlayerJoinedResponse>("/Matchmaker/PlayerJoined", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void PlayerLeft(PlayerLeftRequest request, Action<PlayerLeftResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<PlayerLeftResponse>("/Matchmaker/PlayerLeft", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void StartGame(StartGameRequest request, Action<StartGameResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<StartGameResponse>("/Matchmaker/StartGame", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UserInfo(UserInfoRequest request, Action<UserInfoResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UserInfoResponse>("/Matchmaker/UserInfo", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}
	}
}
