using System;
using CustomVP;
using UnityEngine;

public class SnowBikeFrontSuspension : Suspension
{
	public override SuspensionValue[] GetControlValues()
	{
		return new SuspensionValue[]
		{
			this.Controls.Damping,
			this.Controls.Stiffness,
			this.Controls.Travel
		};
	}

	public override void OnValidate()
	{
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		this.DoWheelColliderParameters();
		this.side = Side.Front;
		if (this.wheelColliders[0] == null)
		{
			return;
		}
		this.wheelColliders[0].OnValidate();
	}

	private void Start()
	{
		this.carController = base.GetComponentInParent<CarController>();
	}

	public override void SetControlValues(SuspensionValue[] values)
	{
		foreach (SuspensionValue suspensionValue in this.GetControlValues())
		{
			foreach (SuspensionValue suspensionValue2 in values)
			{
				if (suspensionValue2.ValueName == suspensionValue.ValueName)
				{
					suspensionValue.ReceiveValues(suspensionValue2);
				}
			}
		}
		this.OnValidate();
	}

	private void DoWheelColliderParameters()
	{
		if (this.wheelColliders[0] == null)
		{
			return;
		}
		this.wheelColliders[0].suspensionLength = this.Controls.Travel.FloatValue;
		this.wheelColliders[0].spring = this.Controls.Stiffness.FloatValue;
		this.wheelColliders[0].damper = this.Controls.Damping.FloatValue;
	}

	public override void UpdateSuspension(float SteerAngle, float WheelRadius, float rpm)
	{
		Vector3 position = this.Raycasters[0].position - this.Raycasters[0].up * (this.Controls.Travel.FloatValue + 0.2f);
		bool flag = false;
		Vector3 up = base.transform.up;
		RaycastHit raycastHit;
		if (Physics.Raycast(this.Raycasters[0].position, -this.Raycasters[0].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
			flag = true;
		}
		Vector3 localPosition = this.hub.localPosition;
		localPosition.z = this.hub.parent.InverseTransformPoint(position).z - 0.02f;
		this.hub.localPosition = localPosition;
		Vector3 vector = base.transform.up;
		if (flag)
		{
			vector = up;
			vector = Vector3.ProjectOnPlane(vector, this.fork.right);
			if (Vector3.SignedAngle(vector, base.transform.up, -base.transform.right) < -45f)
			{
				vector = (base.transform.up - base.transform.forward) / 2f;
			}
			if (Vector3.SignedAngle(vector, base.transform.up, -base.transform.right) > 45f)
			{
				vector = (base.transform.up + base.transform.forward) / 2f;
			}
		}
		this.ski.rotation = Quaternion.Lerp(this.ski.rotation, Quaternion.LookRotation(vector, this.fork.up), Time.deltaTime * 8f);
		Quaternion b = Quaternion.Euler(0f, 0f, -SteerAngle * 30f);
		if (this.fork != null)
		{
			this.fork.localRotation = Quaternion.Lerp(this.fork.localRotation, b, Time.deltaTime * 5f);
		}
	}

	private void FixedUpdate()
	{
		if (this.NoWheelColliders)
		{
			return;
		}
		if (this.wheelColliders[0] == null)
		{
			this.NoWheelColliders = true;
			return;
		}
		Vector3 visualWheelPosition = this.wheelColliders[0].GetVisualWheelPosition();
		Vector3 localPosition = this.hub.localPosition;
		localPosition.z = this.hub.parent.InverseTransformPoint(visualWheelPosition).z - 0.02f;
		this.hub.localPosition = localPosition;
		Vector3 vector = base.transform.up;
		if (this.wheelColliders[0].IsGrounded)
		{
			vector = this.wheelColliders[0].wheelCollider.contactNormal;
			vector = Vector3.ProjectOnPlane(vector, this.fork.right);
			if (Vector3.SignedAngle(vector, base.transform.up, -base.transform.right) < -45f)
			{
				vector = (base.transform.up - base.transform.forward) / 2f;
			}
			if (Vector3.SignedAngle(vector, base.transform.up, -base.transform.right) > 45f)
			{
				vector = (base.transform.up + base.transform.forward) / 2f;
			}
		}
		this.ski.rotation = Quaternion.Lerp(this.ski.rotation, Quaternion.LookRotation(vector, this.fork.up), Time.deltaTime * 8f);
		Quaternion b = Quaternion.Euler(0f, 0f, -this.carController.xInput * 30f);
		if (this.fork != null)
		{
			this.fork.localRotation = Quaternion.Lerp(this.fork.localRotation, b, Time.deltaTime * 5f);
		}
	}

	private CarController carController;

	public Transform hub;

	public Transform ski;

	public Transform fork;

	public SnowBikeFrontSuspensionControls Controls;

	private bool NoWheelColliders;
}
