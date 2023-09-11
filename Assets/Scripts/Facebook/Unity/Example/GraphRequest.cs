using System;
using System.Collections;
using UnityEngine;

namespace Facebook.Unity.Example
{
	internal class GraphRequest : MenuBase
	{
		protected override void GetGui()
		{
			bool enabled = GUI.enabled;
			GUI.enabled = (enabled && FB.IsLoggedIn);
			if (base.Button("Basic Request - Me"))
			{
			//	FB.API("/me", HttpMethod.GET, new FacebookDelegate<IGraphResult>(base.HandleResult), null);
			}
			if (base.Button("Retrieve Profile Photo"))
			{
			//	FB.API("/me/picture", HttpMethod.GET, new FacebookDelegate<IGraphResult>(this.ProfilePhotoCallback), null);
			}
			if (base.Button("Take and Upload screenshot"))
			{
				base.StartCoroutine(this.TakeScreenshot());
			}
			base.LabelAndTextField("Request", ref this.apiQuery);
			if (base.Button("Custom Request"))
			{
			//	FB.API(this.apiQuery, HttpMethod.GET, new FacebookDelegate<IGraphResult>(base.HandleResult), null);
			}
			if (this.profilePic != null)
			{
				GUILayout.Box(this.profilePic, new GUILayoutOption[0]);
			}
			GUI.enabled = enabled;
		}

		private void ProfilePhotoCallback(IGraphResult result)
		{
			if (string.IsNullOrEmpty(result.Error) && result.Texture != null)
			{
				this.profilePic = result.Texture;
			}
			base.HandleResult(result);
		}

		private IEnumerator TakeScreenshot()
		{
			yield return new WaitForEndOfFrame();
			int width = Screen.width;
			int height = Screen.height;
			Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
			tex.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), 0, 0);
			tex.Apply();
			byte[] screenshot = tex.EncodeToPNG();
			WWWForm wwwForm = new WWWForm();
			wwwForm.AddBinaryData("image", screenshot, "InteractiveConsole.png");
			wwwForm.AddField("message", "herp derp.  I did a thing!  Did I do this right?");
			FB.API("me/photos", HttpMethod.POST, new FacebookDelegate<IGraphResult>(base.HandleResult), wwwForm);
			yield break;
		}

		private string apiQuery = string.Empty;

		private Texture2D profilePic;
	}
}
