using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using UnityEngine;

public class WMG_X_Sample_1 : MonoBehaviour
{
	public bool plottingData
	{
		get
		{
			return this._plottingData;
		}
		set
		{
			if (this._plottingData != value)
			{
				this._plottingData = value;
				this.plottingDataC.Changed();
			}
		}
	}

	private void Start()
	{
		this.changeObjs.Add(this.plottingDataC);
		GameObject gameObject = UnityEngine.Object.Instantiate(this.emptyGraphPrefab) as GameObject;
		gameObject.transform.SetParent(base.transform, false);
		this.graph = gameObject.GetComponent<WMG_Axis_Graph>();
		this.graph.legend.hideLegend = true;
		this.graph.stretchToParent(gameObject);
		this.graphOverlay = new GameObject();
		this.graphOverlay.AddComponent<RectTransform>();
		this.graphOverlay.name = "Graph Overlay";
		this.graphOverlay.transform.SetParent(gameObject.transform, false);
		this.indicatorGO = (UnityEngine.Object.Instantiate(this.indicatorPrefab) as GameObject);
		this.indicatorGO.transform.SetParent(this.graphOverlay.transform, false);
		this.indicatorGO.SetActive(false);
		this.graph.GraphBackgroundChanged += this.UpdateIndicatorSize;
		this.graph.paddingLeftRight = new Vector2(65f, 60f);
		this.graph.paddingTopBottom = new Vector2(40f, 40f);
		this.graph.xAxis.LabelType = WMG_Axis.labelTypes.ticks;
		this.graph.xAxis.SetLabelsUsingMaxMin = true;
		this.graph.autoAnimationsEnabled = false;
		this.graph.xAxis.hideLabels = true;
		this.graph.xAxis.hideTicks = true;
		this.graph.xAxis.hideGrid = true;
		this.graph.yAxis.AxisNumTicks = 5;
		this.graph.yAxis.hideTicks = true;
		this.graph.axisWidth = 1;
		this.graph.yAxis.MaxAutoGrow = true;
		this.graph.yAxis.MinAutoGrow = true;
		this.series1 = this.graph.addSeries();
		this.series1.pointColor = Color.red;
		this.series1.lineColor = Color.green;
		this.series1.lineScale = 0.5f;
		this.series1.pointWidthHeight = 8f;
		this.graph.changeSpriteColor(this.graph.graphBackground, Color.black);
		if (this.useAreaShading)
		{
			this.series1.areaShadingType = WMG_Series.areaShadingTypes.Gradient;
			this.series1.areaShadingAxisValue = this.graph.yAxis.AxisMinValue;
			this.series1.areaShadingColor = new Color(0.3137255f, 0.392156869f, 0.235294119f, 1f);
		}
		this.graph.tooltipDisplaySeriesName = false;
		this.graph.theTooltip.tooltipLabeler = new WMG_Graph_Tooltip.TooltipLabeler(this.customTooltipLabeler);
		this.graph.yAxis.axisLabelLabeler = new WMG_Axis.AxisLabelLabeler(this.customYAxisLabelLabeler);
		this.series1.seriesDataLabeler = new WMG_Series.SeriesDataLabeler(this.customSeriesDataLabeler);
		this.plottingDataC.OnChange += this.PlottingDataChanged;
		if (this.plotOnStart)
		{
			this.plottingData = true;
		}
	}

	private void PlottingDataChanged()
	{
		if (this.plottingData)
		{
			base.StartCoroutine(this.plotData());
		}
	}

	public IEnumerator plotData()
	{
		for (;;)
		{
			yield return new WaitForSeconds(this.plotIntervalSeconds);
			if (!this.plottingData)
			{
				break;
			}
			this.animateAddPointFromEnd(new Vector2((this.series1.pointValues.Count != 0) ? (this.series1.pointValues[this.series1.pointValues.Count - 1].x + this.xInterval) : 0f, UnityEngine.Random.Range(this.graph.yAxis.AxisMinValue, this.graph.yAxis.AxisMaxValue * 1.2f)), this.plotAnimationSeconds);
			if (this.blinkCurrentPoint)
			{
				this.blinkCurrentPointAnimation(false);
			}
		}
		yield break;
	}

	private void animateAddPointFromEnd(Vector2 pointVec, float animDuration)
	{
		if (this.series1.pointValues.Count == 0)
		{
			this.series1.pointValues.Add(pointVec);
			this.indicatorGO.SetActive(true);
			this.graph.Refresh();
			this.updateIndicator();
		}
		else
		{
			this.series1.pointValues.Add(this.series1.pointValues[this.series1.pointValues.Count - 1]);
			if (pointVec.x > this.graph.xAxis.AxisMaxValue)
			{
				this.addPointAnimTimeline = 0f;
				Vector2 oldEnd = new Vector2(this.series1.pointValues[this.series1.pointValues.Count - 1].x, this.series1.pointValues[this.series1.pointValues.Count - 1].y);
				Vector2 newStart = new Vector2(this.series1.pointValues[1].x, this.series1.pointValues[1].y);
				Vector2 oldStart = new Vector2(this.series1.pointValues[0].x, this.series1.pointValues[0].y);
				WMG_Anim.animFloatCallbacks(() => this.addPointAnimTimeline, delegate(float x)
				{
					this.addPointAnimTimeline = x;
				}, animDuration, 1f, delegate
				{
					this.onUpdateAnimateAddPoint(pointVec, oldEnd, newStart, oldStart);
				}, delegate
				{
					this.onCompleteAnimateAddPoint();
				}, this.plotEaseType);
			}
			else
			{
				WMG_Anim.animVec2CallbackU(() => this.series1.pointValues[this.series1.pointValues.Count - 1], delegate(Vector2 x)
				{
					this.series1.pointValues[this.series1.pointValues.Count - 1] = x;
				}, animDuration, pointVec, delegate
				{
					this.updateIndicator();
				}, this.plotEaseType);
			}
		}
	}

	private void blinkCurrentPointAnimation(bool fromOnCompleteAnimateAdd = false)
	{
		this.graph.Refresh();
		WMG_Node component = this.series1.getLastPoint().GetComponent<WMG_Node>();
		string text = this.series1.GetHashCode() + "blinkingPointAnim";
		DOTween.Kill(text, false);
		this.blinkingTween = component.objectToScale.transform.DOScale(new Vector3(this.blinkScale, this.blinkScale, this.blinkScale), this.blinkAnimDuration).SetEase(this.plotEaseType).SetUpdate(false).SetId(text).SetLoops(-1, LoopType.Yoyo);
		if (this.series1.pointValues.Count > 1)
		{
			WMG_Node component2 = this.series1.getPoints()[this.series1.getPoints().Count - 2].GetComponent<WMG_Node>();
			if (fromOnCompleteAnimateAdd)
			{
				this.blinkingTween.Goto(this.blinkAnimDuration * component2.objectToScale.transform.localScale.x / this.blinkScale, true);
			}
			component2.objectToScale.transform.localScale = Vector3.one;
		}
	}

	private void updateIndicator()
	{
		if (this.series1.getPoints().Count == 0)
		{
			return;
		}
		WMG_Node component = this.series1.getLastPoint().GetComponent<WMG_Node>();
		this.graph.changeSpritePositionToY(this.indicatorGO, component.transform.localPosition.y);
		Vector2 nodeValue = this.series1.getNodeValue(component);
		this.indicatorLabelNumberFormatInfo.CurrencyDecimalDigits = this.indicatorNumDecimals;
		string aText = nodeValue.y.ToString("C", this.indicatorLabelNumberFormatInfo);
		this.graph.changeLabelText(this.indicatorGO.transform.GetChild(0).GetChild(0).gameObject, aText);
	}

	private void onUpdateAnimateAddPoint(Vector2 newEnd, Vector2 oldEnd, Vector2 newStart, Vector2 oldStart)
	{
		this.series1.pointValues[this.series1.pointValues.Count - 1] = WMG_Util.RemapVec2(this.addPointAnimTimeline, 0f, 1f, oldEnd, newEnd);
		this.graph.xAxis.AxisMaxValue = WMG_Util.RemapFloat(this.addPointAnimTimeline, 0f, 1f, oldEnd.x, newEnd.x);
		this.updateIndicator();
		if (this.moveXaxisMinimum)
		{
			this.series1.pointValues[0] = WMG_Util.RemapVec2(this.addPointAnimTimeline, 0f, 1f, oldStart, newStart);
			this.graph.xAxis.AxisMinValue = WMG_Util.RemapFloat(this.addPointAnimTimeline, 0f, 1f, oldStart.x, newStart.x);
		}
	}

	private void onCompleteAnimateAddPoint()
	{
		if (this.moveXaxisMinimum)
		{
			this.series1.pointValues.RemoveAt(0);
			this.blinkCurrentPointAnimation(true);
		}
	}

	private string customTooltipLabeler(WMG_Series aSeries, WMG_Node aNode)
	{
		Vector2 nodeValue = aSeries.getNodeValue(aNode);
		this.tooltipNumberFormatInfo.CurrencyDecimalDigits = aSeries.theGraph.tooltipNumberDecimals;
		string text = nodeValue.y.ToString("C", this.tooltipNumberFormatInfo);
		if (aSeries.theGraph.tooltipDisplaySeriesName)
		{
			text = aSeries.seriesName + ": " + text;
		}
		return text;
	}

	private string customYAxisLabelLabeler(WMG_Axis axis, int labelIndex)
	{
		float num = axis.AxisMinValue + (float)labelIndex * (axis.AxisMaxValue - axis.AxisMinValue) / (float)(axis.axisLabels.Count - 1);
		this.yAxisNumberFormatInfo.CurrencyDecimalDigits = axis.numDecimalsAxisLabels;
		return num.ToString("C", this.yAxisNumberFormatInfo);
	}

	private string customSeriesDataLabeler(WMG_Series series, float val)
	{
		this.seriesDataLabelsNumberFormatInfo.CurrencyDecimalDigits = series.dataLabelsNumDecimals;
		return val.ToString("C", this.seriesDataLabelsNumberFormatInfo);
	}

	private void UpdateIndicatorSize(WMG_Axis_Graph aGraph)
	{
		aGraph.changeSpritePositionTo(this.graphOverlay, aGraph.graphBackground.transform.parent.transform.localPosition);
		float num = aGraph.getSpriteWidth(aGraph.graphBackground) - aGraph.paddingLeftRight[0] - aGraph.paddingLeftRight[1];
		aGraph.changeSpriteSize(this.indicatorGO, Mathf.RoundToInt(num), 2);
		aGraph.changeSpritePositionToX(this.indicatorGO, num / 2f);
	}

	public UnityEngine.Object emptyGraphPrefab;

	public bool plotOnStart;

	[SerializeField]
	private bool _plottingData;

	public float plotIntervalSeconds;

	public float plotAnimationSeconds;

	private Ease plotEaseType = Ease.OutQuad;

	public float xInterval;

	public bool useAreaShading;

	public bool blinkCurrentPoint;

	public float blinkAnimDuration;

	private float blinkScale = 2f;

	public bool moveXaxisMinimum;

	public UnityEngine.Object indicatorPrefab;

	public int indicatorNumDecimals;

	private List<WMG_Change_Obj> changeObjs = new List<WMG_Change_Obj>();

	private WMG_Change_Obj plottingDataC = new WMG_Change_Obj();

	private WMG_Axis_Graph graph;

	private WMG_Series series1;

	private GameObject graphOverlay;

	private GameObject indicatorGO;

	private NumberFormatInfo tooltipNumberFormatInfo = new CultureInfo("en-US", false).NumberFormat;

	private NumberFormatInfo yAxisNumberFormatInfo = new CultureInfo("en-US", false).NumberFormat;

	private NumberFormatInfo seriesDataLabelsNumberFormatInfo = new CultureInfo("en-US", false).NumberFormat;

	private NumberFormatInfo indicatorLabelNumberFormatInfo = new CultureInfo("en-US", false).NumberFormat;

	private float addPointAnimTimeline;

	private Tween blinkingTween;
}
