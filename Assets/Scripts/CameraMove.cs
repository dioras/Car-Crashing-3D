using System;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
	private void Update()
	{
		base.transform.LookAt(this.target.transform);
	}

	public GameObject target;

	public int speed;
}
