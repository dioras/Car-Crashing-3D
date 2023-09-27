using System;
using System.Collections.Generic;
using Battlehub.Integration;
using Battlehub.MeshTools;
using UnityEngine;

public class BodyPartsSwitcher : MonoBehaviour
{
	public void GenerateRimsTexture()
	{
		if (this.FRimsColor == Color.clear)
		{
			this.FRimsColor = Color.grey;
		}
		if (this.RRimsColor == Color.clear)
		{
			this.RRimsColor = Color.grey;
		}
		if (this.FBeadlocksColor == Color.clear)
		{
			this.FBeadlocksColor = new Color(0.1f, 0.1f, 0.1f, 1f);
		}
		if (this.RBeadlocksColor == Color.clear)
		{
			this.RBeadlocksColor = new Color(0.1f, 0.1f, 0.1f, 1f);
		}
		this.FrontRimTexture = new Texture2D(2, 2);
		this.RearRimTexture = new Texture2D(2, 2);
		this.FrontRimTexture.filterMode = FilterMode.Point;
		this.FrontRimTexture.SetPixel(1, 1, this.FBeadlocksColor);
		this.FrontRimTexture.SetPixel(0, 1, this.FRimsColor);
		this.FrontRimTexture.SetPixel(1, 0, Color.white);
		this.FrontRimTexture.SetPixel(0, 0, new Color(0.2f, 0.2f, 0.2f, 1f));
		this.FrontRimTexture.Apply();
		this.RearRimTexture.filterMode = FilterMode.Point;
		this.RearRimTexture.SetPixel(1, 1, this.RBeadlocksColor);
		this.RearRimTexture.SetPixel(0, 1, this.RRimsColor);
		this.RearRimTexture.SetPixel(1, 0, Color.white);
		this.RearRimTexture.SetPixel(0, 0, new Color(0.2f, 0.2f, 0.2f, 1f));
		this.RearRimTexture.Apply();
		this.propBlock = new MaterialPropertyBlock();
		if (!this.suspensionController.FrontWheelsControls.TankTracks && !this.suspensionController.CurrentFrontSuspension.DontLoadWheels)
		{
			foreach (GameObject gameObject in this.suspensionController.FrontRims)
			{
				foreach (Renderer renderer in gameObject.GetComponentsInChildren<Renderer>())
				{
					renderer.GetPropertyBlock(this.propBlock);
					this.propBlock.SetTexture("_Texture", this.FrontRimTexture);
					renderer.SetPropertyBlock(this.propBlock);
				}
			}
		}
		if (!this.suspensionController.RearWheelsControls.TankTracks && !this.suspensionController.CurrentRearSuspension.DontLoadWheels)
		{
			foreach (GameObject gameObject2 in this.suspensionController.RearRims)
			{
				foreach (Renderer renderer2 in gameObject2.GetComponentsInChildren<Renderer>())
				{
					renderer2.GetPropertyBlock(this.propBlock);
					this.propBlock.SetTexture("_Texture", this.RearRimTexture);
					renderer2.SetPropertyBlock(this.propBlock);
				}
			}
		}
	}

	public Transform TrailerMountPos(bool gooseneck)
	{
		if (gooseneck)
		{
			return this.gooseneckMount;
		}
		return this.RearWinchPoint;
	}

	public void UpdateEngineModel(EngineType engineType)
	{
		if (this.StockEngine != null)
		{
			this.StockEngine.SetActive(engineType == EngineType.Stock);
		}
		if (this.TurboEngine != null)
		{
			this.TurboEngine.SetActive(engineType == EngineType.Turbo);
		}
		if (this.BlowerEngine != null)
		{
			this.BlowerEngine.SetActive(engineType == EngineType.Blower);
		}
	}

	public void InstallBodyPart(PartGroup group, int partID)
	{
		for (int i = 0; i < group.Parts.Length; i++)
		{
			if (group.Parts[i] != null)
			{
				group.Parts[i].SetActive(i == partID);
			}
		}
		group.InstalledPart = partID;
		this.CheckTriggerPartGroups();
		this.CheckDynamicParts();
		group.PaintPart();
		this.CheckTriggerPartGroups();
	}

	public void CheckTriggerPartGroups()
	{
		if (this.triggerPartGroups != null)
		{
			foreach (TriggerPartGroup triggerPartGroup in this.triggerPartGroups)
			{
				bool flag = false;
				foreach (GameObject gameObject in triggerPartGroup.TriggerParts)
				{
					if (gameObject.activeSelf)
					{
						flag = true;
					}
				}
				foreach (GameObject gameObject2 in triggerPartGroup.PartsToToggle)
				{
					if (!triggerPartGroup.invert)
					{
						gameObject2.SetActive(!flag);
					}
					else
					{
						gameObject2.SetActive(flag);
					}
				}
			}
		}
	}

	public void CheckDynamicParts()
	{
		if (this.dynamicParts != null)
		{
			foreach (DynamicPositionPart dynamicPositionPart in this.dynamicParts)
			{
				dynamicPositionPart.UpdatePosition();
			}
		}
	}

	private void Awake()
	{
		this.photonView = base.gameObject.GetPhotonView();
		if (this.photonView.isMine || GameState.GameMode != GameMode.Multiplayer)
		{
			this.carUIControl = CarUIControl.Instance;
		}
		this.suspensionController = base.GetComponent<SuspensionController>();
		this.propBlock = new MaterialPropertyBlock();
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (!this.MeshesMerged && GameState.GameMode == GameMode.Multiplayer && PhotonNetwork.inRoom)
		{
			return;
		}
		if (this.Washing)
		{
			this.Dirtiness = Mathf.Lerp(this.Dirtiness, 0f, Time.deltaTime * this.WashingSpeed);
			if (this.Dirtiness < 0.1f)
			{
				this.Washing = false;
				this.Dirtiness = 0f;
				if (MenuManager.Instance.CurrentVehicle != null)
				{
					MenuManager.Instance.DoneWashing();
					MenuManager.Instance.CurrentVehicle.SaveVehicleData();
				}
			}
			this.UpdateDirtiness();
		}
		//print(MeshesMerged);
	}

	public void UpdateDirtiness()
	{
		if (this.MeshesMerged)
		{
			this.UpdateMudValuesMerged();
		}
		else
		{
			this.UpdateMudValuesUnmerged();
		}
	}

	public void UpdateColor(bool Merge)
	{
		if (!Application.isPlaying)
		{
			return;
		}
		if (Merge)
		{
			this.MergeBodyParts();
		}
		if (this.MeshesMerged)
		{
			this.UpdateBodyColorMerged();
		}
		else
		{
			this.UpdateBodyColorUnmerged();
		}
		this.GenerateRimsTexture();
	}

	public void WashVehicle()
	{
		this.Washing = true;
	}

	public void SetStockModification()
	{
		foreach (PartGroup group in this.partGroups)
		{
			this.InstallBodyPart(group, 0);
		}
		this.FRimsColor = Color.grey;
		this.RRimsColor = Color.grey;
		this.FBeadlocksColor = new Color(0.1f, 0.1f, 0.1f, 1f);
		this.RBeadlocksColor = new Color(0.1f, 0.1f, 0.1f, 1f);
		this.AppliedWrapID = 0;
		this.WrapColor = Color.white;
		this.WrapCoords = new Vector4(0f, 0f, 1f, 1f);
		this.WrapLayers = new List<Wrap>();
		this.IsWrapBaked = false;
		this.UpdateColor(false);
		this.GenerateRimsTexture();
		this.UpdateEngineModel(EngineType.Stock);
	}

	public void SetRandomModification()
	{
		foreach (PartGroup partGroup in this.partGroups)
		{
			this.InstallBodyPart(partGroup, UnityEngine.Random.Range(0, partGroup.Parts.Length));
		}
		this.BodyColor = UnityEngine.Random.ColorHSV();
		this.FRimsColor = UnityEngine.Random.ColorHSV();
		this.FBeadlocksColor = UnityEngine.Random.ColorHSV();
		this.RRimsColor = UnityEngine.Random.ColorHSV();
		this.RBeadlocksColor = UnityEngine.Random.ColorHSV();
		this.UpdateColor(false);
	}

	public void AlwaysUseLOD0()
	{
		foreach (LODGroup lodgroup in base.GetComponentsInChildren<LODGroup>(true))
		{
			lodgroup.ForceLOD(0);
		}
	}

	public void MergeBodyParts()
	{
		if (this.MeshesMerged)
		{
			return;
		}
		this.MeshesMerged = true;
		this.CombineParts();
		this.AllRenderers = this.GetAllRenderers(false);
		this.UpdateBodyColorMerged();
		this.UpdateMudValuesMerged();
	}

	private Renderer[] GetAllRenderers(bool IncludeInactive = false)
	{
		List<Renderer> list = new List<Renderer>();
		List<Shader> list2 = new List<Shader>();
		list2.Add(Shader.Find("Car Crashing 3D/Body"));
		list2.Add(Shader.Find("Car Crashing 3D/Color dirt"));
		list2.Add(Shader.Find("Car Crashing 3D/Diffuse dirt"));
		list2.Add(Shader.Find("Car Crashing 3D/Diffuse dirt with UV0 for dirt"));
		list2.Add(Shader.Find("Car Crashing 3D/Tire"));
		foreach (Renderer renderer in base.GetComponentsInChildren<Renderer>(IncludeInactive))
		{
			foreach (Material material in renderer.sharedMaterials)
			{
				if (material != null && list2.Contains(material.shader))
				{
					list.Add(renderer);
					break;
				}
			}
		}
		return list.ToArray();
	}

	private void UpdateBodyColorMerged()
	{
		if (!this.IsWrapBaked)
		{
			this.BakeWrap();
		}
		Texture texture = Resources.Load("Wraps/Wrap" + this.AppliedWrapID) as Texture;
		if (this.AppliedWrapID == 0)
		{
			texture = null;
		}
		foreach (Renderer renderer in this.AllRenderers)
		{
			renderer.GetPropertyBlock(this.propBlock);
			this.propBlock.SetColor("_PaintColor", this.BodyColor);
			this.propBlock.SetFloat("_ReflectionStrength", (!this.GlossyPaint) ? 0.1f : 1f);
			this.propBlock.SetTexture("_BakedWrap", this.BakedWrap);
			if (texture != null)
			{
				this.propBlock.SetTexture("_Wrap", texture);
				this.propBlock.SetColor("_WrapColor", this.WrapColor);
				this.propBlock.SetVector("_Wrap_ST", new Vector4(this.WrapCoords.w, this.WrapCoords.z, this.WrapCoords.x, this.WrapCoords.y));
			}
			renderer.SetPropertyBlock(this.propBlock);
		}
	}

	public void ChangeCurrentWrap(int ID, Color color, Vector4 coords)
	{
		Renderer[] componentsInChildren = base.GetComponentsInChildren<Renderer>(true);
		Texture texture = Resources.Load("Wraps/Wrap" + ID) as Texture;
		if (ID == 0)
		{
			texture = null;
		}
		foreach (Renderer renderer in componentsInChildren)
		{
			renderer.GetPropertyBlock(this.propBlock);
			this.propBlock.SetColor("_WrapColor", color);
			if (texture != null)
			{
				this.propBlock.SetTexture("_Wrap", texture);
			}
			else
			{
				this.propBlock.SetColor("_WrapColor", Color.clear);
			}
			this.propBlock.SetVector("_Wrap_ST", new Vector4(coords.w, coords.z, coords.x, coords.y));
			renderer.SetPropertyBlock(this.propBlock);
		}
		this.CurrentWrapID = ID;
		this.CurrentWrapColor = color;
		this.CurrentWrapCoords = coords;
		this.AppliedWrapID = ID;
		this.WrapColor = color;
		this.WrapCoords = coords;
	}

	public void ClearWraps()
	{
		this.WrapLayers = new List<Wrap>();
		this.AppliedWrapID = 0;
		this.IsWrapBaked = false;
		this.UpdateBodyColorUnmerged();
	}

	public void BakeWrap()
	{
		RenderTexture renderTexture = new RenderTexture(512, 512, 0);
		this.BakedWrap = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
		Renderer[] allRenderers = this.GetAllRenderers(false);
		Material material = new Material(Shader.Find("Car Crashing 3D/Bake"));
		for (int i = 0; i < this.WrapLayers.Count; i++)
		{
			Texture value = Resources.Load("Wraps/Wrap" + this.WrapLayers[i].ID) as Texture;
			material.SetTexture("_Wrap" + i, value);
			material.SetColor("_WrapColor" + i, this.WrapLayers[i].color);
			material.SetTextureOffset("_Wrap" + i, new Vector2(this.WrapLayers[i].Coords.x, this.WrapLayers[i].Coords.y));
			material.SetTextureScale("_Wrap" + i, new Vector2(this.WrapLayers[i].Coords.w, this.WrapLayers[i].Coords.z));
		}
		if (this.CurrentWrapID != 0)
		{
			int count = this.WrapLayers.Count;
			Texture value2 = Resources.Load("Wraps/Wrap" + this.CurrentWrapID) as Texture;
			material.SetTexture("_Wrap" + count, value2);
			material.SetColor("_WrapColor" + count, this.CurrentWrapColor);
			material.SetTextureOffset("_Wrap" + count, new Vector2(this.CurrentWrapCoords.x, this.CurrentWrapCoords.y));
			material.SetTextureScale("_Wrap" + count, new Vector2(this.CurrentWrapCoords.w, this.CurrentWrapCoords.z));
			this.WrapLayers.Add(new Wrap(this.CurrentWrapID, this.CurrentWrapCoords, this.CurrentWrapColor));
		}
		this.SessionWrapLayerCount = this.WrapLayers.Count;
		if (this.WrapLayers.Count > this.WrapLayerCount)
		{
			this.WrapLayerCount = this.WrapLayers.Count;
		}
		Graphics.Blit(null, renderTexture, material);
		RenderTexture.active = renderTexture;
		this.BakedWrap.ReadPixels(new Rect(0f, 0f, (float)renderTexture.width, (float)renderTexture.height), 0, 0);
		this.BakedWrap.Apply();
		RenderTexture.active = null;
		foreach (Renderer renderer in allRenderers)
		{
			renderer.GetPropertyBlock(this.propBlock);
			this.propBlock.SetTexture("_BakedWrap", this.BakedWrap);
			renderer.SetPropertyBlock(this.propBlock);
		}
		this.IsWrapBaked = true;
	}

	private void UpdateBodyColorUnmerged()
	{
		Renderer[] allRenderers = this.GetAllRenderers(true);
		Texture texture = Resources.Load("Wraps/Wrap" + this.AppliedWrapID) as Texture;
		if (this.AppliedWrapID == 0)
		{
			texture = null;
		}
		if (!this.IsWrapBaked)
		{
			this.BakeWrap();
		}
		foreach (Renderer renderer in allRenderers)
		{
			renderer.GetPropertyBlock(this.propBlock);
			this.propBlock.SetColor("_PaintColor", this.BodyColor);
			this.propBlock.SetFloat("_ReflectionStrength", (!this.GlossyPaint) ? 0.1f : 1f);
			this.propBlock.SetTexture("_BakedWrap", this.BakedWrap);
			if (texture != null)
			{
				this.propBlock.SetTexture("_Wrap", texture);
				this.propBlock.SetColor("_WrapColor", this.WrapColor);
				this.propBlock.SetVector("_Wrap_ST", new Vector4(this.WrapCoords.w, this.WrapCoords.z, this.WrapCoords.x, this.WrapCoords.y));
			}
			renderer.SetPropertyBlock(this.propBlock);
		}
	}

	private void UpdateMudValuesUnmerged()
	{
		Renderer[] allRenderers = this.GetAllRenderers(false);
		Color dryMudColor = VehicleParts.DryMudColor;
		dryMudColor.a = 1f - this.Dirtiness;
		foreach (Renderer renderer in allRenderers)
		{
			renderer.GetPropertyBlock(this.propBlock);
			this.propBlock.SetColor("_DirtColor", dryMudColor);
			renderer.SetPropertyBlock(this.propBlock);
		}
	}

	private void UpdateMudValuesMerged()
	{
		Color value = Color.Lerp(VehicleParts.DryMudColor, VehicleParts.WetMudColor, this.MudWetness);
		value.a = 1f - this.Dirtiness;
		foreach (Renderer renderer in this.AllRenderers)
		{
			if (renderer == null)
			{
				this.AllRenderers = this.GetAllRenderers(false);
				return;
			}
			renderer.GetPropertyBlock(this.propBlock);
			this.propBlock.SetColor("_DirtColor", value);
			renderer.SetPropertyBlock(this.propBlock);
		}
	}

	private BodyPartsData GetBodyPartsData()
	{
		BodyPartsData bodyPartsData = new BodyPartsData();
		bodyPartsData.Dirtiness = this.Dirtiness;
		bodyPartsData.BodyColor = this.BodyColor;
		bodyPartsData.FRimsColor = this.FRimsColor;
		bodyPartsData.FBeadlocksColor = this.FBeadlocksColor;
		bodyPartsData.RRimsColor = this.RRimsColor;
		bodyPartsData.RBeadlocksColor = this.RBeadlocksColor;
		bodyPartsData.Wraps = this.WrapLayers;
		bodyPartsData.WrapColor = this.WrapColor;
		bodyPartsData.WrapID = this.AppliedWrapID;
		bodyPartsData.WrapCoords = this.WrapCoords;
		bodyPartsData.GlossyPaint = this.GlossyPaint;
		bodyPartsData.GlossyPaintPurchased = this.GlossyPaintPurchased;
		PartGroupData[] array = new PartGroupData[this.partGroups.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = this.partGroups[i].returnData();
		}
		bodyPartsData.partGroupsData = array;
		return bodyPartsData;
	}

	[ContextMenu("Export data")]
	public string ExportData()
	{
		BodyPartsData bodyPartsData = this.GetBodyPartsData();
		return XmlSerialization.SerializeData<BodyPartsData>(bodyPartsData);
	}

	public void ImportData(string XmlString)
	{
		BodyPartsData bodyPartsData = new BodyPartsData();
		bodyPartsData = (BodyPartsData)XmlSerialization.DeserializeData<BodyPartsData>(XmlString);
		this.Dirtiness = bodyPartsData.Dirtiness;
		this.BodyColor = bodyPartsData.BodyColor;
		this.FRimsColor = bodyPartsData.FRimsColor;
		this.FBeadlocksColor = bodyPartsData.FBeadlocksColor;
		this.RRimsColor = bodyPartsData.RRimsColor;
		this.RBeadlocksColor = bodyPartsData.RBeadlocksColor;
		this.WrapLayers = bodyPartsData.Wraps;
		this.WrapColor = bodyPartsData.WrapColor;
		this.AppliedWrapID = bodyPartsData.WrapID;
		this.WrapCoords = bodyPartsData.WrapCoords;
		this.GlossyPaint = bodyPartsData.GlossyPaint;
		this.GlossyPaintPurchased = bodyPartsData.GlossyPaintPurchased;
		this.WrapLayerCount = bodyPartsData.WrapLayerCount;
		if (this.WrapCoords == Vector4.zero)
		{
			this.WrapCoords = new Vector4(0f, 0f, 1f, 1f);
		}
		if (this.AppliedWrapID > 0)
		{
			this.WrapLayers.Add(new Wrap(this.AppliedWrapID, this.WrapCoords, this.WrapColor));
			this.AppliedWrapID = 0;
		}
		foreach (PartGroup partGroup in this.partGroups)
		{
			foreach (PartGroupData partGroupData in bodyPartsData.partGroupsData)
			{
				if (partGroup.GroupName.Equals(partGroupData.GroupName))
				{
					partGroup.color = partGroupData.color;
					this.InstallBodyPart(partGroup, partGroupData.InstalledPart);
				}
			}
		}
		if (this.Winch != null)
		{
			this.WinchInstalled = this.Winch.activeSelf;
		}
		if (this.RepairPack != null && !this.MeshesMerged)
		{
			this.RepairPackInstalled = this.RepairPack.activeSelf;
		}
		if ((GameState.GameMode == GameMode.Multiplayer && this.photonView.isMine) || GameState.GameMode != GameMode.Multiplayer)
		{
			this.AlwaysUseLOD0();
		}
		this.GenerateRimsTexture();
	}

	private void CombineParts()
	{
		if (!Application.isPlaying)
		{
			UnityEngine.Debug.LogError("Combining parts works in play mode only");
			return;
		}
		List<GameObject> list = new List<GameObject>();
		list.Add(this.BaseMesh);
		foreach (PartGroup partGroup in this.partGroups)
		{
			if (partGroup.Parts[partGroup.InstalledPart] != null && partGroup.Parts[partGroup.InstalledPart].activeInHierarchy && !partGroup.dontBake)
			{
				list.Add(partGroup.Parts[partGroup.InstalledPart]);
			}
		}
		int count = list.Count;
		for (int j = 0; j < count; j++)
		{
			for (int k = 0; k < list[j].transform.childCount; k++)
			{
				if (list[j].transform.GetChild(k).GetComponent<MeshRenderer>() != null && list[j].activeSelf && list[j].transform.GetChild(k).gameObject.activeSelf)
				{
					list.Add(list[j].transform.GetChild(k).gameObject);
				}
				else
				{
					list[j].transform.GetChild(k).parent = list[j].transform.parent;
					k--;
				}
			}
		}
		GameObject[] gameObjects = new GameObject[list.Count];
		gameObjects = list.ToArray();
		CombineResult combineResult = MeshUtils.Combine(gameObjects, null);
		this.ResultMesh = combineResult.GameObject;
		if (combineResult != null)
		{
			MeshCombinerIntegration.RaiseCombined(combineResult.GameObject, combineResult.Mesh);
			combineResult.GameObject.GetComponent<MeshRenderer>().receiveShadows = false;
		}
	}

	[HideInInspector]
	public Renderer[] AllRenderers;

	public Color BodyColor;

	public bool GlossyPaint;

	public bool GlossyPaintPurchased;

	public bool noPaintAllowed;

	public List<Wrap> WrapLayers = new List<Wrap>();

	public Color WrapColor;

	public Vector4 WrapCoords;

	public int AppliedWrapID;

	public int WrapLayerCount;

	public int SessionWrapLayerCount;

	[HideInInspector]
	public int CurrentWrapID;

	[HideInInspector]
	public Color CurrentWrapColor;

	[HideInInspector]
	public Vector4 CurrentWrapCoords = new Vector4(0f, 0f, 1f, 1f);

	public Color FRimsColor;

	public Color FBeadlocksColor;

	public Color RRimsColor;

	public Color RBeadlocksColor;

	[HideInInspector]
	public float MudWetness;

	[HideInInspector]
	public float Dirtiness;

	[Space(10f)]
	public GameObject BaseMesh;

	public GameObject Winch;

	public Transform FrontWinchPoint;

	public Transform RearWinchPoint;

	public Transform gooseneckMount;

	public GameObject RepairPack;

	[SerializeField]
	public PartGroup[] partGroups;

	[SerializeField]
	public DynamicPositionPart[] dynamicParts;

	[SerializeField]
	public TriggerPartGroup[] triggerPartGroups;

	[SerializeField]
	public GameObject StockEngine;

	[SerializeField]
	public GameObject BlowerEngine;

	[SerializeField]
	public GameObject TurboEngine;

	private bool MeshesMerged;

	private GameObject ResultMesh;

	private MenuManager menuManager;

	[HideInInspector]
	public bool Washing;

	private float WashingSpeed = 1f;

	[HideInInspector]
	public bool WinchInstalled;

	[HideInInspector]
	public bool RepairPackInstalled;

	private PhotonView photonView;

	private SuspensionController suspensionController;

	private CarUIControl carUIControl;

	private Texture2D BakedWrap;

	private bool IsWrapBaked;

	private MaterialPropertyBlock propBlock;

	private Texture2D FrontRimTexture;

	private Texture2D RearRimTexture;
}
