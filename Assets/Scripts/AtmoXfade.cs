using System;
using System.Collections;
using UnityEngine;

public class AtmoXfade : MonoBehaviour
{
	private void Start()
	{
		if (this.skyMat)
		{
			this.skyMat.SetColor("_Tint", this.skyBright);
		}
		if (this.dirLight)
		{
			this.dirLight.color = this.lightBright;
		}
		if (this.useRenderFog)
		{
			RenderSettings.fog = true;
			RenderSettings.fogColor = this.fogBright;
		}
		else
		{
			RenderSettings.fog = false;
		}
		this.curIntensity = this.maxLightIntensity;
	}

	private void OnTriggerEnter(Collider c)
	{
		if (c.sharedMaterial != null && c.sharedMaterial.name == "Player")
		{
			this.fadeState = AtmoXfade.FadeState.FadeDark;
			base.StartCoroutine(this.FadeDark());
		}
	}

	private void OnTriggerExit(Collider c)
	{
		if (c.sharedMaterial != null && c.sharedMaterial.name == "Player")
		{
			this.fadeState = AtmoXfade.FadeState.FadeBright;
			base.StartCoroutine(this.FadeBright());
		}
	}

	private IEnumerator FadeDark()
	{
		float t = 1E-05f;
		while (this.fadeState == AtmoXfade.FadeState.FadeDark && this.curIntensity > this.minLightIntensity)
		{
			this.skyMat.SetColor("_Tint", Color.Lerp(this.skyMat.GetColor("_Tint"), this.skyDark, t));
			this.dirLight.color = Color.Lerp(this.dirLight.color, this.lightDark, t);
			this.curIntensity = this.dirLight.intensity;
			this.dirLight.intensity = Mathf.SmoothStep(this.curIntensity, this.minLightIntensity, t);
			if (this.useRenderFog)
			{
				RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, this.fogDark, t);
				RenderSettings.fogDensity = Mathf.SmoothStep(RenderSettings.fogDensity, this.maxFog, t);
			}
			yield return null;
			t += Time.deltaTime / this.fadeTime;
		}
		yield break;
	}

	private IEnumerator FadeBright()
	{
		float t = 1E-05f;
		while (this.fadeState == AtmoXfade.FadeState.FadeBright && this.curIntensity < this.maxLightIntensity)
		{
			this.skyMat.SetColor("_Tint", Color.Lerp(this.skyMat.GetColor("_Tint"), this.skyBright, t));
			this.dirLight.color = Color.Lerp(this.dirLight.color, this.lightBright, t);
			this.curIntensity = this.dirLight.intensity;
			this.dirLight.intensity = Mathf.SmoothStep(this.curIntensity, this.maxLightIntensity, t);
			if (this.useRenderFog)
			{
				RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, this.fogBright, t);
				RenderSettings.fogDensity = Mathf.SmoothStep(RenderSettings.fogDensity, this.minFog, t);
			}
			yield return null;
			t += Time.deltaTime / this.fadeTime;
		}
		yield break;
	}

	public Material skyMat;

	public Color skyBright = Color.grey;

	public Color skyDark = Color.black;

	public Light dirLight;

	public Color lightBright = Color.grey;

	public Color lightDark = Color.black;

	public float minLightIntensity = 0.2f;

	public float maxLightIntensity = 0.85f;

	private float curIntensity;

	public bool useRenderFog = true;

	public Color fogBright = Color.grey;

	public Color fogDark = Color.black;

	public float minFog = 0.004f;

	public float maxFog = 0.02f;

	public AtmoXfade.FadeState fadeState = AtmoXfade.FadeState.FadeBright;

	public float fadeTime = 80f;

	public enum FadeState
	{
		FadeDark,
		FadeBright
	}
}
