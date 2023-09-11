using System;
using System.Collections;
using UnityEngine;

namespace DigitalRuby.PyroParticles
{
	public class FireProjectileScript : FireBaseScript, ICollisionHandler
	{
		private IEnumerator SendCollisionAfterDelay()
		{
			yield return new WaitForSeconds(this.ProjectileColliderDelay);
			Vector3 dir = this.ProjectileDirection * this.ProjectileColliderSpeed;
			dir = this.ProjectileColliderObject.transform.rotation * dir;
			this.ProjectileColliderObject.GetComponent<Rigidbody>().velocity = dir;
			yield break;
		}

		protected override void Start()
		{
			base.Start();
			base.StartCoroutine(this.SendCollisionAfterDelay());
		}

		public void HandleCollision(GameObject obj, Collision c)
		{
			if (this.collided)
			{
				return;
			}
			this.collided = true;
			this.Stop();
			if (this.ProjectileDestroyParticleSystemsOnCollision != null)
			{
				foreach (ParticleSystem obj2 in this.ProjectileDestroyParticleSystemsOnCollision)
				{
					UnityEngine.Object.Destroy(obj2, 0.1f);
				}
			}
			if (this.ProjectileCollisionSound != null)
			{
				this.ProjectileCollisionSound.Play();
			}
			if (c.contacts.Length != 0)
			{
				this.ProjectileExplosionParticleSystem.transform.position = c.contacts[0].point;
				this.ProjectileExplosionParticleSystem.Play();
				FireBaseScript.CreateExplosion(c.contacts[0].point, this.ProjectileExplosionRadius, this.ProjectileExplosionForce);
				if (this.CollisionDelegate != null)
				{
					this.CollisionDelegate(this, c.contacts[0].point);
				}
			}
		}

		[Tooltip("The collider object to use for collision and physics.")]
		public GameObject ProjectileColliderObject;

		[Tooltip("The sound to play upon collision.")]
		public AudioSource ProjectileCollisionSound;

		[Tooltip("The particle system to play upon collision.")]
		public ParticleSystem ProjectileExplosionParticleSystem;

		[Tooltip("The radius of the explosion upon collision.")]
		public float ProjectileExplosionRadius = 50f;

		[Tooltip("The force of the explosion upon collision.")]
		public float ProjectileExplosionForce = 50f;

		[Tooltip("An optional delay before the collider is sent off, in case the effect has a pre fire animation.")]
		public float ProjectileColliderDelay;

		[Tooltip("The speed of the collider.")]
		public float ProjectileColliderSpeed = 450f;

		[Tooltip("The direction that the collider will go. For example, flame strike goes down, and fireball goes forward.")]
		public Vector3 ProjectileDirection = Vector3.forward;

		[Tooltip("What layers the collider can collide with.")]
		public LayerMask ProjectileCollisionLayers = -1;

		[Tooltip("Particle systems to destroy upon collision.")]
		public ParticleSystem[] ProjectileDestroyParticleSystemsOnCollision;

		[HideInInspector]
		public FireProjectileCollisionDelegate CollisionDelegate;

		private bool collided;
	}
}
