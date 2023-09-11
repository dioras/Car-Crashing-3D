using System;
using UnityEngine;

namespace GeNa
{
	public class XorshiftPlus
	{
		public XorshiftPlus(int seed = 1)
		{
			this.m_seed = seed;
			if (this.m_seed == 0)
			{
				this.m_seed = 1;
			}
			this.Reset();
		}

		public void Reset()
		{
			this.m_stateA = 181353UL * (ulong)this.m_seed;
			this.m_stateB = 7UL * (ulong)this.m_seed;
		}

		public void Reset(int seed)
		{
			this.m_seed = seed;
			if (this.m_seed == 0)
			{
				this.m_seed = 1;
			}
			this.Reset();
		}

		public void Reset(ulong stateA, ulong stateB)
		{
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"Resetting RNG State ",
				stateA,
				" ",
				stateB
			}));
			this.m_stateA = stateA;
			this.m_stateB = stateB;
		}

		public void GetState(out int seed, out ulong stateA, out ulong stateB)
		{
			seed = this.m_seed;
			stateA = this.m_stateA;
			stateB = this.m_stateB;
		}

		public float Next()
		{
			ulong num = this.m_stateA;
			ulong stateB = this.m_stateB;
			this.m_stateA = stateB;
			num ^= num << 23;
			num ^= num >> 17;
			num ^= (stateB ^ stateB >> 26);
			this.m_stateB = num;
			return (num + stateB) / 1.84467441E+19f;
		}

		public int NextInt()
		{
			return (int)(this.Next() * 2.14748365E+09f);
		}

		public float Next(float min, float max)
		{
			return min + this.Next() * (max - min);
		}

		public int Next(int min, int max)
		{
			if (min == max)
			{
				return min;
			}
			return (int)this.Next((float)min, (float)max + 0.999f);
		}

		public Vector3 NextVector()
		{
			return new Vector3(this.Next(), this.Next(), this.Next());
		}

		public Vector3 NextVector(float min, float max)
		{
			return new Vector3(this.Next(min, max), this.Next(min, max), this.Next(min, max));
		}

		private const ulong m_A_Init = 181353UL;

		private const ulong m_B_Init = 7UL;

		public int m_seed;

		public ulong m_stateA;

		public ulong m_stateB;
	}
}
