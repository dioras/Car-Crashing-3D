using System;
using System.Collections;
using System.Text;
using UnityEngine;

public class LevelWebHandler : MonoBehaviour
{
	public void RemoveLevel(string levelID, Action successCallback = null, Action errorCallback = null)
	{
		base.StartCoroutine(this.RemoveLevelCor(levelID, successCallback, errorCallback));
	}

	private IEnumerator RemoveLevelCor(string levelID, Action successCallback = null, Action errorCallback = null)
	{
		bool success = false;
		for (int i = 0; i < 10; i++)
		{
			WWW w = new WWW("http://offroadoutlaws.online/MapRemover.php?ID=" + levelID);
			yield return w;
			if (w.error == null && w.text.Contains("success"))
			{
				success = true;
				break;
			}
		}
		if (success && successCallback != null)
		{
			successCallback();
		}
		if (!success && errorCallback != null)
		{
			errorCallback();
		}
		yield break;
	}

	public void UploadLevel(string data, string fileName, Action successCallback = null, Action errorCallback = null)
	{
		base.StartCoroutine(this.UploadLevelCor(data, fileName, successCallback, errorCallback));
	}

	private IEnumerator UploadLevelCor(string data, string fileName, Action successCallback, Action errorCallback)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(data);
		if (fileName == string.Empty)
		{
			UnityEngine.Debug.Log("file name is null");
		}
		WWWForm form = new WWWForm();
		form.AddField("file", "file");
		form.AddBinaryData("file", bytes, fileName, "text/xml");
		bool success = false;
		for (int i = 0; i < 10; i++)
		{
			WWW query = new WWW("http://offroadoutlaws.online/UploadFile.php", form);
			yield return query;
			if (query.error == null && query.text.Contains("success"))
			{
				success = true;
				break;
			}
		}
		if (success && successCallback != null)
		{
			successCallback();
		}
		if (!success && errorCallback != null)
		{
			errorCallback();
		}
		yield break;
	}

	public void DownloadLevel(string fileName, Action<string> successCallback, Action failCallback = null)
	{
		base.StartCoroutine(this.DownloadLeveLCor(fileName, successCallback, failCallback));
	}

	private IEnumerator DownloadLeveLCor(string fileName, Action<string> successCallback, Action failCallback)
	{
		bool success = false;
		string levelData = string.Empty;
		for (int i = 0; i < 10; i++)
		{
			WWW w = new WWW("http://offroadoutlaws.online/GetMaps.php?ID=" + fileName);
			yield return w;
			if (w.error == null)
			{
				success = true;
				levelData = w.text;
				break;
			}
		}
		if (success && successCallback != null)
		{
			successCallback(levelData);
		}
		if (!success && failCallback != null)
		{
			failCallback();
		}
		yield break;
	}

	public void DownloadLevelsMetadatasPage(int from, int count, SortType sortType, string searchString, string favMapsList, bool includeHidden, Action<string> successCallback, Action<string> failCallback = null)
	{
		base.StartCoroutine(this.DownloadLevelsMetadatasCor(from, count, sortType, searchString, favMapsList, includeHidden, successCallback, failCallback));
	}

	private IEnumerator DownloadLevelsMetadatasCor(int from, int count, SortType sortType, string searchString, string favMapsList, bool includeHidden, Action<string> successCallback, Action<string> failCallback)
	{
		string err = string.Empty;
		for (int i = 0; i < 10; i++)
		{
			WWW w = new WWW(string.Concat(new object[]
			{
				"http://offroadoutlaws.online/GetMaps.php?meta&from=",
				from,
				"&count=",
				count,
				"&sortType=",
				sortType.ToString(),
				"&search=",
				searchString,
				"&favsList=",
				favMapsList,
				"&includeHidden=",
				(!includeHidden) ? "0" : "1"
			}));
			UnityEngine.Debug.Log(w.url);
			yield return w;
			if (w.error == null)
			{
				if (successCallback != null)
				{
					successCallback(w.text);
				}
				yield break;
			}
			err = w.error;
		}
		if (err == string.Empty)
		{
			err = "Can't connect to server";
		}
		if (failCallback != null)
		{
			failCallback(err);
		}
		yield break;
	}

	public void DownloadMapImage(string mapID, Action<Texture2D, string> successCallback)
	{
		base.StartCoroutine(this.DownloadMapImageCor(mapID, successCallback));
	}

	private IEnumerator DownloadMapImageCor(string mapID, Action<Texture2D, string> successCallback)
	{
		for (int i = 0; i < 10; i++)
		{
			WWW w = new WWW("http://offroadoutlaws.online/LevelPics/" + mapID + ".jpg");
			yield return w;
			if (w.error == null)
			{
				if (successCallback != null)
				{
					successCallback(w.texture, mapID);
				}
				yield break;
			}
		}
		yield break;
	}

	private const string uploadWebAdress = "http://offroadoutlaws.online/UploadFile.php";

	private const string getMapWebAdress = "http://offroadoutlaws.online/GetMaps.php?ID=";

	private const string removalWebAdress = "http://offroadoutlaws.online/MapRemover.php?ID=";
}
