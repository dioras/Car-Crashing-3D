using System;
using UnityEngine;

public class FlyCamera : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			this.isRotating = true;
		}
		if (Input.GetMouseButtonUp(1))
		{
			this.isRotating = false;
		}
		if (this.isRotating)
		{
			float y = base.transform.localEulerAngles.y + UnityEngine.Input.GetAxis("Mouse X") * this.mouseSensitivity;
			this.rotationY += UnityEngine.Input.GetAxis("Mouse Y") * this.mouseSensitivity;
			this.rotationY = Mathf.Clamp(this.rotationY, -90f, 90f);
			base.transform.localEulerAngles = new Vector3(-this.rotationY, y, 0f);
		}
		Vector3 vector = this.GetBaseInput();
		if (UnityEngine.Input.GetKey(KeyCode.LeftShift))
		{
			this.totalRun += Time.deltaTime;
			vector = vector * this.totalRun * this.shiftAdd;
			vector.x = Mathf.Clamp(vector.x, -this.maxShift, this.maxShift);
			vector.y = Mathf.Clamp(vector.y, -this.maxShift, this.maxShift);
			vector.z = Mathf.Clamp(vector.z, -this.maxShift, this.maxShift);
			this.speedMultiplier = this.totalRun * this.shiftAdd * Time.deltaTime;
			this.speedMultiplier = Mathf.Clamp(this.speedMultiplier, -this.maxShift, this.maxShift);
		}
		else
		{
			this.totalRun = Mathf.Clamp(this.totalRun * 0.5f, 1f, 1000f);
			vector *= this.mainSpeed;
			this.speedMultiplier = this.mainSpeed * Time.deltaTime;
		}
		vector *= Time.deltaTime;
		Vector3 position = base.transform.position;
		base.transform.Translate(vector);
		position.x = base.transform.position.x;
		position.z = base.transform.position.z;
		if (UnityEngine.Input.GetKey(KeyCode.Q))
		{
			position.y += -this.speedMultiplier;
		}
		if (UnityEngine.Input.GetKey(KeyCode.E))
		{
			position.y += this.speedMultiplier;
		}
		base.transform.position = position;
	}

	public bool amIRotating()
	{
		return this.isRotating;
	}

	private Vector3 GetBaseInput()
	{
		Vector3 vector = default(Vector3);
		if (UnityEngine.Input.GetKey(KeyCode.W))
		{
			vector += new Vector3(0f, 0f, 1f);
		}
		if (UnityEngine.Input.GetKey(KeyCode.S))
		{
			vector += new Vector3(0f, 0f, -1f);
		}
		if (UnityEngine.Input.GetKey(KeyCode.A))
		{
			vector += new Vector3(-1f, 0f, 0f);
		}
		if (UnityEngine.Input.GetKey(KeyCode.D))
		{
			vector += new Vector3(1f, 0f, 0f);
		}
		return vector;
	}

	public float mainSpeed = 100f;

	public float shiftAdd = 250f;

	public float maxShift = 1000f;

	public float camSens = 0.25f;

	private Vector3 lastMouse = new Vector3(255f, 255f, 255f);

	private float totalRun = 1f;

	private bool isRotating;

	private float speedMultiplier;

	public float mouseSensitivity = 5f;

	private float rotationY;
}
