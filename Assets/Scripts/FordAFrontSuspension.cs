using System;
using CustomVP;
using UnityEngine;

public class FordAFrontSuspension : Suspension
{
	public override SuspensionValue[] GetControlValues()
	{
		return new SuspensionValue[]
		{
			this.Controls.AxisWidth,
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
		this.side = Side.Front;
		this.DoWidth();
		this.DoWheelColliderParameters();
		this.DoShocks();
		this.ChangeShocks();
		if (this.wheelColliders[0] == null || this.wheelColliders[1] == null)
		{
			return;
		}
		this.wheelColliders[0].OnValidate();
		this.wheelColliders[1].OnValidate();
	}

	private void DoShocks()
	{
		this.FRWheel.ShockUps.LookAt(this.FRWheel.ShockDowns, base.transform.forward);
		this.FRWheel.ShockDowns.LookAt(this.FRWheel.ShockUps, base.transform.forward);
		this.FLWheel.ShockUps.LookAt(this.FLWheel.ShockDowns, base.transform.forward);
		this.FLWheel.ShockDowns.LookAt(this.FLWheel.ShockUps, base.transform.forward);
		this.FLWheel.ShockUps.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.FLWheel.ShockDowns.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.FRWheel.ShockUps.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.FRWheel.ShockDowns.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
	}

	private void ChangeShocks()
	{
		for (int i = 0; i < this.FLWheel.Shocks.Length; i++)
		{
			this.FLWheel.Shocks[i].gameObject.SetActive(i == this.Controls.ShocksGroup.IntValue);
			this.FRWheel.Shocks[i].gameObject.SetActive(i == this.Controls.ShocksGroup.IntValue);
		}
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
	}

	private void DoWidth()
	{
		this.FLWheel.LowerArmEnd.localPosition = new Vector3(0f, 0f, -this.Controls.AxisWidth.FloatValue);
		this.FLWheel.WheelColliderHolder.localPosition = new Vector3(-this.Controls.AxisWidth.FloatValue * base.transform.lossyScale.x, 0f, 0f);
		this.FRWheel.LowerArmEnd.localPosition = new Vector3(0f, 0f, -this.Controls.AxisWidth.FloatValue);
		this.FRWheel.WheelColliderHolder.localPosition = new Vector3(this.Controls.AxisWidth.FloatValue * base.transform.lossyScale.x, 0f, 0f);
	}

	private void FixedUpdate()
	{
		if (this.NoWheelColliders)
		{
			return;
		}
		foreach (WheelComponent x in this.wheelColliders)
		{
			if (x == null)
			{
				this.NoWheelColliders = true;
				return;
			}
		}
		float num = 0f;
		if (this.carController != null)
		{
			num = this.carController.Steering;
		}
		float perFrameRotation = this.wheelColliders[0].wheelCollider.perFrameRotation;
		this.FLWheel.BrakeDisk.Rotate(-this.FLWheel.BrakeDisk.forward, perFrameRotation, Space.World);
		this.FLWheel.Knuckle.localPosition = this.FLWheel.Knuckle.parent.InverseTransformPoint(this.FLWheel.KnucklePos.position);
		this.FLWheel.Knuckle.localEulerAngles = new Vector3(0f, 0f, num);
		this.FLWheel.UpperArmStart.LookAt(this.FLWheel.UpperArmEnd, base.transform.up);
		this.FLWheel.UpperArmEnd.LookAt(this.FLWheel.UpperArmStart, base.transform.up);
		this.FLWheel.TieRodEndBone.LookAt(this.FLWheel.TieRodStartBone, base.transform.up);
		this.FLWheel.TieRodStartBone.LookAt(this.FLWheel.TieRodEndBone, base.transform.up);
		Vector3 visualWheelPosition = this.wheelColliders[0].GetVisualWheelPosition();
		float num2 = Vector3.Angle(base.transform.right, this.FLWheel.LowerArmStart.position - visualWheelPosition);
		float num3 = Mathf.Cos(num2 * 0.0174532924f);
		Vector3 localPosition = this.FLWheel.LowerArmStart.localPosition;
		localPosition.x -= Vector3.Distance(this.FLWheel.LowerArmStart.position, this.FLWheel.KnucklePos.position) / this.FLWheel.Dummy.lossyScale.x * num3;
		localPosition.y = this.FLWheel.Dummy.parent.InverseTransformPoint(visualWheelPosition).y;
		this.FLWheel.Dummy.localPosition = localPosition;
		this.FLWheel.LowerArmStart.LookAt(this.FLWheel.Dummy, base.transform.up);
		perFrameRotation = this.wheelColliders[1].wheelCollider.perFrameRotation;
		this.FRWheel.BrakeDisk.Rotate(this.FRWheel.BrakeDisk.forward, perFrameRotation, Space.World);
		this.FRWheel.Knuckle.localPosition = this.FRWheel.Knuckle.parent.InverseTransformPoint(this.FRWheel.KnucklePos.position);
		this.FRWheel.Knuckle.localEulerAngles = new Vector3(0f, 0f, num);
		this.FRWheel.UpperArmStart.LookAt(this.FRWheel.UpperArmEnd, base.transform.up);
		this.FRWheel.UpperArmEnd.LookAt(this.FRWheel.UpperArmStart, base.transform.up);
		this.FRWheel.TieRodEndBone.LookAt(this.FRWheel.TieRodStartBone, base.transform.up);
		this.FRWheel.TieRodStartBone.LookAt(this.FRWheel.TieRodEndBone, base.transform.up);
		visualWheelPosition = this.wheelColliders[1].GetVisualWheelPosition();
		num2 = Vector3.Angle(base.transform.right, this.FRWheel.LowerArmStart.position - visualWheelPosition);
		num3 = Mathf.Cos(num2 * 0.0174532924f);
		localPosition = this.FRWheel.LowerArmStart.localPosition;
		localPosition.x -= Vector3.Distance(this.FRWheel.LowerArmStart.position, this.FRWheel.KnucklePos.position) / this.FRWheel.Dummy.lossyScale.x * num3;
		localPosition.y = this.FRWheel.Dummy.parent.InverseTransformPoint(visualWheelPosition).y;
		this.FRWheel.Dummy.localPosition = localPosition;
		this.FRWheel.LowerArmStart.LookAt(this.FRWheel.Dummy, base.transform.up);
		if (this.carController != null)
		{
			this.SteeringRack.localPosition = new Vector3(0f, 0f, -Mathf.Lerp(-0.3f, 0.3f, (num + this.carController.maxSteeringAngle) / (this.carController.maxSteeringAngle * 2f)));
		}
		this.DoShocks();
	}

	private CarController carController;

	public FordAFrontWheel FLWheel;

	public FordAFrontWheel FRWheel;

	public Transform SteeringRack;

	public FordAFrontSuspensionControls Controls;

	private bool NoWheelColliders;
}
