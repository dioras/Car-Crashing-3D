using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabLogin : MonoBehaviour
{
	private void Awake()
	{
		if (PlayFabLogin.Instance == null)
		{
			PlayFabLogin.Instance = this;
		}
	}

	public void Login()
	{
		PlayFabSettings.TitleId = "433F";
		if (PlayFabClientAPI.IsClientLoggedIn())
		{
			return;
		}
		LoginWithAndroidDeviceIDRequest request = new LoginWithAndroidDeviceIDRequest
		{
			AndroidDeviceId = SystemInfo.deviceUniqueIdentifier,
			CreateAccount = new bool?(true)
		};
		PlayFabClientAPI.LoginWithAndroidDeviceID(request, new Action<LoginResult>(this.OnLoginSuccess), new Action<PlayFabError>(this.OnLoginFailure), null, null);
	}

	private void OnLoginSuccess(LoginResult result)
	{
		UnityEngine.Debug.Log("Logged into PlayFab.");
		this.UpdateDisplayName();
	}

	private void OnLoginFailure(PlayFabError error)
	{
		UnityEngine.Debug.LogError("Login Error:");
		UnityEngine.Debug.LogError(error.GenerateErrorReport());
	}

	public void UpdateDisplayName()
	{
		PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
		{
			DisplayName = GameState.playerName
		}, delegate(UpdateUserTitleDisplayNameResult result)
		{
		}, delegate(PlayFabError error)
		{
			UnityEngine.Debug.LogError(error.GenerateErrorReport());
		}, null, null);
	}

	public static PlayFabLogin Instance;
}
