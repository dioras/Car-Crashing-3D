using System;
using UnityEngine;

namespace Gaia
{
	public class WaterFlowMap
	{
		public void CreateWaterFlowMap(Terrain terrain)
		{
			this.m_heightMap = new UnityHeightMap(terrain);
			int num = this.m_heightMap.Width();
			int num2 = this.m_heightMap.Depth();
			this.m_waterFlowMap = new HeightMap(num, num2);
			for (int i = 1; i < num - 1; i++)
			{
				for (int j = 1; j < num2 - 1; j++)
				{
					this.TraceWaterFlow(i, j, num, num2);
				}
			}
			this.m_waterFlowMap.Flip();
			this.m_waterFlowMap.Smooth(this.m_waterflowSmoothIterations);
		}

		private void TraceWaterFlow(int startX, int startZ, int width, int height)
		{
			float num = this.m_dropletVolume;
			int num2 = startX;
			int num3 = startZ;
			while (num > 0f)
			{
				HeightMap waterFlowMap= this.m_waterFlowMap;
				int x= num2;
				int z= num3;
				(waterFlowMap )[x , z ] = waterFlowMap[x, z] + this.m_dropletAbsorbtionRate;
				num -= this.m_dropletAbsorbtionRate;
				float num5;
				float num4 = num5 = this.m_heightMap[num2, num3];
				int num6 = num2;
				int num7 = num3;
				for (int i = -1; i < 2; i++)
				{
					for (int j = -1; j < 2; j++)
					{
						int num8 = num2 + i;
						int num9 = num3 + j;
						if (num8 >= 0 && num8 < width && num9 >= 0 && num9 < height && this.m_heightMap[num8, num9] < num5)
						{
							num6 = num8;
							num7 = num9;
							num5 = this.m_heightMap[num8, num9];
						}
					}
				}
				if (num4 == num5)
				{
					UnityHeightMap heightMap= this.m_heightMap;
					int x2= num2;
					int z2= num3;
					(heightMap )[x2 , z2 ] = heightMap[x2, z2] + this.m_dropletAbsorbtionRate;
				}
				else
				{
					num2 = num6;
					num3 = num7;
				}
			}
		}

		public void ExportWaterMapToPath(string path)
		{
			Utils.CompressToSingleChannelFileImage(this.m_waterFlowMap.Heights(), path, TextureFormat.RGBA32, true, false);
		}

		public float m_dropletVolume = 0.3f;

		public float m_dropletAbsorbtionRate = 0.05f;

		public int m_waterflowSmoothIterations = 1;

		private UnityHeightMap m_heightMap;

		private HeightMap m_waterFlowMap;
	}
}
