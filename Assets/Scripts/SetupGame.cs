using System;
using UnityEngine;

public class SetupGame : MonoBehaviour
{
	private void OnEnable()
	{
		UnityEngine.Debug.Log("Setup game running");
		Application.targetFrameRate = 60;
		Screen.autorotateToLandscapeLeft = true;
		Screen.autorotateToLandscapeRight = true;
		if (Screen.width >= this.minimumResolution)
		{
			Screen.SetResolution(Screen.width / 2, Screen.height / 2, true);
		}
		Screen.sleepTimeout = -1;
	}

	public int minimumResolution = 1920;
}
