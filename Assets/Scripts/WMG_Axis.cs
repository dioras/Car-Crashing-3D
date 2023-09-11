using System;
using System.Collections.Generic;
using UnityEngine;

public class WMG_Axis : WMG_GUI_Functions
{
	public float AxisMinValue
	{
		get
		{
			return this._AxisMinValue;
		}
		set
		{
			if (this._AxisMinValue != value)
			{
				this._AxisMinValue = value;
				this.graphC.Changed();
				this.seriesC.Changed();
			}
		}
	}

	public float AxisMaxValue
	{
		get
		{
			return this._AxisMaxValue;
		}
		set
		{
			if (this._AxisMaxValue != value)
			{
				this._AxisMaxValue = value;
				this.graphC.Changed();
				this.seriesC.Changed();
			}
		}
	}

	public int AxisNumTicks
	{
		get
		{
			return this._AxisNumTicks;
		}
		set
		{
			if (this._AxisNumTicks != value)
			{
				this._AxisNumTicks = value;
				this.graphC.Changed();
			}
		}
	}

	public bool MinAutoGrow
	{
		get
		{
			return this._MinAutoGrow;
		}
		set
		{
			if (this._MinAutoGrow != value)
			{
				this._MinAutoGrow = value;
				this.graphC.Changed();
				this.seriesC.Changed();
			}
		}
	}

	public bool MaxAutoGrow
	{
		get
		{
			return this._MaxAutoGrow;
		}
		set
		{
			if (this._MaxAutoGrow != value)
			{
				this._MaxAutoGrow = value;
				this.graphC.Changed();
				this.seriesC.Changed();
			}
		}
	}

	public bool MinAutoShrink
	{
		get
		{
			return this._MinAutoShrink;
		}
		set
		{
			if (this._MinAutoShrink != value)
			{
				this._MinAutoShrink = value;
				this.graphC.Changed();
				this.seriesC.Changed();
			}
		}
	}

	public bool MaxAutoShrink
	{
		get
		{
			return this._MaxAutoShrink;
		}
		set
		{
			if (this._MaxAutoShrink != value)
			{
				this._MaxAutoShrink = value;
				this.graphC.Changed();
				this.seriesC.Changed();
			}
		}
	}

	public float AxisLinePadding
	{
		get
		{
			return this._AxisLinePadding;
		}
		set
		{
			if (this._AxisLinePadding != value)
			{
				this._AxisLinePadding = value;
				this.graphC.Changed();
			}
		}
	}

	public bool AxisUseNonTickPercent
	{
		get
		{
			return this._AxisUseNonTickPercent;
		}
		set
		{
			if (this._AxisUseNonTickPercent != value)
			{
				this._AxisUseNonTickPercent = value;
				this.graphC.Changed();
			}
		}
	}

	public float AxisNonTickPercent
	{
		get
		{
			return this._AxisNonTickPercent;
		}
		set
		{
			if (this._AxisNonTickPercent != value)
			{
				this._AxisNonTickPercent = value;
				this.graphC.Changed();
			}
		}
	}

	public bool AxisArrowTopRight
	{
		get
		{
			return this._AxisArrowTopRight;
		}
		set
		{
			if (this._AxisArrowTopRight != value)
			{
				this._AxisArrowTopRight = value;
				this.graphC.Changed();
			}
		}
	}

	public bool AxisArrowBotLeft
	{
		get
		{
			return this._AxisArrowBotLeft;
		}
		set
		{
			if (this._AxisArrowBotLeft != value)
			{
				this._AxisArrowBotLeft = value;
				this.graphC.Changed();
			}
		}
	}

	public bool AxisTicksRightAbove
	{
		get
		{
			return this._AxisTicksRightAbove;
		}
		set
		{
			if (this._AxisTicksRightAbove != value)
			{
				this._AxisTicksRightAbove = value;
				this.graphC.Changed();
			}
		}
	}

	public int AxisTick
	{
		get
		{
			return this._AxisTick;
		}
		set
		{
			if (this._AxisTick != value)
			{
				this._AxisTick = value;
				this.graphC.Changed();
			}
		}
	}

	public bool hideTick
	{
		get
		{
			return this._hideTick;
		}
		set
		{
			if (this._hideTick != value)
			{
				this._hideTick = value;
				this.graphC.Changed();
			}
		}
	}

	public WMG_Axis.labelTypes LabelType
	{
		get
		{
			return this._LabelType;
		}
		set
		{
			if (this._LabelType != value)
			{
				this._LabelType = value;
				this.graphC.Changed();
			}
		}
	}

	public int AxisLabelSkipInterval
	{
		get
		{
			return this._AxisLabelSkipInterval;
		}
		set
		{
			if (this._AxisLabelSkipInterval != value)
			{
				this._AxisLabelSkipInterval = value;
				this.graphC.Changed();
			}
		}
	}

	public int AxisLabelSkipStart
	{
		get
		{
			return this._AxisLabelSkipStart;
		}
		set
		{
			if (this._AxisLabelSkipStart != value)
			{
				this._AxisLabelSkipStart = value;
				this.graphC.Changed();
			}
		}
	}

	public float AxisLabelRotation
	{
		get
		{
			return this._AxisLabelRotation;
		}
		set
		{
			if (this._AxisLabelRotation != value)
			{
				this._AxisLabelRotation = value;
				this.graphC.Changed();
			}
		}
	}

	public bool SetLabelsUsingMaxMin
	{
		get
		{
			return this._SetLabelsUsingMaxMin;
		}
		set
		{
			if (this._SetLabelsUsingMaxMin != value)
			{
				this._SetLabelsUsingMaxMin = value;
				this.graphC.Changed();
			}
		}
	}

	public int AxisLabelSize
	{
		get
		{
			return this._AxisLabelSize;
		}
		set
		{
			if (this._AxisLabelSize != value)
			{
				this._AxisLabelSize = value;
				this.graphC.Changed();
			}
		}
	}

	public int numDecimalsAxisLabels
	{
		get
		{
			return this._numDecimalsAxisLabels;
		}
		set
		{
			if (this._numDecimalsAxisLabels != value)
			{
				this._numDecimalsAxisLabels = value;
				this.graphC.Changed();
			}
		}
	}

	public bool hideLabels
	{
		get
		{
			return this._hideLabels;
		}
		set
		{
			if (this._hideLabels != value)
			{
				this._hideLabels = value;
				this.graphC.Changed();
			}
		}
	}

	public float AxisLabelSpaceOffset
	{
		get
		{
			return this._AxisLabelSpaceOffset;
		}
		set
		{
			if (this._AxisLabelSpaceOffset != value)
			{
				this._AxisLabelSpaceOffset = value;
				this.graphC.Changed();
			}
		}
	}

	public float autoFitRotation
	{
		get
		{
			return this._autoFitRotation;
		}
		set
		{
			if (this._autoFitRotation != value)
			{
				this._autoFitRotation = value;
				this.graphC.Changed();
			}
		}
	}

	public float autoFitMaxBorder
	{
		get
		{
			return this._autoFitMaxBorder;
		}
		set
		{
			if (this._autoFitMaxBorder != value)
			{
				this._autoFitMaxBorder = value;
				this.graphC.Changed();
			}
		}
	}

	public float AxisLabelSpacing
	{
		get
		{
			return this._AxisLabelSpacing;
		}
		set
		{
			if (this._AxisLabelSpacing != value)
			{
				this._AxisLabelSpacing = value;
				this.graphC.Changed();
			}
		}
	}

	public float AxisLabelDistBetween
	{
		get
		{
			return this._AxisLabelDistBetween;
		}
		set
		{
			if (this._AxisLabelDistBetween != value)
			{
				this._AxisLabelDistBetween = value;
				this.graphC.Changed();
			}
		}
	}

	public bool hideGrid
	{
		get
		{
			return this._hideGrid;
		}
		set
		{
			if (this._hideGrid != value)
			{
				this._hideGrid = value;
				this.graphC.Changed();
			}
		}
	}

	public bool hideTicks
	{
		get
		{
			return this._hideTicks;
		}
		set
		{
			if (this._hideTicks != value)
			{
				this._hideTicks = value;
				this.graphC.Changed();
			}
		}
	}

	public string AxisTitleString
	{
		get
		{
			return this._AxisTitleString;
		}
		set
		{
			if (this._AxisTitleString != value)
			{
				this._AxisTitleString = value;
				this.graphC.Changed();
			}
		}
	}

	public Vector2 AxisTitleOffset
	{
		get
		{
			return this._AxisTitleOffset;
		}
		set
		{
			if (this._AxisTitleOffset != value)
			{
				this._AxisTitleOffset = value;
				this.graphC.Changed();
			}
		}
	}

	public int AxisTitleFontSize
	{
		get
		{
			return this._AxisTitleFontSize;
		}
		set
		{
			if (this._AxisTitleFontSize != value)
			{
				this._AxisTitleFontSize = value;
				this.graphC.Changed();
			}
		}
	}

	public float AxisLength
	{
		get
		{
			if (this.isY)
			{
				return this.graph.yAxisLength;
			}
			return this.graph.xAxisLength;
		}
	}

	public int origAxisLabelSize { get; private set; }

	public float origAxisLabelSpaceOffset { get; private set; }

	public int origAxisTitleFontSize { get; private set; }

	public float origAxisLinePadding { get; private set; }

	public Vector2 origAxisArrowSize { get; private set; }

	public bool isY { get; private set; }

	public void Init(WMG_Axis otherAxis, bool isY)
	{
		if (this.hasInit)
		{
			return;
		}
		this.hasInit = true;
		this.changeObjs.Add(this.graphC);
		this.changeObjs.Add(this.seriesC);
		this.otherAxis = otherAxis;
		this.isY = isY;
		this.axisLabels.SetList(this._axisLabels);
		this.axisLabels.Changed += this.axisLabelsChanged;
		this.graphC.OnChange += this.GraphChanged;
		this.seriesC.OnChange += this.SeriesChanged;
		this.axisLabelLabeler = new WMG_Axis.AxisLabelLabeler(this.defaultAxisLabelLabeler);
		this.setOriginalPropertyValues();
		this.PauseCallbacks();
	}

	public void PauseCallbacks()
	{
		for (int i = 0; i < this.changeObjs.Count; i++)
		{
			this.changeObjs[i].changesPaused = true;
			this.changeObjs[i].changePaused = false;
		}
	}

	public void ResumeCallbacks()
	{
		for (int i = 0; i < this.changeObjs.Count; i++)
		{
			this.changeObjs[i].changesPaused = false;
			if (this.changeObjs[i].changePaused)
			{
				this.changeObjs[i].Changed();
			}
		}
	}

	private void GraphChanged()
	{
		this.graph.graphC.Changed();
	}

	private void SeriesChanged()
	{
		this.graph.seriesNoCountC.Changed();
	}

	private void axisLabelsChanged(bool editorChange, bool countChanged, bool oneValChanged, int index)
	{
		WMG_Util.listChanged<string>(editorChange, ref this.axisLabels, ref this._axisLabels, oneValChanged, index);
		this.graphC.Changed();
	}

	public void setOriginalPropertyValues()
	{
		this.origAxisLabelSize = this.AxisLabelSize;
		this.origAxisTitleFontSize = this.AxisTitleFontSize;
		this.origAxisLabelSpaceOffset = this.AxisLabelSpaceOffset;
		this.origAxisLinePadding = this.AxisLinePadding;
		this.origAxisArrowSize = base.getSpriteSize(this.AxisArrowDL);
	}

	public void setAxisTopRight(bool rightAbove)
	{
		this._AxisArrowTopRight = true;
		this._AxisArrowBotLeft = false;
		this.otherAxis.setOtherHideTick(false);
		this.otherAxis.setOtherAxisTick(0);
		this.otherAxis.setOtherAxisNonTickPercent(0f);
		this._AxisTicksRightAbove = rightAbove;
	}

	public void setAxisBotLeft(bool rightAbove)
	{
		this._AxisArrowTopRight = false;
		this._AxisArrowBotLeft = true;
		this.otherAxis.setOtherHideTick(false);
		this.otherAxis.setOtherAxisTick(this.AxisNumTicks - 1);
		this.otherAxis.setOtherAxisNonTickPercent(1f);
		this._AxisTicksRightAbove = rightAbove;
	}

	public void setAxisMiddle(bool rightAbove)
	{
		this._AxisArrowTopRight = true;
		this._AxisArrowBotLeft = true;
		this.otherAxis.setOtherHideTick(true);
		this.otherAxis.setOtherAxisTick(this.AxisNumTicks / 2);
		this.otherAxis.setOtherAxisNonTickPercent(0.5f);
		this._AxisTicksRightAbove = rightAbove;
	}

	public void setOtherAxisNonTickPercent(float val)
	{
		this._AxisNonTickPercent = val;
	}

	public void setOtherAxisTick(int val)
	{
		this._AxisTick = val;
	}

	public void setOtherHideTick(bool val)
	{
		this._hideTick = val;
	}

	public void setOtherRightAbove(bool val)
	{
		this._AxisTicksRightAbove = val;
	}

	public void possiblyHideTickBasedOnPercent()
	{
		if (this.otherAxis.AxisUseNonTickPercent && this.AxisNumTicks % 2 == 0)
		{
			this._hideTick = false;
		}
	}

	public void ChangeOrientation()
	{
		WMG_Axis.labelTypes labelType = this.LabelType;
		float axisMaxValue = this.AxisMaxValue;
		float axisMinValue = this.AxisMinValue;
		int axisNumTicks = this.AxisNumTicks;
		int numDecimalsAxisLabels = this.numDecimalsAxisLabels;
		bool minAutoGrow = this.MinAutoGrow;
		bool maxAutoGrow = this.MaxAutoGrow;
		bool minAutoShrink = this.MinAutoShrink;
		bool maxAutoShrink = this.MaxAutoShrink;
		bool setLabelsUsingMaxMin = this.SetLabelsUsingMaxMin;
		float axisLabelSpacing = this.AxisLabelSpacing;
		string axisTitleString = this.AxisTitleString;
		bool hideTicks = this.hideTicks;
		List<string> tLabels = new List<string>(this.axisLabels);
		this.LabelType = this.otherAxis.LabelType;
		this.AxisMaxValue = this.otherAxis.AxisMaxValue;
		this.AxisMinValue = this.otherAxis.AxisMinValue;
		this.AxisNumTicks = this.otherAxis.AxisNumTicks;
		this.hideTicks = this.otherAxis.hideTicks;
		this.numDecimalsAxisLabels = this.otherAxis.numDecimalsAxisLabels;
		this.MinAutoGrow = this.otherAxis.MinAutoGrow;
		this.MaxAutoGrow = this.otherAxis.MaxAutoGrow;
		this.MinAutoShrink = this.otherAxis.MinAutoShrink;
		this.MaxAutoShrink = this.otherAxis.MaxAutoShrink;
		this.SetLabelsUsingMaxMin = this.otherAxis.SetLabelsUsingMaxMin;
		this.AxisLabelSpacing = this.otherAxis.AxisLabelSpacing;
		this.AxisTitleString = this.otherAxis.AxisTitleString;
		this.axisLabels.SetList(this.otherAxis.axisLabels);
		this.otherAxis.ChangeOrientationEnd(labelType, axisMaxValue, axisMinValue, axisNumTicks, numDecimalsAxisLabels, minAutoGrow, maxAutoGrow, minAutoShrink, maxAutoShrink, setLabelsUsingMaxMin, axisLabelSpacing, axisTitleString, tLabels, hideTicks);
	}

	public void ChangeOrientationEnd(WMG_Axis.labelTypes tLabelType, float tAxisMaxValue, float tAxisMinValue, int tAxisNumTicks, int tnumDecimalsAxisLabels, bool tMinAutoGrow, bool tMaxAutoGrow, bool tMinAutoShrink, bool tMaxAutoShrink, bool tSetLabelsUsingMaxMin, float tAxisLabelSpacing, string tAxisTitleString, List<string> tLabels, bool tHideTicks)
	{
		this.LabelType = tLabelType;
		this.AxisMaxValue = tAxisMaxValue;
		this.AxisMinValue = tAxisMinValue;
		this.AxisNumTicks = tAxisNumTicks;
		this.hideTicks = tHideTicks;
		this.numDecimalsAxisLabels = tnumDecimalsAxisLabels;
		this.MinAutoGrow = tMinAutoGrow;
		this.MaxAutoGrow = tMaxAutoGrow;
		this.MinAutoShrink = tMinAutoShrink;
		this.MaxAutoShrink = tMaxAutoShrink;
		this.SetLabelsUsingMaxMin = tSetLabelsUsingMaxMin;
		this.AxisLabelSpacing = tAxisLabelSpacing;
		this.AxisTitleString = tAxisTitleString;
		this.axisLabels.SetList(tLabels);
	}

	public void updateAxesRelativeToOrigin(float originVal)
	{
		if (this.graph.axesType == WMG_Axis_Graph.axesTypes.AUTO_ORIGIN || this.graph.axesType == ((!this.isY) ? WMG_Axis_Graph.axesTypes.AUTO_ORIGIN_X : WMG_Axis_Graph.axesTypes.AUTO_ORIGIN_Y))
		{
			bool axisTicksRightAbove = this.otherAxis.AxisTicksRightAbove;
			if (originVal >= this.otherAxis.AxisMaxValue)
			{
				this.otherAxis.setAxisBotLeft(false);
				this._AxisTicksRightAbove = true;
			}
			else if (originVal <= this.otherAxis.AxisMinValue)
			{
				this.otherAxis.setAxisTopRight(false);
				this._AxisTicksRightAbove = false;
			}
			else
			{
				this.otherAxis.setAxisMiddle(false);
				this._AxisTicksRightAbove = false;
				this._AxisTick = Mathf.RoundToInt((originVal - this.otherAxis.AxisMinValue) / (this.otherAxis.AxisMaxValue - this.otherAxis.AxisMinValue) * (float)(this.otherAxis.AxisNumTicks - 1));
				this._AxisNonTickPercent = (originVal - this.otherAxis.AxisMinValue) / (this.otherAxis.AxisMaxValue - this.otherAxis.AxisMinValue);
			}
			this.otherAxis.setOtherRightAbove(axisTicksRightAbove);
		}
	}

	public void UpdateAxesGridsAndTicks()
	{
		if (this.AxisNumTicks <= 1)
		{
			this._AxisNumTicks = 1;
			this.GridLineLength = 0f;
		}
		else
		{
			this.GridLineLength = this.AxisLength / (float)(this.AxisNumTicks - 1);
		}
		if (this.AxisUseNonTickPercent)
		{
			this.AxisPercentagePosition = this.AxisNonTickPercent;
		}
		else if (this.otherAxis.AxisNumTicks == 1)
		{
			this.AxisPercentagePosition = 1f;
		}
		else
		{
			this.AxisPercentagePosition = (float)this.AxisTick / ((float)this.otherAxis.AxisNumTicks - 1f);
		}
		base.SetActive(this.GridLines, !this.hideGrid);
		if (!this.hideGrid)
		{
			WMG_Grid component = this.GridLines.GetComponent<WMG_Grid>();
			if (this.isY)
			{
				component.gridNumNodesY = this.AxisNumTicks;
				component.gridLinkLengthY = this.GridLineLength;
				component.gridLinkLengthX = this.otherAxis.AxisLength;
			}
			else
			{
				component.gridNumNodesX = this.AxisNumTicks;
				component.gridLinkLengthX = this.GridLineLength;
				component.gridLinkLengthY = this.otherAxis.AxisLength;
			}
			component.Refresh();
		}
		base.SetActive(this.AxisTicks, !this.hideTicks);
		if (!this.hideTicks)
		{
			WMG_Grid component2 = this.AxisTicks.GetComponent<WMG_Grid>();
			if (this.isY)
			{
				component2.gridNumNodesY = this.AxisNumTicks;
				component2.gridLinkLengthY = this.GridLineLength;
			}
			else
			{
				component2.gridNumNodesX = this.AxisNumTicks;
				component2.gridLinkLengthX = this.GridLineLength;
			}
			component2.Refresh();
			if (!this.AxisTicksRightAbove)
			{
				if (this.isY)
				{
					base.changeSpritePositionToX(this.AxisTicks, this.AxisPercentagePosition * this.otherAxis.AxisLength - (float)(this.graph.axisWidth / 2) - this.graph.tickSize.y / 2f);
				}
				else
				{
					base.changeSpritePositionToY(this.AxisTicks, this.AxisPercentagePosition * this.otherAxis.AxisLength - (float)(this.graph.axisWidth / 2) - this.graph.tickSize.y / 2f);
				}
			}
			else if (this.isY)
			{
				base.changeSpritePositionToX(this.AxisTicks, this.AxisPercentagePosition * this.otherAxis.AxisLength + (float)(this.graph.axisWidth / 2) + this.graph.tickSize.y / 2f);
			}
			else
			{
				base.changeSpritePositionToY(this.AxisTicks, this.AxisPercentagePosition * this.otherAxis.AxisLength + (float)(this.graph.axisWidth / 2) + this.graph.tickSize.y / 2f);
			}
			foreach (WMG_Node wmg_Node in this.GetAxisTickNodes())
			{
				base.changeSpriteSize(wmg_Node.objectToScale, Mathf.RoundToInt((!this.isY) ? this.graph.tickSize.x : this.graph.tickSize.y), Mathf.RoundToInt((!this.isY) ? this.graph.tickSize.y : this.graph.tickSize.x));
			}
		}
		this.AxisLinePaddingTot = 2f * this.AxisLinePadding;
		float num = 0f;
		if (!this.AxisArrowTopRight)
		{
			this.AxisLinePaddingTot -= this.AxisLinePadding;
		}
		else
		{
			num += this.AxisLinePadding / 2f;
		}
		if (!this.AxisArrowBotLeft)
		{
			this.AxisLinePaddingTot -= this.AxisLinePadding;
		}
		else
		{
			num -= this.AxisLinePadding / 2f;
		}
		if (this.isY)
		{
			base.changeSpriteSize(this.AxisLine, this.graph.axisWidth, Mathf.RoundToInt(this.AxisLength + this.AxisLinePaddingTot));
			base.changeSpritePositionTo(this.AxisLine, new Vector3(0f, num + this.AxisLength / 2f, 0f));
			base.changeSpritePositionToX(this.AxisObj, this.AxisPercentagePosition * this.otherAxis.AxisLength);
		}
		else
		{
			base.changeSpriteSize(this.AxisLine, Mathf.RoundToInt(this.AxisLength + this.AxisLinePaddingTot), this.graph.axisWidth);
			base.changeSpritePositionTo(this.AxisLine, new Vector3(num + this.AxisLength / 2f, 0f, 0f));
			base.changeSpritePositionToY(this.AxisObj, this.AxisPercentagePosition * this.otherAxis.AxisLength);
		}
		base.SetActiveAnchoredSprite(this.AxisArrowUR, this.AxisArrowTopRight);
		base.SetActiveAnchoredSprite(this.AxisArrowDL, this.AxisArrowBotLeft);
	}

	public void UpdateTitle()
	{
		if (this.AxisTitle != null)
		{
			base.changeLabelText(this.AxisTitle, this.AxisTitleString);
			if (this.isY)
			{
				base.changeSpritePositionTo(this.AxisTitle, new Vector3(this.AxisTitleOffset.x, this.AxisLength / 2f + this.AxisTitleOffset.y));
			}
			else
			{
				base.changeSpritePositionTo(this.AxisTitle, new Vector3(this.AxisTitleOffset.x + this.AxisLength / 2f, this.AxisTitleOffset.y));
			}
			base.changeLabelFontSize(this.AxisTitle, this.AxisTitleFontSize);
		}
	}

	public void UpdateAxesMinMaxValues()
	{
		if (!this.MinAutoGrow && !this.MaxAutoGrow && !this.MinAutoShrink && !this.MaxAutoShrink)
		{
			return;
		}
		float num = float.PositiveInfinity;
		float num2 = float.NegativeInfinity;
		for (int i = 0; i < this.graph.lineSeries.Count; i++)
		{
			if (base.activeInHierarchy(this.graph.lineSeries[i]))
			{
				WMG_Series component = this.graph.lineSeries[i].GetComponent<WMG_Series>();
				if (this.graph.orientationType == WMG_Axis_Graph.orientationTypes.vertical)
				{
					for (int j = 0; j < component.pointValues.Count; j++)
					{
						if (this.isY)
						{
							if (component.pointValues[j].y < num)
							{
								num = component.pointValues[j].y;
							}
							if (component.pointValues[j].y > num2)
							{
								num2 = component.pointValues[j].y;
							}
							if ((this.graph.graphType == WMG_Axis_Graph.graphTypes.bar_stacked || this.graph.graphType == WMG_Axis_Graph.graphTypes.line_stacked) && this.graph.TotalPointValues[j] + this.AxisMinValue > num2)
							{
								num2 = this.graph.TotalPointValues[j] + this.AxisMinValue;
							}
						}
						else
						{
							if (component.pointValues[j].x < num)
							{
								num = component.pointValues[j].x;
							}
							if (component.pointValues[j].x > num2)
							{
								num2 = component.pointValues[j].x;
							}
						}
					}
				}
				else
				{
					for (int k = 0; k < component.pointValues.Count; k++)
					{
						if (this.isY)
						{
							if (component.pointValues[k].x < num)
							{
								num = component.pointValues[k].x;
							}
							if (component.pointValues[k].x > num2)
							{
								num2 = component.pointValues[k].x;
							}
						}
						else
						{
							if (component.pointValues[k].y < num)
							{
								num = component.pointValues[k].y;
							}
							if (component.pointValues[k].y > num2)
							{
								num2 = component.pointValues[k].y;
							}
							if ((this.graph.graphType == WMG_Axis_Graph.graphTypes.bar_stacked || this.graph.graphType == WMG_Axis_Graph.graphTypes.line_stacked) && this.graph.TotalPointValues[k] + this.AxisMinValue > num2)
							{
								num2 = this.graph.TotalPointValues[k] + this.AxisMinValue;
							}
						}
					}
				}
			}
		}
		if (this.MinAutoGrow || this.MaxAutoGrow || this.MinAutoShrink || this.MaxAutoShrink)
		{
			if (num == num2 || num == float.PositiveInfinity || num2 == float.NegativeInfinity)
			{
				return;
			}
			float axisMaxValue = this.AxisMaxValue;
			float axisMinValue = this.AxisMinValue;
			if (this.MaxAutoGrow && num2 > axisMaxValue)
			{
				this.AutoSetAxisMinMax(num2, num, true, true, axisMinValue, axisMaxValue);
			}
			if (this.MinAutoGrow && num < axisMinValue)
			{
				this.AutoSetAxisMinMax(num, num2, false, true, axisMinValue, axisMaxValue);
			}
			if (this.MaxAutoShrink && this.graph.autoShrinkAtPercent > (num2 - axisMinValue) / (axisMaxValue - axisMinValue))
			{
				this.AutoSetAxisMinMax(num2, num, true, false, axisMinValue, axisMaxValue);
			}
			if (this.MinAutoShrink && this.graph.autoShrinkAtPercent > (axisMaxValue - num) / (axisMaxValue - axisMinValue))
			{
				this.AutoSetAxisMinMax(num, num2, false, false, axisMinValue, axisMaxValue);
			}
		}
	}

	private void AutoSetAxisMinMax(float val, float val2, bool max, bool grow, float aMin, float aMax)
	{
		int num = this.AxisNumTicks - 1;
		float num2 = 1f + this.graph.autoGrowAndShrinkByPercent;
		float num3;
		if (max)
		{
			if (grow)
			{
				num3 = num2 * (val - aMin) / (float)num;
			}
			else
			{
				num3 = num2 * (val - val2) / (float)num;
			}
		}
		else if (grow)
		{
			num3 = num2 * (aMax - val) / (float)num;
		}
		else
		{
			num3 = num2 * (val2 - val) / (float)num;
		}
		if (num3 == 0f || aMax <= aMin)
		{
			return;
		}
		float num4 = num3;
		int num5 = 0;
		if (Mathf.Abs(num4) > 1f)
		{
			while (Mathf.Abs(num4) > 10f)
			{
				num5++;
				num4 /= 10f;
			}
		}
		else
		{
			while (Mathf.Abs(num4) < 0.1f)
			{
				num5--;
				num4 *= 10f;
			}
		}
		float num6 = Mathf.Pow(10f, (float)(num5 - 1));
		num4 = num3 - num3 % num6 + num6;
		float num7;
		if (max)
		{
			if (grow)
			{
				num7 = (float)num * num4 + aMin;
			}
			else
			{
				num7 = (float)num * num4 + val2;
			}
		}
		else if (grow)
		{
			num7 = aMax - (float)num * num4;
		}
		else
		{
			num7 = val2 - (float)num * num4;
		}
		if (max)
		{
			this.AxisMaxValue = num7;
		}
		else
		{
			this.AxisMinValue = num7;
		}
	}

	public void UpdateAxesLabels()
	{
		int num;
		if (this.LabelType == WMG_Axis.labelTypes.ticks)
		{
			num = this.AxisNumTicks;
		}
		else if (this.LabelType == WMG_Axis.labelTypes.ticks_center)
		{
			num = this.AxisNumTicks - 1;
		}
		else if (this.LabelType == WMG_Axis.labelTypes.groups)
		{
			num = this.graph.groups.Count;
		}
		else
		{
			num = this.axisLabels.Count;
		}
		float distBetween = this.graph.getDistBetween(this.graph.groups.Count, this.AxisLength);
		if (this.LabelType == WMG_Axis.labelTypes.ticks)
		{
			this._AxisLabelDistBetween = this.AxisLength / (float)(num - 1);
		}
		else if (this.LabelType == WMG_Axis.labelTypes.ticks_center)
		{
			this._AxisLabelDistBetween = this.AxisLength / (float)num;
		}
		else if (this.LabelType == WMG_Axis.labelTypes.groups)
		{
			this._AxisLabelDistBetween = distBetween;
		}
		WMG_Grid component = this.AxisLabelObjs.GetComponent<WMG_Grid>();
		if (this.isY)
		{
			component.gridNumNodesY = num;
			component.gridLinkLengthY = this.AxisLabelDistBetween;
		}
		else
		{
			component.gridNumNodesX = num;
			component.gridLinkLengthX = this.AxisLabelDistBetween;
		}
		component.Refresh();
		for (int i = 0; i < num; i++)
		{
			if (this.axisLabels.Count <= i)
			{
				this.axisLabels.AddNoCb(string.Empty, ref this._axisLabels);
			}
		}
		for (int j = this.axisLabels.Count - 1; j >= 0; j--)
		{
			if (j >= num)
			{
				this.axisLabels.RemoveAtNoCb(j, ref this._axisLabels);
			}
		}
		if (this.LabelType == WMG_Axis.labelTypes.ticks)
		{
			this._AxisLabelSpacing = 0f;
		}
		else if (this.LabelType == WMG_Axis.labelTypes.ticks_center)
		{
			if (this.AxisNumTicks == 1)
			{
				this._AxisLabelSpacing = 0f;
			}
			else
			{
				this._AxisLabelSpacing = this.AxisLength / (float)(this.AxisNumTicks - 1) / 2f;
			}
		}
		else if (this.LabelType == WMG_Axis.labelTypes.groups)
		{
			if (this.graph.graphType == WMG_Axis_Graph.graphTypes.line || this.graph.graphType == WMG_Axis_Graph.graphTypes.line_stacked)
			{
				this._AxisLabelSpacing = 0f;
			}
			else
			{
				this._AxisLabelSpacing = distBetween / 2f;
				if (this.graph.graphType == WMG_Axis_Graph.graphTypes.bar_side)
				{
					this._AxisLabelSpacing += (float)this.graph.lineSeries.Count * this.graph.barWidth / 2f;
				}
				else if (this.graph.graphType == WMG_Axis_Graph.graphTypes.bar_stacked)
				{
					this._AxisLabelSpacing += this.graph.barWidth / 2f;
				}
				else if (this.graph.graphType == WMG_Axis_Graph.graphTypes.bar_stacked_percent)
				{
					this._AxisLabelSpacing += this.graph.barWidth / 2f;
				}
				else if (this.graph.graphType == WMG_Axis_Graph.graphTypes.combo)
				{
					this._AxisLabelSpacing += (float)this.graph.NumComboBarSeries() * this.graph.barWidth / 2f;
				}
				if (this.isY)
				{
					this._AxisLabelSpacing += 2f;
				}
			}
		}
		float num2 = 0f;
		if (this.LabelType == WMG_Axis.labelTypes.ticks || (this.LabelType == WMG_Axis.labelTypes.groups && this.AxisNumTicks == this.graph.groups.Count))
		{
			num2 = this.graph.tickSize.y;
		}
		if (this.isY)
		{
			if (!this.AxisTicksRightAbove)
			{
				base.changeSpritePositionToX(this.AxisLabelObjs, this.AxisPercentagePosition * this.otherAxis.AxisLength - num2 - (float)(this.graph.axisWidth / 2));
			}
			else
			{
				base.changeSpritePositionToX(this.AxisLabelObjs, this.AxisPercentagePosition * this.otherAxis.AxisLength + num2 + (float)(this.graph.axisWidth / 2));
			}
		}
		else if (!this.AxisTicksRightAbove)
		{
			base.changeSpritePositionToY(this.AxisLabelObjs, this.AxisPercentagePosition * this.otherAxis.AxisLength - num2 - (float)(this.graph.axisWidth / 2));
		}
		else
		{
			base.changeSpritePositionToY(this.AxisLabelObjs, this.AxisPercentagePosition * this.otherAxis.AxisLength + num2 + (float)(this.graph.axisWidth / 2));
		}
		List<WMG_Node> axisLabelNodes = this.GetAxisLabelNodes();
		if (axisLabelNodes == null)
		{
			return;
		}
		for (int k = 0; k < this.axisLabels.Count; k++)
		{
			if (k >= axisLabelNodes.Count)
			{
				break;
			}
			base.SetActive(axisLabelNodes[k].gameObject, !this.hideLabels);
			if (this.LabelType == WMG_Axis.labelTypes.ticks && this.hideTick && k == this.otherAxis.AxisTick)
			{
				base.SetActive(axisLabelNodes[this.otherAxis.AxisTick].gameObject, false);
			}
			if (!this.graph._autoFitting)
			{
				axisLabelNodes[k].objectToLabel.transform.localEulerAngles = new Vector3(0f, 0f, this.AxisLabelRotation);
			}
			if (!this.isY && !this.graph.autoFitLabels)
			{
				if (this.AxisLabelRotation > 0f)
				{
					if (!this.AxisTicksRightAbove)
					{
						base.changeSpritePivot(axisLabelNodes[k].objectToLabel, WMG_Text_Functions.WMGpivotTypes.TopRight);
					}
					else
					{
						base.changeSpritePivot(axisLabelNodes[k].objectToLabel, WMG_Text_Functions.WMGpivotTypes.BottomLeft);
					}
				}
				else if (!this.AxisTicksRightAbove)
				{
					base.changeSpritePivot(axisLabelNodes[k].objectToLabel, WMG_Text_Functions.WMGpivotTypes.Top);
				}
				else
				{
					base.changeSpritePivot(axisLabelNodes[k].objectToLabel, WMG_Text_Functions.WMGpivotTypes.Bottom);
				}
			}
			if (this.isY)
			{
				if (!this.AxisTicksRightAbove)
				{
					base.changeSpritePivot(axisLabelNodes[k].objectToLabel, WMG_Text_Functions.WMGpivotTypes.Right);
					base.changeSpritePositionTo(axisLabelNodes[k].objectToLabel, new Vector3(-this.AxisLabelSpaceOffset, this.AxisLabelSpacing, 0f));
				}
				else
				{
					base.changeSpritePivot(axisLabelNodes[k].objectToLabel, WMG_Text_Functions.WMGpivotTypes.Left);
					base.changeSpritePositionTo(axisLabelNodes[k].objectToLabel, new Vector3(this.AxisLabelSpaceOffset, this.AxisLabelSpacing, 0f));
				}
			}
			else if (!this.AxisTicksRightAbove)
			{
				base.changeSpritePositionTo(axisLabelNodes[k].objectToLabel, new Vector3(this.AxisLabelSpacing, -this.AxisLabelSpaceOffset, 0f));
			}
			else
			{
				base.changeSpritePositionTo(axisLabelNodes[k].objectToLabel, new Vector3(this.AxisLabelSpacing, this.AxisLabelSpaceOffset, 0f));
			}
			if (!this.graph._autoFitting)
			{
				base.changeLabelFontSize(axisLabelNodes[k].objectToLabel, this.AxisLabelSize);
			}
			this.axisLabels.SetValNoCb(k, this.axisLabelLabeler(this, k), ref this._axisLabels);
			base.changeLabelText(axisLabelNodes[k].objectToLabel, this.axisLabels[k]);
		}
	}

	private string defaultAxisLabelLabeler(WMG_Axis axis, int labelIndex)
	{
		if (axis.LabelType == WMG_Axis.labelTypes.groups)
		{
			return ((labelIndex - axis.AxisLabelSkipStart) % (axis.AxisLabelSkipInterval + 1) != 0) ? string.Empty : ((labelIndex < axis.AxisLabelSkipStart) ? string.Empty : axis.graph.groups[labelIndex]);
		}
		if (!axis.SetLabelsUsingMaxMin)
		{
			return axis.axisLabels[labelIndex];
		}
		float num = axis.AxisMinValue + (float)labelIndex * (axis.AxisMaxValue - axis.AxisMinValue) / (float)(axis.axisLabels.Count - 1);
		if (labelIndex == 0)
		{
			num = axis.AxisMinValue;
		}
		if (axis.graph.graphType == WMG_Axis_Graph.graphTypes.bar_stacked_percent && ((axis.isY && axis.graph.orientationType == WMG_Axis_Graph.orientationTypes.vertical) || (!axis.isY && axis.graph.orientationType == WMG_Axis_Graph.orientationTypes.horizontal)))
		{
			num = (float)labelIndex / ((float)axis.axisLabels.Count - 1f) * 100f;
		}
		float num2 = Mathf.Pow(10f, (float)axis.numDecimalsAxisLabels);
		string text = ((labelIndex - axis.AxisLabelSkipStart) % (axis.AxisLabelSkipInterval + 1) != 0) ? string.Empty : ((labelIndex < axis.AxisLabelSkipStart) ? string.Empty : (Mathf.Round(num * num2) / num2).ToString());
		if (axis.graph.graphType == WMG_Axis_Graph.graphTypes.bar_stacked_percent && ((axis.isY && axis.graph.orientationType == WMG_Axis_Graph.orientationTypes.vertical) || (!axis.isY && axis.graph.orientationType == WMG_Axis_Graph.orientationTypes.horizontal)))
		{
			return (!string.IsNullOrEmpty(text)) ? (text + "%") : string.Empty;
		}
		return text;
	}

	public void AutofitAxesLabels()
	{
		if (this.graph.autoFitLabels && !this.graph._autoFitting)
		{
			this.graph._autoFitting = true;
			List<WMG_Node> axisLabelNodes = this.GetAxisLabelNodes();
			float num = this.graph.autoFitPadding;
			float num2 = this.graph.autoFitPadding;
			float num3 = this.graph.autoFitPadding;
			float num4 = this.graph.autoFitPadding;
			if (!this.graph.legend.hideLegend && this.graph.legend.offset >= 0f)
			{
				if (this.graph.legend.legendType == WMG_Legend.legendTypes.Bottom)
				{
					if (this.graph.legend.oppositeSideLegend)
					{
						num3 += (float)this.graph.legend.LegendHeight + this.graph.legend.offset;
					}
					else
					{
						num4 += (float)this.graph.legend.LegendHeight + this.graph.legend.offset;
					}
				}
				else if (this.graph.legend.oppositeSideLegend)
				{
					num += (float)this.graph.legend.LegendWidth + this.graph.legend.offset;
				}
				else
				{
					num2 += (float)this.graph.legend.LegendWidth + this.graph.legend.offset;
				}
			}
			float autoFitMaxBorder = this.autoFitMaxBorder;
			Vector2 vector = Vector2.zero;
			if (this.isY)
			{
				vector = this.getLabelsMaxDiff(axisLabelNodes, this.AxisTicksRightAbove, this.AxisTicksRightAbove, num, num2, num3, num4);
				if (Mathf.Abs(vector.x) > 1f || Mathf.Abs(vector.y) > 1f)
				{
					if (this.AxisTicksRightAbove)
					{
						this.graph.paddingLeftRight = new Vector2(this.graph.paddingLeftRight.x, this.graph.paddingLeftRight.y - vector.x);
						this.graph.paddingTopBottom = new Vector2(this.graph.paddingTopBottom.x - vector.y, this.graph.paddingTopBottom.y);
					}
					else
					{
						this.graph.paddingLeftRight = new Vector2(this.graph.paddingLeftRight.x - vector.x, this.graph.paddingLeftRight.y);
						this.graph.paddingTopBottom = new Vector2(this.graph.paddingTopBottom.x, this.graph.paddingTopBottom.y - vector.y);
					}
					Vector2 vector2 = (!this.AxisTicksRightAbove) ? new Vector2(autoFitMaxBorder * base.getSpriteWidth(this.graph.gameObject) + num, this.graph.paddingLeftRight.y) : new Vector2(this.graph.paddingLeftRight.x, autoFitMaxBorder * base.getSpriteWidth(this.graph.gameObject) + num2);
					if ((this.AxisTicksRightAbove && this.graph.paddingLeftRight.y > vector2.y) || (!this.AxisTicksRightAbove && this.graph.paddingLeftRight.x > vector2.x))
					{
						if (this.AxisTicksRightAbove)
						{
							this.graph.paddingLeftRight = new Vector2(this.graph.paddingLeftRight.x, vector2.y);
						}
						else
						{
							this.graph.paddingLeftRight = new Vector2(vector2.x, this.graph.paddingLeftRight.y);
						}
					}
					Vector2 vector3 = this.AxisTicksRightAbove ? new Vector2(autoFitMaxBorder * base.getSpriteHeight(this.graph.gameObject) + num3, this.graph.paddingTopBottom.y) : new Vector2(this.graph.paddingTopBottom.x, autoFitMaxBorder * base.getSpriteHeight(this.graph.gameObject) + num4);
					if ((!this.AxisTicksRightAbove && this.graph.paddingTopBottom.y > vector3.y) || (this.AxisTicksRightAbove && this.graph.paddingTopBottom.x > vector3.x))
					{
						if (this.AxisTicksRightAbove)
						{
							this.graph.paddingTopBottom = new Vector2(vector3.x, this.graph.paddingTopBottom.y);
						}
						else
						{
							this.graph.paddingTopBottom = new Vector2(this.graph.paddingTopBottom.x, vector3.y);
						}
					}
					this.graph.UpdateBG();
				}
			}
			else
			{
				bool flag = false;
				bool flag2 = false;
				if (this.otherAxis.AxisTicksRightAbove)
				{
					flag2 = true;
				}
				else
				{
					flag = true;
				}
				bool flag3 = true;
				for (int i = 1; i < axisLabelNodes.Count; i++)
				{
					flag3 = (flag3 && !base.rectIntersectRect(axisLabelNodes[i - 1].objectToLabel, axisLabelNodes[i].objectToLabel));
				}
				if (!flag3)
				{
					this.setLabelRotations(axisLabelNodes, this.autoFitRotation);
				}
				WMG_Text_Functions.WMGpivotTypes theType;
				if (axisLabelNodes.Count > 0 && axisLabelNodes[0].objectToLabel.transform.localEulerAngles.z > 0f)
				{
					if (!this.AxisTicksRightAbove)
					{
						theType = WMG_Text_Functions.WMGpivotTypes.TopRight;
					}
					else
					{
						theType = WMG_Text_Functions.WMGpivotTypes.BottomLeft;
					}
				}
				else if (!this.AxisTicksRightAbove)
				{
					theType = WMG_Text_Functions.WMGpivotTypes.Top;
				}
				else
				{
					theType = WMG_Text_Functions.WMGpivotTypes.Bottom;
				}
				foreach (WMG_Node wmg_Node in axisLabelNodes)
				{
					base.changeSpritePivot(wmg_Node.objectToLabel, theType);
				}
				vector = this.getLabelsMaxDiff(axisLabelNodes, this.AxisTicksRightAbove, this.AxisTicksRightAbove, num, num2, num3, num4);
				if (Mathf.Abs(vector.x) > 1f || Mathf.Abs(vector.y) > 1f)
				{
					if (this.AxisTicksRightAbove)
					{
						if (flag2)
						{
							this.graph.paddingLeftRight = new Vector2(this.graph.paddingLeftRight.x, Mathf.Max(this.graph.paddingLeftRight.y - vector.x, this.graph.paddingLeftRight.y));
							this.graph.paddingTopBottom = new Vector2(Mathf.Max(this.graph.paddingTopBottom.x - vector.y, this.graph.paddingTopBottom.x), this.graph.paddingTopBottom.y);
						}
						else
						{
							this.graph.paddingLeftRight = new Vector2(this.graph.paddingLeftRight.x, this.graph.paddingLeftRight.y - vector.x);
							this.graph.paddingTopBottom = new Vector2(this.graph.paddingTopBottom.x - vector.y, this.graph.paddingTopBottom.y);
						}
					}
					else if (flag)
					{
						this.graph.paddingLeftRight = new Vector2(Mathf.Max(this.graph.paddingLeftRight.x - vector.x, this.graph.paddingLeftRight.x), this.graph.paddingLeftRight.y);
						this.graph.paddingTopBottom = new Vector2(this.graph.paddingTopBottom.x, Mathf.Max(this.graph.paddingTopBottom.y - vector.y, this.graph.paddingTopBottom.y));
					}
					else
					{
						this.graph.paddingLeftRight = new Vector2(this.graph.paddingLeftRight.x - vector.x, this.graph.paddingLeftRight.y);
						this.graph.paddingTopBottom = new Vector2(this.graph.paddingTopBottom.x, this.graph.paddingTopBottom.y - vector.y);
					}
					Vector2 vector4 = (!this.AxisTicksRightAbove) ? new Vector2(autoFitMaxBorder * base.getSpriteWidth(this.graph.gameObject) + num, this.graph.paddingLeftRight.y) : new Vector2(this.graph.paddingLeftRight.x, autoFitMaxBorder * base.getSpriteWidth(this.graph.gameObject) + num2);
					if ((this.AxisTicksRightAbove && this.graph.paddingLeftRight.y > vector4.y) || (!this.AxisTicksRightAbove && this.graph.paddingLeftRight.x > vector4.x))
					{
						if (this.AxisTicksRightAbove)
						{
							this.graph.paddingLeftRight = new Vector2(this.graph.paddingLeftRight.x, vector4.y);
						}
						else
						{
							this.graph.paddingLeftRight = new Vector2(vector4.x, this.graph.paddingLeftRight.y);
						}
					}
					Vector2 vector5 = this.AxisTicksRightAbove ? new Vector2(autoFitMaxBorder * base.getSpriteHeight(this.graph.gameObject) + num3, this.graph.paddingTopBottom.y) : new Vector2(this.graph.paddingTopBottom.x, autoFitMaxBorder * base.getSpriteHeight(this.graph.gameObject) + num4);
					if ((!this.AxisTicksRightAbove && this.graph.paddingTopBottom.y > vector5.y) || (this.AxisTicksRightAbove && this.graph.paddingTopBottom.x > vector5.x))
					{
						if (this.AxisTicksRightAbove)
						{
							this.graph.paddingTopBottom = new Vector2(vector5.x, this.graph.paddingTopBottom.y);
						}
						else
						{
							this.graph.paddingTopBottom = new Vector2(this.graph.paddingTopBottom.x, vector5.y);
						}
					}
					this.graph.UpdateBG();
				}
			}
			this.graph.GraphChanged();
			this.graph._autoFitting = false;
		}
	}

	private Vector2 getLabelsMaxDiff(List<WMG_Node> LabelNodes, bool isRight, bool isTop, float paddingLeft, float paddingRight, float paddingTop, float paddingBot)
	{
		float num = float.PositiveInfinity;
		float num2 = float.PositiveInfinity;
		Vector2 zero = Vector2.zero;
		Vector2 zero2 = Vector2.zero;
		foreach (WMG_Node wmg_Node in LabelNodes)
		{
			base.getRectDiffs(wmg_Node.objectToLabel, this.graph.gameObject, ref zero, ref zero2);
			if (isRight)
			{
				if (zero.y < num)
				{
					num = zero.y;
				}
			}
			else if (zero.x < num)
			{
				num = zero.x;
			}
			if (isTop)
			{
				if (zero2.y < num2)
				{
					num2 = zero2.y;
				}
			}
			else if (zero2.x < num2)
			{
				num2 = zero2.x;
			}
		}
		return new Vector2(num - ((!isRight) ? paddingLeft : paddingRight), num2 - ((!isTop) ? paddingBot : paddingTop));
	}

	private void setLabelRotations(List<WMG_Node> LabelNodes, float rotation)
	{
		foreach (WMG_Node wmg_Node in LabelNodes)
		{
			wmg_Node.objectToLabel.transform.localEulerAngles = new Vector3(0f, 0f, rotation);
		}
	}

	private void setFontSizeLabels(List<WMG_Node> LabelNodes, int newLabelSize)
	{
		foreach (WMG_Node wmg_Node in LabelNodes)
		{
			base.changeLabelFontSize(wmg_Node.objectToLabel, newLabelSize);
		}
	}

	public void setLabelScales(float newScale)
	{
		foreach (WMG_Node wmg_Node in this.GetAxisLabelNodes())
		{
			wmg_Node.objectToLabel.transform.localScale = new Vector3(newScale, newScale, 1f);
		}
	}

	public List<WMG_Node> GetAxisLabelNodes()
	{
		WMG_Grid component = this.AxisLabelObjs.GetComponent<WMG_Grid>();
		if (this.isY)
		{
			return component.getColumn(0);
		}
		return component.getRow(0);
	}

	public List<WMG_Node> GetAxisTickNodes()
	{
		WMG_Grid component = this.AxisTicks.GetComponent<WMG_Grid>();
		if (this.isY)
		{
			return component.getColumn(0);
		}
		return component.getRow(0);
	}

	public WMG_Axis_Graph graph;

	[SerializeField]
	private List<string> _axisLabels;

	public WMG_List<string> axisLabels = new WMG_List<string>();

	public GameObject AxisTitle;

	public GameObject GridLines;

	public GameObject AxisTicks;

	public GameObject AxisLine;

	public GameObject AxisArrowUR;

	public GameObject AxisArrowDL;

	public GameObject AxisObj;

	public GameObject AxisLabelObjs;

	[SerializeField]
	private float _AxisMinValue;

	[SerializeField]
	private float _AxisMaxValue;

	[SerializeField]
	private int _AxisNumTicks;

	[SerializeField]
	private bool _MinAutoGrow;

	[SerializeField]
	private bool _MaxAutoGrow;

	[SerializeField]
	private bool _MinAutoShrink;

	[SerializeField]
	private bool _MaxAutoShrink;

	[SerializeField]
	private float _AxisLinePadding;

	[SerializeField]
	private bool _AxisUseNonTickPercent;

	[SerializeField]
	private float _AxisNonTickPercent;

	[SerializeField]
	private bool _AxisArrowTopRight;

	[SerializeField]
	private bool _AxisArrowBotLeft;

	[SerializeField]
	private bool _AxisTicksRightAbove;

	[SerializeField]
	private int _AxisTick;

	[SerializeField]
	private bool _hideTick;

	[SerializeField]
	private WMG_Axis.labelTypes _LabelType;

	[SerializeField]
	private int _AxisLabelSkipStart;

	[SerializeField]
	private int _AxisLabelSkipInterval;

	[SerializeField]
	private float _AxisLabelRotation;

	[SerializeField]
	private bool _SetLabelsUsingMaxMin;

	[SerializeField]
	private int _AxisLabelSize;

	[SerializeField]
	private int _numDecimalsAxisLabels;

	[SerializeField]
	private bool _hideLabels;

	[SerializeField]
	private float _AxisLabelSpaceOffset;

	[SerializeField]
	private float _autoFitRotation;

	[SerializeField]
	private float _autoFitMaxBorder;

	[SerializeField]
	private float _AxisLabelSpacing;

	[SerializeField]
	private float _AxisLabelDistBetween;

	[SerializeField]
	private bool _hideGrid;

	[SerializeField]
	private bool _hideTicks;

	[SerializeField]
	private string _AxisTitleString;

	[SerializeField]
	private Vector2 _AxisTitleOffset;

	[SerializeField]
	private int _AxisTitleFontSize;

	private float GridLineLength;

	private float AxisLinePaddingTot;

	private float AxisPercentagePosition;

	private bool hasInit;

	private WMG_Axis otherAxis;

	public WMG_Axis.AxisLabelLabeler axisLabelLabeler;

	private List<WMG_Change_Obj> changeObjs = new List<WMG_Change_Obj>();

	private WMG_Change_Obj graphC = new WMG_Change_Obj();

	private WMG_Change_Obj seriesC = new WMG_Change_Obj();

	public enum labelTypes
	{
		ticks,
		ticks_center,
		groups,
		manual
	}

	public delegate string AxisLabelLabeler(WMG_Axis axis, int labelIndex);
}
