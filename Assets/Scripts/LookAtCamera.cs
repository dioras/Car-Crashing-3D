using System;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
	private void Start()
	{
		this.cam = Camera.main;
	}

	private void Update()
	{
		if (this.cam == null)
		{
			return;
		}
		base.transform.rotation = Quaternion.LookRotation(base.transform.position - this.cam.transform.position);
	}

	private Camera cam;
}
