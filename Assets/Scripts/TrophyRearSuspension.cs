using System;
using CustomVP;
using UnityEngine;

public class TrophyRearSuspension : Suspension
{
	public override SuspensionValue[] GetControlValues()
	{
		return new SuspensionValue[]
		{
			this.Controls.AxisWidth,
			this.Controls.Damping,
			this.Controls.RearSteering,
			this.Controls.ShocksGroup,
			this.Controls.ShocksOffset,
			this.Controls.ShocksSize,
			this.Controls.ShocksTravel,
			this.Controls.Stiffness,
			this.Controls.TrailingArmsHeight,
			this.Controls.TrailingArmsOffset,
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
		this.side = Side.Rear;
		this.DoShocksOffset();
		this.DoTrailingArmsOffset();
		this.DoWheelColliderParameters();
		this.DoWidth();
		this.DoShocks();
		this.ChangeShocks();
		if (this.carController != null)
		{
			this.carController.InverseSteerMultiplier = this.Controls.RearSteering.FloatValue;
		}
		if (this.wheelColliders[0] == null || this.wheelColliders[1] == null)
		{
			return;
		}
		this.wheelColliders[0].OnValidate();
		this.wheelColliders[1].OnValidate();
	}

	private void Awake()
	{
		this.carController = base.GetComponentInParent<CarController>();
		this.RearAxleDummyDefPos = this.RearAxleDummy.localPosition;
		this.RRWheel.DummyDefPos = this.RRWheel.Dummy.localPosition;
	}

	private void DoShocks()
	{
		if (this.RRWheel.Shocks == null || this.RRWheel.ShockDowns == null || this.RLWheel.ShockUps == null || this.RLWheel.ShockDowns == null)
		{
			return;
		}
		this.RRWheel.ShockUps.LookAt(this.RRWheel.ShockDowns, base.transform.up);
		this.RRWheel.ShockDowns.LookAt(this.RRWheel.ShockUps, -this.RRWheel.TrailingArm.forward);
		this.RLWheel.ShockUps.LookAt(this.RLWheel.ShockDowns, base.transform.up);
		this.RLWheel.ShockDowns.LookAt(this.RLWheel.ShockUps, -this.RLWheel.TrailingArm.forward);
		this.RLWheel.ShockUps.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.RLWheel.ShockDowns.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.RRWheel.ShockUps.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.RRWheel.ShockDowns.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
	}

	private void ChangeShocks()
	{
		for (int i = 0; i < this.RRWheel.Shocks.Length; i++)
		{
			this.RLWheel.Shocks[i].gameObject.SetActive(i == this.Controls.ShocksGroup.IntValue);
			this.RRWheel.Shocks[i].gameObject.SetActive(i == this.Controls.ShocksGroup.IntValue);
		}
	}

	private void DoWidth()
	{
		if (this.wheelColliders[0] == null || this.wheelColliders[1] == null)
		{
			return;
		}
		this.RearAxleLeft.localPosition = new Vector3(-this.Controls.AxisWidth.FloatValue, 0f, 0f);
		this.RearAxleRight.localPosition = new Vector3(this.Controls.AxisWidth.FloatValue, 0f, 0f);
		this.RLWheel.WheelColliderHolder.localPosition = new Vector3(-this.Controls.AxisWidth.FloatValue * base.transform.lossyScale.x, 0f, this.RLWheel.Deviation * base.transform.lossyScale.x);
		this.RRWheel.WheelColliderHolder.localPosition = new Vector3(this.Controls.AxisWidth.FloatValue * base.transform.lossyScale.x, 0f, this.RRWheel.Deviation * base.transform.lossyScale.x);
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

	private void DoShocksOffset()
	{
		this.RLWheel.ShockUps.localPosition = new Vector3(this.Controls.ShocksOffset.FloatValue, 0f, -this.Controls.ShocksTravel.FloatValue);
		this.RRWheel.ShockUps.localPosition = new Vector3(-this.Controls.ShocksOffset.FloatValue, 0f, -this.Controls.ShocksTravel.FloatValue);
	}

	private void DoTrailingArmsOffset()
	{
		this.RLWheel.TrailingArmMount.localPosition = new Vector3(this.Controls.TrailingArmsOffset.FloatValue, this.Controls.TrailingArmsHeight.FloatValue, 0f);
		this.RRWheel.TrailingArmMount.localPosition = new Vector3(-this.Controls.TrailingArmsOffset.FloatValue, this.Controls.TrailingArmsHeight.FloatValue, 0f);
	}

	public override void UpdateSuspension(float SteerAngle, float WheelRadius, float rpm)
	{
		this.RLWheel.BrakeDisk.Rotate(this.RLWheel.BrakeDisk.right, rpm, Space.World);
		this.RRWheel.BrakeDisk.Rotate(this.RRWheel.BrakeDisk.right, rpm, Space.World);
		this.RLWheel.SteeringAxle.localEulerAngles = new Vector3(0f, -SteerAngle * this.Controls.RearSteering.FloatValue, 0f);
		this.RLWheel.TrackBarEndBone.LookAt(this.RLWheel.TrackBarStartBone, base.transform.forward);
		this.RLWheel.TrackBarStartBone.LookAt(this.RLWheel.TrackBarEndBone, base.transform.forward);
		Vector3 position = this.Raycasters[0].position - this.Raycasters[0].up * (this.Controls.Travel.FloatValue + 0.2f);
		RaycastHit raycastHit;
		if (Physics.Raycast(this.Raycasters[0].position, -this.Raycasters[0].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		Vector3 localPosition = this.RLWheel.Dummy.localPosition;
		localPosition.z = this.RLWheel.Dummy.parent.InverseTransformPoint(position).z;
		localPosition.x = this.RLWheel.Dummy.parent.InverseTransformPoint(position).x;
		this.RLWheel.Dummy.localPosition = localPosition;
		this.RRWheel.SteeringAxle.localEulerAngles = new Vector3(0f, -SteerAngle * this.Controls.RearSteering.FloatValue, 0f);
		this.RRWheel.TrackBarEndBone.LookAt(this.RRWheel.TrackBarStartBone, base.transform.forward);
		this.RRWheel.TrackBarStartBone.LookAt(this.RRWheel.TrackBarEndBone, base.transform.forward);
		position = this.Raycasters[1].position - this.Raycasters[1].up * (this.Controls.Travel.FloatValue + 0.2f);
		if (Physics.Raycast(this.Raycasters[1].position, -this.Raycasters[1].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		localPosition = this.RRWheel.Helper.localPosition;
		localPosition.z = this.RRWheel.Dummy.parent.InverseTransformPoint(position).z;
		localPosition.x = this.RRWheel.Dummy.parent.InverseTransformPoint(position).x;
		this.RRWheel.Helper.localPosition = localPosition;
		this.RRWheel.Dummy.localPosition = this.RRWheel.Helper.localPosition;
		localPosition = this.RLWheel.Helper.localPosition;
		localPosition.z = this.RLWheel.Dummy.localPosition.z;
		this.RLWheel.Deviation = 1.25f - Mathf.Cos(Vector3.Angle(this.RLWheel.TrailingArm.position - this.RLWheel.TrailingArmTarget.position, -base.transform.up) * 0.0174532924f) * 1.25f;
		this.RLWheel.Deviation /= base.transform.localScale.x;
		localPosition.y = this.RearAxleDummyDefPos.y - this.RLWheel.Deviation;
		this.RLWheel.Helper.localPosition = localPosition;
		this.RearAxleDummy.localPosition = this.RLWheel.Helper.localPosition;
		localPosition = this.Raycasters[0].transform.localPosition;
		localPosition.z = this.RLWheel.Deviation * base.transform.lossyScale.x;
		this.Raycasters[0].transform.localPosition = localPosition;
		localPosition = this.RRWheel.Dummy.localPosition;
		this.RRWheel.Deviation = 1.25f - Mathf.Cos(Vector3.Angle(this.RRWheel.TrailingArm.position - this.RRWheel.TrailingArmTarget.position, -base.transform.up) * 0.0174532924f) * 1.25f;
		this.RRWheel.Deviation /= base.transform.localScale.x;
		localPosition.y = this.RRWheel.DummyDefPos.y - this.RRWheel.Deviation;
		this.RRWheel.Dummy.localPosition = localPosition;
		this.RearAxleDummy.LookAt(this.RRWheel.Dummy, base.transform.up);
		localPosition = this.Raycasters[1].transform.localPosition;
		localPosition.z = this.RRWheel.Deviation * base.transform.lossyScale.x;
		this.Raycasters[1].transform.localPosition = localPosition;
		this.RLWheel.TrailingArm.LookAt(this.RLWheel.TrailingArmTarget, base.transform.forward);
		this.RRWheel.TrailingArm.LookAt(this.RRWheel.TrailingArmTarget, base.transform.forward);
		this.DriveshaftStart.Rotate(0f, 0f, -rpm);
		this.Driveshaft.LookAt(this.DriveshaftTarget, this.DriveshaftStart.up);
		this.DoWidth();
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
		this.RLWheel.BrakeDisk.Rotate(this.RLWheel.BrakeDisk.right, perFrameRotation, Space.World);
		this.RLWheel.SteeringAxle.localEulerAngles = new Vector3(0f, -num * this.Controls.RearSteering.FloatValue, 0f);
		this.RLWheel.TrackBarEndBone.LookAt(this.RLWheel.TrackBarStartBone, base.transform.forward);
		this.RLWheel.TrackBarStartBone.LookAt(this.RLWheel.TrackBarEndBone, base.transform.forward);
		Vector3 localPosition = this.RLWheel.Dummy.localPosition;
		localPosition.z = this.RLWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[0].GetVisualWheelPosition()).z;
		localPosition.x = this.RLWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[0].GetVisualWheelPosition()).x;
		this.RLWheel.Dummy.localPosition = localPosition;
		perFrameRotation = this.wheelColliders[1].wheelCollider.perFrameRotation;
		this.RRWheel.BrakeDisk.Rotate(this.RRWheel.BrakeDisk.right, perFrameRotation, Space.World);
		this.RRWheel.SteeringAxle.localEulerAngles = new Vector3(0f, -num * this.Controls.RearSteering.FloatValue, 0f);
		this.RRWheel.TrackBarEndBone.LookAt(this.RRWheel.TrackBarStartBone, base.transform.forward);
		this.RRWheel.TrackBarStartBone.LookAt(this.RRWheel.TrackBarEndBone, base.transform.forward);
		localPosition = this.RRWheel.Helper.localPosition;
		localPosition.z = this.RRWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[1].GetVisualWheelPosition()).z;
		localPosition.x = this.RRWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[1].GetVisualWheelPosition()).x;
		this.RRWheel.Helper.localPosition = localPosition;
		this.RRWheel.Dummy.localPosition = this.RRWheel.Helper.localPosition;
		UnityEngine.Debug.DrawRay(this.RRWheel.Dummy.parent.TransformPoint(this.RRWheel.DummyDefPos), Vector3.up, Color.cyan);
		UnityEngine.Debug.DrawRay(this.RLWheel.Dummy.parent.TransformPoint(this.RLWheel.DummyDefPos), Vector3.up, Color.cyan);
		this.Driveshaft.LookAt(this.DriveshaftTarget, this.DriveshaftStart.up);
		this.DriveshaftStart.Rotate(0f, 0f, perFrameRotation);
		localPosition = this.RLWheel.Helper.localPosition;
		localPosition.z = this.RLWheel.Dummy.localPosition.z;
		this.RLWheel.Deviation = 1.25f - Mathf.Cos(Vector3.Angle(this.RLWheel.TrailingArm.position - this.RLWheel.TrailingArmTarget.position, -base.transform.up) * 0.0174532924f) * 1.25f;
		this.RLWheel.Deviation /= base.transform.localScale.x;
		localPosition.y = this.RearAxleDummyDefPos.y - this.RLWheel.Deviation;
		this.RLWheel.Helper.localPosition = localPosition;
		this.RearAxleDummy.localPosition = this.RLWheel.Helper.localPosition;
		localPosition = this.wheelColliders[0].transform.localPosition;
		localPosition.z = this.RLWheel.Deviation * base.transform.lossyScale.x;
		this.wheelColliders[0].transform.localPosition = localPosition;
		localPosition = this.RRWheel.Dummy.localPosition;
		this.RRWheel.Deviation = 1.25f - Mathf.Cos(Vector3.Angle(this.RRWheel.TrailingArm.position - this.RRWheel.TrailingArmTarget.position, -base.transform.up) * 0.0174532924f) * 1.25f;
		this.RRWheel.Deviation /= base.transform.localScale.x;
		localPosition.y = this.RRWheel.DummyDefPos.y - this.RRWheel.Deviation;
		this.RRWheel.Dummy.localPosition = localPosition;
		this.RearAxleDummy.LookAt(this.RRWheel.Dummy, base.transform.up);
		localPosition = this.wheelColliders[1].transform.localPosition;
		localPosition.z = this.RRWheel.Deviation * base.transform.lossyScale.x;
		this.wheelColliders[1].transform.localPosition = localPosition;
		this.RLWheel.TrailingArm.LookAt(this.RLWheel.TrailingArmTarget, base.transform.forward);
		this.RRWheel.TrailingArm.LookAt(this.RRWheel.TrailingArmTarget, base.transform.forward);
		if (this.RearAxleDummy.localPosition.y < 0f)
		{
			localPosition = this.RearAxleDummy.localPosition;
			localPosition.y = -localPosition.y;
			this.RearAxleDummy.localPosition = localPosition;
		}
		this.DoWidth();
		this.DoShocks();
	}

	private CarController carController;

	private Vector3 RearAxleDummyDefPos;

	public RearTrophyWheel RLWheel;

	public RearTrophyWheel RRWheel;

	public Transform RearAxleDummy;

	public Transform RearAxleLeft;

	public Transform RearAxleRight;

	public Transform Driveshaft;

	public Transform DriveshaftStart;

	public Transform DriveshaftTarget;

	public RearTrophySuspensionControls Controls;

	private bool NoWheelColliders;

	private Vector3 previousPosition;

	private float previousUpdateTime;
}
