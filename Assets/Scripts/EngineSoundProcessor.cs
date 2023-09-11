using System;
using CustomVP;
using UnityEngine;

public class EngineSoundProcessor : MonoBehaviour
{
	private void Start()
	{
		this.carController = base.GetComponentInParent<CarController>();
		this.LoadSounds();
	}

	private void LoadSounds()
	{
		this.idleSound = this.CreateAudioSource(this.idleClip);
		this.lowOffSound = this.CreateAudioSource(this.lowOffClip);
		this.lowOnSound = this.CreateAudioSource(this.lowOnClip);
		this.medOffSound = this.CreateAudioSource(this.medOffClip);
		this.medOnSound = this.CreateAudioSource(this.medOnClip);
		this.highOffSound = this.CreateAudioSource(this.highOffClip);
		this.highOnSound = this.CreateAudioSource(this.highOnClip);
		this.limiterSound = this.CreateAudioSource(this.maxRPMClip);
		this.turboSound = this.CreateAudioSource(this.turboClip);
		this.gearShiftSound = (UnityEngine.Object.Instantiate(Resources.Load("Sounds/GearShift", typeof(GameObject)), base.transform.position, base.transform.rotation, base.transform) as GameObject).GetComponent<AudioSource>();
		this.SoundsLoaded = true;
	}

	private void DestroySounds()
	{
		if (this.SoundsDestroyed)
		{
			return;
		}
		UnityEngine.Object.Destroy(this.lowOnSound);
		UnityEngine.Object.Destroy(this.medOnSound);
		UnityEngine.Object.Destroy(this.highOnSound);
		UnityEngine.Object.Destroy(this.limiterSound);
		UnityEngine.Object.Destroy(this.turboSound);
		UnityEngine.Object.Destroy(this.gearShiftSound);
	}

	private void OnEnable()
	{
		if (!this.SoundsLoaded)
		{
			return;
		}
		this.idleSound.Play();
		this.lowOffSound.Play();
		this.lowOnSound.Play();
		this.medOffSound.Play();
		this.medOnSound.Play();
		this.highOffSound.Play();
		this.highOnSound.Play();
		this.limiterSound.Play();
		this.turboSound.Play();
	}

	private void OnDisable()
	{
		if (!this.SoundsLoaded)
		{
			return;
		}
		if (this.idleSound != null)
		{
			this.idleSound.Stop();
		}
		if (this.lowOffSound != null)
		{
			this.lowOffSound.Stop();
		}
		if (this.lowOnSound != null)
		{
			this.lowOnSound.Stop();
		}
		if (this.medOffSound != null)
		{
			this.medOffSound.Stop();
		}
		if (this.medOnSound != null)
		{
			this.medOnSound.Stop();
		}
		if (this.highOffSound != null)
		{
			this.highOffSound.Stop();
		}
		if (this.highOnSound != null)
		{
			this.highOnSound.Stop();
		}
		if (this.limiterSound != null)
		{
			this.limiterSound.Stop();
		}
		if (this.turboSound != null)
		{
			this.turboSound.Stop();
		}
	}

	private void Update()
	{
		float time = Mathf.Abs(this.RPM / this.RPMLimit);
		float num = this.idleVolCurve.Evaluate(time);
		float num2 = this.lowVolCurve.Evaluate(time);
		float num3 = this.medVolCurve.Evaluate(time);
		float num4 = this.highVolCurve.Evaluate(time);
		float num5 = this.maxRPMVolCurve.Evaluate(time);
		float num6 = this.turboVolCurve.Evaluate(time);
		if (!this.RevLimiterAllowed)
		{
			num5 = 0f;
		}
		if (this.carController == null)
		{
			this.DestroySounds();
			this.load = 0f;
		}
		if (num > 0.01f)
		{
			this.idleSound.volume = num;
			this.idleSound.pitch = this.idlePitchCurve.Evaluate(time);
		}
		if (num2 > 0.01f)
		{
			if (this.lowOnSound != null)
			{
				this.lowOnSound.volume = num2 * this.load;
				this.lowOnSound.pitch = this.lowPitchCurve.Evaluate(time);
			}
			this.lowOffSound.volume = num2 * (1f - this.load);
			this.lowOffSound.pitch = this.lowPitchCurve.Evaluate(time);
		}
		if (num3 > 0.01f)
		{
			if (this.medOnSound != null)
			{
				this.medOnSound.volume = num3 * this.load;
				this.medOnSound.pitch = this.medPitchCurve.Evaluate(time);
			}
			this.medOffSound.volume = num3 * (1f - this.load);
			this.medOffSound.pitch = this.medPitchCurve.Evaluate(time);
		}
		if (num4 > 0.01f)
		{
			if (this.highOnSound != null)
			{
				this.highOnSound.volume = num4 * this.load * (1f - this.limiterSound.volume);
				this.highOnSound.pitch = this.highPitchCurve.Evaluate(time);
			}
			this.highOffSound.volume = num4 * (1f - this.load);
			this.highOffSound.pitch = this.highPitchCurve.Evaluate(time);
		}
		if (this.limiterSound != null)
		{
			this.limiterSound.volume = num5 * this.load;
			this.limiterSound.pitch = this.maxRPMPitchCurve.Evaluate(time);
		}
		if (num6 > 0.05f && this.turboSound != null)
		{
			this.turboSound.volume = num6 * this.load * (float)((!this.Turbo) ? 0 : 1);
			this.turboSound.pitch = this.turboPitchCurve.Evaluate(time);
		}
	}

	public void GearShift()
	{
		if (this.gearShiftSound != null && !this.gearShiftSound.isPlaying)
		{
			this.gearShiftSound.Play();
		}
	}

	private AudioSource CreateAudioSource(AudioClip clip)
	{
		AudioSource audioSource = base.gameObject.AddComponent<AudioSource>();
		audioSource.maxDistance = 20f;
		audioSource.minDistance = 5f;
		audioSource.clip = clip;
		audioSource.loop = true;
		audioSource.spatialBlend = 1f;
		audioSource.volume = 0f;
		audioSource.pitch = 0f;
		audioSource.Play();
		return audioSource;
	}

	[HideInInspector]
	public float RPM;

	public float RPMLimit = 6000f;

	public AudioClip idleClip;

	public AnimationCurve idleVolCurve;

	public AnimationCurve idlePitchCurve;

	public AudioClip lowOffClip;

	public AudioClip lowOnClip;

	public AnimationCurve lowVolCurve;

	public AnimationCurve lowPitchCurve;

	public AudioClip medOffClip;

	public AudioClip medOnClip;

	public AnimationCurve medVolCurve;

	public AnimationCurve medPitchCurve;

	public AudioClip highOffClip;

	public AudioClip highOnClip;

	public AnimationCurve highVolCurve;

	public AnimationCurve highPitchCurve;

	public AudioClip maxRPMClip;

	public AnimationCurve maxRPMVolCurve;

	public AnimationCurve maxRPMPitchCurve;

	public AudioClip turboClip;

	public AnimationCurve turboVolCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(1f, 1f)
	});

	public AnimationCurve turboPitchCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 2f)
	});

	private AudioSource gearShiftSound;

	private AudioSource idleSound;

	private AudioSource lowOffSound;

	private AudioSource lowOnSound;

	private AudioSource medOffSound;

	private AudioSource medOnSound;

	private AudioSource highOffSound;

	private AudioSource highOnSound;

	private AudioSource limiterSound;

	private AudioSource turboSound;

	[HideInInspector]
	public float load;

	[HideInInspector]
	public bool RevLimiterAllowed;

	[HideInInspector]
	public bool Turbo;

	private bool SoundsLoaded;

	private bool SoundsDestroyed;

	private CarController carController;
}
