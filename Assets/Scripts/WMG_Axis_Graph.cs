using System;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using UnityEngine;

public class WMG_Axis_Graph : WMG_Graph_Manager
{
	public WMG_Axis_Graph.graphTypes graphType
	{
		get
		{
			return this._graphType;
		}
		set
		{
			if (this._graphType != value)
			{
				this._graphType = value;
				this.graphTypeC.Changed();
				this.graphC.Changed();
				this.seriesCountC.Changed();
				this.legend.legendC.Changed();
			}
		}
	}

	public WMG_Axis_Graph.orientationTypes orientationType
	{
		get
		{
			return this._orientationType;
		}
		set
		{
			if (this._orientationType != value)
			{
				this._orientationType = value;
				this.orientationC.Changed();
				this.graphC.Changed();
				this.seriesNoCountC.Changed();
			}
		}
	}

	public WMG_Axis_Graph.axesTypes axesType
	{
		get
		{
			return this._axesType;
		}
		set
		{
			if (this._axesType != value)
			{
				this._axesType = value;
				this.graphC.Changed();
				this.seriesNoCountC.Changed();
			}
		}
	}

	public bool resizeEnabled
	{
		get
		{
			return this._resizeEnabled;
		}
		set
		{
			if (this._resizeEnabled != value)
			{
				this._resizeEnabled = value;
				this.resizeC.Changed();
			}
		}
	}

	public WMG_Axis_Graph.ResizeProperties resizeProperties
	{
		get
		{
			return this._resizeProperties;
		}
		set
		{
			if (this._resizeProperties != value)
			{
				this._resizeProperties = value;
				this.resizeC.Changed();
			}
		}
	}

	public bool useGroups
	{
		get
		{
			return this._useGroups;
		}
		set
		{
			if (this._useGroups != value)
			{
				this._useGroups = value;
				this.graphC.Changed();
			}
		}
	}

	public Vector2 paddingLeftRight
	{
		get
		{
			return this._paddingLeftRight;
		}
		set
		{
			if (this._paddingLeftRight != value)
			{
				this._paddingLeftRight = value;
				this.graphC.Changed();
				this.seriesCountC.Changed();
				this.legend.legendC.Changed();
			}
		}
	}

	public Vector2 paddingTopBottom
	{
		get
		{
			return this._paddingTopBottom;
		}
		set
		{
			if (this._paddingTopBottom != value)
			{
				this._paddingTopBottom = value;
				this.graphC.Changed();
				this.seriesCountC.Changed();
				this.legend.legendC.Changed();
			}
		}
	}

	public Vector2 theOrigin
	{
		get
		{
			return this._theOrigin;
		}
		set
		{
			if (this._theOrigin != value)
			{
				this._theOrigin = value;
				this.graphC.Changed();
				this.seriesNoCountC.Changed();
			}
		}
	}

	public float barWidth
	{
		get
		{
			return this._barWidth;
		}
		set
		{
			if (this._barWidth != value)
			{
				this._barWidth = value;
				this.graphC.Changed();
				this.seriesNoCountC.Changed();
			}
		}
	}

	public float barAxisValue
	{
		get
		{
			return this._barAxisValue;
		}
		set
		{
			if (this._barAxisValue != value)
			{
				this._barAxisValue = value;
				this.graphC.Changed();
				this.seriesNoCountC.Changed();
			}
		}
	}

	public bool autoUpdateOrigin
	{
		get
		{
			return this._autoUpdateOrigin;
		}
		set
		{
			if (this._autoUpdateOrigin != value)
			{
				this._autoUpdateOrigin = value;
				this.graphC.Changed();
			}
		}
	}

	public bool autoUpdateBarWidth
	{
		get
		{
			return this._autoUpdateBarWidth;
		}
		set
		{
			if (this._autoUpdateBarWidth != value)
			{
				this._autoUpdateBarWidth = value;
				this.graphC.Changed();
				this.seriesNoCountC.Changed();
			}
		}
	}

	public float autoUpdateBarWidthSpacing
	{
		get
		{
			return this._autoUpdateBarWidthSpacing;
		}
		set
		{
			if (this._autoUpdateBarWidthSpacing != value)
			{
				this._autoUpdateBarWidthSpacing = value;
				this.graphC.Changed();
				this.seriesNoCountC.Changed();
			}
		}
	}

	public bool autoUpdateSeriesAxisSpacing
	{
		get
		{
			return this._autoUpdateSeriesAxisSpacing;
		}
		set
		{
			if (this._autoUpdateSeriesAxisSpacing != value)
			{
				this._autoUpdateSeriesAxisSpacing = value;
				this.graphC.Changed();
				this.seriesNoCountC.Changed();
			}
		}
	}

	public bool autoUpdateBarAxisValue
	{
		get
		{
			return this._autoUpdateBarAxisValue;
		}
		set
		{
			if (this._autoUpdateBarAxisValue != value)
			{
				this._autoUpdateBarAxisValue = value;
				this.graphC.Changed();
				this.seriesNoCountC.Changed();
			}
		}
	}

	public int axisWidth
	{
		get
		{
			return this._axisWidth;
		}
		set
		{
			if (this._axisWidth != value)
			{
				this._axisWidth = value;
				this.graphC.Changed();
			}
		}
	}

	public float autoShrinkAtPercent
	{
		get
		{
			return this._autoShrinkAtPercent;
		}
		set
		{
			if (this._autoShrinkAtPercent != value)
			{
				this._autoShrinkAtPercent = value;
				this.graphC.Changed();
			}
		}
	}

	public float autoGrowAndShrinkByPercent
	{
		get
		{
			return this._autoGrowAndShrinkByPercent;
		}
		set
		{
			if (this._autoGrowAndShrinkByPercent != value)
			{
				this._autoGrowAndShrinkByPercent = value;
				this.graphC.Changed();
			}
		}
	}

	public bool tooltipEnabled
	{
		get
		{
			return this._tooltipEnabled;
		}
		set
		{
			if (this._tooltipEnabled != value)
			{
				this._tooltipEnabled = value;
				this.tooltipEnabledC.Changed();
			}
		}
	}

	public bool autoAnimationsEnabled
	{
		get
		{
			return this._autoAnimationsEnabled;
		}
		set
		{
			if (this._autoAnimationsEnabled != value)
			{
				this._autoAnimationsEnabled = value;
				this.autoAnimEnabledC.Changed();
			}
		}
	}

	public bool autoFitLabels
	{
		get
		{
			return this._autoFitLabels;
		}
		set
		{
			if (this._autoFitLabels != value)
			{
				this._autoFitLabels = value;
				this.graphC.Changed();
			}
		}
	}

	public float autoFitPadding
	{
		get
		{
			return this._autoFitPadding;
		}
		set
		{
			if (this._autoFitPadding != value)
			{
				this._autoFitPadding = value;
				this.graphC.Changed();
			}
		}
	}

	public Vector2 tickSize
	{
		get
		{
			return this._tickSize;
		}
		set
		{
			if (this._tickSize != value)
			{
				this._tickSize = value;
				this.graphC.Changed();
			}
		}
	}

	public string graphTitleString
	{
		get
		{
			return this._graphTitleString;
		}
		set
		{
			if (this._graphTitleString != value)
			{
				this._graphTitleString = value;
				this.graphC.Changed();
			}
		}
	}

	public Vector2 graphTitleOffset
	{
		get
		{
			return this._graphTitleOffset;
		}
		set
		{
			if (this._graphTitleOffset != value)
			{
				this._graphTitleOffset = value;
				this.graphC.Changed();
			}
		}
	}

	public float xAxisLength
	{
		get
		{
			return base.getSpriteWidth(base.gameObject) - this.paddingLeftRight.x - this.paddingLeftRight.y;
		}
	}

	public float yAxisLength
	{
		get
		{
			return base.getSpriteHeight(base.gameObject) - this.paddingTopBottom.x - this.paddingTopBottom.y;
		}
	}

	public bool IsStacked
	{
		get
		{
			return this.graphType == WMG_Axis_Graph.graphTypes.bar_stacked || this.graphType == WMG_Axis_Graph.graphTypes.bar_stacked_percent || this.graphType == WMG_Axis_Graph.graphTypes.line_stacked;
		}
	}

	public int NumComboBarSeries()
	{
		return this.numComboBarSeries;
	}

	public bool _autoFitting { get; set; }

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event WMG_Axis_Graph.GraphBackgroundChangedHandler GraphBackgroundChanged;

	protected virtual void OnGraphBackgroundChanged()
	{
		WMG_Axis_Graph.GraphBackgroundChangedHandler graphBackgroundChanged = this.GraphBackgroundChanged;
		if (graphBackgroundChanged != null)
		{
			graphBackgroundChanged(this);
		}
	}

	[ContextMenu("start")]
	public void Start()
	{
		this.Init();
		this.PauseCallbacks();
		this.AllChanged();
	}

	public void Init()
	{
		if (this.hasInit)
		{
			return;
		}
		this.hasInit = true;
		this.changeObjs.Add(this.orientationC);
		this.changeObjs.Add(this.graphTypeC);
		this.changeObjs.Add(this.graphC);
		this.changeObjs.Add(this.resizeC);
		this.changeObjs.Add(this.seriesCountC);
		this.changeObjs.Add(this.seriesNoCountC);
		this.changeObjs.Add(this.tooltipEnabledC);
		this.changeObjs.Add(this.autoAnimEnabledC);
		this.legend.Init();
		this.xAxis.Init(this.yAxis, false);
		this.yAxis.Init(this.xAxis, true);
		for (int i = 0; i < this.lineSeries.Count; i++)
		{
			WMG_Series component = this.lineSeries[i].GetComponent<WMG_Series>();
			component.Init(i);
		}
		this.theTooltip = base.gameObject.AddComponent<WMG_Graph_Tooltip>();
		this.theTooltip.hideFlags = HideFlags.HideInInspector;
		this.theTooltip.theGraph = this;
		if (this.tooltipEnabled)
		{
			this.theTooltip.subscribeToEvents(true);
		}
		this.autoAnim = base.gameObject.AddComponent<WMG_Graph_Auto_Anim>();
		this.autoAnim.hideFlags = HideFlags.HideInInspector;
		this.autoAnim.theGraph = this;
		if (this.autoAnimationsEnabled)
		{
			this.autoAnim.subscribeToEvents(true);
		}
		this.groups.SetList(this._groups);
		this.groups.Changed += this.groupsChanged;
		this.graphTypeC.OnChange += this.GraphTypeChanged;
		this.tooltipEnabledC.OnChange += this.TooltipEnabledChanged;
		this.autoAnimEnabledC.OnChange += this.AutoAnimationsEnabledChanged;
		this.orientationC.OnChange += this.OrientationChanged;
		this.resizeC.OnChange += this.ResizeChanged;
		this.graphC.OnChange += this.GraphChanged;
		this.seriesCountC.OnChange += this.SeriesCountChanged;
		this.seriesNoCountC.OnChange += this.SeriesNoCountChanged;
		this.setOriginalPropertyValues();
		this.PauseCallbacks();
	}

	private void Update()
	{
		this.updateFromDataSource();
		this.updateFromResize();
		this.Refresh();
	}

	public void Refresh()
	{
		this.ResumeCallbacks();
		this.PauseCallbacks();
	}

	public void ManualResize()
	{
		this.PauseCallbacks();
		this.resizeEnabled = true;
		this.UpdateFromContainer();
		this.resizeEnabled = false;
		this.ResumeCallbacks();
	}

	private void PauseCallbacks()
	{
		this.yAxis.PauseCallbacks();
		this.xAxis.PauseCallbacks();
		for (int i = 0; i < this.changeObjs.Count; i++)
		{
			this.changeObjs[i].changesPaused = true;
			this.changeObjs[i].changePaused = false;
		}
		for (int j = 0; j < this.lineSeries.Count; j++)
		{
			WMG_Series component = this.lineSeries[j].GetComponent<WMG_Series>();
			component.PauseCallbacks();
		}
		this.legend.PauseCallbacks();
	}

	private void ResumeCallbacks()
	{
		this.yAxis.ResumeCallbacks();
		this.xAxis.ResumeCallbacks();
		for (int i = 0; i < this.changeObjs.Count; i++)
		{
			this.changeObjs[i].changesPaused = false;
			if (this.changeObjs[i].changePaused)
			{
				this.changeObjs[i].Changed();
			}
		}
		for (int j = 0; j < this.lineSeries.Count; j++)
		{
			WMG_Series component = this.lineSeries[j].GetComponent<WMG_Series>();
			component.ResumeCallbacks();
		}
		this.legend.ResumeCallbacks();
	}

	private void updateFromResize()
	{
		bool flag = false;
		base.updateCacheAndFlag<float>(ref this.cachedContainerWidth, base.getSpriteWidth(base.gameObject), ref flag);
		base.updateCacheAndFlag<float>(ref this.cachedContainerHeight, base.getSpriteHeight(base.gameObject), ref flag);
		if (flag)
		{
			this.resizeC.Changed();
			this.graphC.Changed();
			this.seriesNoCountC.Changed();
			this.legend.legendC.Changed();
		}
	}

	private void updateFromDataSource()
	{
		for (int i = 0; i < this.lineSeries.Count; i++)
		{
			WMG_Series component = this.lineSeries[i].GetComponent<WMG_Series>();
			component.UpdateFromDataSource();
			component.RealTimeUpdate();
		}
	}

	private void OrientationChanged()
	{
		this.UpdateOrientation();
	}

	private void TooltipEnabledChanged()
	{
		this.UpdateTooltip();
	}

	private void AutoAnimationsEnabledChanged()
	{
		this.UpdateAutoAnimEvents();
	}

	private void ResizeChanged()
	{
		this.UpdateFromContainer();
	}

	private void AllChanged()
	{
		this.graphC.Changed();
		this.seriesCountC.Changed();
		this.legend.legendC.Changed();
	}

	private void GraphTypeChanged()
	{
		for (int i = 0; i < this.lineSeries.Count; i++)
		{
			WMG_Series component = this.lineSeries[i].GetComponent<WMG_Series>();
			component.prefabC.Changed();
		}
	}

	public void SeriesChanged(bool countChanged, bool instant)
	{
		for (int i = 0; i < this.lineSeries.Count; i++)
		{
			WMG_Series component = this.lineSeries[i].GetComponent<WMG_Series>();
			if (countChanged)
			{
				if (instant)
				{
					component.pointValuesCountChanged();
				}
				else
				{
					component.pointValuesCountC.Changed();
				}
			}
			else if (instant)
			{
				component.pointValuesChanged();
			}
			else
			{
				component.pointValuesC.Changed();
			}
		}
	}

	private void SeriesCountChanged()
	{
		this.SeriesChanged(true, false);
	}

	private void SeriesNoCountChanged()
	{
		this.SeriesChanged(false, false);
	}

	public void aSeriesPointsChanged()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		this.UpdateTotals();
		this.UpdateBarWidth();
		this.UpdateAxesMinMaxValues();
	}

	public void GraphChanged()
	{
		this.UpdateTotals();
		this.UpdateBarWidth();
		this.UpdateAxesMinMaxValues();
		this.UpdateAxesType();
		this.UpdateAxesGridsAndTicks();
		this.UpdateAxesLabels();
		this.UpdateSeriesParentPositions();
		this.UpdateBG();
		this.UpdateTitles();
	}

	private void groupsChanged(bool editorChange, bool countChanged, bool oneValChanged, int index)
	{
		WMG_Util.listChanged<string>(editorChange, ref this.groups, ref this._groups, oneValChanged, index);
		this.graphC.Changed();
		if (oneValChanged)
		{
			this.seriesNoCountC.Changed();
		}
		else
		{
			this.seriesCountC.Changed();
		}
	}

	public void setOriginalPropertyValues()
	{
		this.cachedContainerWidth = base.getSpriteWidth(base.gameObject);
		this.cachedContainerHeight = base.getSpriteHeight(base.gameObject);
		this.origWidth = base.getSpriteWidth(base.gameObject);
		this.origHeight = base.getSpriteHeight(base.gameObject);
		this.origBarWidth = this.barWidth;
		this.origAxisWidth = (float)this.axisWidth;
		this.origAutoFitPadding = this.autoFitPadding;
		this.origTickSize = this.tickSize;
		this.origPaddingLeftRight = this.paddingLeftRight;
		this.origPaddingTopBottom = this.paddingTopBottom;
	}

	private void UpdateOrientation()
	{
		this.yAxis.ChangeOrientation();
		for (int i = 0; i < this.lineSeries.Count; i++)
		{
			WMG_Series component = this.lineSeries[i].GetComponent<WMG_Series>();
			component.origDataLabelOffset = new Vector2(component.origDataLabelOffset.y, component.origDataLabelOffset.x);
			component.dataLabelsOffset = new Vector2(component.dataLabelsOffset.y, component.dataLabelsOffset.x);
			component.setAnimatingFromPreviousData();
		}
	}

	private void UpdateAxesType()
	{
		if (this.axesType != WMG_Axis_Graph.axesTypes.MANUAL)
		{
			if (this.axesType == WMG_Axis_Graph.axesTypes.AUTO_ORIGIN || this.axesType == WMG_Axis_Graph.axesTypes.AUTO_ORIGIN_X || this.axesType == WMG_Axis_Graph.axesTypes.AUTO_ORIGIN_Y)
			{
				this.updateAxesRelativeToOrigin();
			}
			else
			{
				this.updateOriginRelativeToAxes();
				if (this.axesType == WMG_Axis_Graph.axesTypes.I || this.axesType == WMG_Axis_Graph.axesTypes.II || this.axesType == WMG_Axis_Graph.axesTypes.III || this.axesType == WMG_Axis_Graph.axesTypes.IV)
				{
					if (this.axesType == WMG_Axis_Graph.axesTypes.I)
					{
						this.setAxesQuadrant1();
					}
					else if (this.axesType == WMG_Axis_Graph.axesTypes.II)
					{
						this.setAxesQuadrant2();
					}
					else if (this.axesType == WMG_Axis_Graph.axesTypes.III)
					{
						this.setAxesQuadrant3();
					}
					else if (this.axesType == WMG_Axis_Graph.axesTypes.IV)
					{
						this.setAxesQuadrant4();
					}
				}
				else
				{
					if (this.axesType == WMG_Axis_Graph.axesTypes.CENTER)
					{
						this.setAxesQuadrant1_2_3_4();
					}
					else if (this.axesType == WMG_Axis_Graph.axesTypes.I_II)
					{
						this.setAxesQuadrant1_2();
					}
					else if (this.axesType == WMG_Axis_Graph.axesTypes.III_IV)
					{
						this.setAxesQuadrant3_4();
					}
					else if (this.axesType == WMG_Axis_Graph.axesTypes.II_III)
					{
						this.setAxesQuadrant2_3();
					}
					else if (this.axesType == WMG_Axis_Graph.axesTypes.I_IV)
					{
						this.setAxesQuadrant1_4();
					}
					this.yAxis.possiblyHideTickBasedOnPercent();
					this.xAxis.possiblyHideTickBasedOnPercent();
				}
			}
		}
	}

	private void updateOriginRelativeToAxes()
	{
		if (this.autoUpdateOrigin)
		{
			if (this.axesType == WMG_Axis_Graph.axesTypes.I)
			{
				this._theOrigin = new Vector2(this.xAxis.AxisMinValue, this.yAxis.AxisMinValue);
			}
			else if (this.axesType == WMG_Axis_Graph.axesTypes.II)
			{
				this._theOrigin = new Vector2(this.xAxis.AxisMaxValue, this.yAxis.AxisMinValue);
			}
			else if (this.axesType == WMG_Axis_Graph.axesTypes.III)
			{
				this._theOrigin = new Vector2(this.xAxis.AxisMaxValue, this.yAxis.AxisMaxValue);
			}
			else if (this.axesType == WMG_Axis_Graph.axesTypes.IV)
			{
				this._theOrigin = new Vector2(this.xAxis.AxisMinValue, this.yAxis.AxisMaxValue);
			}
			else if (this.axesType == WMG_Axis_Graph.axesTypes.CENTER)
			{
				this._theOrigin = new Vector2((this.xAxis.AxisMaxValue + this.xAxis.AxisMinValue) / 2f, (this.yAxis.AxisMaxValue + this.yAxis.AxisMinValue) / 2f);
			}
			else if (this.axesType == WMG_Axis_Graph.axesTypes.I_II)
			{
				this._theOrigin = new Vector2((this.xAxis.AxisMaxValue + this.xAxis.AxisMinValue) / 2f, this.yAxis.AxisMinValue);
			}
			else if (this.axesType == WMG_Axis_Graph.axesTypes.III_IV)
			{
				this._theOrigin = new Vector2((this.xAxis.AxisMaxValue + this.xAxis.AxisMinValue) / 2f, this.yAxis.AxisMaxValue);
			}
			else if (this.axesType == WMG_Axis_Graph.axesTypes.II_III)
			{
				this._theOrigin = new Vector2(this.xAxis.AxisMaxValue, (this.yAxis.AxisMaxValue + this.yAxis.AxisMinValue) / 2f);
			}
			else if (this.axesType == WMG_Axis_Graph.axesTypes.I_IV)
			{
				this._theOrigin = new Vector2(this.xAxis.AxisMinValue, (this.yAxis.AxisMaxValue + this.yAxis.AxisMinValue) / 2f);
			}
		}
		if (this.autoUpdateBarAxisValue)
		{
			if (this.orientationType == WMG_Axis_Graph.orientationTypes.vertical)
			{
				this._barAxisValue = this.theOrigin.y;
			}
			else
			{
				this._barAxisValue = this.theOrigin.x;
			}
		}
	}

	private void updateAxesRelativeToOrigin()
	{
		this.yAxis.updateAxesRelativeToOrigin(this.theOrigin.x);
		this.xAxis.updateAxesRelativeToOrigin(this.theOrigin.y);
		if (this.autoUpdateBarAxisValue)
		{
			if (this.orientationType == WMG_Axis_Graph.orientationTypes.vertical)
			{
				this._barAxisValue = this.theOrigin.y;
			}
			else
			{
				this._barAxisValue = this.theOrigin.x;
			}
		}
	}

	private void UpdateAxesMinMaxValues()
	{
		this.yAxis.UpdateAxesMinMaxValues();
		this.xAxis.UpdateAxesMinMaxValues();
	}

	private void UpdateAxesGridsAndTicks()
	{
		this.yAxis.UpdateAxesGridsAndTicks();
		this.xAxis.UpdateAxesGridsAndTicks();
	}

	private void UpdateAxesLabels()
	{
		this.yAxis.UpdateAxesLabels();
		this.xAxis.UpdateAxesLabels();
		this.yAxis.AutofitAxesLabels();
		this.xAxis.AutofitAxesLabels();
	}

	private void UpdateSeriesParentPositions()
	{
		int num = -1;
		bool flag = false;
		if (this.graphType == WMG_Axis_Graph.graphTypes.combo)
		{
			for (int i = 0; i < this.lineSeries.Count; i++)
			{
				WMG_Series component = this.lineSeries[i].GetComponent<WMG_Series>();
				if (component.comboType == WMG_Series.comboTypes.bar)
				{
					flag = true;
					break;
				}
			}
		}
		for (int j = 0; j < this.lineSeries.Count; j++)
		{
			WMG_Series component2 = this.lineSeries[j].GetComponent<WMG_Series>();
			Vector2 axesOffsetFactor = this.getAxesOffsetFactor();
			axesOffsetFactor = new Vector2((float)(-(float)this.axisWidth / 2) * axesOffsetFactor.x, (float)(-(float)this.axisWidth / 2) * axesOffsetFactor.y);
			if (component2.seriesIsLine)
			{
				base.changeSpritePositionTo(this.lineSeries[j], new Vector3(0f, 0f, 0f));
			}
			else if (this.orientationType == WMG_Axis_Graph.orientationTypes.vertical)
			{
				base.changeSpritePositionTo(this.lineSeries[j], new Vector3(axesOffsetFactor.x, axesOffsetFactor.y, 0f));
			}
			else
			{
				base.changeSpritePositionTo(this.lineSeries[j], new Vector3(axesOffsetFactor.x, axesOffsetFactor.y + this.barWidth, 0f));
			}
			if (this.graphType == WMG_Axis_Graph.graphTypes.bar_side)
			{
				if (j > 0)
				{
					if (this.orientationType == WMG_Axis_Graph.orientationTypes.vertical)
					{
						base.changeSpritePositionRelativeToObjByX(this.lineSeries[j], this.lineSeries[j - 1], this.barWidth);
					}
					else
					{
						base.changeSpritePositionRelativeToObjByY(this.lineSeries[j], this.lineSeries[j - 1], this.barWidth);
					}
				}
			}
			else if (this.graphType == WMG_Axis_Graph.graphTypes.combo)
			{
				if (j > 0)
				{
					if (this.lineSeries[j - 1].GetComponent<WMG_Series>().comboType == WMG_Series.comboTypes.bar)
					{
						num = j - 1;
					}
					if (num > -1 && this.lineSeries[j].GetComponent<WMG_Series>().comboType == WMG_Series.comboTypes.bar)
					{
						if (this.orientationType == WMG_Axis_Graph.orientationTypes.vertical)
						{
							base.changeSpritePositionRelativeToObjByX(this.lineSeries[j], this.lineSeries[num], this.barWidth);
						}
						else
						{
							base.changeSpritePositionRelativeToObjByY(this.lineSeries[j], this.lineSeries[num], this.barWidth);
						}
					}
					if (flag && this.lineSeries[j].GetComponent<WMG_Series>().comboType == WMG_Series.comboTypes.line)
					{
						base.changeSpritePositionRelativeToObjByX(this.lineSeries[j], this.lineSeries[0], this.barWidth / 2f);
					}
				}
			}
			else if (j > 0)
			{
				if (this.orientationType == WMG_Axis_Graph.orientationTypes.vertical)
				{
					base.changeSpritePositionRelativeToObjByX(this.lineSeries[j], this.lineSeries[0], 0f);
				}
				else
				{
					base.changeSpritePositionRelativeToObjByY(this.lineSeries[j], this.lineSeries[0], 0f);
				}
			}
		}
	}

	public void UpdateBG()
	{
		base.changeSpriteSize(this.graphBackground, Mathf.RoundToInt(base.getSpriteWidth(base.gameObject)), Mathf.RoundToInt(base.getSpriteHeight(base.gameObject)));
		base.changeSpritePositionTo(this.graphBackground, new Vector3(-this.paddingLeftRight.x, -this.paddingTopBottom.y, 0f));
		base.changeSpriteSize(this.anchoredParent, Mathf.RoundToInt(base.getSpriteWidth(base.gameObject)), Mathf.RoundToInt(base.getSpriteHeight(base.gameObject)));
		base.changeSpritePositionTo(this.anchoredParent, new Vector3(-this.paddingLeftRight.x, -this.paddingTopBottom.y, 0f));
		this.UpdateBGandSeriesParentPositions(this.cachedContainerWidth, this.cachedContainerHeight);
		this.OnGraphBackgroundChanged();
	}

	public void UpdateBGandSeriesParentPositions(float x, float y)
	{
		Vector2 spritePivot = base.getSpritePivot(base.gameObject);
		Vector3 newPos = new Vector3(-x * spritePivot.x + this.paddingLeftRight.x, -y * spritePivot.y + this.paddingTopBottom.y);
		base.changeSpritePositionTo(this.graphBackground.transform.parent.gameObject, newPos);
		base.changeSpritePositionTo(this.seriesParent, newPos);
	}

	private void UpdateTotals()
	{
		int num = 0;
		int num2 = 0;
		this.numComboBarSeries = 0;
		for (int i = 0; i < this.lineSeries.Count; i++)
		{
			WMG_Series component = this.lineSeries[i].GetComponent<WMG_Series>();
			if (num < component.pointValues.Count)
			{
				num = component.pointValues.Count;
			}
			if (this.graphType == WMG_Axis_Graph.graphTypes.combo && component.comboType == WMG_Series.comboTypes.bar)
			{
				this.numComboBarSeries++;
				if (num2 < component.pointValues.Count)
				{
					num2 = component.pointValues.Count;
				}
			}
		}
		this.maxSeriesPointCount = num;
		this.maxSeriesBarCount = num2;
		for (int j = 0; j < num; j++)
		{
			if (this.totalPointValues.Count <= j)
			{
				this.totalPointValues.Add(0f);
			}
			this.totalPointValues[j] = 0f;
			for (int k = 0; k < this.lineSeries.Count; k++)
			{
				WMG_Series component2 = this.lineSeries[k].GetComponent<WMG_Series>();
				if (component2.pointValues.Count > j)
				{
					if (this.orientationType == WMG_Axis_Graph.orientationTypes.vertical)
					{
						List<float> list = this.totalPointValues;
						int index= j;
						(list)[index ] = list[index] + (component2.pointValues[j].y - this.yAxis.AxisMinValue);
					}
					else
					{
						List<float> list= this.totalPointValues;
						int index2= j;
						(list )[index2 ] = list[index2] + (component2.pointValues[j].y - this.xAxis.AxisMinValue);
					}
				}
			}
		}
	}

	private void UpdateBarWidth()
	{
		if (this.autoUpdateBarWidth)
		{
			if (this.graphType == WMG_Axis_Graph.graphTypes.line || this.graphType == WMG_Axis_Graph.graphTypes.line_stacked)
			{
				return;
			}
			float num = this.xAxisLength;
			if (this.orientationType == WMG_Axis_Graph.orientationTypes.horizontal)
			{
				num = this.yAxisLength;
			}
			int num2 = this.maxSeriesPointCount * this.lineSeries.Count + 1;
			if (this.graphType == WMG_Axis_Graph.graphTypes.combo)
			{
				num2 = this.maxSeriesBarCount * this.numComboBarSeries + 1;
			}
			if (this.graphType == WMG_Axis_Graph.graphTypes.bar_stacked || this.graphType == WMG_Axis_Graph.graphTypes.bar_stacked_percent)
			{
				num2 = this.maxSeriesPointCount;
			}
			this.autoUpdateBarWidthSpacing = Mathf.Clamp01(this.autoUpdateBarWidthSpacing);
			this.barWidth = (1f - this.autoUpdateBarWidthSpacing) * (num - (float)this.maxSeriesPointCount) / (float)num2;
		}
		for (int i = 0; i < this.lineSeries.Count; i++)
		{
			WMG_Series component = this.lineSeries[i].GetComponent<WMG_Series>();
			component.updateXdistBetween();
			component.updateExtraXSpace();
		}
		this.UpdateSeriesParentPositions();
	}

	private void UpdateTitles()
	{
		if (this.graphTitle != null)
		{
			base.changeLabelText(this.graphTitle, this.graphTitleString);
			base.changeSpritePositionTo(this.graphTitle, new Vector3(this.xAxisLength / 2f + this.graphTitleOffset.x, this.yAxisLength + this.graphTitleOffset.y));
		}
		this.yAxis.UpdateTitle();
		this.xAxis.UpdateTitle();
	}

	private void UpdateTooltip()
	{
		this.theTooltip.subscribeToEvents(this.tooltipEnabled);
	}

	private void UpdateAutoAnimEvents()
	{
		this.autoAnim.subscribeToEvents(this.autoAnimationsEnabled);
	}

	public List<float> TotalPointValues
	{
		get
		{
			return this.totalPointValues;
		}
	}

	public float getDistBetween(int pointsCount, float theAxisLength)
	{
		float num;
		if (pointsCount - 1 <= 0)
		{
			num = this.xAxisLength;
			if (this.graphType == WMG_Axis_Graph.graphTypes.bar_side)
			{
				num -= (float)this.lineSeries.Count * this.barWidth;
			}
			else if (this.graphType == WMG_Axis_Graph.graphTypes.combo)
			{
				num -= (float)this.numComboBarSeries * this.barWidth;
			}
			else if (this.graphType == WMG_Axis_Graph.graphTypes.bar_stacked)
			{
				num -= this.barWidth;
			}
			else if (this.graphType == WMG_Axis_Graph.graphTypes.bar_stacked_percent)
			{
				num -= this.barWidth;
			}
		}
		else
		{
			int num2 = pointsCount - 1;
			if (this.graphType != WMG_Axis_Graph.graphTypes.line && this.graphType != WMG_Axis_Graph.graphTypes.line_stacked)
			{
				num2++;
			}
			num = theAxisLength / (float)num2;
			if (this.graphType == WMG_Axis_Graph.graphTypes.bar_side)
			{
				num -= (float)this.lineSeries.Count * this.barWidth / (float)num2;
			}
			else if (this.graphType == WMG_Axis_Graph.graphTypes.combo)
			{
				num -= (float)this.numComboBarSeries * this.barWidth / (float)num2;
			}
			else if (this.graphType == WMG_Axis_Graph.graphTypes.bar_stacked)
			{
				num -= this.barWidth / (float)num2;
			}
			else if (this.graphType == WMG_Axis_Graph.graphTypes.bar_stacked_percent)
			{
				num -= this.barWidth / (float)num2;
			}
		}
		return num;
	}

	[Obsolete("Use xAxis.GetAxisTickNodes")]
	public List<WMG_Node> getXAxisTicks()
	{
		return this.xAxis.GetAxisTickNodes();
	}

	[Obsolete("Use xAxis.GetAxisLabelNodes")]
	public List<WMG_Node> getXAxisLabels()
	{
		return this.xAxis.GetAxisLabelNodes();
	}

	[Obsolete("Use yAxis.GetAxisTickNodes")]
	public List<WMG_Node> getYAxisTicks()
	{
		return this.yAxis.GetAxisTickNodes();
	}

	[Obsolete("Use yAxis.GetAxisLabelNodes")]
	public List<WMG_Node> getYAxisLabels()
	{
		return this.yAxis.GetAxisLabelNodes();
	}

	public void changeAllLinePivots(WMG_Text_Functions.WMGpivotTypes newPivot)
	{
		for (int i = 0; i < this.lineSeries.Count; i++)
		{
			WMG_Series component = this.lineSeries[i].GetComponent<WMG_Series>();
			List<GameObject> lines = component.getLines();
			for (int j = 0; j < lines.Count; j++)
			{
				base.changeSpritePivot(lines[j], newPivot);
				WMG_Link component2 = lines[j].GetComponent<WMG_Link>();
				component2.Reposition();
			}
		}
	}

	public List<Vector3> getSeriesScaleVectors(bool useLineWidthForX, float x, float y)
	{
		List<Vector3> list = new List<Vector3>();
		for (int i = 0; i < this.lineSeries.Count; i++)
		{
			WMG_Series component = this.lineSeries[i].GetComponent<WMG_Series>();
			if (useLineWidthForX)
			{
				list.Add(new Vector3(component.lineScale, y, 1f));
			}
			else
			{
				list.Add(new Vector3(x, y, 1f));
			}
		}
		return list;
	}

	public float getMaxPointSize()
	{
		if (this.graphType == WMG_Axis_Graph.graphTypes.line || this.graphType == WMG_Axis_Graph.graphTypes.line_stacked || (this.graphType == WMG_Axis_Graph.graphTypes.combo && this.numComboBarSeries == 0))
		{
			float num = 0f;
			for (int i = 0; i < this.lineSeries.Count; i++)
			{
				WMG_Series component = this.lineSeries[i].GetComponent<WMG_Series>();
				if (component.pointWidthHeight > num)
				{
					num = component.pointWidthHeight;
				}
			}
			return num;
		}
		float num2 = this.barWidth;
		if (this.graphType == WMG_Axis_Graph.graphTypes.combo)
		{
			for (int j = 0; j < this.lineSeries.Count; j++)
			{
				WMG_Series component2 = this.lineSeries[j].GetComponent<WMG_Series>();
				if (component2.comboType == WMG_Series.comboTypes.line && component2.pointWidthHeight > num2)
				{
					num2 = component2.pointWidthHeight;
				}
			}
		}
		return num2;
	}

	public int getMaxNumPoints()
	{
		return this.maxSeriesPointCount;
	}

	public void setAxesQuadrant1()
	{
		this.xAxis.setAxisTopRight(false);
		this.yAxis.setAxisTopRight(false);
	}

	public void setAxesQuadrant2()
	{
		this.xAxis.setAxisBotLeft(false);
		this.yAxis.setAxisTopRight(true);
	}

	public void setAxesQuadrant3()
	{
		this.xAxis.setAxisBotLeft(true);
		this.yAxis.setAxisBotLeft(true);
	}

	public void setAxesQuadrant4()
	{
		this.xAxis.setAxisTopRight(true);
		this.yAxis.setAxisBotLeft(false);
	}

	public void setAxesQuadrant1_2_3_4()
	{
		this.xAxis.setAxisMiddle(false);
		this.yAxis.setAxisMiddle(false);
	}

	public void setAxesQuadrant1_2()
	{
		this.xAxis.setAxisMiddle(false);
		this.yAxis.setAxisTopRight(false);
	}

	public void setAxesQuadrant3_4()
	{
		this.xAxis.setAxisMiddle(true);
		this.yAxis.setAxisBotLeft(false);
	}

	public void setAxesQuadrant2_3()
	{
		this.xAxis.setAxisBotLeft(false);
		this.yAxis.setAxisMiddle(true);
	}

	public void setAxesQuadrant1_4()
	{
		this.xAxis.setAxisTopRight(false);
		this.yAxis.setAxisMiddle(false);
	}

	private Vector2 getAxesOffsetFactor()
	{
		if (this.axesType == WMG_Axis_Graph.axesTypes.I)
		{
			return new Vector2(-1f, -1f);
		}
		if (this.axesType == WMG_Axis_Graph.axesTypes.II)
		{
			return new Vector2(1f, -1f);
		}
		if (this.axesType == WMG_Axis_Graph.axesTypes.III)
		{
			return new Vector2(1f, 1f);
		}
		if (this.axesType == WMG_Axis_Graph.axesTypes.IV)
		{
			return new Vector2(-1f, 1f);
		}
		if (this.axesType == WMG_Axis_Graph.axesTypes.CENTER)
		{
			return new Vector2(0f, 0f);
		}
		if (this.axesType == WMG_Axis_Graph.axesTypes.I_II)
		{
			return new Vector2(0f, -1f);
		}
		if (this.axesType == WMG_Axis_Graph.axesTypes.III_IV)
		{
			return new Vector2(0f, 1f);
		}
		if (this.axesType == WMG_Axis_Graph.axesTypes.II_III)
		{
			return new Vector2(1f, 0f);
		}
		if (this.axesType == WMG_Axis_Graph.axesTypes.I_IV)
		{
			return new Vector2(-1f, 0f);
		}
		if (this.axesType == WMG_Axis_Graph.axesTypes.AUTO_ORIGIN || this.axesType == WMG_Axis_Graph.axesTypes.AUTO_ORIGIN_X || this.axesType == WMG_Axis_Graph.axesTypes.AUTO_ORIGIN_Y)
		{
			float x = 0f;
			float y = 0f;
			if (this.axesType == WMG_Axis_Graph.axesTypes.AUTO_ORIGIN || this.axesType == WMG_Axis_Graph.axesTypes.AUTO_ORIGIN_Y)
			{
				if (this.xAxis.AxisMinValue >= this.theOrigin.x)
				{
					y = -1f;
				}
				else if (this.xAxis.AxisMaxValue <= this.theOrigin.x)
				{
					y = 1f;
				}
			}
			if (this.axesType == WMG_Axis_Graph.axesTypes.AUTO_ORIGIN || this.axesType == WMG_Axis_Graph.axesTypes.AUTO_ORIGIN_X)
			{
				if (this.yAxis.AxisMinValue >= this.theOrigin.y)
				{
					x = -1f;
				}
				else if (this.yAxis.AxisMaxValue <= this.theOrigin.y)
				{
					x = 1f;
				}
			}
			return new Vector2(x, y);
		}
		return new Vector2(0f, 0f);
	}

	public void animScaleAllAtOnce(bool isPoint, float duration, float delay, Ease anEaseType, List<Vector3> before, List<Vector3> after)
	{
		for (int i = 0; i < this.lineSeries.Count; i++)
		{
			WMG_Series component = this.lineSeries[i].GetComponent<WMG_Series>();
			List<GameObject> list;
			if (isPoint)
			{
				list = component.getPoints();
			}
			else
			{
				list = component.getLines();
			}
			for (int j = 0; j < list.Count; j++)
			{
				list[j].transform.localScale = before[i];
				WMG_Anim.animScale(list[j], duration, anEaseType, after[i], delay);
			}
		}
	}

	public void animScaleBySeries(bool isPoint, float duration, float delay, Ease anEaseType, List<Vector3> before, List<Vector3> after)
	{
		Sequence t = DOTween.Sequence();
		float num = duration / (float)this.lineSeries.Count;
		float num2 = delay / (float)this.lineSeries.Count;
		for (int i = 0; i < this.lineSeries.Count; i++)
		{
			WMG_Series component = this.lineSeries[i].GetComponent<WMG_Series>();
			List<GameObject> list;
			if (isPoint)
			{
				list = component.getPoints();
			}
			else
			{
				list = component.getLines();
			}
			float insTime = (float)i * num + (float)(i + 1) * num2;
			for (int j = 0; j < list.Count; j++)
			{
				list[j].transform.localScale = before[i];
				WMG_Anim.animScaleSeqInsert(ref t, insTime, list[j], num, anEaseType, after[i], num2);
			}
		}
		t.Play<Sequence>();
	}

	public void animScaleOneByOne(bool isPoint, float duration, float delay, Ease anEaseType, List<Vector3> before, List<Vector3> after, int loopDir)
	{
		for (int i = 0; i < this.lineSeries.Count; i++)
		{
			Sequence t = DOTween.Sequence();
			WMG_Series component = this.lineSeries[i].GetComponent<WMG_Series>();
			List<GameObject> list;
			if (isPoint)
			{
				list = component.getPoints();
			}
			else
			{
				list = component.getLines();
			}
			float duration2 = duration / (float)list.Count;
			float delay2 = delay / (float)list.Count;
			if (loopDir == 0)
			{
				for (int j = 0; j < list.Count; j++)
				{
					list[j].transform.localScale = before[i];
					WMG_Anim.animScaleSeqAppend(ref t, list[j], duration2, anEaseType, after[i], delay2);
				}
			}
			else if (loopDir == 1)
			{
				for (int k = list.Count - 1; k >= 0; k--)
				{
					list[k].transform.localScale = before[i];
					WMG_Anim.animScaleSeqAppend(ref t, list[k], duration2, anEaseType, after[i], delay2);
				}
			}
			else if (loopDir == 2)
			{
				int num = list.Count - 1;
				int num2 = num / 2;
				int num3 = -1;
				int num4 = 0;
				bool flag = false;
				bool flag2 = false;
				while (!flag || !flag2)
				{
					if (num2 >= 0 && num2 <= num)
					{
						list[num2].transform.localScale = before[i];
						WMG_Anim.animScaleSeqAppend(ref t, list[num2], duration2, anEaseType, after[i], delay2);
					}
					num4++;
					num3 *= -1;
					num2 += num3 * num4;
					if (num2 < 0)
					{
						flag = true;
					}
					if (num2 > num)
					{
						flag2 = true;
					}
				}
			}
			t.Play<Sequence>();
		}
	}

	public WMG_Series addSeries()
	{
		return this.addSeriesAt(this.lineSeries.Count);
	}

	public void deleteSeries()
	{
		this.deleteSeriesAt(this.lineSeries.Count - 1);
	}

	public WMG_Series addSeriesAt(int index)
	{
		if (Application.isPlaying)
		{
			this.Init();
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(this.seriesPrefab) as GameObject;
		gameObject.name = "Series" + (index + 1);
		base.changeSpriteParent(gameObject, this.seriesParent);
		gameObject.transform.localScale = Vector3.one;
		WMG_Series component = gameObject.GetComponent<WMG_Series>();
		if (this.autoAnimationsEnabled)
		{
			this.autoAnim.addSeriesForAutoAnim(component);
		}
		component.theGraph = this;
		this.lineSeries.Insert(index, gameObject);
		component.Init(index);
		return gameObject.GetComponent<WMG_Series>();
	}

	public void deleteSeriesAt(int index)
	{
		if (Application.isPlaying)
		{
			this.Init();
		}
		GameObject gameObject = this.lineSeries[index];
		WMG_Series component = gameObject.GetComponent<WMG_Series>();
		this.lineSeries.Remove(gameObject);
		if (Application.isPlaying)
		{
			component.deleteAllNodesFromGraphManager();
			this.legend.deleteLegendEntry(index);
		}
		UnityEngine.Object.DestroyImmediate(gameObject);
		this.graphC.Changed();
		if (this.graphType != WMG_Axis_Graph.graphTypes.line && this.graphType != WMG_Axis_Graph.graphTypes.line_stacked)
		{
			this.seriesNoCountC.Changed();
		}
		this.legend.legendC.Changed();
	}

	private void UpdateFromContainer()
	{
		if (this.resizeEnabled)
		{
			bool flag = true;
			Vector2 vector = new Vector2(this.cachedContainerWidth / this.origWidth, this.cachedContainerHeight / this.origHeight);
			Vector2 vector2 = vector;
			if (this.orientationType == WMG_Axis_Graph.orientationTypes.horizontal)
			{
				vector2 = new Vector2(vector.y, vector.x);
			}
			float num = vector.x;
			if (vector.y < num)
			{
				num = vector.y;
			}
			if ((this.resizeProperties & WMG_Axis_Graph.ResizeProperties.BorderPadding) == WMG_Axis_Graph.ResizeProperties.BorderPadding)
			{
				if (this.autoFitLabels)
				{
					if (this.xAxis.AxisTicksRightAbove)
					{
						this.paddingLeftRight = new Vector2(this.getNewResizeVariable(num, this.origPaddingLeftRight.x), this.paddingLeftRight.y);
					}
					else
					{
						this.paddingLeftRight = new Vector2(this.paddingLeftRight.x, this.getNewResizeVariable(num, this.origPaddingLeftRight.y));
					}
					if (this.yAxis.AxisTicksRightAbove)
					{
						this.paddingTopBottom = new Vector2(this.paddingTopBottom.x, this.getNewResizeVariable(num, this.origPaddingTopBottom.y));
					}
					else
					{
						this.paddingTopBottom = new Vector2(this.getNewResizeVariable(num, this.origPaddingTopBottom.x), this.paddingTopBottom.y);
					}
				}
				else
				{
					this.paddingLeftRight = new Vector2(this.getNewResizeVariable(num, this.origPaddingLeftRight.x), this.getNewResizeVariable(num, this.origPaddingLeftRight.y));
					this.paddingTopBottom = new Vector2(this.getNewResizeVariable(num, this.origPaddingTopBottom.x), this.getNewResizeVariable(num, this.origPaddingTopBottom.y));
				}
			}
			if ((this.resizeProperties & WMG_Axis_Graph.ResizeProperties.AutofitPadding) == WMG_Axis_Graph.ResizeProperties.AutofitPadding)
			{
				this.autoFitPadding = this.getNewResizeVariable(num, this.origAutoFitPadding);
			}
			if ((this.resizeProperties & WMG_Axis_Graph.ResizeProperties.TickSize) == WMG_Axis_Graph.ResizeProperties.TickSize)
			{
				this.tickSize = new Vector2(this.getNewResizeVariable(num, this.origTickSize.x), this.getNewResizeVariable(num, this.origTickSize.y));
			}
			if ((this.resizeProperties & WMG_Axis_Graph.ResizeProperties.AxesWidth) == WMG_Axis_Graph.ResizeProperties.AxesWidth)
			{
				this.axisWidth = Mathf.RoundToInt(this.getNewResizeVariable(num, this.origAxisWidth));
			}
			if ((this.resizeProperties & WMG_Axis_Graph.ResizeProperties.AxesLabelSize) == WMG_Axis_Graph.ResizeProperties.AxesLabelSize)
			{
				if (flag)
				{
					this.yAxis.setLabelScales(this.getNewResizeVariable(num, 1f));
					this.xAxis.setLabelScales(this.getNewResizeVariable(num, 1f));
				}
				else
				{
					this.yAxis.AxisLabelSize = Mathf.RoundToInt(this.getNewResizeVariable(num, (float)this.yAxis.origAxisLabelSize));
					this.xAxis.AxisLabelSize = Mathf.RoundToInt(this.getNewResizeVariable(num, (float)this.xAxis.origAxisLabelSize));
				}
			}
			if ((this.resizeProperties & WMG_Axis_Graph.ResizeProperties.AxesLabelOffset) == WMG_Axis_Graph.ResizeProperties.AxesLabelOffset)
			{
				this.yAxis.AxisLabelSpaceOffset = (float)Mathf.RoundToInt(this.getNewResizeVariable(num, this.yAxis.origAxisLabelSpaceOffset));
				this.xAxis.AxisLabelSpaceOffset = (float)Mathf.RoundToInt(this.getNewResizeVariable(num, this.xAxis.origAxisLabelSpaceOffset));
			}
			if ((this.resizeProperties & WMG_Axis_Graph.ResizeProperties.AxesLabelOffset) == WMG_Axis_Graph.ResizeProperties.AxesLabelOffset)
			{
				this.yAxis.AxisTitleFontSize = Mathf.RoundToInt(this.getNewResizeVariable(num, (float)this.yAxis.origAxisTitleFontSize));
				this.xAxis.AxisTitleFontSize = Mathf.RoundToInt(this.getNewResizeVariable(num, (float)this.xAxis.origAxisTitleFontSize));
			}
			if ((this.resizeProperties & WMG_Axis_Graph.ResizeProperties.AxesLinePadding) == WMG_Axis_Graph.ResizeProperties.AxesLinePadding)
			{
				this.yAxis.AxisLinePadding = this.getNewResizeVariable(num, this.yAxis.origAxisLinePadding);
				this.xAxis.AxisLinePadding = this.getNewResizeVariable(num, this.xAxis.origAxisLinePadding);
			}
			if ((this.resizeProperties & WMG_Axis_Graph.ResizeProperties.AxesArrowSize) == WMG_Axis_Graph.ResizeProperties.AxesArrowSize)
			{
				Vector2 vector3 = new Vector2(this.getNewResizeVariable(num, this.yAxis.origAxisArrowSize.x), this.getNewResizeVariable(num, this.yAxis.origAxisArrowSize.y));
				base.changeSpriteSize(this.yAxis.AxisArrowDL, Mathf.RoundToInt(vector3.x), Mathf.RoundToInt(vector3.y));
				base.changeSpriteSize(this.yAxis.AxisArrowUR, Mathf.RoundToInt(vector3.x), Mathf.RoundToInt(vector3.y));
				Vector2 vector4 = new Vector2(this.getNewResizeVariable(num, this.xAxis.origAxisArrowSize.x), this.getNewResizeVariable(num, this.xAxis.origAxisArrowSize.y));
				base.changeSpriteSize(this.xAxis.AxisArrowDL, Mathf.RoundToInt(vector4.x), Mathf.RoundToInt(vector4.y));
				base.changeSpriteSize(this.xAxis.AxisArrowUR, Mathf.RoundToInt(vector4.x), Mathf.RoundToInt(vector4.y));
			}
			if ((this.resizeProperties & WMG_Axis_Graph.ResizeProperties.LegendFontSize) == WMG_Axis_Graph.ResizeProperties.LegendFontSize)
			{
				if (flag)
				{
					this.legend.setLabelScales(this.getNewResizeVariable(num, 1f));
				}
				else
				{
					this.legend.legendEntryFontSize = Mathf.RoundToInt(this.getNewResizeVariable(num, (float)this.legend.origLegendEntryFontSize));
				}
			}
			if ((this.resizeProperties & WMG_Axis_Graph.ResizeProperties.LegendEntrySize) == WMG_Axis_Graph.ResizeProperties.LegendEntrySize)
			{
				if (!this.legend.setWidthFromLabels)
				{
					this.legend.legendEntryWidth = this.getNewResizeVariable(num, this.legend.origLegendEntryWidth);
				}
				this.legend.legendEntryHeight = this.getNewResizeVariable(num, this.legend.origLegendEntryHeight);
			}
			if ((this.resizeProperties & WMG_Axis_Graph.ResizeProperties.LegendOffset) == WMG_Axis_Graph.ResizeProperties.LegendOffset)
			{
				this.legend.offset = this.getNewResizeVariable(num, this.legend.origOffset);
			}
			if ((this.resizeProperties & WMG_Axis_Graph.ResizeProperties.SeriesPointSize) == WMG_Axis_Graph.ResizeProperties.SeriesPointSize)
			{
				this.legend.legendEntryLinkSpacing = this.getNewResizeVariable(num, this.legend.origLegendEntryLinkSpacing);
				this.legend.legendEntrySpacing = this.getNewResizeVariable(num, this.legend.origLegendEntrySpacing);
			}
			if ((this.resizeProperties & WMG_Axis_Graph.ResizeProperties.SeriesPointSize) == WMG_Axis_Graph.ResizeProperties.SeriesPointSize)
			{
				this.barWidth = this.getNewResizeVariable(vector2.x, this.origBarWidth);
			}
			if ((this.resizeProperties & WMG_Axis_Graph.ResizeProperties.SeriesPointSize) == WMG_Axis_Graph.ResizeProperties.SeriesPointSize || (this.resizeProperties & WMG_Axis_Graph.ResizeProperties.SeriesLineWidth) == WMG_Axis_Graph.ResizeProperties.SeriesLineWidth || (this.resizeProperties & WMG_Axis_Graph.ResizeProperties.SeriesDataLabelSize) == WMG_Axis_Graph.ResizeProperties.SeriesDataLabelSize || (this.resizeProperties & WMG_Axis_Graph.ResizeProperties.SeriesDataLabelOffset) == WMG_Axis_Graph.ResizeProperties.SeriesDataLabelOffset)
			{
				for (int i = 0; i < this.lineSeries.Count; i++)
				{
					if (base.activeInHierarchy(this.lineSeries[i]))
					{
						WMG_Series component = this.lineSeries[i].GetComponent<WMG_Series>();
						if ((this.resizeProperties & WMG_Axis_Graph.ResizeProperties.SeriesPointSize) == WMG_Axis_Graph.ResizeProperties.SeriesPointSize)
						{
							component.pointWidthHeight = this.getNewResizeVariable(num, component.origPointWidthHeight);
						}
						if ((this.resizeProperties & WMG_Axis_Graph.ResizeProperties.SeriesLineWidth) == WMG_Axis_Graph.ResizeProperties.SeriesLineWidth)
						{
							component.lineScale = this.getNewResizeVariable(num, component.origLineScale);
						}
						if ((this.resizeProperties & WMG_Axis_Graph.ResizeProperties.SeriesDataLabelSize) == WMG_Axis_Graph.ResizeProperties.SeriesDataLabelSize)
						{
							component.dataLabelsFontSize = Mathf.RoundToInt(this.getNewResizeVariable(num, (float)component.origDataLabelsFontSize));
						}
						if ((this.resizeProperties & WMG_Axis_Graph.ResizeProperties.SeriesDataLabelOffset) == WMG_Axis_Graph.ResizeProperties.SeriesDataLabelOffset)
						{
							component.dataLabelsOffset = new Vector2(this.getNewResizeVariable(num, component.origDataLabelOffset.x), this.getNewResizeVariable(num, component.origDataLabelOffset.y));
						}
					}
				}
			}
		}
	}

	private float getNewResizeVariable(float sizeFactor, float variable)
	{
		return variable + (sizeFactor - 1f) * variable;
	}

	[SerializeField]
	public WMG_Axis yAxis;

	[SerializeField]
	public WMG_Axis xAxis;

	[SerializeField]
	private List<string> _groups;

	public WMG_List<string> groups = new WMG_List<string>();

	public Vector2 tooltipOffset;

	public int tooltipNumberDecimals;

	public bool tooltipDisplaySeriesName;

	public bool tooltipAnimationsEnabled;

	public Ease tooltipAnimationsEasetype;

	public float tooltipAnimationsDuration;

	public Ease autoAnimationsEasetype;

	public float autoAnimationsDuration;

	public List<GameObject> lineSeries;

	public List<UnityEngine.Object> pointPrefabs;

	public List<UnityEngine.Object> linkPrefabs;

	public UnityEngine.Object barPrefab;

	public UnityEngine.Object seriesPrefab;

	public WMG_Legend legend;

	public GameObject graphTitle;

	public GameObject graphBackground;

	public GameObject anchoredParent;

	public GameObject seriesParent;

	public GameObject toolTipPanel;

	public GameObject toolTipLabel;

	[SerializeField]
	private WMG_Axis_Graph.graphTypes _graphType;

	[SerializeField]
	private WMG_Axis_Graph.orientationTypes _orientationType;

	[SerializeField]
	private WMG_Axis_Graph.axesTypes _axesType;

	[SerializeField]
	private bool _resizeEnabled;

	[WMG_EnumFlag]
	[SerializeField]
	private WMG_Axis_Graph.ResizeProperties _resizeProperties;

	[SerializeField]
	private bool _useGroups;

	[SerializeField]
	private Vector2 _paddingLeftRight;

	[SerializeField]
	private Vector2 _paddingTopBottom;

	[SerializeField]
	private Vector2 _theOrigin;

	[SerializeField]
	private float _barWidth;

	[SerializeField]
	private float _barAxisValue;

	[SerializeField]
	private bool _autoUpdateOrigin;

	[SerializeField]
	private bool _autoUpdateBarWidth;

	[SerializeField]
	private float _autoUpdateBarWidthSpacing;

	[SerializeField]
	private bool _autoUpdateSeriesAxisSpacing;

	[SerializeField]
	private bool _autoUpdateBarAxisValue;

	[SerializeField]
	private int _axisWidth;

	[SerializeField]
	private float _autoShrinkAtPercent;

	[SerializeField]
	private float _autoGrowAndShrinkByPercent;

	[SerializeField]
	private bool _tooltipEnabled;

	[SerializeField]
	private bool _autoAnimationsEnabled;

	[SerializeField]
	private bool _autoFitLabels;

	[SerializeField]
	private float _autoFitPadding;

	[SerializeField]
	private Vector2 _tickSize;

	[SerializeField]
	private string _graphTitleString;

	[SerializeField]
	private Vector2 _graphTitleOffset;

	private List<float> totalPointValues = new List<float>();

	private int maxSeriesPointCount;

	private int maxSeriesBarCount;

	private int numComboBarSeries;

	private float origWidth;

	private float origHeight;

	private float origBarWidth;

	private float origAxisWidth;

	private float origAutoFitPadding;

	private Vector2 origTickSize;

	private Vector2 origPaddingLeftRight;

	private Vector2 origPaddingTopBottom;

	private float cachedContainerWidth;

	private float cachedContainerHeight;

	public WMG_Graph_Tooltip theTooltip;

	private WMG_Graph_Auto_Anim autoAnim;

	private bool hasInit;

	private List<WMG_Change_Obj> changeObjs = new List<WMG_Change_Obj>();

	public WMG_Change_Obj graphC = new WMG_Change_Obj();

	public WMG_Change_Obj resizeC = new WMG_Change_Obj();

	public WMG_Change_Obj seriesCountC = new WMG_Change_Obj();

	public WMG_Change_Obj seriesNoCountC = new WMG_Change_Obj();

	private WMG_Change_Obj tooltipEnabledC = new WMG_Change_Obj();

	private WMG_Change_Obj autoAnimEnabledC = new WMG_Change_Obj();

	private WMG_Change_Obj orientationC = new WMG_Change_Obj();

	private WMG_Change_Obj graphTypeC = new WMG_Change_Obj();

	public enum graphTypes
	{
		line,
		line_stacked,
		bar_side,
		bar_stacked,
		bar_stacked_percent,
		combo
	}

	public enum orientationTypes
	{
		vertical,
		horizontal
	}

	public enum axesTypes
	{
		MANUAL,
		CENTER,
		AUTO_ORIGIN,
		AUTO_ORIGIN_X,
		AUTO_ORIGIN_Y,
		I,
		II,
		III,
		IV,
		I_II,
		III_IV,
		II_III,
		I_IV
	}

	[Flags]
	public enum ResizeProperties
	{
		SeriesPointSize = 1,
		SeriesLineWidth = 2,
		SeriesDataLabelSize = 4,
		SeriesDataLabelOffset = 8,
		LegendFontSize = 16,
		LegendEntrySize = 32,
		LegendOffset = 64,
		AxesWidth = 128,
		AxesLabelSize = 256,
		AxesLabelOffset = 512,
		AxesTitleSize = 1024,
		AxesLinePadding = 2048,
		AxesArrowSize = 4096,
		AutofitPadding = 8192,
		BorderPadding = 16384,
		TickSize = 32768
	}

	public delegate void GraphBackgroundChangedHandler(WMG_Axis_Graph aGraph);
}
