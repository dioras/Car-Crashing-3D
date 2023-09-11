using System;
using UnityEngine;

public class rotate : MonoBehaviour
{
	private void Start()
	{
	}

	private void LateUpdate()
	{
		base.transform.Rotate(this.axis * Time.deltaTime);
	}

	public Vector3 axis;
}
