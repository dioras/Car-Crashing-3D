using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace DigitalRuby.PyroParticles
{
	public class MeteorSwarmScript : FireBaseScript, ICollisionHandler
	{
		[HideInInspector]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event MeteorSwarmCollisionDelegate CollisionDelegate;

		private IEnumerator SpawnMeteor()
		{
			float delay = UnityEngine.Random.Range(0f, 1f);
			yield return new WaitForSeconds(delay);
			Vector3 src = this.Source + UnityEngine.Random.insideUnitSphere * this.SourceRadius;
			GameObject meteor = UnityEngine.Object.Instantiate<GameObject>(this.MeteorPrefab);
			float scale = UnityEngine.Random.Range(this.ScaleRange.Minimum, this.ScaleRange.Maximum);
			meteor.transform.localScale = new Vector3(scale, scale, scale);
			meteor.transform.position = src;
			Vector3 dest = base.gameObject.transform.position + UnityEngine.Random.insideUnitSphere * this.DestinationRadius;
			dest.y = 0f;
			Vector3 dir = dest - src;
			Vector3 vel = dir / this.TimeToImpact;
			Rigidbody r = meteor.GetComponent<Rigidbody>();
			r.velocity = vel;
			float xRot = UnityEngine.Random.Range(-90f, 90f);
			float yRot = UnityEngine.Random.Range(-90f, 90f);
			float zRot = UnityEngine.Random.Range(-90f, 90f);
			r.angularVelocity = new Vector3(xRot, yRot, zRot);
			r.mass *= scale * scale;
			Renderer renderer = meteor.GetComponent<Renderer>();
			renderer.sharedMaterial = this.MeteorMaterials[UnityEngine.Random.Range(0, this.MeteorMaterials.Length)];
			meteor.transform.parent = base.gameObject.transform;
			meteor.GetComponent<FireCollisionForwardScript>().CollisionHandler = this;
			Mesh mesh = this.MeteorMeshes[UnityEngine.Random.Range(0, this.MeteorMeshes.Length - 1)];
			meteor.GetComponent<MeshFilter>().mesh = mesh;
			TrailRenderer t = meteor.GetComponent<TrailRenderer>();
			t.startWidth = UnityEngine.Random.Range(2f, 3f) * scale;
			t.endWidth = UnityEngine.Random.Range(0.25f, 0.5f) * scale;
			t.time = UnityEngine.Random.Range(0.25f, 0.5f);
			if (this.EmissionSounds != null && this.EmissionSounds.Length != 0)
			{
				AudioSource component = meteor.GetComponent<AudioSource>();
				if (component != null)
				{
					int num = UnityEngine.Random.Range(0, this.EmissionSounds.Length);
					AudioClip clip = this.EmissionSounds[num];
					component.PlayOneShot(clip, scale);
				}
			}
			yield break;
		}

		private void SpawnMeteors()
		{
			int num = UnityEngine.Random.Range(this.MeteorsPerSecondRange.Minimum, this.MeteorsPerSecondRange.Maximum);
			for (int i = 0; i < num; i++)
			{
				base.StartCoroutine(this.SpawnMeteor());
			}
		}

		protected override void Update()
		{
			base.Update();
			if (this.Duration > 0f && (this.elapsedSecond += Time.deltaTime) >= 1f)
			{
				this.elapsedSecond -= 1f;
				this.SpawnMeteors();
			}
		}

		private void PlayCollisionSound(GameObject obj)
		{
			if (this.ExplosionSounds == null || this.ExplosionSounds.Length == 0)
			{
				return;
			}
			AudioSource component = obj.GetComponent<AudioSource>();
			if (component == null)
			{
				return;
			}
			int num = UnityEngine.Random.Range(0, this.ExplosionSounds.Length);
			AudioClip clip = this.ExplosionSounds[num];
			component.PlayOneShot(clip, obj.transform.localScale.x);
		}

		private IEnumerator CleanupMeteor(float delay, GameObject obj)
		{
			yield return new WaitForSeconds(delay);
			UnityEngine.Object.Destroy(obj.GetComponent<Collider>());
			UnityEngine.Object.Destroy(obj.GetComponent<Rigidbody>());
			UnityEngine.Object.Destroy(obj.GetComponent<TrailRenderer>());
			yield break;
		}

		public void HandleCollision(GameObject obj, Collision col)
		{
			Renderer component = obj.GetComponent<Renderer>();
			if (component == null)
			{
				return;
			}
			if (this.CollisionDelegate != null)
			{
				this.CollisionDelegate(this, obj);
			}
			Vector3 vector;
			Vector3 forward;
			if (col.contacts.Length == 0)
			{
				vector = obj.transform.position;
				forward = -vector;
			}
			else
			{
				vector = col.contacts[0].point;
				forward = col.contacts[0].normal;
			}
			this.MeteorExplosionParticleSystem.transform.position = vector;
			this.MeteorExplosionParticleSystem.transform.rotation = Quaternion.LookRotation(forward);
			this.MeteorExplosionParticleSystem.Emit(UnityEngine.Random.Range(10, 20));
			this.MeteorShrapnelParticleSystem.transform.position = col.contacts[0].point;
			this.MeteorShrapnelParticleSystem.Emit(UnityEngine.Random.Range(10, 20));
			this.PlayCollisionSound(obj);
			UnityEngine.Object.Destroy(component);
			base.StartCoroutine(this.CleanupMeteor(0.1f, obj));
			UnityEngine.Object.Destroy(obj, 4f);
		}

		[Tooltip("The game object prefab that represents the meteor.")]
		public GameObject MeteorPrefab;

		[Tooltip("Explosion particle system that should be emitted for each initial collision.")]
		public ParticleSystem MeteorExplosionParticleSystem;

		[Tooltip("Shrapnel particle system that should be emitted for each initial collision.")]
		public ParticleSystem MeteorShrapnelParticleSystem;

		[Tooltip("A list of materials to use for the meteors. One will be chosen at random for each meteor.")]
		public Material[] MeteorMaterials;

		[Tooltip("A list of meshes to use for the meteors. One will be chosen at random for each meteor.")]
		public Mesh[] MeteorMeshes;

		[Tooltip("The destination radius")]
		public float DestinationRadius;

		[Tooltip("The source of the meteor swarm (in the sky somewhere usually)")]
		public Vector3 Source;

		[Tooltip("The source radius")]
		public float SourceRadius;

		[Tooltip("The time it should take the meteors to impact assuming a clear path to destination.")]
		public float TimeToImpact = 1f;

		[SingleLine("How many meteors should be emitted per second (min and max)")]
		public RangeOfIntegers MeteorsPerSecondRange = new RangeOfIntegers
		{
			Minimum = 5,
			Maximum = 10
		};

		[SingleLine("Scale multiplier for meteors (min and max)")]
		public RangeOfFloats ScaleRange = new RangeOfFloats
		{
			Minimum = 0.25f,
			Maximum = 1.5f
		};

		[SingleLine("Maximum life time of meteors in seconds (min and max).")]
		public RangeOfFloats MeteorLifeTimeRange = new RangeOfFloats
		{
			Minimum = 4f,
			Maximum = 8f
		};

		[Tooltip("Array of emission sounds. One will be chosen at random upon meteor creation.")]
		public AudioClip[] EmissionSounds;

		[Tooltip("Array of explosion sounds. One will be chosen at random upon impact.")]
		public AudioClip[] ExplosionSounds;

		private float elapsedSecond = 1f;
	}
}
