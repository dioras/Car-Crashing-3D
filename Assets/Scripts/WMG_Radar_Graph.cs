using System;
using System.Collections.Generic;
using UnityEngine;

public class WMG_Radar_Graph : WMG_Axis_Graph
{
	public int numPoints
	{
		get
		{
			return this._numPoints;
		}
		set
		{
			if (this._numPoints != value)
			{
				this._numPoints = value;
				this.radarGraphC.Changed();
			}
		}
	}

	public Vector2 offset
	{
		get
		{
			return this._offset;
		}
		set
		{
			if (this._offset != value)
			{
				this._offset = value;
				this.radarGraphC.Changed();
			}
		}
	}

	public float degreeOffset
	{
		get
		{
			return this._degreeOffset;
		}
		set
		{
			if (this._degreeOffset != value)
			{
				this._degreeOffset = value;
				this.radarGraphC.Changed();
			}
		}
	}

	public float radarMinVal
	{
		get
		{
			return this._radarMinVal;
		}
		set
		{
			if (this._radarMinVal != value)
			{
				this._radarMinVal = value;
				this.radarGraphC.Changed();
			}
		}
	}

	public float radarMaxVal
	{
		get
		{
			return this._radarMaxVal;
		}
		set
		{
			if (this._radarMaxVal != value)
			{
				this._radarMaxVal = value;
				this.radarGraphC.Changed();
			}
		}
	}

	public int numGrids
	{
		get
		{
			return this._numGrids;
		}
		set
		{
			if (this._numGrids != value)
			{
				this._numGrids = value;
				this.gridsC.Changed();
			}
		}
	}

	public float gridLineWidth
	{
		get
		{
			return this._gridLineWidth;
		}
		set
		{
			if (this._gridLineWidth != value)
			{
				this._gridLineWidth = value;
				this.gridsC.Changed();
			}
		}
	}

	public Color gridColor
	{
		get
		{
			return this._gridColor;
		}
		set
		{
			if (this._gridColor != value)
			{
				this._gridColor = value;
				this.gridsC.Changed();
			}
		}
	}

	public int numDataSeries
	{
		get
		{
			return this._numDataSeries;
		}
		set
		{
			if (this._numDataSeries != value)
			{
				this._numDataSeries = value;
				this.dataSeriesC.Changed();
			}
		}
	}

	public float dataSeriesLineWidth
	{
		get
		{
			return this._dataSeriesLineWidth;
		}
		set
		{
			if (this._dataSeriesLineWidth != value)
			{
				this._dataSeriesLineWidth = value;
				this.dataSeriesC.Changed();
			}
		}
	}

	public Color labelsColor
	{
		get
		{
			return this._labelsColor;
		}
		set
		{
			if (this._labelsColor != value)
			{
				this._labelsColor = value;
				this.labelsC.Changed();
			}
		}
	}

	public float labelsOffset
	{
		get
		{
			return this._labelsOffset;
		}
		set
		{
			if (this._labelsOffset != value)
			{
				this._labelsOffset = value;
				this.labelsC.Changed();
			}
		}
	}

	public int fontSize
	{
		get
		{
			return this._fontSize;
		}
		set
		{
			if (this._fontSize != value)
			{
				this._fontSize = value;
				this.labelsC.Changed();
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
				this.labelsC.Changed();
			}
		}
	}

	private new void Start()
	{
		this.Init();
		this.PauseCallbacks();
		this.radarGraphC.Changed();
	}

	public new void Init()
	{
		if (this.hasInit2)
		{
			return;
		}
		this.hasInit2 = true;
		this.changeObjs.Add(this.radarGraphC);
		this.changeObjs.Add(this.gridsC);
		this.changeObjs.Add(this.labelsC);
		this.changeObjs.Add(this.dataSeriesC);
		this.dataSeriesColors.SetList(this._dataSeriesColors);
		this.dataSeriesColors.Changed += this.dataSeriesColorsChanged;
		this.labelStrings.SetList(this._labelStrings);
		this.labelStrings.Changed += this.labelStringsChanged;
		this.radarGraphC.OnChange += this.GraphChanged;
		this.gridsC.OnChange += this.GridsChanged;
		this.labelsC.OnChange += this.LabelsChanged;
		this.dataSeriesC.OnChange += this.DataSeriesChanged;
		this.PauseCallbacks();
	}

	private void Update()
	{
		this.Refresh();
	}

	public new void Refresh()
	{
		this.ResumeCallbacks();
		this.PauseCallbacks();
	}

	private void PauseCallbacks()
	{
		for (int i = 0; i < this.changeObjs.Count; i++)
		{
			this.changeObjs[i].changesPaused = true;
			this.changeObjs[i].changePaused = false;
		}
	}

	private void ResumeCallbacks()
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

	public void dataSeriesColorsChanged(bool editorChange, bool countChanged, bool oneValChanged, int index)
	{
		WMG_Util.listChanged<Color>(editorChange, ref this.dataSeriesColors, ref this._dataSeriesColors, oneValChanged, index);
		this.dataSeriesC.Changed();
	}

	public void labelStringsChanged(bool editorChange, bool countChanged, bool oneValChanged, int index)
	{
		WMG_Util.listChanged<string>(editorChange, ref this.labelStrings, ref this._labelStrings, oneValChanged, index);
		this.labelsC.Changed();
	}

	public void GridsChanged()
	{
		this.updateGrids();
	}

	public void DataSeriesChanged()
	{
		this.updateDataSeries();
	}

	public void LabelsChanged()
	{
		this.updateLabels();
	}

	public new void GraphChanged()
	{
		this.updateGrids();
		this.updateDataSeries();
		this.updateLabels();
	}

	private void updateLabels()
	{
		if (!this.createdLabels)
		{
			WMG_Series wmg_Series = base.addSeriesAt(this.numDataSeries + this.numGrids);
			wmg_Series.hideLines = true;
			this.createdLabels = true;
			wmg_Series.pointPrefab = 3;
			this.radarLabels = wmg_Series;
		}
		for (int i = 0; i < this.numPoints; i++)
		{
			if (this.labelStrings.Count <= i)
			{
				this.labelStrings.AddNoCb(string.Empty, ref this._labelStrings);
			}
		}
		for (int j = this.labelStrings.Count - 1; j >= 0; j--)
		{
			if (this.labelStrings[j] != null && j >= this.numPoints)
			{
				this.labelStrings.RemoveAtNoCb(j, ref this._labelStrings);
			}
		}
		this.radarLabels.hidePoints = this.hideLabels;
		this.radarLabels.pointValues.SetList(base.GenCircular2(this.numPoints, this.offset.x, this.offset.y, this.labelsOffset + (this.radarMaxVal - this.radarMinVal), this.degreeOffset));
		List<GameObject> points = this.radarLabels.getPoints();
		for (int k = 0; k < points.Count; k++)
		{
			if (k >= this.numPoints)
			{
				break;
			}
			base.changeLabelFontSize(points[k], this.fontSize);
			base.changeLabelText(points[k], this.labelStrings[k]);
		}
		this.radarLabels.pointColor = this.labelsColor;
	}

	private void updateDataSeries()
	{
		for (int i = 0; i < this.numDataSeries; i++)
		{
			if (this.dataSeries.Count <= i)
			{
				WMG_Series wmg_Series = base.addSeriesAt(this.numGrids + i);
				wmg_Series.connectFirstToLast = true;
				wmg_Series.hidePoints = true;
				this.dataSeries.Add(wmg_Series);
			}
			if (this.dataSeriesColors.Count <= i)
			{
				this.dataSeriesColors.AddNoCb(new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f), ref this._dataSeriesColors);
			}
		}
		for (int j = this.dataSeries.Count - 1; j >= 0; j--)
		{
			if (this.dataSeries[j] != null && j >= this.numDataSeries)
			{
				base.deleteSeriesAt(this.numGrids + j);
				this.dataSeries.RemoveAt(j);
			}
		}
		for (int k = this.dataSeriesColors.Count - 1; k >= 0; k--)
		{
			if (k >= this.numDataSeries)
			{
				this.dataSeriesColors.RemoveAtNoCb(k, ref this._dataSeriesColors);
			}
		}
		for (int l = 0; l < this.numDataSeries; l++)
		{
			WMG_Series component = this.lineSeries[l + this.numGrids].GetComponent<WMG_Series>();
			if (this.randomData)
			{
				component.pointValues.SetList(base.GenRadar(base.GenRandomList(this.numPoints, this.radarMinVal, this.radarMaxVal), this.offset.x, this.offset.y, this.degreeOffset));
			}
			component.lineScale = this.dataSeriesLineWidth;
			component.linePadding = this.dataSeriesLineWidth;
			component.lineColor = this.dataSeriesColors[l];
		}
	}

	private void updateGrids()
	{
		for (int i = 0; i < this.numGrids; i++)
		{
			if (this.grids.Count <= i)
			{
				WMG_Series wmg_Series = base.addSeriesAt(i);
				wmg_Series.connectFirstToLast = true;
				wmg_Series.hidePoints = true;
				this.grids.Add(wmg_Series);
			}
		}
		for (int j = this.grids.Count - 1; j >= 0; j--)
		{
			if (this.grids[j] != null && j >= this.numGrids)
			{
				base.deleteSeriesAt(j);
				this.grids.RemoveAt(j);
			}
		}
		for (int k = 0; k < this.numGrids; k++)
		{
			WMG_Series component = this.lineSeries[k].GetComponent<WMG_Series>();
			component.pointValues.SetList(base.GenCircular2(this.numPoints, this.offset.x, this.offset.y, ((float)k + 1f) / (float)this.numGrids * (this.radarMaxVal - this.radarMinVal), this.degreeOffset));
			component.lineScale = this.gridLineWidth;
			component.linePadding = this.gridLineWidth;
			component.lineColor = this.gridColor;
		}
	}

	[SerializeField]
	private List<Color> _dataSeriesColors;

	public WMG_List<Color> dataSeriesColors = new WMG_List<Color>();

	[SerializeField]
	private List<string> _labelStrings;

	public WMG_List<string> labelStrings = new WMG_List<string>();

	public bool randomData;

	[SerializeField]
	private int _numPoints;

	[SerializeField]
	private Vector2 _offset;

	[SerializeField]
	private float _degreeOffset;

	[SerializeField]
	private float _radarMinVal;

	[SerializeField]
	private float _radarMaxVal;

	[SerializeField]
	private int _numGrids;

	[SerializeField]
	private float _gridLineWidth;

	[SerializeField]
	private Color _gridColor;

	[SerializeField]
	private int _numDataSeries;

	[SerializeField]
	private float _dataSeriesLineWidth;

	[SerializeField]
	private Color _labelsColor;

	[SerializeField]
	private float _labelsOffset;

	[SerializeField]
	private int _fontSize;

	[SerializeField]
	private bool _hideLabels;

	public List<WMG_Series> grids;

	public List<WMG_Series> dataSeries;

	public WMG_Series radarLabels;

	private bool createdLabels;

	private List<WMG_Change_Obj> changeObjs = new List<WMG_Change_Obj>();

	private WMG_Change_Obj radarGraphC = new WMG_Change_Obj();

	private WMG_Change_Obj gridsC = new WMG_Change_Obj();

	private WMG_Change_Obj labelsC = new WMG_Change_Obj();

	private WMG_Change_Obj dataSeriesC = new WMG_Change_Obj();

	private bool hasInit2;
}
