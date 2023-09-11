using System;
using UnityEngine;
using UnityEngine.UI;

public class WMG_Graph_Tooltip : WMG_GUI_Functions
{
	private void Start()
	{
		this._canvas = this.theGraph.toolTipPanel.GetComponent<Graphic>().canvas;
	}

	private void Update()
	{
		if (this.theGraph.tooltipEnabled)
		{
			if (this.isTooltipObjectNull())
			{
				return;
			}
			if (base.getControlVisibility(this.theGraph.toolTipPanel))
			{
				this.repositionTooltip();
			}
		}
	}

	public void subscribeToEvents(bool val)
	{
		if (val)
		{
			this.theGraph.WMG_MouseEnter += this.TooltipNodeMouseEnter;
			this.theGraph.WMG_MouseEnter_Leg += this.TooltipLegendNodeMouseEnter;
			this.theGraph.WMG_Link_MouseEnter_Leg += this.TooltipLegendLinkMouseEnter;
			this.tooltipLabeler = new WMG_Graph_Tooltip.TooltipLabeler(this.defaultTooltipLabeler);
		}
		else
		{
			this.theGraph.WMG_MouseEnter -= this.TooltipNodeMouseEnter;
			this.theGraph.WMG_MouseEnter_Leg -= this.TooltipLegendNodeMouseEnter;
			this.theGraph.WMG_Link_MouseEnter_Leg -= this.TooltipLegendLinkMouseEnter;
		}
	}

	private bool isTooltipObjectNull()
	{
		return this.theGraph.toolTipPanel == null || this.theGraph.toolTipLabel == null;
	}

	private void repositionTooltip()
	{
		Vector3 position;
		RectTransformUtility.ScreenPointToWorldPointInRectangle(this.theGraph.toolTipPanel.GetComponent<RectTransform>(), new Vector2(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y), (this._canvas.renderMode != RenderMode.ScreenSpaceOverlay) ? this._canvas.worldCamera : null, out position);
		float x = this.theGraph.tooltipOffset.x;
		float y = this.theGraph.tooltipOffset.y;
		this.theGraph.toolTipPanel.transform.localPosition = this.theGraph.toolTipPanel.transform.parent.InverseTransformPoint(position) + new Vector3(x, y + 13f, 0f);
		this.EnsureTooltipStaysOnScreen(position, x, y);
	}

	private void EnsureTooltipStaysOnScreen(Vector3 position, float offsetX, float offsetY)
	{
		Vector3 position2 = this.theGraph.toolTipPanel.transform.position;
		Vector3[] array = new Vector3[4];
		((RectTransform)this.theGraph.toolTipPanel.transform).GetWorldCorners(array);
		float num = array[2].x - array[0].x;
		float num2 = array[1].y - array[0].y;
		float num3 = position.x + offsetX + num - (float)Screen.width;
		if (num3 > 0f)
		{
			position2 = new Vector3(position.x - num3 + offsetX, position2.y, position2.z);
		}
		else
		{
			num3 = position.x + offsetX;
			if (num3 < 0f)
			{
				position2 = new Vector3(position.x - num3 + offsetX, position2.y, position2.z);
			}
		}
		float num4 = position.y + offsetY + num2 - (float)Screen.height;
		if (num4 > 0f)
		{
			position2 = new Vector3(position2.x, position.y - num4 + offsetY + num2 / 2f, position2.z);
		}
		else
		{
			num4 = position.y + offsetY;
			if (num4 < 0f)
			{
				position2 = new Vector3(position2.x, position.y - num4 + offsetY + num2 / 2f, position2.z);
			}
		}
		this.theGraph.toolTipPanel.transform.position = position2;
	}

	private void TooltipNodeMouseEnter(WMG_Series aSeries, WMG_Node aNode, bool state)
	{
		if (this.isTooltipObjectNull())
		{
			return;
		}
		if (state)
		{
			base.changeLabelText(this.theGraph.toolTipLabel, this.tooltipLabeler(aSeries, aNode));
			base.changeSpriteWidth(this.theGraph.toolTipPanel, Mathf.RoundToInt(base.getSpriteWidth(this.theGraph.toolTipLabel)) + 24);
			this.repositionTooltip();
			base.showControl(this.theGraph.toolTipPanel);
			base.bringSpriteToFront(this.theGraph.toolTipPanel);
			Vector3 newScale = new Vector3(2f, 2f, 1f);
			if (!aSeries.seriesIsLine)
			{
				if (this.theGraph.orientationType == WMG_Axis_Graph.orientationTypes.vertical)
				{
					newScale = new Vector3(1f, 1.1f, 1f);
				}
				else
				{
					newScale = new Vector3(1.1f, 1f, 1f);
				}
			}
			this.performTooltipAnimation(aNode.transform, newScale);
		}
		else
		{
			base.hideControl(this.theGraph.toolTipPanel);
			base.sendSpriteToBack(this.theGraph.toolTipPanel);
			this.performTooltipAnimation(aNode.transform, new Vector3(1f, 1f, 1f));
		}
	}

	private string defaultTooltipLabeler(WMG_Series aSeries, WMG_Node aNode)
	{
		Vector2 nodeValue = aSeries.getNodeValue(aNode);
		float num = Mathf.Pow(10f, (float)aSeries.theGraph.tooltipNumberDecimals);
		string text = (Mathf.Round(nodeValue.x * num) / num).ToString();
		string text2 = (Mathf.Round(nodeValue.y * num) / num).ToString();
		string text3;
		if (aSeries.seriesIsLine)
		{
			text3 = string.Concat(new string[]
			{
				"(",
				text,
				", ",
				text2,
				")"
			});
		}
		else
		{
			text3 = text2;
		}
		if (aSeries.theGraph.tooltipDisplaySeriesName)
		{
			text3 = aSeries.seriesName + ": " + text3;
		}
		return text3;
	}

	private void TooltipLegendNodeMouseEnter(WMG_Series aSeries, WMG_Node aNode, bool state)
	{
		if (this.isTooltipObjectNull())
		{
			return;
		}
		if (state)
		{
			base.changeLabelText(this.theGraph.toolTipLabel, aSeries.seriesName);
			base.changeSpriteWidth(this.theGraph.toolTipPanel, Mathf.RoundToInt(base.getSpriteWidth(this.theGraph.toolTipLabel)) + 24);
			this.repositionTooltip();
			base.showControl(this.theGraph.toolTipPanel);
			base.bringSpriteToFront(this.theGraph.toolTipPanel);
			this.performTooltipAnimation(aNode.transform, new Vector3(2f, 2f, 1f));
		}
		else
		{
			base.hideControl(this.theGraph.toolTipPanel);
			base.sendSpriteToBack(this.theGraph.toolTipPanel);
			this.performTooltipAnimation(aNode.transform, new Vector3(1f, 1f, 1f));
		}
	}

	private void TooltipLegendLinkMouseEnter(WMG_Series aSeries, WMG_Link aLink, bool state)
	{
		if (this.isTooltipObjectNull())
		{
			return;
		}
		if (!aSeries.hidePoints)
		{
			return;
		}
		if (state)
		{
			base.changeLabelText(this.theGraph.toolTipLabel, aSeries.seriesName);
			base.changeSpriteWidth(this.theGraph.toolTipPanel, Mathf.RoundToInt(base.getSpriteWidth(this.theGraph.toolTipLabel)) + 24);
			this.repositionTooltip();
			base.showControl(this.theGraph.toolTipPanel);
			base.bringSpriteToFront(this.theGraph.toolTipPanel);
			this.performTooltipAnimation(aLink.transform, new Vector3(2f, 1.05f, 1f));
		}
		else
		{
			base.hideControl(this.theGraph.toolTipPanel);
			base.sendSpriteToBack(this.theGraph.toolTipPanel);
			this.performTooltipAnimation(aLink.transform, new Vector3(1f, 1f, 1f));
		}
	}

	private void performTooltipAnimation(Transform trans, Vector3 newScale)
	{
		if (this.theGraph.tooltipAnimationsEnabled)
		{
			WMG_Anim.animScale(trans.gameObject, this.theGraph.tooltipAnimationsDuration, this.theGraph.tooltipAnimationsEasetype, newScale, 0f);
		}
	}

	public WMG_Graph_Tooltip.TooltipLabeler tooltipLabeler;

	public WMG_Axis_Graph theGraph;

	private Canvas _canvas;

	public delegate string TooltipLabeler(WMG_Series series, WMG_Node node);
}
