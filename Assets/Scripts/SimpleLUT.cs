using System;
using UnityEngine;

[ExecuteInEditMode]
public class SimpleLUT : MonoBehaviour
{
	private void Awake()
	{
		SimpleLUT.Instance = this;
	}

	private void CreateMaterial()
	{
		if (this.Shader == null)
		{
			this.material = null;
			UnityEngine.Debug.LogError("Must set a shader to use LUT");
			return;
		}
		this.material = new Material(this.Shader);
		this.material.hideFlags = HideFlags.DontSave;
	}

	private void OnEnable()
	{
		if (base.GetComponent<Camera>() == null)
		{
			UnityEngine.Debug.LogError("This script must be attached to a Camera");
		}
	}

	private void Update()
	{
		/*if (this.Shader != this.previousShader)
		{
			this.previousShader = this.Shader;
			this.CreateMaterial();
		}
		if (this.LookupTexture != this.previousTexture)
		{
			this.previousTexture = this.LookupTexture;
			this.Convert(this.LookupTexture);
		}*/
	}

	private void OnDestroy()
	{
		if (this.converted3DLut != null)
		{
			UnityEngine.Object.DestroyImmediate(this.converted3DLut);
		}
		this.converted3DLut = null;
	}

	public void SetIdentityLut()
	{
		if (!SystemInfo.supports3DTextures)
		{
			return;
		}
		if (this.converted3DLut != null)
		{
			UnityEngine.Object.DestroyImmediate(this.converted3DLut);
		}
		int num = 16;
		Color[] array = new Color[num * num * num];
		float num2 = 1f / (1f * (float)num - 1f);
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num; j++)
			{
				for (int k = 0; k < num; k++)
				{
					array[i + j * num + k * num * num] = new Color((float)i * 1f * num2, (float)j * 1f * num2, (float)k * 1f * num2, 1f);
				}
			}
		}
		this.converted3DLut = new Texture3D(num, num, num, TextureFormat.ARGB32, false);
		this.converted3DLut.SetPixels(array);
		this.converted3DLut.Apply();
		this.lutSize = this.converted3DLut.width;
		this.converted3DLut.wrapMode = TextureWrapMode.Clamp;
	}

	public bool ValidDimensions(Texture2D tex2d)
	{
		if (tex2d == null)
		{
			return false;
		}
		int height = tex2d.height;
		return height == Mathf.FloorToInt(Mathf.Sqrt((float)tex2d.width));
	}

	internal bool Convert(Texture2D lookupTexture)
	{
		if (!SystemInfo.supports3DTextures)
		{
			UnityEngine.Debug.LogError("System does not support 3D textures");
			return false;
		}
		if (lookupTexture == null)
		{
			this.SetIdentityLut();
		}
		else
		{
			if (this.converted3DLut != null)
			{
				UnityEngine.Object.DestroyImmediate(this.converted3DLut);
			}
			if (lookupTexture.mipmapCount > 1)
			{
				UnityEngine.Debug.LogError("Lookup texture must not have mipmaps");
				return false;
			}
			try
			{
				int num = lookupTexture.width * lookupTexture.height;
				num = lookupTexture.height;
				if (!this.ValidDimensions(lookupTexture))
				{
					UnityEngine.Debug.LogError("Lookup texture dimensions must be a power of two. The height must equal the square root of the width.");
					return false;
				}
				Color[] pixels = lookupTexture.GetPixels();
				Color[] array = new Color[pixels.Length];
				for (int i = 0; i < num; i++)
				{
					for (int j = 0; j < num; j++)
					{
						for (int k = 0; k < num; k++)
						{
							int num2 = num - j - 1;
							array[i + j * num + k * num * num] = pixels[k * num + i + num2 * num * num];
						}
					}
				}
				this.converted3DLut = new Texture3D(num, num, num, TextureFormat.ARGB32, false);
				this.converted3DLut.SetPixels(array);
				this.converted3DLut.Apply();
				this.lutSize = this.converted3DLut.width;
				this.converted3DLut.wrapMode = TextureWrapMode.Clamp;
			}
			catch (Exception arg)
			{
				UnityEngine.Debug.LogError("Unable to convert texture to LUT texture, make sure it is read/write. Error: " + arg);
			}
			return true;
		}
		return true;
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.converted3DLut == null)
		{
			this.SetIdentityLut();
		}
		if (this.converted3DLut == null || this.material == null)
		{
			Graphics.Blit(source, destination);
			return;
		}
		this.material.SetTexture("_MainTex", source);
		this.material.SetTexture("_ClutTex", this.converted3DLut);
		this.material.SetFloat("_Amount", this.Amount);
		this.material.SetColor("_TintColor", this.TintColor);
		this.material.SetFloat("_Hue", this.Hue);
		this.material.SetFloat("_Saturation", this.Saturation + 1f);
		this.material.SetFloat("_Brightness", this.Brightness + 1f);
		this.material.SetFloat("_Contrast", this.Contrast + 1f);
		this.material.SetFloat("_Scale", (float)(this.lutSize - 1) / (1f * (float)this.lutSize));
		this.material.SetFloat("_Offset", 1f / (2f * (float)this.lutSize));
		float num = this.Sharpness * 4f * 0.2f;
		this.material.SetFloat("_SharpnessCenterMultiplier", 1f + 4f * num);
		this.material.SetFloat("_SharpnessEdgeMultiplier", num);
		this.material.SetVector("_ImageWidthFactor", new Vector4(1f / (float)source.width, 0f, 0f, 0f));
		this.material.SetVector("_ImageHeightFactor", new Vector4(0f, 1f / (float)source.height, 0f, 0f));
		Graphics.Blit(source, destination, this.material, (QualitySettings.activeColorSpace != ColorSpace.Linear) ? 0 : 1);
	}

	public static SimpleLUT Instance;

	[Tooltip("Shader to use for the lookup.")]
	public Shader Shader;

	private Shader previousShader;

	[Tooltip("Texture to use for the lookup. Make sure it has read/write enabled and mipmaps disabled. The height must equal the square root of the width.")]
	public Texture2D LookupTexture;

	private Texture2D previousTexture;

	[Range(0f, 1f)]
	[Tooltip("Lerp between original (0) and corrected color (1)")]
	public float Amount = 1f;

	[Tooltip("Tint color, applied to the final pixel")]
	public Color TintColor = Color.white;

	[Range(0f, 360f)]
	[Tooltip("Hue")]
	public float Hue;

	[Range(-1f, 1f)]
	[Tooltip("Saturation")]
	public float Saturation;

	[Range(-1f, 1f)]
	[Tooltip("Brightness")]
	public float Brightness;

	[Range(-1f, 1f)]
	[Tooltip("Contrast")]
	public float Contrast;

	[Range(0f, 1f)]
	[Tooltip("Sharpness")]
	public float Sharpness;

	private Material material;

	private Texture3D converted3DLut;

	private int lutSize;
}
