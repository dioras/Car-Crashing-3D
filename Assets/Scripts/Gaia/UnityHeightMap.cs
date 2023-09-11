using System;
using System.IO;
using UnityEngine;

namespace Gaia
{
	public class UnityHeightMap : HeightMap
	{
		public UnityHeightMap()
		{
		}

		public UnityHeightMap(string path) : base(path)
		{
			this.m_boundsWU.size = new Vector3((float)this.m_widthX, 0f, (float)this.m_depthZ);
			this.m_isDirty = false;
		}

		public UnityHeightMap(TextAsset source) : base(source.bytes)
		{
			this.m_boundsWU.size = new Vector3((float)this.m_widthX, 0f, (float)this.m_depthZ);
			this.m_isDirty = false;
		}

		public UnityHeightMap(UnityHeightMap source) : base(source)
		{
			this.m_boundsWU = source.m_boundsWU;
			this.m_isDirty = false;
		}

		public UnityHeightMap(Terrain terrain)
		{
			this.LoadFromTerrain(terrain);
		}

		public UnityHeightMap(Bounds bounds, string sourceFile) : base(sourceFile)
		{
			this.m_boundsWU = bounds;
			this.m_isDirty = false;
		}

		public UnityHeightMap(Texture2D texture)
		{
			this.LoadFromTexture2D(texture);
			this.m_isDirty = false;
		}

		public Bounds GetBoundsWU()
		{
			return this.m_boundsWU;
		}

		public Vector3 GetPositionWU()
		{
			return this.m_boundsWU.center - this.m_boundsWU.extents;
		}

		public void SetBoundsWU(Bounds bounds)
		{
			this.m_boundsWU = bounds;
			this.m_isDirty = true;
		}

		public void SetPositionWU(Vector3 position)
		{
			this.m_boundsWU.center = position;
			this.m_isDirty = true;
		}

		public void LoadFromTerrain(Terrain terrain)
		{
			base.Reset();
			this.m_boundsWU.center = terrain.transform.position;
			this.m_boundsWU.size = terrain.terrainData.size;
			this.m_boundsWU.center = this.m_boundsWU.center + this.m_boundsWU.extents;
			this.m_widthX = terrain.terrainData.heightmapResolution;
			this.m_depthZ = terrain.terrainData.heightmapResolution;
			this.m_widthInvX = 1f / (float)this.m_widthX;
			this.m_depthInvZ = 1f / (float)this.m_depthZ;
			this.m_heights = terrain.terrainData.GetHeights(0, 0, this.m_widthX, this.m_depthZ);
			this.m_isPowerOf2 = (Utils.Math_IsPowerOf2(this.m_widthX) && Utils.Math_IsPowerOf2(this.m_depthZ));
			this.m_isDirty = false;
		}

		public void SaveToTerrain(Terrain terrain)
		{
			int heightmapWidth = terrain.terrainData.heightmapResolution;
			int heightmapHeight = terrain.terrainData.heightmapResolution;
			if (this.m_widthX == heightmapWidth && this.m_depthZ == heightmapHeight)
			{
				terrain.terrainData.SetHeights(0, 0, this.m_heights);
				this.m_isDirty = false;
				return;
			}
			float[,] array = new float[heightmapWidth, heightmapHeight];
			for (int i = 0; i < heightmapWidth; i++)
			{
				for (int j = 0; j < heightmapHeight; j++)
				{
					array[i, j] = base[(float)i / (float)heightmapWidth, (float)j / (float)heightmapHeight];
				}
			}
			terrain.terrainData.SetHeights(0, 0, array);
			this.m_isDirty = false;
		}

		public void LoadFromTexture2D(Texture2D texture)
		{
			Utils.MakeTextureReadable(texture);
			this.m_widthX = texture.width;
			this.m_depthZ = texture.height;
			this.m_widthInvX = 1f / (float)this.m_widthX;
			this.m_depthInvZ = 1f / (float)this.m_depthZ;
			this.m_heights = new float[this.m_widthX, this.m_depthZ];
			this.m_isPowerOf2 = (Utils.Math_IsPowerOf2(this.m_widthX) && Utils.Math_IsPowerOf2(this.m_depthZ));
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					this.m_heights[i, j] = texture.GetPixel(i, j).grayscale;
				}
			}
			this.m_isDirty = false;
		}

		public void ReadRawFromTextAsset(TextAsset asset)
		{
			using (Stream stream = new MemoryStream(asset.bytes))
			{
				using (BinaryReader binaryReader = new BinaryReader(stream))
				{
					this.m_widthX = (this.m_depthZ = Mathf.CeilToInt(Mathf.Sqrt((float)(stream.Length / 2L))));
					this.m_widthInvX = 1f / (float)this.m_widthX;
					this.m_depthInvZ = 1f / (float)this.m_depthZ;
					this.m_heights = new float[this.m_widthX, this.m_depthZ];
					this.m_isPowerOf2 = (Utils.Math_IsPowerOf2(this.m_widthX) && Utils.Math_IsPowerOf2(this.m_depthZ));
					for (int i = 0; i < this.m_widthX; i++)
					{
						for (int j = 0; j < this.m_depthZ; j++)
						{
							this.m_heights[i, j] = (float)binaryReader.ReadUInt16() / 65535f;
						}
					}
				}
				stream.Close();
			}
			this.m_isDirty = false;
		}

		public Bounds m_boundsWU = default(Bounds);
	}
}
