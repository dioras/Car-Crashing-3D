using System;
using CustomVP;
using UnityEngine;

public class DirtBikeFrontSuspension : Suspension
{
	public override SuspensionValue[] GetControlValues()
	{
		return new SuspensionValue[]
		{
			this.Controls.Damping,
			this.Controls.ShocksGroup,
			this.Controls.ShocksSize,
			this.Controls.Stiffness,
			this.Controls.Travel
		};
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

	private void Start()
	{
		this.carController = base.GetComponentInParent<CarController>();
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
		RaycastHit raycastHit;
		if (Physics.Raycast(this.Raycasters[0].position, -this.Raycasters[0].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		this.Dummy.position = position;
		Vector3 localPosition = this.Hub.localPosition;
		localPosition.z = this.Hub.parent.InverseTransformPoint(this.Dummy.position).z;
		this.Hub.localPosition = localPosition;
		Quaternion b = Quaternion.Euler(0f, 0f, -SteerAngle * 30f);
		if (this.fork != null)
		{
			this.fork.localRotation = Quaternion.Lerp(this.fork.localRotation, b, Time.deltaTime * 5f);
		}
		this.WheelHolders[0].Rotate(this.WheelHolders[0].right, rpm, Space.World);
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
		this.Dummy.position = this.wheelColliders[0].GetVisualWheelPosition();
		Vector3 localPosition = this.Dummy.localPosition;
		localPosition.z = this.Dummy.parent.InverseTransformPoint(this.Hub.position).z;
		this.Dummy.localPosition = localPosition;
		localPosition = this.Hub.localPosition;
		localPosition.z = this.Hub.parent.InverseTransformPoint(this.Dummy.position).z;
		this.Hub.localPosition = localPosition;
		float perFrameRotation = this.wheelColliders[0].wheelCollider.perFrameRotation;
		this.WheelHolders[0].Rotate(this.WheelHolders[0].right, perFrameRotation, Space.World);
		Quaternion b = Quaternion.Euler(0f, 0f, -this.carController.xInput * 30f);
		if (this.fork != null)
		{
			this.fork.localRotation = Quaternion.Lerp(this.fork.localRotation, b, Time.deltaTime * 5f);
		}
	}

	private CarController carController;

	public Transform Dummy;

	public Transform Hub;

	public Transform fork;

	public DirtBikeFrontSuspensionControls Controls;

	private bool NoWheelColliders;

	private Vector3 previousPosition;

	private float previousUpdateTime;
}
