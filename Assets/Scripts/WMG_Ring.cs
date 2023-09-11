using System;
using UnityEngine;

public class WMG_Ring : WMG_GUI_Functions
{
	public void initialize(WMG_Ring_Graph graph)
	{
		this.ringSprite = WMG_Util.createSprite(base.getTexture(this.ring));
		this.bandSprite = WMG_Util.createSprite(base.getTexture(this.band));
		this.ringTexSize = this.ringSprite.texture.width;
		this.bandTexSize = this.bandSprite.texture.width;
		this.ringColors = new Color[this.ringTexSize * this.ringTexSize];
		this.bandColors = new Color[this.bandTexSize * this.bandTexSize];
		base.setTexture(this.ring, this.ringSprite);
		base.setTexture(this.band, this.bandSprite);
		this.graph = graph;
		base.changeSpriteParent(this.label, graph.ringLabelsParent);
	}

	public void updateRing(int ringNum)
	{
		float ringRadius = this.graph.getRingRadius(ringNum);
		WMG_Util.updateBandColors(ref this.ringColors, this.graph.outerRadius * 2f, ringRadius - this.graph.ringWidth, ringRadius, this.graph.antiAliasing, this.graph.antiAliasingStrength, null);
		this.ringSprite.texture.SetPixels(this.ringColors);
		this.ringSprite.texture.Apply();
		if (this.graph.bandMode)
		{
			base.SetActive(this.band, true);
			WMG_Util.updateBandColors(ref this.bandColors, this.graph.outerRadius * 2f, ringRadius + this.graph.bandPadding, this.graph.getRingRadius(ringNum + 1) - this.graph.ringWidth - this.graph.bandPadding, this.graph.antiAliasing, this.graph.antiAliasingStrength, null);
			this.bandSprite.texture.SetPixels(this.bandColors);
			this.bandSprite.texture.Apply();
		}
		else
		{
			base.SetActive(this.band, false);
		}
	}

	public void updateRingPoint(int ringNum)
	{
		float ringRadius = this.graph.getRingRadius(ringNum);
		base.changeSpritePositionToY(this.labelPoint, -(ringRadius - this.graph.RingWidthFactor * this.graph.ringWidth / 2f));
		int num = Mathf.RoundToInt(this.graph.RingWidthFactor * this.graph.ringWidth + this.graph.RingWidthFactor * this.graph.ringPointWidthFactor);
		base.changeSpriteSize(this.labelPoint, num, num);
	}

	public GameObject ring;

	public GameObject band;

	public GameObject label;

	public GameObject textLine;

	public GameObject labelText;

	public GameObject labelPoint;

	public GameObject labelBackground;

	public GameObject line;

	private Sprite ringSprite;

	private Sprite bandSprite;

	private Color[] ringColors;

	private Color[] bandColors;

	private WMG_Ring_Graph graph;

	private int ringTexSize;

	private int bandTexSize;
}
