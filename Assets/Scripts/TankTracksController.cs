using System;
using System.Collections.Generic;
using CustomVP;
using UnityEngine;

public class TankTracksController : MonoBehaviour
{
	private void Start()
	{
		foreach (Renderer renderer in base.GetComponentsInChildren<Renderer>())
		{
			foreach (Material material in renderer.materials)
			{
				if (material.name.Contains(this.TracksMaterial.name))
				{
					this.tracksMats.Add(material);
				}
			}
		}
	}

	private void FixedUpdate()
	{
		if (this.wc.wheelCollider == null)
		{
			return;
		}
		this.SteeringHelper.localEulerAngles = new Vector3(0f, this.wc.wheelCollider.steeringAngle, 0f);
		this.Body.position = this.MasterWheel.position;
		float steeringAngle = this.wc.wheelCollider.steeringAngle;
		Vector3 hitPoint = this.wc.wheelCollider.hitPoint;
		Vector3 position = this.wc.transform.parent.InverseTransformPoint(hitPoint);
		position.x = this.wc.transform.localPosition.x;
		Vector3 a = this.wc.transform.parent.TransformPoint(position);
		float b = Vector3.SignedAngle(-this.wc.transform.up, a - this.wc.transform.position, this.wc.transform.right);
		if (this.wc.IsGrounded)
		{
			this.xAngle = Mathf.Lerp(this.xAngle, b, Time.fixedDeltaTime * 5f);
		}
		Vector3 lhs = this.MasterWheel.right;
		if (base.transform.localPosition.x < 0f)
		{
			lhs = -this.MasterWheel.right;
		}
		Vector3 from = Vector3.Cross(lhs, base.transform.forward);
		float num = Vector3.SignedAngle(from, base.transform.up, base.transform.forward);
		float z = -num;
		this.Body.localEulerAngles = new Vector3(this.xAngle, steeringAngle, z);
		foreach (Transform transform in this.littleWheels)
		{
			transform.Rotate(-this.wc.rpm * 0.1f, 0f, 0f);
		}
		foreach (Material material in this.tracksMats)
		{
			Vector2 vector = material.GetTextureOffset("_Texture");
			vector += new Vector2(0f, this.wc.rpm * 0.0005f);
			material.SetTextureOffset("_Texture", vector);
			material.SetTextureOffset("_Dirt", vector);
		}
	}

	public Transform MasterWheel;

	public Transform Body;

	public Transform[] littleWheels;

	public Transform SteeringHelper;

	[HideInInspector]
	public WheelComponent wc;

	[HideInInspector]
	public Transform wheelHolder;

	public Material TracksMaterial;

	private List<Material> tracksMats = new List<Material>();

	private float xAngle;
}
