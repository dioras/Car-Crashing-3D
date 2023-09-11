using System;
using UnityEngine;
using UnityEngine.UI;

public class CUIColorPicker : MonoBehaviour
{
	public Color Color
	{
		get
		{
			return this._color;
		}
		set
		{
			this.Setup(value);
		}
	}

	private void Start()
	{
		this.menuManager = MenuManager.Instance;
	}

	private static void RGBToHSV(Color color, out float h, out float s, out float v)
	{
		float num = Mathf.Min(new float[]
		{
			color.r,
			color.g,
			color.b
		});
		float num2 = Mathf.Max(new float[]
		{
			color.r,
			color.g,
			color.b
		});
		float num3 = num2 - num;
		if (num3 == 0f)
		{
			h = 0f;
		}
		else if (num2 == color.r)
		{
			h = Mathf.Repeat((color.g - color.b) / num3, 6f);
		}
		else if (num2 == color.g)
		{
			h = (color.b - color.r) / num3 + 2f;
		}
		else
		{
			h = (color.r - color.g) / num3 + 4f;
		}
		s = ((num2 != 0f) ? (num3 / num2) : 0f);
		v = num2;
	}

	private static bool GetLocalMouse(GameObject go, out Vector2 result)
	{
		RectTransform rectTransform = (RectTransform)go.transform;
		Vector3 point = rectTransform.InverseTransformPoint(UnityEngine.Input.mousePosition);
		result.x = Mathf.Clamp(point.x, rectTransform.rect.min.x, rectTransform.rect.max.x);
		result.y = Mathf.Clamp(point.y, rectTransform.rect.min.y, rectTransform.rect.max.y);
		return rectTransform.rect.Contains(point);
	}

	private static Vector2 GetWidgetSize(GameObject go)
	{
		RectTransform rectTransform = (RectTransform)go.transform;
		return rectTransform.rect.size;
	}

	private GameObject GO(string name)
	{
		return base.transform.Find(name).gameObject;
	}

	private void Setup(Color inputColor)
	{
		GameObject satvalGO = this.GO("SaturationValue");
		GameObject satvalKnob = this.GO("SaturationValue/Knob");
		GameObject hueGO = this.GO("Hue");
		GameObject hueKnob = this.GO("Hue/Knob");
		GameObject result = this.GO("Result");
		Color[] hueColors = new Color[]
		{
			Color.red,
			Color.yellow,
			Color.green,
			Color.cyan,
			Color.blue,
			Color.magenta
		};
		Color[] satvalColors = new Color[]
		{
			new Color(0f, 0f, 0f),
			new Color(0f, 0f, 0f),
			new Color(1f, 1f, 1f),
			hueColors[0]
		};
		Texture2D texture2D = new Texture2D(1, 7);
		for (int i = 0; i < 7; i++)
		{
			texture2D.SetPixel(0, i, hueColors[i % 6]);
		}
		texture2D.Apply();
		hueGO.GetComponent<Image>().sprite = Sprite.Create(texture2D, new Rect(0f, 0.5f, 1f, 6f), new Vector2(0.5f, 0.5f));
		Vector2 hueSz = CUIColorPicker.GetWidgetSize(hueGO);
		Texture2D satvalTex = new Texture2D(2, 2);
		satvalGO.GetComponent<Image>().sprite = Sprite.Create(satvalTex, new Rect(0.5f, 0.5f, 1f, 1f), new Vector2(0.5f, 0.5f));
		Action resetSatValTexture = delegate()
		{
			for (int j = 0; j < 2; j++)
			{
				for (int k = 0; k < 2; k++)
				{
					satvalTex.SetPixel(k, j, satvalColors[k + j * 2]);
				}
			}
			satvalTex.Apply();
		};
		Vector2 satvalSz = CUIColorPicker.GetWidgetSize(satvalGO);
		float Hue;
		float Saturation;
		float Value;
		CUIColorPicker.RGBToHSV(inputColor, out Hue, out Saturation, out Value);
		Action applyHue = delegate()
		{
			int num = Mathf.Clamp((int)Hue, 0, 5);
			int num2 = (num + 1) % 6;
			Color color = Color.Lerp(hueColors[num], hueColors[num2], Hue - (float)num);
			satvalColors[3] = color;
			resetSatValTexture();
		};
		Action applySaturationValue = delegate()
		{
			Vector2 vector = new Vector2(Saturation, Value);
			Vector2 vector2 = new Vector2(1f - vector.x, 1f - vector.y);
			Color a = vector2.x * vector2.y * satvalColors[0];
			Color b = vector.x * vector2.y * satvalColors[1];
			Color b2 = vector2.x * vector.y * satvalColors[2];
			Color b3 = vector.x * vector.y * satvalColors[3];
			Color color = a + b + b2 + b3;
			Image component = result.GetComponent<Image>();
			component.color = color;
			if (this._color != color)
			{
				this._color = color;
				if (this.ColorChangedCallback != null)
				{
					this.ColorChangedCallback();
				}
			}
		};
		applyHue();
		applySaturationValue();
		satvalKnob.transform.localPosition = new Vector2(Saturation * satvalSz.x, Value * satvalSz.y);
		hueKnob.transform.localPosition = new Vector2(hueKnob.transform.localPosition.x, Hue / 6f * satvalSz.y);
		Action dragH = null;
		Action dragSV = null;
		Action idle = delegate()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Vector2 vector;
				if (CUIColorPicker.GetLocalMouse(hueGO, out vector))
				{
					this._update = dragH;
				}
				else if (CUIColorPicker.GetLocalMouse(satvalGO, out vector))
				{
					this._update = dragSV;
				}
			}
		};
		dragH = delegate()
		{
			Vector2 vector;
			CUIColorPicker.GetLocalMouse(hueGO, out vector);
			Hue = vector.y / hueSz.y * 6f;
			applyHue();
			applySaturationValue();
			hueKnob.transform.localPosition = new Vector2(hueKnob.transform.localPosition.x, vector.y);
			if (Input.GetMouseButtonUp(0))
			{
				this._update = idle;
			}
		};
		dragSV = delegate()
		{
			Vector2 v;
			CUIColorPicker.GetLocalMouse(satvalGO, out v);
			Saturation = v.x / satvalSz.x;
			Value = v.y / satvalSz.y;
			applySaturationValue();
			satvalKnob.transform.localPosition = v;
			if (Input.GetMouseButtonUp(0))
			{
				this._update = idle;
			}
		};
		this._update = idle;
	}

	public void SetRandomColor()
	{
		System.Random random = new System.Random();
		float r = (float)(random.Next() % 1000) / 1000f;
		float g = (float)(random.Next() % 1000) / 1000f;
		float b = (float)(random.Next() % 1000) / 1000f;
		this.Color = new Color(r, g, b);
	}

	private void Awake()
	{
		this.Color = Color.red;
	}

	private void Update()
	{
		this._update();
	}

	private Color _color = Color.red;

	private Action _update;

	public Action ColorChangedCallback;

	private MenuManager menuManager;
}
