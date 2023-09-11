using System;
using UnityEngine;

[Serializable]
public class MudStamp
{
	public float boundsRadius
	{
		get
		{
			return Mathf.Sqrt(this.stampSize * this.stampSize + this.stampSize * this.stampSize);
		}
	}

	public string Serialize()
	{
		string text = string.Empty;
		text = text + this.stampTextureID + "|";
		text = text + this.stampRotation + "|";
		string text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			this.stampPosition.x,
			"/",
			this.stampPosition.y,
			"/",
			this.stampPosition.z,
			"|"
		});
		text = text + this.stampSize + "|";
		text = text + this.mudDepth + "|";
		return text + this.mudViscosity;
	}

	public void Deserialize(string data)
	{
		string[] array = data.Split(new char[]
		{
			'|'
		});
		this.stampTextureID = int.Parse(array[0]);
		this.stampRotation = float.Parse(array[1]);
		string[] array2 = array[2].Split(new char[]
		{
			'/'
		});
		this.stampPosition = new Vector3(float.Parse(array2[0]), float.Parse(array2[1]), float.Parse(array2[2]));
		this.stampSize = float.Parse(array[3]);
		this.mudDepth = float.Parse(array[4]);
		this.mudViscosity = float.Parse(array[5]);
	}

	public int stampTextureID;

	public float stampRotation;

	public Vector3 stampPosition;

	public float stampSize;

	public GameObject stampIndicator;

	public float mudDepth;

	public float mudViscosity;
}
