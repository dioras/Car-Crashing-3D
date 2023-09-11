using System;
using CustomVP;
using UnityEngine;

public class IBeamFrontSuspension : Suspension
{
	private void Start()
	{
		this.carController = base.GetComponentInParent<CarController>();
		this.FLWheel.DummyDefPos = this.FLWheel.Dummy.localPosition;
		this.FRWheel.DummyDefPos = this.FRWheel.Dummy.localPosition;
	}

	public override void OnValidate()
	{
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		this.side = Side.Front;
		this.DoPerches();
		this.DoWidth();
		this.DoTrailingArmMounts();
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

	public override SuspensionValue[] GetControlValues()
	{
		return new SuspensionValue[]
		{
			this.Controls.AxisWidth,
			this.Controls.Damping,
			this.Controls.PerchHeight,
			this.Controls.PerchWidth,
			this.Controls.ShocksGroup,
			this.Controls.ShocksSize,
			this.Controls.Stiffness,
			this.Controls.TrailingArmMountsWidth,
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

	private void DoShocks()
	{
		this.FRWheel.ShockUps.LookAt(this.FRWheel.ShockDowns, -base.transform.forward);
		this.FRWheel.ShockDowns.LookAt(this.FRWheel.ShockUps, -base.transform.forward);
		this.FLWheel.ShockUps.LookAt(this.FLWheel.ShockDowns, -base.transform.forward);
		this.FLWheel.ShockDowns.LookAt(this.FLWheel.ShockUps, -base.transform.forward);
		this.FLWheel.ShockUps.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.FLWheel.ShockDowns.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.FRWheel.ShockUps.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.FRWheel.ShockDowns.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
	}

	private void ChangeShocks()
	{
		for (int i = 0; i < this.FRWheel.Shocks.Length; i++)
		{
			this.FLWheel.Shocks[i].gameObject.SetActive(i == this.Controls.ShocksGroup.IntValue);
			this.FRWheel.Shocks[i].gameObject.SetActive(i == this.Controls.ShocksGroup.IntValue);
		}
	}

	private void DoTrailingArmMounts()
	{
		this.FLWheel.TrailingArmMountBone.localPosition = new Vector3(-this.Controls.TrailingArmMountsWidth.FloatValue, 0f, 0f);
		this.FRWheel.TrailingArmMountBone.localPosition = new Vector3(this.Controls.TrailingArmMountsWidth.FloatValue, 0f, 0f);
	}

	private void DoPerches()
	{
		this.FRWheel.PerchBone.localPosition = new Vector3(this.Controls.PerchWidth.FloatValue, 0f, this.Controls.PerchHeight.FloatValue);
		this.FLWheel.PerchBone.localPosition = new Vector3(-this.Controls.PerchWidth.FloatValue, 0f, this.Controls.PerchHeight.FloatValue);
	}

	private void DoWidth()
	{
		this.FLWheel.FrameBone.localPosition = new Vector3(this.Controls.AxisWidth.FloatValue, 0f, 0f);
		this.FRWheel.FrameBone.localPosition = new Vector3(-this.Controls.AxisWidth.FloatValue, 0f, 0f);
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

	public override void UpdateSuspension(float SteerAngle, float WheelRadius, float rpm)
	{
		this.FLWheel.BrakeDisk.Rotate(this.FLWheel.BrakeDisk.forward, -rpm, Space.World);
		this.FRWheel.BrakeDisk.Rotate(this.FRWheel.BrakeDisk.right, rpm, Space.World);
		float num = -0.002f;
		if (this.KeepWheelsVertical)
		{
			num /= 2f;
		}
		this.FLWheel.TieRodEndBone.LookAt(this.FLWheel.TieRodStartBone, base.transform.up);
		this.FLWheel.TieRodStartBone.LookAt(this.FLWheel.TieRodEndBone, base.transform.up);
		this.FLWheel.SteeringBrace.localEulerAngles = new Vector3(0f, -SteerAngle, 0f);
		this.SteeringRailLBone.LookAt(this.SteeringRailRBone, base.transform.up);
		this.SteeringRailRBone.LookAt(this.SteeringRailLBone, base.transform.up);
		float num2 = 0.7f - Mathf.Cos(Vector3.Angle(this.FLWheel.TrailingArmTarget.position - this.FLWheel.TrailingArm.position, base.transform.forward) * 0.0174532924f) * 0.7f;
		num2 /= 3f;
		this.FLWheel.WheelColliderHolder.transform.localPosition = new Vector3(-this.Controls.AxisWidth.FloatValue * base.transform.lossyScale.x, 0f, -num2 * base.transform.lossyScale.x);
		this.FLWheel.RealCamber = Vector3.Angle(base.transform.right, Vector3.ProjectOnPlane(this.FLWheel.CamberMeasurer.right, base.transform.forward));
		if (base.transform.InverseTransformVector(Vector3.Cross(base.transform.right, Vector3.ProjectOnPlane(this.FLWheel.CamberMeasurer.right, base.transform.forward))).z < 0f)
		{
			this.FLWheel.RealCamber = -this.FLWheel.RealCamber;
		}
		Vector3 position = this.Raycasters[0].position - this.Raycasters[0].up * (this.Controls.Travel.FloatValue + 0.2f);
		RaycastHit raycastHit;
		if (Physics.Raycast(this.Raycasters[0].position, -this.Raycasters[0].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		Vector3 localPosition = this.FLWheel.Dummy.localPosition;
		localPosition.y = this.FLWheel.DummyDefPos.y + num2;
		localPosition.z = this.FLWheel.Dummy.parent.InverseTransformPoint(position).z - num + 0.01f;
		this.FLWheel.Dummy.localPosition = localPosition;
		this.FLWheel.Arm.LookAt(this.FLWheel.Dummy, base.transform.up);
		this.FLWheel.TrailingArm.LookAt(this.FLWheel.TrailingArmTarget, base.transform.up);
		this.FLWheel.Knuckle.localEulerAngles = new Vector3(0f, (!this.KeepWheelsVertical) ? 0f : this.FLWheel.RealCamber, SteerAngle - this.FLWheel.Arm.localEulerAngles.x);
		this.FRWheel.TieRodEndBone.LookAt(this.FRWheel.TieRodStartBone, base.transform.up);
		this.FRWheel.TieRodStartBone.LookAt(this.FRWheel.TieRodEndBone, base.transform.up);
		this.FRWheel.SteeringBrace.localEulerAngles = new Vector3(0f, -SteerAngle, 0f);
		num2 = 0.7f - Mathf.Cos(Vector3.Angle(this.FRWheel.TrailingArmTarget.position - this.FRWheel.TrailingArm.position, base.transform.forward) * 0.0174532924f) * 0.7f;
		num2 /= 3f;
		this.FRWheel.WheelColliderHolder.transform.localPosition = new Vector3(this.Controls.AxisWidth.FloatValue * base.transform.lossyScale.x, 0f, -num2 * base.transform.lossyScale.x);
		this.FRWheel.RealCamber = Vector3.Angle(base.transform.right, Vector3.ProjectOnPlane(this.FRWheel.CamberMeasurer.right, base.transform.forward));
		if (base.transform.InverseTransformVector(Vector3.Cross(base.transform.right, Vector3.ProjectOnPlane(this.FRWheel.CamberMeasurer.right, base.transform.forward))).z > 0f)
		{
			this.FRWheel.RealCamber = -this.FRWheel.RealCamber;
		}
		position = this.Raycasters[1].position - this.Raycasters[1].up * (this.Controls.Travel.FloatValue + 0.2f);
		if (Physics.Raycast(this.Raycasters[1].position, -this.Raycasters[1].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		localPosition = this.FRWheel.Dummy.localPosition;
		localPosition.y = this.FRWheel.DummyDefPos.y + num2;
		localPosition.z = this.FRWheel.Dummy.parent.InverseTransformPoint(position).z - num + 0.01f;
		this.FRWheel.Dummy.localPosition = localPosition;
		this.FRWheel.Arm.LookAt(this.FRWheel.Dummy, base.transform.up);
		this.FRWheel.TrailingArm.LookAt(this.FRWheel.TrailingArmTarget, base.transform.up);
		this.FRWheel.Knuckle.localEulerAngles = new Vector3(0f, (!this.KeepWheelsVertical) ? 0f : (-this.FRWheel.RealCamber), SteerAngle + this.FRWheel.Arm.localEulerAngles.x);
		this.DoShocks();
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
		if (this.carController != null)
		{
			this.SteeringRackMovingPart.localPosition = new Vector3(0f, 0f, -Mathf.LerpUnclamped(0.006f, -0.006f, (num + this.carController.maxSteeringAngle) / (this.carController.maxSteeringAngle * 2f)));
		}
		float num2 = -0.002f;
		if (this.KeepWheelsVertical)
		{
			num2 /= 2f;
		}
		float perFrameRotation = this.wheelColliders[0].wheelCollider.perFrameRotation;
		this.FLWheel.BrakeDisk.Rotate(this.FLWheel.BrakeDisk.forward, -perFrameRotation, Space.World);
		this.FLWheel.TieRodEndBone.LookAt(this.FLWheel.TieRodStartBone, base.transform.up);
		this.FLWheel.TieRodStartBone.LookAt(this.FLWheel.TieRodEndBone, base.transform.up);
		this.FLWheel.SteeringBrace.localEulerAngles = new Vector3(0f, -num, 0f);
		this.SteeringRailLBone.LookAt(this.SteeringRailRBone, base.transform.up);
		this.SteeringRailRBone.LookAt(this.SteeringRailLBone, base.transform.up);
		float num3 = 0.7f - Mathf.Cos(Vector3.Angle(this.FLWheel.TrailingArmTarget.position - this.FLWheel.TrailingArm.position, base.transform.forward) * 0.0174532924f) * 0.7f;
		num3 /= 3f;
		this.FLWheel.WheelColliderHolder.transform.localPosition = new Vector3(-this.Controls.AxisWidth.FloatValue * base.transform.lossyScale.x, 0f, -num3 * base.transform.lossyScale.x);
		this.FLWheel.RealCamber = Vector3.Angle(base.transform.right, Vector3.ProjectOnPlane(this.FLWheel.CamberMeasurer.right, base.transform.forward));
		if (base.transform.InverseTransformVector(Vector3.Cross(base.transform.right, Vector3.ProjectOnPlane(this.FLWheel.CamberMeasurer.right, base.transform.forward))).z < 0f)
		{
			this.FLWheel.RealCamber = -this.FLWheel.RealCamber;
		}
		Vector3 localPosition = this.FLWheel.Dummy.localPosition;
		localPosition.y = this.FLWheel.DummyDefPos.y + num3;
		localPosition.z = this.FLWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[0].VisualWheel.position).z - num2 + 0.01f;
		this.FLWheel.Dummy.localPosition = localPosition;
		this.FLWheel.Arm.LookAt(this.FLWheel.Dummy, base.transform.up);
		this.FLWheel.TrailingArm.LookAt(this.FLWheel.TrailingArmTarget, base.transform.up);
		this.FLWheel.Knuckle.localEulerAngles = new Vector3(0f, (!this.KeepWheelsVertical) ? 0f : this.FLWheel.RealCamber, num - this.FLWheel.Arm.localEulerAngles.x);
		perFrameRotation = this.wheelColliders[1].wheelCollider.perFrameRotation;
		this.FRWheel.BrakeDisk.Rotate(this.FRWheel.BrakeDisk.right, perFrameRotation, Space.World);
		this.FRWheel.TieRodEndBone.LookAt(this.FRWheel.TieRodStartBone, base.transform.up);
		this.FRWheel.TieRodStartBone.LookAt(this.FRWheel.TieRodEndBone, base.transform.up);
		this.FRWheel.SteeringBrace.localEulerAngles = new Vector3(0f, -num, 0f);
		num3 = 0.7f - Mathf.Cos(Vector3.Angle(this.FRWheel.TrailingArmTarget.position - this.FRWheel.TrailingArm.position, base.transform.forward) * 0.0174532924f) * 0.7f;
		num3 /= 3f;
		this.FRWheel.WheelColliderHolder.transform.localPosition = new Vector3(this.Controls.AxisWidth.FloatValue * base.transform.lossyScale.x, 0f, -num3 * base.transform.lossyScale.x);
		this.FRWheel.RealCamber = Vector3.Angle(base.transform.right, Vector3.ProjectOnPlane(this.FRWheel.CamberMeasurer.right, base.transform.forward));
		if (base.transform.InverseTransformVector(Vector3.Cross(base.transform.right, Vector3.ProjectOnPlane(this.FRWheel.CamberMeasurer.right, base.transform.forward))).z > 0f)
		{
			this.FRWheel.RealCamber = -this.FRWheel.RealCamber;
		}
		localPosition = this.FRWheel.Dummy.localPosition;
		localPosition.y = this.FRWheel.DummyDefPos.y + num3;
		localPosition.z = this.FRWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[1].VisualWheel.position).z - num2 + 0.01f;
		this.FRWheel.Dummy.localPosition = localPosition;
		this.FRWheel.Arm.LookAt(this.FRWheel.Dummy, base.transform.up);
		this.FRWheel.TrailingArm.LookAt(this.FRWheel.TrailingArmTarget, base.transform.up);
		this.FRWheel.Knuckle.localEulerAngles = new Vector3(0f, (!this.KeepWheelsVertical) ? 0f : (-this.FRWheel.RealCamber), num + this.FRWheel.Arm.localEulerAngles.x);
		this.DoShocks();
	}

	public FrontIBeamWheel FLWheel;

	public FrontIBeamWheel FRWheel;

	private CarController carController;

	public Transform SteeringRailLBone;

	public Transform SteeringRailRBone;

	public Transform SteeringRackMovingPart;

	public IBeamFrontSuspensionControls Controls;

	private bool NoWheelColliders;

	private bool KeepWheelsVertical = true;

	private Vector3 previousPosition;

	private float previousUpdateTime;
}
