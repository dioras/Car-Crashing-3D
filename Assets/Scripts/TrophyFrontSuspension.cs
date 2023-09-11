using System;
using CustomVP;
using UnityEngine;

public class TrophyFrontSuspension : Suspension
{
	private float Squared(float number)
	{
		return Mathf.Pow(number, 2f);
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
		this.FLWheel.DefBrakeDiskPosition = this.FLWheel.FrameBone.InverseTransformPoint(this.FLWheel.BrakeDisk.position);
		this.FRWheel.DefBrakeDiskPosition = this.FRWheel.FrameBone.InverseTransformPoint(this.FRWheel.BrakeDisk.position);
	}

	public override void OnValidate()
	{
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		this.side = Side.Front;
		this.DoWidth();
		this.DoPerches();
		this.DoWheelsOffset();
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
		this.FRWheel.ShockUps.LookAt(this.FRWheel.ShockDowns, -base.transform.up);
		this.FRWheel.ShockDowns.LookAt(this.FRWheel.ShockUps, -base.transform.up);
		this.FLWheel.ShockUps.LookAt(this.FLWheel.ShockDowns, -base.transform.up);
		this.FLWheel.ShockDowns.LookAt(this.FLWheel.ShockUps, -base.transform.up);
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

	private void DoPerches()
	{
		this.FLWheel.PerchBone.localPosition = new Vector3(-this.Controls.PerchWidth.FloatValue, 0f, this.Controls.PerchHeight.FloatValue);
		this.FRWheel.PerchBone.localPosition = new Vector3(this.Controls.PerchWidth.FloatValue, 0f, this.Controls.PerchHeight.FloatValue);
	}

	private void DoWidth()
	{
		this.FLWheel.FrameBone.localPosition = new Vector3(-this.Controls.AxisWidth.FloatValue, 0f, 0f);
		this.FRWheel.FrameBone.localPosition = new Vector3(this.Controls.AxisWidth.FloatValue, 0f, 0f);
	}

	private void DoWheelsOffset()
	{
		if (this.wheelColliders[0] == null || this.wheelColliders[1] == null)
		{
			return;
		}
		this.FLWheel.WheelColliderHolder.transform.localPosition = new Vector3(-this.FLWheel.Deviation * base.transform.lossyScale.x, 0f, 0f);
		this.FRWheel.WheelColliderHolder.transform.localPosition = new Vector3(this.FRWheel.Deviation * base.transform.lossyScale.x, 0f, 0f);
	}

	public override void UpdateSuspension(float SteerAngle, float WheelRadius, float rpm)
	{
		this.FLWheel.BrakeDisk.Rotate(this.FLWheel.BrakeDisk.right, rpm, Space.World);
		this.FRWheel.BrakeDisk.Rotate(this.FRWheel.BrakeDisk.right, rpm, Space.World);
		float num = Vector3.Distance(this.FLWheel.Knuckle.position, this.FLWheel.UpperWishbone.position);
		float num2 = Vector3.Distance(this.FLWheel.Knuckle.position, this.FLWheel.UpperWishboneTarget.position);
		float number = 1.95f * num2;
		float f = (this.Squared(num2) + this.Squared(num) - this.Squared(number)) / (2f * num2 * num);
		float num3 = Mathf.Acos(f) * 57.29578f;
		float num4 = -(this.FLWheel.LowerWishbone.localEulerAngles.x - (90f - Vector3.Angle(-this.FLWheel.LowerWishbone.forward, this.FLWheel.UpperWishbone.position - this.FLWheel.Knuckle.position) - num3));
		if (!float.IsNaN(num4) && !float.IsNaN(SteerAngle))
		{
			this.FLWheel.Knuckle.localEulerAngles = new Vector3(0f, num4, SteerAngle);
		}
		this.FLWheel.TieRodEndBone.LookAt(this.FLWheel.TieRodStartBone, base.transform.forward);
		this.FLWheel.TieRodStartBone.LookAt(this.FLWheel.TieRodEndBone, base.transform.forward);
		float num5 = (this.FLWheel.LowerWishbone.localEulerAngles.x <= 20f || this.FLWheel.LowerWishbone.localEulerAngles.x >= 90f) ? 0f : ((this.FLWheel.LowerWishbone.localEulerAngles.x - 20f) * this.FLWheel.HeightCorrectionRatio);
		Vector3 position = this.Raycasters[0].position - this.Raycasters[0].up * (this.Controls.Travel.FloatValue + 0.2f);
		RaycastHit raycastHit;
		if (Physics.Raycast(this.Raycasters[0].position, -this.Raycasters[0].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		Vector3 localPosition = this.FLWheel.Dummy.localPosition;
		localPosition.z = this.FLWheel.Dummy.parent.InverseTransformPoint(position).z - num5;
		this.FLWheel.Dummy.localPosition = localPosition;
		this.FLWheel.LowerWishbone.LookAt(this.FLWheel.Dummy, base.transform.forward);
		this.FLWheel.Knuckle.position = this.FLWheel.KnucklePos.position;
		this.FLWheel.UpperWishbone.LookAt(this.FLWheel.UpperWishboneTarget, base.transform.up);
		this.FLWheel.Deviation = this.FLWheel.DefBrakeDiskPosition.x - this.FLWheel.FrameBone.InverseTransformPoint(this.FLWheel.BrakeDisk.position).x;
		num = Vector3.Distance(this.FRWheel.Knuckle.position, this.FRWheel.UpperWishbone.position);
		num2 = Vector3.Distance(this.FRWheel.Knuckle.position, this.FRWheel.UpperWishboneTarget.position);
		number = 1.95f * num2;
		f = (this.Squared(num2) + this.Squared(num) - this.Squared(number)) / (2f * num2 * num);
		num3 = Mathf.Acos(f) * 57.29578f;
		num4 = -(this.FRWheel.LowerWishbone.localEulerAngles.x - (90f - Vector3.Angle(-this.FRWheel.LowerWishbone.forward, this.FRWheel.UpperWishbone.position - this.FRWheel.Knuckle.position) - num3));
		if (!float.IsNaN(num4) && !float.IsNaN(SteerAngle))
		{
			this.FRWheel.Knuckle.localEulerAngles = new Vector3(0f, -num4, SteerAngle);
		}
		this.FRWheel.TieRodEndBone.LookAt(this.FRWheel.TieRodStartBone, base.transform.forward);
		this.FRWheel.TieRodStartBone.LookAt(this.FRWheel.TieRodEndBone, base.transform.forward);
		num5 = ((this.FRWheel.LowerWishbone.localEulerAngles.x <= 20f || this.FRWheel.LowerWishbone.localEulerAngles.x >= 90f) ? 0f : ((this.FRWheel.LowerWishbone.localEulerAngles.x - 20f) * this.FRWheel.HeightCorrectionRatio));
		position = this.Raycasters[1].position - this.Raycasters[1].up * (this.Controls.Travel.FloatValue + 0.2f);
		if (Physics.Raycast(this.Raycasters[1].position, -this.Raycasters[1].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		localPosition = this.FRWheel.Dummy.localPosition;
		localPosition.z = this.FRWheel.Dummy.parent.InverseTransformPoint(position).z - num5;
		this.FRWheel.Dummy.localPosition = localPosition;
		this.FRWheel.LowerWishbone.LookAt(this.FRWheel.Dummy, base.transform.forward);
		this.FRWheel.Knuckle.position = this.FRWheel.KnucklePos.position;
		this.FRWheel.UpperWishbone.LookAt(this.FRWheel.UpperWishboneTarget, base.transform.forward);
		this.FRWheel.Deviation = -this.FRWheel.DefBrakeDiskPosition.x + this.FRWheel.FrameBone.InverseTransformPoint(this.FRWheel.BrakeDisk.position).x;
		this.DoWheelsOffset();
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
		float perFrameRotation = this.wheelColliders[0].wheelCollider.perFrameRotation;
		this.FLWheel.BrakeDisk.Rotate(this.FLWheel.BrakeDisk.right, perFrameRotation, Space.World);
		float num2 = Vector3.Distance(this.FLWheel.Knuckle.position, this.FLWheel.UpperWishbone.position);
		float num3 = Vector3.Distance(this.FLWheel.Knuckle.position, this.FLWheel.UpperWishboneTarget.position);
		float number = 1.95f * num3;
		float f = (this.Squared(num3) + this.Squared(num2) - this.Squared(number)) / (2f * num3 * num2);
		float num4 = Mathf.Acos(f) * 57.29578f;
		float num5 = -(this.FLWheel.LowerWishbone.localEulerAngles.x - (90f - Vector3.Angle(-this.FLWheel.LowerWishbone.forward, this.FLWheel.UpperWishbone.position - this.FLWheel.Knuckle.position) - num4));
		this.FLWheel.Knuckle.localEulerAngles = new Vector3(0f, num5, num);
		this.FLWheel.TieRodEndBone.LookAt(this.FLWheel.TieRodStartBone, base.transform.forward);
		this.FLWheel.TieRodStartBone.LookAt(this.FLWheel.TieRodEndBone, base.transform.forward);
		float num6 = (this.FLWheel.LowerWishbone.localEulerAngles.x <= 20f || this.FLWheel.LowerWishbone.localEulerAngles.x >= 90f) ? 0f : ((this.FLWheel.LowerWishbone.localEulerAngles.x - 20f) * this.FLWheel.HeightCorrectionRatio);
		Vector3 localPosition = this.FLWheel.Dummy.localPosition;
		localPosition.z = this.FLWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[0].GetVisualWheelPosition()).z - num6;
		this.FLWheel.Dummy.localPosition = localPosition;
		this.FLWheel.LowerWishbone.LookAt(this.FLWheel.Dummy, base.transform.forward);
		this.FLWheel.Knuckle.position = this.FLWheel.KnucklePos.position;
		this.FLWheel.UpperWishbone.LookAt(this.FLWheel.UpperWishboneTarget, base.transform.up);
		this.FLWheel.Deviation = this.FLWheel.DefBrakeDiskPosition.x - this.FLWheel.FrameBone.InverseTransformPoint(this.FLWheel.BrakeDisk.position).x;
		perFrameRotation = this.wheelColliders[1].wheelCollider.perFrameRotation;
		this.FRWheel.BrakeDisk.Rotate(this.FRWheel.BrakeDisk.right, perFrameRotation, Space.World);
		num2 = Vector3.Distance(this.FRWheel.Knuckle.position, this.FRWheel.UpperWishbone.position);
		num3 = Vector3.Distance(this.FRWheel.Knuckle.position, this.FRWheel.UpperWishboneTarget.position);
		number = 1.95f * num3;
		f = (this.Squared(num3) + this.Squared(num2) - this.Squared(number)) / (2f * num3 * num2);
		num4 = Mathf.Acos(f) * 57.29578f;
		num5 = -(this.FRWheel.LowerWishbone.localEulerAngles.x - (90f - Vector3.Angle(-this.FRWheel.LowerWishbone.forward, this.FRWheel.UpperWishbone.position - this.FRWheel.Knuckle.position) - num4));
		this.FRWheel.Knuckle.localEulerAngles = new Vector3(0f, -num5, num);
		this.FRWheel.TieRodEndBone.LookAt(this.FRWheel.TieRodStartBone, base.transform.forward);
		this.FRWheel.TieRodStartBone.LookAt(this.FRWheel.TieRodEndBone, base.transform.forward);
		num6 = ((this.FRWheel.LowerWishbone.localEulerAngles.x <= 20f || this.FRWheel.LowerWishbone.localEulerAngles.x >= 90f) ? 0f : ((this.FRWheel.LowerWishbone.localEulerAngles.x - 20f) * this.FRWheel.HeightCorrectionRatio));
		localPosition = this.FRWheel.Dummy.localPosition;
		localPosition.z = this.FRWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[1].GetVisualWheelPosition()).z - num6;
		this.FRWheel.Dummy.localPosition = localPosition;
		this.FRWheel.LowerWishbone.LookAt(this.FRWheel.Dummy, base.transform.forward);
		this.FRWheel.Knuckle.position = this.FRWheel.KnucklePos.position;
		this.FRWheel.UpperWishbone.LookAt(this.FRWheel.UpperWishboneTarget, base.transform.forward);
		this.FRWheel.Deviation = -this.FRWheel.DefBrakeDiskPosition.x + this.FRWheel.FrameBone.InverseTransformPoint(this.FRWheel.BrakeDisk.position).x;
		if (this.carController != null)
		{
			this.SteeringRack.localPosition = new Vector3(Mathf.Lerp(-0.015f, 0.015f, (num + this.carController.maxSteeringAngle) / (this.carController.maxSteeringAngle * 2f)), 0f, 0f);
		}
		this.DoWheelsOffset();
		this.DoShocks();
	}

	private CarController carController;

	public TrophyFrontWheel FLWheel;

	public TrophyFrontWheel FRWheel;

	public Transform SteeringRack;

	public TrophyFrontSuspensionControls Controls;

	private bool NoWheelColliders;

	private Vector3 previousPosition;

	private float previousUpdateTime;
}
