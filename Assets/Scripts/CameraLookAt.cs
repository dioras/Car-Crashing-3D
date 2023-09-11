using System;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (this.target != null)
		{
			base.transform.LookAt(this.target);
		}
	}

	public Transform target;
}
