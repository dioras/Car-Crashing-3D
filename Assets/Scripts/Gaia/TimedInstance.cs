using System;
using System.Diagnostics;

namespace Gaia
{
	public class TimedInstance : Stopwatch
	{
		public TimedInstance(string name, bool start = true)
		{
			this.m_name = name;
			if (start)
			{
				this.Start();
			}
		}

		public new void Start()
		{
			this.m_iterations++;
			base.Start();
		}

		public new void Reset()
		{
			this.m_iterations = 0;
			base.Reset();
		}

		public void IncIterations()
		{
			this.m_iterations++;
		}

		public float GetAvgMs()
		{
			return (float)base.ElapsedMilliseconds / (float)this.m_iterations;
		}

		public float GetAvgS()
		{
			return (float)base.ElapsedMilliseconds / (float)this.m_iterations / 1000f;
		}

		public override string ToString()
		{
			return string.Format("{0}: Avg: {1:0.000}s Last: {1:0.000}s", this.m_name, this.GetAvgMs() / 1000f, (float)base.ElapsedMilliseconds / 1000f);
		}

		public long nanosecPerTick = 1000000000L / Stopwatch.Frequency;

		public string m_name;

		public int m_iterations;
	}
}
