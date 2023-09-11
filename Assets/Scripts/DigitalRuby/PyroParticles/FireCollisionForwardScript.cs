using System;
using UnityEngine;

namespace DigitalRuby.PyroParticles
{
	public class FireCollisionForwardScript : MonoBehaviour
	{
		public void OnCollisionEnter(Collision col)
		{
			this.CollisionHandler.HandleCollision(base.gameObject, col);
		}

		public ICollisionHandler CollisionHandler;
	}
}
