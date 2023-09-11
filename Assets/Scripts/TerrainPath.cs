using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TerrainPath
{
	public string Serialize()
	{
		string text = string.Empty;
		text = text + (int)this.pathAction + "|";
		text = text + Mathf.Round(this.pathWidth * 100f) / 100f + "|";
		text = text + Mathf.Round(this.pathStrength * 100f) / 100f + "|";
		text = text + this.pathPattern + "|";
		text = text + this.extraInt + "|";
		for (int i = 0; i < this.pathPositions.Count; i++)
		{
			string str = (int)this.pathPositions[i].x + "/" + (int)this.pathPositions[i].z;
			text = text + str + "|";
		}
		return text;
	}

	public void Deserialize(string data)
	{
		string[] array = data.Split(new char[]
		{
			'|'
		});
		this.pathAction = (ModAction)int.Parse(array[0]);
		this.pathWidth = float.Parse(array[1]);
		this.pathStrength = float.Parse(array[2]);
		this.pathPattern = int.Parse(array[3]);
		this.extraInt = int.Parse(array[4]);
		this.pathPositions = new List<Vector3>();
		for (int i = 5; i < array.Length; i++)
		{
			string text = array[i];
			string[] array2 = text.Split(new char[]
			{
				'/'
			});
			if (array2.Length == 2)
			{
				Vector3 item = new Vector3((float)int.Parse(array2[0]), 0f, (float)int.Parse(array2[1]));
				this.pathPositions.Add(item);
			}
		}
	}

	public ModAction pathAction;

	public List<Vector3> pathPositions;

	public float pathWidth;

	public float pathStrength;

	public int pathPattern;

	public int extraInt;
}
