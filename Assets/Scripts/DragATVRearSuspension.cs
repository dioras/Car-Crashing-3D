using System;
using CustomVP;
using UnityEngine;

public class DragATVRearSuspension : Suspension
{
	private void Start()
	{
		this.OnValidate();
	}

	private void DoWheelColliderParameters()
	{
		if (this.wheelColliders[0] == null || this.wheelColliders[1] == null)
		{
			return;
		}
		WheelComponent wheelComponent = this.wheelColliders[0];
		float floatValue = this.Controls.Travel.FloatValue;
		this.wheelColliders[1].suspensionLength = floatValue;
		wheelComponent.suspensionLength = floatValue;
		this.wheelColliders[0].spring = (this.wheelColliders[1].spring = this.Controls.Stiffness.FloatValue);
		this.wheelColliders[0].damper = (this.wheelColliders[1].damper = this.Controls.Damping.FloatValue);
		this.wheelColliders[0].OnValidate();
		this.wheelColliders[1].OnValidate();
	}

	public override SuspensionValue[] GetControlValues()
	{
		return new SuspensionValue[]
		{
			this.Controls.AxisWidth,
			this.Controls.Damping,
			this.Controls.RearAxleOffset,
			this.Controls.ShocksGroup,
			this.Controls.ShocksSize,
			this.Controls.ShocksUpsHeight,
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

	public override void OnValidate()
	{
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		this.DoRearAxleOffset();
		this.DoWheelColliderParameters();
		this.DoAxisWidth();
		this.ChangeShocks();
		this.DoShocks();
		this.side = Side.Rear;
	}

	private void DoRearAxleOffset()
	{
		this.RearAxleHolder.localPosition = new Vector3(this.Controls.RearAxleOffset.FloatValue, 0f, 0f);
		this.DriveshaftEnd.localPosition = new Vector3(0f, 0f, this.Controls.RearAxleOffset.FloatValue);
		this.RearArmsStart.localPosition = new Vector3(0f, 0f, -this.Controls.RearAxleOffset.FloatValue);
	}

	private void DoAxisWidth()
	{
		this.RLWheel.AxleEnd.localPosition = new Vector3(0f, -this.Controls.AxisWidth.FloatValue, 0f);
		this.RRWheel.AxleEnd.localPosition = new Vector3(0f, this.Controls.AxisWidth.FloatValue, 0f);
		this.RLWheel.WheelColliderHolder.localPosition = new Vector3(-this.Controls.AxisWidth.FloatValue, 0f, 0f);
		this.RRWheel.WheelColliderHolder.localPosition = new Vector3(this.Controls.AxisWidth.FloatValue, 0f, 0f);
	}

	private void DoShocks()
	{
		this.ShockDowns.LookAt(this.ShockUps, base.transform.right);
		this.ShockUps.LookAt(this.ShockDowns, base.transform.right);
	}

	private void ChangeShocks()
	{
		for (int i = 0; i < this.Shocks.Length; i++)
		{
			this.Shocks[i].gameObject.SetActive(i == this.Controls.ShocksGroup.IntValue);
		}
		this.ShockDowns.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.ShockUps.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
	}

	private void FixedUpdate()
	{
		if (this.NoWheelColliders)
		{
			return;
		}
		if (this.wheelColliders[0] == null || this.wheelColliders[1] == null)
		{
			this.NoWheelColliders = true;
			return;
		}
		float perFrameRotation = this.wheelColliders[0].wheelCollider.perFrameRotation;
		this.RLWheel.Dummy.position = this.wheelColliders[0].GetVisualWheelPosition();
		this.RRWheel.Dummy.position = this.wheelColliders[1].GetVisualWheelPosition();
		Vector3 localPosition = this.RearAxleDummy.localPosition;
		localPosition.y = this.RLWheel.Dummy.localPosition.y;
		this.RearAxleDummy.localPosition = localPosition;
		this.RearAxleDummy.LookAt(this.RRWheel.Dummy, this.RearAxleTarget.position - this.RearAxleDummy.position);
		this.RotationAxle.Rotate(this.RotationAxle.right, perFrameRotation, Space.World);
		this.DriveshaftStart.Rotate(0f, 0f, perFrameRotation);
		this.DoShocks();
	}

	public override void UpdateSuspension(float SteerAngle, float WheelRadius, float rpm)
	{
		Vector3 position = this.Raycasters[0].position - this.Raycasters[0].up * (this.Controls.Travel.FloatValue + 0.2f);
		RaycastHit raycastHit;
		if (Physics.Raycast(this.Raycasters[0].position, -this.Raycasters[0].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		this.RLWheel.Dummy.position = position;
		position = this.Raycasters[1].position - this.Raycasters[1].up * (this.Controls.Travel.FloatValue + 0.2f);
		if (Physics.Raycast(this.Raycasters[1].position, -this.Raycasters[1].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		this.RRWheel.Dummy.position = position;
		Vector3 localPosition = this.RearAxleDummy.localPosition;
		localPosition.y = this.RLWheel.Dummy.localPosition.y;
		this.RearAxleDummy.localPosition = localPosition;
		this.RearAxleDummy.LookAt(this.RRWheel.Dummy, this.RearAxleTarget.position - this.RearAxleDummy.position);
		this.RotationAxle.Rotate(this.RotationAxle.right, rpm, Space.World);
		this.DriveshaftStart.Rotate(0f, 0f, rpm);
		this.DoShocks();
	}

	public DragATVRearWheel RLWheel;

	public DragATVRearWheel RRWheel;

	public Transform RearAxleDummy;

	public Transform RearAxleTarget;

	public Transform DriveshaftStart;

	public Transform DriveshaftEnd;

	public Transform RotationAxle;

	public Transform RearAxleHolder;

	public Transform RearArmsStart;

	public Transform ShockDowns;

	public Transform ShockUps;

	public Transform[] Shocks;

	public DragATVRearSuspensionControls Controls;

	private bool NoWheelColliders;

	private Vector3 previousPosition;

	private float previousUpdateTime;
}
