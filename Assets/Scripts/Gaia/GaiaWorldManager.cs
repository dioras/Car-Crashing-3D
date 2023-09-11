using System;
using System.Text;
using UnityEngine;

namespace Gaia
{
	public class GaiaWorldManager
	{
		public GaiaWorldManager()
		{
		}

		public GaiaWorldManager(Terrain[] terrains)
{
	Terrain terrain = null;
	m_worldBoundsWU = default(Bounds);
	m_worldBoundsTU = default(Bounds);
	m_worldBoundsNU = default(Bounds);
	string text = IsValidWorld(terrains);
	if (!string.IsNullOrEmpty(text))
	{
		Debug.LogError("GaiaWorldManager(terrains) ERROR" + text);
		return;
	}
	for (int i = 0; i < terrains.Length; i++)
	{
		terrain = terrains[i];
		Bounds bounds = new Bounds(terrain.transform.position, terrain.terrainData.size);
		bounds.center += bounds.extents;
		if (i == 0)
		{
			m_worldBoundsWU = new Bounds(bounds.center, bounds.size);
		}
		else
		{
			m_worldBoundsWU.Encapsulate(bounds);
		}
		Bounds bounds2 = default(Bounds);
		float num = terrain.terrainData.heightmapResolution;
		Vector3 size = terrain.terrainData.size;
		float x = num / size.x;
		float num2 = terrain.terrainData.heightmapResolution - 1;
		Vector3 size2 = terrain.terrainData.size;
		float x2 = size2.x;
		Vector3 size3 = terrain.terrainData.size;
		float num3 = num2 / Mathf.Max(x2, size3.z);
		Vector3 size4 = terrain.terrainData.size;
		float num4 = num3 * size4.y;
		Vector3 size5 = terrain.terrainData.size;
		float y = num4 / size5.y;
		float num5 = terrain.terrainData.heightmapResolution;
		Vector3 size6 = terrain.terrainData.size;
		m_WUtoTU = new Vector3(x, y, num5 / size6.z);
		m_TUtoWU = new Vector3(1f / m_WUtoTU.x, 1f / m_WUtoTU.y, 1f / m_WUtoTU.z);
		bounds2.center = Vector3.Scale(bounds.center, m_WUtoTU);
		bounds2.size = Vector3.Scale(bounds.size, m_WUtoTU);
		if (i == 0)
		{
			m_worldBoundsTU = new Bounds(bounds2.center, bounds2.size);
		}
		else
		{
			m_worldBoundsTU.Encapsulate(bounds2);
		}
	}
	if (terrain != null)
	{
		Vector3 size7 = m_worldBoundsTU.size;
		float x3 = 1f / size7.x;
		Vector3 size8 = m_worldBoundsTU.size;
		float y2 = 1f / size8.y;
		Vector3 size9 = m_worldBoundsTU.size;
		m_TUtoNU = new Vector3(x3, y2, 1f / size9.z);
		m_NUtoTU = m_worldBoundsTU.size;
		m_WUtoNU = Vector3.Scale(m_WUtoTU, m_TUtoNU);
		m_NUtoWU = m_worldBoundsWU.size;
	}
	m_worldBoundsNU.center = Vector3.Scale(m_worldBoundsTU.center, m_TUtoNU);
	m_worldBoundsNU.size = Vector3.Scale(m_worldBoundsTU.size, m_TUtoNU);
	m_NUZeroOffset = Vector3.zero - m_worldBoundsNU.min;
	m_TUZeroOffset = Vector3.zero - m_worldBoundsTU.min;
	Vector3 size10 = m_worldBoundsNU.size;
	float x4 = size10.x;
	Vector3 size11 = m_worldBoundsNU.size;
	m_tileCount = (int)(x4 * size11.z);
	Vector3 size12 = m_worldBoundsNU.size;
	int num6 = (int)size12.x;
	Vector3 size13 = m_worldBoundsNU.size;
	m_physicalTerrainArray = new Terrain[num6, (int)size13.z];
	Vector3 size14 = m_worldBoundsNU.size;
	int num7 = (int)size14.x;
	Vector3 size15 = m_worldBoundsNU.size;
	m_heightMapTerrainArray = new UnityHeightMap[num7, (int)size15.z];
	for (int j = 0; j < terrains.Length; j++)
	{
		terrain = terrains[j];
		Vector3 vector = WUtoPTI(terrain.transform.position);
		m_physicalTerrainArray[(int)vector.x, (int)vector.z] = terrain;
	}
}

		public int TileCount
		{
			get
			{
				return this.m_tileCount;
			}
		}

		public Terrain[,] PhysicalTerrainArray
		{
			get
			{
				return this.m_physicalTerrainArray;
			}
			set
			{
				this.m_physicalTerrainArray = value;
			}
		}

		public UnityHeightMap[,] HeightMapTerrainArray
		{
			get
			{
				return this.m_heightMapTerrainArray;
			}
			set
			{
				this.m_heightMapTerrainArray = value;
			}
		}

		public Bounds WorldBoundsWU
		{
			get
			{
				return this.m_worldBoundsWU;
			}
		}

		public Bounds WorldBoundsTU
		{
			get
			{
				return this.m_worldBoundsTU;
			}
		}

		public Bounds WorldBoundsNU
		{
			get
			{
				return this.m_worldBoundsNU;
			}
		}

		public Vector3 WUtoTUConversionFactor
		{
			get
			{
				return this.m_WUtoTU;
			}
		}

		public Vector3 WUtoNUConversionFactor
		{
			get
			{
				return this.m_WUtoNU;
			}
		}

		public ulong BoundsCheckErrors
		{
			get
			{
				return this.m_boundsCheckErrors;
			}
			set
			{
				this.m_boundsCheckErrors = value;
			}
		}

		public string IsValidWorld(Terrain[] terrains)
		{
			Terrain terrain = null;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Terrain terrain2 in terrains)
			{
				if (terrain == null)
				{
					terrain = terrain2;
				}
				if (terrain2.terrainData.size.x != terrain2.terrainData.size.z)
				{
					stringBuilder.Append(string.Format("\nTerrain {0} is not a square {1} {2}", terrain2.name, terrain2.terrainData.size.x, terrain2.terrainData.size.z));
				}
				if (terrain2.terrainData.size != terrain.terrainData.size)
				{
					stringBuilder.Append(string.Format("\nTerrain {0} - {1} size does not match {2} {3}", new object[]
					{
						terrain2.name,
						terrain.name,
						terrain2.terrainData.size,
						terrain.terrainData.size
					}));
				}
				if (terrain2.terrainData.heightmapResolution != terrain.terrainData.heightmapResolution)
				{
					stringBuilder.Append(string.Format("\nTerrain {0} - {1} heightmapResolution does not match {2} {3}", new object[]
					{
						terrain2.name,
						terrain.name,
						terrain2.terrainData.heightmapResolution,
						terrain.terrainData.heightmapResolution
					}));
				}
				if (terrain2.terrainData.alphamapResolution != terrain.terrainData.alphamapResolution)
				{
					stringBuilder.Append(string.Format("\nTerrain {0} - {1} alphamapResolution does not match {2} {3}", new object[]
					{
						terrain2.name,
						terrain.name,
						terrain2.terrainData.alphamapResolution,
						terrain.terrainData.alphamapResolution
					}));
				}
				if (terrain2.terrainData.baseMapResolution != terrain.terrainData.baseMapResolution)
				{
					stringBuilder.Append(string.Format("\nTerrain {0} - {1} baseMapResolution does not match {2} {3}", new object[]
					{
						terrain2.name,
						terrain.name,
						terrain2.terrainData.baseMapResolution,
						terrain.terrainData.baseMapResolution
					}));
				}
				if (terrain2.terrainData.detailResolution != terrain.terrainData.detailResolution)
				{
					stringBuilder.Append(string.Format("\nTerrain {0} - {1} detailResolution does not match {2} {3}", new object[]
					{
						terrain2.name,
						terrain.name,
						terrain2.terrainData.detailResolution,
						terrain.terrainData.detailResolution
					}));
				}
				if (terrain2.terrainData.alphamapLayers != terrain.terrainData.alphamapLayers)
				{
					stringBuilder.Append(string.Format("\nTerrain {0} - {1} alphamapLayers does not match {2} {3}", new object[]
					{
						terrain2.name,
						terrain.name,
						terrain2.terrainData.alphamapLayers,
						terrain.terrainData.alphamapLayers
					}));
				}
				if (terrain2.terrainData.detailPrototypes.Length != terrain.terrainData.detailPrototypes.Length)
				{
					stringBuilder.Append(string.Format("\nTerrain {0} - {1} detailPrototypes.Length does not match {2} {3}", new object[]
					{
						terrain2.name,
						terrain.name,
						terrain2.terrainData.detailPrototypes.Length,
						terrain.terrainData.detailPrototypes.Length
					}));
				}
				if (terrain2.terrainData.splatPrototypes.Length != terrain.terrainData.splatPrototypes.Length)
				{
					stringBuilder.Append(string.Format("\nTerrain {0} - {1} splatPrototypes.Length does not match {2} {3}", new object[]
					{
						terrain2.name,
						terrain.name,
						terrain2.terrainData.splatPrototypes.Length,
						terrain.terrainData.splatPrototypes.Length
					}));
				}
				if (terrain2.terrainData.treePrototypes.Length != terrain.terrainData.treePrototypes.Length)
				{
					stringBuilder.Append(string.Format("\nTerrain {0} - {1} treePrototypes.Length does not match {2} {3}", new object[]
					{
						terrain2.name,
						terrain.name,
						terrain2.terrainData.treePrototypes.Length,
						terrain.terrainData.treePrototypes.Length
					}));
				}
			}
			return stringBuilder.ToString();
		}

		private Terrain GetTerrainWU(Vector3 positionWU)
		{
			if (!this.InBoundsWU(positionWU))
			{
				this.m_boundsCheckErrors += 1UL;
				return null;
			}
			Vector3 vector = this.WUtoPTI(positionWU);
			return this.m_physicalTerrainArray[(int)vector.x, (int)vector.z];
		}

		private Terrain GetTerrainTU(Vector3 positionTU)
		{
			if (!this.InBoundsTU(positionTU))
			{
				this.m_boundsCheckErrors += 1UL;
				return null;
			}
			Vector3 vector = this.TUtoPTI(positionTU);
			return this.m_physicalTerrainArray[(int)vector.x, (int)vector.z];
		}

		private Terrain GetTerrainNU(Vector3 positionNU)
		{
			if (!this.InBoundsNU(positionNU))
			{
				this.m_boundsCheckErrors += 1UL;
				return null;
			}
			Vector3 vector = this.NUtoPTI(positionNU);
			return this.m_physicalTerrainArray[(int)vector.x, (int)vector.z];
		}

		private UnityHeightMap GetHeightMapWU(Vector3 positionWU)
		{
			if (!this.InBoundsWU(positionWU))
			{
				this.m_boundsCheckErrors += 1UL;
				return null;
			}
			Vector3 vector = this.WUtoPTI(positionWU);
			UnityHeightMap unityHeightMap = this.m_heightMapTerrainArray[(int)vector.x, (int)vector.z];
			if (unityHeightMap == null)
			{
				Terrain terrainWU = this.GetTerrainWU(positionWU);
				if (terrainWU != null)
				{
					unityHeightMap = (this.m_heightMapTerrainArray[(int)vector.x, (int)vector.z] = new UnityHeightMap(terrainWU));
				}
			}
			return unityHeightMap;
		}

		private UnityHeightMap GetHeightMapTU(Vector3 positionTU)
		{
			if (!this.InBoundsTU(positionTU))
			{
				this.m_boundsCheckErrors += 1UL;
				return null;
			}
			Vector3 vector = this.TUtoPTI(positionTU);
			UnityHeightMap unityHeightMap = this.m_heightMapTerrainArray[(int)vector.x, (int)vector.z];
			if (unityHeightMap == null)
			{
				Terrain terrainTU = this.GetTerrainTU(positionTU);
				if (terrainTU != null)
				{
					unityHeightMap = (this.m_heightMapTerrainArray[(int)vector.x, (int)vector.z] = new UnityHeightMap(terrainTU));
				}
			}
			return unityHeightMap;
		}

		private UnityHeightMap GetHeightMapNU(Vector3 positionNU)
		{
			if (!this.InBoundsNU(positionNU))
			{
				this.m_boundsCheckErrors += 1UL;
				return null;
			}
			Vector3 vector = this.NUtoPTI(positionNU);
			UnityHeightMap unityHeightMap = this.m_heightMapTerrainArray[(int)vector.x, (int)vector.z];
			if (unityHeightMap == null)
			{
				Terrain terrainNU = this.GetTerrainNU(positionNU);
				if (terrainNU != null)
				{
					unityHeightMap = (this.m_heightMapTerrainArray[(int)vector.x, (int)vector.z] = new UnityHeightMap(terrainNU));
				}
			}
			return unityHeightMap;
		}

		public void LoadFromWorld()
		{
			for (int i = 0; i < this.m_heightMapTerrainArray.GetLength(0); i++)
			{
				for (int j = 0; j < this.m_heightMapTerrainArray.GetLength(1); j++)
				{
					UnityHeightMap unityHeightMap = this.m_heightMapTerrainArray[i, j];
					if (unityHeightMap == null)
					{
						Terrain terrain = this.m_physicalTerrainArray[i, j];
						if (terrain != null)
						{
							this.m_heightMapTerrainArray[i, j] = new UnityHeightMap(terrain);
						}
					}
					else
					{
						unityHeightMap.LoadFromTerrain(this.m_physicalTerrainArray[i, j]);
					}
				}
			}
		}

		public void SaveToWorld(bool forceWrite = false)
		{
			for (int i = 0; i < this.m_heightMapTerrainArray.GetLength(0); i++)
			{
				for (int j = 0; j < this.m_heightMapTerrainArray.GetLength(1); j++)
				{
					UnityHeightMap unityHeightMap = this.m_heightMapTerrainArray[i, j];
					if (unityHeightMap != null)
					{
						if (!forceWrite)
						{
							if (unityHeightMap.IsDirty())
							{
								unityHeightMap.SaveToTerrain(this.m_physicalTerrainArray[i, j]);
							}
						}
						else
						{
							unityHeightMap.SaveToTerrain(this.m_physicalTerrainArray[i, j]);
						}
					}
				}
			}
		}

		public void SetHeightWU(float heightWU)
		{
			float height = Mathf.Clamp01(heightWU / this.m_worldBoundsWU.size.y);
			for (int i = 0; i < this.m_heightMapTerrainArray.GetLength(0); i++)
			{
				for (int j = 0; j < this.m_heightMapTerrainArray.GetLength(1); j++)
				{
					this.m_heightMapTerrainArray[i, j].SetHeight(height);
				}
			}
		}

		public void SetHeightWU(Vector3 positionWU, float height)
		{
			UnityHeightMap heightMapWU = this.GetHeightMapWU(positionWU);
			if (heightMapWU != null)
			{
				positionWU = this.WUtoPTO(positionWU);
				heightMapWU[(int)positionWU.x, (int)positionWU.z] = height;
			}
			else
			{
				this.m_boundsCheckErrors += 1UL;
			}
		}

		public float GetHeightWU(Vector3 positionWU)
		{
			UnityHeightMap heightMapWU = this.GetHeightMapWU(positionWU);
			if (heightMapWU != null)
			{
				positionWU = this.WUtoPTO(positionWU);
				return heightMapWU[(int)positionWU.x, (int)positionWU.z];
			}
			return float.MinValue;
		}

		public float GetHeightInterpolatedWU(Vector3 positionWU)
		{
			UnityHeightMap heightMapWU = this.GetHeightMapWU(positionWU);
			if (heightMapWU != null)
			{
				positionWU = this.WUtoPTO(positionWU);
				return heightMapWU[positionWU.x, positionWU.z];
			}
			return float.MinValue;
		}

		public void SetHeightTU(Vector3 positionTU, float height)
		{
			UnityHeightMap heightMapTU = this.GetHeightMapTU(positionTU);
			if (heightMapTU != null)
			{
				positionTU = this.TUtoPTO(positionTU);
				heightMapTU[(int)positionTU.x, (int)positionTU.z] = height;
			}
			else
			{
				this.m_boundsCheckErrors += 1UL;
			}
		}

		public float GetHeightTU(Vector3 positionTU)
		{
			UnityHeightMap heightMapTU = this.GetHeightMapTU(positionTU);
			if (heightMapTU != null)
			{
				positionTU = this.TUtoPTO(positionTU);
				return heightMapTU[(int)positionTU.x, (int)positionTU.z];
			}
			return float.MinValue;
		}

		public float GetHeightInterpolatedTU(Vector3 positionTU)
		{
			UnityHeightMap heightMapTU = this.GetHeightMapTU(positionTU);
			if (heightMapTU != null)
			{
				positionTU = this.TUtoPTO(positionTU);
				return heightMapTU[positionTU.x, positionTU.z];
			}
			return float.MinValue;
		}

		public void FlattenWorld()
		{
			for (int i = 0; i < this.m_heightMapTerrainArray.GetLength(0); i++)
			{
				for (int j = 0; j < this.m_heightMapTerrainArray.GetLength(1); j++)
				{
					UnityHeightMap unityHeightMap = this.m_heightMapTerrainArray[i, j];
					if (unityHeightMap == null)
					{
						Terrain terrain = this.m_physicalTerrainArray[i, j];
						if (terrain != null)
						{
							unityHeightMap = (this.m_heightMapTerrainArray[i, j] = new UnityHeightMap(terrain));
						}
					}
					if (unityHeightMap != null)
					{
						unityHeightMap.SetHeight(0f);
						unityHeightMap.SaveToTerrain(this.m_physicalTerrainArray[i, j]);
					}
				}
			}
		}

		public void SmoothWorld()
		{
			for (int i = 0; i < this.m_heightMapTerrainArray.GetLength(0); i++)
			{
				for (int j = 0; j < this.m_heightMapTerrainArray.GetLength(1); j++)
				{
					UnityHeightMap unityHeightMap = this.m_heightMapTerrainArray[i, j];
					if (unityHeightMap == null)
					{
						Terrain terrain = this.m_physicalTerrainArray[i, j];
						if (terrain != null)
						{
							unityHeightMap = (this.m_heightMapTerrainArray[i, j] = new UnityHeightMap(terrain));
						}
					}
					if (unityHeightMap != null)
					{
						unityHeightMap.Smooth(1);
						unityHeightMap.SaveToTerrain(this.m_physicalTerrainArray[i, j]);
					}
				}
			}
		}

		public void ExportWorldAsPng(string path)
		{
			Vector3 center = this.m_worldBoundsTU.center;
			HeightMap heightMap = new HeightMap((int)this.m_worldBoundsTU.size.z, (int)this.m_worldBoundsTU.size.x);
			int num = 0;
			for (int i = (int)this.m_worldBoundsTU.min.x; i < (int)this.m_worldBoundsTU.max.x; i++)
			{
				center.x = (float)i;
				int num2 = 0;
				for (int j = (int)this.m_worldBoundsTU.min.z; j < (int)this.m_worldBoundsTU.max.z; j++)
				{
					center.z = (float)j;
					heightMap[num2, num] = this.GetHeightTU(center);
					num2++;
				}
				num++;
			}
			Utils.CompressToSingleChannelFileImage(heightMap.Heights(), path, TextureFormat.RGBA32, true, false);
		}

		public void ExportSplatmapAsPng(string path, int textureIdx)
		{
			Terrain activeTerrain = Terrain.activeTerrain;
			if (activeTerrain == null)
			{
				UnityEngine.Debug.LogError("No active terrain, unable to export splatmaps");
				return;
			}
			int alphamapWidth = activeTerrain.terrainData.alphamapWidth;
			int alphamapHeight = activeTerrain.terrainData.alphamapHeight;
			int alphamapLayers = activeTerrain.terrainData.alphamapLayers;
			if (textureIdx < alphamapLayers)
			{
				HeightMap heightMap = new HeightMap(activeTerrain.terrainData.GetAlphamaps(0, 0, alphamapWidth, alphamapHeight), textureIdx);
				heightMap.Flip();
				Utils.CompressToSingleChannelFileImage(heightMap.Heights(), path, TextureFormat.RGBA32, true, false);
			}
			else
			{
				float[,,] alphamaps = activeTerrain.terrainData.GetAlphamaps(0, 0, alphamapWidth, alphamapHeight);
				Utils.CompressToMultiChannelFileImage(alphamaps, path, TextureFormat.RGBA32, true, false);
			}
		}

		public void ExportGrassmapAsPng(string path)
		{
			Terrain activeTerrain = Terrain.activeTerrain;
			if (activeTerrain == null)
			{
				UnityEngine.Debug.LogError("No active terrain, unable to export grassmaps");
				return;
			}
			int detailWidth = activeTerrain.terrainData.detailWidth;
			int detailHeight = activeTerrain.terrainData.detailHeight;
			int num = activeTerrain.terrainData.detailPrototypes.Length;
			float[,,] array = new float[detailWidth, detailHeight, num];
			for (int i = 0; i < activeTerrain.terrainData.detailPrototypes.Length; i++)
			{
				int[,] detailLayer = activeTerrain.terrainData.GetDetailLayer(0, 0, activeTerrain.terrainData.detailWidth, activeTerrain.terrainData.detailHeight, i);
				for (int j = 0; j < detailWidth; j++)
				{
					for (int k = 0; k < detailHeight; k++)
					{
						array[j, k, i] = (float)detailLayer[j, k] / 16f;
					}
				}
				for (int l = 0; l < detailWidth; l++)
				{
					for (int m = 0; m < detailHeight; m++)
					{
						array[m, l, i] = array[l, m, i];
					}
				}
			}
			Utils.CompressToMultiChannelFileImage(array, path, TextureFormat.RGBA32, true, false);
		}

		public void ExportNormalmapAsPng(string path)
		{
			for (int i = 0; i < this.m_physicalTerrainArray.GetLength(0); i++)
			{
				for (int j = 0; j < this.m_physicalTerrainArray.GetLength(1); j++)
				{
					Terrain terrain = this.m_physicalTerrainArray[i, j];
					if (terrain != null)
					{
						int heightmapWidth = terrain.terrainData.heightmapResolution;
						int heightmapHeight = terrain.terrainData.heightmapResolution;
						float[,,] array = new float[heightmapWidth, heightmapHeight, 4];
						for (int k = 0; k < heightmapWidth; k++)
						{
							for (int l = 0; l < heightmapHeight; l++)
							{
								Vector3 interpolatedNormal = terrain.terrainData.GetInterpolatedNormal((float)k / (float)heightmapWidth, (float)l / (float)heightmapHeight);
								array[k, l, 0] = interpolatedNormal.x * 0.5f + 0.5f;
								array[k, l, 1] = interpolatedNormal.y * 0.5f + 0.5f;
								array[k, l, 2] = interpolatedNormal.z * 0.5f + 0.5f;
							}
						}
						Utils.CompressToMultiChannelFileImage(array, string.Concat(new object[]
						{
							path,
							"_",
							i,
							"_",
							j
						}), TextureFormat.RGBA32, true, false);
					}
				}
			}
		}

		public void ExportShorelineMask(string path, float shoreHeightWU, float shoreWidthWU)
		{
			Vector3 center = this.m_worldBoundsTU.center;
			float shoreHeightNU = shoreHeightWU / this.m_worldBoundsWU.size.y;
			Vector3 vector = this.WUtoTU(new Vector3(shoreWidthWU, shoreWidthWU, shoreWidthWU));
			HeightMap heightMap = new HeightMap((int)this.m_worldBoundsTU.size.z, (int)this.m_worldBoundsTU.size.x);
			float num = 0f;
			for (float num2 = this.m_worldBoundsTU.min.x; num2 < this.m_worldBoundsTU.max.x; num2 += 1f)
			{
				center.x = num2;
				float num3 = 0f;
				for (float num4 = this.m_worldBoundsTU.min.z; num4 < this.m_worldBoundsTU.max.z; num4 += 1f)
				{
					center.z = num4;
					this.MakeMask(center, shoreHeightNU, vector.x, heightMap);
					num3 += 1f;
				}
				num += 1f;
			}
			heightMap.Flip();
			Utils.CompressToSingleChannelFileImage(heightMap.Heights(), path, TextureFormat.RGBA32, true, false);
		}

		private void MakeMask(Vector3 positionTU, float shoreHeightNU, float maskSizeTU, HeightMap waterMask)
		{
			float num = positionTU.x - maskSizeTU;
			float num2 = positionTU.x + maskSizeTU;
			float num3 = positionTU.z - maskSizeTU;
			float num4 = positionTU.z + maskSizeTU;
			Vector3 center = this.m_worldBoundsTU.center;
			for (float num5 = num; num5 < num2; num5 += 1f)
			{
				center.x = num5;
				for (float num6 = num3; num6 < num4; num6 += 1f)
				{
					center.z = num6;
					if (this.InBoundsTU(center) && this.GetHeightTU(center) <= shoreHeightNU)
					{
						float num7 = Utils.Math_Distance(num5, num6, positionTU.x, positionTU.z) / maskSizeTU;
						if (num7 <= 1f)
						{
							num7 = 1f - num7;
							int x = (int)(num5 + this.m_TUZeroOffset.x);
							int z = (int)(num6 + this.m_TUZeroOffset.z);
							if (num7 > waterMask[x, z])
							{
								waterMask[x, z] = num7;
							}
						}
					}
				}
			}
		}

		public bool InBoundsWU(Vector3 positionWU)
		{
			return positionWU.x >= this.m_worldBoundsWU.min.x && positionWU.z >= this.m_worldBoundsWU.min.z && positionWU.x < this.m_worldBoundsWU.max.x && positionWU.z < this.m_worldBoundsWU.max.z;
		}

		public bool InBoundsTU(Vector3 positionTU)
		{
			return positionTU.x >= this.m_worldBoundsTU.min.x && positionTU.z >= this.m_worldBoundsTU.min.z && positionTU.x < this.m_worldBoundsTU.max.x && positionTU.z < this.m_worldBoundsTU.max.z;
		}

		public bool InBoundsNU(Vector3 positionNU)
		{
			return positionNU.x >= this.m_worldBoundsNU.min.x && positionNU.z >= this.m_worldBoundsNU.min.z && positionNU.x < this.m_worldBoundsNU.max.x && positionNU.z < this.m_worldBoundsNU.max.z;
		}

		public Vector3 WUtoTU(Vector3 positionWU)
		{
			return Vector3.Scale(positionWU, this.m_WUtoTU);
		}

		public Vector3 WUtoNU(Vector3 positionWU)
		{
			return Vector3.Scale(positionWU, this.m_WUtoNU);
		}

		public Vector3 WUtoPTI(Vector3 positionWU)
		{
			return this.NUtoPTI(this.WUtoNU(positionWU));
		}

		public Vector3 WUtoPTO(Vector3 positionWU)
		{
			return this.TUtoPTO(this.WUtoTU(positionWU));
		}

		public Vector3 TUtoWU(Vector3 positionTU)
		{
			return Vector3.Scale(positionTU, this.m_TUtoWU);
		}

		public Vector3 TUtoNU(Vector3 positionTU)
		{
			return Vector3.Scale(positionTU, this.m_TUtoNU);
		}

		public Vector3 TUtoPTI(Vector3 positionTU)
		{
			return this.NUtoPTI(this.TUtoNU(positionTU));
		}

		public Vector3 TUtoPTO(Vector3 positionTU)
		{
			return new Vector3((positionTU.x + this.m_TUZeroOffset.x) % this.m_worldBoundsTU.size.x, (positionTU.y + this.m_TUZeroOffset.y) % this.m_worldBoundsTU.size.y, (positionTU.z + this.m_TUZeroOffset.z) % this.m_worldBoundsTU.size.z);
		}

		public Vector3 NUtoWU(Vector3 positionNU)
		{
			return Vector3.Scale(positionNU, this.m_NUtoWU);
		}

		public Vector3 NUtoTU(Vector3 positionNU)
		{
			return Vector3.Scale(positionNU, this.m_NUtoTU);
		}

		public Vector3 NUtoPTI(Vector3 positionNU)
		{
			return new Vector3(Mathf.Floor(positionNU.x + this.m_NUZeroOffset.x), Mathf.Floor(positionNU.y + this.m_NUZeroOffset.y), Mathf.Floor(positionNU.z + this.m_NUZeroOffset.z));
		}

		public Vector3 NUtoPTO(Vector3 positionNU)
		{
			return new Vector3((positionNU.x + this.m_NUZeroOffset.x) % 1f * this.m_worldBoundsTU.size.x, (positionNU.y + this.m_NUZeroOffset.y) % 1f * this.m_worldBoundsTU.size.y, (positionNU.z + this.m_NUZeroOffset.z) % 1f * this.m_worldBoundsTU.size.z);
		}

		public Vector3 Ceil(Vector3 source)
		{
			return new Vector3(Mathf.Ceil(source.x), Mathf.Ceil(source.y), Mathf.Ceil(source.z));
		}

		public Vector3 Floor(Vector3 source)
		{
			return new Vector3(Mathf.Floor(source.x), Mathf.Floor(source.y), Mathf.Floor(source.z));
		}

		public void Test()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GaiaWorldManagerTest\n");
			stringBuilder.Append(string.Format("World Bounds WU : Min {0}, Centre {1}, Max {2}, Size {3}\n", new object[]
			{
				this.m_worldBoundsWU.min,
				this.m_worldBoundsWU.center,
				this.m_worldBoundsWU.max,
				this.m_worldBoundsWU.size
			}));
			stringBuilder.Append(string.Format("World Bounds TU : Min {0}, Centre {1}, Max {2}, Size {3}\n", new object[]
			{
				this.m_worldBoundsTU.min,
				this.m_worldBoundsTU.center,
				this.m_worldBoundsTU.max,
				this.m_worldBoundsTU.size
			}));
			stringBuilder.Append(string.Format("World Bounds NU : Min {0}, Centre {1}, Max {2}, Size {3}\n", new object[]
			{
				this.m_worldBoundsNU.min,
				this.m_worldBoundsNU.center,
				this.m_worldBoundsNU.max,
				this.m_worldBoundsNU.size
			}));
			stringBuilder.Append("\nBounds Tests:");
			Vector3 vector = new Vector3(this.m_worldBoundsWU.min.x - 1f, this.m_worldBoundsWU.min.y, this.m_worldBoundsWU.min.z);
			stringBuilder.Append(string.Format("\n<MIN - InBoundsWU({0}) = {1}\n", vector, this.InBoundsWU(vector)));
			vector = new Vector3(this.m_worldBoundsWU.min.x, this.m_worldBoundsWU.min.y, this.m_worldBoundsWU.min.z);
			stringBuilder.Append(string.Format("  MIN - InBoundsWU({0}) = {1}\n", vector, this.InBoundsWU(vector)));
			vector = new Vector3(this.m_worldBoundsWU.max.x, this.m_worldBoundsWU.max.y, this.m_worldBoundsWU.max.z);
			stringBuilder.Append(string.Format("  MAX - InBoundsWU({0}) = {1}\n", vector, this.InBoundsWU(vector)));
			vector = new Vector3(this.m_worldBoundsWU.max.x + 1f, this.m_worldBoundsWU.max.y, this.m_worldBoundsWU.max.z);
			stringBuilder.Append(string.Format(">MAX - InBoundsWU({0}) = {1}\n", vector, this.InBoundsWU(vector)));
			vector = new Vector3(this.m_worldBoundsTU.min.x - 1f, this.m_worldBoundsTU.min.y, this.m_worldBoundsTU.min.z);
			stringBuilder.Append(string.Format("\n<MIN - InBoundsTU({0}) = {1}\n", vector, this.InBoundsTU(vector)));
			vector = new Vector3(this.m_worldBoundsTU.min.x, this.m_worldBoundsTU.min.y, this.m_worldBoundsTU.min.z);
			stringBuilder.Append(string.Format("  MIN - InBoundsTU({0}) = {1}\n", vector, this.InBoundsTU(vector)));
			vector = new Vector3(this.m_worldBoundsTU.max.x, this.m_worldBoundsTU.max.y, this.m_worldBoundsTU.max.z);
			stringBuilder.Append(string.Format("  MAX - InBoundsTU({0}) = {1}\n", vector, this.InBoundsTU(vector)));
			vector = new Vector3(this.m_worldBoundsTU.max.x + 1f, this.m_worldBoundsTU.max.y, this.m_worldBoundsTU.max.y);
			stringBuilder.Append(string.Format(">MAX - InBoundsTU({0}) = {1}\n", vector, this.InBoundsTU(vector)));
			vector = new Vector3(this.m_worldBoundsNU.min.x - 0.1f, this.m_worldBoundsNU.min.y, this.m_worldBoundsNU.min.z);
			stringBuilder.Append(string.Format("\n<MIN - InBoundsNU({0}) = {1}\n", vector, this.InBoundsNU(vector)));
			vector = new Vector3(this.m_worldBoundsNU.min.x, this.m_worldBoundsNU.min.y, this.m_worldBoundsNU.min.z);
			stringBuilder.Append(string.Format("  MIN - InBoundsNU({0}) = {1}\n", vector, this.InBoundsNU(vector)));
			vector = new Vector3(this.m_worldBoundsNU.max.x, this.m_worldBoundsNU.max.y, this.m_worldBoundsNU.max.z);
			stringBuilder.Append(string.Format("  MAX - InBoundsNU({0}) = {1}\n", vector, this.InBoundsNU(vector)));
			vector = new Vector3(this.m_worldBoundsNU.max.x + 0.1f, this.m_worldBoundsNU.max.y, this.m_worldBoundsNU.max.z);
			stringBuilder.Append(string.Format(">MAX - InBoundsNU({0}) = {1}\n", vector, this.InBoundsNU(vector)));
			stringBuilder.Append("\nPosition Conversion Tests (<MIN, CENTRE, >MAX):");
			vector = new Vector3(this.m_worldBoundsWU.min.x - 1f, this.m_worldBoundsWU.center.y, this.m_worldBoundsWU.max.z + 1f);
			stringBuilder.Append(string.Format("\nInBoundsWU({0}) = {1}\n", vector, this.InBoundsWU(vector)));
			stringBuilder.Append(string.Format("WUtoTU({0}) = {1:0.000}, {2:0.000}\n", vector, this.WUtoTU(vector).x, this.WUtoTU(vector).z));
			stringBuilder.Append(string.Format("WUtoNU({0}) = {1:0.000}, {2:0.000}\n", vector, this.WUtoNU(vector).x, this.WUtoNU(vector).z));
			stringBuilder.Append(string.Format("WUtoPTI({0}) = {1}, {2}\n", vector, this.WUtoPTI(vector).x, this.WUtoPTI(vector).z));
			stringBuilder.Append(string.Format("WUtoPTO({0}) = {1}, {2}\n", vector, this.WUtoPTO(vector).x, this.WUtoPTO(vector).z));
			stringBuilder.Append("\nPosition Conversion Tests (MIN, CENTRE, MAX):");
			vector = new Vector3(this.m_worldBoundsWU.min.x, this.m_worldBoundsWU.center.y, this.m_worldBoundsWU.max.z);
			stringBuilder.Append(string.Format("\nInBoundsWU({0}) = {1}\n", vector, this.InBoundsWU(vector)));
			stringBuilder.Append(string.Format("WUtoTU({0}) = {1:0.000}, {2:0.000}\n", vector, this.WUtoTU(vector).x, this.WUtoTU(vector).z));
			stringBuilder.Append(string.Format("WUtoNU({0}) = {1:0.000}, {2:0.000}\n", vector, this.WUtoNU(vector).x, this.WUtoNU(vector).z));
			stringBuilder.Append(string.Format("WUtoPTI({0}) = {1}, {2}\n", vector, this.WUtoPTI(vector).x, this.WUtoPTI(vector).z));
			stringBuilder.Append(string.Format("WUtoPTO({0}) = {1}, {2}\n", vector, this.WUtoPTO(vector).x, this.WUtoPTO(vector).z));
			vector = this.WUtoTU(vector);
			stringBuilder.Append(string.Format("\nTUtoWU({0}) = {1}\n", vector, this.TUtoWU(vector)));
			stringBuilder.Append(string.Format("TUtoNU({0}) = {1}\n", vector, this.TUtoNU(vector)));
			vector = this.TUtoNU(vector);
			stringBuilder.Append(string.Format("\nNUtoWU({0}) = {1}\n", vector, this.NUtoWU(vector)));
			stringBuilder.Append(string.Format("NUtoTU({0}) = {1}\n", vector, this.NUtoTU(vector)));
			stringBuilder.Append("\nTerrain Tests:");
			this.FlattenWorld();
			this.m_boundsCheckErrors = 0UL;
			this.TestBlobWU(this.m_worldBoundsWU.min, 100, 0.25f);
			this.TestBlobTU(this.m_worldBoundsTU.center, 100, 0.5f);
			this.TestBlobWU(this.m_worldBoundsWU.max, 100, 1f);
			this.SaveToWorld(false);
			stringBuilder.Append(string.Format("Bounds check errors : {0}", this.m_boundsCheckErrors));
			UnityEngine.Debug.Log(stringBuilder.ToString());
		}

		public void TestBlobWU(Vector3 positionWU, int widthWU, float height)
		{
			Vector3 vector = this.WUtoTU(new Vector3((float)widthWU, (float)widthWU, (float)widthWU));
			Vector3 vector2 = this.WUtoTU(positionWU);
			for (int i = (int)(vector2.x - vector.x); i < (int)(vector2.x + vector.x); i++)
			{
				for (int j = (int)(vector2.z - vector.z); j < (int)(vector2.z + vector.z); j++)
				{
					Vector3 positionTU = new Vector3((float)i, this.m_worldBoundsTU.center.y, (float)j);
					this.SetHeightTU(positionTU, height);
				}
			}
		}

		public void TestBlobTU(Vector3 positionTU, int widthWU, float height)
		{
			Vector3 vector = this.WUtoTU(new Vector3((float)widthWU, (float)widthWU, (float)widthWU));
			for (int i = (int)(positionTU.x - vector.x); i < (int)(positionTU.x + vector.x); i++)
			{
				for (int j = (int)(positionTU.z - vector.z); j < (int)(positionTU.z + vector.z); j++)
				{
					Vector3 positionTU2 = new Vector3((float)i, this.m_worldBoundsTU.center.y, (float)j);
					this.SetHeightTU(positionTU2, height);
				}
			}
		}

		private Bounds m_worldBoundsWU = default(Bounds);

		private Bounds m_worldBoundsTU = default(Bounds);

		private Bounds m_worldBoundsNU = default(Bounds);

		private Vector3 m_WUtoTU = Vector3.one;

		private Vector3 m_TUtoWU = Vector3.one;

		private Vector3 m_TUtoNU = Vector3.one;

		private Vector3 m_NUtoTU = Vector3.one;

		private Vector3 m_WUtoNU = Vector3.one;

		private Vector3 m_NUtoWU = Vector3.one;

		private Vector3 m_NUZeroOffset = Vector3.zero;

		private Vector3 m_TUZeroOffset = Vector3.zero;

		private ulong m_boundsCheckErrors;

		private Terrain[,] m_physicalTerrainArray;

		private UnityHeightMap[,] m_heightMapTerrainArray;

		private int m_tileCount;
	}
}
