using System;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/FastMobileBloom")]
public class FastMobileBloom : MonoBehaviour
{
	private void Awake()
	{
		FastMobileBloom.Instance = this;
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		int width = source.width / 4;
		int height = source.height / 4;
		RenderTexture renderTexture = RenderTexture.GetTemporary(width, height, 0, source.format);
		renderTexture.DiscardContents();
		this.fastBloomMaterial.SetFloat("_Spread", this.blurSize);
		this.fastBloomMaterial.SetVector("_ThresholdParams", new Vector2(1f, -this.threshold));
		Graphics.Blit(source, renderTexture, this.fastBloomMaterial, 0);
		for (int i = 0; i < this.blurIterations - 1; i++)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(renderTexture.width / 2, renderTexture.height / 2, 0, source.format);
			temporary.DiscardContents();
			this.fastBloomMaterial.SetFloat("_Spread", this.blurSize);
			Graphics.Blit(renderTexture, temporary, this.fastBloomMaterial, 1);
			RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary;
		}
		for (int j = 0; j < this.blurIterations - 1; j++)
		{
			RenderTexture temporary2 = RenderTexture.GetTemporary(renderTexture.width * 2, renderTexture.height * 2, 0, source.format);
			temporary2.DiscardContents();
			this.fastBloomMaterial.SetFloat("_Spread", this.blurSize);
			Graphics.Blit(renderTexture, temporary2, this.fastBloomMaterial, 2);
			RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary2;
		}
		this.fastBloomMaterial.SetFloat("_BloomIntensity", this.intensity);
		this.fastBloomMaterial.SetTexture("_BloomTex", renderTexture);
		Graphics.Blit(source, destination, this.fastBloomMaterial, 3);
		RenderTexture.ReleaseTemporary(renderTexture);
	}

	public static FastMobileBloom Instance;

	[Range(0f, 1.5f)]
	public float threshold = 0.25f;

	[Range(0f, 4f)]
	public float intensity = 1f;

	[Range(0.25f, 5.5f)]
	public float blurSize = 1f;

	[Range(1f, 4f)]
	public int blurIterations = 2;

	public Material fastBloomMaterial;
}
