using System;
using System.IO;
using UnityEngine;

public class ScreenshotWithAlpha : MonoBehaviour
{
	private static Texture2D Screenshot()
	{
		int pixelWidth = Camera.main.pixelWidth;
		int pixelHeight = Camera.main.pixelHeight;
		Camera main = Camera.main;
		CameraClearFlags clearFlags = main.clearFlags;
		main.clearFlags = CameraClearFlags.Depth;
		RenderTexture renderTexture = new RenderTexture(pixelWidth, pixelHeight, 32);
		main.targetTexture = renderTexture;
		Texture2D texture2D = new Texture2D(pixelWidth, pixelHeight, TextureFormat.ARGB32, false);
		main.Render();
		RenderTexture.active = renderTexture;
		texture2D.ReadPixels(new Rect(0f, 0f, (float)pixelWidth, (float)pixelHeight), 0, 0);
		texture2D.Apply();
		main.targetTexture = null;
		RenderTexture.active = null;
		UnityEngine.Object.Destroy(renderTexture);
		main.clearFlags = clearFlags;
		return texture2D;
	}

	[ContextMenu("Take screenshot")]
	public void SaveScreenshotToFile()
	{
		string path = this.screenshotName + ".png";
		Texture2D tex = ScreenshotWithAlpha.Screenshot();
		byte[] bytes = tex.EncodeToPNG();
		File.WriteAllBytes(path, bytes);
	}

	public string screenshotName;
}
