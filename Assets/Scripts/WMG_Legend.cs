using System;
using System.Collections.Generic;
using UnityEngine;

public class WMG_Legend : WMG_GUI_Functions
{
	public bool hideLegend
	{
		get
		{
			return this._hideLegend;
		}
		set
		{
			if (this._hideLegend != value)
			{
				this._hideLegend = value;
				this.setGraphCallback();
				this.legendC.Changed();
			}
		}
	}

	public WMG_Legend.legendTypes legendType
	{
		get
		{
			return this._legendType;
		}
		set
		{
			if (this._legendType != value)
			{
				this._legendType = value;
				this.setGraphCallback();
				this.legendC.Changed();
			}
		}
	}

	public WMG_Enums.labelTypes labelType
	{
		get
		{
			return this._labelType;
		}
		set
		{
			if (this._labelType != value)
			{
				this._labelType = value;
				this.setGraphCallback();
				this.legendC.Changed();
			}
		}
	}

	public bool showBackground
	{
		get
		{
			return this._showBackground;
		}
		set
		{
			if (this._showBackground != value)
			{
				this._showBackground = value;
				this.legendC.Changed();
			}
		}
	}

	public bool oppositeSideLegend
	{
		get
		{
			return this._oppositeSideLegend;
		}
		set
		{
			if (this._oppositeSideLegend != value)
			{
				this._oppositeSideLegend = value;
				this.setGraphCallback();
				this.legendC.Changed();
			}
		}
	}

	public float offset
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
				this.setGraphCallback();
				this.legendC.Changed();
			}
		}
	}

	public float legendEntryWidth
	{
		get
		{
			return this._legendEntryWidth;
		}
		set
		{
			if (this._legendEntryWidth != value)
			{
				this._legendEntryWidth = value;
				this.setGraphCallback();
				this.legendC.Changed();
			}
		}
	}

	public bool setWidthFromLabels
	{
		get
		{
			return this._setWidthFromLabels;
		}
		set
		{
			if (this._setWidthFromLabels != value)
			{
				this._setWidthFromLabels = value;
				this.legendC.Changed();
			}
		}
	}

	public float legendEntryHeight
	{
		get
		{
			return this._legendEntryHeight;
		}
		set
		{
			if (this._legendEntryHeight != value)
			{
				this._legendEntryHeight = value;
				this.setGraphCallback();
				this.legendC.Changed();
			}
		}
	}

	public int numRowsOrColumns
	{
		get
		{
			return this._numRowsOrColumns;
		}
		set
		{
			if (this._numRowsOrColumns != value)
			{
				this._numRowsOrColumns = value;
				this.legendC.Changed();
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
				this.setGraphCallback();
				this.legendC.Changed();
			}
		}
	}

	public float legendEntryLinkSpacing
	{
		get
		{
			return this._legendEntryLinkSpacing;
		}
		set
		{
			if (this._legendEntryLinkSpacing != value)
			{
				this._legendEntryLinkSpacing = value;
				this.legendC.Changed();
			}
		}
	}

	public int legendEntryFontSize
	{
		get
		{
			return this._legendEntryFontSize;
		}
		set
		{
			if (this._legendEntryFontSize != value)
			{
				this._legendEntryFontSize = value;
				this.setGraphCallback();
				this.legendC.Changed();
			}
		}
	}

	public float legendEntrySpacing
	{
		get
		{
			return this._legendEntrySpacing;
		}
		set
		{
			if (this._legendEntrySpacing != value)
			{
				this._legendEntrySpacing = value;
				this.setGraphCallback();
				this.legendC.Changed();
			}
		}
	}

	public float pieSwatchSize
	{
		get
		{
			return this._pieSwatchSize;
		}
		set
		{
			if (this._pieSwatchSize != value)
			{
				this._pieSwatchSize = value;
				this.setGraphCallback();
				this.legendC.Changed();
			}
		}
	}

	public float backgroundPadding
	{
		get
		{
			return this._backgroundPadding;
		}
		set
		{
			if (this._backgroundPadding != value)
			{
				this._backgroundPadding = value;
				this.setGraphCallback();
				this.legendC.Changed();
			}
		}
	}

	public bool autofitEnabled
	{
		get
		{
			return this._autofitEnabled;
		}
		set
		{
			if (this._autofitEnabled != value)
			{
				this._autofitEnabled = value;
				this.setGraphCallback();
				this.legendC.Changed();
			}
		}
	}

	public Color labelColor
	{
		get
		{
			return this._labelColor;
		}
		set
		{
			if (this._labelColor != value)
			{
				this._labelColor = value;
				this.legendC.Changed();
			}
		}
	}

	public int LegendWidth
	{
		get
		{
			return Mathf.RoundToInt(2f * this.backgroundPadding + this.legendEntryLinkSpacing + this.legendEntryWidth * (float)((this.legendType != WMG_Legend.legendTypes.Right) ? this.MaxInRowOrColumn : this.numRowsOrColumns));
		}
	}

	public int LegendHeight
	{
		get
		{
			return Mathf.RoundToInt(2f * this.backgroundPadding + this.legendEntryHeight * (float)((this.legendType != WMG_Legend.legendTypes.Bottom) ? this.MaxInRowOrColumn : this.numRowsOrColumns));
		}
	}

	public int NumEntries
	{
		get
		{
			int num = this.legendEntries.Count;
			for (int i = 0; i < this.legendEntries.Count; i++)
			{
				if (!base.activeInHierarchy(this.legendEntries[i].gameObject))
				{
					num--;
				}
			}
			return num;
		}
	}

	public int MaxInRowOrColumn
	{
		get
		{
			return Mathf.CeilToInt(1f * (float)this.NumEntries / (float)this.numRowsOrColumns);
		}
	}

	public float origLegendEntryWidth { get; private set; }

	public float origLegendEntryHeight { get; private set; }

	public float origLegendEntryLinkSpacing { get; private set; }

	public int origLegendEntryFontSize { get; private set; }

	public float origLegendEntrySpacing { get; private set; }

	public float origPieSwatchSize { get; private set; }

	public float origOffset { get; private set; }

	public float origBackgroundPadding { get; private set; }

	public void Init()
	{
		if (this.hasInit)
		{
			return;
		}
		this.hasInit = true;
		this.pieGraph = this.theGraph.GetComponent<WMG_Pie_Graph>();
		this.axisGraph = this.theGraph.GetComponent<WMG_Axis_Graph>();
		this.changeObjs.Add(this.legendC);
		this.setOriginalPropertyValues();
		this.legendC.OnChange += this.LegendChanged;
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

	public void LegendChanged()
	{
		this.updateLegend();
	}

	private void setGraphCallback()
	{
		if (this.pieGraph != null)
		{
			this.pieGraph.graphC.Changed();
		}
	}

	public void setOriginalPropertyValues()
	{
		this.origLegendEntryWidth = this.legendEntryWidth;
		this.origLegendEntryHeight = this.legendEntryHeight;
		this.origLegendEntryLinkSpacing = this.legendEntryLinkSpacing;
		this.origLegendEntryFontSize = this.legendEntryFontSize;
		this.origLegendEntrySpacing = this.legendEntrySpacing;
		this.origPieSwatchSize = this.pieSwatchSize;
		this.origOffset = this.offset;
		this.origBackgroundPadding = this.backgroundPadding;
	}

	public WMG_Legend_Entry createLegendEntry(UnityEngine.Object prefab)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(prefab) as GameObject;
		this.theGraph.changeSpriteParent(gameObject, this.entriesParent);
		WMG_Legend_Entry component = gameObject.GetComponent<WMG_Legend_Entry>();
		component.legend = this;
		this.legendEntries.Add(component);
		return component;
	}

	public WMG_Legend_Entry createLegendEntry(UnityEngine.Object prefab, WMG_Series series, int index)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(prefab) as GameObject;
		this.theGraph.changeSpriteParent(gameObject, this.entriesParent);
		WMG_Legend_Entry component = gameObject.GetComponent<WMG_Legend_Entry>();
		component.seriesRef = series;
		component.legend = this;
		component.nodeLeft = this.theGraph.CreateNode(this.emptyPrefab, gameObject);
		component.nodeRight = this.theGraph.CreateNode(this.emptyPrefab, gameObject);
		this.legendEntries.Insert(index, component);
		return component;
	}

	public void deleteLegendEntry(int index)
	{
		UnityEngine.Object.DestroyImmediate(this.legendEntries[index].gameObject);
		this.legendEntries.RemoveAt(index);
	}

	private bool backgroundEnabled()
	{
		int num = 1;
		if (this.axisGraph != null)
		{
			num = this.axisGraph.lineSeries.Count;
		}
		if (this.pieGraph != null)
		{
			num = this.pieGraph.sliceValues.Count;
		}
		return !this.hideLegend && this.showBackground && num != 0;
	}

	private float getMaxLabelWidth()
	{
		float num = 0f;
		foreach (WMG_Legend_Entry wmg_Legend_Entry in this.legendEntries)
		{
			float num2 = base.getTextSize(wmg_Legend_Entry.label).x * wmg_Legend_Entry.label.transform.localScale.x;
			if (num2 > num)
			{
				num = num2;
			}
		}
		return num;
	}

	public void updateLegend()
	{
		if (this.backgroundEnabled() && !this.theGraph.activeInHierarchy(this.background))
		{
			this.theGraph.SetActive(this.background, true);
		}
		if (!this.backgroundEnabled() && this.theGraph.activeInHierarchy(this.background))
		{
			this.theGraph.SetActive(this.background, false);
		}
		if (!this.hideLegend && !this.theGraph.activeInHierarchy(this.entriesParent))
		{
			this.theGraph.SetActive(this.entriesParent, true);
		}
		if (this.hideLegend && this.theGraph.activeInHierarchy(this.entriesParent))
		{
			this.theGraph.SetActive(this.entriesParent, false);
		}
		if (this.hideLegend)
		{
			return;
		}
		float num = 0f;
		Vector2 zero = Vector2.zero;
		Vector2 pivot = Vector2.zero;
		Vector2 zero2 = Vector2.zero;
		if (this.axisGraph != null)
		{
			num = this.axisGraph.getMaxPointSize();
		}
		if (this.pieGraph != null)
		{
			num = this.pieSwatchSize;
		}
		if (this.legendType == WMG_Legend.legendTypes.Bottom)
		{
			if (this.oppositeSideLegend)
			{
				zero = new Vector2(0.5f, 1f);
				pivot = zero;
				zero2 = new Vector2(0f, -this.offset);
			}
			else
			{
				zero = new Vector2(0.5f, 0f);
				pivot = zero;
				zero2 = new Vector2(0f, this.offset);
			}
		}
		else if (this.legendType == WMG_Legend.legendTypes.Right)
		{
			if (this.oppositeSideLegend)
			{
				zero = new Vector2(0f, 0.5f);
				pivot = zero;
				zero2 = new Vector2(this.offset, 0f);
			}
			else
			{
				zero = new Vector2(1f, 0.5f);
				pivot = zero;
				zero2 = new Vector2(-this.offset, 0f);
			}
		}
		if (this.pieGraph != null)
		{
			zero2 = new Vector2(-1f * zero2.x, -1f * zero2.y);
			if (this.legendType == WMG_Legend.legendTypes.Bottom)
			{
				pivot = new Vector2(pivot.x, 1f - pivot.y);
			}
			else
			{
				pivot = new Vector2(1f - pivot.x, pivot.y);
			}
		}
		base.changeSpriteWidth(base.gameObject, this.LegendWidth);
		base.changeSpriteHeight(base.gameObject, this.LegendHeight);
		base.setAnchor(base.gameObject, zero, pivot, zero2);
		Vector2 anchoredPosition = new Vector2(this.legendEntryLinkSpacing + this.backgroundPadding + num / 2f, -this.legendEntryHeight / 2f + (float)this.LegendHeight / 2f - this.backgroundPadding);
		base.setAnchor(this.entriesParent, new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), anchoredPosition);
		int numEntries = this.NumEntries;
		int maxInRowOrColumn = this.MaxInRowOrColumn;
		if (this.numRowsOrColumns < 1)
		{
			this._numRowsOrColumns = 1;
		}
		if (this.numRowsOrColumns > numEntries)
		{
			this._numRowsOrColumns = numEntries;
		}
		int num2 = 0;
		if (numEntries > 0)
		{
			num2 = numEntries % this.numRowsOrColumns;
		}
		int num3 = num2;
		int num4 = 0;
		int num5 = 0;
		bool flag = false;
		if (maxInRowOrColumn == 0)
		{
			return;
		}
		for (int i = 0; i < this.legendEntries.Count; i++)
		{
			WMG_Legend_Entry wmg_Legend_Entry = this.legendEntries[i];
			if (this.axisGraph != null)
			{
				if (wmg_Legend_Entry.swatchNode == null)
				{
					foreach (GameObject gameObject in this.axisGraph.lineSeries)
					{
						gameObject.GetComponent<WMG_Series>().CreateOrDeleteSpritesBasedOnPointValues();
					}
				}
				this.theGraph.changeSpritePositionRelativeToObjBy(wmg_Legend_Entry.nodeLeft, wmg_Legend_Entry.swatchNode, new Vector3(-this.legendEntryLinkSpacing, 0f, 0f));
				this.theGraph.changeSpritePositionRelativeToObjBy(wmg_Legend_Entry.nodeRight, wmg_Legend_Entry.swatchNode, new Vector3(this.legendEntryLinkSpacing, 0f, 0f));
				WMG_Link component = wmg_Legend_Entry.line.GetComponent<WMG_Link>();
				component.Reposition();
			}
			else
			{
				base.changeSpriteWidth(wmg_Legend_Entry.swatchNode, Mathf.RoundToInt(this.pieSwatchSize));
				base.changeSpriteHeight(wmg_Legend_Entry.swatchNode, Mathf.RoundToInt(this.pieSwatchSize));
			}
			if (this.axisGraph != null)
			{
				this.theGraph.changeSpritePositionToX(wmg_Legend_Entry.label, this.legendEntrySpacing);
			}
			else
			{
				this.theGraph.changeSpritePositionToX(wmg_Legend_Entry.label, this.legendEntrySpacing + this.pieSwatchSize / 2f);
			}
			if (this.axisGraph != null)
			{
				string aText = wmg_Legend_Entry.seriesRef.seriesName;
				if (this.labelType == WMG_Enums.labelTypes.None)
				{
					aText = string.Empty;
				}
				base.changeLabelText(wmg_Legend_Entry.label, aText);
			}
			base.changeLabelFontSize(wmg_Legend_Entry.label, this.legendEntryFontSize);
			base.changeSpriteColor(wmg_Legend_Entry.label, this.labelColor);
			int num6 = Mathf.FloorToInt((float)(i / maxInRowOrColumn));
			if (num3 > 0)
			{
				num6 = Mathf.FloorToInt((float)((i + 1) / maxInRowOrColumn));
			}
			if (num2 == 0 && num3 > 0)
			{
				num6 = num3 + Mathf.FloorToInt((float)((i - num3 * maxInRowOrColumn) / (maxInRowOrColumn - 1)));
				if (i - num3 * maxInRowOrColumn > 0)
				{
					flag = true;
				}
			}
			if (num2 > 0 && (i + 1) % maxInRowOrColumn == 0)
			{
				num2--;
				num6--;
			}
			if (num5 != num6)
			{
				num5 = num6;
				if (flag)
				{
					num4 += maxInRowOrColumn - 1;
				}
				else
				{
					num4 += maxInRowOrColumn;
				}
			}
			if (this.legendType == WMG_Legend.legendTypes.Bottom)
			{
				this.theGraph.changeSpritePositionTo(wmg_Legend_Entry.gameObject, new Vector3((float)i * this.legendEntryWidth - this.legendEntryWidth * (float)num4, (float)(-(float)num6) * this.legendEntryHeight, 0f));
			}
			else if (this.legendType == WMG_Legend.legendTypes.Right)
			{
				this.theGraph.changeSpritePositionTo(wmg_Legend_Entry.gameObject, new Vector3((float)num6 * this.legendEntryWidth, (float)(-(float)i) * this.legendEntryHeight + this.legendEntryHeight * (float)num4, 0f));
			}
		}
		if (this.setWidthFromLabels)
		{
			if (this.axisGraph != null && (this.axisGraph.graphType == WMG_Axis_Graph.graphTypes.line || this.axisGraph.graphType == WMG_Axis_Graph.graphTypes.line_stacked))
			{
				this.legendEntryWidth = Mathf.Max(this.legendEntryLinkSpacing, num / 2f) + this.legendEntrySpacing + this.getMaxLabelWidth() + 5f;
			}
			else
			{
				this.legendEntryWidth = num + this.legendEntrySpacing + this.getMaxLabelWidth() + 5f;
			}
		}
		if (this.autofitEnabled)
		{
			if (this.legendType == WMG_Legend.legendTypes.Bottom)
			{
				if ((float)this.LegendWidth > base.getSpriteWidth(this.theGraph.gameObject))
				{
					if (this.numRowsOrColumns < this.NumEntries)
					{
						this.numRowsOrColumns++;
					}
				}
				else if (this.numRowsOrColumns > 1)
				{
					this._numRowsOrColumns--;
					if ((float)this.LegendWidth > base.getSpriteWidth(this.theGraph.gameObject))
					{
						this._numRowsOrColumns++;
					}
					else
					{
						this._numRowsOrColumns++;
						this.numRowsOrColumns--;
					}
				}
			}
			else if ((float)this.LegendHeight > base.getSpriteHeight(this.theGraph.gameObject))
			{
				if (this.numRowsOrColumns < this.NumEntries)
				{
					this.numRowsOrColumns++;
				}
			}
			else if (this.numRowsOrColumns > 1)
			{
				this._numRowsOrColumns--;
				if ((float)this.LegendHeight > base.getSpriteHeight(this.theGraph.gameObject))
				{
					this._numRowsOrColumns++;
				}
				else
				{
					this._numRowsOrColumns++;
					this.numRowsOrColumns--;
				}
			}
		}
	}

	public void setLabelScales(float newScale)
	{
		foreach (WMG_Legend_Entry wmg_Legend_Entry in this.legendEntries)
		{
			wmg_Legend_Entry.label.transform.localScale = new Vector3(newScale, newScale, 1f);
		}
	}

	public WMG_Graph_Manager theGraph;

	public GameObject background;

	public GameObject entriesParent;

	public UnityEngine.Object emptyPrefab;

	public List<WMG_Legend_Entry> legendEntries;

	private WMG_Pie_Graph pieGraph;

	private WMG_Axis_Graph axisGraph;

	[SerializeField]
	private bool _hideLegend;

	[SerializeField]
	private WMG_Legend.legendTypes _legendType;

	[SerializeField]
	private WMG_Enums.labelTypes _labelType;

	[SerializeField]
	private bool _showBackground;

	[SerializeField]
	private bool _oppositeSideLegend;

	[SerializeField]
	private float _offset;

	[SerializeField]
	private float _legendEntryWidth;

	[SerializeField]
	private bool _setWidthFromLabels;

	[SerializeField]
	private float _legendEntryHeight;

	[SerializeField]
	private int _numRowsOrColumns;

	[SerializeField]
	private int _numDecimals;

	[SerializeField]
	private float _legendEntryLinkSpacing;

	[SerializeField]
	private int _legendEntryFontSize;

	[SerializeField]
	private float _legendEntrySpacing;

	[SerializeField]
	private float _pieSwatchSize;

	[SerializeField]
	private float _backgroundPadding;

	[SerializeField]
	private bool _autofitEnabled;

	[SerializeField]
	private Color _labelColor;

	private bool hasInit;

	private List<WMG_Change_Obj> changeObjs = new List<WMG_Change_Obj>();

	public WMG_Change_Obj legendC = new WMG_Change_Obj();

	public enum legendTypes
	{
		Bottom,
		Right
	}
}
