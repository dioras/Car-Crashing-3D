using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Noise And Grain (Overlay)")]
[Serializable]
public class NoiseAndGrain : PostEffectsBase
{
	public NoiseAndGrain()
	{
		this.strength = 1f;
		this.blackIntensity = 1f;
		this.whiteIntensity = 1f;
		this.redChannelNoise = 0.975f;
		this.greenChannelNoise = 0.875f;
		this.blueChannelNoise = 1.2f;
		this.redChannelTiling = 24f;
		this.greenChannelTiling = 28f;
		this.blueChannelTiling = 34f;
		this.filterMode = FilterMode.Bilinear;
	}

	public virtual void OnDisable()
	{
		if (this.noiseMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.noiseMaterial);
		}
	}

	public override bool CheckResources()
	{
		this.CheckSupport(false);
		this.noiseMaterial = this.CheckShaderAndCreateMaterial(this.noiseShader, this.noiseMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			this.noiseMaterial.SetVector("_NoisePerChannel", new Vector3(this.redChannelNoise, this.greenChannelNoise, this.blueChannelNoise));
			this.noiseMaterial.SetVector("_NoiseTilingPerChannel", new Vector3(this.redChannelTiling, this.greenChannelTiling, this.blueChannelTiling));
			this.noiseMaterial.SetVector("_NoiseAmount", new Vector3(this.strength, this.blackIntensity, this.whiteIntensity));
			this.noiseMaterial.SetTexture("_NoiseTex", this.noiseTexture);
			this.noiseTexture.filterMode = this.filterMode;
			NoiseAndGrain.DrawNoiseQuadGrid(source, destination, this.noiseMaterial, this.noiseTexture, 0);
		}
	}

	public static void DrawNoiseQuadGrid(RenderTexture source, RenderTexture dest, Material fxMaterial, Texture2D noise, int passNr)
	{
		RenderTexture.active = dest;
		float num = (float)noise.width * 1f;
		float num2 = num;
		float num3 = 1f * (float)source.width / num2;
		fxMaterial.SetTexture("_MainTex", source);
		GL.PushMatrix();
		GL.LoadOrtho();
		float num4 = 1f * (float)source.width / (1f * (float)source.height);
		float num5 = 1f / num3;
		float num6 = num5 * num4;
		float num7 = num2 / ((float)noise.width * 1f);
		fxMaterial.SetPass(passNr);
		GL.Begin(7);
		for (float num8 = (float)0; num8 < 1f; num8 += num5)
		{
			for (float num9 = (float)0; num9 < 1f; num9 += num6)
			{
				float num10 = UnityEngine.Random.Range((float)0, 1f);
				float num11 = UnityEngine.Random.Range((float)0, 1f);
				num10 = Mathf.Floor(num10 * num) / num;
				num11 = Mathf.Floor(num11 * num) / num;
				float num12 = 1f / num;
				GL.MultiTexCoord2(0, num10, num11);
				GL.MultiTexCoord2(1, (float)0, (float)0);
				GL.Vertex3(num8, num9, 0.1f);
				GL.MultiTexCoord2(0, num10 + num7 * num12, num11);
				GL.MultiTexCoord2(1, 1f, (float)0);
				GL.Vertex3(num8 + num5, num9, 0.1f);
				GL.MultiTexCoord2(0, num10 + num7 * num12, num11 + num7 * num12);
				GL.MultiTexCoord2(1, 1f, 1f);
				GL.Vertex3(num8 + num5, num9 + num6, 0.1f);
				GL.MultiTexCoord2(0, num10, num11 + num7 * num12);
				GL.MultiTexCoord2(1, (float)0, 1f);
				GL.Vertex3(num8, num9 + num6, 0.1f);
			}
		}
		GL.End();
		GL.PopMatrix();
	}

	public override void Main()
	{
	}

	public float strength;

	public float blackIntensity;

	public float whiteIntensity;

	public float redChannelNoise;

	public float greenChannelNoise;

	public float blueChannelNoise;

	public float redChannelTiling;

	public float greenChannelTiling;

	public float blueChannelTiling;

	public FilterMode filterMode;

	public Shader noiseShader;

	public Texture2D noiseTexture;

	private Material noiseMaterial;
}
