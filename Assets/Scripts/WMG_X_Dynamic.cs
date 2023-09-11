using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WMG_X_Dynamic : MonoBehaviour
{
	private void Start()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.graphPrefab);
		this.graph = gameObject.GetComponent<WMG_Axis_Graph>();
		this.graph.changeSpriteParent(gameObject, base.gameObject);
		this.graph.changeSpritePositionTo(gameObject, Vector3.zero);
		this.graph.graphTitleOffset = new Vector2(0f, 60f);
		this.graph.autoAnimationsDuration = this.testInterval - 0.1f;
		this.waitTime = new WaitForSeconds(this.testInterval);
		this.animDuration = this.testInterval - 0.1f;
		if (this.animDuration < 0f)
		{
			this.animDuration = 0f;
		}
		if (this.performTests)
		{
			base.StartCoroutine(this.startTests());
		}
	}

	private void Update()
	{
	}

	private IEnumerator startTests()
	{
		yield return new WaitForSeconds(this.testGroupInterval);
		this.graph.graphTitleString = "Animation Function Tests";
		base.StartCoroutine(this.animationFunctionTests());
		if (!this.noTestDelay)
		{
			yield return new WaitForSeconds(this.testInterval * 12f);
		}
		yield return new WaitForSeconds(this.testGroupInterval);
		this.graph.graphTitleString = "Auto Animation Tests";
		base.StartCoroutine(this.autoAnimationTests());
		if (!this.noTestDelay)
		{
			yield return new WaitForSeconds(this.testInterval * 15f);
		}
		yield return new WaitForSeconds(this.testGroupInterval);
		this.graph.graphTitleString = "Graph Type and Orientation Tests";
		base.StartCoroutine(this.graphTypeAndOrientationTests());
		if (!this.noTestDelay)
		{
			yield return new WaitForSeconds(this.testInterval * 13f);
		}
		yield return new WaitForSeconds(this.testGroupInterval);
		this.graph.graphTitleString = "Data Labels Tests";
		base.StartCoroutine(this.dataLabelsTests());
		if (!this.noTestDelay)
		{
			yield return new WaitForSeconds(this.testInterval * 9f);
		}
		yield return new WaitForSeconds(this.testGroupInterval);
		this.graph.graphTitleString = "Series Tests";
		base.StartCoroutine(this.seriesTests());
		if (!this.noTestDelay)
		{
			yield return new WaitForSeconds(this.testInterval * 24f);
		}
		yield return new WaitForSeconds(this.testGroupInterval);
		this.graph.graphTitleString = "Grouping / Null Tests";
		base.StartCoroutine(this.groupingTests());
		if (!this.noTestDelay)
		{
			yield return new WaitForSeconds(this.testInterval * 6f);
		}
		yield return new WaitForSeconds(this.testGroupInterval);
		this.graph.graphTitleString = "Axes Tests";
		base.StartCoroutine(this.axesTests());
		if (!this.noTestDelay)
		{
			yield return new WaitForSeconds(this.testInterval * 13f);
		}
		yield return new WaitForSeconds(this.testGroupInterval);
		this.graph.graphTitleString = "Axes Tests - Bar";
		this.graph.axisWidth = 2;
		this.graph.graphType = WMG_Axis_Graph.graphTypes.bar_side;
		if (!this.noTestDelay)
		{
			yield return new WaitForSeconds(this.testInterval);
		}
		base.StartCoroutine(this.axesTests());
		if (!this.noTestDelay)
		{
			yield return new WaitForSeconds(this.testInterval * 13f);
		}
		yield return new WaitForSeconds(this.testGroupInterval);
		this.graph.graphTitleString = "Axes Tests - Bar - Horizontal";
		this.graph.orientationType = WMG_Axis_Graph.orientationTypes.horizontal;
		if (!this.noTestDelay)
		{
			yield return new WaitForSeconds(this.testInterval);
		}
		base.StartCoroutine(this.axesTests());
		if (!this.noTestDelay)
		{
			yield return new WaitForSeconds(this.testInterval * 13f);
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.line;
		this.graph.orientationType = WMG_Axis_Graph.orientationTypes.vertical;
		this.graph.axisWidth = 4;
		yield return new WaitForSeconds(this.testGroupInterval);
		this.graph.graphTitleString = "Add / Delete Series Tests";
		base.StartCoroutine(this.addDeleteTests());
		if (!this.noTestDelay)
		{
			yield return new WaitForSeconds(this.testInterval * 11f);
		}
		yield return new WaitForSeconds(this.testGroupInterval);
		this.graph.graphTitleString = "Add / Delete Series Tests - Bar";
		this.graph.graphType = WMG_Axis_Graph.graphTypes.bar_side;
		if (!this.noTestDelay)
		{
			yield return new WaitForSeconds(this.testInterval);
		}
		base.StartCoroutine(this.addDeleteTests());
		if (!this.noTestDelay)
		{
			yield return new WaitForSeconds(this.testInterval * 11f);
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.line;
		yield return new WaitForSeconds(this.testGroupInterval);
		this.graph.graphTitleString = "Legend Tests";
		base.StartCoroutine(this.legendTests());
		if (!this.noTestDelay)
		{
			yield return new WaitForSeconds(this.testInterval * 7f);
		}
		yield return new WaitForSeconds(this.testGroupInterval);
		this.graph.graphTitleString = "Hide / Show Tests";
		base.StartCoroutine(this.hideShowTests());
		if (!this.noTestDelay)
		{
			yield return new WaitForSeconds(this.testInterval * 12f);
		}
		yield return new WaitForSeconds(this.testGroupInterval);
		this.graph.graphTitleString = "Grids / Ticks Tests";
		base.StartCoroutine(this.gridsTicksTests());
		if (!this.noTestDelay)
		{
			yield return new WaitForSeconds(this.testInterval * 4f);
		}
		yield return new WaitForSeconds(this.testGroupInterval);
		this.graph.graphTitleString = "Resize Tests";
		base.StartCoroutine(this.sizeTests());
		if (!this.noTestDelay)
		{
			yield return new WaitForSeconds(this.testInterval * 3f);
		}
		yield return new WaitForSeconds(this.testGroupInterval);
		this.graph.graphTitleString = "Resize Tests - Resize Content";
		this.graph.resizeEnabled = true;
		this.graph.resizeProperties = (WMG_Axis_Graph.ResizeProperties)(-1);
		if (!this.noTestDelay)
		{
			yield return new WaitForSeconds(this.testInterval);
		}
		base.StartCoroutine(this.sizeTests());
		if (!this.noTestDelay)
		{
			yield return new WaitForSeconds(this.testInterval * 3f);
		}
		this.graph.resizeEnabled = false;
		this.graph.resizeProperties = (WMG_Axis_Graph.ResizeProperties)0;
		yield return new WaitForSeconds(this.testGroupInterval);
		this.graph.graphTitleString = "Dynamic Data Population via Reflection";
		base.StartCoroutine(this.dynamicDataPopulationViaReflectionTests());
		if (!this.noTestDelay)
		{
			yield return new WaitForSeconds(this.testInterval * 8f);
		}
		yield return new WaitForSeconds(this.testGroupInterval);
		this.graph.graphTitleString = "Real-time Tests";
		base.StartCoroutine(this.realTimeTests());
		if (!this.noTestDelay)
		{
			yield return new WaitForSeconds(10f);
		}
		yield return new WaitForSeconds(this.testGroupInterval);
		this.graph.graphTitleString = "Axis Auto Grow / Shrink Tests";
		base.StartCoroutine(this.axisAutoGrowShrinkTests());
		if (!this.noTestDelay)
		{
			yield return new WaitForSeconds(23f);
		}
		yield return new WaitForSeconds(this.testGroupInterval);
		this.graph.graphTitleString = "Demo Tests Completed Successfully :)";
		yield break;
	}

	private IEnumerator autofitTests()
	{
		string s = "Short";
		string s2 = "Medium length";
		string s3 = "This is a lonnnnnnnnnnnnng string";
		this.graph.yAxis.SetLabelsUsingMaxMin = false;
		this.graph.paddingTopBottom = new Vector2(40f, 60f);
		this.graph.paddingLeftRight = new Vector2(60f, 40f);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.yAxis.axisLabels.SetList(new List<string>
		{
			s,
			s,
			s
		});
		this.graph.xAxis.axisLabels.SetList(new List<string>
		{
			s,
			s,
			s,
			s
		});
		this.graph.autoFitLabels = true;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.yAxis.axisLabels.SetList(new List<string>
		{
			s,
			s,
			s3
		});
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.yAxis.axisLabels.SetList(new List<string>
		{
			s3,
			s,
			s3
		});
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.yAxis.axisLabels.SetList(new List<string>
		{
			s,
			s,
			s
		});
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.xAxis.axisLabels.SetList(new List<string>
		{
			s,
			s2,
			s2,
			s
		});
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.xAxis.axisLabels.SetList(new List<string>
		{
			s,
			s2,
			s2,
			s3
		});
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.xAxis.axisLabels.SetList(new List<string>
		{
			s,
			s,
			s,
			s
		});
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.legend.hideLegend = false;
		this.graph.yAxis.SetLabelsUsingMaxMin = true;
		this.graph.paddingTopBottom = new Vector2(40f, 70f);
		this.graph.paddingLeftRight = new Vector2(45f, 40f);
		this.graph.xAxis.axisLabels.SetList(new List<string>
		{
			"Q1 '15",
			"Q2 '15",
			"Q3 '15",
			"Q4 '15"
		});
		this.graph.autoFitLabels = false;
		yield break;
	}

	private IEnumerator groupingTests()
	{
		List<string> xLabels = new List<string>(this.graph.xAxis.axisLabels);
		WMG_Series s = this.graph.lineSeries[0].GetComponent<WMG_Series>();
		Vector2 p = s.pointValues[3];
		Vector2 p2 = s.pointValues[6];
		Vector2 p3 = s.pointValues[9];
		this.graph.useGroups = true;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s.pointValues.RemoveAt(3);
		s.pointValues.RemoveAt(5);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s.pointValues[9] = new Vector2(-s.pointValues[9].x, s.pointValues[9].y);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.xAxis.LabelType = WMG_Axis.labelTypes.groups;
		this.graph.xAxis.AxisNumTicks = this.graph.groups.Count;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.bar_side;
		this.graph.xAxis.AxisNumTicks = 2;
		WMG_Anim.animFloat(() => this.graph.xAxis.AxisLabelRotation, delegate(float x)
		{
			this.graph.xAxis.AxisLabelRotation = x;
		}, this.animDuration, 60f);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s.pointValues[3] = p;
		s.pointValues[6] = p2;
		s.pointValues[9] = p3;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.line;
		this.graph.xAxis.LabelType = WMG_Axis.labelTypes.ticks_center;
		this.graph.useGroups = false;
		this.graph.xAxis.AxisNumTicks = 5;
		this.graph.xAxis.AxisLabelRotation = 0f;
		this.graph.xAxis.axisLabels.SetList(xLabels);
		yield break;
	}

	private IEnumerator seriesTests()
	{
		WMG_Series s1 = this.graph.lineSeries[0].GetComponent<WMG_Series>();
		WMG_Series s2 = this.graph.lineSeries[1].GetComponent<WMG_Series>();
		List<Vector2> s1Data = s1.pointValues.list;
		List<Vector2> s2Data = s2.pointValues.list;
		Color s1PointColor = s1.pointColor;
		Color s2PointColor = s2.pointColor;
		Vector2 origSize = this.graph.getSpriteSize(this.graph.gameObject);
		WMG_Anim.animSize(this.graph.gameObject, this.animDuration, Ease.Linear, new Vector2(origSize.x * 2f, origSize.y * 2f));
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.deleteSeries();
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s1.pointWidthHeight = 15f;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s1.pointPrefab = 1;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s1.pointPrefab = 0;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s1.linkPrefab = 1;
		s1.lineScale = 1f;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		WMG_Anim.animFloat(() => s1.linePadding, delegate(float x)
		{
			s1.linePadding = x;
		}, this.animDuration, -15f);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s1.linkPrefab = 0;
		s1.lineScale = 0.5f;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		List<Color> pointColors = new List<Color>();
		for (int i = 0; i < s1.pointValues.Count; i++)
		{
			pointColors.Add(new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f));
		}
		s1.usePointColors = true;
		s1.pointColors.SetList(pointColors);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.bar_side;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.line;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s1.usePointColors = false;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s1.UseXDistBetweenToSpace = false;
		this.graph.xAxis.AxisMaxValue = this.graph.yAxis.AxisMaxValue * (this.graph.xAxisLength / this.graph.yAxisLength);
		this.graph.xAxis.SetLabelsUsingMaxMin = true;
		this.graph.xAxis.LabelType = WMG_Axis.labelTypes.ticks;
		this.graph.xAxis.numDecimalsAxisLabels = 1;
		s1.pointValues.SetList(this.graph.GenCircular(s1.pointValues.Count, this.graph.xAxis.AxisMaxValue / 2f, this.graph.yAxis.AxisMaxValue / 2f, this.graph.yAxis.AxisMaxValue / 2f - 2f));
		s1.connectFirstToLast = true;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s1.pointValues.SetList(this.graph.GenCircular(3, this.graph.xAxis.AxisMaxValue / 2f, this.graph.yAxis.AxisMaxValue / 2f, this.graph.yAxis.AxisMaxValue / 2f - 2f));
		this.graph.autoAnimationsEnabled = true;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s1.pointValues.SetList(this.graph.GenCircular2(3, this.graph.xAxis.AxisMaxValue / 2f, this.graph.yAxis.AxisMaxValue / 2f, this.graph.yAxis.AxisMaxValue / 2f - 2f, 90f));
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.autoAnimationsEnabled = false;
		s1.pointValues.SetList(this.graph.GenCircular(50, this.graph.xAxis.AxisMaxValue / 2f, this.graph.yAxis.AxisMaxValue / 2f, this.graph.yAxis.AxisMaxValue / 2f - 2f));
		s1.linePadding = 0f;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s1.hidePoints = true;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s1.lineColor = Color.green;
		WMG_Anim.animFloat(() => s1.lineScale, delegate(float x)
		{
			s1.lineScale = x;
		}, this.animDuration, 2f);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		WMG_Anim.animFloat(() => s1.lineScale, delegate(float x)
		{
			s1.lineScale = x;
		}, this.animDuration, 0.5f);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s1.hideLines = true;
		s1.hidePoints = false;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s1.pointValues.SetList(this.graph.GenRandomXY(50, this.graph.xAxis.AxisMinValue, this.graph.xAxis.AxisMaxValue, this.graph.yAxis.AxisMinValue, this.graph.yAxis.AxisMaxValue));
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.autoAnimationsEnabled = true;
		s1.pointColor = Color.green;
		s1.pointValues.SetList(this.graph.GenRandomXY(50, this.graph.xAxis.AxisMinValue, this.graph.xAxis.AxisMaxValue, this.graph.yAxis.AxisMinValue, this.graph.yAxis.AxisMaxValue));
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.autoAnimationsEnabled = false;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s1.lineColor = Color.white;
		s1.pointColor = s1PointColor;
		s1.hideLines = false;
		s1.pointValues.SetList(s1Data);
		s1.connectFirstToLast = false;
		s1.UseXDistBetweenToSpace = true;
		s1.pointWidthHeight = 10f;
		this.addSeriesWithRandomData();
		this.graph.lineSeries[1].GetComponent<WMG_Series>().pointValues.SetList(s2Data);
		this.graph.lineSeries[1].GetComponent<WMG_Series>().pointColor = s2PointColor;
		this.graph.lineSeries[1].GetComponent<WMG_Series>().pointPrefab = 1;
		this.graph.xAxis.SetLabelsUsingMaxMin = false;
		this.graph.xAxis.axisLabels.SetList(new List<string>
		{
			"Q1 '15",
			"Q2 '15",
			"Q3 '15",
			"Q4 '15"
		});
		this.graph.xAxis.LabelType = WMG_Axis.labelTypes.ticks_center;
		this.graph.xAxis.numDecimalsAxisLabels = 0;
		this.graph.xAxis.AxisMaxValue = 100f;
		WMG_Anim.animSize(this.graph.gameObject, this.animDuration, Ease.Linear, new Vector2(origSize.x, origSize.y));
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		yield break;
	}

	private IEnumerator autoAnimationTests()
	{
		WMG_Series s = this.graph.lineSeries[0].GetComponent<WMG_Series>();
		WMG_Series s2 = this.graph.lineSeries[1].GetComponent<WMG_Series>();
		List<Vector2> s1Data = s.pointValues.list;
		List<Vector2> s2Data = s2.pointValues.list;
		this.graph.autoAnimationsEnabled = true;
		this.graph.graphType = WMG_Axis_Graph.graphTypes.line;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.orientationType = WMG_Axis_Graph.orientationTypes.horizontal;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.orientationType = WMG_Axis_Graph.orientationTypes.vertical;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.bar_side;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.orientationType = WMG_Axis_Graph.orientationTypes.horizontal;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.orientationType = WMG_Axis_Graph.orientationTypes.vertical;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.line;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		List<Vector2> s1Data2 = new List<Vector2>(s1Data);
		s1Data2[6] = new Vector2(s1Data2[6].x, s1Data2[6].y + 5f);
		s.pointValues.SetList(s1Data2);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s.pointValues.SetList(s1Data);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s.pointValues.SetList(this.graph.GenRandomY(s1Data.Count, 0f, (float)(s1Data.Count - 1), this.graph.yAxis.AxisMinValue, this.graph.yAxis.AxisMaxValue));
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s.pointValues.SetList(s1Data);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.autoAnimationsDuration = 2f * this.testInterval - 0.1f;
		s.pointValues.SetList(this.graph.GenRandomY(s1Data.Count, 0f, (float)(s1Data.Count - 1), this.graph.yAxis.AxisMinValue, this.graph.yAxis.AxisMaxValue));
		s2.pointValues.SetList(this.graph.GenRandomY(s2Data.Count, 0f, (float)(s2Data.Count - 1), this.graph.yAxis.AxisMinValue, this.graph.yAxis.AxisMaxValue));
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s.pointValues.SetList(this.graph.GenRandomY(s1Data.Count, 0f, (float)(s1Data.Count - 1), this.graph.yAxis.AxisMinValue, this.graph.yAxis.AxisMaxValue));
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.autoAnimationsDuration = this.testInterval - 0.1f;
		s.pointValues.SetList(s1Data);
		s2.pointValues.SetList(s2Data);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.autoAnimationsEnabled = false;
		yield break;
	}

	private IEnumerator animationFunctionTests()
	{
		List<Vector3> beforeScaleLine = this.graph.getSeriesScaleVectors(true, -1f, 0f);
		List<Vector3> afterScaleLine = this.graph.getSeriesScaleVectors(true, -1f, 1f);
		List<Vector3> beforeScalePoint = this.graph.getSeriesScaleVectors(false, 0f, 0f);
		List<Vector3> afterScalePoint = this.graph.getSeriesScaleVectors(false, 1f, 1f);
		List<Vector3> beforeScaleBar;
		if (this.graph.orientationType == WMG_Axis_Graph.orientationTypes.vertical)
		{
			beforeScaleBar = this.graph.getSeriesScaleVectors(false, 1f, 0f);
		}
		else
		{
			beforeScaleBar = this.graph.getSeriesScaleVectors(false, 0f, 1f);
		}
		this.graph.changeAllLinePivots(WMG_Text_Functions.WMGpivotTypes.Center);
		this.graph.animScaleAllAtOnce(false, this.animDuration, 0f, this.easeType, beforeScaleLine, afterScaleLine);
		this.graph.animScaleAllAtOnce(true, this.animDuration, 0f, this.easeType, beforeScalePoint, afterScalePoint);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.changeAllLinePivots(WMG_Text_Functions.WMGpivotTypes.Top);
		this.graph.animScaleAllAtOnce(false, this.animDuration, 0f, this.easeType, beforeScaleLine, afterScaleLine);
		this.graph.animScaleAllAtOnce(true, this.animDuration, 0f, this.easeType, beforeScalePoint, afterScalePoint);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.changeAllLinePivots(WMG_Text_Functions.WMGpivotTypes.Bottom);
		this.graph.animScaleAllAtOnce(false, this.animDuration, 0f, this.easeType, beforeScaleLine, afterScaleLine);
		this.graph.animScaleAllAtOnce(true, this.animDuration, 0f, this.easeType, beforeScalePoint, afterScalePoint);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.changeAllLinePivots(WMG_Text_Functions.WMGpivotTypes.Center);
		this.graph.animScaleBySeries(false, this.animDuration, 0f, this.easeType, beforeScaleLine, afterScaleLine);
		this.graph.animScaleBySeries(true, this.animDuration, 0f, this.easeType, beforeScalePoint, afterScalePoint);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.changeAllLinePivots(WMG_Text_Functions.WMGpivotTypes.Top);
		this.graph.animScaleBySeries(false, this.animDuration, 0f, this.easeType, beforeScaleLine, afterScaleLine);
		this.graph.animScaleBySeries(true, this.animDuration, 0f, this.easeType, beforeScalePoint, afterScalePoint);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.changeAllLinePivots(WMG_Text_Functions.WMGpivotTypes.Bottom);
		this.graph.animScaleBySeries(false, this.animDuration, 0f, this.easeType, beforeScaleLine, afterScaleLine);
		this.graph.animScaleBySeries(true, this.animDuration, 0f, this.easeType, beforeScalePoint, afterScalePoint);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.changeAllLinePivots(WMG_Text_Functions.WMGpivotTypes.Center);
		this.graph.animScaleOneByOne(false, this.animDuration, 0f, this.easeType, beforeScaleLine, afterScaleLine, 2);
		this.graph.animScaleOneByOne(true, this.animDuration / 2f, this.animDuration / 2f, this.easeType, beforeScalePoint, afterScalePoint, 2);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.changeAllLinePivots(WMG_Text_Functions.WMGpivotTypes.Top);
		this.graph.animScaleOneByOne(false, this.animDuration, 0f, this.easeType, beforeScaleLine, afterScaleLine, 0);
		this.graph.animScaleOneByOne(true, this.animDuration / 2f, this.animDuration / 2f, this.easeType, beforeScalePoint, afterScalePoint, 0);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.changeAllLinePivots(WMG_Text_Functions.WMGpivotTypes.Bottom);
		this.graph.animScaleOneByOne(false, this.animDuration, 0f, this.easeType, beforeScaleLine, afterScaleLine, 1);
		this.graph.animScaleOneByOne(true, this.animDuration / 2f, this.animDuration / 2f, this.easeType, beforeScalePoint, afterScalePoint, 1);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.changeAllLinePivots(WMG_Text_Functions.WMGpivotTypes.Center);
		this.graph.graphType = WMG_Axis_Graph.graphTypes.bar_side;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.animScaleAllAtOnce(true, this.animDuration, 0f, this.easeType, beforeScaleBar, afterScalePoint);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.animScaleBySeries(true, this.animDuration, 0f, this.easeType, beforeScaleBar, afterScalePoint);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.animScaleOneByOne(true, this.animDuration, 0f, this.easeType, beforeScaleBar, afterScalePoint, 0);
		yield break;
	}

	private IEnumerator dynamicDataPopulationViaReflectionTests()
	{
		WMG_Data_Source ds = base.gameObject.AddComponent<WMG_Data_Source>();
		ds.dataSourceType = WMG_Data_Source.WMG_DataSourceTypes.Multiple_Objects_Single_Variable;
		List<Vector2> randomData = this.graph.GenRandomY(this.graph.groups.Count, 1f, (float)this.graph.groups.Count, this.graph.yAxis.AxisMinValue, this.graph.yAxis.AxisMaxValue);
		List<GameObject> dataProviders = new List<GameObject>();
		for (int i = 0; i < this.graph.groups.Count; i++)
		{
			GameObject gameObject = new GameObject();
			dataProviders.Add(gameObject);
			WMG_X_Data_Provider wmg_X_Data_Provider = gameObject.AddComponent<WMG_X_Data_Provider>();
			wmg_X_Data_Provider.vec1 = randomData[i];
			ds.addDataProviderToList<WMG_X_Data_Provider>(wmg_X_Data_Provider);
		}
		ds.setVariableName("vec1");
		ds.variableType = WMG_Data_Source.WMG_VariableTypes.Field;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		WMG_Series s = this.graph.lineSeries[0].GetComponent<WMG_Series>();
		List<Vector2> s1Data = s.pointValues.list;
		s.pointValuesDataSource = ds;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		randomData = this.graph.GenRandomY(this.graph.groups.Count, 1f, (float)this.graph.groups.Count, this.graph.yAxis.AxisMinValue, this.graph.yAxis.AxisMaxValue);
		for (int j = 0; j < this.graph.groups.Count; j++)
		{
			dataProviders[j].GetComponent<WMG_X_Data_Provider>().vec1 = randomData[j];
		}
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.autoAnimationsEnabled = true;
		randomData = this.graph.GenRandomY(this.graph.groups.Count, 1f, (float)this.graph.groups.Count, this.graph.yAxis.AxisMinValue, this.graph.yAxis.AxisMaxValue);
		for (int k = 0; k < this.graph.groups.Count; k++)
		{
			dataProviders[k].GetComponent<WMG_X_Data_Provider>().vec1 = randomData[k];
		}
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.autoAnimationsEnabled = false;
		s.pointValuesDataSource = null;
		s.pointValues.SetList(s1Data);
		yield break;
	}

	private IEnumerator realTimeTests()
	{
		WMG_Data_Source ds = this.graph.lineSeries[0].AddComponent<WMG_Data_Source>();
		WMG_Data_Source ds2 = this.graph.lineSeries[1].AddComponent<WMG_Data_Source>();
		ds.dataSourceType = WMG_Data_Source.WMG_DataSourceTypes.Single_Object_Single_Variable;
		ds2.dataSourceType = WMG_Data_Source.WMG_DataSourceTypes.Single_Object_Single_Variable;
		WMG_Series s = this.graph.lineSeries[0].GetComponent<WMG_Series>();
		WMG_Series s2 = this.graph.lineSeries[1].GetComponent<WMG_Series>();
		this.realTimeObj = UnityEngine.Object.Instantiate<GameObject>(this.realTimePrefab);
		this.graph.changeSpriteParent(this.realTimeObj, base.gameObject);
		ds.setDataProvider<Transform>(this.realTimeObj.transform);
		ds2.setDataProvider<Transform>(this.realTimeObj.transform);
		ds.setVariableName("localPosition.x");
		ds2.setVariableName("localPosition.y");
		s.realTimeDataSource = ds;
		s2.realTimeDataSource = ds2;
		this.graph.xAxis.AxisMaxValue = 0f;
		this.graph.xAxis.AxisMaxValue = 5f;
		this.graph.yAxis.AxisMinValue = -200f;
		this.graph.yAxis.AxisMaxValue = 200f;
		s.seriesName = "Hex X";
		s2.seriesName = "Hex Y";
		s.UseXDistBetweenToSpace = false;
		s2.UseXDistBetweenToSpace = false;
		this.graph.xAxis.SetLabelsUsingMaxMin = true;
		this.graph.xAxis.LabelType = WMG_Axis.labelTypes.ticks;
		this.graph.xAxis.numDecimalsAxisLabels = 1;
		s.StartRealTimeUpdate();
		s2.StartRealTimeUpdate();
		WMG_Anim.animPosition(this.realTimeObj, 3f, Ease.Linear, new Vector3(200f, -150f, 0f));
		yield return new WaitForSeconds(4f);
		WMG_Anim.animPosition(this.realTimeObj, 1f, Ease.Linear, new Vector3(-150f, 100f, 0f));
		yield return new WaitForSeconds(3f);
		WMG_Anim.animPosition(this.realTimeObj, 1f, Ease.Linear, new Vector3(-125f, 75f, 0f));
		yield return new WaitForSeconds(3f);
		s.StopRealTimeUpdate();
		s2.StopRealTimeUpdate();
		yield break;
	}

	private IEnumerator axisAutoGrowShrinkTests()
	{
		WMG_Series s = this.graph.lineSeries[0].GetComponent<WMG_Series>();
		WMG_Series s2 = this.graph.lineSeries[1].GetComponent<WMG_Series>();
		s.ResumeRealTimeUpdate();
		s2.ResumeRealTimeUpdate();
		yield return new WaitForSeconds(1f);
		this.graph.graphTitleString = "Axis Auto Grow / Shrink - Disabled";
		WMG_Anim.animPosition(this.realTimeObj, 1f, Ease.Linear, new Vector3(-125f, 300f, 0f));
		yield return new WaitForSeconds(2f);
		WMG_Anim.animPosition(this.realTimeObj, 1f, Ease.Linear, new Vector3(-125f, 75f, 0f));
		yield return new WaitForSeconds(6f);
		this.graph.graphTitleString = "Axis Auto Grow / Shrink - Enabled";
		this.graph.yAxis.MaxAutoGrow = true;
		this.graph.yAxis.MinAutoGrow = true;
		this.graph.yAxis.MaxAutoShrink = true;
		this.graph.yAxis.MinAutoShrink = true;
		this.graph.autoShrinkAtPercent = 0.6f;
		this.graph.autoGrowAndShrinkByPercent = 0.2f;
		WMG_Anim.animPosition(this.realTimeObj, 2f, Ease.Linear, new Vector3(-125f, 350f, 0f));
		yield return new WaitForSeconds(3f);
		WMG_Anim.animPosition(this.realTimeObj, 2f, Ease.Linear, new Vector3(-125f, 75f, 0f));
		yield return new WaitForSeconds(3f);
		WMG_Anim.animPosition(this.realTimeObj, 2f, Ease.Linear, new Vector3(-5f, 5f, 0f));
		yield return new WaitForSeconds(8f);
		s.StopRealTimeUpdate();
		s2.StopRealTimeUpdate();
		yield break;
	}

	private IEnumerator hideShowTests()
	{
		WMG_Series s = this.graph.lineSeries[0].GetComponent<WMG_Series>();
		WMG_Series s2 = this.graph.lineSeries[1].GetComponent<WMG_Series>();
		this.graph.legend.hideLegend = true;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.xAxis.hideLabels = true;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.yAxis.hideLabels = true;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.xAxis.hideTicks = true;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.yAxis.hideTicks = true;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.xAxis.hideGrid = true;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.yAxis.hideGrid = true;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.SetActive(this.graph.xAxis.AxisObj, false);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.SetActive(this.graph.yAxis.AxisObj, false);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s.hidePoints = true;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s2.hideLines = true;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s.hideLines = true;
		s2.hidePoints = true;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.legend.hideLegend = false;
		this.graph.xAxis.hideLabels = false;
		this.graph.yAxis.hideLabels = false;
		this.graph.xAxis.hideTicks = false;
		this.graph.yAxis.hideTicks = false;
		this.graph.xAxis.hideGrid = false;
		this.graph.yAxis.hideGrid = false;
		this.graph.SetActive(this.graph.xAxis.AxisObj, true);
		this.graph.SetActive(this.graph.yAxis.AxisObj, true);
		s.hideLines = false;
		s2.hideLines = false;
		s.hidePoints = false;
		s2.hidePoints = false;
		yield break;
	}

	private IEnumerator gridsTicksTests()
	{
		List<string> xLabels = new List<string>(this.graph.xAxis.axisLabels);
		WMG_Anim.animInt(() => this.graph.yAxis.AxisNumTicks, delegate(int x)
		{
			this.graph.yAxis.AxisNumTicks = x;
		}, this.animDuration, 11);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.xAxis.LabelType = WMG_Axis.labelTypes.ticks;
		this.graph.xAxis.SetLabelsUsingMaxMin = true;
		WMG_Anim.animInt(() => this.graph.xAxis.AxisNumTicks, delegate(int x)
		{
			this.graph.xAxis.AxisNumTicks = x;
		}, this.animDuration, 11);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		WMG_Anim.animInt(() => this.graph.yAxis.AxisNumTicks, delegate(int x)
		{
			this.graph.yAxis.AxisNumTicks = x;
		}, this.animDuration, 3);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		WMG_Anim.animInt(() => this.graph.xAxis.AxisNumTicks, delegate(int x)
		{
			this.graph.xAxis.AxisNumTicks = x;
		}, this.animDuration, 5);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.xAxis.LabelType = WMG_Axis.labelTypes.ticks_center;
		this.graph.xAxis.SetLabelsUsingMaxMin = false;
		this.graph.xAxis.axisLabels.SetList(xLabels);
		yield break;
	}

	private IEnumerator sizeTests()
	{
		Vector2 origSize = this.graph.getSpriteSize(this.graph.gameObject);
		WMG_Anim.animSize(this.graph.gameObject, this.animDuration, Ease.Linear, new Vector2(origSize.x * 2f, origSize.y * 2f));
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		WMG_Anim.animSize(this.graph.gameObject, this.animDuration, Ease.Linear, new Vector2(origSize.x * 2f, origSize.y));
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		WMG_Anim.animSize(this.graph.gameObject, this.animDuration, Ease.Linear, new Vector2(origSize.x, origSize.y * 2f));
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		WMG_Anim.animSize(this.graph.gameObject, this.animDuration, Ease.Linear, new Vector2(origSize.x, origSize.y));
		yield break;
	}

	private IEnumerator legendTests()
	{
		this.graph.legend.legendType = WMG_Legend.legendTypes.Right;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.legend.legendType = WMG_Legend.legendTypes.Bottom;
		this.graph.legend.oppositeSideLegend = true;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.legend.legendType = WMG_Legend.legendTypes.Right;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.legend.legendType = WMG_Legend.legendTypes.Bottom;
		this.graph.legend.oppositeSideLegend = false;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.addSeriesWithRandomData();
		this.addSeriesWithRandomData();
		this.addSeriesWithRandomData();
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.legend.legendType = WMG_Legend.legendTypes.Right;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.deleteSeries();
		this.graph.deleteSeries();
		this.graph.deleteSeries();
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.legend.legendType = WMG_Legend.legendTypes.Bottom;
		yield break;
	}

	private IEnumerator dataLabelsTests()
	{
		WMG_Series s = this.graph.lineSeries[0].GetComponent<WMG_Series>();
		WMG_Series s2 = this.graph.lineSeries[1].GetComponent<WMG_Series>();
		s.dataLabelsEnabled = true;
		s2.dataLabelsEnabled = true;
		this.graph.graphType = WMG_Axis_Graph.graphTypes.line;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.bar_side;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.combo;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.line_stacked;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.orientationType = WMG_Axis_Graph.orientationTypes.horizontal;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.combo;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.bar_side;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.line;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.orientationType = WMG_Axis_Graph.orientationTypes.vertical;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		s.dataLabelsEnabled = false;
		s2.dataLabelsEnabled = false;
		yield break;
	}

	private IEnumerator graphTypeAndOrientationTests()
	{
		this.graph.graphType = WMG_Axis_Graph.graphTypes.bar_side;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.combo;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.bar_stacked;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.line_stacked;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.bar_stacked_percent;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.line;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.orientationType = WMG_Axis_Graph.orientationTypes.horizontal;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.bar_side;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.combo;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.bar_stacked;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.line_stacked;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.bar_stacked_percent;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.graphType = WMG_Axis_Graph.graphTypes.line;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.orientationType = WMG_Axis_Graph.orientationTypes.vertical;
		yield break;
	}

	private IEnumerator axesTests()
	{
		this.graph.axesType = WMG_Axis_Graph.axesTypes.I_II;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.axesType = WMG_Axis_Graph.axesTypes.II;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.axesType = WMG_Axis_Graph.axesTypes.II_III;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.axesType = WMG_Axis_Graph.axesTypes.III;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.axesType = WMG_Axis_Graph.axesTypes.III_IV;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.axesType = WMG_Axis_Graph.axesTypes.IV;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.axesType = WMG_Axis_Graph.axesTypes.I_IV;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.axesType = WMG_Axis_Graph.axesTypes.CENTER;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.axesType = WMG_Axis_Graph.axesTypes.I;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.axesType = WMG_Axis_Graph.axesTypes.AUTO_ORIGIN_X;
		this.graph.xAxis.AxisUseNonTickPercent = true;
		WMG_Anim.animVec2(() => this.graph.theOrigin, delegate(Vector2 x)
		{
			this.graph.theOrigin = x;
		}, this.animDuration, new Vector2(this.graph.theOrigin.x, this.graph.yAxis.AxisMaxValue), Ease.Linear);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.axesType = WMG_Axis_Graph.axesTypes.AUTO_ORIGIN_Y;
		this.graph.yAxis.AxisUseNonTickPercent = true;
		WMG_Anim.animVec2(() => this.graph.theOrigin, delegate(Vector2 x)
		{
			this.graph.theOrigin = x;
		}, this.animDuration, new Vector2(this.graph.xAxis.AxisMaxValue, this.graph.theOrigin.y), Ease.Linear);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.axesType = WMG_Axis_Graph.axesTypes.AUTO_ORIGIN;
		WMG_Anim.animVec2(() => this.graph.theOrigin, delegate(Vector2 x)
		{
			this.graph.theOrigin = x;
		}, this.animDuration, new Vector2(this.graph.xAxis.AxisMaxValue / 4f, this.graph.yAxis.AxisMaxValue / 2f), Ease.Linear);
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.axesType = WMG_Axis_Graph.axesTypes.I;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		yield break;
	}

	private IEnumerator addDeleteTests()
	{
		WMG_Series s = this.graph.lineSeries[0].GetComponent<WMG_Series>();
		WMG_Series s2 = this.graph.lineSeries[1].GetComponent<WMG_Series>();
		List<Vector2> s1Data = s.pointValues.list;
		List<Vector2> s2Data = s2.pointValues.list;
		Color s1PointColor = s.pointColor;
		Color s2PointColor = s2.pointColor;
		float barWidth = this.graph.barWidth;
		this.addSeriesWithRandomData();
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.deleteSeries();
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.addSeriesWithRandomData();
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.addSeriesWithRandomData();
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.deleteSeries();
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.deleteSeries();
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.deleteSeries();
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.deleteSeries();
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.addSeriesWithRandomData();
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.graph.deleteSeries();
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.addSeriesWithRandomData();
		this.graph.lineSeries[0].GetComponent<WMG_Series>().pointValues.SetList(s1Data);
		this.graph.lineSeries[0].GetComponent<WMG_Series>().pointColor = s1PointColor;
		if (!this.noTestDelay)
		{
			yield return this.waitTime;
		}
		this.addSeriesWithRandomData();
		this.graph.lineSeries[1].GetComponent<WMG_Series>().pointValues.SetList(s2Data);
		this.graph.lineSeries[1].GetComponent<WMG_Series>().pointColor = s2PointColor;
		this.graph.lineSeries[1].GetComponent<WMG_Series>().pointPrefab = 1;
		this.graph.barWidth = barWidth;
		yield break;
	}

	private void addSeriesWithRandomData()
	{
		WMG_Series wmg_Series = this.graph.addSeries();
		wmg_Series.UseXDistBetweenToSpace = true;
		wmg_Series.AutoUpdateXDistBetween = true;
		wmg_Series.lineScale = 0.5f;
		wmg_Series.pointColor = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);
		wmg_Series.seriesName = "Series " + this.graph.lineSeries.Count;
		wmg_Series.pointValues.SetList(this.graph.GenRandomY(this.graph.groups.Count, 1f, (float)this.graph.groups.Count, this.graph.yAxis.AxisMinValue, this.graph.yAxis.AxisMaxValue));
		wmg_Series.setOriginalPropertyValues();
	}

	public GameObject graphPrefab;

	public WMG_Axis_Graph graph;

	public bool performTests;

	public bool noTestDelay;

	public float testInterval;

	public float testGroupInterval = 2f;

	public Ease easeType;

	public GameObject realTimePrefab;

	private GameObject realTimeObj;

	private float animDuration;

	private WaitForSeconds waitTime;
}
