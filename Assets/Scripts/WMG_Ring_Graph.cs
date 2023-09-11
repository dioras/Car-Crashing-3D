using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WMG_Ring_Graph : WMG_Graph_Manager
{
	public bool bandMode
	{
		get
		{
			return this._bandMode;
		}
		set
		{
			if (this._bandMode != value)
			{
				this._bandMode = value;
				this.textureC.Changed();
			}
		}
	}

	public float innerRadiusPercentage
	{
		get
		{
			return this._innerRadiusPercentage;
		}
		set
		{
			if (this._innerRadiusPercentage != value)
			{
				this._innerRadiusPercentage = value;
				this.textureC.Changed();
			}
		}
	}

	public float degrees
	{
		get
		{
			return this._degrees;
		}
		set
		{
			if (this._degrees != value)
			{
				this._degrees = value;
				this.degreesC.Changed();
			}
		}
	}

	public float minValue
	{
		get
		{
			return this._minValue;
		}
		set
		{
			if (this._minValue != value)
			{
				this._minValue = value;
				this.degreesC.Changed();
			}
		}
	}

	public float maxValue
	{
		get
		{
			return this._maxValue;
		}
		set
		{
			if (this._maxValue != value)
			{
				this._maxValue = value;
				this.degreesC.Changed();
			}
		}
	}

	public Color bandColor
	{
		get
		{
			return this._bandColor;
		}
		set
		{
			if (this._bandColor != value)
			{
				this._bandColor = value;
				this.bandColorC.Changed();
			}
		}
	}

	public bool autoUpdateBandAlpha
	{
		get
		{
			return this._autoUpdateBandAlpha;
		}
		set
		{
			if (this._autoUpdateBandAlpha != value)
			{
				this._autoUpdateBandAlpha = value;
				this.bandColorC.Changed();
			}
		}
	}

	public Color ringColor
	{
		get
		{
			return this._ringColor;
		}
		set
		{
			if (this._ringColor != value)
			{
				this._ringColor = value;
				this.ringColorC.Changed();
			}
		}
	}

	public float ringWidth
	{
		get
		{
			return this._ringWidth;
		}
		set
		{
			if (this._ringWidth != value)
			{
				this._ringWidth = value;
				this.textureC.Changed();
			}
		}
	}

	public float ringPointWidthFactor
	{
		get
		{
			return this._ringPointWidthFactor;
		}
		set
		{
			if (this._ringPointWidthFactor != value)
			{
				this._ringPointWidthFactor = value;
				this.textureC.Changed();
			}
		}
	}

	public float bandPadding
	{
		get
		{
			return this._bandPadding;
		}
		set
		{
			if (this._bandPadding != value)
			{
				this._bandPadding = value;
				this.textureC.Changed();
			}
		}
	}

	public float labelLinePadding
	{
		get
		{
			return this._labelLinePadding;
		}
		set
		{
			if (this._labelLinePadding != value)
			{
				this._labelLinePadding = value;
				this.radiusC.Changed();
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
				this.radiusC.Changed();
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
				this.radiusC.Changed();
			}
		}
	}

	public bool antiAliasing
	{
		get
		{
			return this._antiAliasing;
		}
		set
		{
			if (this._antiAliasing != value)
			{
				this._antiAliasing = value;
				this.textureC.Changed();
			}
		}
	}

	public float antiAliasingStrength
	{
		get
		{
			return this._antiAliasingStrength;
		}
		set
		{
			if (this._antiAliasingStrength != value)
			{
				this._antiAliasingStrength = value;
				this.textureC.Changed();
			}
		}
	}

	public float outerRadius
	{
		get
		{
			return Mathf.Min((base.getSpriteWidth(base.gameObject) - this.leftRightPadding.x - this.leftRightPadding.y) / 2f, (base.getSpriteHeight(base.gameObject) - this.topBotPadding.x - this.topBotPadding.y) / 2f);
		}
	}

	public float RingWidthFactor
	{
		get
		{
			return (1f - this.innerRadiusPercentage) * this.outerRadius / this.origGraphWidth;
		}
	}

	public List<WMG_Ring> rings { get; private set; }

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
		this.changeObjs.Add(this.numberRingsC);
		this.changeObjs.Add(this.textureC);
		this.changeObjs.Add(this.degreesC);
		this.changeObjs.Add(this.ringColorC);
		this.changeObjs.Add(this.bandColorC);
		this.changeObjs.Add(this.radiusC);
		this.changeObjs.Add(this.labelsC);
		this.extraRingSprite = WMG_Util.createSprite(base.getTexture(this.extraRing));
		this.ringTexSize = this.extraRingSprite.texture.width;
		this.extraRingColors = new Color[this.ringTexSize * this.ringTexSize];
		base.setTexture(this.extraRing, this.extraRingSprite);
		this.rings = new List<WMG_Ring>();
		this.origGraphWidth = (1f - this.innerRadiusPercentage) * this.outerRadius;
		this.bandColors.SetList(this._bandColors);
		this.bandColors.Changed += this.bandColorsChanged;
		this.values.SetList(this._values);
		this.values.Changed += this.valuesChanged;
		this.labels.SetList(this._labels);
		this.labels.Changed += this.labelsChanged;
		this.ringIDs.SetList(this._ringIDs);
		this.ringIDs.Changed += this.ringIDsChanged;
		this.numberRingsC.OnChange += this.NumberRingsChanged;
		this.bandColorC.OnChange += this.BandColorChanged;
		this.ringColorC.OnChange += this.RingColorChanged;
		this.labelsC.OnChange += this.LabelsChanged;
		this.degreesC.OnChange += this.DegreesChanged;
		this.radiusC.OnChange += this.RadiusChanged;
		this.textureC.OnChange += this.TextureChanged;
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

	private void updateFromResize()
	{
		bool flag = false;
		base.updateCacheAndFlag<float>(ref this.containerWidthCached, base.getSpriteWidth(base.gameObject), ref flag);
		base.updateCacheAndFlag<float>(ref this.containerHeightCached, base.getSpriteHeight(base.gameObject), ref flag);
		if (flag)
		{
			this.radiusC.Changed();
		}
	}

	private void updateFromDataSource()
	{
		if (this.valuesDataSource != null)
		{
			this.values.SetList(this.valuesDataSource.getData<float>());
		}
		if (this.labelsDataSource != null)
		{
			this.labels.SetList(this.labelsDataSource.getData<string>());
		}
		if (this.ringIDsDataSource != null)
		{
			this.ringIDs.SetList(this.ringIDsDataSource.getData<string>());
		}
	}

	private void NumberRingsChanged()
	{
		this.updateNumberRings();
	}

	private void TextureChanged()
	{
		this.updateRingsAndBands();
		this.updateOuterRadius();
	}

	private void DegreesChanged()
	{
		this.updateDegrees();
	}

	private void RingColorChanged()
	{
		this.updateRingColors();
	}

	private void BandColorChanged()
	{
		this.updateBandColors();
	}

	private void RadiusChanged()
	{
		this.updateOuterRadius();
	}

	private void LabelsChanged()
	{
		this.updateLabelsText();
	}

	private void AllChanged()
	{
		this.numberRingsC.Changed();
		this.textureC.Changed();
		this.degreesC.Changed();
		this.ringColorC.Changed();
		this.bandColorC.Changed();
		this.radiusC.Changed();
		this.labelsC.Changed();
	}

	public void bandColorsChanged(bool editorChange, bool countChanged, bool oneValChanged, int index)
	{
		WMG_Util.listChanged<Color>(editorChange, ref this.bandColors, ref this._bandColors, oneValChanged, index);
		this.bandColorC.Changed();
	}

	public void valuesChanged(bool editorChange, bool countChanged, bool oneValChanged, int index)
	{
		WMG_Util.listChanged<float>(editorChange, ref this.values, ref this._values, oneValChanged, index);
		if (countChanged)
		{
			this.AllChanged();
		}
		else
		{
			this.degreesC.Changed();
		}
	}

	public void labelsChanged(bool editorChange, bool countChanged, bool oneValChanged, int index)
	{
		WMG_Util.listChanged<string>(editorChange, ref this.labels, ref this._labels, oneValChanged, index);
		this.labelsC.Changed();
	}

	public void ringIDsChanged(bool editorChange, bool countChanged, bool oneValChanged, int index)
	{
		WMG_Util.listChanged<string>(editorChange, ref this.ringIDs, ref this._ringIDs, oneValChanged, index);
	}

	private void updateNumberRings()
	{
		for (int i = 0; i < this.values.Count; i++)
		{
			if (this.labels.Count <= i)
			{
				this.labels.AddNoCb("Ring " + (i + 1), ref this._labels);
			}
			if (this.bandColors.Count <= i)
			{
				this.bandColors.AddNoCb(this.bandColor, ref this._bandColors);
			}
			if (this.rings.Count <= i)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(this.ringPrefab) as GameObject;
				base.changeSpriteParent(gameObject, this.ringsParent);
				WMG_Ring component = gameObject.GetComponent<WMG_Ring>();
				component.initialize(this);
				this.rings.Add(component);
			}
		}
		for (int j = this.rings.Count - 1; j >= 0; j--)
		{
			if (this.rings[j] != null && j >= this.values.Count)
			{
				UnityEngine.Object.Destroy(this.rings[j].label);
				UnityEngine.Object.Destroy(this.rings[j].gameObject);
				this.rings.RemoveAt(j);
			}
		}
	}

	private void updateOuterRadius()
	{
		int num = Mathf.RoundToInt(this.outerRadius * 2f);
		base.changeSpriteSize(this.extraRing, num, num);
		for (int i = 0; i < this.rings.Count; i++)
		{
			base.changeSpriteSize(this.rings[i].ring, num, num);
			base.changeSpriteSize(this.rings[i].band, num, num);
			base.changeSpriteHeight(this.rings[i].label, Mathf.RoundToInt(this.outerRadius + this.labelLinePadding));
		}
		base.changeSpriteHeight(this.zeroLine, Mathf.RoundToInt(this.outerRadius + this.labelLinePadding));
		for (int j = 0; j < this.rings.Count; j++)
		{
			this.rings[j].updateRingPoint(j);
		}
		Vector3 newPos = new Vector3((this.leftRightPadding.y - this.leftRightPadding.x) / 2f, (this.topBotPadding.y - this.topBotPadding.x) / 2f);
		base.changeSpritePositionTo(this.contentParent, newPos);
	}

	private void updateLabelsText()
	{
		for (int i = 0; i < this.rings.Count; i++)
		{
			base.changeLabelText(this.rings[i].labelText, this.labels[i]);
			base.forceUpdateUI();
			base.changeSpriteHeight(this.rings[i].textLine, Mathf.RoundToInt(base.getSpriteWidth(this.rings[i].labelBackground) + 10f));
		}
	}

	private void updateRingsAndBands()
	{
		if (this.bandMode)
		{
			base.SetActive(this.extraRing, true);
			float ringRadius = this.getRingRadius(this.rings.Count);
			WMG_Util.updateBandColors(ref this.extraRingColors, this.outerRadius * 2f, ringRadius - this.ringWidth, ringRadius, this.antiAliasing, this.antiAliasingStrength, null);
			this.extraRingSprite.texture.SetPixels(this.extraRingColors);
			this.extraRingSprite.texture.Apply();
		}
		else
		{
			base.SetActive(this.extraRing, false);
		}
		for (int i = 0; i < this.rings.Count; i++)
		{
			this.rings[i].updateRing(i);
		}
	}

	public float getRingRadius(int index)
	{
		int num = this.rings.Count - 1;
		if (this.bandMode)
		{
			num++;
		}
		if (num == 0)
		{
			return this.outerRadius;
		}
		float num2 = (1f - this.innerRadiusPercentage) * this.outerRadius / (float)num;
		return this.innerRadiusPercentage * this.outerRadius + (float)index * num2;
	}

	private void updateDegrees()
	{
		Vector3 vector = new Vector3(0f, 0f, -this.degrees / 2f);
		float num = (360f - this.degrees) / 360f;
		base.changeRadialSpriteRotation(this.extraRing, vector);
		base.changeSpriteFill(this.extraRing, num);
		bool flag = false;
		for (int i = 0; i < this.rings.Count; i++)
		{
			WMG_Ring ring = this.rings[i];
			base.changeRadialSpriteRotation(this.rings[i].ring, vector);
			base.changeSpriteFill(this.rings[i].ring, num);
			float num2 = this.values[i] / (this.maxValue - this.minValue);
			base.changeRadialSpriteRotation(this.rings[i].band, vector);
			base.changeSpriteFill(this.rings[i].band, 0f);
			if (this.animateData)
			{
				WMG_Anim.animFill(this.rings[i].band, this.animDuration, this.animEaseType, num * num2);
			}
			else
			{
				base.changeSpriteFill(this.rings[i].band, num * num2);
			}
			if (num2 == 0f)
			{
				flag = true;
			}
			int num3 = 0;
			for (int j = i - 1; j >= 0; j--)
			{
				float num4 = this.values[j] / (this.maxValue - this.minValue);
				if (Mathf.Abs(num2 - num4) < 0.01f)
				{
					num3++;
					num2 = num4;
				}
			}
			base.changeSpriteWidth(ring.textLine, 2 + 20 * num3);
			bool labelsOverlap = num3 > 0;
			if (!labelsOverlap)
			{
				base.SetActiveImage(ring.line, true);
				base.changeSpritePivot(ring.textLine, WMG_Text_Functions.WMGpivotTypes.Bottom);
				base.setTexture(ring.textLine, this.labelLineSprite);
				base.setAnchor(ring.labelBackground, new Vector2(0f, 1f), new Vector2(1f, 0f), Vector2.zero);
			}
			else
			{
				base.SetActiveImage(ring.line, false);
			}
			Vector3 vector2 = new Vector3(0f, 0f, -num2 * (360f - this.degrees));
			if (this.animateData)
			{
				if (DOTween.IsTweening(this.rings[i].label.transform))
				{
					this.labelRotationUpdated(ring, 0f, labelsOverlap);
					float degOffset = 90f;
					if (ring.label.transform.localEulerAngles.z < 180f)
					{
						degOffset *= -1f;
					}
					WMG_Anim.animRotation(this.rings[i].label, this.animDuration, this.animEaseType, vector2 + new Vector3(0f, 0f, 360f) + vector, false);
					WMG_Anim.animRotationCallbackU(this.rings[i].textLine, this.animDuration, this.animEaseType, -vector2 - vector + new Vector3(0f, 0f, degOffset), false, delegate
					{
						this.labelRotationUpdated(ring, degOffset, labelsOverlap);
					});
				}
				else
				{
					this.rings[i].label.transform.localEulerAngles = vector;
					this.rings[i].textLine.transform.localEulerAngles = -vector + new Vector3(0f, 0f, 90f);
					WMG_Anim.animRotation(this.rings[i].label, this.animDuration, this.animEaseType, vector2, true);
					WMG_Anim.animRotationCallbackU(this.rings[i].textLine, this.animDuration, this.animEaseType, -vector2, true, delegate
					{
						this.labelRotationUpdated(ring, 0f, labelsOverlap);
					});
				}
			}
			else
			{
				this.rings[i].label.transform.localEulerAngles = vector2 + vector;
				this.rings[i].textLine.transform.localEulerAngles = -vector2 - vector + new Vector3(0f, 0f, 90f);
				this.labelRotationUpdated(ring, 0f, labelsOverlap);
			}
		}
		this.zeroLine.transform.localEulerAngles = vector;
		this.zeroLineText.transform.localEulerAngles = -vector;
		base.SetActive(this.zeroLine, !flag);
	}

	private void labelRotationUpdated(WMG_Ring ring, float degOffset, bool labelsOverlap)
	{
		if (ring.label.transform.localEulerAngles.z < 180f)
		{
			if (labelsOverlap)
			{
				base.changeSpritePivot(ring.textLine, WMG_Text_Functions.WMGpivotTypes.BottomLeft);
				base.setTexture(ring.textLine, this.botRightCorners);
				base.setAnchor(ring.labelBackground, Vector2.one, new Vector2(1f, 0f), Vector2.zero);
			}
			if (degOffset == 0f || degOffset == 90f)
			{
				ring.textLine.transform.localEulerAngles = new Vector3(ring.textLine.transform.localEulerAngles.x, ring.textLine.transform.localEulerAngles.y, ring.textLine.transform.localEulerAngles.z - 180f);
			}
			ring.labelBackground.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
			base.changeSpritePivot(ring.labelBackground, WMG_Text_Functions.WMGpivotTypes.BottomRight);
		}
		else
		{
			if (labelsOverlap)
			{
				base.changeSpritePivot(ring.textLine, WMG_Text_Functions.WMGpivotTypes.BottomRight);
				base.setTexture(ring.textLine, this.botLeftCorners);
				base.setAnchor(ring.labelBackground, new Vector2(0f, 1f), new Vector2(1f, 0f), Vector2.zero);
			}
			if (degOffset == -90f)
			{
				ring.textLine.transform.localEulerAngles = new Vector3(ring.textLine.transform.localEulerAngles.x, ring.textLine.transform.localEulerAngles.y, ring.textLine.transform.localEulerAngles.z + 180f);
			}
			ring.labelBackground.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
			base.changeSpritePivot(ring.labelBackground, WMG_Text_Functions.WMGpivotTypes.BottomLeft);
		}
	}

	public List<int> getRingsSortedByValue()
	{
		List<float> list = new List<float>(this.values);
		list.Sort();
		List<int> list2 = new List<int>();
		for (int i = 0; i < list.Count; i++)
		{
			for (int j = 0; j < this.values.Count; j++)
			{
				if (Mathf.Approximately(this.values[j], list[i]))
				{
					list2.Add(j);
					break;
				}
			}
		}
		return list2;
	}

	private void updateRingColors()
	{
		base.changeSpriteColor(this.extraRing, this.ringColor);
		for (int i = 0; i < this.rings.Count; i++)
		{
			base.changeSpriteColor(this.rings[i].ring, this.ringColor);
		}
	}

	private void updateBandColors()
	{
		for (int i = 0; i < this.rings.Count; i++)
		{
			if (this.autoUpdateBandAlpha)
			{
				this.bandColors.SetValNoCb(i, new Color(this.bandColors[i].r, this.bandColors[i].g, this.bandColors[i].b, ((float)i + 1f) / (float)this.rings.Count), ref this._bandColors);
			}
			base.changeSpriteColor(this.rings[i].band, this.bandColors[i]);
		}
	}

	public WMG_Ring getRing(string id)
	{
		for (int i = 0; i < this.ringIDs.Count; i++)
		{
			if (id == this.ringIDs[i])
			{
				return this.rings[i];
			}
		}
		UnityEngine.Debug.LogError("No ring found with id: " + id);
		return null;
	}

	public void HighlightRing(string id)
	{
		for (int i = 0; i < this.rings.Count; i++)
		{
			base.changeSpriteColor(this.rings[i].band, new Color(this.bandColor.r, this.bandColor.g, this.bandColor.b, 0f));
		}
		base.changeSpriteColor(this.getRing(id).band, new Color(this.bandColor.r, this.bandColor.g, this.bandColor.b, 1f));
	}

	public void RemoveHighlights()
	{
		this.bandColorC.Changed();
	}

	[SerializeField]
	private List<Color> _bandColors;

	public WMG_List<Color> bandColors = new WMG_List<Color>();

	[SerializeField]
	private List<float> _values;

	public WMG_List<float> values = new WMG_List<float>();

	[SerializeField]
	private List<string> _labels;

	public WMG_List<string> labels = new WMG_List<string>();

	[SerializeField]
	private List<string> _ringIDs;

	public WMG_List<string> ringIDs = new WMG_List<string>();

	public bool animateData;

	public float animDuration;

	public Ease animEaseType;

	public UnityEngine.Object ringPrefab;

	public GameObject extraRing;

	public GameObject background;

	public GameObject zeroLine;

	public GameObject zeroLineText;

	public GameObject ringsParent;

	public GameObject ringLabelsParent;

	public GameObject contentParent;

	public WMG_Data_Source valuesDataSource;

	public WMG_Data_Source labelsDataSource;

	public WMG_Data_Source ringIDsDataSource;

	public Sprite labelLineSprite;

	public Sprite botLeftCorners;

	public Sprite botRightCorners;

	[SerializeField]
	private bool _bandMode;

	[SerializeField]
	private float _innerRadiusPercentage;

	[SerializeField]
	private float _degrees;

	[SerializeField]
	private float _minValue;

	[SerializeField]
	private float _maxValue;

	[SerializeField]
	private Color _bandColor;

	[SerializeField]
	private bool _autoUpdateBandAlpha;

	[SerializeField]
	private Color _ringColor;

	[SerializeField]
	private float _ringWidth;

	[SerializeField]
	private float _ringPointWidthFactor;

	[SerializeField]
	private float _bandPadding;

	[SerializeField]
	private float _labelLinePadding;

	[SerializeField]
	private Vector2 _leftRightPadding;

	[SerializeField]
	private Vector2 _topBotPadding;

	[SerializeField]
	private bool _antiAliasing;

	[SerializeField]
	private float _antiAliasingStrength;

	private float origGraphWidth;

	private float containerWidthCached;

	private float containerHeightCached;

	private Sprite extraRingSprite;

	private Color[] extraRingColors;

	private int ringTexSize;

	private List<WMG_Change_Obj> changeObjs = new List<WMG_Change_Obj>();

	private WMG_Change_Obj numberRingsC = new WMG_Change_Obj();

	private WMG_Change_Obj bandColorC = new WMG_Change_Obj();

	private WMG_Change_Obj ringColorC = new WMG_Change_Obj();

	private WMG_Change_Obj labelsC = new WMG_Change_Obj();

	private WMG_Change_Obj degreesC = new WMG_Change_Obj();

	private WMG_Change_Obj radiusC = new WMG_Change_Obj();

	private WMG_Change_Obj textureC = new WMG_Change_Obj();

	private bool hasInit;
}
