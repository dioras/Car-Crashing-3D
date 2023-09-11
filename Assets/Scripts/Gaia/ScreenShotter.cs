using System;
using System.IO;
using UnityEngine;

namespace Gaia
{
	public class ScreenShotter : MonoBehaviour
	{
		private void OnEnable()
		{
			if (this.m_mainCamera == null)
			{
				this.m_mainCamera = Camera.main;
			}
			string path = Path.Combine(Application.dataPath, this.m_targetDirectory);
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
		}

		private void OnDisable()
		{
			if (this.m_refreshAssetDB)
			{
				this.m_refreshAssetDB = false;
			}
		}

		private string ScreenShotName(int width, int height)
		{
			string text = Path.Combine(Application.dataPath, this.m_targetDirectory);
			text = text.Replace('\\', '/');
			if (text[text.Length - 1] == '/')
			{
				text = text.Substring(0, text.Length - 1);
			}
			if (this.m_imageFormat == GaiaConstants.StorageFormat.JPG)
			{
				return string.Format("{0}/Grab {1} w{2}h{3} x{4}y{5}z{6}r{7}.jpg", new object[]
				{
					text,
					DateTime.Now.ToString("yyyyMMddHHmmss"),
					width,
					height,
					(int)this.m_mainCamera.transform.position.x,
					(int)this.m_mainCamera.transform.position.y,
					(int)this.m_mainCamera.transform.position.z,
					(int)this.m_mainCamera.transform.rotation.eulerAngles.y
				});
			}
			return string.Format("{0}/Grab {1} w{2}h{3} x{4}y{5}z{6}r{7}.png", new object[]
			{
				text,
				DateTime.Now.ToString("yyyyMMdd HHmmss"),
				width,
				height,
				(int)this.m_mainCamera.transform.position.x,
				(int)this.m_mainCamera.transform.position.y,
				(int)this.m_mainCamera.transform.position.z,
				(int)this.m_mainCamera.transform.rotation.eulerAngles.y
			});
		}

		public void TakeHiResShot()
		{
			this.m_takeShot = true;
		}

		private void LateUpdate()
		{
			if (UnityEngine.Input.GetKeyDown(this.m_screenShotKey) || this.m_takeShot)
			{
				if (this.m_useScreenSize)
				{
					this.m_targetWidth = Screen.width;
					this.m_targetHeight = Screen.height;
				}
				this.m_refreshAssetDB = true;
				RenderTexture renderTexture = new RenderTexture(this.m_targetWidth, this.m_targetHeight, 24);
				this.m_mainCamera.targetTexture = renderTexture;
				Texture2D texture2D = new Texture2D(this.m_targetWidth, this.m_targetHeight, TextureFormat.RGB24, false);
				this.m_mainCamera.Render();
				RenderTexture.active = renderTexture;
				texture2D.ReadPixels(new Rect(0f, 0f, (float)this.m_targetWidth, (float)this.m_targetHeight), 0, 0);
				this.m_mainCamera.targetTexture = null;
				RenderTexture.active = null;
				UnityEngine.Object.Destroy(renderTexture);
				if (this.m_watermark != null)
				{
					Utils.MakeTextureReadable(this.m_watermark);
					texture2D = this.AddWatermark(texture2D, this.m_watermark);
				}
				byte[] bytes = texture2D.EncodeToJPG();
				string text = this.ScreenShotName(this.m_targetWidth, this.m_targetHeight);
				Utils.WriteAllBytes(text, bytes);
				this.m_takeShot = false;
				UnityEngine.Debug.Log(string.Format("Took screenshot to: {0}", text));
			}
		}

		public Texture2D AddWatermark(Texture2D background, Texture2D watermark)
		{
			int num = background.width - watermark.width - 10;
			int num2 = num + watermark.width;
			int num3 = 8;
			int num4 = num3 + watermark.height;
			for (int i = num; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					Color pixel = background.GetPixel(i, j);
					Color pixel2 = watermark.GetPixel(i - num, j - num3);
					Color color = Color.Lerp(pixel, pixel2, pixel2.a / 1f);
					background.SetPixel(i, j, color);
				}
			}
			background.Apply();
			return background;
		}

		public KeyCode m_screenShotKey = KeyCode.F12;

		public GaiaConstants.StorageFormat m_imageFormat = GaiaConstants.StorageFormat.JPG;

		public string m_targetDirectory = "Screenshots";

		public int m_targetWidth = 1900;

		public int m_targetHeight = 1200;

		public bool m_useScreenSize = true;

		public Camera m_mainCamera;

		private bool m_takeShot;

		private bool m_refreshAssetDB;

		public Texture2D m_watermark;
	}
}
