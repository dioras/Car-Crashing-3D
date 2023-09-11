using System;
using UnityEngine;

namespace DigitalRuby.PyroParticles
{
	public class FireConstantBaseScript : FireBaseScript
	{
		protected override void Awake()
		{
			base.Awake();
			this.LoopingAudioSource = new LoopingAudioSource(this, this.AudioSource, this.StartTime, this.StopTime);
			this.Duration = 1E+09f;
		}

		protected override void Update()
		{
			base.Update();
			this.LoopingAudioSource.Update();
		}

		protected override void Start()
		{
			base.Start();
			this.LoopingAudioSource.Play();
		}

		public override void Stop()
		{
			this.LoopingAudioSource.Stop();
			base.Stop();
		}

		[HideInInspector]
		public LoopingAudioSource LoopingAudioSource;
	}
}
