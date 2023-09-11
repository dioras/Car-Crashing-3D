using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WMG_Graph_Auto_Anim : MonoBehaviour
{
	public void subscribeToEvents(bool val)
	{
		for (int i = 0; i < this.theGraph.lineSeries.Count; i++)
		{
			if (this.theGraph.activeInHierarchy(this.theGraph.lineSeries[i]))
			{
				WMG_Series component = this.theGraph.lineSeries[i].GetComponent<WMG_Series>();
				if (val)
				{
					component.SeriesDataChanged += this.SeriesDataChangedMethod;
				}
				else
				{
					component.SeriesDataChanged -= this.SeriesDataChangedMethod;
				}
			}
		}
	}

	public void addSeriesForAutoAnim(WMG_Series aSeries)
	{
		aSeries.SeriesDataChanged += this.SeriesDataChangedMethod;
	}

	private void SeriesDataChangedMethod(WMG_Series aSeries)
	{
		List<GameObject> points = aSeries.getPoints();
		for (int i = 0; i < points.Count; i++)
		{
			if (aSeries.seriesIsLine)
			{
				GameObject go = points[i];
				string text = aSeries.GetHashCode() + "autoAnim" + i;
				bool isLast = i == points.Count - 1;
				if (aSeries.currentlyAnimating)
				{
					DOTween.Kill(text, false);
					this.animateLinkCallback(aSeries, go, isLast);
				}
				WMG_Anim.animPositionCallbacks(go, this.theGraph.autoAnimationsDuration, this.theGraph.autoAnimationsEasetype, new Vector3(aSeries.AfterPositions()[i].x, aSeries.AfterPositions()[i].y), delegate
				{
					this.animateLinkCallback(aSeries, go, isLast);
				}, delegate
				{
					this.animateLinkCallbackEnd(aSeries, isLast);
				}, text);
			}
			else
			{
				Vector2 changeSpritePositionTo = this.theGraph.getChangeSpritePositionTo(points[i], new Vector2(aSeries.AfterPositions()[i].x, aSeries.AfterPositions()[i].y));
				WMG_Anim.animPosition(points[i], this.theGraph.autoAnimationsDuration, this.theGraph.autoAnimationsEasetype, new Vector3(changeSpritePositionTo.x, changeSpritePositionTo.y));
				WMG_Anim.animSize(points[i], this.theGraph.autoAnimationsDuration, this.theGraph.autoAnimationsEasetype, new Vector2((float)aSeries.AfterWidths()[i], (float)aSeries.AfterHeights()[i]));
			}
		}
		List<GameObject> dataLabels = aSeries.getDataLabels();
		for (int j = 0; j < dataLabels.Count; j++)
		{
			if (aSeries.seriesIsLine)
			{
				float x = aSeries.dataLabelsOffset.x;
				float y = aSeries.dataLabelsOffset.y;
				Vector2 changeSpritePositionTo2 = this.theGraph.getChangeSpritePositionTo(dataLabels[j], new Vector2(x, y));
				changeSpritePositionTo2 = new Vector2(changeSpritePositionTo2.x + aSeries.AfterPositions()[j].x, changeSpritePositionTo2.y + aSeries.AfterPositions()[j].y);
				WMG_Anim.animPosition(dataLabels[j], this.theGraph.autoAnimationsDuration, this.theGraph.autoAnimationsEasetype, new Vector3(changeSpritePositionTo2.x, changeSpritePositionTo2.y));
			}
			else
			{
				float y2 = aSeries.dataLabelsOffset.y + aSeries.AfterPositions()[j].y + this.theGraph.barWidth / 2f;
				float x2 = aSeries.dataLabelsOffset.x + aSeries.AfterPositions()[j].x + (float)aSeries.AfterWidths()[j];
				if (aSeries.getBarIsNegative(j))
				{
					x2 = -aSeries.dataLabelsOffset.x - (float)aSeries.AfterWidths()[j] + (float)Mathf.RoundToInt((this.theGraph.barAxisValue - this.theGraph.xAxis.AxisMinValue) / (this.theGraph.xAxis.AxisMaxValue - this.theGraph.xAxis.AxisMinValue) * this.theGraph.xAxisLength);
				}
				if (this.theGraph.orientationType == WMG_Axis_Graph.orientationTypes.vertical)
				{
					y2 = aSeries.dataLabelsOffset.y + aSeries.AfterPositions()[j].y + (float)aSeries.AfterHeights()[j];
					x2 = aSeries.dataLabelsOffset.x + aSeries.AfterPositions()[j].x + this.theGraph.barWidth / 2f;
					if (aSeries.getBarIsNegative(j))
					{
						y2 = -aSeries.dataLabelsOffset.y - (float)aSeries.AfterHeights()[j] + (float)Mathf.RoundToInt((this.theGraph.barAxisValue - this.theGraph.yAxis.AxisMinValue) / (this.theGraph.yAxis.AxisMaxValue - this.theGraph.yAxis.AxisMinValue) * this.theGraph.yAxisLength);
					}
				}
				Vector2 changeSpritePositionTo3 = this.theGraph.getChangeSpritePositionTo(dataLabels[j], new Vector2(x2, y2));
				WMG_Anim.animPosition(dataLabels[j], this.theGraph.autoAnimationsDuration, this.theGraph.autoAnimationsEasetype, new Vector3(changeSpritePositionTo3.x, changeSpritePositionTo3.y));
			}
		}
		if (!aSeries.currentlyAnimating)
		{
			aSeries.currentlyAnimating = true;
		}
	}

	private void animateLinkCallback(WMG_Series aSeries, GameObject aGO, bool isLast)
	{
		WMG_Node component = aGO.GetComponent<WMG_Node>();
		if (component.links.Count != 0)
		{
			WMG_Link component2 = component.links[component.links.Count - 1].GetComponent<WMG_Link>();
			component2.Reposition();
		}
		if (isLast)
		{
			aSeries.updateAreaShading();
		}
		if (aSeries.connectFirstToLast)
		{
			component = aSeries.getPoints()[0].GetComponent<WMG_Node>();
			WMG_Link component3 = component.links[0].GetComponent<WMG_Link>();
			component3.Reposition();
		}
	}

	private void animateLinkCallbackEnd(WMG_Series aSeries, bool isLast)
	{
		aSeries.RepositionLines();
		if (isLast)
		{
			aSeries.updateAreaShading();
		}
		aSeries.currentlyAnimating = false;
	}

	public WMG_Axis_Graph theGraph;
}
