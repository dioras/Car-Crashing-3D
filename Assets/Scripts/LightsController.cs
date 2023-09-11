using System;
using CustomVP;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class LightsController : MonoBehaviour
{
	[ContextMenu("Shut all lights")]
	private void Shut()
	{
		foreach (LensFlare lensFlare in base.GetComponentsInChildren<LensFlare>(true))
		{
			lensFlare.brightness = 0f;
		}
	}

	public float LightsState
	{
		get
		{
			return (float)((!this.LightsOn) ? 0 : 1);
		}
		set
		{
			this.LightsOn = (value == 1f);
		}
	}

	private void Start()
	{
		this.mainCamera = Camera.main;
		this.photonTransformView = base.GetComponent<PhotonTransformView>();
		this.carController = base.GetComponent<CarController>();
		foreach (LensFlare lensFlare in base.GetComponentsInChildren<LensFlare>())
		{
			lensFlare.fadeSpeed = 100f;
		}
	}

	private void OnDestroy()
	{
		if (this.FLTurnLight != null)
		{
			this.FLTurnLight.enabled = false;
		}
		if (this.FRTurnLight != null)
		{
			this.FRTurnLight.enabled = false;
		}
		if (this.RLTurnLight != null)
		{
			this.RLTurnLight.enabled = false;
		}
		if (this.RRTurnLight != null)
		{
			this.RRTurnLight.enabled = false;
		}
		foreach (LensFlare lensFlare in this.BrakeLights)
		{
			lensFlare.enabled = false;
		}
		foreach (LensFlare lensFlare2 in this.RoofLights)
		{
			lensFlare2.enabled = false;
		}
		foreach (SpriteRenderer spriteRenderer in this.LightBars)
		{
			spriteRenderer.enabled = false;
		}
		foreach (LensFlare lensFlare3 in this.HeadLights)
		{
			lensFlare3.enabled = false;
		}
		for (int m = 0; m < this.PoliceLights.Length; m++)
		{
			this.PoliceLights[m].enabled = false;
		}
	}

	private void Update()
	{
		if (this.mainCamera == null)
		{
			return;
		}
		float num = Vector3.Distance(base.transform.position, this.mainCamera.transform.position);
		float target = 0f;
		if (this.HeadLights.Length > 0)
		{
			target = (float)((Vector3.Angle(base.transform.forward, this.mainCamera.transform.position - this.HeadLights[0].transform.position) >= 90f) ? 0 : 1);
		}
		else if (this.RoofLights.Length > 0)
		{
			target = (float)((Vector3.Angle(base.transform.forward, this.mainCamera.transform.position - this.RoofLights[0].transform.position) >= 90f) ? 0 : 1);
		}
		float target2 = 0f;
		if (this.BrakeLights.Length > 0)
		{
			target2 = (float)((Vector3.Angle(-base.transform.forward, this.mainCamera.transform.position - this.BrakeLights[0].transform.position) >= 90f) ? 0 : 1);
		}
		this.FrontVisiblityMultiplier = Mathf.MoveTowards(this.FrontVisiblityMultiplier, target, Time.deltaTime * 5f);
		this.RearVisiblityMultiplier = Mathf.MoveTowards(this.RearVisiblityMultiplier, target2, Time.deltaTime * 5f);
		float num2 = 1f;
		RaycastHit raycastHit;
		if (Physics.Raycast(this.mainCamera.transform.position, base.transform.position - this.mainCamera.transform.position, out raycastHit) && raycastHit.collider.transform.root != base.transform)
		{
			num2 = 0f;
		}
		float target3 = (float)((!this.LightsOn) ? 0 : 1);
		this.MasterBrightnessMultiplier = Mathf.MoveTowards(this.MasterBrightnessMultiplier, target3, Time.deltaTime * 5f) * num2;
		if (this.HeadLights.Length > 0)
		{
			foreach (LensFlare lensFlare in this.HeadLights)
			{
				lensFlare.brightness = Mathf.Max(this.minSize, this.HeadlightsBrightness / num) * this.FrontVisiblityMultiplier * this.MasterBrightnessMultiplier;
			}
		}
		float num3;
		if (this.carController != null)
		{
			num3 = this.carController.Steering;
		}
		else
		{
			num3 = this.photonTransformView.lastSteeringAngle;
		}
		float num4 = (float)((num3 >= -15f || Mathf.PingPong(Time.time, 0.5f) <= 0.25f) ? 0 : 1);
		float num5 = (float)((num3 <= 15f || Mathf.PingPong(Time.time, 0.5f) <= 0.25f) ? 0 : 1);
		if (this.FLTurnLight != null)
		{
			this.FLTurnLight.brightness = Mathf.Max(this.minSize, this.TurnlightsBrightness / num) * this.FrontVisiblityMultiplier * this.MasterBrightnessMultiplier * num4;
		}
		if (this.FRTurnLight != null)
		{
			this.FRTurnLight.brightness = Mathf.Max(this.minSize, this.TurnlightsBrightness / num) * this.FrontVisiblityMultiplier * this.MasterBrightnessMultiplier * num5;
		}
		if (this.RLTurnLight != null)
		{
			this.RLTurnLight.brightness = Mathf.Max(this.minSize, this.TurnlightsBrightness / num) * this.RearVisiblityMultiplier * this.MasterBrightnessMultiplier * num4;
		}
		if (this.RRTurnLight != null)
		{
			this.RRTurnLight.brightness = Mathf.Max(this.minSize, this.TurnlightsBrightness / num) * this.RearVisiblityMultiplier * this.MasterBrightnessMultiplier * num5;
		}
		float num6 = 0.5f;
		if (this.carController != null)
		{
			num6 = 0.5f + this.carController.Braking / 2f;
		}
		if (this.BrakeLights.Length > 0)
		{
			foreach (LensFlare lensFlare2 in this.BrakeLights)
			{
				lensFlare2.brightness = Mathf.Max(this.minSize, this.BrakeBrightness / num) * this.RearVisiblityMultiplier * this.MasterBrightnessMultiplier * num6;
			}
		}
		if (this.RoofLights.Length > 0)
		{
			foreach (LensFlare lensFlare3 in this.RoofLights)
			{
				lensFlare3.brightness = Mathf.Max(this.minSize, this.RooflightsBrightness / num) * this.FrontVisiblityMultiplier * this.MasterBrightnessMultiplier * 0.5f;
			}
		}
		if (this.PoliceLights.Length > 0)
		{
			for (int l = 0; l < this.PoliceLights.Length; l++)
			{
				this.PoliceLights[l].brightness = Mathf.Max(this.minSize, this.RooflightsBrightness / num) * this.MasterBrightnessMultiplier * Mathf.Abs((float)l - Mathf.PingPong(Time.time * 10f, 1f));
			}
		}
		foreach (SpriteRenderer spriteRenderer in this.LightBars)
		{
			spriteRenderer.enabled = this.LightsOn;
		}
		if (CrossPlatformInputManager.GetButtonDown("ToggleLights") && this.carController != null)
		{
			this.LightsOn = !this.LightsOn;
			if (this.photonTransformView.enabled)
			{
				this.photonTransformView.SendLightsChangingEvent(this.LightsState);
			}
		}
	}

	private PhotonTransformView photonTransformView;

	private CarController carController;

	private Camera mainCamera;

	public LensFlare[] HeadLights;

	public LensFlare[] BrakeLights;

	public LensFlare[] RoofLights;

	public LensFlare[] PoliceLights;

	public LensFlare FLTurnLight;

	public LensFlare FRTurnLight;

	public LensFlare RLTurnLight;

	public LensFlare RRTurnLight;

	public SpriteRenderer[] LightBars;

	private float MasterBrightnessMultiplier;

	private float FrontVisiblityMultiplier;

	private float RearVisiblityMultiplier;

	public float HeadlightsBrightness = 5f;

	public float BrakeBrightness = 5f;

	public float RooflightsBrightness = 5f;

	public float TurnlightsBrightness = 5f;

	public bool LightsOn;

	private float minSize;
}
