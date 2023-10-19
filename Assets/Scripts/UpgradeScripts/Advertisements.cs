using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;

public class Advertisements : MonoBehaviour
{
	public static Advertisements Instance { private set; get; }
	private Action onRVComplete;

	private void Awake() {
		if (Instance == null) {
			Instance = this;
			IronSource.Agent.setMetaData("is_test_suite", "enable");
			
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
		
	}

	private void Start() {


#if UNITY_ANDROID
        string appKey = "19a3568a5";
#elif UNITY_IPHONE
        string appKey = "19a3734e5";
#else
        string appKey = "unexpected_platform";
#endif

        Debug.Log("unity-script: IronSource.Agent.validateIntegration");
        IronSource.Agent.validateIntegration();

        // SDK init
        Debug.Log("unity-script: IronSource.Agent.init");
        IronSource.Agent.init(appKey);


		IronSourceEvents.onSdkInitializationCompletedEvent += OnSdkInitializationComplete;
		IronSourceRewardedVideoEvents.onAdRewardedEvent += OnRewardedVideoClosed;

		//Add AdInfo Interstitial Events
		IronSourceInterstitialEvents.onAdReadyEvent += InterstitialOnAdReadyEvent;
		IronSourceInterstitialEvents.onAdLoadFailedEvent += InterstitialOnAdLoadFailed;
		IronSourceInterstitialEvents.onAdOpenedEvent += InterstitialOnAdOpenedEvent;
		IronSourceInterstitialEvents.onAdClickedEvent += InterstitialOnAdClickedEvent;
		IronSourceInterstitialEvents.onAdShowSucceededEvent += InterstitialOnAdShowSucceededEvent;
		IronSourceInterstitialEvents.onAdShowFailedEvent += InterstitialOnAdShowFailedEvent;
		IronSourceInterstitialEvents.onAdClosedEvent += InterstitialOnAdClosedEvent;
		
		InitUGS();
	}
	
	private async void InitUGS()
	{
		string environment = "production";
		try
		{
			var options = new InitializationOptions()
				.SetEnvironmentName(environment);

			await UnityServices.InitializeAsync(options);
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	/************* Interstitial AdInfo Delegates *************/
	// Invoked when the interstitial ad was loaded succesfully.
	void InterstitialOnAdReadyEvent(IronSourceAdInfo adInfo) {
		Debug.Log("Inter loaded success!!");
	}
	 // Invoked when the initialization process has failed.
	void InterstitialOnAdLoadFailed(IronSourceError ironSourceError) {
		Debug.LogError("Inter loaded failed!!");
	}
	// Invoked when the Interstitial Ad Unit has opened. This is the impression indication. 
	void InterstitialOnAdOpenedEvent(IronSourceAdInfo adInfo) {
	}
	// Invoked when end user clicked on the interstitial ad
	void InterstitialOnAdClickedEvent(IronSourceAdInfo adInfo) {
	}
	// Invoked when the ad failed to show.
	void InterstitialOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo) {
		Debug.LogError("Inter failed to SHOW");
	}
	// Invoked when the interstitial ad closed and the user went back to the application screen.
	void InterstitialOnAdClosedEvent(IronSourceAdInfo adInfo) {
		IronSource.Agent.loadInterstitial();
	}
	// Invoked before the interstitial ad was opened, and before the InterstitialOnAdOpenedEvent is reported.
	// This callback is not supported by all networks, and we recommend using it only if  
	// it's supported by all networks you included in your build. 
	void InterstitialOnAdShowSucceededEvent(IronSourceAdInfo adInfo) {
	}

	private void OnApplicationPause(bool pause) {
		IronSource.Agent.onApplicationPause(pause);
	}

	public void ShowInterstitial() {
        if(IronSource.Agent.isInterstitialReady())
		{
			Debug.Log("Viewing interstitial ads");
			IronSource.Agent.showInterstitial();
		}
		else
		{
			Debug.Log("interstitial ads are not ready");
		}
	}

	public bool IsRewardedAvailable() {
		return IronSource.Agent.isRewardedVideoAvailable();
	}

	public void ShowRewardedVideo(Action _onComplete) {
		if (IsRewardedAvailable()) {
			onRVComplete = _onComplete;
			IronSource.Agent.showRewardedVideo();
		}
	}

	private void OnRewardedVideoClosed(IronSourcePlacement placement, IronSourceAdInfo adInfo) {
		onRVComplete?.Invoke();
	}

	private void OnSdkInitializationComplete() {
		Debug.Log("OnSDKInitializationComplete");
		IronSource.Agent.launchTestSuite();
		IronSource.Agent.loadInterstitial();
	}
}