using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampsLoadTest : MonoBehaviour
{
	[ContextMenu("Load stamps")]
	private void LoadStamps()
	{
		base.StartCoroutine(this.LoadStampsCor());
	}

	private IEnumerator LoadStampsCor()
	{
		List<string> stampFileNames = new List<string>();
		WWW listQuery = new WWW("https://keereedev.000webhostapp.com/LoadStamps.php");
		yield return listQuery;
		string[] rawNames = listQuery.text.Split(new char[]
		{
			'\n'
		});
		foreach (string text in rawNames)
		{
			if (text != string.Empty)
			{
				stampFileNames.Add(text);
			}
		}
		foreach (string fn in stampFileNames)
		{
			WWW stampQuery = new WWW("https://keereedev.000webhostapp.com/Stamps/" + fn);
			UnityEngine.Debug.Log(stampQuery.url);
			yield return stampQuery;
			this.loadedStamps.Add(stampQuery.texture);
		}
		yield break;
	}

	public List<Texture2D> loadedStamps = new List<Texture2D>();
}
