using System;
using UnityEngine;

[Serializable]
public class SnapPoint
{
	public void ResetAffectors()
	{
		if (this.leftAffector != null && this.leftAffectorDefPos != Vector3.zero)
		{
			this.leftAffector.localPosition = this.leftAffectorDefPos;
		}
		if (this.rightAffector != null && this.rightAffectorDefPos != Vector3.zero)
		{
			this.rightAffector.localPosition = this.rightAffectorDefPos;
		}
	}

	public Transform transform;

	public Transform leftAffector;

	public Transform rightAffector;

	[HideInInspector]
	public Vector3 leftAffectorDefPos;

	[HideInInspector]
	public Vector3 rightAffectorDefPos;

	[HideInInspector]
	public bool busy;
}
