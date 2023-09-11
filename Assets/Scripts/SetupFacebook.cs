using System;
using Facebook.Unity;
using UnityEngine;

public class SetupFacebook : MonoBehaviour
{
	private void Start()
	{
		FB.Init(new InitDelegate(this.InitCallback), null, null);
	}

	private void Update()
	{
	}

	private void InitCallback()
	{
		if (FB.IsInitialized)
		{
			UnityEngine.Debug.Log("Facebook initialized");
			FB.ActivateApp();
		}
		else
		{
			UnityEngine.Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}
}
