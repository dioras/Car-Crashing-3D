using System;
using UnityEngine;

public class SnowBikeRearSuspension : Suspension
{
	private void Start()
	{
		this.trackMat = this.renderer.materials[this.tracksMatID];
	}

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

	public override void OnValidate()
	{
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		this.DoWheelColliderParameters();
		this.DoShocks();
		this.side = Side.Rear;
		if (this.wheelColliders[0] == null)
		{
			return;
		}
		this.wheelColliders[0].OnValidate();
	}

	private void DoShocks()
	{
		for (int i = 0; i < this.ShockUps.Length; i++)
		{
			this.ShockDowns[i].LookAt(this.ShockUps[i], -base.transform.forward);
			this.ShockUps[i].LookAt(this.ShockDowns[i], -base.transform.forward);
		}
	}

	private void DoWheelColliderParameters()
	{
		if (this.wheelColliders[0] == null)
		{
			return;
		}
		this.wheelColliders[0].suspensionLength = this.Controls.Travel.FloatValue;
		this.wheelColliders[0].spring = this.Controls.Stiffness.FloatValue;
		this.wheelColliders[0].damper = this.Controls.Damping.FloatValue;
	}

	public override void UpdateSuspension(float SteerAngle, float WheelRadius, float rpm)
	{
		bool flag = false;
		Vector3 vector = base.transform.up;
		Vector3 position = this.Raycasters[0].position - this.Raycasters[0].up * (this.Controls.Travel.FloatValue + 0.2f);
		RaycastHit raycastHit;
		if (Physics.Raycast(this.Raycasters[0].position, -this.Raycasters[0].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
			vector = raycastHit.normal;
			flag = true;
		}
		Vector3 localPosition = this.trackBottom.localPosition;
		localPosition.y = this.trackBottom.parent.InverseTransformPoint(position).y + this.offset;
		this.trackBottom.localPosition = localPosition;
		Vector3 vector2 = base.transform.up;
		if (flag)
		{
			vector2 = vector;
			vector2 = Vector3.ProjectOnPlane(vector2, base.transform.right);
			if (Vector3.SignedAngle(vector2, base.transform.up, -base.transform.right) < -45f)
			{
				vector2 = (base.transform.up - base.transform.forward) / 2f;
			}
			if (Vector3.SignedAngle(vector2, base.transform.up, -base.transform.right) > 45f)
			{
				vector2 = (base.transform.up + base.transform.forward) / 2f;
			}
		}
		this.trackBottom.rotation = Quaternion.Lerp(this.trackBottom.rotation, Quaternion.LookRotation(vector2, base.transform.forward), Time.deltaTime * 8f);
		this.wheel.Rotate(new Vector3(rpm, 0f, 0f), Space.Self);
		this.currentOffset -= rpm * this.offsetSpeed;
		this.trackMat.SetTextureOffset("_Texture", new Vector2(0f, this.currentOffset));
		this.trackMat.SetTextureOffset("_Dirt", new Vector2(0f, this.currentOffset));
		this.DoShocks();
	}

	private void FixedUpdate()
	{
		if (this.NoWheelColliders)
		{
			return;
		}
		if (this.wheelColliders[0] == null)
		{
			this.NoWheelColliders = true;
			return;
		}
		Vector3 visualWheelPosition = this.wheelColliders[0].GetVisualWheelPosition();
		Vector3 localPosition = this.trackBottom.localPosition;
		localPosition.y = this.trackBottom.parent.InverseTransformPoint(visualWheelPosition).y + this.offset;
		this.trackBottom.localPosition = localPosition;
		Vector3 vector = base.transform.up;
		if (this.wheelColliders[0].IsGrounded)
		{
			vector = this.wheelColliders[0].wheelCollider.contactNormal;
			vector = Vector3.ProjectOnPlane(vector, base.transform.right);
			if (Vector3.SignedAngle(vector, base.transform.up, -base.transform.right) < -45f)
			{
				vector = (base.transform.up - base.transform.forward) / 2f;
			}
			if (Vector3.SignedAngle(vector, base.transform.up, -base.transform.right) > 45f)
			{
				vector = (base.transform.up + base.transform.forward) / 2f;
			}
		}
		this.trackBottom.rotation = Quaternion.Lerp(this.trackBottom.rotation, Quaternion.LookRotation(vector, base.transform.forward), Time.deltaTime * 8f);
		float perFrameRotation = this.wheelColliders[0].wheelCollider.perFrameRotation;
		this.wheel.Rotate(new Vector3(perFrameRotation, 0f, 0f), Space.Self);
		this.currentOffset -= perFrameRotation * this.offsetSpeed;
		this.trackMat.SetTextureOffset("_Texture", new Vector2(0f, this.currentOffset));
		this.trackMat.SetTextureOffset("_Dirt", new Vector2(0f, this.currentOffset));
		this.DoShocks();
	}

	public Transform wheel;

	public float offset;

	public Transform trackBottom;

	public Transform[] ShockUps;

	public Transform[] ShockDowns;

	public Renderer renderer;

	public int tracksMatID;

	private Material trackMat;

	private float currentOffset;

	public float offsetSpeed;

	public SnowBikeRearSuspensionControls Controls;

	private bool NoWheelColliders;
}
