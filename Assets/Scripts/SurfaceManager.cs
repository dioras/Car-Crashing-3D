using System;
using System.Collections.Generic;
using CustomVP;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class SurfaceManager : MonoBehaviour
{
	private CarController carController
	{
		get
		{
			if (VehicleLoader.Instance != null)
			{
				return VehicleLoader.Instance.playerCarController;
			}
			return null;
		}
	}

	private SuspensionController suspensionController
	{
		get
		{
			if (VehicleLoader.Instance != null)
			{
				return VehicleLoader.Instance.playerSuspensionController;
			}
			return null;
		}
	}

	private BodyPartsSwitcher partsSwitcher
	{
		get
		{
			if (VehicleLoader.Instance != null)
			{
				return VehicleLoader.Instance.playerPartsSwitcher;
			}
			return null;
		}
	}

	private Rigidbody playerRigidbody
	{
		get
		{
			if (VehicleLoader.Instance != null)
			{
				return VehicleLoader.Instance.playerRigidbody;
			}
			return null;
		}
	}

	private void Awake()
	{
		SurfaceManager.Instance = this;
	}

	private void Start()
	{
		if (this.BaseTerrain == null)
		{
			UnityEngine.Debug.LogError("Base terrain is not assigned. Surface manager is off.");
			base.enabled = false;
			return;
		}
		this.InitializeBaseTerrain();
		this.LoadParticles();
		this.MudTerrains = this.BaseTerrain.GetComponentsInChildren<MudTerrain>();
		this.CurrentQuality = QualitySettings.GetQualityLevel();
		this.BlurScript = Camera.main.GetComponent<BlurOptimized>();
		if (this.BlurScript == null)
		{
			this.BlurScript = Camera.main.gameObject.AddComponent<BlurOptimized>();
			this.BlurScript.downsample = 2;
			this.BlurScript.blurIterations = 1;
			this.BlurScript.blurShader = Shader.Find("Hidden/FastBlur");
		}
		if (this.BlurScript != null)
		{
			this.BlurScript.enabled = false;
		}
	}

	private void OnEnable()
	{
		this.FindWater();
	}

	private void UpdateTireMarks(int WheelID)
	{
		if (this.carController == null)
		{
			return;
		}
		if (WheelID >= this.carController.WheelsCount)
		{
			this.InitializeTireFxData();
			return;
		}
		this.marksRenderer = this.m_fxData[WheelID].marksRenderer;
		if (this.SnowTracksRenderers != null)
		{
			for (int i = 0; i < this.m_fxData.Length; i++)
			{
				bool renderTrack = false;
				if (this.m_fxData[i].surfaceMaterial != null && this.m_fxData[i].surfaceMaterial.tireTracks == SurfaceMaterial.TireTracks.SnowTrack)
				{
					renderTrack = true;
				}
				this.SnowTracksRenderers[i].RenderTrack = renderTrack;
				this.SnowTracksRenderers[i].TireWidth = 0.4f * ((i <= this.carController.wheels.Count / 2 - 1) ? this.suspensionController.FrontWheelsControls.WheelsWidth.FloatValue : this.suspensionController.RearWheelsControls.WheelsWidth.FloatValue);
			}
		}
		TireFxData tireFxData = this.m_fxData[WheelID];
		_Wheel wheel = this.carController.wheels[WheelID];
		float width = this.suspensionController.FrontWheelsControls.WheelsWidth.FloatValue * 0.3f;
		if (WheelID > 1 && this.carController.wheels.Count >= 4)
		{
			width = this.suspensionController.RearWheelsControls.WheelsWidth.FloatValue * 0.3f;
		}
		if (tireFxData.lastMarksIndex != -1 && wheel.wc.IsGrounded && tireFxData.marksDelta < this.tireMarksUpdateInterval)
		{
			tireFxData.marksDelta += Time.deltaTime;
			return;
		}
		float num = tireFxData.marksDelta;
		if (num == 0f)
		{
			num = Time.deltaTime;
		}
		tireFxData.marksDelta = 0f;

		if (!wheel.wc.IsGrounded || (wheel.wc.wheelCollider.contactColliderHit && wheel.wc.wheelCollider.contactColliderHit.attachedRigidbody != null))
		{
			tireFxData.lastMarksIndex = -1;
			return;
		}
		if (this.marksRenderer != tireFxData.lastRenderer)
		{
			tireFxData.lastRenderer = this.marksRenderer;
			tireFxData.lastMarksIndex = -1;
		}
		if (this.marksRenderer != null)
		{
			Vector3 b = this.carController.Speed * wheel.wc.transform.forward * 0.01f + wheel.wc.transform.forward * num;
			tireFxData.lastMarksIndex = this.marksRenderer.AddMark(wheel.wc.wheelCollider.realHitPoint + b, wheel.wc.wheelCollider.contactNormal, width, tireFxData.lastMarksIndex);
		}
	}

	public void FindWater()
	{
		this.WaterMeshes = new List<GameObject>();
		this.WaterMeshesRects = new List<Rect>();
		foreach (Renderer renderer in UnityEngine.Object.FindObjectsOfType<Renderer>())
		{
			if (renderer.sharedMaterial != null && renderer.sharedMaterial.Equals(this.WaterMaterial))
			{
				this.WaterMeshes.Add(renderer.gameObject);
				this.WaterMeshesRects.Add(new Rect(renderer.bounds.min.x, renderer.bounds.min.z, renderer.bounds.size.x, renderer.bounds.size.z));
			}
		}
	}

	public void RemoveMudTerrains(List<MudStamp> mudStamps = null)
	{
		this.InitializeBaseTerrain();
		MudTerrain[] array = UnityEngine.Object.FindObjectsOfType<MudTerrain>();
		if (array.Length == 0)
		{
			return;
		}
		float[,] heights = this.BaseTerrain.terrainData.GetHeights(0, 0, this.hRes, this.hRes);
		UnityEngine.Random.InitState(0);
		for (int i = 0; i < this.aRes; i++)
		{
			for (int j = 0; j < this.aRes; j++)
			{
				Vector3 position = this.BaseTerrain.transform.position;
				position.x += (float)i / (float)this.hRes * this.BaseTerrain.terrainData.size.x;
				position.z += (float)j / (float)this.hRes * this.BaseTerrain.terrainData.size.z;
				foreach (MudTerrain mudTerrain in array)
				{
					if (mudTerrain.terRect.Contains(new Vector2(position.x, position.z)))
					{
						this.SurfaceMaterialUnderCar = this.GetSurfaceMaterialByTerrainCoords(new Vector2((float)i, (float)j));
						Vector3 point = this.BaseTerrain.GetPosition() + new Vector3((float)i / (float)this.hRes * this.BaseTerrain.terrainData.size.x, 0f, (float)j / (float)this.hRes * this.BaseTerrain.terrainData.size.z);
						float num = mudTerrain.DeformableMaterialMaxDepth / this.BaseTerrain.terrainData.size.y;
						if (mudStamps != null)
						{
							foreach (MudStamp mudStamp in mudStamps)
							{
								if (this.DoesPointBelongToMudStamp(mudStamp, point))
								{
									num = mudStamp.mudDepth / this.BaseTerrain.terrainData.size.y;
								}
							}
						}
						float num2 = 0f;
						if (this.SurfaceMaterialUnderCar != null && this.SurfaceMaterialUnderCar.surfaceType == SurfaceMaterial.SurfaceType.Mud)
						{
							num2 = num;
						}
						if (num2 != 0f)
						{
							num2 += UnityEngine.Random.Range(0.1f / this.BaseTerrain.terrainData.size.y, 0.7f / this.BaseTerrain.terrainData.size.y);
						}
						heights[j, i] += num2;
					}
				}
			}
		}
		this.BaseTerrain.terrainData.SetHeights(0, 0, heights);
		int num3 = array.Length;
		for (int l = 0; l < num3; l++)
		{
			UnityEngine.Object.DestroyImmediate(array[l].gameObject);
		}
		UnityEngine.Debug.Log(num3 + " mud terrains were successfully removed");
	}

	private void InitializeTireFxData()
	{
		this.m_fxData = new TireFxData[this.carController.WheelsCount];
		for (int i = 0; i < this.m_fxData.Length; i++)
		{
			this.m_fxData[i] = new TireFxData();
		}
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		bool flag5 = false;
		foreach (SurfaceMaterial surfaceMaterial in this.surfaceMaterials)
		{
			if (surfaceMaterial.tireTracks == SurfaceMaterial.TireTracks.MudMark)
			{
				flag3 = true;
			}
			if (surfaceMaterial.tireTracks == SurfaceMaterial.TireTracks.SandMark)
			{
				flag2 = true;
			}
			if (surfaceMaterial.tireTracks == SurfaceMaterial.TireTracks.SnowMark)
			{
				flag = true;
			}
			if (surfaceMaterial.tireTracks == SurfaceMaterial.TireTracks.SnowTrack)
			{
				flag4 = true;
			}
			if (surfaceMaterial.tireTracks == SurfaceMaterial.TireTracks.SkidMark)
			{
				flag5 = true;
			}
		}
		if (this.SnowMarksRenderer != null)
		{
			UnityEngine.Object.Destroy(this.SnowMarksRenderer.gameObject);
		}
		if (this.SandMarksRenderer != null)
		{
			UnityEngine.Object.Destroy(this.SandMarksRenderer.gameObject);
		}
		if (this.MudMarksRenderer != null)
		{
			UnityEngine.Object.Destroy(this.MudMarksRenderer.gameObject);
		}
		if (this.SkidMarksRenderer != null)
		{
			UnityEngine.Object.Destroy(this.SkidMarksRenderer.gameObject);
		}
		if (this.SnowTracksRenderers != null && this.SnowTracksRenderers.Length > 0)
		{
			foreach (VolumetricTireTrackRenderer volumetricTireTrackRenderer in this.SnowTracksRenderers)
			{
				UnityEngine.Object.Destroy(volumetricTireTrackRenderer.gameObject);
			}
		}
		if (flag)
		{
			this.SnowMarksRenderer = (UnityEngine.Object.Instantiate(Resources.Load("TireTracks/SnowMark", typeof(GameObject))) as GameObject).GetComponent<TireMarksRenderer>();
		}
		if (flag2)
		{
			this.SandMarksRenderer = (UnityEngine.Object.Instantiate(Resources.Load("TireTracks/SandMark", typeof(GameObject))) as GameObject).GetComponent<TireMarksRenderer>();
		}
		if (flag3)
		{
			this.MudMarksRenderer = (UnityEngine.Object.Instantiate(Resources.Load("TireTracks/MudMark", typeof(GameObject))) as GameObject).GetComponent<TireMarksRenderer>();
		}
		if (flag5)
		{
			this.SkidMarksRenderer = (UnityEngine.Object.Instantiate(Resources.Load("TireTracks/SkidMark", typeof(GameObject))) as GameObject).GetComponent<TireMarksRenderer>();
		}
		if (this.carController.tankController != null)
		{
			flag4 = false;
		}
		if (flag4)
		{
			this.SnowTracksRenderers = new VolumetricTireTrackRenderer[this.m_fxData.Length];
			for (int l = 0; l < this.m_fxData.Length; l++)
			{
				this.SnowTracksRenderers[l] = (UnityEngine.Object.Instantiate(Resources.Load("TireTracks/SnowTrack", typeof(GameObject))) as GameObject).GetComponent<VolumetricTireTrackRenderer>();
				this.SnowTracksRenderers[l].wheelCollider = this.carController.wheels[l].wc;
			}
		}
	}

	private void InitializeBaseTerrain()
	{
		SplatPrototype[] splatPrototypes = this.BaseTerrain.terrainData.splatPrototypes;
		this.terrainTextures = new Texture2D[splatPrototypes.Length];
		for (int i = 0; i < this.terrainTextures.Length; i++)
		{
			this.terrainTextures[i] = splatPrototypes[i].texture;
		}
		this.hRes = this.BaseTerrain.terrainData.heightmapResolution;
		this.aRes = this.BaseTerrain.terrainData.alphamapResolution;
	}

	private void LoadParticles()
	{
		this.MudParticles = (UnityEngine.Object.Instantiate(Resources.Load("ParticleEffects/Mud", typeof(GameObject))) as GameObject).GetComponent<ParticleSystem>();
		this.SandDustParticles = (UnityEngine.Object.Instantiate(Resources.Load("ParticleEffects/SandDust", typeof(GameObject))) as GameObject).GetComponent<ParticleSystem>();
		this.SkidSmokeParticles = (UnityEngine.Object.Instantiate(Resources.Load("ParticleEffects/SkidSmoke", typeof(GameObject))) as GameObject).GetComponent<ParticleSystem>();
		this.WaterSplashParticles = (UnityEngine.Object.Instantiate(Resources.Load("ParticleEffects/WaterSplash", typeof(GameObject))) as GameObject).GetComponent<ParticleSystem>();
		this.RippleParticles = (UnityEngine.Object.Instantiate(Resources.Load("ParticleEffects/RippleParticles", typeof(GameObject))) as GameObject).GetComponent<ParticleSystem>();
		this.SnowParticles = (UnityEngine.Object.Instantiate(Resources.Load("ParticleEffects/Snow", typeof(GameObject))) as GameObject).GetComponent<ParticleSystem>();
		this.MudParticles.transform.parent = base.transform;
		this.SandDustParticles.transform.parent = base.transform;
		this.SkidSmokeParticles.transform.parent = base.transform;
		this.WaterSplashParticles.transform.parent = base.transform;
		this.RippleParticles.transform.parent = base.transform;
		this.SnowParticles.transform.parent = base.transform;
	}

	private void OnDrawGizmos()
	{
		if (!this.ShowChunksGizmos)
		{
			return;
		}
		Gizmos.color = this.GizmosColor;
		Terrain baseTerrain = this.BaseTerrain;
		if (baseTerrain == null)
		{
			return;
		}
		for (int i = 0; i < baseTerrain.terrainData.alphamapWidth; i += this.ChunkSize)
		{
			Gizmos.DrawLine(baseTerrain.GetPosition() + new Vector3((float)i / (float)baseTerrain.terrainData.alphamapWidth * baseTerrain.terrainData.size.x, 0f, 0f), baseTerrain.GetPosition() + new Vector3((float)i / (float)baseTerrain.terrainData.alphamapWidth * baseTerrain.terrainData.size.x, 0f, baseTerrain.terrainData.size.z));
		}
		for (int j = 0; j < baseTerrain.terrainData.alphamapHeight; j += this.ChunkSize)
		{
			Gizmos.DrawLine(baseTerrain.GetPosition() + new Vector3(0f, 0f, (float)j / (float)baseTerrain.terrainData.alphamapHeight * baseTerrain.terrainData.size.z), baseTerrain.GetPosition() + new Vector3(baseTerrain.terrainData.size.x, 0f, (float)j / (float)baseTerrain.terrainData.alphamapHeight * baseTerrain.terrainData.size.z));
		}
	}

	public void CreateMudTerrains(List<MudStamp> mudStamps = null)
	{
		if (this.BaseTerrain.terrainData.alphamapResolution + 1 != this.BaseTerrain.terrainData.heightmapResolution)
		{
			UnityEngine.Debug.LogError("Please make -Heightmap resolution- and -Control texture resolution- of Base Terrain equal.");
			base.enabled = false;
			return;
		}
		this.RemoveMudTerrains(null);
		this.InitializeBaseTerrain();
		Vector2[,] array = new Vector2[this.aRes / this.ChunkSize + 1, this.aRes / this.ChunkSize + 1];
		List<Vector2> list = new List<Vector2>();
		List<MudArea> list2 = new List<MudArea>();
		for (int i = 0; i < this.aRes; i++)
		{
			for (int j = 0; j < this.aRes; j++)
			{
				if (i % this.ChunkSize == 0 && j % this.ChunkSize == 0)
				{
					array[i / this.ChunkSize, j / this.ChunkSize] = new Vector2((float)i, (float)j);
				}
			}
		}
		for (int k = 0; k < array.GetUpperBound(0); k++)
		{
			for (int l = 0; l < array.GetUpperBound(1); l++)
			{
				for (int m = 0; m < this.ChunkSize; m++)
				{
					for (int n = 0; n < this.ChunkSize; n++)
					{
						this.SurfaceMaterialUnderCar = this.GetSurfaceMaterialByTerrainCoords(new Vector2((float)(k * this.ChunkSize + m), (float)(l * this.ChunkSize + n)));
						if (this.SurfaceMaterialUnderCar != null && this.SurfaceMaterialUnderCar.surfaceType == SurfaceMaterial.SurfaceType.Mud && !list.Contains(new Vector2((float)k, (float)l)))
						{
							list.Add(new Vector2((float)k, (float)l));
						}
					}
				}
			}
		}
		UnityEngine.Debug.Log("Checking for mud");
		if (list.Count == 0)
		{
			return;
		}
		UnityEngine.Debug.Log("Got mud!");
		List<Vector2> list3 = new List<Vector2>(list);
		list2.Add(new MudArea());
		list2[0].Chunks = new List<Vector2>();
		int num = 0;
		List<Vector2> list4 = new List<Vector2>();
		list4.Add(list3[0]);
		list2[num].Chunks.Add(list4[0]);
		while (list3.Count > 0)
		{
			bool flag = false;
			if (list4.Count > 0)
			{
				for (int num2 = -1; num2 <= 1; num2++)
				{
					for (int num3 = -1; num3 <= 1; num3++)
					{
						Vector2 item = new Vector2(list4[0].x + (float)num2, list4[0].y + (float)num3);
						if (list3.Contains(item))
						{
							if (!list2[num].Chunks.Contains(item))
							{
								list2[num].Chunks.Add(item);
							}
							list3.Remove(list4[0]);
							if (!list4.Contains(item) && list3.Contains(item))
							{
								list4.Add(new Vector2(list4[0].x + (float)num2, list4[0].y + (float)num3));
							}
							flag = true;
						}
					}
				}
			}
			if (list4.Count > 0)
			{
				list4.RemoveAt(0);
			}
			if (!flag && list3.Count > 0)
			{
				num++;
				list2.Add(new MudArea());
				list2[num].Chunks = new List<Vector2>();
				list4.Add(list3[0]);
			}
		}
		for (int num4 = 0; num4 < list2.Count; num4++)
		{
			list2[num4].xMin = (list2[num4].xMax = (int)list2[num4].Chunks[0].x);
			list2[num4].yMin = (list2[num4].yMax = (int)list2[num4].Chunks[0].y);
			foreach (Vector2 vector in list2[num4].Chunks)
			{
				if (vector.x < (float)list2[num4].xMin)
				{
					list2[num4].xMin = (int)vector.x;
				}
				if (vector.x > (float)list2[num4].xMax)
				{
					list2[num4].xMax = (int)vector.x;
				}
				if (vector.y < (float)list2[num4].yMin)
				{
					list2[num4].yMin = (int)vector.y;
				}
				if (vector.y > (float)list2[num4].yMax)
				{
					list2[num4].yMax = (int)vector.y;
				}
			}
			list2[num4].xMin *= this.ChunkSize;
			list2[num4].yMin *= this.ChunkSize;
			list2[num4].xMax = list2[num4].xMax * this.ChunkSize + this.ChunkSize;
			list2[num4].yMax = list2[num4].yMax * this.ChunkSize + this.ChunkSize;
			list2[num4].Width = list2[num4].xMax - list2[num4].xMin;
			list2[num4].Height = list2[num4].yMax - list2[num4].yMin;
			while (list2[num4].Width != list2[num4].Height)
			{
				if (list2[num4].Width > list2[num4].Height)
				{
					list2[num4].yMax += this.ChunkSize;
				}
				else
				{
					list2[num4].xMax += this.ChunkSize;
				}
				list2[num4].Width = list2[num4].xMax - list2[num4].xMin;
				list2[num4].Height = list2[num4].yMax - list2[num4].yMin;
			}
		}
		foreach (MudArea mudArea in list2)
		{
			for (int num5 = mudArea.xMin; num5 < mudArea.xMax; num5++)
			{
				for (int num6 = mudArea.yMin; num6 < mudArea.yMax; num6++)
				{
					this.SurfaceMaterialUnderCar = this.GetSurfaceMaterialByTerrainCoords(new Vector2((float)num5, (float)num6));
					if (this.SurfaceMaterialUnderCar != null)
					{
						if (this.SurfaceMaterialUnderCar.surfaceType == SurfaceMaterial.SurfaceType.Mud)
						{
							mudArea.deformableMaterial = this.SurfaceMaterialUnderCar;
						}
						if (this.SurfaceMaterialUnderCar.MudWater)
						{
							mudArea.HasMudWater = true;
						}
					}
				}
			}
		}
		this.MudTerrains = new MudTerrain[list2.Count];
		for (int num7 = 0; num7 < list2.Count; num7++)
		{
			TerrainData terrainData = new TerrainData();
			Terrain component = Terrain.CreateTerrainGameObject(terrainData).GetComponent<Terrain>();
			component.transform.parent = this.BaseTerrain.transform;
			this.MudTerrains[num7] = component.gameObject.AddComponent<MudTerrain>();
			this.MudTerrains[num7].terrainData = terrainData;
			this.MudTerrains[num7].terrain = component;
			this.MudTerrains[num7].terrain.name = "MudTerrain" + num7.ToString();
			this.MudTerrains[num7].terrain.gameObject.layer = this.BaseTerrain.gameObject.layer;
			this.MudTerrains[num7].terrain.basemapDistance = (float)this.SmallTerrainBasemapDistance;
			this.MudTerrains[num7].terrain.heightmapPixelError = 20f;
			this.MudTerrains[num7].DeformableMaterialMaxDepth = list2[num7].deformableMaterial.MaxDepth;
			int num8 = 65;
			if (this.maxSmallTerrainResolution > MaxSmallTerrainResolution.x65 && (float)list2[num7].Width / (float)this.aRes * this.BaseTerrain.terrainData.size.x * (float)this.TargetVertexPerMeterNumber > 65f)
			{
				num8 = 129;
			}
			if (this.maxSmallTerrainResolution > MaxSmallTerrainResolution.x129 && (float)list2[num7].Width / (float)this.aRes * this.BaseTerrain.terrainData.size.x * (float)this.TargetVertexPerMeterNumber > 129f)
			{
				num8 = 257;
			}
			if (this.maxSmallTerrainResolution > list2[num7].deformableMaterial.MaxResolution)
			{
				if (list2[num7].deformableMaterial.MaxResolution == MaxSmallTerrainResolution.x65)
				{
					num8 = 65;
				}
				if (list2[num7].deformableMaterial.MaxResolution > MaxSmallTerrainResolution.x65)
				{
					num8 = 129;
				}
				if (list2[num7].deformableMaterial.MaxResolution > MaxSmallTerrainResolution.x129)
				{
					num8 = 257;
				}
			}
			this.MudTerrains[num7].terrainData.heightmapResolution = num8;
			this.MudTerrains[num7].hRes = num8;
			this.MudTerrains[num7].hRes = num8;
			this.MudTerrains[num7].terrainData.size = new Vector3((float)list2[num7].Width / (float)this.aRes * this.BaseTerrain.terrainData.size.x, this.BaseTerrain.terrainData.size.y, (float)list2[num7].Height / (float)this.aRes * this.BaseTerrain.terrainData.size.z);
			this.MudTerrains[num7].terrainData.alphamapResolution = (int)((float)this.BaseTerrain.terrainData.alphamapResolution / this.BaseTerrain.terrainData.size.x * this.MudTerrains[num7].terrainData.size.x);
			float[,] array2 = new float[num8, num8];
			Vector3 a = this.BaseTerrain.GetPosition() + new Vector3((float)list2[num7].xMin / (float)this.aRes * this.BaseTerrain.terrainData.size.x, 0f, (float)list2[num7].yMin / (float)this.aRes * this.BaseTerrain.terrainData.size.z);
			for (int num9 = 0; num9 < num8; num9++)
			{
				for (int num10 = 0; num10 < num8; num10++)
				{
					Vector2 zero = Vector2.zero;
					zero.x = a.x - this.BaseTerrain.GetPosition().x + (float)num9 / (float)num8 * this.MudTerrains[num7].terrainData.size.x;
					zero.y = a.z - this.BaseTerrain.GetPosition().z + (float)num10 / (float)num8 * this.MudTerrains[num7].terrainData.size.z;
					array2[num10, num9] = this.BaseTerrain.terrainData.GetInterpolatedHeight(zero.x / this.BaseTerrain.terrainData.size.x, zero.y / this.BaseTerrain.terrainData.size.z) / this.BaseTerrain.terrainData.size.y;
				}
			}
			this.MudTerrains[num7].terrainData.SetHeights(0, 0, array2);
			int num11 = num8 - 1;
			this.MudTerrains[num7].terrainData.alphamapResolution = num11;
			this.MudTerrains[num7].terrainData.splatPrototypes = this.BaseTerrain.terrainData.splatPrototypes;
			this.MudTerrains[num7].aRes = num11;
			this.MudTerrains[num7].aRes = num11;
			float[,,] array3 = new float[num11, num11, this.terrainTextures.Length];
			int num12 = list2[num7].Width;
			int num13 = list2[num7].Height;
			if (list2[num7].xMin + num12 > this.BaseTerrain.terrainData.alphamapWidth - 1)
			{
				num12 = this.BaseTerrain.terrainData.alphamapWidth - list2[num7].xMin;
			}
			if (list2[num7].yMin + num13 > this.BaseTerrain.terrainData.alphamapWidth - 1)
			{
				num13 = this.BaseTerrain.terrainData.alphamapWidth - list2[num7].yMin;
			}
			float[,,] alphamaps = this.BaseTerrain.terrainData.GetAlphamaps(list2[num7].xMin, list2[num7].yMin, num12, num13);
			List<SplatPrototype> list5 = new List<SplatPrototype>();
			List<int> list6 = new List<int>();
			for (int num14 = 0; num14 < num11; num14++)
			{
				for (int num15 = 0; num15 < num11; num15++)
				{
					for (int num16 = 0; num16 < this.terrainTextures.Length; num16++)
					{
						int num17 = (int)((float)num15 / (float)num11 * (float)list2[num7].Height);
						int num18 = (int)((float)num14 / (float)num11 * (float)list2[num7].Width);
						if (num18 <= alphamaps.GetUpperBound(1) && num17 <= alphamaps.GetUpperBound(0))
						{
							array3[num15, num14, num16] = alphamaps[num17, num18, num16];
							if (array3[num15, num14, num16] > 0f && !list6.Contains(num16))
							{
								list6.Add(num16);
							}
						}
					}
				}
			}
			this.MudTerrains[num7].terrainData.SetAlphamaps(0, 0, array3);
			foreach (int num19 in list6)
			{
				list5.Add(this.BaseTerrain.terrainData.splatPrototypes[num19]);
			}
			this.MudTerrains[num7].textures = new Texture2D[list5.Count];
			for (int num20 = 0; num20 < list5.Count; num20++)
			{
				this.MudTerrains[num7].textures[num20] = list5[num20].texture;
			}
			this.MudTerrains[num7].terrainData.splatPrototypes = list5.ToArray();
			List<Vector2> list7 = new List<Vector2>();
			for (int num21 = 0; num21 < this.BaseTerrain.terrainData.splatPrototypes.Length; num21++)
			{
				for (int num22 = 0; num22 < list5.Count; num22++)
				{
					if (this.BaseTerrain.terrainData.splatPrototypes[num21].texture.Equals(list5[num22].texture))
					{
						list7.Add(new Vector2((float)num21, (float)num22));
					}
				}
			}
			float[,,] array4 = new float[num11, num11, list7.Count];
			for (int num23 = 0; num23 < num11; num23++)
			{
				for (int num24 = 0; num24 < num11; num24++)
				{
					for (int num25 = 0; num25 < list7.Count; num25++)
					{
						array4[num24, num23, (int)list7[num25].y] = array3[num24, num23, (int)list7[num25].x];
					}
				}
			}
			this.MudTerrains[num7].terrainData.SetAlphamaps(0, 0, array4);
			this.MudTerrains[num7].terrain.transform.position = a + new Vector3(0f, -0.03f, 0f);
			this.MudTerrains[num7].terrain.materialType = this.BaseTerrain.materialType;
			this.MudTerrains[num7].terrain.materialTemplate = this.BaseTerrain.materialTemplate;
			this.MudTerrains[num7].terRect = new Rect(this.BaseTerrain.GetPosition().x + (float)list2[num7].xMin / (float)this.hRes * this.BaseTerrain.terrainData.size.x, this.BaseTerrain.GetPosition().z + (float)list2[num7].yMin / (float)this.hRes * this.BaseTerrain.terrainData.size.z, (float)list2[num7].Width / (float)this.aRes * this.BaseTerrain.terrainData.size.x, (float)list2[num7].Height / (float)this.aRes * this.BaseTerrain.terrainData.size.z);
			if (mudStamps != null)
			{
				foreach (MudStamp mudStamp in mudStamps)
				{
					if (this.MudTerrains[num7].terRect.Contains(new Vector2(mudStamp.stampPosition.x, mudStamp.stampPosition.z)))
					{
						this.MudTerrains[num7].drag = mudStamp.mudViscosity;
					}
				}
			}
		}
		float[,] heights = this.BaseTerrain.terrainData.GetHeights(0, 0, this.hRes, this.hRes);
		UnityEngine.Random.InitState(0);
		for (int num26 = 0; num26 < this.aRes; num26++)
		{
			for (int num27 = 0; num27 < this.aRes; num27++)
			{
				this.SurfaceMaterialUnderCar = this.GetSurfaceMaterialByTerrainCoords(new Vector2((float)num26, (float)num27));
				Vector3 point = this.BaseTerrain.GetPosition() + new Vector3((float)num26 / (float)this.hRes * this.BaseTerrain.terrainData.size.x, 0f, (float)num27 / (float)this.hRes * this.BaseTerrain.terrainData.size.z);
				float num28 = 0f;
				float num29 = 0f;
				if (this.SurfaceMaterialUnderCar != null)
				{
					num29 = this.SurfaceMaterialUnderCar.MaxDepth / this.BaseTerrain.terrainData.size.y;
				}
				if (mudStamps != null)
				{
					foreach (MudStamp mudStamp2 in mudStamps)
					{
						if (this.DoesPointBelongToMudStamp(mudStamp2, point))
						{
							num29 = mudStamp2.mudDepth / this.BaseTerrain.terrainData.size.y;
						}
					}
				}
				if (this.SurfaceMaterialUnderCar != null && this.SurfaceMaterialUnderCar.surfaceType == SurfaceMaterial.SurfaceType.Mud)
				{
					num28 = num29;
				}
				if (num28 != 0f)
				{
					num28 += UnityEngine.Random.Range(0.1f / this.BaseTerrain.terrainData.size.y, 0.7f / this.BaseTerrain.terrainData.size.y);
				}
				heights[num27, num26] -= num28;
			}
		}
		this.BaseTerrain.terrainData.SetHeights(0, 0, heights);
		UnityEngine.Debug.Log(this.MudTerrains.Length + " mud terrains were successfully created");
		for (int num30 = 0; num30 < list2.Count; num30++)
		{
			if (list2[num30].HasMudWater)
			{
				this.CreateMudWaterMesh(this.MudTerrains[num30].terrain);
			}
		}
	}

	private bool DoesPointBelongToMudStamp(MudStamp mudStamp, Vector3 point)
	{
		Vector3 stampPosition = mudStamp.stampPosition;
		stampPosition.y = 0f;
		Vector3 a = point;
		a.y = 0f;
		return Vector3.Distance(a, stampPosition) < mudStamp.boundsRadius;
	}

	private void CreateMudWaterMesh(Terrain ter)
	{
		int mudWaterResoultion = this.MudWaterResoultion;
		int mudWaterResoultion2 = this.MudWaterResoultion;
		float num = (float)ter.terrainData.heightmapResolution * ter.terrainData.heightmapScale.x;
		float num2 = (float)ter.terrainData.heightmapResolution * ter.terrainData.heightmapScale.z;
		Vector3 position = ter.transform.position;
		Vector3[] array = new Vector3[mudWaterResoultion * mudWaterResoultion2];
		int[] array2 = new int[mudWaterResoultion * mudWaterResoultion2 * 2 * 3];
		int num3 = 0;
		for (int i = 0; i < mudWaterResoultion; i++)
		{
			for (int j = 0; j < mudWaterResoultion2; j++)
			{
				this.SurfaceMaterialUnderCar = this.GetSurfaceMaterialByWorldCoords(new Vector3(position.x + num / (float)(mudWaterResoultion2 - 1) * (float)j, 0f, position.z + num2 / (float)(mudWaterResoultion - 1) * (float)i));
				float num4 = 0.1f;
				if (this.SurfaceMaterialUnderCar != null)
				{
					num4 = Mathf.Max(0.1f, this.SurfaceMaterialUnderCar.MudWaterDepth);
					if (this.SurfaceMaterialUnderCar.surfaceType == SurfaceMaterial.SurfaceType.Hard)
					{
						num4 = 5f;
					}
				}
				float y = ter.SampleHeight(new Vector3(position.x + num / (float)(mudWaterResoultion2 - 1) * (float)j, 0f, position.z + num2 / (float)(mudWaterResoultion - 1) * (float)i)) - num4;
				array[i * mudWaterResoultion2 + j] = new Vector3(num / (float)(mudWaterResoultion2 - 1) * (float)j, y, num2 / (float)(mudWaterResoultion - 1) * (float)i);
				if (i > 0 && j > 0)
				{
					array2[num3] = i * mudWaterResoultion2 + j - 1;
					array2[num3 + 1] = (i - 1) * mudWaterResoultion2 + j;
					array2[num3 + 2] = (i - 1) * mudWaterResoultion2 + j - 1;
					num3 += 3;
					array2[num3] = i * mudWaterResoultion2 + j;
					array2[num3 + 1] = (i - 1) * mudWaterResoultion2 + j;
					array2[num3 + 2] = i * mudWaterResoultion2 + j - 1;
					num3 += 3;
				}
			}
		}
		Mesh mesh = new Mesh();
		mesh.vertices = array;
		mesh.triangles = array2;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		GameObject gameObject = new GameObject();
		gameObject.name = "MudWaterMesh";
		gameObject.transform.parent = ter.transform;
		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
		meshFilter.mesh = mesh;
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.material = this.MudWaterMaterial;
		gameObject.AddComponent<WaterBasic>();
		gameObject.transform.position = ter.GetPosition();
	}

	public float GetTireFriction(int wheelID, int InstalledTire)
	{
		if (this.m_fxData == null)
		{
			return 1f;
		}
		if (wheelID >= this.m_fxData.Length)
		{
			return 1f;
		}
		if (this.m_fxData[wheelID].surfaceMaterial != null && this.m_fxData[wheelID].surfaceMaterial.TiresFriction != null && InstalledTire < this.m_fxData[wheelID].surfaceMaterial.TiresFriction.Length)
		{
			return this.m_fxData[wheelID].surfaceMaterial.TiresFriction[InstalledTire];
		}
		return 1f;
	}

	public static Vector2 GetTerrainCoords(Vector3 WorldCoords, Terrain ter)
	{
		Vector2 zero = Vector2.zero;
		zero.x = (WorldCoords - ter.GetPosition()).x / ter.terrainData.size.x * (float)ter.terrainData.heightmapResolution;
		zero.y = (WorldCoords - ter.GetPosition()).z / ter.terrainData.size.z * (float)ter.terrainData.heightmapResolution;
		return zero;
	}

	private SurfaceMaterial GetSurfaceMaterialByTerrainCoords(Vector2 terCoords)
	{
		if (terCoords.x < 0f || terCoords.x > (float)(this.BaseTerrain.terrainData.alphamapWidth - 1) || terCoords.y < 0f || terCoords.y > (float)(this.BaseTerrain.terrainData.alphamapHeight - 1))
		{
			return null;
		}
		float[,,] alphamaps = this.BaseTerrain.terrainData.GetAlphamaps((int)terCoords.x, (int)terCoords.y, 1, 1);
		for (int i = 0; i < this.terrainTextures.Length; i++)
		{
			if (alphamaps[0, 0, i] > 0.5f)
			{
				for (int j = 0; j < this.surfaceMaterials.Length; j++)
				{
					if (this.terrainTextures[i].Equals(this.surfaceMaterials[j].TerrainTexture))
					{
						return this.surfaceMaterials[j];
					}
				}
			}
		}
		return null;
	}

	private SurfaceMaterial GetSurfaceMaterialByWorldCoords(Vector3 WorldCoords)
	{
		Vector2 terrainCoords = SurfaceManager.GetTerrainCoords(WorldCoords, this.BaseTerrain);
		return this.GetSurfaceMaterialByTerrainCoords(terrainCoords);
	}

	private SurfaceMaterial GetSurfaceMaterialAtPoint(Vector3 point, Collider hitCollider)
	{
		if (hitCollider == null)
		{
			return null;
		}
		if (hitCollider.GetType() == typeof(TerrainCollider))
		{
			if (this.MudTerrains != null)
			{
				for (int i = 0; i < this.MudTerrains.Length; i++)
				{
					if (this.MudTerrains[i].terRect.Contains(new Vector2(point.x, point.z)))
					{
						this.MudTerrains[i].LODS_Baked = false;
						this.CurrentMudTerrain = this.MudTerrains[i];
						break;
					}
					if (!this.MudTerrains[i].LODS_Baked)
					{
						this.MudTerrains[i].LODS_Baked = true;
						this.MudTerrains[i].terrainData.SyncHeightmap();
					}
				}
			}
			return this.GetSurfaceMaterialByWorldCoords(point);
		}
		for (int j = 0; j < this.surfaceMaterials.Length; j++)
		{
			if (this.surfaceMaterials[j].physicMaterial != null && hitCollider.material.name.Equals(this.surfaceMaterials[j].physicMaterial.name + " (Instance)"))
			{
				this.CurrentMudTerrain = null;
				return this.surfaceMaterials[j];
			}
		}
		this.CurrentMudTerrain = null;
		return null;
	}

	private void PushTerrain(int wheelID)
	{
		Vector3 forward = Vector3.forward;
		Vector3 a = Vector3.zero;
		bool flag;
		float rpm;
		if (this.carController.tankController == null)
		{
			flag = this.carController.wheels[wheelID].wc.IsGrounded;
			forward = this.carController.wheels[wheelID].wc.transform.forward;
			rpm = this.carController.wheels[wheelID].wc.rpm;
			a = this.carController.wheels[wheelID].wc.wheelCollider.worldHitPos;
		}
		else
		{
			flag = this.carController.tankController.borderWheelColliders[wheelID].grounded;
			forward = this.carController.tankController.borderWheelColliders[wheelID].transform.forward;
			rpm = this.carController.tankController.borderWheelColliders[wheelID].rpm;
			a = this.carController.tankController.borderWheelColliders[wheelID].worldHitPos;
		}
		if (!flag)
		{
			return;
		}
		if (this.CurrentMudTerrain == null)
		{
			return;
		}
		if (this.CurrentMudTerrain.DefaultHeights == null)
		{
			return;
		}
		SurfaceMaterial surfaceMaterial = this.m_fxData[wheelID].surfaceMaterial;
		Vector3 vector = a + forward * this.DeformationPointOffset * Mathf.Sign(rpm);
		Vector2 terrainCoords = SurfaceManager.GetTerrainCoords(vector, this.CurrentMudTerrain.terrain);
		int num = Mathf.Clamp((int)terrainCoords.x - this.PushDiameter / 2, 0, this.CurrentMudTerrain.aRes - this.PushDiameter / 2 - 2);
		int num2 = Mathf.Clamp((int)terrainCoords.y - this.PushDiameter / 2, 0, this.CurrentMudTerrain.aRes - this.PushDiameter / 2 - 2);
		float[,] heights = this.CurrentMudTerrain.terrainData.GetHeights(num, num2, this.PushDiameter, this.PushDiameter);
		float[,] array = new float[this.PushDiameter, this.PushDiameter];
		for (int i = 0; i < this.PushDiameter; i++)
		{
			for (int j = 0; j < this.PushDiameter; j++)
			{
				array[j, i] = this.CurrentMudTerrain.DefaultHeights[num2 + j, num + i];
			}
		}
		for (int k = 0; k < this.PushDiameter; k++)
		{
			for (int l = 0; l < this.PushDiameter; l++)
			{
				int x = (int)((float)k / (float)this.PushDiameter * (float)this.PushStampTexture.width);
				int y = (int)((float)l / (float)this.PushDiameter * (float)this.PushStampTexture.height);
				if (surfaceMaterial.surfaceType == SurfaceMaterial.SurfaceType.Mud)
				{
					Color pixel = this.PushStampTexture.GetPixel(x, y);
					float num3 = pixel.r;
					if (pixel.g > pixel.r)
					{
						num3 = -pixel.g;
					}
					Vector2 vector2 = new Vector2((float)(num + k), (float)(num2 + l));
					Vector3 a2 = this.CurrentMudTerrain.terrain.GetPosition() + new Vector3(vector2.x / (float)this.CurrentMudTerrain.terrainData.heightmapResolution * this.CurrentMudTerrain.terrainData.size.x, 0f, vector2.y / (float)this.CurrentMudTerrain.terrainData.heightmapResolution * this.CurrentMudTerrain.terrainData.size.z);
					a2.y = vector.y;
					Vector3 lhs = forward * Mathf.Sign(rpm);
					float num4 = Vector3.Dot(lhs, a2 - vector);
					if (num4 < 0f && num3 < 0f)
					{
						num3 = 0f;
					}
					heights[l, k] -= num3 * (1f - surfaceMaterial.Hardness) * this.PushStrength / this.CurrentMudTerrain.terrainData.size.y * 2f;
				}
				float min = array[l, k] - surfaceMaterial.MaxDepth / this.CurrentMudTerrain.terrainData.size.y;
				float max = array[l, k] + surfaceMaterial.MaxExtrudeHeight / this.CurrentMudTerrain.terrainData.size.y;
				heights[l, k] = Mathf.Clamp(heights[l, k], min, max);
			}
		}
		if (surfaceMaterial.surfaceType == SurfaceMaterial.SurfaceType.Mud)
		{
			Debug.Log("digging");
			this.CurrentMudTerrain.terrainData.SetHeightsDelayLOD(num, num2, heights);
		}
	}

	public bool IsCarInWater()
	{
		if (this.WaterMeshesRects == null || this.carController == null)
		{
			return false;
		}
		for (int i = 0; i < this.WaterMeshesRects.Count; i++)
		{
			if (this.WaterMeshesRects[i].Contains(new Vector2(this.carController.transform.position.x, this.carController.transform.position.z)))
			{
				return true;
			}
		}
		return false;
	}

	public bool IsCameraInWater()
	{
		if (this.WaterMeshesRects == null || this.carController == null)
		{
			return false;
		}
		for (int i = 0; i < this.WaterMeshesRects.Count; i++)
		{
			if (this.WaterMeshesRects[i].Contains(new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.z)) && this.WaterMeshes[i].transform.position.y > Camera.main.transform.position.y)
			{
				return true;
			}
		}
		return false;
	}

	public int WhatWaterMeshIsCarOn()
	{
		if (!this.IsCarInWater())
		{
			return 999;
		}
		for (int i = 0; i < this.WaterMeshesRects.Count; i++)
		{
			if (this.WaterMeshesRects[i].Contains(new Vector2(this.carController.transform.position.x, this.carController.transform.position.z)))
			{
				return i;
			}
		}
		return 999;
	}

	private void DoParticles(int wheelID)
	{
		Vector3 forward = Vector3.forward;
		Vector3 position = Vector3.zero;
		Quaternion rotation = Quaternion.identity;
		bool flag;
		float rpm;
		float num;
		if (this.carController.tankController == null)
		{
			flag = this.carController.wheels[wheelID].wc.IsGrounded;
			forward = this.carController.wheels[wheelID].wc.transform.forward;
			rpm = this.carController.wheels[wheelID].wc.rpm;
			position = this.carController.wheels[wheelID].wc.wheelCollider.hitPoint;
			num = this.carController.wheels[wheelID].wc.CommonSlip;
			rotation = this.carController.wheels[wheelID].wc.transform.rotation;
		}
		else
		{
			flag = this.carController.tankController.borderWheelColliders[wheelID].grounded;
			forward = this.carController.tankController.borderWheelColliders[wheelID].transform.forward;
			rpm = this.carController.tankController.borderWheelColliders[wheelID].rpm;
			position = this.carController.tankController.borderWheelColliders[wheelID].hitPoint;
			num = this.carController.tankController.borderWheelColliders[wheelID].sLat + this.carController.tankController.borderWheelColliders[wheelID].sLong;
			num *= Mathf.InverseLerp(3f, 6f, Mathf.Abs(this.carController.Speed));
			rotation = this.carController.tankController.borderWheelColliders[wheelID].transform.rotation;
		}
		if (!flag)
		{
			return;
		}
		SurfaceMaterial surfaceMaterial = this.m_fxData[wheelID].surfaceMaterial;
		int num2 = 0;
		this.CurrentParticles = null;
		this.SecondaryParticles = null;
		int index = this.WhatWaterMeshIsCarOn();
		int count = 0;
		switch (surfaceMaterial.particlesType)
		{
		case SurfaceMaterial.ParticlesType.None:
			this.CurrentParticles = null;
			num2 = 0;
			break;
		case SurfaceMaterial.ParticlesType.MudPieces:
			this.CurrentParticles = this.MudParticles;
			num2 = ((num <= this.MudSlipThreshold) ? 0 : ((int)(num * (float)this.MudEmitStrength)));
			if (WeatherController.Instance != null)
			{
				num2 = (int)((float)num2 * (WeatherController.Instance.overcast + 1f));
			}
			break;
		case SurfaceMaterial.ParticlesType.SkidSmoke:
			this.CurrentParticles = this.SkidSmokeParticles;
			num2 = ((num <= this.SkidSmokeSlipThreshold) ? 0 : ((int)((float)this.SkidSmokeEmitStrength * num)));
			if (this.carController.tankController != null)
			{
				num2 = 0;
			}
			break;
		case SurfaceMaterial.ParticlesType.SandDust:
			this.CurrentParticles = this.SandDustParticles;
			num2 = ((Mathf.Abs(rpm) <= this.SandDustMinRpm) ? 0 : this.SandDustEmitStrength);
			break;
		case SurfaceMaterial.ParticlesType.WaterSplash:
			this.CurrentParticles = this.WaterSplashParticles;
			this.SecondaryParticles = this.RippleParticles;
			num2 = ((Mathf.Abs(rpm) <= this.WaterSplashMinRpm) ? 0 : this.WaterSplashEmitStrength);
			count = ((Mathf.Abs(this.carController.Speed) <= (float)this.RipplesSpeedThreshold) ? 0 : 1);
			break;
		case SurfaceMaterial.ParticlesType.Snow:
			this.CurrentParticles = this.SnowParticles;
			num2 = ((Mathf.Abs(rpm) <= this.SnowRPMThreshold) ? 0 : this.SnowEmitStrength);
			break;
		}
		if (this.IsCarInWater() && position.y < this.WaterMeshes[index].transform.position.y)
		{
			this.CurrentParticles = this.WaterSplashParticles;
			this.SecondaryParticles = this.RippleParticles;
			num2 = ((Mathf.Abs(rpm) <= this.WaterSplashMinRpm) ? 0 : this.WaterSplashEmitStrength);
			count = ((Mathf.Abs(this.carController.Speed) <= (float)this.RipplesSpeedThreshold) ? 0 : 1);
			this.CleanBody(true);
		}
		if (this.CurrentParticles != null)
		{
			this.CurrentParticles.transform.position = position;
			this.CurrentParticles.transform.rotation = rotation;
			this.CurrentParticles.transform.eulerAngles += new Vector3(-45f, (float)((rpm >= 0f) ? 180 : 0), 0f);
			this.CurrentParticles.Emit(num2);
			if (num2 > 0 && this.CurrentParticles == this.MudParticles)
			{
				this.AddDirtToCarBody();
			}
			if (num2 > 0 && this.CurrentParticles == this.WaterSplashParticles)
			{
				this.CleanBody(false);
			}
		}
		if (this.SecondaryParticles != null)
		{
			Vector3 position2 = this.carController.transform.position;
			position2.y = this.WaterMeshes[index].transform.position.y + 0.01f;
			this.SecondaryParticles.transform.position = position2;
			this.SecondaryParticles.Emit(count);
		}
	}

	private void CleanBody(bool useDefault = false)
	{
		if (this.SurfaceMaterialUnderCar == null)
		{
			return;
		}
		float num = 0.2f;
		if (useDefault)
		{
			this.partsSwitcher.Dirtiness -= num * Time.deltaTime;
		}
		else
		{
			this.partsSwitcher.Dirtiness -= this.SurfaceMaterialUnderCar.CleaningSpeed * Time.deltaTime;
		}
		if (!useDefault)
		{
			this.partsSwitcher.Dirtiness = Mathf.Max(this.SurfaceMaterialUnderCar.MinimumDirtiness, Mathf.Clamp01(this.partsSwitcher.Dirtiness));
		}
		this.partsSwitcher.UpdateDirtiness();
		this.CarWetnessTarget = 1f;
	}

	private void AddDirtToCarBody()
	{
		if (this.SurfaceMaterialUnderCar == null)
		{
			return;
		}
		if (this.partsSwitcher == null)
		{
			return;
		}
		this.partsSwitcher.Dirtiness += this.SurfaceMaterialUnderCar.DirtinessSpeed * Time.deltaTime;
		this.partsSwitcher.Dirtiness = Mathf.Max(this.SurfaceMaterialUnderCar.MinimumDirtiness, Mathf.Clamp01(this.partsSwitcher.Dirtiness));
		this.partsSwitcher.UpdateDirtiness();
		this.CarWetnessTarget = 1f;
	}

	private void UpdateCarWetness()
	{
		if (this.partsSwitcher == null)
		{
			return;
		}
		if (this.partsSwitcher.MudWetness == this.CarWetnessTarget)
		{
			this.CarWetnessTarget = 0f;
		}
		float num = (float)((this.CarWetnessTarget != 1f) ? 1 : 2);
		this.partsSwitcher.MudWetness = Mathf.MoveTowards(this.partsSwitcher.MudWetness, this.CarWetnessTarget, ((this.CarWetnessTarget <= this.partsSwitcher.MudWetness) ? this.DrynessSpeed : this.WetnessSpeed) * num * Time.deltaTime);
		this.partsSwitcher.Dirtiness = Mathf.Clamp01(this.partsSwitcher.Dirtiness);
		this.partsSwitcher.UpdateDirtiness();
	}

	private void ProcessWheels()
	{
		if (this.carController == null)
		{
			return;
		}
		if (this.carController.WheelsCount == 0)
		{
			return;
		}
		if (this.lastCarController != this.carController)
		{
			this.InitializeTireFxData();
		}
		this.lastCarController = this.carController;
		Collider hitCollider;
		if (this.carController.tankController == null)
		{
			hitCollider = this.carController.wheels[0].wc.wheelCollider.contactColliderHit;
		}
		else
		{
			hitCollider = this.carController.tankController.borderWheelColliders[0].hitCollider;
		}
		this.SurfaceMaterialUnderCar = this.GetSurfaceMaterialAtPoint(this.carController.transform.position, hitCollider);
		this.UpdateCarWetness();
		for (int i = 0; i < this.carController.WheelsCount; i++)
		{
			if ((i < this.carController.wheels.Count && this.carController.wheels[i].wc.wheelCollider != null) || this.carController.tankController != null)
			{
				Vector3 point = (!(this.carController.tankController == null)) ? this.carController.tankController.borderWheelColliders[i].transform.position : this.carController.wheels[i].wc.transform.position;
				Collider hitCollider2 = (!(this.carController.tankController == null)) ? this.carController.tankController.borderWheelColliders[0].hitCollider : this.carController.wheels[0].wc.wheelCollider.contactColliderHit;
				this.m_fxData[i].surfaceMaterial = this.GetSurfaceMaterialAtPoint(point, hitCollider2);
				this.m_fxData[i].marksRenderer = null;
				SurfaceMaterial surfaceMaterial = this.m_fxData[i].surfaceMaterial;
				if (this.carController.tankController == null)
				{
					this.carController.wheels[i].wc.wheelCollider.hitOffset = 0f;
				}
				if (surfaceMaterial != null)
				{
					if (this.carController.tankController == null)
					{
						switch (surfaceMaterial.tireTracks)
						{
						case SurfaceMaterial.TireTracks.SnowMark:
							this.m_fxData[i].marksRenderer = this.SnowMarksRenderer;
							break;
						case SurfaceMaterial.TireTracks.SandMark:
							this.m_fxData[i].marksRenderer = this.SandMarksRenderer;
							break;
						case SurfaceMaterial.TireTracks.MudMark:
							this.m_fxData[i].marksRenderer = this.MudMarksRenderer;
							break;
						case SurfaceMaterial.TireTracks.SkidMark:
							if (this.carController.wheels[i].wc.CommonSlip > 0.5f && this.carController.tankController == null)
							{
								this.m_fxData[i].marksRenderer = this.SkidMarksRenderer;
							}
							break;
						}
						this.carController.wheels[i].wc.wheelCollider.hitOffset = ((surfaceMaterial.surfaceType != SurfaceMaterial.SurfaceType.Snow) ? 0f : surfaceMaterial.MaxDepth);
					}
					if (this.CurrentQuality > 1)
					{
						this.PushTerrain(i);
						this.DoParticles(i);
					}
				}
			}
		}
		float num = 0f;
		if (this.SurfaceMaterialUnderCar != null && this.carController.Grounded)
		{
			if (this.CurrentMudTerrain != null && this.SurfaceMaterialUnderCar.AddedDrag > 0f)
			{
				num = this.CurrentMudTerrain.drag;
			}
			if (num == 0f)
			{
				num = this.SurfaceMaterialUnderCar.AddedDrag;
			}
		}
		if (this.carController.tankController != null)
		{
			num /= 4f;
		}
		this.playerRigidbody.drag = num;
	}

	private void Update()
	{
		if (this.m_fxData != null && this.carController.tankController == null)
		{
			for (int i = 0; i < this.m_fxData.Length; i++)
			{
				this.UpdateTireMarks(i);
			}
		}
		if (Time.frameCount % this.UpdateEveryNFrame == 0)
		{
			this.ProcessWheels();
			if (this.IsCameraInWater() && this.TargetTintColor != this.WaterTintColor)
			{
				this.TargetTintColor = this.WaterTintColor;
				if (this.BlurScript != null && this.CurrentQuality >= 2)
				{
					this.BlurScript.enabled = true;
				}
				this.BlurAmountTarget = 10f;
				AudioListener.volume = 0.1f;
			}
			else if (!this.IsCameraInWater() && this.TargetTintColor != this.SkyTintColor)
			{
				this.TargetTintColor = this.SkyTintColor;
				if (this.BlurScript != null)
				{
					this.BlurScript.enabled = false;
					this.BlurScript.blurSize = 0f;
				}
				AudioListener.volume = 1f;
			}
			RenderSettings.ambientSkyColor = Color.Lerp(RenderSettings.ambientSkyColor, this.TargetTintColor, 6f * Time.deltaTime);
			if (this.BlurScript != null && this.BlurScript.enabled)
			{
				this.BlurScript.blurSize = Mathf.Lerp(this.BlurScript.blurSize, 10f, 3f * Time.deltaTime);
			}
		}
	}

	public static SurfaceManager Instance;

	public Terrain BaseTerrain;

	public float maxDroneHeight;

	private int hRes;

	private int aRes;

	[Header("Materials")]
	public SurfaceMaterial[] surfaceMaterials;

	[HideInInspector]
	public SurfaceMaterial SurfaceMaterialUnderCar;

	public Material MudWaterMaterial;

	public Material WaterMaterial;

	private float DrynessSpeed = 0.05f;

	private float WetnessSpeed = 0.1f;

	[Header("Deformation")]
	public int PushDiameter = 4;

	public float PushStrength = 0.1f;

	public float DeformationPointOffset = 0.1f;

	public Texture2D PushStampTexture;

	public int MudWaterResoultion = 32;

	[Header("Particles")]
	[Range(0f, 1f)]
	public float SkidSmokeSlipThreshold = 0.4f;

	[Range(0f, 1f)]
	public float MudSlipThreshold = 0.3f;

	public float SandDustMinRpm = 10f;

	public float WaterSplashMinRpm = 10f;

	public int SkidSmokeEmitStrength = 2;

	public int SandDustEmitStrength = 1;

	public int MudEmitStrength = 3;

	public int WaterSplashEmitStrength = 3;

	public int RipplesSpeedThreshold = 5;

	public float SnowRPMThreshold = 30f;

	public int SnowEmitStrength = 20;

	private float CarWetnessTarget;

	[Header("System")]
	public MaxSmallTerrainResolution maxSmallTerrainResolution;

	public int TargetVertexPerMeterNumber = 3;

	public int ChunkSize = 8;

	public int UpdateEveryNFrame = 2;

	public int SmallTerrainBasemapDistance = 10;

	public bool ShowChunksGizmos;

	public Color GizmosColor = Color.grey;

	private float tireMarksUpdateInterval = 0.02f;

	private TireFxData[] m_fxData;

	private TireMarksRenderer marksRenderer;

	public MudTerrain[] MudTerrains;

	public MudTerrain CurrentMudTerrain;

	private TireMarksRenderer SnowMarksRenderer;

	private TireMarksRenderer SandMarksRenderer;

	private TireMarksRenderer MudMarksRenderer;

	private TireMarksRenderer SkidMarksRenderer;

	private VolumetricTireTrackRenderer[] SnowTracksRenderers;

	public List<GameObject> WaterMeshes;

	public List<Rect> WaterMeshesRects;

	private ParticleSystem MudParticles;

	private ParticleSystem SandDustParticles;

	private ParticleSystem SkidSmokeParticles;

	private ParticleSystem WaterSplashParticles;

	private ParticleSystem RippleParticles;

	private ParticleSystem SnowParticles;

	private CarController lastCarController;

	private Texture2D[] terrainTextures;

	private ParticleSystem CurrentParticles;

	private ParticleSystem SecondaryParticles;

	private int CurrentQuality;

	private Color SkyTintColor = Color.white;

	private Color WaterTintColor = new Color(0f, 0f, 1f);

	private Color TargetTintColor = Color.white;

	private BlurOptimized BlurScript;

	private float BlurAmountTarget;
}
