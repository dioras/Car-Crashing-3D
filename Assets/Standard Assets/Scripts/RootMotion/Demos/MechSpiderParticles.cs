using System;
using UnityEngine;

namespace RootMotion.Demos
{
	public class MechSpiderParticles : MonoBehaviour
	{
		private void Start()
		{
			this.particles = (ParticleSystem)base.GetComponent(typeof(ParticleSystem));
		}

		private void Update()
		{
			float magnitude = this.mechSpiderController.inputVector.magnitude;
			float constant = Mathf.Clamp(magnitude * 50f, 30f, 50f);
			var particlesEmission = this.particles.emission;
			particlesEmission.rateOverTime = new ParticleSystem.MinMaxCurve(constant);
			var particlesMain = this.particles.main;
			particlesMain.startColor = new Color(this.particles.main.startColor.color.r, this.particles.main.startColor.color.g, this.particles.main.startColor.color.b, Mathf.Clamp(magnitude, 0.4f, 1f));
		}

		public MechSpiderController mechSpiderController;

		private ParticleSystem particles;
	}
}
