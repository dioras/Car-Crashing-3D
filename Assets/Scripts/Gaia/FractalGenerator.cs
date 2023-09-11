using System;
using UnityEngine;

namespace Gaia
{
	public class FractalGenerator
	{
		public FractalGenerator()
		{
			this.FractalType = FractalGenerator.Fractals.Perlin;
		}

		public FractalGenerator(float frequency, float lacunarity, int octaves, float persistance, float seed, FractalGenerator.Fractals type)
		{
			this.m_frequency = frequency;
			this.m_lacunarity = lacunarity;
			this.m_octaves = octaves;
			this.m_persistence = persistance;
			this.m_seed = seed;
			switch (type)
			{
			case FractalGenerator.Fractals.Perlin:
				this.m_noiseCalculator = new FractalGenerator.GetCalcValue(this.GetValue_Perlin);
				break;
			case FractalGenerator.Fractals.Billow:
				this.m_noiseCalculator = new FractalGenerator.GetCalcValue(this.GetValue_Billow);
				break;
			case FractalGenerator.Fractals.RidgeMulti:
				this.CalcSpectralWeights();
				this.m_noiseCalculator = new FractalGenerator.GetCalcValue(this.GetValue_RidgedMulti);
				break;
			default:
				this.m_noiseCalculator = new FractalGenerator.GetCalcValue(this.GetValue_Perlin);
				break;
			}
		}

		public float Seed
		{
			get
			{
				return this.m_seed;
			}
			set
			{
				this.m_seed = value;
			}
		}

		public int Octaves
		{
			get
			{
				return this.m_octaves;
			}
			set
			{
				this.m_octaves = value;
			}
		}

		public float Persistence
		{
			get
			{
				return this.m_persistence;
			}
			set
			{
				this.m_persistence = value;
			}
		}

		public float Frequency
		{
			get
			{
				return this.m_frequency;
			}
			set
			{
				this.m_frequency = value;
			}
		}

		public float Lacunarity
		{
			get
			{
				return this.m_lacunarity;
			}
			set
			{
				this.m_lacunarity = value;
			}
		}

		public float XOffset
		{
			get
			{
				return this.m_XOffset;
			}
			set
			{
				this.m_XOffset = value;
			}
		}

		public float ZOffset
		{
			get
			{
				return this.m_ZOffset;
			}
			set
			{
				this.m_ZOffset = value;
			}
		}

		public float YOffset
		{
			get
			{
				return this.m_YOffset;
			}
			set
			{
				this.m_YOffset = value;
			}
		}

		public FractalGenerator.Fractals FractalType
		{
			get
			{
				return this.m_fractalType;
			}
			set
			{
				this.m_fractalType = value;
				FractalGenerator.Fractals fractalType = this.m_fractalType;
				if (fractalType != FractalGenerator.Fractals.Perlin)
				{
					if (fractalType != FractalGenerator.Fractals.Billow)
					{
						if (fractalType == FractalGenerator.Fractals.RidgeMulti)
						{
							this.CalcSpectralWeights();
							this.m_noiseCalculator = new FractalGenerator.GetCalcValue(this.GetValue_RidgedMulti);
						}
					}
					else
					{
						this.m_noiseCalculator = new FractalGenerator.GetCalcValue(this.GetValue_Billow);
					}
				}
				else
				{
					this.m_noiseCalculator = new FractalGenerator.GetCalcValue(this.GetValue_Perlin);
				}
			}
		}

		public void SetDefaults()
		{
			FractalGenerator.Fractals fractalType = this.m_fractalType;
			if (fractalType != FractalGenerator.Fractals.Perlin)
			{
				if (fractalType != FractalGenerator.Fractals.Billow)
				{
					if (fractalType == FractalGenerator.Fractals.RidgeMulti)
					{
						this.m_frequency = 1f;
						this.m_lacunarity = 2f;
						this.m_octaves = 6;
						this.m_seed = 0f;
						this.CalcSpectralWeights();
						this.m_noiseCalculator = new FractalGenerator.GetCalcValue(this.GetValue_RidgedMulti);
					}
				}
				else
				{
					this.m_frequency = 1f;
					this.m_lacunarity = 2f;
					this.m_octaves = 6;
					this.m_persistence = 0.5f;
					this.m_seed = 0f;
					this.m_noiseCalculator = new FractalGenerator.GetCalcValue(this.GetValue_Billow);
				}
			}
			else
			{
				this.m_frequency = 1f;
				this.m_lacunarity = 2f;
				this.m_octaves = 6;
				this.m_persistence = 0.5f;
				this.m_seed = 0f;
				this.m_noiseCalculator = new FractalGenerator.GetCalcValue(this.GetValue_Perlin);
			}
		}

		public float GetValue(float x, float z)
		{
			return this.m_noiseCalculator(x, z);
		}

		public double GetValue(double x, double z)
		{
			return (double)this.GetValue((float)x, (float)z);
		}

		public float GetNormalisedValue(float x, float z)
		{
			return Mathf.Clamp01((this.GetValue(x, z) + 1f) / 2f);
		}

		public double GetNormalisedValue(double x, double z)
		{
			return (double)this.GetNormalisedValue((float)x, (float)z);
		}

		public float GetValue_Perlin(float x, float z)
		{
			float num = 0f;
			float num2 = 1f;
			x += this.m_seed;
			z += this.m_seed;
			x += this.m_XOffset;
			z += this.m_ZOffset;
			x *= this.m_frequency;
			z *= this.m_frequency;
			for (int i = 0; i < this.m_octaves; i++)
			{
				float x2 = x;
				float y = z;
				float num3 = SimplexNoiseGenerator.Generate(x2, y);
				num += num3 * num2;
				x *= this.m_lacunarity;
				z *= this.m_lacunarity;
				num2 *= this.m_persistence;
			}
			return num + this.m_YOffset * 2.4f;
		}

		public float GetValue_Billow(float x, float z)
		{
			float num = 0f;
			float num2 = 1f;
			x += this.m_seed;
			z += this.m_seed;
			x += this.m_XOffset;
			z += this.m_ZOffset;
			x *= this.m_frequency;
			z *= this.m_frequency;
			for (int i = 0; i < this.m_octaves; i++)
			{
				float x2 = x;
				float y = z;
				float num3 = SimplexNoiseGenerator.Generate(x2, y);
				num3 = 2f * Mathf.Abs(num3) - 1f;
				num += num3 * num2;
				x *= this.m_lacunarity;
				z *= this.m_lacunarity;
				num2 *= this.m_persistence;
			}
			return num + this.m_YOffset * 2.4f;
		}

		public float GetValue_RidgedMulti(float x, float z)
		{
			float num = 0f;
			float num2 = 1f;
			float num3 = 1f;
			float persistence = this.m_persistence;
			x += this.m_seed;
			z += this.m_seed;
			x += this.m_XOffset;
			z += this.m_ZOffset;
			x *= this.m_frequency;
			z *= this.m_frequency;
			for (int i = 0; i < this.m_octaves; i++)
			{
				float x2 = x;
				float y = z;
				float num4 = SimplexNoiseGenerator.Generate(x2, y);
				num4 = Mathf.Abs(num4);
				num4 = num3 - num4;
				num4 *= num4;
				num4 *= num2;
				num2 = num4 * persistence;
				if ((double)num2 > 1.0)
				{
					num2 = 1f;
				}
				if (num2 < 0f)
				{
					num2 = 0f;
				}
				num += num4 * this.m_spectralWeights[i];
				x *= this.m_lacunarity;
				z *= this.m_lacunarity;
			}
			num = num * 1.25f - 1f;
			return num + this.m_YOffset;
		}

		private void CalcSpectralWeights()
		{
			float num = 1f;
			float num2 = 1f;
			int length = this.m_spectralWeights.GetLength(0);
			for (int i = 0; i < length; i++)
			{
				this.m_spectralWeights[i] = Mathf.Pow(num2, -num);
				num2 *= this.m_lacunarity;
			}
		}

		private float m_seed;

		private int m_octaves = 8;

		private float m_persistence = 0.65f;

		private float m_frequency = 1f;

		private float m_lacunarity = 1.5f;

		private float m_XOffset;

		private float m_ZOffset;

		private float m_YOffset;

		private FractalGenerator.Fractals m_fractalType;

		private float[] m_spectralWeights = new float[20];

		private FractalGenerator.GetCalcValue m_noiseCalculator;

		public enum Fractals
		{
			Perlin,
			Billow,
			RidgeMulti
		}

		private delegate float GetCalcValue(float x, float z);
	}
}
