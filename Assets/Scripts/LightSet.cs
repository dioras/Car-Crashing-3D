using System;
using UnityEngine;

[Serializable]
public class LightSet
{
	public void SetLightState(LightState state)
	{
		this.lightState = state;
		this.UpdateLights();
	}

	public void SetBrightness(float value)
	{
		this.Brightness = value;
		this.UpdateLights();
	}

	public void UpdateLights()
	{
		foreach (LensFlare lensFlare in this.PreStageLights)
		{
			lensFlare.brightness = ((this.lightState <= LightState.ShutAll) ? 0f : this.Brightness);
		}
		foreach (LensFlare lensFlare2 in this.StageLights)
		{
			lensFlare2.brightness = ((this.lightState <= LightState.PreStage) ? 0f : this.Brightness);
		}
		this.Countdown3Light.brightness = ((this.lightState <= LightState.Stage || this.lightState >= LightState.Start) ? 0f : this.Brightness);
		this.Countdown2Light.brightness = ((this.lightState <= LightState.Countdown3 || this.lightState >= LightState.Start) ? 0f : this.Brightness);
		this.Countdown1Light.brightness = ((this.lightState <= LightState.Countdown2 || this.lightState >= LightState.Start) ? 0f : this.Brightness);
		this.StartLight.brightness = ((this.lightState <= LightState.Countdown1) ? 0f : this.Brightness);
	}

	public void ShutAllLights()
	{
		this.SetLightState(LightState.ShutAll);
	}

	private float Brightness;

	private LightState lightState;

	public LensFlare[] PreStageLights;

	public LensFlare[] StageLights;

	public LensFlare Countdown3Light;

	public LensFlare Countdown2Light;

	public LensFlare Countdown1Light;

	public LensFlare StartLight;
}
