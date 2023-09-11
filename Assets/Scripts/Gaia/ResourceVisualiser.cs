using System;
using UnityEngine;

namespace Gaia
{
	public class ResourceVisualiser : MonoBehaviour
	{
		private void Awake()
		{
			base.gameObject.SetActive(false);
		}

		private void OnEnable()
		{
			this.Initialise();
		}

		public void Initialise()
		{
			this.m_fitnessCollisionLayers = TerrainHelper.GetActiveTerrainLayer();
			this.m_spawner = base.GetComponent<Spawner>();
			if (this.m_spawner == null)
			{
				this.m_spawner = base.gameObject.AddComponent<Spawner>();
				this.m_spawner.m_spawnCollisionLayers = this.m_fitnessCollisionLayers;
				this.m_spawner.hideFlags = HideFlags.HideInInspector;
				this.m_spawner.m_resources = this.m_resources;
				this.m_spawner.m_spawnRange = this.m_range;
				this.m_spawner.m_showGizmos = false;
				this.m_spawner.Initialise();
			}
			else
			{
				this.m_spawner.m_spawnCollisionLayers = this.m_fitnessCollisionLayers;
				this.m_spawner.Initialise();
			}
		}

		public void Visualise()
		{
			this.m_terrainHeightMap = new UnityHeightMap(TerrainHelper.GetActiveTerrain());
		}

		public SpawnInfo GetSpawnInfo(Vector3 location)
		{
			SpawnInfo spawnInfo = new SpawnInfo();
			spawnInfo.m_textureStrengths = new float[Terrain.activeTerrain.terrainData.alphamapLayers];
			if (this.m_spawner.CheckLocation(location, ref spawnInfo))
			{
				spawnInfo.m_fitness = this.GetFitness(ref spawnInfo);
			}
			else
			{
				spawnInfo.m_fitness = 0f;
			}
			return spawnInfo;
		}

		public float GetFitness(ref SpawnInfo spawnInfo)
		{
			SpawnCritera[] spawnCriteria;
			switch (this.m_selectedResourceType)
			{
			case GaiaConstants.SpawnerResourceType.TerrainTexture:
				spawnCriteria = spawnInfo.m_spawner.m_resources.m_texturePrototypes[this.m_selectedResourceIdx].m_spawnCriteria;
				break;
			case GaiaConstants.SpawnerResourceType.TerrainDetail:
				spawnCriteria = spawnInfo.m_spawner.m_resources.m_detailPrototypes[this.m_selectedResourceIdx].m_spawnCriteria;
				break;
			case GaiaConstants.SpawnerResourceType.TerrainTree:
				spawnCriteria = spawnInfo.m_spawner.m_resources.m_treePrototypes[this.m_selectedResourceIdx].m_spawnCriteria;
				break;
			default:
				spawnCriteria = spawnInfo.m_spawner.m_resources.m_gameObjectPrototypes[this.m_selectedResourceIdx].m_spawnCriteria;
				break;
			}
			if (spawnCriteria == null || spawnCriteria.Length == 0)
			{
				return 0f;
			}
			float num = float.MinValue;
			foreach (SpawnCritera spawnCritera in spawnCriteria)
			{
				if (spawnCritera.m_checkType == GaiaConstants.SpawnerLocationCheckType.BoundedAreaCheck && !spawnInfo.m_spawner.CheckLocationBounds(ref spawnInfo, this.GetMaxScaledRadius(ref spawnInfo)))
				{
					return 0f;
				}
				float fitness = spawnCritera.GetFitness(ref spawnInfo);
				if (fitness > num)
				{
					num = fitness;
					if (num >= 1f)
					{
						return num;
					}
				}
			}
			if (num == -3.40282347E+38f)
			{
				return 0f;
			}
			return num;
		}

		public float GetMinFitness(ref SpawnInfo spawnInfo)
		{
			SpawnCritera[] spawnCriteria;
			switch (this.m_selectedResourceType)
			{
			case GaiaConstants.SpawnerResourceType.TerrainTexture:
				if (this.m_selectedResourceIdx >= spawnInfo.m_spawner.m_resources.m_texturePrototypes.Length)
				{
					return 0f;
				}
				spawnCriteria = spawnInfo.m_spawner.m_resources.m_texturePrototypes[this.m_selectedResourceIdx].m_spawnCriteria;
				break;
			case GaiaConstants.SpawnerResourceType.TerrainDetail:
				if (this.m_selectedResourceIdx >= spawnInfo.m_spawner.m_resources.m_detailPrototypes.Length)
				{
					return 0f;
				}
				spawnCriteria = spawnInfo.m_spawner.m_resources.m_detailPrototypes[this.m_selectedResourceIdx].m_spawnCriteria;
				break;
			case GaiaConstants.SpawnerResourceType.TerrainTree:
				if (this.m_selectedResourceIdx >= spawnInfo.m_spawner.m_resources.m_treePrototypes.Length)
				{
					return 0f;
				}
				spawnCriteria = spawnInfo.m_spawner.m_resources.m_treePrototypes[this.m_selectedResourceIdx].m_spawnCriteria;
				break;
			default:
				if (this.m_selectedResourceIdx >= spawnInfo.m_spawner.m_resources.m_gameObjectPrototypes.Length)
				{
					return 0f;
				}
				spawnCriteria = spawnInfo.m_spawner.m_resources.m_gameObjectPrototypes[this.m_selectedResourceIdx].m_spawnCriteria;
				break;
			}
			if (spawnCriteria == null || spawnCriteria.Length == 0)
			{
				return 0f;
			}
			float num = float.MaxValue;
			foreach (SpawnCritera spawnCritera in spawnCriteria)
			{
				if (spawnCritera.m_checkType == GaiaConstants.SpawnerLocationCheckType.BoundedAreaCheck && !spawnInfo.m_spawner.CheckLocationBounds(ref spawnInfo, this.GetMaxScaledRadius(ref spawnInfo)))
				{
					return 0f;
				}
				float fitness = spawnCritera.GetFitness(ref spawnInfo);
				if (fitness < num)
				{
					num = fitness;
					if (num <= 0f)
					{
						return num;
					}
				}
			}
			if (num == 3.40282347E+38f)
			{
				return 0f;
			}
			return num;
		}

		public float GetMaxScaledRadius(ref SpawnInfo spawnInfo)
		{
			switch (this.m_selectedResourceType)
			{
			case GaiaConstants.SpawnerResourceType.TerrainTexture:
				return 1f;
			case GaiaConstants.SpawnerResourceType.TerrainDetail:
				return spawnInfo.m_spawner.m_resources.m_detailPrototypes[this.m_selectedResourceIdx].m_dna.m_boundsRadius * spawnInfo.m_spawner.m_resources.m_detailPrototypes[this.m_selectedResourceIdx].m_dna.m_maxScale;
			case GaiaConstants.SpawnerResourceType.TerrainTree:
				return spawnInfo.m_spawner.m_resources.m_treePrototypes[this.m_selectedResourceIdx].m_dna.m_boundsRadius * spawnInfo.m_spawner.m_resources.m_treePrototypes[this.m_selectedResourceIdx].m_dna.m_maxScale;
			default:
				return spawnInfo.m_spawner.m_resources.m_gameObjectPrototypes[this.m_selectedResourceIdx].m_dna.m_boundsRadius * spawnInfo.m_spawner.m_resources.m_gameObjectPrototypes[this.m_selectedResourceIdx].m_dna.m_maxScale;
			}
		}

		private void OnDrawGizmos()
		{
			if (this.m_resources == null)
			{
				return;
			}
			if (this.m_spawner == null)
			{
				return;
			}
			if (this.m_terrainHeightMap == null)
			{
				return;
			}
			float y = base.transform.position.y;
			float num = base.transform.position.x - this.m_range;
			float num2 = base.transform.position.x + this.m_range;
			float num3 = base.transform.position.z - this.m_range;
			float num4 = base.transform.position.z + this.m_range;
			float radius = Mathf.Clamp(this.m_resolution * 0.25f, 0.5f, 5f);
			this.m_spawner.m_spawnRange = this.m_range;
			this.m_spawner.m_spawnerBounds = new Bounds(base.transform.position, new Vector3(this.m_range * 2f, this.m_range * 20f, this.m_range * 2f));
			SpawnInfo spawnInfo = new SpawnInfo();
			Vector3 locationWU = default(Vector3);
			if ((DateTime.Now - this.m_lastCacheUpdateDate).TotalSeconds > 5.0)
			{
				this.m_lastCacheUpdateDate = DateTime.Now;
				this.m_spawner.DeleteSpawnCaches(false);
				this.m_spawner.CreateSpawnCaches(this.m_selectedResourceType, this.m_selectedResourceIdx);
				Terrain terrain = TerrainHelper.GetTerrain(base.transform.position);
				if (terrain != null)
				{
					base.transform.position = new Vector3(base.transform.position.x, terrain.SampleHeight(base.transform.position) + 5f, base.transform.position.z);
				}
			}
			spawnInfo.m_textureStrengths = new float[Terrain.activeTerrain.terrainData.alphamapLayers];
			for (float num5 = num; num5 < num2; num5 += this.m_resolution)
			{
				for (float num6 = num3; num6 < num4; num6 += this.m_resolution)
				{
					locationWU.Set(num5, y, num6);
					if (this.m_spawner.CheckLocation(locationWU, ref spawnInfo))
					{
						float fitness = this.GetFitness(ref spawnInfo);
						if (fitness >= this.m_minimumFitness)
						{
							Gizmos.color = Color.Lerp(this.m_unfitColour, this.m_fitColour, fitness);
							Gizmos.DrawSphere(spawnInfo.m_hitLocationWU, radius);
						}
					}
				}
			}
			if (this.m_resources != null)
			{
				Bounds bounds = default(Bounds);
				if (TerrainHelper.GetTerrainBounds(base.transform.position, ref bounds))
				{
					bounds.center = new Vector3(bounds.center.x, this.m_resources.m_seaLevel, bounds.center.z);
					bounds.size = new Vector3(bounds.size.x, 0.05f, bounds.size.z);
					Gizmos.color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, Color.blue.a / 4f);
					Gizmos.DrawCube(bounds.center, bounds.size);
				}
			}
		}

		[Tooltip("Choose the resources - these are the resources that will be managed.")]
		public GaiaResource m_resources;

		[Tooltip("Visualiser range - controls how far the visualiser extends. Make smaller on lower powered computers.")]
		public float m_range = 200f;

		[Tooltip("Visualiser resolution. Make larger on lower powered computers.")]
		[Range(3f, 50f)]
		public float m_resolution = 25f;

		[Tooltip("Minimum fitness - points with fitness less than this value will not be shown.")]
		[Range(0f, 1f)]
		public float m_minimumFitness;

		[Tooltip("Controls which layers are checked for collisions. Must at least include the layer the terrain is on. Add additional layers if other collisions need to be detected as well. Influences terrain detection, tree detection and game object detection.")]
		public LayerMask m_fitnessCollisionLayers;

		[Tooltip("Colour of high fitness locations.")]
		public Color m_fitColour = Color.green;

		[Tooltip("Colour of low fitness locations.")]
		public Color m_unfitColour = Color.red;

		[HideInInspector]
		public Spawner m_spawner;

		[HideInInspector]
		public Vector3 m_lastHitPoint;

		[HideInInspector]
		public string m_lastHitObjectname;

		[HideInInspector]
		public float m_lastHitFitness;

		[HideInInspector]
		public float m_lastHitHeight;

		[HideInInspector]
		public float m_lastHitTerrainHeight;

		[HideInInspector]
		public float m_lastHitTerrainRelativeHeight;

		[HideInInspector]
		public float m_lastHitTerrainSlope;

		[HideInInspector]
		public float m_lastHitAreaSlope;

		[HideInInspector]
		public bool m_lastHitWasVirgin = true;

		[HideInInspector]
		public GaiaConstants.SpawnerResourceType m_selectedResourceType;

		[HideInInspector]
		public int m_selectedResourceIdx;

		[HideInInspector]
		private DateTime m_lastUpdateDate = DateTime.Now;

		[HideInInspector]
		private DateTime m_lastCacheUpdateDate = DateTime.Now;

		private UnityHeightMap m_terrainHeightMap;
	}
}
