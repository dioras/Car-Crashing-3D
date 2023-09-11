using System;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Depth of Field (HDR, Scatter, Lens Blur)")]
[Serializable]
public class DepthOfFieldScatter : PostEffectsBase
{
	public DepthOfFieldScatter()
	{
		this.focalLength = 10f;
		this.focalSize = 0.05f;
		this.aperture = 10f;
		this.maxBlurSize = 2f;
		this.blurQuality = DepthOfFieldScatter.BlurQuality.Medium;
		this.blurResolution = DepthOfFieldScatter.BlurResolution.Low;
		this.foregroundOverlap = 0.55f;
		this.focalDistance01 = 10f;
	}

	public override bool CheckResources()
	{
		this.CheckSupport(true);
		this.dofHdrMaterial = this.CheckShaderAndCreateMaterial(this.dofHdrShader, this.dofHdrMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	public virtual float FocalDistance01(float worldDist)
	{
		return this.GetComponent<Camera>().WorldToViewportPoint((worldDist - this.GetComponent<Camera>().nearClipPlane) * this.GetComponent<Camera>().transform.forward + this.GetComponent<Camera>().transform.position).z / (this.GetComponent<Camera>().farClipPlane - this.GetComponent<Camera>().nearClipPlane);
	}

	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			float num = this.maxBlurSize;
			int num2 = (this.blurResolution != DepthOfFieldScatter.BlurResolution.High) ? 2 : 1;
			if (this.aperture < (float)0)
			{
				this.aperture = (float)0;
			}
			if (this.maxBlurSize < (float)0)
			{
				this.maxBlurSize = (float)0;
			}
			this.focalSize = Mathf.Clamp(this.focalSize, (float)0, 0.3f);
			this.focalDistance01 = ((!this.focalTransform) ? this.FocalDistance01(this.focalLength) : (this.GetComponent<Camera>().WorldToViewportPoint(this.focalTransform.position).z / this.GetComponent<Camera>().farClipPlane));
			bool flag = source.format == RenderTextureFormat.ARGBHalf;
			RenderTexture renderTexture = (num2 <= 1) ? null : RenderTexture.GetTemporary(source.width / num2, source.height / num2, 0, source.format);
			if (renderTexture)
			{
				renderTexture.filterMode = FilterMode.Bilinear;
			}
			RenderTexture temporary = RenderTexture.GetTemporary(source.width / (2 * num2), source.height / (2 * num2), 0, source.format);
			RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / (2 * num2), source.height / (2 * num2), 0, source.format);
			if (temporary)
			{
				temporary.filterMode = FilterMode.Bilinear;
			}
			if (temporary2)
			{
				temporary2.filterMode = FilterMode.Bilinear;
			}
			this.dofHdrMaterial.SetVector("_CurveParams", new Vector4((float)0, this.focalSize, this.aperture / 10f, this.focalDistance01));
			if (this.foregroundBlur)
			{
				RenderTexture temporary3 = RenderTexture.GetTemporary(source.width / (2 * num2), source.height / (2 * num2), 0, source.format);
				Graphics.Blit(source, temporary2, this.dofHdrMaterial, 4);
				this.dofHdrMaterial.SetTexture("_FgOverlap", temporary2);
				float num3 = num * this.foregroundOverlap * 0.225f;
				this.dofHdrMaterial.SetVector("_Offsets", new Vector4((float)0, num3, (float)0, num3));
				Graphics.Blit(temporary2, temporary3, this.dofHdrMaterial, 2);
				this.dofHdrMaterial.SetVector("_Offsets", new Vector4(num3, (float)0, (float)0, num3));
				Graphics.Blit(temporary3, temporary, this.dofHdrMaterial, 2);
				this.dofHdrMaterial.SetTexture("_FgOverlap", null);
				Graphics.Blit(temporary, source, this.dofHdrMaterial, 7);
				RenderTexture.ReleaseTemporary(temporary3);
			}
			else
			{
				this.dofHdrMaterial.SetTexture("_FgOverlap", null);
			}
			Graphics.Blit(source, source, this.dofHdrMaterial, (!this.foregroundBlur) ? 0 : 3);
			RenderTexture renderTexture2 = source;
			if (num2 > 1)
			{
				Graphics.Blit(source, renderTexture, this.dofHdrMaterial, 6);
				renderTexture2 = renderTexture;
			}
			Graphics.Blit(renderTexture2, temporary2, this.dofHdrMaterial, 6);
			Graphics.Blit(temporary2, renderTexture2, this.dofHdrMaterial, 8);
			int pass = 10;
			DepthOfFieldScatter.BlurQuality blurQuality = this.blurQuality;
			if (blurQuality == DepthOfFieldScatter.BlurQuality.Low)
			{
				pass = ((num2 <= 1) ? 10 : 13);
			}
			else if (blurQuality == DepthOfFieldScatter.BlurQuality.Medium)
			{
				pass = ((num2 <= 1) ? 11 : 12);
			}
			else if (blurQuality == DepthOfFieldScatter.BlurQuality.High)
			{
				pass = ((num2 <= 1) ? 14 : 15);
			}
			else
			{
				UnityEngine.Debug.Log("DOF couldn't find valid blur quality setting", this.transform);
			}
			if (this.visualizeFocus)
			{
				Graphics.Blit(source, destination, this.dofHdrMaterial, 1);
			}
			else
			{
				this.dofHdrMaterial.SetVector("_Offsets", new Vector4((float)0, (float)0, (float)0, num));
				this.dofHdrMaterial.SetTexture("_LowRez", renderTexture2);
				Graphics.Blit(source, destination, this.dofHdrMaterial, pass);
			}
			if (temporary)
			{
				RenderTexture.ReleaseTemporary(temporary);
			}
			if (temporary2)
			{
				RenderTexture.ReleaseTemporary(temporary2);
			}
			if (renderTexture)
			{
				RenderTexture.ReleaseTemporary(renderTexture);
			}
		}
	}

	public override void Main()
	{
	}

	public bool visualizeFocus;

	public float focalLength;

	public float focalSize;

	public float aperture;

	public Transform focalTransform;

	public float maxBlurSize;

	public DepthOfFieldScatter.BlurQuality blurQuality;

	public DepthOfFieldScatter.BlurResolution blurResolution;

	public bool foregroundBlur;

	public float foregroundOverlap;

	public Shader dofHdrShader;

	private float focalDistance01;

	private Material dofHdrMaterial;

	[Serializable]
	public enum BlurQuality
	{
		Low,
		Medium,
		High
	}

	[Serializable]
	public enum BlurResolution
	{
		High,
		Low
	}
}
