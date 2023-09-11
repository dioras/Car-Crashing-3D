using System;
using CustomVP;
using UnityEngine;

public class RipsawSuspension : Suspension
{
	public override SuspensionValue[] GetControlValues()
	{
		return new SuspensionValue[]
		{
			this.Controls.Damping,
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
		this.OnValidate();
		this.leftTrackMaterial = this.leftTrackMesh.materials[this.tracksMaterialID];
		this.rightTrackMaterial = this.rightTrackMesh.materials[this.tracksMaterialID];
	}

	public override void OnValidate()
	{
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		this.side = this.SuspensionSide;
	}

	public override void UpdateSuspension(float SteerAngle, float WheelRadius, float rpm)
	{
	}

	private void DoMpSuspension()
	{
		for (int i = 0; i < this.wheels.Length; i++)
		{
			Vector3 a = this.Raycasters[i].position - this.Raycasters[i].up * 0.5f;
			RaycastHit raycastHit;
			if (Physics.Raycast(this.Raycasters[i].position, -this.Raycasters[i].up, out raycastHit) && raycastHit.distance < 0.73f)
			{
				a = raycastHit.point + Vector3.up * 0.23f;
			}
			Vector3 vector = a - this.wheels[i].arm.position;
			Vector3 forward = Vector3.ProjectOnPlane(vector, base.transform.right);
			this.wheels[i].arm.rotation = Quaternion.LookRotation(forward, base.transform.up);
			Vector3 localPosition = this.wheels[i].trackSegment.localPosition;
			localPosition.z = this.wheels[i].trackSegment.parent.InverseTransformPoint(a - base.transform.up * 0.23f).z;
			this.wheels[i].trackSegment.localPosition = localPosition;
		}
		for (int j = 0; j < this.shockUps.Length; j++)
		{
			this.shockUps[j].LookAt(this.shockDowns[j], base.transform.forward);
			this.shockDowns[j].LookAt(this.shockUps[j], base.transform.forward);
		}
		Vector3 vector2 = (base.transform.position - this.lastPos) / Time.deltaTime;
		float z = base.transform.InverseTransformVector(vector2).z;
		this.lastPos = base.transform.position;
		float num = -0.008f * z;
		float num2 = -0.008f * z;
		float num3 = Vector3.SignedAngle(base.transform.forward, this.lastForward, base.transform.up) / Time.deltaTime;
		this.lastForward = base.transform.forward;
		num += 0.0001f * num3;
		num2 -= 0.0001f * num3;
		this.leftTrackOffset += num;
		this.rightTrackOffset += num2;
		if (this.leftTrackOffset <= -3f)
		{
			this.leftTrackOffset += 3f;
		}
		if (this.leftTrackOffset >= 3f)
		{
			this.leftTrackOffset -= 3f;
		}
		if (this.rightTrackOffset <= -3f)
		{
			this.rightTrackOffset += 3f;
		}
		if (this.rightTrackOffset >= 3f)
		{
			this.rightTrackOffset -= 3f;
		}
		this.leftTrackMaterial.SetTextureOffset("_Texture", new Vector2(0f, this.leftTrackOffset));
		this.rightTrackMaterial.SetTextureOffset("_Texture", new Vector2(0f, this.rightTrackOffset));
		this.leftTrackMaterial.SetTextureOffset("_Dirt", new Vector2(0f, this.leftTrackOffset));
		this.rightTrackMaterial.SetTextureOffset("_Dirt", new Vector2(0f, this.rightTrackOffset));
		for (int k = 0; k < this.visualWheels.Length; k++)
		{
			float num4 = (k <= this.visualWheels.Length / 2 - 1) ? num : num2;
			if (!float.IsNaN(500f * num4))
			{
				this.visualWheels[k].Rotate(new Vector3(500f * num4, 0f, 0f), Space.Self);
			}
		}
	}

	private void Update()
	{
		if (this.NoWheelColliders)
		{
			this.DoMpSuspension();
			return;
		}
		float num = 0f;
		int num2 = 0;
		for (int i = 0; i < this.tankWheelColliders.Length / 2; i++)
		{
			if (this.tankWheelColliders[i].grounded)
			{
				num += this.tankWheelColliders[i].perFrameRotation;
				num2++;
			}
		}
		if (num2 > 0)
		{
			num /= (float)num2;
			this.lastLeftTrackRpm = num;
		}
		this.leftTrackOffset += -0.002f * this.lastLeftTrackRpm;
		float num3 = 0f;
		int num4 = 0;
		for (int j = 0; j < this.tankWheelColliders.Length / 2; j++)
		{
			if (this.tankWheelColliders[j + this.tankWheelColliders.Length / 2].grounded)
			{
				num3 += this.tankWheelColliders[j + this.tankWheelColliders.Length / 2].perFrameRotation;
				num4++;
			}
		}
		if (num4 > 0)
		{
			num3 /= (float)num4;
			this.lastRightTrackRpm = num3;
		}
		this.rightTrackOffset += -0.002f * this.lastRightTrackRpm;
		if (this.leftTrackOffset <= -3f)
		{
			this.leftTrackOffset += 3f;
		}
		if (this.leftTrackOffset >= 3f)
		{
			this.leftTrackOffset -= 3f;
		}
		if (this.rightTrackOffset <= -3f)
		{
			this.rightTrackOffset += 3f;
		}
		if (this.rightTrackOffset >= 3f)
		{
			this.rightTrackOffset -= 3f;
		}
		this.leftTrackMaterial.SetTextureOffset("_Texture", new Vector2(0f, this.leftTrackOffset));
		this.rightTrackMaterial.SetTextureOffset("_Texture", new Vector2(0f, this.rightTrackOffset));
		this.leftTrackMaterial.SetTextureOffset("_Dirt", new Vector2(0f, this.leftTrackOffset));
		this.rightTrackMaterial.SetTextureOffset("_Dirt", new Vector2(0f, this.rightTrackOffset));
		for (int k = 0; k < this.visualWheels.Length; k++)
		{
			float num5 = (k <= this.visualWheels.Length / 2 - 1) ? this.lastLeftTrackRpm : this.lastRightTrackRpm;
			this.visualWheels[k].Rotate(new Vector3(-1.2f * num5, 0f, 0f), Space.Self);
		}
	}

	private void FixedUpdate()
	{
		if (this.NoWheelColliders)
		{
			return;
		}
		foreach (TankWheelCollider x in this.tankWheelColliders)
		{
			if (x == null)
			{
				this.NoWheelColliders = true;
				return;
			}
		}
		for (int j = 0; j < this.wheels.Length; j++)
		{
			Vector3 visualWheelPos = this.tankWheelColliders[j].visualWheelPos;
			Vector3 vector = visualWheelPos - this.wheels[j].arm.position;
			Vector3 forward = Vector3.ProjectOnPlane(vector, base.transform.right);
			this.wheels[j].arm.rotation = Quaternion.LookRotation(forward, base.transform.up);
			Vector3 localPosition = this.wheels[j].trackSegment.localPosition;
			localPosition.z = this.wheels[j].trackSegment.parent.InverseTransformPoint(visualWheelPos - base.transform.up * this.tankWheelColliders[j].wheelRadius).z;
			this.wheels[j].trackSegment.localPosition = localPosition;
		}
		for (int k = 0; k < this.shockUps.Length; k++)
		{
			this.shockUps[k].LookAt(this.shockDowns[k], base.transform.forward);
			this.shockDowns[k].LookAt(this.shockUps[k], base.transform.forward);
		}
	}

	private CarController carController;

	public Side SuspensionSide;

	public TankWheel[] wheels;

	public TankWheelCollider[] tankWheelColliders;

	public Transform[] visualWheels;

	public Transform[] shockUps;

	public Transform[] shockDowns;

	public SkinnedMeshRenderer leftTrackMesh;

	public SkinnedMeshRenderer rightTrackMesh;

	public int tracksMaterialID;

	private Material leftTrackMaterial;

	private Material rightTrackMaterial;

	public float leftTrackOffset;

	public float rightTrackOffset;

	private float lastLeftTrackRpm;

	private float lastRightTrackRpm;

	public MonsterControls Controls;

	private bool NoWheelColliders;

	private Vector3 lastPos;

	private Vector3 lastForward;
}
