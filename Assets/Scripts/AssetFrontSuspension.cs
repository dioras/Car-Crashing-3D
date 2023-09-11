using System;
using CustomVP;
using UnityEngine;

public class AssetFrontSuspension : Suspension
{
	public override SuspensionValue[] GetControlValues()
	{
		return new SuspensionValue[]
		{
			this.Controls.AxisWidth,
			this.Controls.Damping,
			this.Controls.MiddleFrameWidth,
			this.Controls.ShocksGroup,
			this.Controls.ShocksHeight,
			this.Controls.ShocksOffset,
			this.Controls.ShocksSize,
			this.Controls.Stiffness,
			this.Controls.Travel,
			this.Controls.AxleType,
			this.Controls.BrakeType,
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
		this.DoShocksOffset();
		this.DoControlArmsHiding();
		this.ChangeShocks();
		this.DoShocks();
		this.SwitchModels();
		if (this.wheelColliders[0] == null || this.wheelColliders[1] == null)
		{
			return;
		}
		this.wheelColliders[0].OnValidate();
		this.wheelColliders[1].OnValidate();
	}

	private void DoControlArmsHiding()
	{
		this.ControlArms.gameObject.SetActive(this.Controls.ShowArms.IntValue == 1);
	}

	private void SwitchModels()
	{
		this.RegularAxle.gameObject.SetActive(this.Controls.AxleType.IntValue == 0);
		this.RockwellAxle.gameObject.SetActive(this.Controls.AxleType.IntValue == 1);
		this.RegularBrake.gameObject.SetActive(this.Controls.BrakeType.IntValue == 0);
		this.PinionBrake.gameObject.SetActive(this.Controls.BrakeType.IntValue == 1);
		this.DriveshaftEnd.position = ((this.Controls.AxleType.IntValue != 0) ? this.DrivehsaftStartRockwellAxlePos.position : this.DriveshaftStartRegularAxlePos.position);
	}

	private void DoShocks()
	{
		this.FRWheel.ShockUps.LookAt(this.FRWheel.ShockDowns, base.transform.up);
		this.FRWheel.ShockDowns.LookAt(this.FRWheel.ShockUps, base.transform.up);
		this.FLWheel.ShockUps.LookAt(this.FLWheel.ShockDowns, base.transform.up);
		this.FLWheel.ShockDowns.LookAt(this.FLWheel.ShockUps, base.transform.up);
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

	private void DoShocksOffset()
	{
		this.FLWheel.ShockUps.localPosition = new Vector3(this.Controls.ShocksOffset.FloatValue, 0f, -this.Controls.ShocksHeight.FloatValue);
		this.FRWheel.ShockUps.localPosition = new Vector3(-this.Controls.ShocksOffset.FloatValue, 0f, -this.Controls.ShocksHeight.FloatValue);
	}

	private void DoWidth()
	{
		if (this.wheelColliders[0] == null || this.wheelColliders[1] == null)
		{
			return;
		}
		this.FLWheel.Axle.localPosition = new Vector3(this.Controls.AxisWidth.FloatValue, 0f, 0f);
		this.FRWheel.Axle.localPosition = new Vector3(-this.Controls.AxisWidth.FloatValue, 0f, 0f);
		this.MiddleFrameL.localPosition = new Vector3(-this.Controls.MiddleFrameWidth.FloatValue, 0f, 0f);
		this.MiddleFrameR.localPosition = new Vector3(this.Controls.MiddleFrameWidth.FloatValue, 0f, 0f);
		this.FLWheel.WheelColliderHolder.transform.localPosition = new Vector3(-this.Controls.AxisWidth.FloatValue * base.transform.lossyScale.x, 0f, 0f);
		this.FRWheel.WheelColliderHolder.transform.localPosition = new Vector3(this.Controls.AxisWidth.FloatValue * base.transform.lossyScale.x, 0f, 0f);
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
		this.FRWheel.BrakeDisk.Rotate(this.FRWheel.BrakeDisk.forward, -rpm, Space.World);
		this.FLWheel.Joint.Rotate(0f, 0f, -rpm);
		this.FLWheel.Knuckle.localEulerAngles = new Vector3(0f, SteerAngle + 90f, 0f);
		this.FLWheel.ConnectingTieRodBone.LookAt(this.FRWheel.ConnectingTieRodBone, base.transform.forward);
		Vector3 position = this.Raycasters[0].position - this.Raycasters[0].up * (this.Controls.Travel.FloatValue + 0.2f);
		RaycastHit raycastHit;
		if (Physics.Raycast(this.Raycasters[0].position, -this.Raycasters[0].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		Vector3 localPosition = this.FLWheel.Dummy.localPosition;
		localPosition.z = this.FLWheel.Dummy.parent.InverseTransformPoint(position).z;
		this.FLWheel.Dummy.localPosition = localPosition;
		this.FLWheel.ControlArmEnd.LookAt(this.FLWheel.ControlArmStart, base.transform.forward);
		this.FLWheel.ControlArmStart.LookAt(this.FLWheel.ControlArmEnd, base.transform.forward);
		this.FRWheel.Joint.Rotate(0f, 0f, -rpm);
		this.FRWheel.Knuckle.localEulerAngles = new Vector3(0f, SteerAngle + 90f, 0f);
		this.FRWheel.ConnectingTieRodBone.LookAt(this.FLWheel.ConnectingTieRodBone, base.transform.forward);
		position = this.Raycasters[1].position - this.Raycasters[1].up * (this.Controls.Travel.FloatValue + 0.2f);
		if (Physics.Raycast(this.Raycasters[1].position, -this.Raycasters[1].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		localPosition = this.FRWheel.Dummy.localPosition;
		localPosition.z = this.FRWheel.Dummy.parent.InverseTransformPoint(position).z;
		localPosition.x = this.FRWheel.Dummy.parent.InverseTransformPoint(position).x;
		this.FRWheel.Dummy.localPosition = localPosition;
		localPosition = this.FrontAxleDummy.localPosition;
		localPosition.z = this.FLWheel.Dummy.localPosition.z;
		this.FrontAxleDummy.localPosition = localPosition;
		this.FrontAxleDummy.LookAt(this.FRWheel.Dummy, base.transform.forward);
		this.DriveshaftStart.Rotate(new Vector3(0f, 0f, rpm));
		this.DriveshaftEnd.Rotate(new Vector3(0f, 0f, rpm));
		this.PinionShaft.Rotate(new Vector3(rpm, 0f, 0f));
		this.MiddleDriveshaft.Rotate(new Vector3(0f, 0f, -rpm));
		this.FRWheel.ControlArmEnd.LookAt(this.FRWheel.ControlArmStart, base.transform.forward);
		this.FRWheel.ControlArmStart.LookAt(this.FRWheel.ControlArmEnd, base.transform.forward);
		this.TieRodEnd.LookAt(this.TieRodStart, base.transform.forward);
		this.TieRodStart.LookAt(this.TieRodEnd, base.transform.forward);
		this.SteeringRod.localEulerAngles = new Vector3(0f, 0f, -SteerAngle);
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
		this.FLWheel.BrakeDisk.Rotate(this.FLWheel.BrakeDisk.forward, -perFrameRotation, Space.World);
		this.FLWheel.Joint.Rotate(0f, 0f, -perFrameRotation);
		this.FLWheel.Knuckle.localEulerAngles = new Vector3(0f, num + 90f, 0f);
		this.FLWheel.ConnectingTieRodBone.LookAt(this.FRWheel.ConnectingTieRodBone, base.transform.forward);
		Vector3 localPosition = this.FLWheel.Dummy.localPosition;
		localPosition.z = this.FLWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[0].VisualWheel.position).z;
		float num2 = this.Controls.AxisWidth.FloatValue + 0.03f;
		Vector3 from = -this.FLWheel.Dummy.forward;
		Vector3 to = this.wheelColliders[1].GetVisualWheelPosition() - this.wheelColliders[0].GetVisualWheelPosition();
		float num3 = Vector3.SignedAngle(from, to, this.wheelColliders[0].transform.forward);
		float num4 = num2 * Mathf.Tan(num3 * 0.0174532924f);
		localPosition.z += num4;
		this.FLWheel.Dummy.localPosition = localPosition;
		this.FLWheel.ControlArmEnd.LookAt(this.FLWheel.ControlArmStart, base.transform.forward);
		this.FLWheel.ControlArmStart.LookAt(this.FLWheel.ControlArmEnd, base.transform.forward);
		perFrameRotation = this.wheelColliders[1].wheelCollider.perFrameRotation;
		this.FRWheel.BrakeDisk.Rotate(this.FRWheel.BrakeDisk.forward, -perFrameRotation, Space.World);
		this.FRWheel.Joint.Rotate(0f, 0f, -perFrameRotation);
		this.FRWheel.Knuckle.localEulerAngles = new Vector3(0f, num + 90f, 0f);
		this.FRWheel.ConnectingTieRodBone.LookAt(this.FLWheel.ConnectingTieRodBone, base.transform.forward);
		localPosition = this.FRWheel.Dummy.localPosition;
		localPosition.z = this.FRWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[1].VisualWheel.position).z;
		localPosition.x = this.FRWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[1].VisualWheel.position).x;
		this.FRWheel.Dummy.localPosition = localPosition;
		localPosition = this.FrontAxleDummy.localPosition;
		localPosition.z = this.FLWheel.Dummy.localPosition.z;
		this.FrontAxleDummy.localPosition = localPosition;
		this.FrontAxleDummy.LookAt(this.FRWheel.Dummy, base.transform.forward);
		this.DriveshaftStart.Rotate(new Vector3(0f, 0f, perFrameRotation));
		this.DriveshaftEnd.Rotate(new Vector3(0f, 0f, perFrameRotation));
		this.PinionShaft.Rotate(new Vector3(perFrameRotation, 0f, 0f));
		this.MiddleDriveshaft.Rotate(new Vector3(0f, 0f, -perFrameRotation));
		this.FRWheel.ControlArmEnd.LookAt(this.FRWheel.ControlArmStart, base.transform.forward);
		this.FRWheel.ControlArmStart.LookAt(this.FRWheel.ControlArmEnd, base.transform.forward);
		this.TieRodEnd.LookAt(this.TieRodStart, base.transform.forward);
		this.TieRodStart.LookAt(this.TieRodEnd, base.transform.forward);
		this.SteeringRod.localEulerAngles = new Vector3(0f, 0f, -num);
		this.TrackBarEnd.LookAt(this.TrackBarStart, base.transform.forward);
		this.TrackBarStart.LookAt(this.TrackBarEnd, base.transform.forward);
		this.DoShocks();
	}

	private CarController carController;

	public AssetFrontWheel FLWheel;

	public AssetFrontWheel FRWheel;

	public Transform SteeringRod;

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

	public Transform TieRodStart;

	public Transform TieRodEnd;

	public Transform MiddleFrameL;

	public Transform MiddleFrameR;

	public Transform FrontAxleDummy;

	public Transform ControlArms;

	public Transform TrackBarStart;

	public Transform TrackBarEnd;

	[Space(10f)]
	public AssetFrontControls Controls;

	private bool NoWheelColliders;
}
