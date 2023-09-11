using System;
using UnityEngine;

namespace CustomVP
{
	public class WheelComponent : MonoBehaviour
	{
		public float suspensionLength
		{
			get
			{
				return this.travel;
			}
			set
			{
				this.travel = value + 0.2f;
			}
		}

		public void Start()
		{
			this.rigidBody = base.GetComponentInParent<Rigidbody>();
			this.wheelCollider = base.gameObject.AddComponent<WheelCollider>();
			this.wheelCollider.rb = this.rigidBody;
			this.SpherecastProtector = new GameObject("SpherecastProtector").AddComponent<SphereCollider>();
			this.SpherecastProtector.transform.parent = base.transform.parent.parent;
			this.SpherecastProtector.transform.localPosition = new Vector3(0f, 0f, 0f);
			this.SpherecastProtector.gameObject.layer = 26;
			PhysicMaterial physicMaterial = new PhysicMaterial("ZeroFriction");
			physicMaterial.bounciness = 0f;
			physicMaterial.dynamicFriction = 0f;
			physicMaterial.staticFriction = 0f;
			this.SpherecastProtector.material = physicMaterial;
			this.OnValidate();
		}

		public Vector3 GetVisualWheelPosition()
		{
			return base.transform.position - base.transform.up * (this.travel - this.wheelCollider.correctedSuspensionCompression);
		}

		public void FixedUpdate()
		{
			this.wheelCollider.updateWheel();
			this.Compression = this.wheelCollider.compressionDistance;
		}

		private void Update()
		{
			this.wheelCollider.motorTorque = this.MotorTorque;
			this.wheelCollider.steeringAngle = this.Steer;
			this.wheelCollider.brakeTorque = this.BrakeTorque;
			if (this.lastHitOffset != this.wheelCollider._hitOffsetSmooth)
			{
				this.SpherecastProtector.transform.localPosition = new Vector3(0f, this.wheelCollider._hitOffsetSmooth, 0f);
			}
			this.lastHitOffset = this.wheelCollider._hitOffsetSmooth;
			if (this.VisualWheel != null)
			{
				this.VisualWheel.Rotate(this.VisualWheel.right, this.wheelCollider.perFrameRotation, Space.World);
				this.VisualWheel.position = this.GetVisualWheelPosition();
			}
			if (this.wheelCollider.rb != null)
			{
				this.m_speed = this.wheelCollider.rb.velocity.magnitude;
			}
			this.rpm = ((this.FakeRPM <= 0f) ? this.wheelCollider.rpm : this.FakeRPM);
			this.sLong = this.wheelCollider.longitudinalSlip;
			if (this.m_speed < 1f && Mathf.Abs(this.rpm) < 5f)
			{
				this.sLong = 0f;
			}
			this.sLat = this.wheelCollider.lateralSlip;
			if (this.m_speed < 1f && Mathf.Abs(this.rpm) < 5f)
			{
				this.sLat = 0f;
			}
			this.deltaCompression = Mathf.Abs(this.Compression - this.lastCompression);
			this.lastCompression = this.Compression;
			this.CommonSlip = this.sLat + this.sLong;
			this.IsGrounded = this.wheelCollider.isGrounded;
		}

		public void OnValidate()
		{
			if (this.wheelCollider != null)
			{
				this.wheelCollider.radius = this.wheelRadius;
				this.wheelCollider.mass = this.wheelMass;
				this.wheelCollider.length = this.travel;
				this.wheelCollider.spring = this.spring;
				this.wheelCollider.damper = this.damper;
				this.wheelCollider.sweepType = this.sweepType;
				this.wheelCollider.springCurve = this.SpringCurve;
				this.wheelCollider.bias = this.handlingBias;
				this.UpdateFriction();
				this.SpherecastProtector.radius = this.wheelRadius * 0.9f;
			}
		}

		public void UpdateFriction()
		{
			this.wheelCollider.forwardFrictionCoefficient = this.forwardFrictionCoefficient;
			this.wheelCollider.sideFrictionCoefficient = this.sideFrictionCoefficient;
			this.wheelCollider.surfaceFrictionCoefficient = this.surfaceFrictionCoefficient;
			this.wheelCollider.forwardFrictionCurve = new CustomWheelFrictionCurve(this.f_extSlip, this.f_extVal, this.f_asSlip, this.f_asVal, this.f_tailVal);
			this.wheelCollider.sidewaysFrictionCurve = new CustomWheelFrictionCurve(this.s_extSlip, this.s_extVal, this.s_asSlip, this.s_asVal, this.s_tailVal);
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(base.gameObject.transform.position, 0.04f);
			Vector3 vector = base.gameObject.transform.position - base.gameObject.transform.up * this.travel;
			if (this.wheelCollider != null)
			{
				vector += base.gameObject.transform.up * this.wheelCollider.compressionDistance;
			}
			Gizmos.DrawWireSphere(vector, this.wheelRadius);
			Gizmos.DrawSphere(vector, 0.04f);
			Gizmos.DrawRay(base.gameObject.transform.position - base.gameObject.transform.up * this.wheelRadius, base.gameObject.transform.up * this.travel);
			if (this.wheelCollider != null)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawSphere(this.wheelCollider.hitPoint, 0.05f);
			}
		}

		private Rigidbody rigidBody;

		public Transform VisualWheel;

		public float wheelRadius = 0.5f;

		public float wheelMass = 1f;

		public WheelSweepType sweepType;

		[Header("Springs")]
		public float travel = 0.5f;

		public float spring = 1000f;

		public float damper = 1500f;

		public AnimationCurve SpringCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		[Header("Friction")]
		public float forwardFrictionCoefficient = 1f;

		public float sideFrictionCoefficient = 1f;

		public float surfaceFrictionCoefficient = 1f;

		[Header("Forward friction params")]
		public float f_extSlip;

		public float f_extVal;

		public float f_asSlip;

		public float f_asVal;

		public float f_tailVal;

		[Header("Sideways friction params")]
		public float s_extSlip;

		public float s_extVal;

		public float s_asSlip;

		public float s_asVal;

		public float s_tailVal;

		[Space(10f)]
		public float handlingBias;

		[HideInInspector]
		public float rpm;

		[HideInInspector]
		public float FakeRPM;

		[HideInInspector]
		public float sLong;

		[HideInInspector]
		public float sLat;

		[HideInInspector]
		public float Compression;

		[HideInInspector]
		public float lastCompression;

		[HideInInspector]
		public float deltaCompression;

		[HideInInspector]
		public float CommonSlip;

		[HideInInspector]
		public bool IsGrounded;

		[HideInInspector]
		public WheelCollider wheelCollider;

		[HideInInspector]
		public float MotorTorque;

		[HideInInspector]
		public float Steer;

		[HideInInspector]
		public float BrakeTorque;

		private SphereCollider SpherecastProtector;

		private float m_speed;

		private float lastHitOffset;
	}
}
