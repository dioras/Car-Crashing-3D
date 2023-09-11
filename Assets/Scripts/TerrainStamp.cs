using System;
using UnityEngine;

[Serializable]
public class TerrainStamp
{
	public string Serialize()
	{
		string arg = string.Empty;
		arg = arg + (int)this.stampAction + "|";
		arg = arg + this.stampTextureID + "|";
		arg = arg + (int)this.stampPosition.x + "|";
		arg = arg + (int)this.stampPosition.y + "|";
		arg = arg + (int)this.stampRotation + "|";
		arg = arg + (int)this.stampSize + "|";
		arg = arg + Mathf.Round(this.stampStrength * 100f) / 100f + "|";
		return arg + this.extraInt;
	}

	public void Deserialize(string s)
	{
		string[] array = s.Split(new char[]
		{
			'|'
		});
		this.stampAction = (ModAction)int.Parse(array[0]);
		this.stampTextureID = int.Parse(array[1]);
		this.stampPosition.x = float.Parse(array[2]);
		this.stampPosition.y = float.Parse(array[3]);
		this.stampRotation = float.Parse(array[4]);
		this.stampSize = float.Parse(array[5]);
		this.stampStrength = float.Parse(array[6]);
		this.extraInt = int.Parse(array[7]);
	}

	public ModAction stampAction;

	public int stampTextureID;

	public Vector2 stampPosition;

	public float stampRotation;

	[Range(0.1f, 20f)]
	public float stampSize;

	[Range(0f, 1f)]
	public float stampStrength;

	public int extraInt;
}
