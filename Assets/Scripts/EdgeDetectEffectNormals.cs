using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Edge Detection (Geometry)")]
[ExecuteInEditMode]
[Serializable]
public class EdgeDetectEffectNormals : PostEffectsBase
{
	public EdgeDetectEffectNormals()
	{
		this.mode = EdgeDetectMode.Thin;
		this.sensitivityDepth = 1f;
		this.sensitivityNormals = 1f;
		this.edgesOnlyBgColor = Color.white;
	}

	public virtual void OnDisable()
	{
		if (this.edgeDetectMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.edgeDetectMaterial);
		}
	}

	public override bool CheckResources()
	{
		this.CheckSupport(true);
		this.edgeDetectMaterial = this.CheckShaderAndCreateMaterial(this.edgeDetectShader, this.edgeDetectMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	[ImageEffectOpaque]
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			Vector2 vector = new Vector2(this.sensitivityDepth, this.sensitivityNormals);
			source.filterMode = FilterMode.Point;
			this.edgeDetectMaterial.SetVector("sensitivity", new Vector4(vector.x, vector.y, 1f, vector.y));
			this.edgeDetectMaterial.SetFloat("_BgFade", this.edgesOnly);
			Vector4 value = this.edgesOnlyBgColor;
			this.edgeDetectMaterial.SetVector("_BgColor", value);
			if (this.mode == EdgeDetectMode.Thin)
			{
				Graphics.Blit(source, destination, this.edgeDetectMaterial, 0);
			}
			else
			{
				Graphics.Blit(source, destination, this.edgeDetectMaterial, 1);
			}
		}
	}

	public override void Main()
	{
	}

	public EdgeDetectMode mode;

	public float sensitivityDepth;

	public float sensitivityNormals;

	public float edgesOnly;

	public Color edgesOnlyBgColor;

	public Shader edgeDetectShader;

	private Material edgeDetectMaterial;
}
