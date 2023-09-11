using System;
using UnityEngine;

namespace CustomVP
{
	public class TrailerWheelCollider : MonoBehaviour
	{
		public float rpm
		{
			get
			{
				return this.currentAngularVelocity * 9.549296f;
			}
			set
			{
				this.currentAngularVelocity = value * 0.104719758f;
			}
		}

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
				return this.rpm * 6f * Time.deltaTime;
			}
		}

		public Vector3 GetVisualWheelPosition()
		{
			return base.transform.position - base.transform.up * (this.suspensionLength - this.correctedSuspensionCompression);
		}

		private void Start()
		{
			this.rigidBody = base.GetComponentInParent<Rigidbody>();
			this.InitializeColliders();
		}

		private void InitializeColliders()
		{
			this.BumpStopCollider = new GameObject("BumpStop").AddComponent<SphereCollider>();
			this.BumpStopCollider.transform.parent = base.gameObject.transform;
			this.BumpStopCollider.transform.localPosition = new Vector3(0f, 0f, 0f);
			this.BumpStopCollider.gameObject.layer = 26;
			this.BumpStopCollider.radius = 0.1f;
			this.SpherecastProtector = new GameObject("SpherecastProtector").AddComponent<SphereCollider>();
			this.SpherecastProtector.transform.parent = base.gameObject.transform;
			this.SpherecastProtector.transform.localPosition = new Vector3(0f, 0f, 0f);
			this.SpherecastProtector.gameObject.layer = 26;
			this.SpherecastProtector.radius = this.wheelRadius;
			PhysicMaterial physicMaterial = new PhysicMaterial("ZeroFriction");
			physicMaterial.bounciness = 0f;
			physicMaterial.dynamicFriction = 0f;
			physicMaterial.staticFriction = 0f;
			this.SpherecastProtector.material = physicMaterial;
			this.BumpStopCollider.material = physicMaterial;
		}

		public void Update()
		{
			this.hitOffsetSmooth = Mathf.MoveTowards(this.hitOffsetSmooth, this.hitOffset, Time.deltaTime);
			this.BumpStopCollider.transform.position = base.transform.position - base.transform.up * (this.wheelRadius - this.hitOffset - 0.1f);
		}

		private void FixedUpdate()
		{
			this.DoWheelCollider();
		}

		private void DoWheelCollider()
		{
			this.prevSuspensionCompression = this.correctedSuspensionCompression;
			if (this.DoSpherecast())
			{
				this.wF = base.transform.forward - this.hitNormal * Vector3.Dot(base.transform.forward, this.hitNormal);
				this.wR = Vector3.Cross(this.hitNormal, this.wF);
				this.currentMomentOfInertia = this.wheelMass * this.wheelRadius * this.wheelRadius * 0.5f;
				this.inertiaInverse = 1f / this.currentMomentOfInertia;
				this.radiusInverse = 1f / this.wheelRadius;
				Vector3 a = Vector3.zero;
				a = this.rigidBody.GetPointVelocity(this.hitPoint);
				if (this.hitCollider != null && this.hitCollider.attachedRigidbody != null)
				{
					a -= this.hitCollider.attachedRigidbody.GetPointVelocity(this.hitPoint);
				}
				float magnitude = a.magnitude;
				this.localVelocity.z = Vector3.Dot(a.normalized, this.wF) * magnitude;
				this.localVelocity.x = Vector3.Dot(a.normalized, this.wR) * magnitude;
				this.localVelocity.y = Vector3.Dot(a.normalized, this.hitNormal) * magnitude;
				this.DoSpring();
				this.ApplyForces();
			}
			else
			{
				this.grounded = false;
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
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(base.gameObject.transform.position, 0.04f);
			Vector3 vector = base.gameObject.transform.position - base.gameObject.transform.up * this.suspensionLength;
			vector += base.transform.up * this.currentSuspensionCompression;
			Gizmos.DrawWireSphere(vector, this.wheelRadius);
			Gizmos.DrawSphere(vector, 0.04f);
			Gizmos.DrawRay(base.gameObject.transform.position - base.gameObject.transform.up * this.wheelRadius, base.gameObject.transform.up * this.suspensionLength);
		}

		private void ApplyForces()
		{
			this.CalcFrictionStandard();
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
			vector += this.calcAG(this.hitNormal, this.localForce.y);
			vector += this.localForce.z * this.wF;
			vector += this.localForce.x * this.wR;
			if (this.rigidBody != null && !float.IsNaN(vector.x))
			{
				this.rigidBody.AddForceAtPosition(vector, this.hitPoint, ForceMode.Force);
			}
			this.prevFLong = this.localForce.z;
			this.prevFLat = this.localForce.x;
		}

		private Vector3 calcAG(Vector3 hitNormal, float springForce)
		{
			Vector3 result = new Vector3(0f, 0f, 0f);
			float num = Vector3.Dot(hitNormal, Vector3.down);
			float num2 = num * springForce;
			Vector3 lhs = Vector3.Cross(hitNormal, Vector3.down);
			Vector3 lhs2 = Vector3.Cross(lhs, hitNormal);
			float num3 = Vector3.Dot(lhs2, this.wR);
			result = num2 * num3 * this.wR * Mathf.Clamp(this.currentSideFrictionCoef, 0f, 1f);
			return result;
		}

		private bool DoSpherecast()
		{
			RaycastHit raycastHit;
			if (Physics.SphereCast(base.transform.position + base.transform.up * this.wheelRadius, this.wheelRadius, -base.transform.up, out raycastHit, this.suspensionLength + this.wheelRadius, -67108865))
			{
				this.realHitPoint = raycastHit.point;
				raycastHit.point -= base.transform.up * this.hitOffsetSmooth;
				raycastHit.distance += this.hitOffsetSmooth;
				this.currentSuspensionCompression = this.suspensionLength + this.wheelRadius - raycastHit.distance;
				float b = Mathf.InverseLerp(60f, 0f, Vector3.Angle(base.transform.up, raycastHit.normal)) * 6f + 1f;
				this.alignSpeed = Mathf.Lerp(this.alignSpeed, b, Time.fixedDeltaTime * 5f);
				this.correctedSuspensionCompression = Mathf.MoveTowards(this.correctedSuspensionCompression, this.currentSuspensionCompression, Time.fixedDeltaTime * this.alignSpeed);
				this.hitNormal = raycastHit.normal;
				this.hitCollider = raycastHit.collider;
				this.hitPoint = raycastHit.point;
				this.grounded = true;
				return true;
			}
			this.grounded = false;
			return false;
		}

		private void DoSpring()
		{
			this.vSpring = (this.correctedSuspensionCompression - this.prevSuspensionCompression) / Time.fixedDeltaTime;
			this.fDamp = this.suspensionDamper * this.vSpring;
			float num = Mathf.InverseLerp(140f, 0f, Vector3.Angle(base.transform.up, this.hitNormal));
			float num2 = this.suspensionSpring * (this.springcurve.Evaluate(this.compressionPercent) * (this.correctedSuspensionCompression / this.compressionPercent));
			num2 += this.fDamp;
			num2 *= num;
			if (num2 < 0f)
			{
				num2 = 0f;
			}
			this.localForce.y = num2;
		}

		private float calcLongSlip(float vLong, float vWheel)
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

		private float calcLatSlip(float vLong, float vLat)
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

		public void CalcFrictionStandard()
		{
			float num = this.currentBrakeTorque * this.inertiaInverse * Time.fixedDeltaTime;
			float num2 = Mathf.Min(Mathf.Abs(this.currentAngularVelocity), num);
			this.currentAngularVelocity += num2 * -Mathf.Sign(this.currentAngularVelocity);
			float num3 = num - num2;
			this.vWheel = this.currentAngularVelocity * this.wheelRadius;
			this.sLong = this.calcLongSlip(this.localVelocity.z, this.vWheel);
			this.sLat = this.calcLatSlip(this.localVelocity.z, this.localVelocity.x);
			this.vWheelDelta = this.vWheel - this.localVelocity.z;
			float y = this.localForce.y;
			float num4 = this.fwdFrictionCurve.evaluate(this.sLong) * y * this.currentFwdFrictionCoef * this.currentSurfaceFrictionCoef;
			float num5 = this.sideFrictionCurve.evaluate(this.sLat) * y * this.currentSideFrictionCoef * this.currentSurfaceFrictionCoef;
			this.localForce.x = num5;
			if (this.localForce.x > Mathf.Abs(this.localVelocity.x) * y)
			{
				this.localForce.x = Mathf.Abs(this.localVelocity.x) * y;
			}
			this.localForce.x = this.localForce.x * -Mathf.Sign(this.localVelocity.x);
			float num6 = this.vWheelDelta * this.radiusInverse;
			float f = num6 * this.currentMomentOfInertia;
			float num7 = Mathf.Abs(f) / Time.fixedDeltaTime;
			float num8 = num7 * this.radiusInverse;
			num8 = Mathf.Min(num8, num4);
			float num9 = num8 * this.wheelRadius * -Mathf.Sign(this.vWheelDelta);
			this.localForce.z = num8 * Mathf.Sign(this.vWheelDelta);
			float num10 = num9 * this.inertiaInverse * Time.fixedDeltaTime;
			this.currentAngularVelocity += num10;
			if (Mathf.Abs(this.currentAngularVelocity) < num3)
			{
				this.currentAngularVelocity = 0f;
				num3 -= Mathf.Abs(this.currentAngularVelocity);
				float a = Mathf.Max(0f, Mathf.Abs(num4) - Mathf.Abs(this.localForce.z));
				float b = Mathf.Max(0f, y * Mathf.Abs(this.localVelocity.z) - Mathf.Abs(this.localForce.z));
				float num11 = Mathf.Min(a, b);
				this.localForce.z = this.localForce.z + num11 * -Mathf.Sign(this.localVelocity.z);
			}
			else
			{
				this.currentAngularVelocity += -Mathf.Sign(this.currentAngularVelocity) * num3;
			}
			this.combinatorialFriction(num5, num4, this.localForce.x, this.localForce.z, out this.localForce.x, out this.localForce.z);
		}

		private void combinatorialFriction(float latMax, float longMax, float fLat, float fLong, out float combLat, out float combLong)
		{
			float num = (this.fwdFrictionCurve.max + this.sideFrictionCurve.max) * 0.5f * this.localForce.y;
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

		public float suspensionLength = 1f;

		public float suspensionSpring = 10f;

		public float suspensionDamper = 2f;

		public float currentFwdFrictionCoef = 1f;

		public float currentSideFrictionCoef = 1f;

		public float currentSurfaceFrictionCoef = 1f;

		[HideInInspector]
		public float currentBrakeTorque;

		public AnimationCurve springcurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		private float currentMomentOfInertia = 0.125f;

		private CustomWheelFrictionCurve fwdFrictionCurve = new CustomWheelFrictionCurve(0.06f, 1.2f, 0.065f, 1.25f, 0.7f);

		private CustomWheelFrictionCurve sideFrictionCurve = new CustomWheelFrictionCurve(0.03f, 1f, 0.04f, 1.05f, 0.7f);

		private float inertiaInverse;

		private float radiusInverse;

		private bool grounded;

		private float prevFLong;

		private float prevFLat;

		private float currentSuspensionCompression;

		private float prevSuspensionCompression;

		private float vSpring;

		private float fDamp;

		private float alignSpeed;

		[HideInInspector]
		public float correctedSuspensionCompression;

		private float currentAngularVelocity;

		private Vector3 wF;

		private Vector3 wR;

		private Vector3 localVelocity;

		private Vector3 localForce;

		private float vWheel;

		private float vWheelDelta;

		private float sLong;

		private float sLat;

		[HideInInspector]
		public Vector3 hitPoint;

		[HideInInspector]
		public Vector3 realHitPoint;

		[HideInInspector]
		public float hitOffset;

		private float hitOffsetSmooth;

		private Vector3 hitNormal;

		private Collider hitCollider;

		private SphereCollider BumpStopCollider;

		private SphereCollider SpherecastProtector;
	}
}
