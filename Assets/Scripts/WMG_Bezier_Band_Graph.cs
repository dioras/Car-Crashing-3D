using System;
using System.Collections.Generic;
using UnityEngine;

public class WMG_Bezier_Band_Graph : WMG_Graph_Manager
{
	public Color bandLineColor
	{
		get
		{
			return this._bandLineColor;
		}
		set
		{
			if (this._bandLineColor != value)
			{
				this._bandLineColor = value;
				this.colorsC.Changed();
			}
		}
	}

	public float startHeightPercent
	{
		get
		{
			return this._startHeightPercent;
		}
		set
		{
			if (this._startHeightPercent != value)
			{
				this._startHeightPercent = value;
				this.cubicBezierC.Changed();
			}
		}
	}

	public int bandSpacing
	{
		get
		{
			return this._bandSpacing;
		}
		set
		{
			if (this._bandSpacing != value)
			{
				this._bandSpacing = value;
				this.cubicBezierC.Changed();
			}
		}
	}

	public int bandLineWidth
	{
		get
		{
			return this._bandLineWidth;
		}
		set
		{
			if (this._bandLineWidth != value)
			{
				this._bandLineWidth = value;
				this.cubicBezierC.Changed();
			}
		}
	}

	public Vector2 cubicBezierP1
	{
		get
		{
			return this._cubicBezierP1;
		}
		set
		{
			if (this._cubicBezierP1 != value)
			{
				this._cubicBezierP1 = value;
				this.cubicBezierC.Changed();
			}
		}
	}

	public Vector2 cubicBezierP2
	{
		get
		{
			return this._cubicBezierP2;
		}
		set
		{
			if (this._cubicBezierP2 != value)
			{
				this._cubicBezierP2 = value;
				this.cubicBezierC.Changed();
			}
		}
	}

	public int numDecimals
	{
		get
		{
			return this._numDecimals;
		}
		set
		{
			if (this._numDecimals != value)
			{
				this._numDecimals = value;
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
				this.fontC.Changed();
			}
		}
	}

	public List<WMG_Bezier_Band> bands { get; private set; }

	public float TotalVal { get; private set; }

	private void Start()
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
		this.changeObjs.Add(this.allC);
		this.changeObjs.Add(this.cubicBezierC);
		this.changeObjs.Add(this.labelsC);
		this.changeObjs.Add(this.colorsC);
		this.changeObjs.Add(this.fontC);
		this.bands = new List<WMG_Bezier_Band>();
		this.values.SetList(this._values);
		this.values.Changed += this.valuesChanged;
		this.fillColors.SetList(this._fillColors);
		this.fillColors.Changed += this.fillColorsChanged;
		this.labels.SetList(this._labels);
		this.labels.Changed += this.labelsChanged;
		this.allC.OnChange += this.AllChanged;
		this.cubicBezierC.OnChange += this.CubicBezierChanged;
		this.labelsC.OnChange += this.UpdateLabels;
		this.colorsC.OnChange += this.UpdateColors;
		this.fontC.OnChange += this.UpdateFontSize;
		this.allC.Changed();
	}

	private void Update()
	{
		this.Refresh();
	}

	public void Refresh()
	{
		this.ResumeCallbacks();
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

	public void valuesChanged(bool editorChange, bool countChanged, bool oneValChanged, int index)
	{
		WMG_Util.listChanged<float>(editorChange, ref this.values, ref this._values, oneValChanged, index);
		this.allC.Changed();
	}

	public void fillColorsChanged(bool editorChange, bool countChanged, bool oneValChanged, int index)
	{
		WMG_Util.listChanged<Color>(editorChange, ref this.fillColors, ref this._fillColors, oneValChanged, index);
		this.colorsC.Changed();
	}

	public void labelsChanged(bool editorChange, bool countChanged, bool oneValChanged, int index)
	{
		WMG_Util.listChanged<string>(editorChange, ref this.labels, ref this._labels, oneValChanged, index);
		this.labelsC.Changed();
	}

	private void AllChanged()
	{
		this.CreateOrDeleteBasedOnValues();
		this.UpdateColors();
		this.UpdateBands();
		this.UpdateLabelPositions();
		this.UpdateLabels();
	}

	public void CubicBezierChanged()
	{
		this.UpdateBands();
	}

	private void CreateOrDeleteBasedOnValues()
	{
		for (int i = 0; i < this.values.Count; i++)
		{
			if (this.fillColors.Count <= i)
			{
				this.fillColors.AddNoCb(Color.white, ref this._fillColors);
			}
			if (this.labels.Count <= i)
			{
				this.labels.AddNoCb(string.Empty, ref this._labels);
			}
			if (this.bands.Count <= i)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(this.bandPrefab) as GameObject;
				base.changeSpriteParent(gameObject, this.bandsParent);
				WMG_Bezier_Band component = gameObject.GetComponent<WMG_Bezier_Band>();
				if (component == null)
				{
					UnityEngine.Debug.Log(gameObject.name);
					UnityEngine.Debug.Log("asdf");
				}
				component.Init(this);
				this.bands.Add(component);
			}
		}
		for (int j = this.bands.Count - 1; j >= 0; j--)
		{
			if (this.bands[j] != null && j >= this.values.Count)
			{
				UnityEngine.Object.Destroy(this.bands[j].gameObject);
				this.bands.RemoveAt(j);
			}
		}
	}

	private void UpdateColors()
	{
		this.UpdateBandFillColors();
		this.UpdateBandLineColors();
	}

	private void UpdateBandFillColors()
	{
		for (int i = 0; i < this.values.Count; i++)
		{
			this.bands[i].setFillColor(this.fillColors[i]);
		}
	}

	private void UpdateBandLineColors()
	{
		for (int i = 0; i < this.values.Count; i++)
		{
			this.bands[i].setLineColor(this.bandLineColor);
		}
	}

	private void UpdateBands()
	{
		this.TotalVal = 0f;
		for (int i = 0; i < this.values.Count; i++)
		{
			this.TotalVal += this.values[i];
		}
		float num = 0f;
		float num2 = 0f;
		for (int j = 0; j < this.values.Count; j++)
		{
			num += this.values[j];
			this.bands[j].setCumulativePercents(num, num2);
			num2 += this.values[j];
			this.bands[j].UpdateBand();
		}
	}

	private void UpdateLabelPositions()
	{
		for (int i = 0; i < this.values.Count; i++)
		{
			base.setAnchor(this.bands[i].labelParent, new Vector2(1f, 1f - this.bands[i].cumulativePercent + (this.bands[i].cumulativePercent - this.bands[i].prevCumulativePercent) / 2f), new Vector2(0.5f, 0.5f), Vector2.zero);
		}
	}

	private void UpdateLabels()
	{
		for (int i = 0; i < this.values.Count; i++)
		{
			base.changeLabelText(this.bands[i].percentLabel, base.getLabelText(string.Empty, WMG_Enums.labelTypes.Percents_Only, 0f, this.bands[i].cumulativePercent - this.bands[i].prevCumulativePercent, this.numDecimals));
			base.changeLabelText(this.bands[i].label, this.labels[i]);
		}
	}

	private void UpdateFontSize()
	{
		for (int i = 0; i < this.values.Count; i++)
		{
			base.changeLabelFontSize(this.bands[i].percentLabel, this.fontSize);
			base.changeLabelFontSize(this.bands[i].label, this.fontSize);
		}
	}

	[SerializeField]
	private List<float> _values;

	public WMG_List<float> values = new WMG_List<float>();

	[SerializeField]
	private List<string> _labels;

	public WMG_List<string> labels = new WMG_List<string>();

	[SerializeField]
	private List<Color> _fillColors;

	public WMG_List<Color> fillColors = new WMG_List<Color>();

	public GameObject bandsParent;

	public UnityEngine.Object bandPrefab;

	[SerializeField]
	private Color _bandLineColor;

	[SerializeField]
	private float _startHeightPercent;

	[SerializeField]
	private int _bandSpacing;

	[SerializeField]
	private int _bandLineWidth;

	[SerializeField]
	private Vector2 _cubicBezierP1;

	[SerializeField]
	private Vector2 _cubicBezierP2;

	[SerializeField]
	private int _numDecimals;

	[SerializeField]
	private int _fontSize;

	private List<WMG_Change_Obj> changeObjs = new List<WMG_Change_Obj>();

	private WMG_Change_Obj allC = new WMG_Change_Obj();

	private WMG_Change_Obj cubicBezierC = new WMG_Change_Obj();

	private WMG_Change_Obj labelsC = new WMG_Change_Obj();

	private WMG_Change_Obj colorsC = new WMG_Change_Obj();

	private WMG_Change_Obj fontC = new WMG_Change_Obj();

	private bool hasInit;
}
