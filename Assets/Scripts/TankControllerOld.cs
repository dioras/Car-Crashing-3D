using System;
using CustomVP;
using UnityEngine;

public class TankControllerOld : MonoBehaviour
{
	private void Start()
	{
		base.GetComponent<Rigidbody>().centerOfMass = base.transform.InverseTransformPoint(this.COM.position);
		this._leftmaterial = this.leftTracks.materials[0];
		this._rightmaterial = this.rightTracks.materials[0];
	}

	private void Update()
	{
		this.xInput = UnityEngine.Input.GetAxis("Horizontal");
		this.yInput = UnityEngine.Input.GetAxis("Vertical");
		if (this.xInput != 0f)
		{
			this.yInput = 0f;
		}
		this.smoothYinput = Mathf.MoveTowards(this.smoothYinput, this.yInput, Time.deltaTime);
		if (Mathf.Abs(this.yInput) < Mathf.Abs(this.smoothYinput))
		{
			this.smoothYinput = this.yInput;
		}
		float num = this.torque * (this.smoothYinput + this.xInput * 2f);
		num = Mathf.Clamp(num, -this.torque, this.torque);
		float num2 = this.torque * (this.smoothYinput - this.xInput * 2f);
		num2 = Mathf.Clamp(num2, -this.torque, this.torque);
		foreach (WheelComponent wheelComponent in this.LeftWheelColliders)
		{
			wheelComponent.MotorTorque = num;
			if (num == 0f || this.xInput != 0f)
			{
				wheelComponent.BrakeTorque = this.RollingStopTorque;
			}
			else
			{
				wheelComponent.BrakeTorque = 0f;
			}
			wheelComponent.wheelCollider.rpmLimit = 30f;
		}
		foreach (WheelComponent wheelComponent2 in this.RightWheelColliders)
		{
			wheelComponent2.MotorTorque = num2;
			if (num2 == 0f || this.xInput != 0f)
			{
				wheelComponent2.BrakeTorque = this.RollingStopTorque;
			}
			else
			{
				wheelComponent2.BrakeTorque = 0f;
			}
			wheelComponent2.wheelCollider.rpmLimit = 30f;
		}
		this.FLDriveWheel.Rotate(-this.LeftWheelColliders[2].rpm * 0.05f, 0f, 0f);
		this.RLDriveWheel.Rotate(-this.LeftWheelColliders[2].rpm * 0.05f, 0f, 0f);
		for (int k = 0; k < this.LeftWheels.Length; k++)
		{
			this.LeftWheels[k].Rotate(-this.LeftWheelColliders[2].rpm * 0.05f, 0f, 0f);
		}
		this.FRDriveWheel.Rotate(-this.RightWheelColliders[2].rpm * 0.05f, 0f, 0f);
		this.RRDriveWheel.Rotate(-this.RightWheelColliders[2].rpm * 0.05f, 0f, 0f);
		for (int l = 0; l < this.RightWheels.Length; l++)
		{
			this.RightWheels[l].Rotate(-this.RightWheelColliders[2].rpm * 0.05f, 0f, 0f);
		}
		for (int m = 0; m < this.LeftTrackBones.Length; m++)
		{
			Vector3 localPosition = this.LeftTrackBones[m].localPosition;
			localPosition.z = this.LeftTrackBones[m].parent.InverseTransformPoint(this.LeftWheelColliders[m].GetVisualWheelPosition()).z;
			this.LeftTrackBones[m].localPosition = localPosition;
		}
		for (int n = 0; n < this.LeftWheels.Length; n++)
		{
			Vector3 localPosition2 = this.LeftWheels[n].localPosition;
			localPosition2.z = this.LeftWheels[n].parent.InverseTransformPoint(this.LeftWheelColliders[n].GetVisualWheelPosition()).z;
			this.LeftWheels[n].localPosition = localPosition2;
		}
		for (int num3 = 0; num3 < this.RightTrackBones.Length; num3++)
		{
			Vector3 localPosition3 = this.RightTrackBones[num3].localPosition;
			localPosition3.z = this.RightTrackBones[num3].parent.InverseTransformPoint(this.RightWheelColliders[num3].GetVisualWheelPosition()).z;
			this.RightTrackBones[num3].localPosition = localPosition3;
		}
		for (int num4 = 0; num4 < this.RightWheels.Length; num4++)
		{
			Vector3 localPosition4 = this.RightWheels[num4].localPosition;
			localPosition4.z = this.RightWheels[num4].parent.InverseTransformPoint(this.RightWheelColliders[num4].GetVisualWheelPosition()).z;
			this.RightWheels[num4].localPosition = localPosition4;
		}
		this._leftmaterial.mainTextureOffset += new Vector2(0f, this.LeftWheelColliders[2].rpm * 0.0002f);
		this._rightmaterial.mainTextureOffset += new Vector2(0f, this.RightWheelColliders[2].rpm * 0.0002f);
	}

	public Transform COM;

	public WheelComponent[] LeftWheelColliders;

	public WheelComponent[] RightWheelColliders;

	public Transform[] LeftWheels;

	public Transform[] RightWheels;

	public Transform FLDriveWheel;

	public Transform FRDriveWheel;

	public Transform RLDriveWheel;

	public Transform RRDriveWheel;

	public Transform[] LeftTrackBones;

	public Transform[] RightTrackBones;

	public float torque;

	public float RollingStopTorque;

	private float xInput;

	private float yInput;

	private float smoothYinput;

	public Material TankTracksMaterial;

	public Renderer leftTracks;

	public Renderer rightTracks;

	private Material _leftmaterial;

	private Material _rightmaterial;
}
