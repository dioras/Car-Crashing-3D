using System;
using System.Collections;
using UnityEngine;

namespace DigitalRuby.PyroParticles
{
	public class FireBaseScript : MonoBehaviour
	{
		private IEnumerator CleanupEverythingCoRoutine()
		{
			yield return new WaitForSeconds(this.StopTime + 2f);
			UnityEngine.Object.Destroy(base.gameObject);
			yield break;
		}

		private void StartParticleSystems()
		{
			foreach (ParticleSystem particleSystem in base.gameObject.GetComponentsInChildren<ParticleSystem>())
			{
				if (this.ManualParticleSystems == null || this.ManualParticleSystems.Length == 0 || Array.IndexOf<ParticleSystem>(this.ManualParticleSystems, particleSystem) < 0)
				{
					if (particleSystem.startDelay == 0f)
					{
						particleSystem.startDelay = 0.01f;
					}
					particleSystem.Play();
				}
			}
		}

		protected virtual void Awake()
		{
			this.Starting = true;
			int num = LayerMask.NameToLayer("FireLayer");
			Physics.IgnoreLayerCollision(num, num);
		}

		protected virtual void Start()
		{
			if (this.AudioSource != null)
			{
				this.AudioSource.Play();
			}
			this.stopTimeMultiplier = 1f / this.StopTime;
			this.startTimeMultiplier = 1f / this.StartTime;
			FireBaseScript.CreateExplosion(base.gameObject.transform.position, this.ForceRadius, this.ForceAmount);
			this.StartParticleSystems();
			ICollisionHandler collisionHandler = this as ICollisionHandler;
			if (collisionHandler != null)
			{
				FireCollisionForwardScript componentInChildren = base.GetComponentInChildren<FireCollisionForwardScript>();
				if (componentInChildren != null)
				{
					componentInChildren.CollisionHandler = collisionHandler;
				}
			}
		}

		protected virtual void Update()
		{
			this.Duration -= Time.deltaTime;
			if (this.Stopping)
			{
				this.stopTimeIncrement += Time.deltaTime;
				if (this.stopTimeIncrement < this.StopTime)
				{
					this.StopPercent = this.stopTimeIncrement * this.stopTimeMultiplier;
				}
			}
			else if (this.Starting)
			{
				this.startTimeIncrement += Time.deltaTime;
				if (this.startTimeIncrement < this.StartTime)
				{
					this.StartPercent = this.startTimeIncrement * this.startTimeMultiplier;
				}
				else
				{
					this.Starting = false;
				}
			}
			else if (this.Duration <= 0f)
			{
				this.Stop();
			}
		}

		public static void CreateExplosion(Vector3 pos, float radius, float force)
		{
			if (force <= 0f || radius <= 0f)
			{
				return;
			}
			Collider[] array = Physics.OverlapSphere(pos, radius);
			foreach (Collider collider in array)
			{
				Rigidbody component = collider.GetComponent<Rigidbody>();
				if (component != null)
				{
					component.AddExplosionForce(force, pos, radius);
				}
			}
		}

		public virtual void Stop()
		{
			if (this.Stopping)
			{
				return;
			}
			this.Stopping = true;
			foreach (ParticleSystem particleSystem in base.gameObject.GetComponentsInChildren<ParticleSystem>())
			{
				particleSystem.Stop();
			}
			base.StartCoroutine(this.CleanupEverythingCoRoutine());
		}

		public bool Starting { get; private set; }

		public float StartPercent { get; private set; }

		public bool Stopping { get; private set; }

		public float StopPercent { get; private set; }

		[Tooltip("Optional audio source to play once when the script starts.")]
		public AudioSource AudioSource;

		[Tooltip("How long the script takes to fully start. This is used to fade in animations and sounds, etc.")]
		public float StartTime = 1f;

		[Tooltip("How long the script takes to fully stop. This is used to fade out animations and sounds, etc.")]
		public float StopTime = 3f;

		[Tooltip("How long the effect lasts. Once the duration ends, the script lives for StopTime and then the object is destroyed.")]
		public float Duration = 2f;

		[Tooltip("How much force to create at the center (explosion), 0 for none.")]
		public float ForceAmount;

		[Tooltip("The radius of the force, 0 for none.")]
		public float ForceRadius;

		[Tooltip("A hint to users of the script that your object is a projectile and is meant to be shot out from a person or trap, etc.")]
		public bool IsProjectile;

		[Tooltip("Particle systems that must be manually started and will not be played on start.")]
		public ParticleSystem[] ManualParticleSystems;

		private float startTimeMultiplier;

		private float startTimeIncrement;

		private float stopTimeMultiplier;

		private float stopTimeIncrement;
	}
}
