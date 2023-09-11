using System;
using CustomVP;
using UnityEngine;

public class AssetRearSuspension : Suspension
{
	public override SuspensionValue[] GetControlValues()
	{
		return new SuspensionValue[]
		{
			this.Controls.AxisWidth,
			this.Controls.Damping,
			this.Controls.MiddleFrameWidth,
			this.Controls.RearSteering,
			this.Controls.ShocksGroup,
			this.Controls.ShocksHeight,
			this.Controls.ShocksOffset,
			this.Controls.ShocksSize,
			this.Controls.Stiffness,
			this.Controls.Travel,
			this.Controls.BrakeType,
			this.Controls.AxleType,
			this.Controls.ShowArms
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

	private void Awake()
	{
		this.carController = base.GetComponentInParent<CarController>();
	}

	public override void OnValidate()
	{
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		this.side = Side.Rear;
		this.DoWidth();
		this.DoWheelColliderParameters();
		this.DoShocksOffset();
		this.DoControlArmsHiding();
		this.ChangeShocks();
		this.DoShocks();
		this.SwitchModels();
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

	private void SwitchModels()
	{
		this.RegularAxle.gameObject.SetActive(this.Controls.AxleType.IntValue == 0);
		this.RockwellAxle.gameObject.SetActive(this.Controls.AxleType.IntValue == 1);
		this.RegularBrake.gameObject.SetActive(this.Controls.BrakeType.IntValue == 0);
		this.PinionBrake.gameObject.SetActive(this.Controls.BrakeType.IntValue == 1);
		this.DriveshaftEnd.position = ((this.Controls.AxleType.IntValue != 0) ? this.DrivehsaftStartRockwellAxlePos.position : this.DriveshaftStartRegularAxlePos.position);
	}

	private void DoControlArmsHiding()
	{
		this.ControlArms.gameObject.SetActive(this.Controls.ShowArms.IntValue == 1);
	}

	private void DoShocks()
	{
		this.RRWheel.ShockUps.LookAt(this.RRWheel.ShockDowns, base.transform.up);
		this.RRWheel.ShockDowns.LookAt(this.RRWheel.ShockUps, base.transform.up);
		this.RLWheel.ShockUps.LookAt(this.RLWheel.ShockDowns, base.transform.up);
		this.RLWheel.ShockDowns.LookAt(this.RLWheel.ShockUps, base.transform.up);
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

	private void DoShocksOffset()
	{
		this.RLWheel.ShockUps.localPosition = new Vector3(this.Controls.ShocksOffset.FloatValue, 0f, -this.Controls.ShocksHeight.FloatValue);
		this.RRWheel.ShockUps.localPosition = new Vector3(-this.Controls.ShocksOffset.FloatValue, 0f, -this.Controls.ShocksHeight.FloatValue);
	}

	private void DoWidth()
	{
		if (this.wheelColliders[0] == null || this.wheelColliders[1] == null)
		{
			return;
		}
		this.RLWheel.Axle.localPosition = new Vector3(-this.Controls.AxisWidth.FloatValue, 0f, 0f);
		this.RRWheel.Axle.localPosition = new Vector3(this.Controls.AxisWidth.FloatValue, 0f, 0f);
		this.RLWheel.WheelColliderHolder.localPosition = new Vector3(-this.Controls.AxisWidth.FloatValue * base.transform.lossyScale.x, 0f, 0f);
		this.RRWheel.WheelColliderHolder.localPosition = new Vector3(this.Controls.AxisWidth.FloatValue * base.transform.lossyScale.x, 0f, 0f);
		this.MiddleFrameL.localPosition = new Vector3(this.Controls.MiddleFrameWidth.FloatValue, 0f, 0f);
		this.MiddleFrameR.localPosition = new Vector3(-this.Controls.MiddleFrameWidth.FloatValue, 0f, 0f);
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
		this.RLWheel.BrakeDisk.Rotate(this.RLWheel.BrakeDisk.right, rpm, Space.World);
		this.RRWheel.BrakeDisk.Rotate(this.RRWheel.BrakeDisk.right, rpm, Space.World);
		Vector3 position = this.Raycasters[0].position - this.Raycasters[0].up * (this.Controls.Travel.FloatValue + 0.2f);
		RaycastHit raycastHit;
		if (Physics.Raycast(this.Raycasters[0].position, -this.Raycasters[0].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		this.RLWheel.SteeringAxis.localEulerAngles = new Vector3(0f, -SteerAngle * this.Controls.RearSteering.FloatValue, 0f);
		Vector3 localPosition = this.RLWheel.Dummy.localPosition;
		localPosition.z = this.RLWheel.Dummy.parent.InverseTransformPoint(position).z;
		this.RLWheel.Dummy.localPosition = localPosition;
		this.RLWheel.ControlArmEnd.LookAt(this.RLWheel.ControlArmStart, base.transform.forward);
		this.RLWheel.ControlArmStart.LookAt(this.RLWheel.ControlArmEnd, base.transform.forward);
		position = this.Raycasters[1].position - this.Raycasters[1].up * (this.Controls.Travel.FloatValue + 0.2f);
		if (Physics.Raycast(this.Raycasters[1].position, -this.Raycasters[1].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		this.RRWheel.SteeringAxis.localEulerAngles = new Vector3(0f, -SteerAngle * this.Controls.RearSteering.FloatValue, 0f);
		localPosition = this.RRWheel.Dummy.localPosition;
		localPosition.z = this.RRWheel.Dummy.parent.InverseTransformPoint(position).z;
		localPosition.x = this.RRWheel.Dummy.parent.InverseTransformPoint(position).x;
		this.RRWheel.Dummy.localPosition = localPosition;
		localPosition = this.RearAxleDummy.localPosition;
		localPosition.z = this.RLWheel.Dummy.localPosition.z;
		this.RearAxleDummy.localPosition = localPosition;
		this.RearAxleDummy.LookAt(this.RRWheel.Dummy, base.transform.forward);
		this.DriveshaftStart.Rotate(new Vector3(0f, 0f, rpm));
		this.DriveshaftEnd.Rotate(new Vector3(0f, 0f, rpm));
		this.PinionShaft.Rotate(new Vector3(rpm, 0f, 0f));
		this.MiddleDriveshaft.Rotate(new Vector3(0f, 0f, -rpm));
		this.RRWheel.ControlArmEnd.LookAt(this.RRWheel.ControlArmStart, base.transform.forward);
		this.RRWheel.ControlArmStart.LookAt(this.RRWheel.ControlArmEnd, base.transform.forward);
		this.TrackBarEnd.LookAt(this.TrackBarStart, base.transform.forward);
		this.TrackBarStart.LookAt(this.TrackBarEnd, base.transform.forward);
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
		this.RLWheel.SteeringAxis.localEulerAngles = new Vector3(0f, -num * this.Controls.RearSteering.FloatValue, 0f);
		Vector3 localPosition = this.RLWheel.Dummy.localPosition;
		localPosition.z = this.RLWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[0].VisualWheel.position).z;
		float num2 = this.Controls.AxisWidth.FloatValue + 0.03f;
		Vector3 from = -this.RLWheel.Dummy.right;
		Vector3 to = this.wheelColliders[1].GetVisualWheelPosition() - this.wheelColliders[0].GetVisualWheelPosition();
		float num3 = Vector3.SignedAngle(from, to, this.wheelColliders[0].transform.forward);
		float num4 = num2 * Mathf.Tan(num3 * 0.0174532924f);
		localPosition.z += num4;
		this.RLWheel.Dummy.localPosition = localPosition;
		this.RLWheel.ControlArmEnd.LookAt(this.RLWheel.ControlArmStart, base.transform.forward);
		this.RLWheel.ControlArmStart.LookAt(this.RLWheel.ControlArmEnd, base.transform.forward);
		perFrameRotation = this.wheelColliders[1].wheelCollider.perFrameRotation;
		this.RRWheel.BrakeDisk.Rotate(this.RRWheel.BrakeDisk.right, perFrameRotation, Space.World);
		this.RRWheel.SteeringAxis.localEulerAngles = new Vector3(0f, -num * this.Controls.RearSteering.FloatValue, 0f);
		localPosition = this.RRWheel.Dummy.localPosition;
		localPosition.z = this.RRWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[1].VisualWheel.position).z;
		localPosition.x = this.RRWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[1].VisualWheel.position).x;
		this.RRWheel.Dummy.localPosition = localPosition;
		localPosition = this.RearAxleDummy.localPosition;
		localPosition.z = this.RLWheel.Dummy.localPosition.z;
		this.RearAxleDummy.localPosition = localPosition;
		this.RearAxleDummy.LookAt(this.RRWheel.Dummy, base.transform.forward);
		this.DriveshaftStart.Rotate(new Vector3(0f, 0f, perFrameRotation));
		this.DriveshaftEnd.Rotate(new Vector3(0f, 0f, perFrameRotation));
		this.PinionShaft.Rotate(new Vector3(perFrameRotation, 0f, 0f));
		this.MiddleDriveshaft.Rotate(new Vector3(0f, 0f, -perFrameRotation));
		this.RRWheel.ControlArmEnd.LookAt(this.RRWheel.ControlArmStart, base.transform.forward);
		this.RRWheel.ControlArmStart.LookAt(this.RRWheel.ControlArmEnd, base.transform.forward);
		this.TrackBarEnd.LookAt(this.TrackBarStart, base.transform.forward);
		this.TrackBarStart.LookAt(this.TrackBarEnd, base.transform.forward);
		this.DoShocks();
	}

	private CarController carController;

	public AssetRearWheel RLWheel;

	public AssetRearWheel RRWheel;

	public Transform DriveshaftStart;

	public Transform DriveshaftEnd;

	public Transform PinionShaft;

	public Transform DriveshaftStartRegularAxlePos;

	public Transform DrivehsaftStartRockwellAxlePos;

	public Transform RegularAxle;

	public Transform RockwellAxle;

	public Transform RegularBrake;

	public Transform PinionBrake;

	public Transform MiddleDriveshaft;

	public Transform MiddleFrameL;

	public Transform MiddleFrameR;

	public Transform RearAxleDummy;

	public Transform ControlArms;

	public Transform TrackBarStart;

	public Transform TrackBarEnd;

	public AssetRearSuspensionControls Controls;

	private bool NoWheelColliders;
}
