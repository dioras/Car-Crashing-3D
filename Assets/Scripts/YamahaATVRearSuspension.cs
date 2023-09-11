using System;
using CustomVP;
using UnityEngine;

public class YamahaATVRearSuspension : Suspension
{
	public override SuspensionValue[] GetControlValues()
	{
		return new SuspensionValue[]
		{
			this.Controls.AxisWidth,
			this.Controls.Damping,
			this.Controls.RearAxleOffset,
			this.Controls.ShocksGroup,
			this.Controls.ShocksSize,
			this.Controls.ShockUpsHeight,
			this.Controls.ShockUpsOffset,
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
		this.OnValidate();
	}

	public override void OnValidate()
	{
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		this.DoRearAxleOffset();
		this.DoRearArm();
		this.DoShocks();
		this.ChangeShocks();
		this.DoWheelColliderParameters();
		this.side = Side.Rear;
	}

	private void DoShocks()
	{
		if (this.ShockUp == null)
		{
			return;
		}
		this.ShockUp.localPosition = new Vector3(-this.Controls.ShockUpsOffset.FloatValue, 0f, -this.Controls.ShockUpsHeight.FloatValue);
		this.ShockUp.LookAt(this.ShockDown, -base.transform.up);
		this.ShockDown.LookAt(this.ShockUp, base.transform.up);
		this.ShockUp.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.ShockDown.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
	}

	private void ChangeShocks()
	{
		for (int i = 0; i < this.Shocks.Length; i++)
		{
			this.Shocks[i].gameObject.SetActive(i == this.Controls.ShocksGroup.IntValue);
		}
	}

	private void DoRearAxleOffset()
	{
		Vector3 localPosition = this.RearAxleDummy.localPosition;
		localPosition.x = this.Controls.RearAxleOffset.FloatValue;
		this.RearAxleDummy.localPosition = localPosition;
		if (this.LOffsetHolder == null || this.ROffsetHolder == null)
		{
			return;
		}
		this.LOffsetHolder.localPosition = new Vector3(this.Controls.RearAxleOffset.FloatValue, 0f, -this.Controls.AxisWidth.FloatValue);
		this.ROffsetHolder.localPosition = new Vector3(this.Controls.RearAxleOffset.FloatValue, 0f, this.Controls.AxisWidth.FloatValue);
		this.RLWheel.AxleBone.localPosition = new Vector3(0f, -this.Controls.AxisWidth.FloatValue, 0f);
		this.RRWheel.AxleBone.localPosition = new Vector3(0f, this.Controls.AxisWidth.FloatValue, 0f);
		this.RearArmEndBone.localPosition = new Vector3(this.Controls.RearAxleOffset.FloatValue, 0f, 0f);
	}

	private void DoRearArm()
	{
		this.RearArm.LookAt(this.RearArmTarget, this.RearAxleDummy.up);
		Vector3 localPosition = this.RearArmTarget.localPosition;
		localPosition.z = this.Controls.RearAxleOffset.FloatValue / 4f;
		this.RearArmTarget.localPosition = localPosition;
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

	public override void UpdateSuspension(float SteerAngle, float WheelRadius, float rpm)
	{
		this.RearAxle.Rotate(this.RearAxle.up, rpm, Space.World);
		Vector3 position = this.Raycasters[0].position - this.Raycasters[0].up * (this.Controls.Travel.FloatValue + 0.2f);
		RaycastHit raycastHit;
		if (Physics.Raycast(this.Raycasters[0].position, -this.Raycasters[0].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		Vector3 localPosition = this.RLWheel.Dummy.localPosition;
		localPosition.y = this.RLWheel.Dummy.parent.InverseTransformPoint(position).y;
		this.RLWheel.Dummy.localPosition = localPosition;
		position = this.Raycasters[1].position - this.Raycasters[1].up * (this.Controls.Travel.FloatValue + 0.2f);
		if (Physics.Raycast(this.Raycasters[1].position, -this.Raycasters[1].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		localPosition = this.RRWheel.Dummy.localPosition;
		localPosition.y = this.RRWheel.Dummy.parent.InverseTransformPoint(position).y;
		this.RRWheel.Dummy.localPosition = localPosition;
		localPosition = this.RearAxleDummy.localPosition;
		localPosition.y = this.RLWheel.Dummy.localPosition.y;
		this.RearAxleDummy.localPosition = localPosition;
		this.RearAxleDummy.LookAt(this.RRWheel.Dummy, base.transform.forward);
		this.DoRearArm();
		this.DoShocks();
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
		Vector3 localPosition = this.RLWheel.Dummy.localPosition;
		localPosition.y = this.RLWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[0].GetVisualWheelPosition()).y;
		this.RLWheel.Dummy.localPosition = localPosition;
		localPosition = this.RRWheel.Dummy.localPosition;
		localPosition.y = this.RRWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[1].GetVisualWheelPosition()).y;
		this.RRWheel.Dummy.localPosition = localPosition;
		float perFrameRotation = this.wheelColliders[0].wheelCollider.perFrameRotation;
		this.RearAxle.Rotate(this.RearAxle.up, perFrameRotation, Space.World);
		localPosition = this.RearAxleDummy.localPosition;
		localPosition.y = this.RLWheel.Dummy.localPosition.y;
		this.RearAxleDummy.localPosition = localPosition;
		this.RearAxleDummy.LookAt(this.RRWheel.Dummy, base.transform.forward);
		this.DoRearArm();
		this.DoShocks();
	}

	public YamahaATVRearWheel RLWheel;

	public YamahaATVRearWheel RRWheel;

	public Transform RearArm;

	public Transform RearArmTarget;

	public Transform RearArmEndBone;

	public Transform RearAxle;

	public Transform RearAxleDummy;

	public Transform ShockUp;

	public Transform ShockDown;

	public Transform LOffsetHolder;

	public Transform ROffsetHolder;

	public Transform[] Shocks;

	public YamahaATVRearSuspensionControls Controls;

	private bool NoWheelColliders;
}
