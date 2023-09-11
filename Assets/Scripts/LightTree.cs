using System;
using System.Collections.Generic;
using UnityEngine;

public class LightTree : MonoBehaviour
{
	private void Awake()
	{
		this.Player1Lights.ShutAllLights();
		this.Player2Lights.ShutAllLights();
		this.AllLights.Add(this.Player1Lights.Countdown1Light);
		this.AllLights.Add(this.Player1Lights.Countdown2Light);
		this.AllLights.Add(this.Player1Lights.Countdown3Light);
		this.AllLights.Add(this.Player1Lights.StartLight);
		this.AllLights.Add(this.Player1Lights.StageLights[0]);
		this.AllLights.Add(this.Player1Lights.StageLights[1]);
		this.AllLights.Add(this.Player1Lights.PreStageLights[0]);
		this.AllLights.Add(this.Player1Lights.PreStageLights[1]);
		this.AllLights.Add(this.Player2Lights.Countdown1Light);
		this.AllLights.Add(this.Player2Lights.Countdown2Light);
		this.AllLights.Add(this.Player2Lights.Countdown3Light);
		this.AllLights.Add(this.Player2Lights.StartLight);
		this.AllLights.Add(this.Player2Lights.StageLights[0]);
		this.AllLights.Add(this.Player2Lights.StageLights[1]);
		this.AllLights.Add(this.Player2Lights.PreStageLights[0]);
		this.AllLights.Add(this.Player2Lights.PreStageLights[1]);
		this.cam = Camera.main.transform;
	}

	private void Update()
	{
		float brightness = this.Brightness / Vector3.Distance(base.transform.position, this.cam.position);
		this.Player1Lights.SetBrightness(brightness);
		this.Player2Lights.SetBrightness(brightness);
		if (UnityEngine.Input.GetKeyDown(KeyCode.Keypad0))
		{
			this.Player1Lights.SetLightState(LightState.ShutAll);
			this.Player2Lights.SetLightState(LightState.ShutAll);
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.Keypad1))
		{
			this.Player1Lights.SetLightState(LightState.PreStage);
			this.Player2Lights.SetLightState(LightState.PreStage);
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.Keypad2))
		{
			this.Player1Lights.SetLightState(LightState.Stage);
			this.Player2Lights.SetLightState(LightState.Stage);
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.Keypad3))
		{
			this.Player1Lights.SetLightState(LightState.Countdown3);
			this.Player2Lights.SetLightState(LightState.Countdown3);
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.Keypad4))
		{
			this.Player1Lights.SetLightState(LightState.Countdown2);
			this.Player2Lights.SetLightState(LightState.Countdown2);
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.Keypad5))
		{
			this.Player1Lights.SetLightState(LightState.Countdown1);
			this.Player2Lights.SetLightState(LightState.Countdown1);
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.Keypad6))
		{
			this.Player1Lights.SetLightState(LightState.Start);
			this.Player2Lights.SetLightState(LightState.Start);
		}
	}

	public float Brightness = 1f;

	public LightSet Player1Lights;

	public LightSet Player2Lights;

	private List<LensFlare> AllLights = new List<LensFlare>();

	private Transform cam;
}
