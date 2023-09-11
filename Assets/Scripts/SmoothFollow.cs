using System;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
	[AddComponentMenu("Camera-Control/Smooth Follow")]
	private void LateUpdate()
	{
		if (!this.target)
		{
			return;
		}
		float y = this.target.eulerAngles.y;
		float b = this.target.position.y + this.height;
		float num = base.transform.eulerAngles.y;
		float num2 = base.transform.position.y;
		num = Mathf.LerpAngle(num, y, this.rotationDamping * Time.deltaTime);
		num2 = Mathf.Lerp(num2, b, this.heightDamping * Time.deltaTime);
		Quaternion rotation = Quaternion.Euler(0f, num, 0f);
		base.transform.position = this.target.position;
		base.transform.position -= rotation * Vector3.forward * this.distance;
		base.transform.position = new Vector3(base.transform.position.x, num2, base.transform.position.z);
		base.transform.LookAt(this.target);
	}

	public Transform target;

	public float distance = 10f;

	public float height = 5f;

	public float heightDamping = 2f;

	public float rotationDamping = 3f;
}
