using System;
using UnityEngine;

namespace DigitalRuby.PyroParticles
{
	public class FireLightScript : MonoBehaviour
	{
		private void Awake()
		{
			this.firePointLight = base.gameObject.GetComponentInChildren<Light>();
			if (this.firePointLight != null)
			{
				this.lightIntensity = this.firePointLight.intensity;
				this.firePointLight.intensity = 0f;
				this.baseY = this.firePointLight.gameObject.transform.position.y;
			}
			this.seed = UnityEngine.Random.value * this.Seed;
			this.fireBaseScript = base.gameObject.GetComponent<FireBaseScript>();
		}

		private void Update()
		{
			if (this.firePointLight == null)
			{
				return;
			}
			if (this.seed != 0f)
			{
				bool flag = true;
				float num = 1f;
				if (this.fireBaseScript != null)
				{
					if (this.fireBaseScript.Stopping)
					{
						flag = false;
						this.firePointLight.intensity = Mathf.Lerp(this.firePointLight.intensity, 0f, this.fireBaseScript.StopPercent);
					}
					else if (this.fireBaseScript.Starting)
					{
						num = this.fireBaseScript.StartPercent;
					}
				}
				if (flag)
				{
					float intensity = Mathf.Clamp(this.IntensityModifier * num * Mathf.PerlinNoise(this.seed + Time.time, this.seed + 1f + Time.time), this.IntensityMaxRange.Minimum, this.IntensityMaxRange.Maximum);
					this.firePointLight.intensity = intensity;
				}
				float x = Mathf.PerlinNoise(this.seed + Time.time * 2f, this.seed + 1f + Time.time * 2f) - 0.5f;
				float y = this.baseY + Mathf.PerlinNoise(this.seed + 2f + Time.time * 2f, this.seed + 3f + Time.time * 2f) - 0.5f;
				float z = Mathf.PerlinNoise(this.seed + 4f + Time.time * 2f, this.seed + 5f + Time.time * 2f) - 0.5f;
				this.firePointLight.gameObject.transform.localPosition = Vector3.up + new Vector3(x, y, z);
			}
			else if (this.fireBaseScript.Stopping)
			{
				this.firePointLight.intensity = Mathf.Lerp(this.firePointLight.intensity, 0f, this.fireBaseScript.StopPercent);
			}
			else if (this.fireBaseScript.Starting)
			{
				this.firePointLight.intensity = Mathf.Lerp(0f, this.lightIntensity, this.fireBaseScript.StartPercent);
			}
		}

		[Tooltip("Random seed for movement, 0 for no movement.")]
		public float Seed = 100f;

		[Tooltip("Multiplier for light intensity.")]
		public float IntensityModifier = 2f;

		[SingleLine("Min and max intensity range.")]
		public RangeOfFloats IntensityMaxRange = new RangeOfFloats
		{
			Minimum = 0f,
			Maximum = 8f
		};

		private Light firePointLight;

		private float lightIntensity;

		private float seed;

		private FireBaseScript fireBaseScript;

		private float baseY;
	}
}
