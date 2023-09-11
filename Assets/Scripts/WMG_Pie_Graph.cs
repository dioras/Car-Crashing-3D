using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WMG_Pie_Graph : WMG_Graph_Manager
{
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

	public WMG_Pie_Graph.ResizeProperties resizeProperties
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

	public Vector2 leftRightPadding
	{
		get
		{
			return this._leftRightPadding;
		}
		set
		{
			if (this._leftRightPadding != value)
			{
				this._leftRightPadding = value;
				this.graphC.Changed();
			}
		}
	}

	public Vector2 topBotPadding
	{
		get
		{
			return this._topBotPadding;
		}
		set
		{
			if (this._topBotPadding != value)
			{
				this._topBotPadding = value;
				this.graphC.Changed();
			}
		}
	}

	public float bgCircleOffset
	{
		get
		{
			return this._bgCircleOffset;
		}
		set
		{
			if (this._bgCircleOffset != value)
			{
				this._bgCircleOffset = value;
				this.graphC.Changed();
			}
		}
	}

	public bool autoCenter
	{
		get
		{
			return this._autoCenter;
		}
		set
		{
			if (this._autoCenter != value)
			{
				this._autoCenter = value;
				this.graphC.Changed();
			}
		}
	}

	public float autoCenterMinPadding
	{
		get
		{
			return this._autoCenterMinPadding;
		}
		set
		{
			if (this._autoCenterMinPadding != value)
			{
				this._autoCenterMinPadding = value;
				this.graphC.Changed();
			}
		}
	}

	public WMG_Pie_Graph.sortMethod sortBy
	{
		get
		{
			return this._sortBy;
		}
		set
		{
			if (this._sortBy != value)
			{
				this._sortBy = value;
				this.graphC.Changed();
			}
		}
	}

	public bool swapColorsDuringSort
	{
		get
		{
			return this._swapColorsDuringSort;
		}
		set
		{
			if (this._swapColorsDuringSort != value)
			{
				this._swapColorsDuringSort = value;
				this.graphC.Changed();
			}
		}
	}

	public WMG_Enums.labelTypes sliceLabelType
	{
		get
		{
			return this._sliceLabelType;
		}
		set
		{
			if (this._sliceLabelType != value)
			{
				this._sliceLabelType = value;
				this.graphC.Changed();
			}
		}
	}

	public float explodeLength
	{
		get
		{
			return this._explodeLength;
		}
		set
		{
			if (this._explodeLength != value)
			{
				this._explodeLength = value;
				this.graphC.Changed();
			}
		}
	}

	public bool explodeSymmetrical
	{
		get
		{
			return this._explodeSymmetrical;
		}
		set
		{
			if (this._explodeSymmetrical != value)
			{
				this._explodeSymmetrical = value;
				this.graphC.Changed();
			}
		}
	}

	public float doughnutPercentage
	{
		get
		{
			return this._doughnutPercentage;
		}
		set
		{
			if (this._doughnutPercentage != value)
			{
				this._doughnutPercentage = value;
				this.doughnutC.Changed();
			}
		}
	}

	public bool limitNumberSlices
	{
		get
		{
			return this._limitNumberSlices;
		}
		set
		{
			if (this._limitNumberSlices != value)
			{
				this._limitNumberSlices = value;
				this.graphC.Changed();
			}
		}
	}

	public bool includeOthers
	{
		get
		{
			return this._includeOthers;
		}
		set
		{
			if (this._includeOthers != value)
			{
				this._includeOthers = value;
				this.graphC.Changed();
			}
		}
	}

	public int maxNumberSlices
	{
		get
		{
			return this._maxNumberSlices;
		}
		set
		{
			if (this._maxNumberSlices != value)
			{
				this._maxNumberSlices = value;
				this.graphC.Changed();
			}
		}
	}

	public string includeOthersLabel
	{
		get
		{
			return this._includeOthersLabel;
		}
		set
		{
			if (this._includeOthersLabel != value)
			{
				this._includeOthersLabel = value;
				this.graphC.Changed();
			}
		}
	}

	public Color includeOthersColor
	{
		get
		{
			return this._includeOthersColor;
		}
		set
		{
			if (this._includeOthersColor != value)
			{
				this._includeOthersColor = value;
				this.graphC.Changed();
			}
		}
	}

	public float animationDuration
	{
		get
		{
			return this._animationDuration;
		}
		set
		{
			if (this._animationDuration != value)
			{
				this._animationDuration = value;
				this.graphC.Changed();
			}
		}
	}

	public float sortAnimationDuration
	{
		get
		{
			return this._sortAnimationDuration;
		}
		set
		{
			if (this._sortAnimationDuration != value)
			{
				this._sortAnimationDuration = value;
				this.graphC.Changed();
			}
		}
	}

	public float sliceLabelExplodeLength
	{
		get
		{
			return this._sliceLabelExplodeLength;
		}
		set
		{
			if (this._sliceLabelExplodeLength != value)
			{
				this._sliceLabelExplodeLength = value;
				this.graphC.Changed();
			}
		}
	}

	public int sliceLabelFontSize
	{
		get
		{
			return this._sliceLabelFontSize;
		}
		set
		{
			if (this._sliceLabelFontSize != value)
			{
				this._sliceLabelFontSize = value;
				this.graphC.Changed();
			}
		}
	}

	public int numberDecimalsInPercents
	{
		get
		{
			return this._numberDecimalsInPercents;
		}
		set
		{
			if (this._numberDecimalsInPercents != value)
			{
				this._numberDecimalsInPercents = value;
				this.graphC.Changed();
			}
		}
	}

	public Color sliceLabelColor
	{
		get
		{
			return this._sliceLabelColor;
		}
		set
		{
			if (this._sliceLabelColor != value)
			{
				this._sliceLabelColor = value;
				this.graphC.Changed();
			}
		}
	}

	public bool hideZeroValueLegendEntry
	{
		get
		{
			return this._hideZeroValueLegendEntry;
		}
		set
		{
			if (this._hideZeroValueLegendEntry != value)
			{
				this._hideZeroValueLegendEntry = value;
				this.graphC.Changed();
			}
		}
	}

	public float pieSize
	{
		get
		{
			return Mathf.Min(base.getSpriteWidth(base.gameObject) - this.leftRightPadding.x - this.leftRightPadding.y + 2f * this.explodeLength, base.getSpriteHeight(base.gameObject) - this.topBotPadding.x - this.topBotPadding.y + 2f * this.explodeLength);
		}
	}

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
		this.legend.Init();
		this.changeObjs.Add(this.graphC);
		this.changeObjs.Add(this.resizeC);
		this.changeObjs.Add(this.doughnutC);
		if (this.animationDuration > 0f)
		{
			this.UpdateVisuals(true);
		}
		this.createTextureData();
		this.cachedContainerWidth = base.getSpriteWidth(base.gameObject);
		this.cachedContainerHeight = base.getSpriteHeight(base.gameObject);
		this.sliceValues.SetList(this._sliceValues);
		this.sliceValues.Changed += this.sliceValuesChanged;
		this.sliceLabels.SetList(this._sliceLabels);
		this.sliceLabels.Changed += this.sliceLabelsChanged;
		this.sliceColors.SetList(this._sliceColors);
		this.sliceColors.Changed += this.sliceColorsChanged;
		this.graphC.OnChange += this.GraphChanged;
		this.resizeC.OnChange += this.ResizeChanged;
		this.doughnutC.OnChange += this.DoughtnutChanged;
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

	private void PauseCallbacks()
	{
		for (int i = 0; i < this.changeObjs.Count; i++)
		{
			this.changeObjs[i].changesPaused = true;
			this.changeObjs[i].changePaused = false;
		}
		this.legend.PauseCallbacks();
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
		}
	}

	private void updateFromDataSource()
	{
		if (this.sliceValuesDataSource != null)
		{
			this.sliceValues.SetList(this.sliceValuesDataSource.getData<float>());
		}
		if (this.sliceLabelsDataSource != null)
		{
			this.sliceLabels.SetList(this.sliceLabelsDataSource.getData<string>());
		}
		if (this.sliceColorsDataSource != null)
		{
			this.sliceColors.SetList(this.sliceColorsDataSource.getData<Color>());
		}
		if ((this.sliceValuesDataSource != null || this.sliceLabelsDataSource != null || this.sliceColorsDataSource != null) && this.sortBy != WMG_Pie_Graph.sortMethod.None)
		{
			this.sortData();
		}
	}

	private void ResizeChanged()
	{
		this.UpdateFromContainer();
		this.UpdateVisuals(true);
	}

	private void DoughtnutChanged()
	{
		this.UpdateDoughnut();
	}

	private void GraphChanged()
	{
		if (!this.isAnimating)
		{
			this.UpdateVisuals(false);
		}
	}

	private void AllChanged()
	{
		this.UpdateDoughnut();
		if (!this.isAnimating)
		{
			this.UpdateVisuals(false);
		}
	}

	private void sliceValuesChanged(bool editorChange, bool countChanged, bool oneValChanged, int index)
	{
		WMG_Util.listChanged<float>(editorChange, ref this.sliceValues, ref this._sliceValues, oneValChanged, index);
		this.graphC.Changed();
	}

	private void sliceLabelsChanged(bool editorChange, bool countChanged, bool oneValChanged, int index)
	{
		WMG_Util.listChanged<string>(editorChange, ref this.sliceLabels, ref this._sliceLabels, oneValChanged, index);
		this.graphC.Changed();
	}

	private void sliceColorsChanged(bool editorChange, bool countChanged, bool oneValChanged, int index)
	{
		WMG_Util.listChanged<Color>(editorChange, ref this.sliceColors, ref this._sliceColors, oneValChanged, index);
		this.graphC.Changed();
	}

	private void setOriginalPropertyValues()
	{
		this.origPieSize = this.pieSize;
		this.origSliceLabelExplodeLength = this.sliceLabelExplodeLength;
		this.origSliceLabelFontSize = this.sliceLabelFontSize;
		this.origAutoCenterPadding = this.autoCenterMinPadding;
	}

	private void UpdateFromContainer()
	{
		if (this.resizeEnabled)
		{
			float sizeFactor = this.pieSize / this.origPieSize;
			if ((this.resizeProperties & WMG_Pie_Graph.ResizeProperties.LabelExplodeLength) == WMG_Pie_Graph.ResizeProperties.LabelExplodeLength)
			{
				this.sliceLabelExplodeLength = this.getNewResizeVariable(sizeFactor, this.origSliceLabelExplodeLength);
			}
			if ((this.resizeProperties & WMG_Pie_Graph.ResizeProperties.LabelFontSize) == WMG_Pie_Graph.ResizeProperties.LabelFontSize)
			{
				this.sliceLabelFontSize = Mathf.RoundToInt(this.getNewResizeVariable(sizeFactor, (float)this.origSliceLabelFontSize));
			}
			if ((this.resizeProperties & WMG_Pie_Graph.ResizeProperties.LegendFontSize) == WMG_Pie_Graph.ResizeProperties.LegendFontSize)
			{
				this.legend.legendEntryFontSize = Mathf.RoundToInt(this.getNewResizeVariable(sizeFactor, (float)this.legend.origLegendEntryFontSize));
			}
			if ((this.resizeProperties & WMG_Pie_Graph.ResizeProperties.LegendEntrySize) == WMG_Pie_Graph.ResizeProperties.LegendEntrySize)
			{
				if (!this.legend.setWidthFromLabels)
				{
					this.legend.legendEntryWidth = this.getNewResizeVariable(sizeFactor, this.legend.origLegendEntryWidth);
				}
				this.legend.legendEntryHeight = this.getNewResizeVariable(sizeFactor, this.legend.origLegendEntryHeight);
			}
			if ((this.resizeProperties & WMG_Pie_Graph.ResizeProperties.LegendSwatchSize) == WMG_Pie_Graph.ResizeProperties.LegendSwatchSize)
			{
				this.legend.pieSwatchSize = this.getNewResizeVariable(sizeFactor, this.legend.origPieSwatchSize);
			}
			if ((this.resizeProperties & WMG_Pie_Graph.ResizeProperties.LegendOffset) == WMG_Pie_Graph.ResizeProperties.LegendOffset)
			{
				this.legend.offset = this.getNewResizeVariable(sizeFactor, this.legend.origOffset);
			}
			if ((this.resizeProperties & WMG_Pie_Graph.ResizeProperties.AutoCenterPadding) == WMG_Pie_Graph.ResizeProperties.AutoCenterPadding)
			{
				this.autoCenterMinPadding = (float)Mathf.RoundToInt(this.getNewResizeVariable(sizeFactor, this.origAutoCenterPadding));
			}
		}
	}

	private float getNewResizeVariable(float sizeFactor, float variable)
	{
		return variable + (sizeFactor - 1f) * variable / 2f;
	}

	public void updateBG(int thePieSize)
	{
		base.changeSpriteSize(this.backgroundCircle, Mathf.RoundToInt((float)thePieSize + this.bgCircleOffset), Mathf.RoundToInt((float)thePieSize + this.bgCircleOffset));
		Vector2 paddingOffset = this.getPaddingOffset();
		base.changeSpritePositionTo(this.slicesParent, new Vector3(-paddingOffset.x, -paddingOffset.y));
	}

	public Vector2 getPaddingOffset()
	{
		Vector2 spritePivot = base.getSpritePivot(base.gameObject);
		float num = (float)Mathf.RoundToInt(base.getSpriteWidth(base.gameObject)) * (spritePivot.x - 0.5f);
		float num2 = (float)Mathf.RoundToInt(base.getSpriteHeight(base.gameObject)) * (spritePivot.y - 0.5f);
		return new Vector2(-this.leftRightPadding.x * 0.5f + this.leftRightPadding.y * 0.5f + num, this.topBotPadding.x * 0.5f - this.topBotPadding.y * 0.5f + num2);
	}

	public List<GameObject> getSlices()
	{
		return this.slices;
	}

	private void UpdateData()
	{
		this.isOtherSlice = false;
		this.numSlices = this.sliceValues.Count;
		if (this.limitNumberSlices && this.numSlices > this.maxNumberSlices)
		{
			this.numSlices = this.maxNumberSlices;
			if (this.includeOthers)
			{
				this.isOtherSlice = true;
				this.numSlices++;
			}
		}
		this.otherSliceValue = 0f;
		this.totalVal = 0f;
		for (int i = 0; i < this.sliceValues.Count; i++)
		{
			this.totalVal += this.sliceValues[i];
			if (this.isOtherSlice && i >= this.maxNumberSlices)
			{
				this.otherSliceValue += this.sliceValues[i];
			}
			if (this.limitNumberSlices && !this.isOtherSlice && i >= this.maxNumberSlices)
			{
				this.totalVal -= this.sliceValues[i];
			}
		}
	}

	private void CreateOrDeleteSlicesBasedOnValues()
	{
		this.LabelToSliceMap.Clear();
		for (int i = 0; i < this.numSlices; i++)
		{
			if (this.sliceLabels.Count <= i)
			{
				this.sliceLabels.Add(string.Empty);
			}
			if (this.sliceColors.Count <= i)
			{
				this.sliceColors.Add(Color.white);
			}
			if (this.slices.Count <= i)
			{
				GameObject gameObject = base.CreateNode(this.nodePrefab, this.slicesParent);
				this.slices.Add(gameObject);
				WMG_Pie_Graph_Slice component = gameObject.GetComponent<WMG_Pie_Graph_Slice>();
				base.setTexture(component.objectToColor, this.pieSprite);
				base.setTexture(component.objectToMask, this.pieSprite);
			}
			if (this.legend.legendEntries.Count <= i)
			{
				this.legend.createLegendEntry(this.legendEntryPrefab);
			}
		}
		for (int j = this.slices.Count - 1; j >= 0; j--)
		{
			if (this.slices[j] != null && j >= this.numSlices)
			{
				WMG_Pie_Graph_Slice component2 = this.slices[j].GetComponent<WMG_Pie_Graph_Slice>();
				base.DeleteNode(component2);
				this.slices.RemoveAt(j);
			}
		}
		for (int k = this.legend.legendEntries.Count - 1; k >= 0; k--)
		{
			if (this.legend.legendEntries[k] != null && k >= this.numSlices)
			{
				this.legend.deleteLegendEntry(k);
			}
		}
	}

	private void UpdateVisuals(bool noAnim)
	{
		this.UpdateData();
		this.CreateOrDeleteSlicesBasedOnValues();
		if (this.totalVal == 0f && this.numSlices > 0)
		{
			return;
		}
		for (int i = 0; i < this.numSlices; i++)
		{
			WMG_Pie_Graph_Slice component = this.slices[i].GetComponent<WMG_Pie_Graph_Slice>();
			base.SetActive(component.objectToMask, this.explodeSymmetrical);
			if (this.explodeSymmetrical)
			{
				base.changeSpriteParent(component.objectToColor, component.objectToMask);
			}
			else
			{
				base.changeSpriteParent(component.objectToColor, component.gameObject);
				base.bringSpriteToFront(component.objectToLabel);
			}
		}
		int num = Mathf.RoundToInt(this.pieSize);
		this.updateBG(num);
		if (this.animationDuration == 0f && this.sortBy != WMG_Pie_Graph.sortMethod.None)
		{
			this.sortData();
		}
		float num2 = 0f;
		if (!noAnim)
		{
			this.animSortSwap = false;
		}
		for (int j = 0; j < this.numSlices; j++)
		{
			float num3 = -1f * num2;
			if (num3 < 0f)
			{
				num3 += 360f;
			}
			WMG_Pie_Graph_Slice component2 = this.slices[j].GetComponent<WMG_Pie_Graph_Slice>();
			if (this.sliceLabelType != WMG_Enums.labelTypes.None && !base.activeInHierarchy(component2.objectToLabel))
			{
				base.SetActive(component2.objectToLabel, true);
			}
			if (this.sliceLabelType == WMG_Enums.labelTypes.None && base.activeInHierarchy(component2.objectToLabel))
			{
				base.SetActive(component2.objectToLabel, false);
			}
			if (!this.explodeSymmetrical)
			{
				base.changeSpriteSize(component2.objectToColor, num, num);
			}
			else
			{
				base.changeSpriteSize(component2.objectToColor, num, num);
				base.changeSpriteSize(component2.objectToMask, num + Mathf.RoundToInt(this.explodeLength * 4f), num + Mathf.RoundToInt(this.explodeLength * 4f));
			}
			Color aColor = this.sliceColors[j];
			string text = this.sliceLabels[j];
			float num4 = this.sliceValues[j];
			if (this.isOtherSlice && j == this.numSlices - 1)
			{
				aColor = this.includeOthersColor;
				text = this.includeOthersLabel;
				num4 = this.otherSliceValue;
			}
			if (!this.LabelToSliceMap.ContainsKey(text))
			{
				this.LabelToSliceMap.Add(text, component2);
			}
			if (num4 == 0f)
			{
				base.SetActive(component2.objectToLabel, false);
			}
			float num5 = num4 / this.totalVal;
			component2.slicePercent = num5 * 360f;
			float num6 = num3 * -1f + 0.5f * num5 * 360f;
			float num7 = this.sliceLabelExplodeLength + (float)(num / 2);
			float num8 = Mathf.Sin(num6 * 0.0174532924f);
			float num9 = Mathf.Cos(num6 * 0.0174532924f);
			if (!noAnim && this.animationDuration > 0f)
			{
				this.isAnimating = true;
				WMG_Anim.animFill(component2.objectToColor, this.animationDuration, Ease.Linear, num5);
				WMG_Anim.animPosition(component2.objectToLabel, this.animationDuration, Ease.Linear, new Vector3(num7 * num8, num7 * num9));
				int newI = j;
				WMG_Anim.animPositionCallbackC(this.slices[j], this.animationDuration, Ease.Linear, new Vector3(this.explodeLength * num8, this.explodeLength * num9), delegate
				{
					this.shrinkSlices(newI);
				});
				if (!this.explodeSymmetrical)
				{
					WMG_Anim.animRotation(component2.objectToColor, this.animationDuration, Ease.Linear, new Vector3(0f, 0f, num3), false);
					WMG_Anim.animPosition(component2.objectToColor, this.animationDuration, Ease.Linear, Vector3.zero);
				}
				else
				{
					WMG_Anim.animRotation(component2.objectToColor, this.animationDuration, Ease.Linear, Vector3.zero, false);
					Vector2 vector = new Vector2(-this.explodeLength * num8, -this.explodeLength * num9);
					float num10 = Mathf.Sin(num3 * 0.0174532924f);
					float num11 = Mathf.Cos(num3 * 0.0174532924f);
					WMG_Anim.animPosition(component2.objectToColor, this.animationDuration, Ease.Linear, new Vector3(num11 * vector.x + num10 * vector.y, num11 * vector.y - num10 * vector.x));
					WMG_Anim.animRotation(component2.objectToMask, this.animationDuration, Ease.Linear, new Vector3(0f, 0f, num3), false);
					WMG_Anim.animFill(component2.objectToMask, this.animationDuration, Ease.Linear, num5);
				}
			}
			else
			{
				base.changeSpriteFill(component2.objectToColor, num5);
				component2.objectToLabel.transform.localPosition = new Vector3(num7 * num8, num7 * num9);
				this.slices[j].transform.localPosition = new Vector3(this.explodeLength * num8, this.explodeLength * num9);
				if (!this.explodeSymmetrical)
				{
					component2.objectToColor.transform.localEulerAngles = new Vector3(0f, 0f, num3);
					component2.objectToColor.transform.localPosition = Vector3.zero;
				}
				else
				{
					component2.objectToColor.transform.localEulerAngles = Vector3.zero;
					Vector2 vector2 = new Vector2(-this.explodeLength * num8, -this.explodeLength * num9);
					float num12 = Mathf.Sin(num3 * 0.0174532924f);
					float num13 = Mathf.Cos(num3 * 0.0174532924f);
					component2.objectToColor.transform.localPosition = new Vector3(num13 * vector2.x + num12 * vector2.y, num13 * vector2.y - num12 * vector2.x);
					component2.objectToMask.transform.localEulerAngles = new Vector3(0f, 0f, num3);
					base.changeSpriteFill(component2.objectToMask, num5);
				}
			}
			base.changeSpriteColor(component2.objectToColor, aColor);
			base.changeSpriteColor(component2.objectToMask, aColor);
			base.changeLabelText(component2.objectToLabel, base.getLabelText(text, this.sliceLabelType, num4, num5, this.numberDecimalsInPercents));
			base.changeLabelFontSize(component2.objectToLabel, this.sliceLabelFontSize);
			base.changeSpriteColor(component2.objectToLabel, this.sliceLabelColor);
			this.slices[j].name = text;
			this.legend.legendEntries[j].name = text;
			num2 += num5 * 360f;
			component2.slicePercentPosition = num2 - component2.slicePercent / 2f;
			WMG_Legend_Entry wmg_Legend_Entry = this.legend.legendEntries[j];
			base.changeLabelText(wmg_Legend_Entry.label, base.getLabelText(text, this.legend.labelType, num4, num5, this.legend.numDecimals));
			base.changeSpriteColor(wmg_Legend_Entry.swatchNode, aColor);
			if (this.hideZeroValueLegendEntry)
			{
				if (num4 == 0f)
				{
					base.SetActive(wmg_Legend_Entry.gameObject, false);
				}
				else
				{
					base.SetActive(wmg_Legend_Entry.gameObject, true);
				}
			}
			else
			{
				base.SetActive(wmg_Legend_Entry.gameObject, true);
			}
		}
		this.legend.LegendChanged();
		this.updateAutoCenter();
		if (!this.setOrig)
		{
			this.setOrig = true;
			this.setOriginalPropertyValues();
		}
	}

	private void updateAutoCenter()
	{
		if (this.autoCenter)
		{
			float num = this.autoCenterMinPadding + this.explodeLength + this.bgCircleOffset / 2f;
			if (this.legend.hideLegend)
			{
				this.leftRightPadding = new Vector2(num, num);
				this.topBotPadding = new Vector2(num, num);
			}
			else if (this.legend.legendType == WMG_Legend.legendTypes.Right)
			{
				this.topBotPadding = new Vector2(num, num);
				if (this.legend.oppositeSideLegend)
				{
					this.leftRightPadding = new Vector2(num + (float)this.legend.LegendWidth + Mathf.Abs(this.legend.offset), num);
				}
				else
				{
					this.leftRightPadding = new Vector2(num, num + (float)this.legend.LegendWidth + Mathf.Abs(this.legend.offset));
				}
			}
			else
			{
				this.leftRightPadding = new Vector2(num, num);
				if (!this.legend.oppositeSideLegend)
				{
					this.topBotPadding = new Vector2(num, num + (float)this.legend.LegendHeight + Mathf.Abs(this.legend.offset));
				}
				else
				{
					this.topBotPadding = new Vector2(num + (float)this.legend.LegendHeight + Mathf.Abs(this.legend.offset), num);
				}
			}
		}
	}

	private void shrinkSlices(int sliceNum)
	{
		if (!this.animSortSwap && this.sortBy != WMG_Pie_Graph.sortMethod.None)
		{
			this.animSortSwap = this.sortData();
		}
		if (this.animSortSwap)
		{
			if (this.sortAnimationDuration > 0f)
			{
				WMG_Anim.animScaleCallbackC(this.slices[sliceNum], this.sortAnimationDuration / 2f, Ease.Linear, Vector3.zero, delegate
				{
					this.enlargeSlices(sliceNum);
				});
			}
			else
			{
				this.isAnimating = false;
				this.UpdateVisuals(true);
			}
		}
		else
		{
			this.isAnimating = false;
		}
	}

	private void enlargeSlices(int sliceNum)
	{
		if (sliceNum == 0)
		{
			this.UpdateVisuals(true);
		}
		WMG_Anim.animScaleCallbackC(this.slices[sliceNum], this.sortAnimationDuration / 2f, Ease.Linear, Vector3.one, delegate
		{
			this.endSortAnimating(sliceNum);
		});
	}

	private void endSortAnimating(int sliceNum)
	{
		if (sliceNum == this.numSlices - 1)
		{
			this.animSortSwap = false;
			this.isAnimating = false;
		}
	}

	private bool sortData()
	{
		bool result = false;
		bool flag = true;
		int num = this.numSlices;
		int num2 = 1;
		while (num2 <= num && flag)
		{
			flag = false;
			for (int i = 0; i < num - 1; i++)
			{
				bool flag2 = false;
				if (this.sortBy == WMG_Pie_Graph.sortMethod.Largest_First)
				{
					if (this.sliceValues[i + 1] > this.sliceValues[i])
					{
						flag2 = true;
					}
				}
				else if (this.sortBy == WMG_Pie_Graph.sortMethod.Smallest_First)
				{
					if (this.sliceValues[i + 1] < this.sliceValues[i])
					{
						flag2 = true;
					}
				}
				else if (this.sortBy == WMG_Pie_Graph.sortMethod.Alphabetically)
				{
					if (this.sliceLabels[i + 1].CompareTo(this.sliceLabels[i]) == -1)
					{
						flag2 = true;
					}
				}
				else if (this.sortBy == WMG_Pie_Graph.sortMethod.Reverse_Alphabetically && this.sliceLabels[i + 1].CompareTo(this.sliceLabels[i]) == 1)
				{
					flag2 = true;
				}
				if (flag2)
				{
					float val = this.sliceValues[i];
					this.sliceValues.SetValNoCb(i, this.sliceValues[i + 1], ref this._sliceValues);
					this.sliceValues.SetValNoCb(i + 1, val, ref this._sliceValues);
					string val2 = this.sliceLabels[i];
					this.sliceLabels.SetValNoCb(i, this.sliceLabels[i + 1], ref this._sliceLabels);
					this.sliceLabels.SetValNoCb(i + 1, val2, ref this._sliceLabels);
					GameObject value = this.slices[i];
					this.slices[i] = this.slices[i + 1];
					this.slices[i + 1] = value;
					if (this.swapColorsDuringSort)
					{
						Color val3 = this.sliceColors[i];
						this.sliceColors.SetValNoCb(i, this.sliceColors[i + 1], ref this._sliceColors);
						this.sliceColors.SetValNoCb(i + 1, val3, ref this._sliceColors);
					}
					flag = true;
					result = true;
				}
			}
			num2++;
		}
		return result;
	}

	private void UpdateDoughnut()
	{
		WMG_Util.updateBandColors(ref this.colors, this.pieSize, this.doughnutPercentage * this.pieSize / 2f, this.pieSize / 2f, true, 2f, this.origColors);
		this.pieSprite.texture.SetPixels(this.colors);
		this.pieSprite.texture.Apply();
	}

	private void createTextureData()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(this.nodePrefab) as GameObject;
		Texture2D texture = base.getTexture(gameObject.GetComponent<WMG_Pie_Graph_Slice>().objectToColor);
		this.colors = texture.GetPixels();
		this.origColors = texture.GetPixels();
		this.pieSprite = WMG_Util.createSprite(texture);
		UnityEngine.Object.Destroy(gameObject);
	}

	public Vector3 getCalloutSlicePosition(string label, float amt)
	{
		if (this.LabelToSliceMap.ContainsKey(label))
		{
			float f = 0.0174532924f * (-this.LabelToSliceMap[label].slicePercentPosition + 90f);
			return new Vector3(amt * Mathf.Cos(f), amt * Mathf.Sin(f), 0f);
		}
		return Vector3.zero;
	}

	[SerializeField]
	private List<float> _sliceValues;

	public WMG_List<float> sliceValues = new WMG_List<float>();

	[SerializeField]
	private List<string> _sliceLabels;

	public WMG_List<string> sliceLabels = new WMG_List<string>();

	[SerializeField]
	private List<Color> _sliceColors;

	public WMG_List<Color> sliceColors = new WMG_List<Color>();

	public WMG_Data_Source sliceValuesDataSource;

	public WMG_Data_Source sliceLabelsDataSource;

	public WMG_Data_Source sliceColorsDataSource;

	public GameObject background;

	public GameObject backgroundCircle;

	public GameObject slicesParent;

	public WMG_Legend legend;

	public UnityEngine.Object legendEntryPrefab;

	public UnityEngine.Object nodePrefab;

	[SerializeField]
	private bool _resizeEnabled;

	[WMG_EnumFlag]
	[SerializeField]
	private WMG_Pie_Graph.ResizeProperties _resizeProperties;

	[SerializeField]
	private Vector2 _leftRightPadding;

	[SerializeField]
	private Vector2 _topBotPadding;

	[SerializeField]
	private float _bgCircleOffset;

	[SerializeField]
	private bool _autoCenter;

	[SerializeField]
	private float _autoCenterMinPadding;

	[SerializeField]
	private WMG_Pie_Graph.sortMethod _sortBy;

	[SerializeField]
	private bool _swapColorsDuringSort;

	[SerializeField]
	private WMG_Enums.labelTypes _sliceLabelType;

	[SerializeField]
	private float _explodeLength;

	[SerializeField]
	private bool _explodeSymmetrical;

	[SerializeField]
	private float _doughnutPercentage;

	[SerializeField]
	private bool _limitNumberSlices;

	[SerializeField]
	private bool _includeOthers;

	[SerializeField]
	private int _maxNumberSlices;

	[SerializeField]
	private string _includeOthersLabel;

	[SerializeField]
	private Color _includeOthersColor;

	[SerializeField]
	private float _animationDuration;

	[SerializeField]
	private float _sortAnimationDuration;

	[SerializeField]
	private float _sliceLabelExplodeLength;

	[SerializeField]
	private int _sliceLabelFontSize;

	[SerializeField]
	private int _numberDecimalsInPercents;

	[SerializeField]
	private Color _sliceLabelColor;

	[SerializeField]
	private bool _hideZeroValueLegendEntry;

	public Dictionary<string, WMG_Pie_Graph_Slice> LabelToSliceMap = new Dictionary<string, WMG_Pie_Graph_Slice>();

	private float origPieSize;

	private float origSliceLabelExplodeLength;

	private int origSliceLabelFontSize;

	private float origAutoCenterPadding;

	private float cachedContainerWidth;

	private float cachedContainerHeight;

	private List<GameObject> slices = new List<GameObject>();

	private int numSlices;

	private bool isOtherSlice;

	private float otherSliceValue;

	private float totalVal;

	private bool animSortSwap;

	private bool isAnimating;

	private Color[] colors;

	private Color[] origColors;

	private Sprite pieSprite;

	private List<WMG_Change_Obj> changeObjs = new List<WMG_Change_Obj>();

	public WMG_Change_Obj graphC = new WMG_Change_Obj();

	private WMG_Change_Obj resizeC = new WMG_Change_Obj();

	private WMG_Change_Obj doughnutC = new WMG_Change_Obj();

	private bool hasInit;

	private bool setOrig;

	public enum sortMethod
	{
		None,
		Largest_First,
		Smallest_First,
		Alphabetically,
		Reverse_Alphabetically
	}

	[Flags]
	public enum ResizeProperties
	{
		LabelExplodeLength = 1,
		LabelFontSize = 2,
		LegendFontSize = 4,
		LegendSwatchSize = 8,
		LegendEntrySize = 16,
		LegendOffset = 32,
		AutoCenterPadding = 64
	}
}
