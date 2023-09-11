using System;

namespace GeNa
{
	public class Heightmap
	{
		public Heightmap()
		{
			this.Reset();
		}

		public Heightmap(int width, int depth)
		{
			this.m_widthX = width;
			this.m_depthZ = depth;
			this.m_widthInvX = 1f / (float)this.m_widthX;
			this.m_depthInvZ = 1f / (float)this.m_depthZ;
			this.m_heights = new float[this.m_widthX, this.m_depthZ];
			this.m_isPowerOf2 = (Heightmap.Math_IsPowerOf2(this.m_widthX) && Heightmap.Math_IsPowerOf2(this.m_depthZ));
			this.m_statMinVal = (this.m_statMaxVal = 0f);
			this.m_statSumVals = 0.0;
			this.m_metaData = new byte[0];
			this.m_isDirty = false;
		}

		public Heightmap(float[,] source)
		{
			this.m_widthX = source.GetLength(0);
			this.m_depthZ = source.GetLength(1);
			this.m_widthInvX = 1f / (float)this.m_widthX;
			this.m_depthInvZ = 1f / (float)this.m_depthZ;
			this.m_heights = new float[this.m_widthX, this.m_depthZ];
			this.m_isPowerOf2 = (Heightmap.Math_IsPowerOf2(this.m_widthX) && Heightmap.Math_IsPowerOf2(this.m_depthZ));
			this.m_statMinVal = (this.m_statMaxVal = 0f);
			this.m_statSumVals = 0.0;
			this.m_metaData = new byte[0];
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					this.m_heights[i, j] = source[i, j];
				}
			}
			this.m_isDirty = false;
		}

		public Heightmap(float[,,] source, int slice)
		{
			this.m_widthX = source.GetLength(0);
			this.m_depthZ = source.GetLength(1);
			this.m_widthInvX = 1f / (float)this.m_widthX;
			this.m_depthInvZ = 1f / (float)this.m_depthZ;
			this.m_heights = new float[this.m_widthX, this.m_depthZ];
			this.m_isPowerOf2 = (Heightmap.Math_IsPowerOf2(this.m_widthX) && Heightmap.Math_IsPowerOf2(this.m_depthZ));
			this.m_statMinVal = (this.m_statMaxVal = 0f);
			this.m_statSumVals = 0.0;
			this.m_metaData = new byte[0];
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					this.m_heights[i, j] = source[i, j, slice];
				}
			}
			this.m_isDirty = false;
		}

		public Heightmap(int[,] source)
		{
			this.m_widthX = source.GetLength(0);
			this.m_depthZ = source.GetLength(1);
			this.m_widthInvX = 1f / (float)this.m_widthX;
			this.m_depthInvZ = 1f / (float)this.m_depthZ;
			this.m_heights = new float[this.m_widthX, this.m_depthZ];
			this.m_isPowerOf2 = (Heightmap.Math_IsPowerOf2(this.m_widthX) && Heightmap.Math_IsPowerOf2(this.m_depthZ));
			this.m_statMinVal = (this.m_statMaxVal = 0f);
			this.m_statSumVals = 0.0;
			this.m_metaData = new byte[0];
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					this.m_heights[i, j] = (float)source[i, j];
				}
			}
			this.m_isDirty = false;
		}

		public Heightmap(Heightmap source)
		{
			this.Reset();
			this.m_widthX = source.m_widthX;
			this.m_depthZ = source.m_depthZ;
			this.m_widthInvX = 1f / (float)this.m_widthX;
			this.m_depthInvZ = 1f / (float)this.m_depthZ;
			this.m_heights = new float[this.m_widthX, this.m_depthZ];
			this.m_isPowerOf2 = source.m_isPowerOf2;
			this.m_metaData = new byte[source.m_metaData.Length];
			for (int i = 0; i < source.m_metaData.Length; i++)
			{
				this.m_metaData[i] = source.m_metaData[i];
			}
			for (int j = 0; j < this.m_widthX; j++)
			{
				for (int k = 0; k < this.m_depthZ; k++)
				{
					this.m_heights[j, k] = source.m_heights[j, k];
				}
			}
			this.m_isDirty = false;
		}

		public int Width()
		{
			return this.m_widthX;
		}

		public int Depth()
		{
			return this.m_depthZ;
		}

		public float MinVal()
		{
			return this.m_statMinVal;
		}

		public float MaxVal()
		{
			return this.m_statMaxVal;
		}

		public double SumVal()
		{
			return this.m_statSumVals;
		}

		public byte[] GetMetaData()
		{
			return this.m_metaData;
		}

		public bool IsDirty()
		{
			return this.m_isDirty;
		}

		public void SetDirty(bool dirty = true)
		{
			this.m_isDirty = dirty;
		}

		public void ClearDirty()
		{
			this.m_isDirty = false;
		}

		public void SetMetaData(byte[] metadata)
		{
			this.m_metaData = new byte[metadata.Length];
			Buffer.BlockCopy(metadata, 0, this.m_metaData, 0, metadata.Length);
			this.m_isDirty = true;
		}

		public float[,] Heights()
		{
			return this.m_heights;
		}

		public float GetSafeHeight(int x, int z)
		{
			if (x < 0)
			{
				x = 0;
			}
			if (z < 0)
			{
				z = 0;
			}
			if (x >= this.m_widthX)
			{
				x = this.m_widthX - 1;
			}
			if (z >= this.m_depthZ)
			{
				z = this.m_depthZ - 1;
			}
			return this.m_heights[x, z];
		}

		public void SetSafeHeight(int x, int z, float height)
		{
			if (x < 0)
			{
				x = 0;
			}
			if (z < 0)
			{
				z = 0;
			}
			if (x >= this.m_widthX)
			{
				x = this.m_widthX - 1;
			}
			if (z >= this.m_depthZ)
			{
				z = this.m_depthZ - 1;
			}
			this.m_heights[x, z] = height;
			this.m_isDirty = true;
		}

		protected float GetInterpolatedHeight(float x, float z)
		{
			x *= (float)this.m_widthX - 1f;
			z *= (float)this.m_depthZ - 1f;
			int num = (int)x;
			int num2 = (int)z;
			int num3 = num + 1;
			int num4 = num2 + 1;
			if (num3 >= this.m_widthX)
			{
				num3 = num;
			}
			if (num4 >= this.m_depthZ)
			{
				num4 = num2;
			}
			float num5 = x - (float)num;
			float num6 = z - (float)num2;
			float num7 = 1f - num5;
			float num8 = 1f - num6;
			return num7 * num8 * this.m_heights[num, num2] + num7 * num6 * this.m_heights[num, num4] + num5 * num8 * this.m_heights[num3, num2] + num5 * num6 * this.m_heights[num3, num4];
		}

		public float this[int x, int z]
		{
			get
			{
				return this.m_heights[x, z];
			}
			set
			{
				this.m_heights[x, z] = value;
				this.m_isDirty = true;
			}
		}

		public float this[float x, float z]
		{
			get
			{
				return this.GetInterpolatedHeight(x, z);
			}
			set
			{
				x *= (float)this.m_widthX;
				z *= (float)this.m_depthZ;
				this.m_heights[(int)x, (int)z] = value;
				this.m_isDirty = true;
			}
		}

		public void SetHeight(float height)
		{
			float num = Heightmap.Math_Clamp(0f, 1f, height);
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					this.m_heights[i, j] = num;
				}
			}
			this.m_isDirty = true;
		}

		public void GetHeightRange(ref float minHeight, ref float maxHeight)
		{
			maxHeight = float.MinValue;
			minHeight = float.MaxValue;
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					float num = this.m_heights[i, j];
					if (num > maxHeight)
					{
						maxHeight = num;
					}
					if (num < minHeight)
					{
						minHeight = num;
					}
				}
			}
		}

		public float GetSlope(int x, int z)
		{
			float num = this.m_heights[x, z];
			float num2 = this.m_heights[x + 1, z] - num;
			float num3 = this.m_heights[x, z + 1] - num;
			return (float)Math.Sqrt((double)(num2 * num2 + num3 * num3));
		}

		public float GetSlope(float x, float z)
		{
			float num = this.GetInterpolatedHeight(x + this.m_widthInvX * 0.9f, z) - this.GetInterpolatedHeight(x - this.m_widthInvX * 0.9f, z);
			float num2 = this.GetInterpolatedHeight(x, z + this.m_depthInvZ * 0.9f) - this.GetInterpolatedHeight(x, z - this.m_depthInvZ * 0.9f);
			return Heightmap.Math_Clamp(0f, 90f, (float)(Math.Sqrt((double)(num * num + num2 * num2)) * 10000.0));
		}

		public float GetSlope_a(float x, float z)
		{
			float interpolatedHeight = this.GetInterpolatedHeight(x, z);
			float num = Math.Abs(this.GetInterpolatedHeight(x - this.m_widthInvX, z) - interpolatedHeight);
			float num2 = Math.Abs(this.GetInterpolatedHeight(x + this.m_widthInvX, z) - interpolatedHeight);
			float num3 = Math.Abs(this.GetInterpolatedHeight(x, z - this.m_depthInvZ) - interpolatedHeight);
			float num4 = Math.Abs(this.GetInterpolatedHeight(x, z + this.m_depthInvZ) - interpolatedHeight);
			return (num + num2 + num3 + num4) / 4f * 400f;
		}

		public float GetBaseLevel()
		{
			float num = 0f;
			for (int i = 0; i < this.m_widthX; i++)
			{
				if (this.m_heights[i, 0] > num)
				{
					num = this.m_heights[i, 0];
				}
				if (this.m_heights[i, this.m_depthZ - 1] > num)
				{
					num = this.m_heights[i, this.m_depthZ - 1];
				}
			}
			for (int j = 0; j < this.m_depthZ; j++)
			{
				if (this.m_heights[0, j] > num)
				{
					num = this.m_heights[0, j];
				}
				if (this.m_heights[this.m_widthX - 1, j] > num)
				{
					num = this.m_heights[this.m_widthX - 1, j];
				}
			}
			return num;
		}

		public bool HasData()
		{
			return this.m_widthX > 0 && this.m_depthZ > 0 && this.m_heights != null && this.m_heights.GetLength(0) == this.m_widthX && this.m_heights.GetLength(1) == this.m_depthZ;
		}

		public void Reset()
		{
			this.m_widthX = (this.m_depthZ = 0);
			this.m_widthInvX = (this.m_depthInvZ = 0f);
			this.m_heights = null;
			this.m_statMinVal = (this.m_statMaxVal = 0f);
			this.m_statSumVals = 0.0;
			this.m_metaData = new byte[0];
			this.m_isDirty = false;
		}

		public void UpdateStats()
		{
			this.m_statMinVal = 1f;
			this.m_statMaxVal = 0f;
			this.m_statSumVals = 0.0;
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					float num = this.m_heights[i, j];
					if (num < this.m_statMinVal)
					{
						this.m_statMinVal = num;
					}
					if (num > this.m_statMaxVal)
					{
						this.m_statMaxVal = num;
					}
					this.m_statSumVals += (double)num;
				}
			}
		}

		public void Smooth(int iterations)
		{
			for (int i = 0; i < iterations; i++)
			{
				for (int j = 0; j < this.m_widthX; j++)
				{
					for (int k = 0; k < this.m_depthZ; k++)
					{
						this.m_heights[j, k] = Heightmap.Math_Clamp(0f, 1f, (this.GetSafeHeight(j - 1, k) + this.GetSafeHeight(j + 1, k) + this.GetSafeHeight(j, k - 1) + this.GetSafeHeight(j, k + 1)) / 4f);
					}
				}
			}
			this.m_isDirty = true;
		}

		public Heightmap GetSlopeMap()
		{
			Heightmap heightmap = new Heightmap(this);
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					heightmap[i, j] = this.GetSlope(i, j);
				}
			}
			return heightmap;
		}

		public void Invert()
		{
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					this.m_heights[i, j] = 1f - this.m_heights[i, j];
				}
			}
			this.m_isDirty = true;
		}

		public void Flip()
		{
			float[,] array = new float[this.m_depthZ, this.m_widthX];
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					array[j, i] = this.m_heights[i, j];
				}
			}
			this.m_heights = array;
			this.m_widthX = array.GetLength(0);
			this.m_depthZ = array.GetLength(1);
			this.m_widthInvX = 1f / (float)this.m_widthX;
			this.m_depthInvZ = 1f / (float)this.m_depthZ;
			this.m_isPowerOf2 = (Heightmap.Math_IsPowerOf2(this.m_widthX) && Heightmap.Math_IsPowerOf2(this.m_depthZ));
			this.m_statMinVal = (this.m_statMaxVal = 0f);
			this.m_statSumVals = 0.0;
			this.m_isDirty = true;
		}

		public void Normalise()
		{
			float num = float.MinValue;
			float num2 = float.MaxValue;
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					float num3 = this.m_heights[i, j];
					if (num3 > num)
					{
						num = num3;
					}
					if (num3 < num2)
					{
						num2 = num3;
					}
				}
			}
			float num4 = num - num2;
			if (num4 > 0f)
			{
				for (int k = 0; k < this.m_widthX; k++)
				{
					for (int l = 0; l < this.m_depthZ; l++)
					{
						this.m_heights[k, l] = (this.m_heights[k, l] - num2) / num4;
					}
				}
				this.m_isDirty = true;
			}
		}

		public static bool Math_IsPowerOf2(int value)
		{
			return (value & value - 1) == 0;
		}

		public static float Math_Clamp(float min, float max, float value)
		{
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		protected int m_widthX;

		protected int m_depthZ;

		protected float[,] m_heights;

		protected bool m_isPowerOf2;

		protected float m_widthInvX;

		protected float m_depthInvZ;

		protected float m_statMinVal;

		protected float m_statMaxVal;

		protected double m_statSumVals;

		protected bool m_isDirty;

		protected byte[] m_metaData = new byte[0];
	}
}
