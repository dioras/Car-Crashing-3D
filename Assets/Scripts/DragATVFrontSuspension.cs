using System;
using CustomVP;
using UnityEngine;

public class DragATVFrontSuspension : Suspension
{
	public override SuspensionValue[] GetControlValues()
	{
		return new SuspensionValue[]
		{
			this.Controls.AxisWidth,
			this.Controls.Damping,
			this.Controls.PerchWidth,
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
		this.FLWheel.KnuckleDefPos = this.FLWheel.Frame.InverseTransformPoint(this.FLWheel.Knuckle.position);
		this.FRWheel.KnuckleDefPos = this.FRWheel.Frame.InverseTransformPoint(this.FRWheel.Knuckle.position);
	}

	public override void OnValidate()
	{
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		this.DoPerches();
		this.DoWidth();
		this.DoWheelColliderParameters();
		this.DoWheelsOffset();
		this.DoShocks();
		this.ChangeShocks();
		this.side = Side.Front;
		if (this.wheelColliders[0] == null || this.wheelColliders[1] == null)
		{
			return;
		}
		this.wheelColliders[0].OnValidate();
		this.wheelColliders[1].OnValidate();
	}

	private void DoShocks()
	{
		this.FRWheel.ShockUps.LookAt(this.FRWheel.ShockDowns, -base.transform.right);
		this.FRWheel.ShockDowns.LookAt(this.FRWheel.ShockUps, -base.transform.right);
		this.FLWheel.ShockUps.LookAt(this.FLWheel.ShockDowns, base.transform.right);
		this.FLWheel.ShockDowns.LookAt(this.FLWheel.ShockUps, base.transform.right);
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

	private void DoWheelsOffset()
	{
		if (this.wheelColliders[0] == null || this.wheelColliders[1] == null)
		{
			return;
		}
		this.FLWheel.WheelColliderHolder.localPosition = new Vector3(-this.FLWheel.Deviation * base.transform.lossyScale.x, 0f, 0f);
		this.FRWheel.WheelColliderHolder.transform.localPosition = new Vector3(this.FRWheel.Deviation * base.transform.lossyScale.x, 0f, 0f);
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
		this.FLWheel.Frame.localPosition = new Vector3(0f, -this.Controls.AxisWidth.FloatValue, 0f);
		this.FRWheel.Frame.localPosition = new Vector3(0f, this.Controls.AxisWidth.FloatValue, 0f);
	}

	private void DoPerches()
	{
		this.FLWheel.Perch.localPosition = new Vector3(0f, -this.Controls.PerchWidth.FloatValue, 0f);
		this.FRWheel.Perch.localPosition = new Vector3(0f, this.Controls.PerchWidth.FloatValue, 0f);
	}

	public override void UpdateSuspension(float SteerAngle, float WheelRadius, float rpm)
	{
		this.FLWheel.BrakeDisk.Rotate(new Vector3(0f, 0f, rpm));
		this.FRWheel.BrakeDisk.Rotate(new Vector3(0f, 0f, rpm));
		this.FLWheel.TieRod.LookAt(this.SteeringRack, base.transform.up);
		Vector3 position = this.Raycasters[0].position - this.Raycasters[0].up * (this.Controls.Travel.FloatValue + 0.2f);
		RaycastHit raycastHit;
		if (Physics.Raycast(this.Raycasters[0].position, -this.Raycasters[0].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		Vector3 localPosition = this.FLWheel.Dummy.localPosition;
		localPosition.z = this.FLWheel.Dummy.parent.InverseTransformPoint(position).z - 0.1f;
		this.FLWheel.Dummy.localPosition = localPosition;
		this.FLWheel.LowerArm.LookAt(this.FLWheel.Dummy, base.transform.forward);
		this.FLWheel.Knuckle.position = this.FLWheel.KnucklePosition.position;
		this.FLWheel.Knuckle.localEulerAngles = new Vector3(0f, 0f, SteerAngle);
		this.FLWheel.UpperArm.LookAt(this.FLWheel.UpperArmTarget, base.transform.forward);
		this.FLWheel.Deviation = -this.FLWheel.Frame.InverseTransformPoint(this.FLWheel.Knuckle.position).y + this.FLWheel.KnuckleDefPos.y;
		this.FRWheel.TieRod.LookAt(this.SteeringRack, base.transform.up);
		position = this.Raycasters[1].position - this.Raycasters[1].up * (this.Controls.Travel.FloatValue + 0.2f);
		if (Physics.Raycast(this.Raycasters[1].position, -this.Raycasters[1].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		localPosition = this.FRWheel.Dummy.localPosition;
		localPosition.z = this.FRWheel.Dummy.parent.InverseTransformPoint(position).z - 0.1f;
		this.FRWheel.Dummy.localPosition = localPosition;
		this.FRWheel.LowerArm.LookAt(this.FRWheel.Dummy, base.transform.forward);
		this.FRWheel.Knuckle.position = this.FRWheel.KnucklePosition.position;
		this.FRWheel.Knuckle.localEulerAngles = new Vector3(0f, 0f, SteerAngle);
		this.FRWheel.UpperArm.LookAt(this.FRWheel.UpperArmTarget, base.transform.forward);
		this.FRWheel.Deviation = this.FRWheel.Frame.InverseTransformPoint(this.FRWheel.Knuckle.position).y - this.FRWheel.KnuckleDefPos.y;
		this.DoShocks();
		this.DoWheelsOffset();
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
		float num = 0f;
		if (this.carController != null)
		{
			num = this.carController.Steering;
		}
		if (this.carController != null)
		{
			this.SteeringRack.localPosition = new Vector3(0f, Mathf.Lerp(-0.03f, 0.03f, (-num + this.carController.maxSteeringAngle) / (this.carController.maxSteeringAngle * 2f)), 0f);
		}
		float num2 = this.wheelColliders[0].rpm * Time.fixedDeltaTime * 6f / base.transform.lossyScale.x;
		this.FLWheel.BrakeDisk.Rotate(new Vector3(0f, 0f, -num2));
		this.FLWheel.TieRod.LookAt(this.SteeringRack, base.transform.forward);
		Vector3 localPosition = this.FLWheel.Dummy.localPosition;
		localPosition.z = this.FLWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[0].GetVisualWheelPosition()).z - 0.1f;
		this.FLWheel.Dummy.localPosition = localPosition;
		this.FLWheel.LowerArm.LookAt(this.FLWheel.Dummy, base.transform.forward);
		this.FLWheel.Knuckle.position = this.FLWheel.KnucklePosition.position;
		this.FLWheel.Knuckle.localEulerAngles = new Vector3(0f, 0f, num);
		this.FLWheel.UpperArm.LookAt(this.FLWheel.UpperArmTarget, base.transform.forward);
		this.FLWheel.Deviation = -this.FLWheel.Frame.InverseTransformPoint(this.FLWheel.Knuckle.position).y + this.FLWheel.KnuckleDefPos.y;
		num2 = this.wheelColliders[1].rpm * Time.fixedDeltaTime * 6f / base.transform.lossyScale.x;
		this.FRWheel.BrakeDisk.Rotate(new Vector3(0f, 0f, -num2));
		this.FRWheel.TieRod.LookAt(this.SteeringRack, base.transform.forward);
		localPosition = this.FRWheel.Dummy.localPosition;
		localPosition.z = this.FRWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[1].GetVisualWheelPosition()).z - 0.1f;
		this.FRWheel.Dummy.localPosition = localPosition;
		this.FRWheel.LowerArm.LookAt(this.FRWheel.Dummy, base.transform.forward);
		this.FRWheel.Knuckle.position = this.FRWheel.KnucklePosition.position;
		this.FRWheel.Knuckle.localEulerAngles = new Vector3(0f, 0f, num);
		this.FRWheel.UpperArm.LookAt(this.FRWheel.UpperArmTarget, base.transform.forward);
		this.FRWheel.Deviation = this.FRWheel.Frame.InverseTransformPoint(this.FRWheel.Knuckle.position).y - this.FRWheel.KnuckleDefPos.y;
		this.DoShocks();
		this.DoWheelsOffset();
	}

	private CarController carController;

	public DragATVFrontWheel FLWheel;

	public DragATVFrontWheel FRWheel;

	public Transform SteeringRack;

	public DragATVFrontSuspensionControls Controls;

	private bool NoWheelColliders;

	private Vector3 previousPosition;

	private float previousUpdateTime;
}
