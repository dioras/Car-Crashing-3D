using System;
using System.Collections;
using CustomVP;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
	private CarController carController
	{
		get
		{
			if (VehicleLoader.Instance != null)
			{
				return VehicleLoader.Instance.playerCarController;
			}
			return null;
		}
	}

	private PhotonTransformView photonTransformView
	{
		get
		{
			if (VehicleLoader.Instance != null)
			{
				return VehicleLoader.Instance.playerTView;
			}
			return null;
		}
	}

	private WeatherConrollerResources res
	{
		get
		{
			if (this._res == null)
			{
				this._res = (Resources.Load("WeatherControllerResources") as WeatherConrollerResources);
			}
			return this._res;
		}
	}

	public void ChangeRainState(bool r)
	{
		this.rainy = r;
	}

	public void ChangeWeatherImmediately(bool r)
	{
		this.rainy = r;
		this.overcast = (float)((!r) ? 0 : 1);
		this.UpdateWeather();
	}

	private void Awake()
	{
		WeatherController.Instance = this;
	}

	private void Start()
	{
		if (!PhotonNetwork.inRoom)
		{
			bool flag = UnityEngine.Random.Range(0f, 1f) < this.res.rainChance;
			this.rainy = flag;
			this.overcast = (float)((!this.rainy) ? 0 : 1);
		}
		else if (!PhotonNetwork.isMasterClient)
		{
			this.requestWeather = true;
		}
		this.skybox = RenderSettings.skybox;
		this.cam = CameraController.Instance;
		this.lut = SimpleLUT.Instance;
		if (this.skybox.shader.name != "Skybox/Blended")
		{
			UnityEngine.Debug.Log("Wrong skybox! Weather disabled. Use skybox with Blended shader.");
			base.enabled = false;
			return;
		}
		if (this.rain != null)
		{
			this.rainEm = this.rain.emission;
		}
		if (this.snow != null)
		{
			this.snowEm = this.snow.emission;
		}
		this.UpdateWeather();
	}

	private void Update()
	{
		if (this.requestWeather && this.photonTransformView != null)
		{
			this.requestWeather = false;
			this.photonTransformView.RequestCurrentWeather();
		}
		float target = (float)((!this.rainy) ? 0 : 1);
		if (!this.doingLightning)
		{
			this.overcast = Mathf.MoveTowards(this.overcast, target, Time.deltaTime * this.res.weatherChangeRate);
		}
		bool flag = false;
		if (PhotonNetwork.inRoom && PhotonNetwork.isMasterClient)
		{
			flag = true;
		}
		if (!PhotonNetwork.inRoom || flag)
		{
			this.rainCheckTimer += Time.deltaTime;
			if (this.rainCheckTimer >= this.res.rainCheckInterval)
			{
				this.rainCheckTimer = 0f;
				this.rainy = (UnityEngine.Random.Range(0f, 1f) < this.res.rainChance);
				if (flag)
				{
					this.photonTransformView.ChangeWeather(this.rainy);
				}
			}
		}
		if (this.overcast == 1f)
		{
			this.lightningTimer += Time.deltaTime;
			if (this.lightningTimer >= this.res.lightningInterval)
			{
				this.lightningTimer = 0f;
				if (UnityEngine.Random.Range(0f, 1f) < this.res.lightningChance)
				{
					base.StartCoroutine(this.LightningCor());
				}
			}
		}
		else
		{
			this.lightningTimer = 0f;
		}
		if (this.overcast != this._lastOvercast)
		{
			this.UpdateWeather();
		}
		if (this.rain != null)
		{
			this.rain.transform.position = this.cam.transform.position + Vector3.up * 10f + this.cam.transform.forward * 10f;
			this.rainEm.rateOverTime = Mathf.Lerp(0f, this.res.rainMaxEmission, Mathf.InverseLerp(0.5f, 1f, this.overcast)) * (1f - this.snowBlend);
			if (this.rainSound != null)
			{
				this.rainSound.volume = Mathf.InverseLerp(0.5f, 1f, this.overcast) * (1f - this.snowBlend);
			}
		}
		if (this.snow != null)
		{
			this.snow.transform.position = this.cam.transform.position + Vector3.up * 10f + this.cam.transform.forward * 10f;
			this.snowEm.rateOverTime = Mathf.Lerp(0f, this.res.snowMaxEmission, Mathf.InverseLerp(0.5f, 1f, this.overcast)) * this.snowBlend;
		}
		if (this.snowLine != null && this.carController != null)
		{
			this.snowBlend = Mathf.InverseLerp(this.snowLine.position.y - 15f, this.snowLine.position.y, this.carController.transform.position.y);
		}
		this._lastOvercast = this.overcast;
	}

	private void UpdateWeather()
	{
		this.skybox.SetFloat("_Blend", this.overcast);
		RenderSettings.fogColor = Color.Lerp(this.sunnyWeatherFogColor, this.overcastWeatherFogColor, this.overcast);
		if (this.sun != null)
		{
			this.sun.intensity = Mathf.Lerp(this.sunnyLightIntensity, this.overcastLightIntensity, this.overcast);
			this.sun.shadowStrength = Mathf.Lerp(1f, this.res.overcastShadowStrength, this.overcast);
		}
		if (this.lut != null && this.lut.enabled)
		{
			this.lut.Amount = Mathf.Lerp(this.sunnyLutAmount, 0f, this.overcast);
			this.lut.Brightness = Mathf.Lerp(this.sunnyBrightness, this.overcastBrightness, this.overcast);
		}
	}

	private IEnumerator LightningCor()
	{
		if (this.sun == null || this.lightningSource == null || this.res.lightningClips.Length == 0)
		{
			yield break;
		}
		this.doingLightning = true;
		AnimationCurve curve = this.res.lightningLightCurves[UnityEngine.Random.Range(0, this.res.lightningLightCurves.Length)];
		float length = curve.keys[curve.length - 1].time;
		float timer = 0f;
		float startLightIntensity = this.sun.intensity;
		this.lightningSource.clip = this.res.lightningClips[UnityEngine.Random.Range(0, this.res.lightningClips.Length)];
		this.lightningSource.Play();
		while (timer < length)
		{
			this.sun.intensity = startLightIntensity + curve.Evaluate(timer);
			timer += Time.deltaTime;
			yield return null;
		}
		this.doingLightning = false;
		yield break;
	}

	public static WeatherController Instance;

	private Material skybox;

	private SimpleLUT lut;

	private ParticleSystem.EmissionModule rainEm;

	private ParticleSystem.EmissionModule snowEm;

	private WeatherConrollerResources _res;

	[Header("References")]
	public Light sun;

	public ParticleSystem rain;

	public ParticleSystem snow;

	public AudioSource rainSound;

	public Transform snowLine;

	public AudioSource lightningSource;

	private CameraController cam;

	[Header("Lighting settings")]
	public Color sunnyWeatherFogColor;

	public Color overcastWeatherFogColor;

	public float sunnyLightIntensity;

	public float overcastLightIntensity;

	[Header("LUT Settings")]
	public float sunnyLutAmount;

	public float sunnyBrightness;

	public float overcastBrightness;

	[Header("Debug")]
	public bool rainy;

	private float lightningTimer;

	private bool doingLightning;

	private float rainCheckTimer;

	[HideInInspector]
	public float overcast;

	private float _lastOvercast;

	private float snowBlend;

	private bool requestWeather;
}
