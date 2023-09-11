using System;
using UnityEngine;

[Serializable]
public class ExtraObjectReference
{
	public ExtraObjectReference DeepCopy()
	{
		return new ExtraObjectReference
		{
			arrayID = this.arrayID,
			density = this.density,
			onlyByEdges = this.onlyByEdges
		};
	}

	public int arrayID;

	[Range(0f, 1f)]
	public float density;

	public bool onlyByEdges;
}
