using System;
using UnityEngine;

public class TagussanBSpline
{
	public TagussanBSpline(Vector3[] points, int degree)
	{
		if (this.copy)
		{
			this.points = new float[points.Length][];
			for (int i = 0; i < points.Length; i++)
			{
				this.points[i] = new float[]
				{
					points[i].x,
					points[i].y,
					points[i].z
				};
			}
		}
		this.degree = degree;
		this.dimension = 3;
		if (degree == 2)
		{
			this.baseFunc = new TagussanBSpline.bf(this.basisDeg2);
			this.baseFuncRangeInt = 2;
		}
		else if (degree == 3)
		{
			this.baseFunc = new TagussanBSpline.bf(this.basisDeg3);
			this.baseFuncRangeInt = 2;
		}
		else if (degree == 4)
		{
			this.baseFunc = new TagussanBSpline.bf(this.basisDeg4);
			this.baseFuncRangeInt = 3;
		}
		else if (degree == 5)
		{
			this.baseFunc = new TagussanBSpline.bf(this.basisDeg5);
			this.baseFuncRangeInt = 3;
		}
	}

	private TagussanBSpline.Seq seqAt(int dim)
	{
		float[][] points = this.points;
		int margin = this.degree + 1;
		return delegate(int n)
		{
			if (n < margin)
			{
				return points[0][dim];
			}
			if (points.Length + margin <= n)
			{
				return points[points.Length - 1][dim];
			}
			return points[n - margin][dim];
		};
	}

	private float basisDeg2(float x)
	{
		if (-0.5 <= (double)x && (double)x < 0.5)
		{
			return 0.75f - x * x;
		}
		if (0.5 <= (double)x && (double)x <= 1.5)
		{
			return 1.125f + (-1.5f + x / 2f) * x;
		}
		if (-1.5 <= (double)x && (double)x < -0.5)
		{
			return 1.125f + (1.5f + x / 2f) * x;
		}
		return 0f;
	}

	private float basisDeg3(float x)
	{
		if (-1f <= x && x < 0f)
		{
			return 0.6666667f + (-1f - x / 2f) * x * x;
		}
		if (1f <= x && x <= 2f)
		{
			return 1.33333337f + x * (-2f + (1f - x / 6f) * x);
		}
		if (-2f <= x && x < -1f)
		{
			return 1.33333337f + x * (2f + (1f + x / 6f) * x);
		}
		if (0f <= x && x < 1f)
		{
			return 0.6666667f + (-1f + x / 2f) * x * x;
		}
		return 0f;
	}

	private float basisDeg4(float x)
	{
		if (-1.5 <= (double)x && (double)x < -0.5)
		{
			return 0.5729167f + x * (-0.208333328f + x * (-1.25f + (-0.8333333f - x / 6f) * x));
		}
		if (0.5 <= (double)x && (double)x < 1.5)
		{
			return 0.5729167f + x * (0.208333328f + x * (-1.25f + (0.8333333f - x / 6f) * x));
		}
		if (1.5 <= (double)x && (double)x <= 2.5)
		{
			return 1.62760413f + x * (-2.60416675f + x * (1.5625f + (-0.416666657f + x / 24f) * x));
		}
		if (-2.5 <= (double)x && (double)x <= -1.5)
		{
			return 1.62760413f + x * (2.60416675f + x * (1.5625f + (0.416666657f + x / 24f) * x));
		}
		if (-1.5 <= (double)x && (double)x < 1.5)
		{
			return 0.5989583f + x * x * (-0.625f + x * x / 4f);
		}
		return 0f;
	}

	private float basisDeg5(float x)
	{
		if (-2f <= x && x < -1f)
		{
			return 0.425f + x * (-0.625f + x * (-1.75f + x * (-1.25f + (-0.375f - x / 24f) * x)));
		}
		if (0f <= x && x < 1f)
		{
			return 0.55f + x * x * (-0.5f + (0.25f - x / 12f) * x * x);
		}
		if (2f <= x && x <= 3f)
		{
			return 2.025f + x * (-3.375f + x * (2.25f + x * (-0.75f + (0.125f - x / 120f) * x)));
		}
		if (-3f <= x && x < -2f)
		{
			return 2.025f + x * (3.375f + x * (2.25f + x * (0.75f + (0.125f + x / 120f) * x)));
		}
		if (1f <= x && x < 2f)
		{
			return 0.425f + x * (0.625f + x * (-1.75f + x * (1.25f + (-0.375f + x / 24f) * x)));
		}
		if (-1f <= x && x < 0f)
		{
			return 0.55f + x * x * (-0.5f + (0.25f + x / 12f) * x * x);
		}
		return 0f;
	}

	private float getInterpol(TagussanBSpline.Seq seq, float t)
	{
		TagussanBSpline.bf bf = this.baseFunc;
		int num = this.baseFuncRangeInt;
		int num2 = (int)Mathf.Floor(t);
		float num3 = 0f;
		for (int i = num2 - num; i <= num2 + num; i++)
		{
			num3 += seq(i) * bf(t - (float)i);
		}
		return num3;
	}

	public float[] calcAt(float t)
	{
		t *= (float)((this.degree + 1) * 2 + this.points.Length);
		if (this.dimension == 2)
		{
			return new float[]
			{
				this.getInterpol(this.seqAt(0), t),
				this.getInterpol(this.seqAt(1), t)
			};
		}
		if (this.dimension == 3)
		{
			return new float[]
			{
				this.getInterpol(this.seqAt(0), t),
				this.getInterpol(this.seqAt(1), t),
				this.getInterpol(this.seqAt(2), t)
			};
		}
		float[] array = new float[this.dimension];
		for (int i = 0; i < this.dimension; i++)
		{
			array[i] = this.getInterpol(this.seqAt(i), t);
		}
		return array;
	}

	private float[][] points;

	private int degree;

	private bool copy = true;

	private int dimension;

	private TagussanBSpline.bf baseFunc;

	private int baseFuncRangeInt;

	private delegate float bf(float i);

	private delegate float Seq(int obj);
}
