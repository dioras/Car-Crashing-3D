using System;
using UnityEngine;

public class WMG_Bezier_Band : WMG_GUI_Functions
{
	public void Init(WMG_Bezier_Band_Graph graph)
	{
		this.graph = graph;
		this.bandFillSprite = WMG_Util.createSprite(base.getTexture(this.bandFillSpriteGO));
		this.bandLineSprite = WMG_Util.createSprite(base.getTexture(this.bandLineSpriteGO));
		this.texSize = this.bandFillSprite.texture.width;
		this.bandFillColors = new Color[this.texSize * this.texSize];
		this.bandLineColors = new Color[this.texSize * this.texSize];
		base.setTexture(this.bandFillSpriteGO, this.bandFillSprite);
		base.setTexture(this.bandLineSpriteGO, this.bandLineSprite);
		this.size = Mathf.RoundToInt(Mathf.Sqrt((float)this.bandFillColors.Length));
		this.maxS = this.size - 1;
	}

	public void setCumulativePercents(float val, float prev)
	{
		this.cumulativePercent = val / this.graph.TotalVal;
		this.prevCumulativePercent = prev / this.graph.TotalVal;
	}

	public void setFillColor(Color color)
	{
		base.changeSpriteColor(this.bandFillSpriteGO, color);
	}

	public void setLineColor(Color color)
	{
		base.changeSpriteColor(this.bandLineSpriteGO, color);
	}

	public void UpdateBand()
	{
		this.updateColors(ref this.bandFillColors, ref this.bandLineColors);
		this.bandFillSprite.texture.SetPixels(this.bandFillColors);
		this.bandFillSprite.texture.Apply();
		this.bandLineSprite.texture.SetPixels(this.bandLineColors);
		this.bandLineSprite.texture.Apply();
	}

	private void updateColors(ref Color[] bandFillColors, ref Color[] bandLineColors)
	{
		bandFillColors = new Color[this.texSize * this.texSize];
		bandLineColors = new Color[this.texSize * this.texSize];
		int num = Mathf.Max(0, this.graph.bandLineWidth - 1);
		int[] array = new int[this.size];
		for (int i = 0; i <= 1; i++)
		{
			float num2 = this.graph.startHeightPercent * (float)this.maxS;
			float num3 = this.prevCumulativePercent;
			if (i == 1)
			{
				num3 = this.cumulativePercent;
			}
			Vector2 p = new Vector2(0f, (float)this.maxS / 2f + (0.5f - num3) * num2);
			Vector2 p2 = new Vector2((float)this.maxS, (1f - num3) * (float)this.maxS - (float)((i != 1) ? this.graph.bandSpacing : (-(float)this.graph.bandSpacing)));
			if (i == 1 && this.cumulativePercent == 1f && this.graph.bandSpacing < this.graph.bandLineWidth)
			{
				p2 = new Vector2(p2.x, p2.y + (float)(this.graph.bandLineWidth - this.graph.bandSpacing));
			}
			Vector2 p3 = new Vector2(this.graph.cubicBezierP1.x * p2.x, p.y + this.graph.cubicBezierP1.y * (p2.y - p.y));
			Vector2 p4 = new Vector2((1f - this.graph.cubicBezierP2.x) * p2.x, p2.y - this.graph.cubicBezierP2.y * (p2.y - p.y));
			int[] array2 = new int[this.size];
			for (int j = 0; j < this.size * this.superSamplingRate; j++)
			{
				Vector2 vector = this.cubicBezier(p, p3, p4, p2, (float)j / ((float)(this.size * this.superSamplingRate) - 1f));
				int num4 = Mathf.RoundToInt(vector.x);
				int a = Mathf.RoundToInt(vector.y);
				array2[num4] = Mathf.Max(a, array2[num4]);
			}
			p = new Vector2(p.x, p.y - (float)num);
			p2 = new Vector2(p2.x, p2.y - (float)num);
			p3 = new Vector2(this.graph.cubicBezierP1.x * p2.x, p.y + this.graph.cubicBezierP1.y * (p2.y - p.y));
			p4 = new Vector2((1f - this.graph.cubicBezierP2.x) * p2.x, p2.y - this.graph.cubicBezierP2.y * (p2.y - p.y));
			int[] array3 = new int[this.size];
			for (int k = 0; k < this.size; k++)
			{
				array3[k] = this.maxS;
			}
			for (int l = 0; l < this.size * this.superSamplingRate; l++)
			{
				Vector2 vector2 = this.cubicBezier(p, p3, p4, p2, (float)l / ((float)(this.size * this.superSamplingRate) - 1f));
				int num5 = Mathf.RoundToInt(vector2.x);
				int a2 = Mathf.RoundToInt(vector2.y);
				array3[num5] = Mathf.Min(a2, array3[num5]);
			}
			for (int m = this.xPad; m < this.size - this.xPad; m++)
			{
				int num6 = array2[m] - array3[m];
				num6++;
				if (i == 0)
				{
					array[m] = array3[m] - 1;
				}
				for (int n = 0; n < num6; n++)
				{
					int num7 = array2[m] - n;
					float num8 = 1f - Mathf.Abs((float)n - (float)num6 / 2f) / ((float)num6 / 2f);
					bandLineColors[m + this.size * num7] = new Color(1f, 1f, 1f, num8 * num8);
				}
				if (i == 1)
				{
					int num9 = array[m] - array2[m];
					for (int num10 = 0; num10 < num9; num10++)
					{
						int num11 = array[m] - num10;
						bandFillColors[m + this.size * num11] = Color.white;
					}
					int num12 = Mathf.Min(num / 2, num9 / 2);
					for (int num13 = 0; num13 < num12; num13++)
					{
						float a3 = ((float)num13 + 1f) / (float)num12;
						int num14 = array[m] - num13;
						bandFillColors[m + this.size * num14] = new Color(1f, 1f, 1f, a3);
					}
					for (int num15 = 0; num15 < num12; num15++)
					{
						float a4 = ((float)num15 + 1f) / (float)num12;
						int num16 = array2[m] + num15;
						bandFillColors[m + this.size * num16] = new Color(1f, 1f, 1f, a4);
					}
				}
			}
		}
	}

	private Vector2 cubicBezier(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
	{
		float num = 1f - t;
		float d = num * num * num;
		float d2 = num * num * t * 3f;
		float d3 = num * t * t * 3f;
		float d4 = t * t * t;
		return d * p0 + d2 * p1 + d3 * p2 + d4 * p3;
	}

	public GameObject bandFillSpriteGO;

	public GameObject bandLineSpriteGO;

	public GameObject labelParent;

	public GameObject percentLabel;

	public GameObject label;

	private Sprite bandFillSprite;

	private Sprite bandLineSprite;

	private Material bandFillMat;

	private Material bandLineMat;

	private Color[] bandFillColors;

	private Color[] bandLineColors;

	private int texSize;

	private WMG_Bezier_Band_Graph graph;

	public float cumulativePercent;

	public float prevCumulativePercent;

	private int size;

	private int maxS;

	private int superSamplingRate = 3;

	private int xPad = 2;
}
