using System;
using UnityEngine;

[AddComponentMenu("Image Effects/Screen Overlay")]
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[Serializable]
public class ScreenOverlay : PostEffectsBase
{
	public ScreenOverlay()
	{
		this.blendMode = ScreenOverlay.OverlayBlendMode.Overlay;
		this.intensity = 1f;
	}

	public virtual void OnDisable()
	{
		if (this.overlayMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.overlayMaterial);
		}
	}

	public override bool CheckResources()
	{
		this.CheckSupport(false);
		this.overlayMaterial = this.CheckShaderAndCreateMaterial(this.overlayShader, this.overlayMaterial);
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
			this.overlayMaterial.SetFloat("_Intensity", this.intensity);
			this.overlayMaterial.SetTexture("_Overlay", this.texture);
			Graphics.Blit(source, destination, this.overlayMaterial, (int)this.blendMode);
		}
	}

	public override void Main()
	{
	}

	public ScreenOverlay.OverlayBlendMode blendMode;

	public float intensity;

	public Texture2D texture;

	public Shader overlayShader;

	private Material overlayMaterial;

	[Serializable]
	public enum OverlayBlendMode
	{
		AddSub,
		ScreenBlend,
		Multiply,
		Overlay,
		AlphaBlend
	}
}
