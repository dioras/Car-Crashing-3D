using System;
using UnityEngine;

namespace CustomVP
{
	public class CarEffects : MonoBehaviour
	{
		private void Awake()
		{
			this.carController = base.GetComponent<CarController>();
			this.tankController = base.GetComponent<TankController>();
			this.engine = base.GetComponent<EngineController>();
		}

		private void LoadResources()
		{
			Transform transform = base.transform.Find("Sounds");
			if (transform == null)
			{
				transform = base.transform;
			}
			this.HitSound = (UnityEngine.Object.Instantiate(Resources.Load("Sounds/Hit", typeof(GameObject)), base.transform.position, base.transform.rotation, transform) as GameObject).GetComponent<AudioSource>();
			this.SkidSound = (UnityEngine.Object.Instantiate(Resources.Load("Sounds/Skid", typeof(GameObject)), base.transform.position, base.transform.rotation, transform) as GameObject).GetComponent<AudioSource>();
			this.WheelBumpSound = (UnityEngine.Object.Instantiate(Resources.Load("Sounds/WheelBump", typeof(GameObject)), base.transform.position, base.transform.rotation, transform) as GameObject).GetComponent<AudioSource>();
			this.OffroadSound = (UnityEngine.Object.Instantiate(Resources.Load("Sounds/Offroad", typeof(GameObject)), base.transform.position, base.transform.rotation, transform) as GameObject).GetComponent<AudioSource>();
			this.WaterSplashSound = (UnityEngine.Object.Instantiate(Resources.Load("Sounds/WaterSplash", typeof(GameObject)), base.transform.position, base.transform.rotation, transform) as GameObject).GetComponent<AudioSource>();
			this.ExhaustParticle = (UnityEngine.Object.Instantiate(Resources.Load("ParticleEffects/ExhaustParticle", typeof(ParticleSystem))) as ParticleSystem);
			this.ExhaustParticle.transform.parent = base.transform;
			this.ExhaustParticle.transform.localPosition = Vector3.zero;
			this.surfaceManager = SurfaceManager.Instance;
			this.ResourcesLoaded = true;
		}

		private void Update()
		{
			if (this.carController == null)
			{
				return;
			}
			if (!this.ResourcesLoaded)
			{
				this.LoadResources();
			}
			this.DoParticles();
			this.DoWheelBumpSounds();
			if (this.surfaceManager != null)
			{
				this.DoSurfaceSounds();
			}
		}

		private void DoParticles()
		{
			if (this.ExhaustParticle == null || this.ExhaustPoints == null || this.engine == null)
			{
				return;
			}
			if (!this.engine.Diesel)
			{
				return;
			}
			foreach (Transform transform in this.ExhaustPoints)
			{
				if (transform.gameObject.activeInHierarchy && Mathf.Abs(this.carController.Throttle) == 1f && Mathf.Abs(this.carController.Speed) < 10f)
				{
					this.ExhaustParticle.transform.position = transform.position;
					this.ExhaustParticle.transform.rotation = transform.rotation;
					this.ExhaustParticle.Emit(1);
				}
			}
		}

		private void OnCollisionEnter(Collision collision)
		{
			this.DoHitSounds(collision);
		}

		private void DoWheelBumpSounds()
		{
			if (this.WheelBumpSound == null)
			{
				return;
			}
			foreach (_Wheel wheel in this.carController.wheels)
			{
				if (wheel.wc.deltaCompression > 0.1f * this.MinWheelBumpValue && wheel.wc.wheelCollider.isGrounded && !this.WheelBumpSound.isPlaying)
				{
					this.WheelBumpSound.Play();
				}
			}
		}

		private void DoSurfaceSounds()
		{
			if (this.SkidSound == null || this.OffroadSound == null)
			{
				return;
			}
			if (!this.SkidSound.isPlaying)
			{
				this.SkidSound.Play();
			}
			if (!this.OffroadSound.isPlaying)
			{
				this.OffroadSound.Play();
			}
			if (!this.WaterSplashSound.isPlaying)
			{
				this.WaterSplashSound.Play();
			}
			float num = Mathf.Lerp(0f, 1f, (Mathf.Abs(this.carController.Speed) - 1f) / 6f);
			float t = (!this.carController.Grounded) ? 0f : Mathf.Lerp(0f, 1f, Mathf.Abs(this.carController.Speed) / 20f);
			float num2 = 0f;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (this.surfaceManager.SurfaceMaterialUnderCar != null)
			{
				flag = (this.surfaceManager.SurfaceMaterialUnderCar.SkidSounds && this.carController.tankController == null);
				flag2 = this.surfaceManager.SurfaceMaterialUnderCar.OffroadSounds;
				flag3 = this.surfaceManager.SurfaceMaterialUnderCar.WaterSplashSounds;
			}
			if (this.tankController == null)
			{
				foreach (_Wheel wheel in this.carController.wheels)
				{
					if (wheel.wc.sLong > num2 && wheel.wc.wheelCollider.isGrounded)
					{
						num2 = wheel.wc.CommonSlip;
					}
				}
			}
			else
			{
				foreach (TankWheelCollider tankWheelCollider in this.tankController.allWheelColliders)
				{
					if (tankWheelCollider.sLong > num2 && tankWheelCollider.grounded)
					{
						num2 = tankWheelCollider.sLat + tankWheelCollider.sLong;
					}
				}
			}
			this.SkidSound.volume = ((!flag) ? 0f : Mathf.Lerp(0f, this.SkidMaxVolume, (num2 - 0.3f) / 0.7f * num));
			this.OffroadSound.volume = ((!flag2) ? 0f : Mathf.Lerp(0f, this.OffroadMaxVolume, t));
			this.WaterSplashSound.volume = ((!flag3) ? 0f : Mathf.Lerp(0f, this.MaxSplashVolume, t));
		}

		private void DoHitSounds(Collision collision)
		{
			if (this.HitSound == null || (double)(Time.time - this.LastHitTime) < 0.5 || collision.impulse.magnitude < this.MinCollisionSoundForce)
			{
				return;
			}
			bool flag = false;
			foreach (ContactPoint contactPoint in collision.contacts)
			{
				foreach (Collider y in this.carController.BodyColliders)
				{
					if (contactPoint.thisCollider == y)
					{
						flag = true;
					}
				}
			}
			if (!flag)
			{
				return;
			}
			this.HitSound.Play();
			this.LastHitTime = Time.time;
		}

		private SurfaceManager surfaceManager;

		private CarController carController;

		private TankController tankController;

		private EngineController engine;

		private AudioSource HitSound;

		private AudioSource SkidSound;

		private AudioSource WheelBumpSound;

		private AudioSource OffroadSound;

		private AudioSource WaterSplashSound;

		private ParticleSystem ExhaustParticle;

		public Transform[] ExhaustPoints;

		[Header("Impacts")]
		public float MinCollisionSoundForce = 2000f;

		public float SkidMaxVolume = 1f;

		public float MaxSplashVolume = 0.5f;

		private float MinWheelBumpValue = 1f;

		private float OffroadMaxVolume = 0.2f;

		private float LastHitTime;

		private bool ResourcesLoaded;
	}
}
