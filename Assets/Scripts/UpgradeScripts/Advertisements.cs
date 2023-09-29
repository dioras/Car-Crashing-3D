using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Advertisements : MonoBehaviour
{
	public static Advertisements Instance { private set; get; }
	private Action onRVComplete;

	private void Awake() {
		if (Instance == null) {
			Instance = this;
			//IronSource.Agent.setMetaData("is_test_suite", "enable");
			
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}

	private void Start() {


#if UNITY_ANDROID
        string appKey = "19a3568a5";
#elif UNITY_IPHONE
        string appKey = "";
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
	}

	private void OnApplicationPause(bool pause) {
		IronSource.Agent.onApplicationPause(pause);
	}

	public void ShowInterstitial() {
        if(!IsInterstitialAvailable()) return;
		IronSource.Agent.showInterstitial();
	}

    public bool IsInterstitialAvailable(){
        return IronSource.Agent.isInterstitialReady();
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
		Debug.LogError("OnSDKInitializationComplete");
		//IronSource.Agent.launchTestSuite();
	}
}