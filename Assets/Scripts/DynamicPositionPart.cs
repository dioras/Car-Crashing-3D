using System;
using UnityEngine;

[Serializable]
public class DynamicPositionPart
{
	public void UpdatePosition()
	{
		foreach (Transform transform in this.Positions)
		{
			if (transform.gameObject.activeInHierarchy)
			{
				this.part.position = transform.position;
			}
		}
	}

	public Transform part;

	public Transform[] Positions;
}
