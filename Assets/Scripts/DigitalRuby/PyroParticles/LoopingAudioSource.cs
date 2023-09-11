using System;
using UnityEngine;

namespace DigitalRuby.PyroParticles
{
	public class LoopingAudioSource
	{
		public LoopingAudioSource(MonoBehaviour script, AudioSource audioSource, float startMultiplier, float stopMultiplier)
		{
			this.AudioSource = audioSource;
			if (audioSource != null)
			{
				this.AudioSource.loop = true;
				this.AudioSource.volume = 0f;
				this.AudioSource.Stop();
			}
			this.TargetVolume = 1f;
			this.currentMultiplier = startMultiplier;
			this.startMultiplier = startMultiplier;
			this.stopMultiplier = stopMultiplier;
		}

		public AudioSource AudioSource { get; private set; }

		public float TargetVolume { get; private set; }

		public void Play()
		{
			this.Play(this.TargetVolume);
		}

		public void Play(float targetVolume)
		{
			if (this.AudioSource != null && !this.AudioSource.isPlaying)
			{
				this.AudioSource.volume = 0f;
				this.AudioSource.Play();
				this.currentMultiplier = this.startMultiplier;
			}
			this.TargetVolume = targetVolume;
		}

		public void Stop()
		{
			if (this.AudioSource != null && this.AudioSource.isPlaying)
			{
				this.TargetVolume = 0f;
				this.currentMultiplier = this.stopMultiplier;
			}
		}

		public void Update()
		{
			if (this.AudioSource != null && this.AudioSource.isPlaying)
			{
				float num = Mathf.Lerp(this.AudioSource.volume, this.TargetVolume, Time.deltaTime / this.currentMultiplier);
				this.AudioSource.volume = num;
				if (num == 0f)
				{
					this.AudioSource.Stop();
				}
			}
		}

		private float startMultiplier;

		private float stopMultiplier;

		private float currentMultiplier;
	}
}
