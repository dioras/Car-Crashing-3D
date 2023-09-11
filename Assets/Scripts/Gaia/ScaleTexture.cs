using System;
using System.Threading;
using UnityEngine;

namespace Gaia
{
	public class ScaleTexture
	{
		public static void Point(Texture2D tex, int newWidth, int newHeight)
		{
			ScaleTexture.ThreadedScale(tex, newWidth, newHeight, false);
		}

		public static void Bilinear(Texture2D tex, int newWidth, int newHeight)
		{
			ScaleTexture.ThreadedScale(tex, newWidth, newHeight, true);
		}

		private static void ThreadedScale(Texture2D tex, int newWidth, int newHeight, bool useBilinear)
		{
			ScaleTexture.texColors = tex.GetPixels();
			ScaleTexture.newColors = new Color[newWidth * newHeight];
			if (useBilinear)
			{
				ScaleTexture.ratioX = 1f / ((float)newWidth / (float)(tex.width - 1));
				ScaleTexture.ratioY = 1f / ((float)newHeight / (float)(tex.height - 1));
			}
			else
			{
				ScaleTexture.ratioX = (float)tex.width / (float)newWidth;
				ScaleTexture.ratioY = (float)tex.height / (float)newHeight;
			}
			ScaleTexture.w = tex.width;
			ScaleTexture.w2 = newWidth;
			int num = Mathf.Min(SystemInfo.processorCount, newHeight);
			int num2 = newHeight / num;
			ScaleTexture.finishCount = 0;
			if (ScaleTexture.mutex == null)
			{
				ScaleTexture.mutex = new Mutex(false);
			}
			if (num > 1)
			{
				int i;
				ScaleTexture.ThreadData threadData;
				for (i = 0; i < num - 1; i++)
				{
					threadData = new ScaleTexture.ThreadData(num2 * i, num2 * (i + 1));
					ParameterizedThreadStart start = (!useBilinear) ? new ParameterizedThreadStart(ScaleTexture.PointScale) : new ParameterizedThreadStart(ScaleTexture.BilinearScale);
					Thread thread = new Thread(start);
					thread.Start(threadData);
				}
				threadData = new ScaleTexture.ThreadData(num2 * i, newHeight);
				if (useBilinear)
				{
					ScaleTexture.BilinearScale(threadData);
				}
				else
				{
					ScaleTexture.PointScale(threadData);
				}
				while (ScaleTexture.finishCount < num)
				{
					Thread.Sleep(1);
				}
			}
			else
			{
				ScaleTexture.ThreadData obj = new ScaleTexture.ThreadData(0, newHeight);
				if (useBilinear)
				{
					ScaleTexture.BilinearScale(obj);
				}
				else
				{
					ScaleTexture.PointScale(obj);
				}
			}
			tex.Reinitialize(newWidth, newHeight);
			tex.SetPixels(ScaleTexture.newColors);
			tex.Apply();
		}

		public static void BilinearScale(object obj)
		{
			ScaleTexture.ThreadData threadData = (ScaleTexture.ThreadData)obj;
			for (int i = threadData.start; i < threadData.end; i++)
			{
				int num = (int)Mathf.Floor((float)i * ScaleTexture.ratioY);
				int num2 = num * ScaleTexture.w;
				int num3 = (num + 1) * ScaleTexture.w;
				int num4 = i * ScaleTexture.w2;
				for (int j = 0; j < ScaleTexture.w2; j++)
				{
					int num5 = (int)Mathf.Floor((float)j * ScaleTexture.ratioX);
					float value = (float)j * ScaleTexture.ratioX - (float)num5;
					ScaleTexture.newColors[num4 + j] = ScaleTexture.ColorLerpUnclamped(ScaleTexture.ColorLerpUnclamped(ScaleTexture.texColors[num2 + num5], ScaleTexture.texColors[num2 + num5 + 1], value), ScaleTexture.ColorLerpUnclamped(ScaleTexture.texColors[num3 + num5], ScaleTexture.texColors[num3 + num5 + 1], value), (float)i * ScaleTexture.ratioY - (float)num);
				}
			}
			ScaleTexture.mutex.WaitOne();
			ScaleTexture.finishCount++;
			ScaleTexture.mutex.ReleaseMutex();
		}

		public static void PointScale(object obj)
		{
			ScaleTexture.ThreadData threadData = (ScaleTexture.ThreadData)obj;
			for (int i = threadData.start; i < threadData.end; i++)
			{
				int num = (int)(ScaleTexture.ratioY * (float)i) * ScaleTexture.w;
				int num2 = i * ScaleTexture.w2;
				for (int j = 0; j < ScaleTexture.w2; j++)
				{
					ScaleTexture.newColors[num2 + j] = ScaleTexture.texColors[(int)((float)num + ScaleTexture.ratioX * (float)j)];
				}
			}
			ScaleTexture.mutex.WaitOne();
			ScaleTexture.finishCount++;
			ScaleTexture.mutex.ReleaseMutex();
		}

		private static Color ColorLerpUnclamped(Color c1, Color c2, float value)
		{
			return new Color(c1.r + (c2.r - c1.r) * value, c1.g + (c2.g - c1.g) * value, c1.b + (c2.b - c1.b) * value, c1.a + (c2.a - c1.a) * value);
		}

		private static Color[] texColors;

		private static Color[] newColors;

		private static int w;

		private static float ratioX;

		private static float ratioY;

		private static int w2;

		private static int finishCount;

		private static Mutex mutex;

		public class ThreadData
		{
			public ThreadData(int s, int e)
			{
				this.start = s;
				this.end = e;
			}

			public int start;

			public int end;
		}
	}
}
