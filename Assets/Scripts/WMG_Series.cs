using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class WMG_Series : MonoBehaviour
{
	public WMG_Series.comboTypes comboType
	{
		get
		{
			return this._comboType;
		}
		set
		{
			if (this._comboType != value)
			{
				this._comboType = value;
				this.prefabC.Changed();
			}
		}
	}

	public string seriesName
	{
		get
		{
			return this._seriesName;
		}
		set
		{
			if (this._seriesName != value)
			{
				this._seriesName = value;
				this.seriesNameC.Changed();
			}
		}
	}

	public float pointWidthHeight
	{
		get
		{
			return this._pointWidthHeight;
		}
		set
		{
			if (this._pointWidthHeight != value)
			{
				this._pointWidthHeight = value;
				this.pointWidthHeightC.Changed();
			}
		}
	}

	public float lineScale
	{
		get
		{
			return this._lineScale;
		}
		set
		{
			if (this._lineScale != value)
			{
				this._lineScale = value;
				this.lineScaleC.Changed();
			}
		}
	}

	public Color pointColor
	{
		get
		{
			return this._pointColor;
		}
		set
		{
			if (this._pointColor != value)
			{
				this._pointColor = value;
				this.pointColorC.Changed();
			}
		}
	}

	public bool usePointColors
	{
		get
		{
			return this._usePointColors;
		}
		set
		{
			if (this._usePointColors != value)
			{
				this._usePointColors = value;
				this.pointColorC.Changed();
			}
		}
	}

	public Color lineColor
	{
		get
		{
			return this._lineColor;
		}
		set
		{
			if (this._lineColor != value)
			{
				this._lineColor = value;
				this.lineColorC.Changed();
			}
		}
	}

	public bool UseXDistBetweenToSpace
	{
		get
		{
			return this._UseXDistBetweenToSpace;
		}
		set
		{
			if (this._UseXDistBetweenToSpace != value)
			{
				this._UseXDistBetweenToSpace = value;
				this.pointValuesC.Changed();
			}
		}
	}

	public bool AutoUpdateXDistBetween
	{
		get
		{
			return this._AutoUpdateXDistBetween;
		}
		set
		{
			if (this._AutoUpdateXDistBetween != value)
			{
				this._AutoUpdateXDistBetween = value;
				this.pointValuesC.Changed();
			}
		}
	}

	public float xDistBetweenPoints
	{
		get
		{
			return this._xDistBetweenPoints;
		}
		set
		{
			if (this._xDistBetweenPoints != value)
			{
				this._xDistBetweenPoints = value;
				this.pointValuesC.Changed();
			}
		}
	}

	public float extraXSpace
	{
		get
		{
			return this._extraXSpace;
		}
		set
		{
			if (this._extraXSpace != value)
			{
				this._extraXSpace = value;
				this.pointValuesC.Changed();
			}
		}
	}

	public bool hidePoints
	{
		get
		{
			return this._hidePoints;
		}
		set
		{
			if (this._hidePoints != value)
			{
				this._hidePoints = value;
				this.hidePointC.Changed();
			}
		}
	}

	public bool hideLines
	{
		get
		{
			return this._hideLines;
		}
		set
		{
			if (this._hideLines != value)
			{
				this._hideLines = value;
				this.hideLineC.Changed();
			}
		}
	}

	public bool connectFirstToLast
	{
		get
		{
			return this._connectFirstToLast;
		}
		set
		{
			if (this._connectFirstToLast != value)
			{
				this._connectFirstToLast = value;
				this.connectFirstToLastC.Changed();
				this.lineScaleC.Changed();
				this.linePaddingC.Changed();
				this.hideLineC.Changed();
				this.lineColorC.Changed();
			}
		}
	}

	public float linePadding
	{
		get
		{
			return this._linePadding;
		}
		set
		{
			if (this._linePadding != value)
			{
				this._linePadding = value;
				this.linePaddingC.Changed();
			}
		}
	}

	public bool dataLabelsEnabled
	{
		get
		{
			return this._dataLabelsEnabled;
		}
		set
		{
			if (this._dataLabelsEnabled != value)
			{
				this._dataLabelsEnabled = value;
				this.dataLabelsC.Changed();
			}
		}
	}

	public int dataLabelsNumDecimals
	{
		get
		{
			return this._dataLabelsNumDecimals;
		}
		set
		{
			if (this._dataLabelsNumDecimals != value)
			{
				this._dataLabelsNumDecimals = value;
				this.dataLabelsC.Changed();
			}
		}
	}

	public int dataLabelsFontSize
	{
		get
		{
			return this._dataLabelsFontSize;
		}
		set
		{
			if (this._dataLabelsFontSize != value)
			{
				this._dataLabelsFontSize = value;
				this.dataLabelsC.Changed();
			}
		}
	}

	public Vector2 dataLabelsOffset
	{
		get
		{
			return this._dataLabelsOffset;
		}
		set
		{
			if (this._dataLabelsOffset != value)
			{
				this._dataLabelsOffset = value;
				this.dataLabelsC.Changed();
			}
		}
	}

	public WMG_Series.areaShadingTypes areaShadingType
	{
		get
		{
			return this._areaShadingType;
		}
		set
		{
			if (this._areaShadingType != value)
			{
				this._areaShadingType = value;
				this.areaShadingTypeC.Changed();
			}
		}
	}

	public Color areaShadingColor
	{
		get
		{
			return this._areaShadingColor;
		}
		set
		{
			if (this._areaShadingColor != value)
			{
				this._areaShadingColor = value;
				this.areaShadingC.Changed();
			}
		}
	}

	public float areaShadingAxisValue
	{
		get
		{
			return this._areaShadingAxisValue;
		}
		set
		{
			if (this._areaShadingAxisValue != value)
			{
				this._areaShadingAxisValue = value;
				this.areaShadingC.Changed();
			}
		}
	}

	public int pointPrefab
	{
		get
		{
			return this._pointPrefab;
		}
		set
		{
			if (this._pointPrefab != value)
			{
				this._pointPrefab = value;
				this.prefabC.Changed();
			}
		}
	}

	public int linkPrefab
	{
		get
		{
			return this._linkPrefab;
		}
		set
		{
			if (this._linkPrefab != value)
			{
				this._linkPrefab = value;
				this.prefabC.Changed();
			}
		}
	}

	public bool seriesIsLine
	{
		get
		{
			return this.theGraph.graphType == WMG_Axis_Graph.graphTypes.line || this.theGraph.graphType == WMG_Axis_Graph.graphTypes.line_stacked || (this.theGraph.graphType == WMG_Axis_Graph.graphTypes.combo && this.comboType == WMG_Series.comboTypes.line);
		}
	}

	public bool IsLast
	{
		get
		{
			return this.theGraph.lineSeries[this.theGraph.lineSeries.Count - 1].GetComponent<WMG_Series>() == this;
		}
	}

	public float origPointWidthHeight { get; private set; }

	public float origLineScale { get; private set; }

	public int origDataLabelsFontSize { get; private set; }

	public Vector2 origDataLabelOffset { get; set; }

	public bool currentlyAnimating { get; set; }

	public string formatSeriesDataLabel(WMG_Series series, float val)
	{
		float num = Mathf.Pow(10f, (float)series.dataLabelsNumDecimals);
		return (Mathf.Round(val * num) / num).ToString();
	}

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event WMG_Series.SeriesDataChangedHandler SeriesDataChanged;

	protected virtual void OnSeriesDataChanged()
	{
		WMG_Series.SeriesDataChangedHandler seriesDataChanged = this.SeriesDataChanged;
		if (seriesDataChanged != null)
		{
			seriesDataChanged(this);
		}
	}

	[ContextMenu("Init")]
	public void Init(int index)
	{
		if (this.hasInit)
		{
			return;
		}
		this.hasInit = true;
		this.changeObjs.Add(this.pointValuesCountC);
		this.changeObjs.Add(this.pointValuesC);
		this.changeObjs.Add(this.pointValuesValC);
		this.changeObjs.Add(this.connectFirstToLastC);
		this.changeObjs.Add(this.lineScaleC);
		this.changeObjs.Add(this.pointWidthHeightC);
		this.changeObjs.Add(this.dataLabelsC);
		this.changeObjs.Add(this.lineColorC);
		this.changeObjs.Add(this.pointColorC);
		this.changeObjs.Add(this.hideLineC);
		this.changeObjs.Add(this.hidePointC);
		this.changeObjs.Add(this.seriesNameC);
		this.changeObjs.Add(this.linePaddingC);
		this.changeObjs.Add(this.areaShadingTypeC);
		this.changeObjs.Add(this.areaShadingC);
		this.changeObjs.Add(this.prefabC);
		if (this.seriesIsLine)
		{
			this.nodePrefab = this.theGraph.pointPrefabs[this.pointPrefab];
		}
		else
		{
			this.nodePrefab = this.theGraph.barPrefab;
		}
		this.legendEntry = this.theGraph.legend.createLegendEntry(this.legendEntryPrefab, this, index);
		this.createLegendSwatch();
		this.theGraph.legend.updateLegend();
		this.pointValues.SetList(this._pointValues);
		this.pointValues.Changed += this.pointValuesListChanged;
		this.pointColors.SetList(this._pointColors);
		this.pointColors.Changed += this.pointColorsListChanged;
		this.pointValuesCountC.OnChange += this.PointValuesCountChanged;
		this.pointValuesC.OnChange += this.PointValuesChanged;
		this.pointValuesValC.OnChange += this.PointValuesValChanged;
		this.lineScaleC.OnChange += this.LineScaleChanged;
		this.pointWidthHeightC.OnChange += this.PointWidthHeightChanged;
		this.dataLabelsC.OnChange += this.DataLabelsChanged;
		this.lineColorC.OnChange += this.LineColorChanged;
		this.pointColorC.OnChange += this.PointColorChanged;
		this.hideLineC.OnChange += this.HideLinesChanged;
		this.hidePointC.OnChange += this.HidePointsChanged;
		this.seriesNameC.OnChange += this.SeriesNameChanged;
		this.linePaddingC.OnChange += this.LinePaddingChanged;
		this.areaShadingTypeC.OnChange += this.AreaShadingTypeChanged;
		this.areaShadingC.OnChange += this.AreaShadingChanged;
		this.prefabC.OnChange += this.PrefabChanged;
		this.connectFirstToLastC.OnChange += this.ConnectFirstToLastChanged;
		this.seriesDataLabeler = new WMG_Series.SeriesDataLabeler(this.formatSeriesDataLabel);
		this.setOriginalPropertyValues();
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

	public void pointColorsListChanged(bool editorChange, bool countChanged, bool oneValChanged, int index)
	{
		WMG_Util.listChanged<Color>(editorChange, ref this.pointColors, ref this._pointColors, oneValChanged, index);
		this.pointColorC.Changed();
	}

	public void pointValuesListAboutToChange()
	{
	}

	public void pointValuesListChanged(bool editorChange, bool countChanged, bool oneValChanged, int index)
	{
		WMG_Util.listChanged<Vector2>(editorChange, ref this.pointValues, ref this._pointValues, oneValChanged, index);
		if (countChanged)
		{
			this.pointValuesCountC.Changed();
		}
		else
		{
			this.setAnimatingFromPreviousData();
			if (oneValChanged)
			{
				this.changedValIndices.Add(index);
				this.pointValuesValC.Changed();
			}
			else
			{
				this.pointValuesC.Changed();
			}
		}
	}

	public void PrefabChanged()
	{
		this.UpdatePrefabType();
		this.pointValuesCountC.Changed();
	}

	[ContextMenu("Values changed")]
	public void pointValuesChanged()
	{
		this.theGraph.aSeriesPointsChanged();
		this.UpdateNullVisibility();
		this.UpdateSprites();
	}

	public void pointValuesCountChanged()
	{
		this.theGraph.aSeriesPointsChanged();
		this.CreateOrDeleteSpritesBasedOnPointValues();
		this.UpdateLineColor();
		this.UpdatePointColor();
		this.UpdateLineScale();
		this.UpdatePointWidthHeight();
		this.UpdateHideLines();
		this.UpdateHidePoints();
		this.UpdateNullVisibility();
		this.UpdateLinePadding();
		this.UpdateSprites();
	}

	public void pointValuesValChanged(int index)
	{
		this.theGraph.aSeriesPointsChanged();
		this.UpdateNullVisibility();
		this.UpdateSprites();
	}

	public void PointValuesChanged()
	{
		if (this.theGraph.graphType == WMG_Axis_Graph.graphTypes.bar_stacked_percent || (this.theGraph.IsStacked && !this.IsLast))
		{
			this.theGraph.aSeriesPointsChanged();
			this.theGraph.SeriesChanged(false, true);
		}
		else
		{
			this.pointValuesChanged();
		}
	}

	public void PointValuesCountChanged()
	{
		if (this.theGraph.graphType == WMG_Axis_Graph.graphTypes.bar_stacked_percent || (this.theGraph.IsStacked && !this.IsLast))
		{
			this.theGraph.aSeriesPointsChanged();
			this.theGraph.SeriesChanged(true, true);
		}
		else
		{
			this.pointValuesCountChanged();
		}
	}

	public void PointValuesValChanged()
	{
		if (this.changedValIndices.Count != 1)
		{
			this.PointValuesChanged();
		}
		else
		{
			if (this.theGraph.graphType == WMG_Axis_Graph.graphTypes.bar_stacked_percent || (this.theGraph.IsStacked && !this.IsLast))
			{
				this.theGraph.aSeriesPointsChanged();
				this.theGraph.SeriesChanged(false, true);
			}
			else
			{
				this.pointValuesValChanged(this.changedValIndices[0]);
			}
			this.changedValIndices.Clear();
		}
	}

	public void LineColorChanged()
	{
		this.UpdateLineColor();
	}

	public void ConnectFirstToLastChanged()
	{
		this.createOrDeletePoints(this.pointValues.Count);
	}

	public void PointColorChanged()
	{
		this.UpdatePointColor();
	}

	public void LineScaleChanged()
	{
		this.UpdateLineScale();
	}

	public void PointWidthHeightChanged()
	{
		this.UpdatePointWidthHeight();
	}

	public void HideLinesChanged()
	{
		this.UpdateHideLines();
		this.UpdateNullVisibility();
	}

	public void HidePointsChanged()
	{
		this.UpdateHidePoints();
		this.UpdateNullVisibility();
	}

	public void SeriesNameChanged()
	{
		this.UpdateSeriesName();
	}

	public void LinePaddingChanged()
	{
		this.UpdateLinePadding();
	}

	public void AreaShadingTypeChanged()
	{
		this.createOrDeleteAreaShading(this.pointValues.Count);
	}

	public void AreaShadingChanged()
	{
		this.updateAreaShading();
	}

	public void DataLabelsChanged()
	{
		this.createOrDeleteLabels(this.pointValues.Count);
		this.updateDataLabels();
	}

	public void UpdateFromDataSource()
	{
		if (this.pointValuesDataSource != null)
		{
			List<Vector2> list = this.pointValuesDataSource.getData<Vector2>();
			if (this.theGraph.useGroups)
			{
				list = this.sanitizeGroupData(list);
			}
			this.pointValues.SetList(list);
		}
	}

	public void RealTimeUpdate()
	{
		if (this.realTimeRunning)
		{
			this.DoRealTimeUpdate();
		}
	}

	public List<Vector2> AfterPositions()
	{
		return this.afterPositions;
	}

	public List<int> AfterHeights()
	{
		return this.afterHeights;
	}

	public List<int> AfterWidths()
	{
		return this.afterWidths;
	}

	public bool AnimatingFromPreviousData()
	{
		return this.animatingFromPreviousData;
	}

	public void setAnimatingFromPreviousData()
	{
		if (this.realTimeRunning)
		{
			return;
		}
		if (this.theGraph.IsStacked)
		{
			return;
		}
		if (this.theGraph.autoAnimationsEnabled)
		{
			this.animatingFromPreviousData = true;
		}
	}

	public void setOriginalPropertyValues()
	{
		this.origPointWidthHeight = this.pointWidthHeight;
		this.origLineScale = this.lineScale;
		this.origDataLabelsFontSize = this.dataLabelsFontSize;
		this.origDataLabelOffset = this.dataLabelsOffset;
	}

	public List<GameObject> getPoints()
	{
		return this.points;
	}

	public GameObject getLastPoint()
	{
		return this.points[this.points.Count - 1];
	}

	public GameObject getFirstPoint()
	{
		return this.points[0];
	}

	public List<GameObject> getLines()
	{
		return this.lines;
	}

	public List<GameObject> getDataLabels()
	{
		return this.dataLabels;
	}

	public bool getBarIsNegative(int i)
	{
		return this.barIsNegative[i];
	}

	public Vector2 getNodeValue(WMG_Node aNode)
	{
		for (int i = 0; i < this.pointValues.Count; i++)
		{
			if (this.points[i].GetComponent<WMG_Node>() == aNode)
			{
				return this.pointValues[i];
			}
		}
		return Vector2.zero;
	}

	public void UpdateHidePoints()
	{
		for (int i = 0; i < this.points.Count; i++)
		{
			this.theGraph.SetActive(this.points[i], !this.hidePoints);
		}
		this.theGraph.SetActive(this.legendEntry.swatchNode, !this.hidePoints);
		base.StartCoroutine(this.SetDelayedAreaShadingChanged());
	}

	public void UpdateNullVisibility()
	{
		if (this.theGraph.useGroups)
		{
			for (int i = 0; i < this.points.Count; i++)
			{
				this.theGraph.SetActive(this.points[i], this.pointValues[i].x > 0f);
			}
			if (this.seriesIsLine)
			{
				for (int j = 0; j < this.lines.Count; j++)
				{
					this.theGraph.SetActive(this.lines[j], true);
				}
				for (int k = 0; k < this.points.Count; k++)
				{
					if (this.pointValues[k].x < 0f)
					{
						WMG_Node component = this.points[k].GetComponent<WMG_Node>();
						for (int l = 0; l < component.links.Count; l++)
						{
							this.theGraph.SetActive(component.links[l], false);
						}
					}
				}
			}
			base.StartCoroutine(this.SetDelayedAreaShadingChanged());
		}
		if (this.hidePoints)
		{
			for (int m = 0; m < this.points.Count; m++)
			{
				this.theGraph.SetActive(this.points[m], false);
			}
		}
		if (this.hideLines || !this.seriesIsLine)
		{
			for (int n = 0; n < this.lines.Count; n++)
			{
				this.theGraph.SetActive(this.lines[n], false);
			}
		}
	}

	public void UpdateHideLines()
	{
		for (int i = 0; i < this.lines.Count; i++)
		{
			if (this.hideLines || !this.seriesIsLine)
			{
				this.theGraph.SetActive(this.lines[i], false);
			}
			else
			{
				this.theGraph.SetActive(this.lines[i], true);
			}
		}
		if (this.hideLines || !this.seriesIsLine)
		{
			this.theGraph.SetActive(this.legendEntry.line, false);
		}
		else
		{
			this.theGraph.SetActive(this.legendEntry.line, true);
		}
		base.StartCoroutine(this.SetDelayedAreaShadingChanged());
	}

	public void UpdateLineColor()
	{
		for (int i = 0; i < this.lines.Count; i++)
		{
			WMG_Link component = this.lines[i].GetComponent<WMG_Link>();
			this.theGraph.changeSpriteColor(component.objectToColor, this.lineColor);
		}
		WMG_Link component2 = this.legendEntry.line.GetComponent<WMG_Link>();
		this.theGraph.changeSpriteColor(component2.objectToColor, this.lineColor);
	}

	public void UpdatePointColor()
	{
		for (int i = 0; i < this.points.Count; i++)
		{
			WMG_Node component = this.points[i].GetComponent<WMG_Node>();
			if (this.usePointColors)
			{
				if (i < this.pointColors.Count)
				{
					this.theGraph.changeSpriteColor(component.objectToColor, this.pointColors[i]);
				}
			}
			else
			{
				this.theGraph.changeSpriteColor(component.objectToColor, this.pointColor);
			}
		}
		WMG_Node component2 = this.legendEntry.swatchNode.GetComponent<WMG_Node>();
		this.theGraph.changeSpriteColor(component2.objectToColor, this.pointColor);
	}

	public void UpdateLineScale()
	{
		for (int i = 0; i < this.lines.Count; i++)
		{
			WMG_Link component = this.lines[i].GetComponent<WMG_Link>();
			component.objectToScale.transform.localScale = new Vector3(this.lineScale, component.objectToScale.transform.localScale.y, component.objectToScale.transform.localScale.z);
		}
		WMG_Link component2 = this.legendEntry.line.GetComponent<WMG_Link>();
		component2.objectToScale.transform.localScale = new Vector3(this.lineScale, component2.objectToScale.transform.localScale.y, component2.objectToScale.transform.localScale.z);
	}

	public void UpdatePointWidthHeight()
	{
		if (this.seriesIsLine)
		{
			for (int i = 0; i < this.points.Count; i++)
			{
				WMG_Node component = this.points[i].GetComponent<WMG_Node>();
				this.theGraph.changeSpriteHeight(component.objectToColor, Mathf.RoundToInt(this.pointWidthHeight));
				this.theGraph.changeSpriteWidth(component.objectToColor, Mathf.RoundToInt(this.pointWidthHeight));
			}
		}
		WMG_Node component2 = this.legendEntry.swatchNode.GetComponent<WMG_Node>();
		this.theGraph.changeSpriteHeight(component2.objectToColor, Mathf.RoundToInt(this.pointWidthHeight));
		this.theGraph.changeSpriteWidth(component2.objectToColor, Mathf.RoundToInt(this.pointWidthHeight));
	}

	public void UpdatePrefabType()
	{
		if (this.seriesIsLine)
		{
			this.nodePrefab = this.theGraph.pointPrefabs[this.pointPrefab];
		}
		else
		{
			this.nodePrefab = this.theGraph.barPrefab;
		}
		for (int i = this.points.Count - 1; i >= 0; i--)
		{
			if (this.points[i] != null)
			{
				WMG_Node component = this.points[i].GetComponent<WMG_Node>();
				foreach (GameObject item in component.links)
				{
					this.lines.Remove(item);
				}
				this.theGraph.DeleteNode(component);
				this.points.RemoveAt(i);
			}
		}
		if (this.legendEntry.swatchNode != null)
		{
			this.theGraph.DeleteNode(this.legendEntry.swatchNode.GetComponent<WMG_Node>());
			this.theGraph.DeleteLink(this.legendEntry.line.GetComponent<WMG_Link>());
		}
	}

	public void UpdateSeriesName()
	{
		this.theGraph.legend.LegendChanged();
	}

	public void UpdateLinePadding()
	{
		for (int i = 0; i < this.points.Count; i++)
		{
			this.points[i].GetComponent<WMG_Node>().radius = -1f * this.linePadding;
		}
		this.RepositionLines();
	}

	public void RepositionLines()
	{
		for (int i = 0; i < this.lines.Count; i++)
		{
			this.lines[i].GetComponent<WMG_Link>().Reposition();
		}
	}

	public void CreateOrDeleteSpritesBasedOnPointValues()
	{
		if (this.theGraph.useGroups)
		{
			this.pointValues.SetListNoCb(this.sanitizeGroupData(this.pointValues.list), ref this._pointValues);
		}
		int count = this.pointValues.Count;
		this.createOrDeletePoints(count);
		this.createOrDeleteLabels(count);
		this.createOrDeleteAreaShading(count);
	}

	private List<Vector2> sanitizeGroupData(List<Vector2> groupData)
	{
		for (int i = groupData.Count - 1; i >= 0; i--)
		{
			int num = Mathf.RoundToInt(groupData[i].x);
			if ((float)num - groupData[i].x != 0f)
			{
				groupData.RemoveAt(i);
			}
			else if (Mathf.Abs(num) > this.theGraph.groups.Count)
			{
				groupData.RemoveAt(i);
			}
			else if (num == 0)
			{
				groupData.RemoveAt(i);
			}
		}
		groupData.Sort((Vector2 vec1, Vector2 vec2) => vec1.x.CompareTo(vec2.x));
		List<Vector2> list = new List<Vector2>();
		bool flag = true;
		for (int j = 0; j < groupData.Count; j++)
		{
			if (flag)
			{
				list.Add(groupData[j]);
				flag = false;
			}
			else
			{
				Vector2 vector = list[list.Count - 1];
				list[list.Count - 1] = new Vector2(vector.x, vector.y + groupData[j].y);
			}
			if (j < groupData.Count - 1 && groupData[j].x != groupData[j + 1].x)
			{
				flag = true;
			}
		}
		if (list.Count < this.theGraph.groups.Count)
		{
			int num2 = this.theGraph.groups.Count - list.Count;
			for (int k = 0; k < num2; k++)
			{
				list.Insert(0, new Vector2(-1f, 0f));
			}
		}
		if (list.Count > this.theGraph.groups.Count)
		{
			int num3 = list.Count - this.theGraph.groups.Count;
			for (int l = 0; l < num3; l++)
			{
				list.RemoveAt(0);
			}
		}
		List<int> list2 = new List<int>();
		for (int m = 0; m < this.theGraph.groups.Count; m++)
		{
			list2.Add(m + 1);
		}
		for (int n = list.Count - 1; n >= 0; n--)
		{
			if (list[n].x > 0f)
			{
				list2.Remove(Mathf.RoundToInt(list[n].x));
			}
		}
		for (int num4 = 0; num4 < list2.Count; num4++)
		{
			list[num4] = new Vector2((float)(-1 * list2[num4]), 0f);
		}
		list.Sort((Vector2 vec1, Vector2 vec2) => Mathf.Abs(vec1.x).CompareTo(Mathf.Abs(vec2.x)));
		return list;
	}

	private void createOrDeletePoints(int pointValuesCount)
	{
		for (int i = 0; i < pointValuesCount; i++)
		{
			if (this.points.Count <= i)
			{
				GameObject gameObject = this.theGraph.CreateNode(this.nodePrefab, this.nodeParent);
				this.theGraph.addNodeClickEvent(gameObject);
				this.theGraph.addNodeMouseEnterEvent(gameObject);
				this.theGraph.addNodeMouseLeaveEvent(gameObject);
				gameObject.GetComponent<WMG_Node>().radius = -1f * this.linePadding;
				this.theGraph.SetActive(gameObject, false);
				this.points.Add(gameObject);
				this.barIsNegative.Add(false);
				if (i > 0)
				{
					WMG_Node component = this.points[i - 1].GetComponent<WMG_Node>();
					gameObject = this.theGraph.CreateLink(component, gameObject, this.theGraph.linkPrefabs[this.linkPrefab], this.linkParent);
					this.theGraph.addLinkClickEvent(gameObject);
					this.theGraph.addLinkMouseEnterEvent(gameObject);
					this.theGraph.addLinkMouseLeaveEvent(gameObject);
					this.theGraph.SetActive(gameObject, false);
					this.lines.Add(gameObject);
				}
			}
		}
		for (int j = this.points.Count - 1; j >= 0; j--)
		{
			if (this.points[j] != null && j >= pointValuesCount)
			{
				WMG_Node component2 = this.points[j].GetComponent<WMG_Node>();
				foreach (GameObject item in component2.links)
				{
					this.lines.Remove(item);
				}
				this.theGraph.DeleteNode(component2);
				this.points.RemoveAt(j);
				this.barIsNegative.RemoveAt(j);
			}
			if (j > 1 && j < pointValuesCount - 1)
			{
				WMG_Node component3 = this.points[0].GetComponent<WMG_Node>();
				WMG_Node component4 = this.points[j].GetComponent<WMG_Node>();
				WMG_Link link = this.theGraph.GetLink(component3, component4);
				if (link != null)
				{
					this.lines.Remove(link.gameObject);
					this.theGraph.DeleteLink(link);
				}
			}
		}
		if (this.points.Count > 2)
		{
			WMG_Node component5 = this.points[0].GetComponent<WMG_Node>();
			WMG_Node component6 = this.points[this.points.Count - 1].GetComponent<WMG_Node>();
			WMG_Link link2 = this.theGraph.GetLink(component5, component6);
			if (this.connectFirstToLast && link2 == null)
			{
				GameObject gameObject2 = this.theGraph.CreateLink(component5, component6.gameObject, this.theGraph.linkPrefabs[this.linkPrefab], this.linkParent);
				this.theGraph.addLinkClickEvent(gameObject2);
				this.theGraph.addLinkMouseEnterEvent(gameObject2);
				this.theGraph.addLinkMouseLeaveEvent(gameObject2);
				this.theGraph.SetActive(gameObject2, false);
				this.lines.Add(gameObject2);
			}
			if (!this.connectFirstToLast && link2 != null)
			{
				this.lines.Remove(link2.gameObject);
				this.theGraph.DeleteLink(link2);
			}
		}
		if (this.legendEntry.swatchNode == null)
		{
			this.createLegendSwatch();
		}
	}

	private void createLegendSwatch()
	{
		this.legendEntry.swatchNode = this.theGraph.CreateNode(this.nodePrefab, this.legendEntry.gameObject);
		this.theGraph.addNodeClickEvent_Leg(this.legendEntry.swatchNode);
		this.theGraph.addNodeMouseEnterEvent_Leg(this.legendEntry.swatchNode);
		this.theGraph.addNodeMouseLeaveEvent_Leg(this.legendEntry.swatchNode);
		WMG_Node component = this.legendEntry.swatchNode.GetComponent<WMG_Node>();
		this.theGraph.changeSpritePivot(component.objectToColor, WMG_Text_Functions.WMGpivotTypes.Center);
		component.Reposition(0f, 0f);
		this.legendEntry.line = this.theGraph.CreateLink(this.legendEntry.nodeRight.GetComponent<WMG_Node>(), this.legendEntry.nodeLeft, this.theGraph.linkPrefabs[this.linkPrefab], this.legendEntry.gameObject);
		this.theGraph.addLinkClickEvent_Leg(this.legendEntry.line);
		this.theGraph.addLinkMouseEnterEvent_Leg(this.legendEntry.line);
		this.theGraph.addLinkMouseLeaveEvent_Leg(this.legendEntry.line);
		this.theGraph.bringSpriteToFront(this.legendEntry.swatchNode);
	}

	private void createOrDeleteLabels(int pointValuesCount)
	{
		if (this.dataLabelPrefab != null && this.dataLabelsParent != null)
		{
			if (this.dataLabelsEnabled)
			{
				for (int i = 0; i < pointValuesCount; i++)
				{
					if (this.dataLabels.Count <= i)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate(this.dataLabelPrefab) as GameObject;
						this.theGraph.changeSpriteParent(gameObject, this.dataLabelsParent);
						gameObject.transform.localScale = Vector3.one;
						this.dataLabels.Add(gameObject);
						gameObject.name = "Data_Label_" + this.dataLabels.Count;
					}
				}
			}
			int num = pointValuesCount;
			if (!this.dataLabelsEnabled)
			{
				num = 0;
			}
			else if (this.theGraph.IsStacked && this.theGraph.graphType != WMG_Axis_Graph.graphTypes.line_stacked)
			{
				num = 0;
				this.dataLabelsEnabled = false;
			}
			for (int j = this.dataLabels.Count - 1; j >= 0; j--)
			{
				if (this.dataLabels[j] != null && j >= num)
				{
					UnityEngine.Object.DestroyImmediate(this.dataLabels[j]);
					this.dataLabels.RemoveAt(j);
				}
			}
			base.StartCoroutine(this.SetDelayedAreaShadingChanged());
		}
	}

	private void createOrDeleteAreaShading(int pointValuesCount)
	{
		if (this.areaShadingPrefab == null || this.areaShadingParent == null)
		{
			return;
		}
		if (this.areaShadingType != WMG_Series.areaShadingTypes.None)
		{
			for (int i = 0; i < pointValuesCount - 1; i++)
			{
				if (this.areaShadingRects.Count <= i)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate(this.areaShadingPrefab) as GameObject;
					this.theGraph.changeSpriteParent(gameObject, this.areaShadingParent);
					gameObject.transform.localScale = Vector3.one;
					this.areaShadingRects.Add(gameObject);
					gameObject.name = "Area_Shading_" + this.areaShadingRects.Count;
					base.StartCoroutine(this.SetDelayedAreaShadingChanged());
				}
			}
		}
		int num = pointValuesCount - 1;
		if (this.areaShadingType == WMG_Series.areaShadingTypes.None)
		{
			num = 0;
		}
		for (int j = this.areaShadingRects.Count - 1; j >= 0; j--)
		{
			if (this.areaShadingRects[j] != null && j >= num)
			{
				UnityEngine.Object.DestroyImmediate(this.areaShadingRects[j]);
				this.areaShadingRects.RemoveAt(j);
				base.StartCoroutine(this.SetDelayedAreaShadingChanged());
			}
		}
		Material aMat = this.areaShadingMatSolid;
		if (this.areaShadingType == WMG_Series.areaShadingTypes.Gradient)
		{
			aMat = this.areaShadingMatGradient;
		}
		for (int k = 0; k < this.areaShadingRects.Count; k++)
		{
			this.theGraph.setTextureMaterial(this.areaShadingRects[k], aMat);
			base.StartCoroutine(this.SetDelayedAreaShadingChanged());
		}
	}

	private IEnumerator SetDelayedAreaShadingChanged()
	{
		yield return new WaitForEndOfFrame();
		this.AreaShadingChanged();
		yield return new WaitForEndOfFrame();
		this.AreaShadingChanged();
		yield break;
	}

	public void UpdateSprites()
	{
		List<GameObject> prevPoints = null;
		if (this.theGraph.IsStacked)
		{
			for (int i = 1; i < this.theGraph.lineSeries.Count; i++)
			{
				WMG_Series component = this.theGraph.lineSeries[i].GetComponent<WMG_Series>();
				if (component == this)
				{
					WMG_Series component2 = this.theGraph.lineSeries[i - 1].GetComponent<WMG_Series>();
					prevPoints = component2.getPoints();
				}
			}
		}
		this.updatePointSprites(prevPoints);
		this.updateDataLabels();
		this.updateAreaShading();
	}

	public void updateXdistBetween()
	{
		if (this.AutoUpdateXDistBetween)
		{
			this._xDistBetweenPoints = this.theGraph.getDistBetween(this.points.Count, (this.theGraph.orientationType != WMG_Axis_Graph.orientationTypes.horizontal) ? this.theGraph.xAxisLength : this.theGraph.yAxisLength);
		}
	}

	public void updateExtraXSpace()
	{
		if (this.theGraph.autoUpdateSeriesAxisSpacing)
		{
			if (this.theGraph.graphType == WMG_Axis_Graph.graphTypes.line || this.theGraph.graphType == WMG_Axis_Graph.graphTypes.line_stacked)
			{
				this._extraXSpace = 0f;
			}
			else
			{
				this._extraXSpace = this.xDistBetweenPoints / 2f;
			}
		}
	}

	private void updatePointSprites(List<GameObject> prevPoints)
	{
		if (this.points.Count == 0)
		{
			return;
		}
		float xAxisLength = this.theGraph.xAxisLength;
		float yAxisLength = this.theGraph.yAxisLength;
		float axisMaxValue = this.theGraph.xAxis.AxisMaxValue;
		float axisMaxValue2 = this.theGraph.yAxis.AxisMaxValue;
		float axisMinValue = this.theGraph.xAxis.AxisMinValue;
		float axisMinValue2 = this.theGraph.yAxis.AxisMinValue;
		if (this.theGraph.orientationType == WMG_Axis_Graph.orientationTypes.horizontal)
		{
			this.theGraph.SwapVals<float>(ref xAxisLength, ref yAxisLength);
			this.theGraph.SwapVals<float>(ref axisMaxValue, ref axisMaxValue2);
			this.theGraph.SwapVals<float>(ref axisMinValue, ref axisMinValue2);
		}
		this.updateXdistBetween();
		this.updateExtraXSpace();
		List<Vector2> list = new List<Vector2>();
		List<int> list2 = new List<int>();
		List<int> list3 = new List<int>();
		for (int i = 0; i < this.points.Count; i++)
		{
			if (i >= this.pointValues.Count)
			{
				break;
			}
			float num = 0f;
			float num2 = (this.pointValues[i].y - axisMinValue2) / (axisMaxValue2 - axisMinValue2) * yAxisLength;
			if (!this.theGraph.useGroups && this.UseXDistBetweenToSpace)
			{
				if (i > 0)
				{
					float num3 = list[i - 1].x;
					float num4 = 0f;
					if (this.theGraph.orientationType == WMG_Axis_Graph.orientationTypes.horizontal)
					{
						num3 = list[i - 1].y;
						num4 = this.theGraph.barWidth;
					}
					num = num3 + this.xDistBetweenPoints;
					if (!this.seriesIsLine)
					{
						num += num4;
					}
				}
				else
				{
					num = this.extraXSpace;
				}
			}
			else if (this.theGraph.useGroups)
			{
				num = this.extraXSpace + this.xDistBetweenPoints * (Mathf.Abs(this.pointValues[i].x) - 1f);
			}
			else
			{
				num = (this.pointValues[i].x - axisMinValue) / (axisMaxValue - axisMinValue) * xAxisLength;
			}
			if (this.theGraph.orientationType == WMG_Axis_Graph.orientationTypes.horizontal)
			{
				this.theGraph.SwapVals<float>(ref num, ref num2);
			}
			int num5;
			int num6;
			if (this.seriesIsLine)
			{
				num5 = Mathf.RoundToInt(this.pointWidthHeight);
				num6 = Mathf.RoundToInt(this.pointWidthHeight);
				if (this.theGraph.graphType == WMG_Axis_Graph.graphTypes.line_stacked)
				{
					if (this.theGraph.orientationType == WMG_Axis_Graph.orientationTypes.vertical)
					{
						if (prevPoints != null && i < prevPoints.Count)
						{
							num2 += this.theGraph.getSpritePositionY(prevPoints[i]);
						}
					}
					else if (prevPoints != null && i < prevPoints.Count)
					{
						num += this.theGraph.getSpritePositionX(prevPoints[i]);
					}
				}
			}
			else
			{
				if (this.theGraph.graphType == WMG_Axis_Graph.graphTypes.bar_stacked_percent && this.theGraph.TotalPointValues.Count > i)
				{
					if (this.theGraph.orientationType == WMG_Axis_Graph.orientationTypes.vertical)
					{
						num2 = (this.pointValues[i].y - axisMinValue2) / this.theGraph.TotalPointValues[i] * yAxisLength;
					}
					else
					{
						num = (this.pointValues[i].y - axisMinValue2) / this.theGraph.TotalPointValues[i] * yAxisLength;
					}
				}
				if (this.theGraph.orientationType == WMG_Axis_Graph.orientationTypes.vertical)
				{
					num5 = Mathf.RoundToInt(this.theGraph.barWidth);
					num6 = Mathf.RoundToInt(num2);
					int num7 = 0;
					if (this.theGraph.graphType == WMG_Axis_Graph.graphTypes.bar_side || (this.theGraph.graphType == WMG_Axis_Graph.graphTypes.combo && this.comboType == WMG_Series.comboTypes.bar))
					{
						num7 = Mathf.RoundToInt((this.theGraph.barAxisValue - axisMinValue2) / (axisMaxValue2 - axisMinValue2) * yAxisLength);
					}
					num6 -= num7;
					num2 -= (float)num6;
					this.barIsNegative[i] = false;
					if (num6 < 0)
					{
						num6 *= -1;
						num2 -= (float)num6;
						this.barIsNegative[i] = true;
					}
					if (prevPoints != null && i < prevPoints.Count)
					{
						num2 += this.theGraph.getSpritePositionY(prevPoints[i]) + this.theGraph.getSpriteHeight(prevPoints[i]);
					}
				}
				else
				{
					num5 = Mathf.RoundToInt(num);
					num6 = Mathf.RoundToInt(this.theGraph.barWidth);
					int num8 = 0;
					if (this.theGraph.graphType == WMG_Axis_Graph.graphTypes.bar_side || (this.theGraph.graphType == WMG_Axis_Graph.graphTypes.combo && this.comboType == WMG_Series.comboTypes.bar))
					{
						num8 = Mathf.RoundToInt((this.theGraph.barAxisValue - axisMinValue2) / (axisMaxValue2 - axisMinValue2) * yAxisLength);
					}
					num5 -= num8;
					num = (float)num8;
					num2 -= this.theGraph.barWidth;
					this.barIsNegative[i] = false;
					if (num5 < 0)
					{
						num5 *= -1;
						num -= (float)num5;
						this.barIsNegative[i] = true;
					}
					if (prevPoints != null && i < prevPoints.Count)
					{
						num += this.theGraph.getSpritePositionX(prevPoints[i]) + this.theGraph.getSpriteWidth(prevPoints[i]);
					}
				}
			}
			list2.Add(num5);
			list3.Add(num6);
			list.Add(new Vector2(num, num2));
		}
		if (this.animatingFromPreviousData)
		{
			if (this.seriesIsLine)
			{
				for (int j = 0; j < this.points.Count; j++)
				{
					if (j >= this.pointValues.Count)
					{
						break;
					}
					list[j] = this.theGraph.getChangeSpritePositionTo(this.points[j], list[j]);
				}
			}
			this.afterPositions = new List<Vector2>(list);
			this.afterWidths = new List<int>(list2);
			this.afterHeights = new List<int>(list3);
			this.OnSeriesDataChanged();
			this.animatingFromPreviousData = false;
		}
		else
		{
			for (int k = 0; k < this.points.Count; k++)
			{
				if (k >= this.pointValues.Count)
				{
					break;
				}
				if (!this.seriesIsLine)
				{
					WMG_Node component = this.points[k].GetComponent<WMG_Node>();
					this.theGraph.changeBarWidthHeight(component.objectToColor, list2[k], list3[k]);
				}
				this.theGraph.changeSpritePositionTo(this.points[k], new Vector3(list[k].x, list[k].y, 0f));
			}
			this.RepositionLines();
		}
	}

	private void updateDataLabels()
	{
		if (!this.dataLabelsEnabled)
		{
			return;
		}
		for (int i = 0; i < this.dataLabels.Count; i++)
		{
			Vector2 vector = new Vector2(this.theGraph.getSpritePositionX(this.points[i]), this.theGraph.getSpritePositionY(this.points[i]));
			this.theGraph.changeLabelFontSize(this.dataLabels[i], this.dataLabelsFontSize);
			this.theGraph.changeLabelText(this.dataLabels[i], this.seriesDataLabeler(this, this.pointValues[i].y));
			if (this.seriesIsLine)
			{
				this.theGraph.changeSpritePositionTo(this.dataLabels[i], new Vector3(this.dataLabelsOffset.x + vector.x, this.dataLabelsOffset.y + vector.y, 0f));
			}
			else if (this.theGraph.orientationType == WMG_Axis_Graph.orientationTypes.vertical)
			{
				float y = this.dataLabelsOffset.y + vector.y + this.theGraph.getSpriteHeight(this.points[i]);
				if (this.barIsNegative[i])
				{
					y = -this.dataLabelsOffset.y - this.theGraph.getSpriteHeight(this.points[i]) + (float)Mathf.RoundToInt((this.theGraph.barAxisValue - this.theGraph.yAxis.AxisMinValue) / (this.theGraph.yAxis.AxisMaxValue - this.theGraph.yAxis.AxisMinValue) * this.theGraph.yAxisLength);
				}
				this.theGraph.changeSpritePositionTo(this.dataLabels[i], new Vector3(this.dataLabelsOffset.x + vector.x + this.theGraph.barWidth / 2f, y, 0f));
			}
			else
			{
				float x = this.dataLabelsOffset.x + vector.x + this.theGraph.getSpriteWidth(this.points[i]);
				if (this.barIsNegative[i])
				{
					x = -this.dataLabelsOffset.x - this.theGraph.getSpriteWidth(this.points[i]) + (float)Mathf.RoundToInt((this.theGraph.barAxisValue - this.theGraph.xAxis.AxisMinValue) / (this.theGraph.xAxis.AxisMaxValue - this.theGraph.xAxis.AxisMinValue) * this.theGraph.xAxisLength);
				}
				this.theGraph.changeSpritePositionTo(this.dataLabels[i], new Vector3(x, this.dataLabelsOffset.y + vector.y + this.theGraph.barWidth / 2f, 0f));
			}
		}
	}

	public void updateAreaShading()
	{
		if (this.areaShadingType == WMG_Series.areaShadingTypes.None)
		{
			return;
		}
		float num = float.NegativeInfinity;
		for (int i = 0; i < this.points.Count; i++)
		{
			if (i >= this.pointValues.Count)
			{
				break;
			}
			if (this.pointValues[i].y > num)
			{
				num = this.pointValues[i].y;
			}
		}
		for (int j = 0; j < this.points.Count - 1; j++)
		{
			if (j >= this.pointValues.Count)
			{
				break;
			}
			int num2 = 180;
			Vector2 vector = new Vector2(this.theGraph.getSpritePositionX(this.points[j]), this.theGraph.getSpritePositionY(this.points[j]));
			Vector2 vector2 = new Vector2(this.theGraph.getSpritePositionX(this.points[j + 1]), this.theGraph.getSpritePositionY(this.points[j + 1]));
			float num3 = this.theGraph.yAxisLength / (this.theGraph.yAxis.AxisMaxValue - this.theGraph.yAxis.AxisMinValue);
			float num4 = (this.areaShadingAxisValue - this.theGraph.yAxis.AxisMinValue) * num3;
			if (this.theGraph.orientationType == WMG_Axis_Graph.orientationTypes.horizontal)
			{
				num2 = 90;
				vector = new Vector2(this.theGraph.getSpritePositionY(this.points[j]), this.theGraph.getSpritePositionX(this.points[j]));
				vector2 = new Vector2(this.theGraph.getSpritePositionY(this.points[j + 1]), this.theGraph.getSpritePositionX(this.points[j + 1]));
				num3 = this.theGraph.xAxisLength / (this.theGraph.xAxis.AxisMaxValue - this.theGraph.xAxis.AxisMinValue);
				num4 = (this.areaShadingAxisValue - this.theGraph.xAxis.AxisMinValue) * num3;
			}
			this.areaShadingRects[j].transform.localEulerAngles = new Vector3(0f, 0f, (float)num2);
			float num5 = Mathf.Max(vector2.y, vector.y);
			float num6 = Mathf.Min(vector2.y, vector.y);
			int num7 = Mathf.RoundToInt(vector.x);
			int num8 = Mathf.RoundToInt(vector2.x - vector.x);
			float num9 = num5 - num6 + (Mathf.Min(this.pointValues[j + 1].y, this.pointValues[j].y) - this.areaShadingAxisValue) * num3;
			if (num6 < num4)
			{
				float num10 = (vector2.y - vector.y) / (vector2.x - vector.x);
				if (vector2.y > vector.y)
				{
					float num11 = num4 - num6;
					int num12 = Mathf.RoundToInt(num11 / num10);
					num8 -= num12;
					num7 += num12;
				}
				else
				{
					float num13 = num4 - num6;
					int num14 = Mathf.RoundToInt(num13 / num10 * -1f);
					num8 -= num14;
				}
			}
			if (this.theGraph.orientationType == WMG_Axis_Graph.orientationTypes.horizontal)
			{
				this.theGraph.changeSpritePositionTo(this.areaShadingRects[j], new Vector3(num5, (float)(num7 + num8), 0f));
			}
			else
			{
				this.theGraph.changeSpritePositionTo(this.areaShadingRects[j], new Vector3((float)num7, num5, 0f));
			}
			this.theGraph.changeSpriteSizeFloat(this.areaShadingRects[j], (float)num8, num9);
			if (j > 0)
			{
				if (this.theGraph.orientationType == WMG_Axis_Graph.orientationTypes.horizontal)
				{
					int num15 = Mathf.RoundToInt(this.theGraph.getSpritePositionY(this.areaShadingRects[j])) - Mathf.RoundToInt(this.theGraph.getSpriteWidth(this.areaShadingRects[j]));
					int num16 = Mathf.RoundToInt(this.theGraph.getSpritePositionY(this.areaShadingRects[j - 1]));
					if (num15 > num16)
					{
						this.theGraph.changeSpriteWidth(this.areaShadingRects[j], Mathf.RoundToInt(this.theGraph.getSpriteWidth(this.areaShadingRects[j]) + 1f));
					}
					if (num15 < num16)
					{
						this.theGraph.changeSpriteWidth(this.areaShadingRects[j], Mathf.RoundToInt(this.theGraph.getSpriteWidth(this.areaShadingRects[j]) - 1f));
					}
				}
				else
				{
					int num17 = Mathf.RoundToInt(this.theGraph.getSpriteWidth(this.areaShadingRects[j - 1])) + Mathf.RoundToInt(this.theGraph.getSpritePositionX(this.areaShadingRects[j - 1]));
					if (num17 > Mathf.RoundToInt(this.theGraph.getSpritePositionX(this.areaShadingRects[j])))
					{
						this.theGraph.changeSpriteWidth(this.areaShadingRects[j - 1], Mathf.RoundToInt(this.theGraph.getSpriteWidth(this.areaShadingRects[j - 1]) - 1f));
					}
					if (num17 < Mathf.RoundToInt(this.theGraph.getSpritePositionX(this.areaShadingRects[j])))
					{
						this.theGraph.changeSpriteWidth(this.areaShadingRects[j - 1], Mathf.RoundToInt(this.theGraph.getSpriteWidth(this.areaShadingRects[j - 1]) + 1f));
					}
				}
			}
			Material textureMaterial = this.theGraph.getTextureMaterial(this.areaShadingRects[j]);
			if (!(textureMaterial == null))
			{
				if (this.theGraph.orientationType == WMG_Axis_Graph.orientationTypes.horizontal)
				{
					textureMaterial.SetFloat("_Slope", -(vector2.y - vector.y) / num9);
				}
				else
				{
					textureMaterial.SetFloat("_Slope", (vector2.y - vector.y) / num9);
				}
				textureMaterial.SetColor("_Color", this.areaShadingColor);
				textureMaterial.SetFloat("_Transparency", 1f - this.areaShadingColor.a);
				textureMaterial.SetFloat("_GradientScale", (Mathf.Max(this.pointValues[j + 1].y, this.pointValues[j].y) - this.areaShadingAxisValue) / (num - this.areaShadingAxisValue));
			}
		}
	}

	public void StartRealTimeUpdate()
	{
		if (this.realTimeRunning)
		{
			return;
		}
		if (this.realTimeDataSource != null)
		{
			this.realTimeRunning = true;
			this.pointValues.SetListNoCb(new List<Vector2>(), ref this._pointValues);
			this.pointValues.AddNoCb(new Vector2(0f, this.realTimeDataSource.getDatum<float>()), ref this._pointValues);
			this.realTimeLoopVar = 0f;
			if (this.theGraph.orientationType == WMG_Axis_Graph.orientationTypes.vertical)
			{
				this.realTimeOrigMax = this.theGraph.xAxis.AxisMaxValue;
			}
			else
			{
				this.realTimeOrigMax = this.theGraph.yAxis.AxisMaxValue;
			}
		}
	}

	public void StopRealTimeUpdate()
	{
		this.realTimeRunning = false;
	}

	public void ResumeRealTimeUpdate()
	{
		this.realTimeRunning = true;
	}

	private void DoRealTimeUpdate()
	{
		float num = 0.0166f;
		this.realTimeLoopVar += num;
		float datum = this.realTimeDataSource.getDatum<float>();
		int num2 = 2;
		if (this.pointValues.Count >= 2)
		{
			float num3 = 0.3f;
			float num4 = (this.theGraph.yAxis.AxisMaxValue - this.theGraph.yAxis.AxisMinValue) / (this.theGraph.xAxis.AxisMaxValue - this.theGraph.xAxis.AxisMinValue);
			float[] array = new float[num2];
			Vector2 vector = new Vector2(this.realTimeLoopVar, datum);
			for (int i = 0; i < array.Length; i++)
			{
				Vector2 vector2 = this.pointValues[this.pointValues.Count - (i + 1)];
				array[i] = (vector.y - vector2.y) / (vector.x - vector2.x) / num4;
			}
			if (Mathf.Abs(array[0] - array[1]) <= num3)
			{
				this.pointValues[this.pointValues.Count - 1] = new Vector2(this.realTimeLoopVar, datum);
			}
			else
			{
				this.pointValues.Add(new Vector2(this.realTimeLoopVar, datum));
			}
		}
		else
		{
			this.pointValues.Add(new Vector2(this.realTimeLoopVar, datum));
		}
		if (this.pointValues.Count > 1 && this.pointValues[this.pointValues.Count - 1].x > this.realTimeOrigMax)
		{
			if (this.theGraph.orientationType == WMG_Axis_Graph.orientationTypes.vertical)
			{
				this.theGraph.xAxis.AxisMinValue = this.realTimeLoopVar - this.realTimeOrigMax;
				this.theGraph.xAxis.AxisMaxValue = this.realTimeLoopVar;
			}
			else
			{
				this.theGraph.yAxis.AxisMinValue = this.realTimeLoopVar - this.realTimeOrigMax;
				this.theGraph.yAxis.AxisMaxValue = this.realTimeLoopVar;
			}
			float x = this.pointValues[0].x;
			float x2 = this.pointValues[1].x;
			float y = this.pointValues[0].y;
			float y2 = this.pointValues[1].y;
			if (Mathf.Approximately(x + num, x2))
			{
				this.pointValues.RemoveAt(0);
			}
			else
			{
				this.pointValues[0] = new Vector2(x + num, y + (y2 - y) / (x2 - x) * num);
			}
		}
	}

	public void deleteAllNodesFromGraphManager()
	{
		for (int i = this.points.Count - 1; i >= 0; i--)
		{
			this.theGraph.DeleteNode(this.points[i].GetComponent<WMG_Node>());
		}
		this.theGraph.DeleteNode(this.legendEntry.nodeLeft.GetComponent<WMG_Node>());
		this.theGraph.DeleteNode(this.legendEntry.nodeRight.GetComponent<WMG_Node>());
		this.theGraph.DeleteNode(this.legendEntry.swatchNode.GetComponent<WMG_Node>());
	}

	[SerializeField]
	private List<Vector2> _pointValues;

	public WMG_List<Vector2> pointValues = new WMG_List<Vector2>();

	[SerializeField]
	private List<Color> _pointColors;

	public WMG_List<Color> pointColors = new WMG_List<Color>();

	public UnityEngine.Object dataLabelPrefab;

	public GameObject dataLabelsParent;

	public Material areaShadingMatSolid;

	public Material areaShadingMatGradient;

	public GameObject areaShadingParent;

	public UnityEngine.Object areaShadingPrefab;

	public WMG_Axis_Graph theGraph;

	public WMG_Data_Source realTimeDataSource;

	public WMG_Data_Source pointValuesDataSource;

	public UnityEngine.Object legendEntryPrefab;

	public GameObject linkParent;

	public GameObject nodeParent;

	public WMG_Legend_Entry legendEntry;

	[SerializeField]
	private WMG_Series.comboTypes _comboType;

	[SerializeField]
	private string _seriesName;

	[SerializeField]
	private float _pointWidthHeight;

	[SerializeField]
	private float _lineScale;

	[SerializeField]
	private Color _pointColor;

	[SerializeField]
	private bool _usePointColors;

	[SerializeField]
	private Color _lineColor;

	[SerializeField]
	private bool _UseXDistBetweenToSpace;

	[SerializeField]
	private bool _AutoUpdateXDistBetween;

	[SerializeField]
	private float _xDistBetweenPoints;

	[SerializeField]
	private float _extraXSpace;

	[SerializeField]
	private bool _hidePoints;

	[SerializeField]
	private bool _hideLines;

	[SerializeField]
	private bool _connectFirstToLast;

	[SerializeField]
	private float _linePadding;

	[SerializeField]
	private bool _dataLabelsEnabled;

	[SerializeField]
	private int _dataLabelsNumDecimals;

	[SerializeField]
	private int _dataLabelsFontSize;

	[SerializeField]
	private Vector2 _dataLabelsOffset;

	[SerializeField]
	private WMG_Series.areaShadingTypes _areaShadingType;

	[SerializeField]
	private Color _areaShadingColor;

	[SerializeField]
	private float _areaShadingAxisValue;

	[SerializeField]
	private int _pointPrefab;

	[SerializeField]
	private int _linkPrefab;

	private UnityEngine.Object nodePrefab;

	private List<GameObject> points = new List<GameObject>();

	private List<GameObject> lines = new List<GameObject>();

	private List<GameObject> areaShadingRects = new List<GameObject>();

	private List<GameObject> dataLabels = new List<GameObject>();

	private List<bool> barIsNegative = new List<bool>();

	private List<int> changedValIndices = new List<int>();

	private WMG_Axis_Graph.graphTypes cachedSeriesType;

	private bool realTimeRunning;

	private float realTimeLoopVar;

	private float realTimeOrigMax;

	private bool animatingFromPreviousData;

	private List<Vector2> afterPositions = new List<Vector2>();

	private List<int> afterWidths = new List<int>();

	private List<int> afterHeights = new List<int>();

	private List<WMG_Change_Obj> changeObjs = new List<WMG_Change_Obj>();

	public WMG_Change_Obj pointValuesC = new WMG_Change_Obj();

	public WMG_Change_Obj pointValuesCountC = new WMG_Change_Obj();

	private WMG_Change_Obj pointValuesValC = new WMG_Change_Obj();

	private WMG_Change_Obj lineScaleC = new WMG_Change_Obj();

	private WMG_Change_Obj pointWidthHeightC = new WMG_Change_Obj();

	private WMG_Change_Obj dataLabelsC = new WMG_Change_Obj();

	private WMG_Change_Obj lineColorC = new WMG_Change_Obj();

	private WMG_Change_Obj pointColorC = new WMG_Change_Obj();

	private WMG_Change_Obj hideLineC = new WMG_Change_Obj();

	private WMG_Change_Obj hidePointC = new WMG_Change_Obj();

	private WMG_Change_Obj seriesNameC = new WMG_Change_Obj();

	private WMG_Change_Obj linePaddingC = new WMG_Change_Obj();

	private WMG_Change_Obj areaShadingTypeC = new WMG_Change_Obj();

	private WMG_Change_Obj areaShadingC = new WMG_Change_Obj();

	public WMG_Change_Obj prefabC = new WMG_Change_Obj();

	private WMG_Change_Obj connectFirstToLastC = new WMG_Change_Obj();

	private bool hasInit;

	public WMG_Series.SeriesDataLabeler seriesDataLabeler;

	public enum comboTypes
	{
		line,
		bar
	}

	public enum areaShadingTypes
	{
		None,
		Solid,
		Gradient
	}

	public delegate string SeriesDataLabeler(WMG_Series series, float val);

	public delegate void SeriesDataChangedHandler(WMG_Series aSeries);
}
