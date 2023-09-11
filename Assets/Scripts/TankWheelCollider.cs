using System;
using CustomVP;
using UnityEngine;

public class TankWheelCollider : MonoBehaviour
{
	public float compressionPercent
	{
		get
		{
			return this.correctedSuspensionCompression / this.suspensionLength;
		}
	}

	public float perFrameRotation
	{
		get
		{
			if (this.FakeRPM > 0f)
			{
				return this.FakeRPM;
			}
			return this.currentAngularVelocity * 9.549296f * 6f * Time.deltaTime;
		}
	}

	public Vector3 worldHitPos
	{
		get
		{
			return base.transform.position - base.transform.up * (this.suspensionLength - this.currentSuspensionCompression + this.wheelRadius - 0.5f);
		}
	}

	public Vector3 visualWheelPos
	{
		get
		{
			return base.transform.position - base.transform.up * (this.suspensionLength - this.correctedSuspensionCompression);
		}
	}

	public Vector3 LongForce
	{
		get
		{
			return this.wF * this.localForce.z;
		}
	}

	public float rpm
	{
		get
		{
			return this.currentAngularVelocity * 9.549296f;
		}
	}

	private void Start()
	{
		this.rigidBody = base.GetComponentInParent<Rigidbody>();
		PhysicMaterial physicMaterial = new PhysicMaterial("ZeroFriction");
		physicMaterial.bounciness = 0f;
		physicMaterial.dynamicFriction = 0f;
		physicMaterial.bounceCombine = PhysicMaterialCombine.Minimum;
		physicMaterial.staticFriction = 0f;
		this.SpherecastProtector = new GameObject("SpherecastProtector").AddComponent<SphereCollider>();
		this.SpherecastProtector.transform.parent = base.transform;
		this.SpherecastProtector.transform.localPosition = Vector3.zero;
		this.SpherecastProtector.gameObject.layer = 26;
		this.SpherecastProtector.material = physicMaterial;
		this.SpherecastProtector.radius = this.wheelRadius * 1.05f;
		this.OnValidate();
	}

	private void OnValidate()
	{
		this.fwdFrictionCurve = new CustomWheelFrictionCurve(this.fwdF0, this.fwdF1, this.fwdF2, this.fwdF3, this.fwdF4);
		this.sideFrictionCurve = new CustomWheelFrictionCurve(this.sdF0, this.sdF1, this.sdF2, this.sdF3, this.sdF4);
		if (this.SpherecastProtector != null)
		{
			this.SpherecastProtector.radius = this.wheelRadius * 0.9f;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(base.transform.position, 0.04f);
		Vector3 vector = base.gameObject.transform.position - base.gameObject.transform.up * this.suspensionLength;
		if (Application.isPlaying)
		{
			vector += base.gameObject.transform.up * this.correctedSuspensionCompression;
		}
		Gizmos.DrawWireSphere(vector, this.wheelRadius);
		Gizmos.DrawSphere(vector, 0.04f);
		Gizmos.DrawRay(base.transform.position, -base.transform.up * this.suspensionLength);
		if (this.hitCollider != null)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(this.hitPoint, 0.05f);
		}
	}

	private void Update()
	{
		this._hitOffsetSmooth = Mathf.MoveTowards(this._hitOffsetSmooth, this.hitOffset, Time.deltaTime);
		this.currentMomentOfInertia = this.wheelMass * this.wheelRadius * this.wheelRadius * 0.5f;
		this.inertiaInverse = 1f / this.currentMomentOfInertia;
		this.radiusInverse = 1f / this.wheelRadius;
		if (this.lastHitOffset != this._hitOffsetSmooth)
		{
			this.SpherecastProtector.transform.localPosition = new Vector3(0f, this._hitOffsetSmooth, 0f);
		}
		this.lastHitOffset = this._hitOffsetSmooth;
	}

	private void FixedUpdate()
	{
		this.prevSuspensionCompression = this.correctedSuspensionCompression;
		bool flag = this.grounded;
		this.grounded = this.SuspensionSweepSpherecast();
		if (this.grounded)
		{
			this.wF = base.transform.forward - this.hitNormal * Vector3.Dot(base.transform.forward, this.hitNormal);
			this.wR = Vector3.Cross(this.hitNormal, this.wF);
			Vector3 a = Vector3.zero;
			if (this.rigidBody != null)
			{
				a = this.rigidBody.GetPointVelocity(this.hitPoint);
				if (this.hitCollider != null && this.hitCollider.attachedRigidbody != null)
				{
					a -= this.hitCollider.attachedRigidbody.GetPointVelocity(this.hitPoint);
				}
			}
			this.localVelocity.z = Vector3.Dot(a.normalized, this.wF) * a.magnitude;
			this.localVelocity.x = Vector3.Dot(a.normalized, this.wR) * a.magnitude;
			this.localVelocity.y = Vector3.Dot(a.normalized, this.hitNormal) * a.magnitude;
			this.CalcSpring();
			this.CalcFriction();
			this.IntegrateForces();
		}
		else
		{
			this.IntegrateUngroundedTorques();
			this.vSpring = (this.fDamp = (this.prevSuspensionCompression = 0f));
			this.currentSuspensionCompression = Mathf.MoveTowards(this.currentSuspensionCompression, 0f, Time.deltaTime);
			this.correctedSuspensionCompression = this.currentSuspensionCompression;
			this.localForce = Vector3.zero;
			this.hitNormal = Vector3.zero;
			this.hitPoint = Vector3.zero;
			this.realHitPoint = Vector3.zero;
			this.hitCollider = null;
			this.localVelocity = Vector3.zero;
		}
		this.ClampMaxRPM();
	}

	private void IntegrateForces()
	{
		float num = 0.1f;
		if ((this.prevFLong < 0f && this.localForce.z > 0f) || (this.prevFLong > 0f && this.localForce.z < 0f))
		{
			this.localForce.z = this.localForce.z * num;
		}
		if ((this.prevFLat < 0f && this.localForce.x > 0f) || (this.prevFLat > 0f && this.localForce.x < 0f))
		{
			this.localForce.x = this.localForce.x * num;
		}
		Vector3 vector = this.hitNormal * this.localForce.y;
		Vector3 vector2 = this.localForce.z * this.wF * this.forwardTorqueMult;
		Vector3 vector3 = this.localForce.x * this.wR;
		UnityEngine.Debug.DrawRay(this.hitPoint, vector / 1000f, Color.blue);
		UnityEngine.Debug.DrawRay(this.hitPoint, vector3 / 1000f, Color.green);
		UnityEngine.Debug.DrawRay(this.hitPoint, vector2 / 1000f, Color.yellow);
		if (this.rigidBody != null)
		{
			this.rigidBody.AddForceAtPosition(vector, this.hitPoint, ForceMode.Force);
			this.rigidBody.AddForceAtPosition(vector2, this.hitPoint, ForceMode.Force);
			this.rigidBody.AddForceAtPosition(vector3, this.hitPoint, ForceMode.Force);
		}
		Vector3 a = vector + vector2 + vector3;
		if (this.hitCollider != null && this.hitCollider.attachedRigidbody != null)
		{
			this.hitCollider.attachedRigidbody.AddForceAtPosition(-a, this.hitPoint, ForceMode.Force);
		}
		this.prevFLong = this.localForce.z * this.forwardTorqueMult;
		this.prevFLat = this.localForce.x;
	}

	private void IntegrateUngroundedTorques()
	{
		this.currentAngularVelocity += this.currentMotorTorque * this.inertiaInverse * Time.fixedDeltaTime;
		if (this.currentAngularVelocity != 0f)
		{
			float num = this.rotationalResistanceCoefficient * this.currentAngularVelocity * this.inertiaInverse * Time.fixedDeltaTime;
			num = Mathf.Min(Mathf.Abs(num), Mathf.Abs(this.currentAngularVelocity)) * Mathf.Sign(this.currentAngularVelocity);
			this.currentAngularVelocity -= num;
		}
		if (this.currentAngularVelocity != 0f)
		{
			float num2 = this.currentBrakeTorque * this.inertiaInverse * Time.fixedDeltaTime;
			num2 = Mathf.Min(Mathf.Abs(this.currentAngularVelocity), num2) * Mathf.Sign(this.currentAngularVelocity);
			this.currentAngularVelocity -= num2;
		}
	}

	private bool SuspensionSweepSpherecast()
	{
		this.grounded = false;
		RaycastHit raycastHit;
		if (Physics.SphereCast(base.transform.position + base.transform.up * this.wheelRadius, this.wheelRadius, -base.transform.up, out raycastHit, this.suspensionLength + this.wheelRadius, this.currentRaycastMask))
		{
			this.realHitPoint = raycastHit.point;
			this.hitPoint = raycastHit.point - base.transform.up * this._hitOffsetSmooth;
			raycastHit.distance += this._hitOffsetSmooth;
			this.currentSuspensionCompression = this.suspensionLength + this.wheelRadius - raycastHit.distance;
			float b = Mathf.InverseLerp(60f, 0f, Vector3.Angle(base.transform.up, raycastHit.normal)) * 6f + 1f;
			this.alignSpeed = Mathf.Lerp(this.alignSpeed, b, Time.fixedDeltaTime * 5f);
			this.correctedSuspensionCompression = Mathf.MoveTowards(this.correctedSuspensionCompression, this.currentSuspensionCompression, Time.fixedDeltaTime * this.alignSpeed);
			this.hitNormal = raycastHit.normal;
			if (Vector3.Angle(this.hitNormal, base.transform.up) > 45f)
			{
				Vector3 vector = Vector3.ProjectOnPlane(this.hitNormal, base.transform.up);
				this.hitNormal = ((base.transform.up + vector.normalized) / 2f).normalized;
			}
			this.hitCollider = raycastHit.collider;
			this.grounded = true;
		}
		return this.grounded;
	}

	private void CalcSpring()
	{
		this.vSpring = (this.correctedSuspensionCompression - this.prevSuspensionCompression) / Time.fixedDeltaTime;
		this.fDamp = this.suspensionDamper * this.vSpring;
		float num = this.suspensionSpring * (this.springcurve.Evaluate(this.compressionPercent) * (this.correctedSuspensionCompression / this.compressionPercent));
		num += this.fDamp;
		if (num < 0f)
		{
			num = 0f;
		}
		Vector3 to = Vector3.ProjectOnPlane(this.hitNormal, base.transform.right);
		float value = Vector3.Angle(base.transform.up, to);
		float num2 = Mathf.InverseLerp(135f, 0f, value);
		num *= num2;
		if (this.maxSpringForce > 0f)
		{
			num = Mathf.Clamp(num, 0f, this.maxSpringForce);
		}
		this.localForce.y = num;
	}

	private float CalcLongSlip(float vLong, float vWheel)
	{
		if (vLong == 0f && vWheel == 0f)
		{
			return 0f;
		}
		float num = Mathf.Max(vLong, vWheel);
		float num2 = Mathf.Min(vLong, vWheel);
		float value = (num - num2) / Mathf.Abs(num);
		return Mathf.Clamp(value, 0f, 1f);
	}

	private float CalcLatSlip(float vLong, float vLat)
	{
		if (vLat == 0f)
		{
			return 0f;
		}
		if (vLong == 0f)
		{
			return 1f;
		}
		float num = Mathf.Abs(Mathf.Atan(vLat / vLong));
		num *= 57.29578f;
		return num / 90f;
	}

	public void CalcFriction()
	{
		this.currentAngularVelocity += this.currentMotorTorque * this.inertiaInverse * Time.fixedDeltaTime;
		float num = this.currentBrakeTorque * this.inertiaInverse * Time.fixedDeltaTime;
		this.currentAngularVelocity += num * -Mathf.Sign(this.localVelocity.z);
		this.vWheel = this.currentAngularVelocity * this.wheelRadius;
		this.sLong = this.CalcLongSlip(this.localVelocity.z, this.vWheel);
		this.sLat = this.CalcLatSlip(this.localVelocity.z, this.localVelocity.x);
		this.vWheelDelta = this.vWheel - this.localVelocity.z;
		float y = this.localForce.y;
		y = this.constantDownforce;
		float num2 = this.fwdFrictionCurve.evaluate(this.sLong) * y * this.currentFwdFrictionCoef * this.currentSurfaceFrictionCoef;
		float num3 = this.sideFrictionCurve.evaluate(this.sLat) * y * this.currentSideFrictionCoef * this.currentSurfaceFrictionCoef;
		this.localForce.x = num3;
		if (this.localForce.x > Mathf.Abs(this.localVelocity.x) * y)
		{
			this.localForce.x = Mathf.Abs(this.localVelocity.x) * y;
		}
		this.localForce.x = this.localForce.x * -Mathf.Sign(this.localVelocity.x);
		float num4 = this.vWheelDelta * this.radiusInverse;
		float f = num4 * this.currentMomentOfInertia;
		float num5 = Mathf.Abs(f) / Time.fixedDeltaTime;
		float num6 = num5 * this.radiusInverse;
		num6 = Mathf.Min(num6, num2);
		float num7 = num6 * this.wheelRadius * -Mathf.Sign(this.vWheelDelta);
		this.localForce.z = num6 * Mathf.Sign(this.vWheelDelta);
		float num8 = num7 * this.inertiaInverse * Time.fixedDeltaTime;
		this.currentAngularVelocity += num8;
		this.CombinatorialFriction(num3, num2, this.localForce.x, this.localForce.z, out this.localForce.x, out this.localForce.z);
	}

	private void ClampMaxRPM()
	{
		if (this.rpmLimit == 0f)
		{
			return;
		}
		float num = Mathf.Abs(this.currentAngularVelocity);
		if (num > this.rpmLimit)
		{
			this.currentAngularVelocity = this.rpmLimit * Mathf.Sign(this.currentAngularVelocity);
		}
	}

	private void CombinatorialFriction(float latMax, float longMax, float fLat, float fLong, out float combLat, out float combLong)
	{
		float num = (this.fwdFrictionCurve.max + this.sideFrictionCurve.max) * 0.5f * this.constantDownforce;
		float num2 = Mathf.Sqrt(fLat * fLat + fLong * fLong);
		if (num2 > num)
		{
			fLong /= num2;
			fLat /= num2;
			fLong *= num;
			fLat *= num;
		}
		combLat = fLat;
		combLong = fLong;
	}

	private Rigidbody rigidBody;

	public float wheelMass = 1f;

	public float wheelRadius = 0.5f;

	public float rotationalResistanceCoefficient;

	[Space(20f)]
	public float suspensionLength = 1f;

	public float suspensionSpring = 10f;

	public float suspensionDamper = 2f;

	public float maxSpringForce;

	public float constantDownforce;

	public AnimationCurve springcurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(1f, 1f)
	});

	[Space(20f)]
	public float currentFwdFrictionCoef = 1f;

	public float currentSideFrictionCoef = 1f;

	public float currentSurfaceFrictionCoef = 1f;

	[Space(20f)]
	public float fwdF0 = 0.06f;

	public float fwdF1 = 1.2f;

	public float fwdF2 = 0.065f;

	public float fwdF3 = 1.25f;

	public float fwdF4 = 0.7f;

	[Space(20f)]
	public float sdF0 = 0.03f;

	public float sdF1 = 1f;

	public float sdF2 = 0.04f;

	public float sdF3 = 1.05f;

	public float sdF4 = 0.7f;

	[Space(20f)]
	private SphereCollider SpherecastProtector;

	private float _hitOffsetSmooth;

	private float lastHitOffset;

	[HideInInspector]
	public float hitOffset;

	public float rpmLimit;

	[HideInInspector]
	public float FakeRPM;

	[HideInInspector]
	public float currentMotorTorque;

	[HideInInspector]
	public float currentBrakeTorque;

	[HideInInspector]
	public float forwardTorqueMult = 1f;

	private CustomWheelFrictionCurve fwdFrictionCurve;

	private CustomWheelFrictionCurve sideFrictionCurve;

	private float currentMomentOfInertia;

	private int currentRaycastMask = -67108865;

	private float inertiaInverse;

	private float radiusInverse;

	private float prevFLong;

	private float prevFLat;

	private float currentSuspensionCompression;

	private float prevSuspensionCompression;

	private float currentAngularVelocity;

	private float vSpring;

	private float fDamp;

	[HideInInspector]
	public bool grounded;

	[HideInInspector]
	public Vector3 hitPoint;

	[HideInInspector]
	public Vector3 realHitPoint;

	[HideInInspector]
	public Vector3 hitNormal;

	[HideInInspector]
	public Collider hitCollider;

	[HideInInspector]
	public float correctedSuspensionCompression;

	private float alignSpeed;

	private Vector3 wF;

	private Vector3 wR;

	[HideInInspector]
	public Vector3 localVelocity;

	[HideInInspector]
	public Vector3 localForce;

	private float vWheel;

	private float vWheelDelta;

	[HideInInspector]
	public float sLong;

	[HideInInspector]
	public float sLat;
}
