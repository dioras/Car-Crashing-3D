using System;
using UnityEngine;

namespace CustomVP
{
	public class WheelCollider : MonoBehaviour
	{
		public Vector3 LongForce
		{
			get
			{
				return this.wF * this.localForce.z;
			}
		}

		public AnimationCurve springCurve
		{
			get
			{
				return this.springcurve;
			}
			set
			{
				this.springcurve = value;
			}
		}

		public Rigidbody rb
		{
			get
			{
				return this.rigidBody;
			}
			set
			{
				this.rigidBody = value;
			}
		}

		public float spring
		{
			get
			{
				return this.suspensionSpring;
			}
			set
			{
				this.suspensionSpring = value;
			}
		}

		public float damper
		{
			get
			{
				return this.suspensionDamper;
			}
			set
			{
				this.suspensionDamper = value;
			}
		}

		public float length
		{
			get
			{
				return this.suspensionLength;
			}
			set
			{
				this.suspensionLength = value;
			}
		}

		public float mass
		{
			get
			{
				return this.wheelMass;
			}
			set
			{
				this.wheelMass = value;
				this.currentMomentOfInertia = this.wheelMass * this.wheelRadius * this.wheelRadius * 0.5f;
				this.inertiaInverse = 1f / this.currentMomentOfInertia;
			}
		}

		public float Velocity
		{
			get
			{
				return this.vWheel;
			}
		}

		public float radius
		{
			get
			{
				return this.wheelRadius;
			}
			set
			{
				this.wheelRadius = value;
				this.currentMomentOfInertia = this.wheelMass * this.wheelRadius * this.wheelRadius * 0.5f;
				this.inertiaInverse = 1f / this.currentMomentOfInertia;
				this.radiusInverse = 1f / this.wheelRadius;
			}
		}

		public CustomWheelFrictionCurve forwardFrictionCurve
		{
			get
			{
				return this.fwdFrictionCurve;
			}
			set
			{
				if (value != null)
				{
					this.fwdFrictionCurve = value;
				}
			}
		}

		public CustomWheelFrictionCurve sidewaysFrictionCurve
		{
			get
			{
				return this.sideFrictionCurve;
			}
			set
			{
				if (value != null)
				{
					this.sideFrictionCurve = value;
				}
			}
		}

		public float forwardFrictionCoefficient
		{
			get
			{
				return this.currentFwdFrictionCoef;
			}
			set
			{
				this.currentFwdFrictionCoef = value;
			}
		}

		public float sideFrictionCoefficient
		{
			get
			{
				return this.currentSideFrictionCoef;
			}
			set
			{
				this.currentSideFrictionCoef = value;
			}
		}

		public float surfaceFrictionCoefficient
		{
			get
			{
				return this.currentSurfaceFrictionCoef;
			}
			set
			{
				this.currentSurfaceFrictionCoef = value;
			}
		}

		public float rollingResistance
		{
			get
			{
				return this.rollingResistanceCoefficient;
			}
			set
			{
				this.rollingResistanceCoefficient = value;
			}
		}

		public float rotationalResistance
		{
			get
			{
				return this.rotationalResistanceCoefficient;
			}
			set
			{
				this.rotationalResistanceCoefficient = value;
			}
		}

		public float brakeTorque
		{
			get
			{
				return this.currentBrakeTorque;
			}
			set
			{
				this.currentBrakeTorque = Mathf.Abs(value);
			}
		}

		public float motorTorque
		{
			get
			{
				return this.currentMotorTorque;
			}
			set
			{
				this.currentMotorTorque = value;
			}
		}

		public float steeringAngle
		{
			get
			{
				return this.currentSteeringAngle;
			}
			set
			{
				this.currentSteeringAngle = value;
			}
		}

		public WheelSweepType sweepType
		{
			get
			{
				return this.currentSweepType;
			}
			set
			{
				this.currentSweepType = value;
			}
		}

		public bool autoUpdateEnabled
		{
			get
			{
				return this.automaticUpdates;
			}
			set
			{
				this.automaticUpdates = value;
			}
		}

		public void setImpactCallback(Action<Vector3> callback)
		{
			this.onImpactCallback = callback;
		}

		public void setPreUpdateCallback(Action<WheelCollider> callback)
		{
			this.preUpdateCallback = callback;
		}

		public void setPostUpdateCallback(Action<WheelCollider> callback)
		{
			this.postUpdateCallback = callback;
		}

		public bool isGrounded
		{
			get
			{
				return this.grounded;
			}
		}

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

		public float angularVelocity
		{
			get
			{
				return this.currentAngularVelocity;
			}
			set
			{
				this.currentAngularVelocity = value;
			}
		}

		public float linearVelocity
		{
			get
			{
				return this.currentAngularVelocity * this.wheelRadius;
			}
		}

		public float compressionDistance
		{
			get
			{
				return this.currentSuspensionCompression;
			}
		}

		public float compressionPercent
		{
			get
			{
				return this.correctedSuspensionCompression / this.suspensionLength;
			}
		}

		public int raycastMask
		{
			get
			{
				return this.currentRaycastMask;
			}
			set
			{
				this.currentRaycastMask = value;
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
				return this.rpm * 6f * Time.deltaTime;
			}
		}

		public float externalSpringForce
		{
			get
			{
				return this.extSpringForce;
			}
			set
			{
				this.extSpringForce = value;
			}
		}

		public float momentOfInertia
		{
			get
			{
				return this.currentMomentOfInertia;
			}
		}

		public float springForce
		{
			get
			{
				return this.localForce.y + this.extSpringForce;
			}
		}

		public float dampForce
		{
			get
			{
				return this.fDamp;
			}
		}

		public float longitudinalForce
		{
			get
			{
				return this.localForce.z;
			}
		}

		public float lateralForce
		{
			get
			{
				return this.localForce.x;
			}
		}

		public float longitudinalSlip
		{
			get
			{
				return this.sLong;
			}
		}

		public float lateralSlip
		{
			get
			{
				return this.sLat;
			}
		}

		public Vector3 wheelLocalVelocity
		{
			get
			{
				return this.localVelocity;
			}
		}

		public Collider contactColliderHit
		{
			get
			{
				return this.hitCollider;
			}
		}

		public Vector3 contactNormal
		{
			get
			{
				return this.hitNormal;
			}
		}

		public Vector3 worldHitPos
		{
			get
			{
				return base.transform.position - base.transform.up * (this.suspensionLength - this.currentSuspensionCompression + this.wheelRadius - 0.5f);
			}
		}

		public void Update()
		{
			this._hitOffsetSmooth = Mathf.MoveTowards(this._hitOffsetSmooth, this.hitOffset, Time.deltaTime);
			if (!this.automaticUpdates)
			{
				return;
			}
			if (this.preUpdateCallback != null)
			{
				this.preUpdateCallback(this);
			}
			this.updateWheel();
			if (this.postUpdateCallback != null)
			{
				this.postUpdateCallback(this);
			}
		}

		public void updateWheel()
		{
			if (this.wheel == null)
			{
				this.wheel = base.gameObject;
			}
			this.wheelForward = Quaternion.AngleAxis(this.currentSteeringAngle, this.wheel.transform.up) * this.wheel.transform.forward;
			this.prevSuspensionCompression = this.correctedSuspensionCompression;
			bool flag = this.grounded;
			float num = 0f;
			if (this.checkSuspensionContact(ref num))
			{
				float spring = this.spring;
				float num2 = this.spring * 2f;
				if (num < this.MaxClampedOffset)
				{
					if (num > this.MinClampedOffset)
					{
						float num3 = (this.MaxClampedOffset - num) / (this.MaxClampedOffset - this.MinClampedOffset);
						float num4 = spring + (num2 - spring) * num3;
					}
				}
				this.wR = Vector3.Cross(this.hitNormal, this.wheelForward);
				this.wF = -Vector3.Cross(this.hitNormal, this.wR);
				this.wF = this.wheelForward - this.hitNormal * Vector3.Dot(this.wheelForward, this.hitNormal);
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
				float magnitude = a.magnitude;
				this.localVelocity.z = Vector3.Dot(a.normalized, this.wF) * magnitude;
				this.localVelocity.x = Vector3.Dot(a.normalized, this.wR) * magnitude;
				this.localVelocity.y = Vector3.Dot(a.normalized, this.hitNormal) * magnitude;
				this.calcSpring();
				this.integrateForces(spring);
				if (!flag && this.onImpactCallback != null)
				{
					this.onImpactCallback(this.localVelocity);
				}
			}
			else
			{
				this.integrateUngroundedTorques();
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
			if (this.OppositeWheel != null && this.DiffLock)
			{
				float num5 = this.currentAngularVelocity - this.OppositeWheel.currentAngularVelocity;
				this.currentAngularVelocity -= num5 * 0.5f * this.DiffLockRatio;
			}
			if (this.AnotherAxleWheelL != null && this.AnotherAxleWheelR != null && this.InteraxleDifLock)
			{
				float num6 = (this.AnotherAxleWheelL.currentAngularVelocity + this.AnotherAxleWheelR.currentAngularVelocity) / 2f;
				float num7 = (this.currentAngularVelocity + this.OppositeWheel.currentAngularVelocity) / 2f;
				if (this.AnotherAxleWheelL.radius > this.radius)
				{
					float num8 = this.radius / this.AnotherAxleWheelL.radius;
				}
				else
				{
					float num8 = this.AnotherAxleWheelL.radius / this.radius;
				}
				float num9 = num7 - num6;
				this.currentAngularVelocity -= num9 * 0.5f * this.InteraxleDiffLockRatio * this.AnotherAxleWheelL.radius * this.AnotherAxleWheelL.radius;
			}
			this.ClampMaxRPM();
		}

		public void clearGroundedState()
		{
			this.grounded = false;
			this.vSpring = (this.fDamp = (this.prevSuspensionCompression = (this.currentSuspensionCompression = (this.correctedSuspensionCompression = 0f))));
			this.localForce = Vector3.zero;
			this.hitNormal = Vector3.up;
			this.hitPoint = Vector3.zero;
			this.realHitPoint = Vector3.zero;
			this.localVelocity = Vector3.zero;
			this.hitCollider = null;
		}

		private void integrateForces(float clampedForce)
		{
			this.calcFriction();
			float num = 0.1f;
			if ((this.prevFLong < 0f && this.localForce.z > 0f) || (this.prevFLong > 0f && this.localForce.z < 0f))
			{
				this.localForce.z = this.localForce.z * num;
			}
			if ((this.prevFLat < 0f && this.localForce.x > 0f) || (this.prevFLat > 0f && this.localForce.x < 0f))
			{
				this.localForce.x = this.localForce.x * num;
			}
			Vector3 vector = this.hitNormal * (this.localForce.y - this.fakeForce);
			if (clampedForce > 0f)
			{
				Vector3.ClampMagnitude(vector, clampedForce);
			}
			vector += this.calcAG(this.hitNormal, this.localForce.y - this.fakeForce);
			vector += this.localForce.z * this.wF;
			if (this.rigidBody != null && !float.IsNaN(vector.x))
			{
				this.rigidBody.AddForceAtPosition(vector, this.hitPoint, ForceMode.Force);
				if (this.currentSteeringAngle != 0f)
				{
					this.rigidBody.AddForceAtPosition(this.localForce.x * this.wR, this.hitPoint + base.transform.forward * this.bias, ForceMode.Force);
				}
				else
				{
					this.rigidBody.AddForceAtPosition(this.localForce.x * this.wR, this.hitPoint, ForceMode.Force);
				}
			}
			if (this.hitCollider != null && this.hitCollider.attachedRigidbody != null)
			{
				this.hitCollider.attachedRigidbody.AddForceAtPosition(-vector, this.hitPoint, ForceMode.Force);
			}
			this.prevFLong = this.localForce.z;
			this.prevFLat = this.localForce.x;
		}

		private Vector3 calcAG(Vector3 hitNormal, float springForce)
		{
			Vector3 vector = new Vector3(0f, 0f, 0f);
			float num = Vector3.Dot(hitNormal, this.gNorm);
			float num2 = num * springForce;
			Vector3 lhs = Vector3.Cross(hitNormal, this.gNorm);
			Vector3 lhs2 = Vector3.Cross(lhs, hitNormal);
			float num3 = Vector3.Dot(lhs2, this.wR);
			vector = num2 * num3 * this.wR * Mathf.Clamp(this.currentSideFrictionCoef, 0f, 1f);
			if (this.brakeTorque > 0f && Mathf.Abs(this.motorTorque) < this.brakeTorque)
			{
				float num4 = Vector3.Dot(lhs2, this.wF);
				vector += num2 * num4 * this.wF * Mathf.Clamp(this.currentFwdFrictionCoef, 0f, 1f);
			}
			return vector;
		}

		private void integrateUngroundedTorques()
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

		private bool checkSuspensionContact(ref float xContactOffset)
		{
			WheelSweepType wheelSweepType = this.currentSweepType;
			if (wheelSweepType == WheelSweepType.RAY)
			{
				return this.suspensionSweepRaycast();
			}
			if (wheelSweepType != WheelSweepType.SPHERE)
			{
				return this.suspensionSweepRaycast();
			}
			return this.suspensionSweepSpherecast(ref xContactOffset);
		}

		private bool suspensionSweepRaycast()
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(this.wheel.transform.position, -this.wheel.transform.up, out raycastHit, this.suspensionLength + this.wheelRadius, this.currentRaycastMask))
			{
				this.currentSuspensionCompression = this.suspensionLength + this.wheelRadius - raycastHit.distance;
				this.correctedSuspensionCompression = this.currentSuspensionCompression;
				this.hitNormal = raycastHit.normal;
				this.hitCollider = raycastHit.collider;
				this.hitPoint = raycastHit.point;
				this.grounded = true;
				return true;
			}
			this.grounded = false;
			return false;
		}

		private bool suspensionSweepSpherecast(ref float xContactOffset)
		{
			RaycastHit raycastHit;
			if (Physics.SphereCast(this.wheel.transform.position + this.wheel.transform.up * this.wheelRadius, this.radius, -this.wheel.transform.up, out raycastHit, this.length + this.wheelRadius, this.currentRaycastMask))
			{
				this.realHitPoint = raycastHit.point;
				raycastHit.point -= this.wheel.transform.up * this._hitOffsetSmooth;
				raycastHit.distance += this._hitOffsetSmooth;
				Vector3 b = base.transform.position - base.transform.up * this.suspensionLength + base.transform.up * this.compressionDistance;
				Vector3 direction = Vector3.ProjectOnPlane((raycastHit.point - b).normalized, base.transform.forward);
				xContactOffset = base.transform.InverseTransformDirection(direction).x;
				this.currentSuspensionCompression = this.suspensionLength + this.wheelRadius - raycastHit.distance;
				float b2 = Mathf.InverseLerp(60f, 0f, Vector3.Angle(base.transform.up, raycastHit.normal)) * 6f + 1f;
				this.alignSpeed = Mathf.Lerp(this.alignSpeed, b2, Time.fixedDeltaTime * 5f);
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

		private void calcSpring()
		{
			this.vSpring = (this.correctedSuspensionCompression - this.prevSuspensionCompression) / Time.fixedDeltaTime;
			this.fDamp = this.suspensionDamper * this.vSpring;
			float num = Mathf.InverseLerp(140f, 0f, Vector3.Angle(base.transform.up, this.hitNormal));
			float num2 = this.suspensionSpring * (this.springCurve.Evaluate(this.compressionPercent) * (this.correctedSuspensionCompression / this.compressionPercent));
			num2 += this.fDamp;
			num2 *= num;
			if (this.correctedSuspensionCompression > this.suspensionLength)
			{
				this.fakeForce = 10000f;
			}
			else
			{
				this.fakeForce = 0f;
			}
			if (num2 < 0f)
			{
				num2 = 0f;
			}
			this.localForce.y = num2 + this.fakeForce;
		}

		private void calcFriction()
		{
			this.calcFrictionStandard();
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

		public void calcFrictionStandard()
		{
			this.currentAngularVelocity += this.currentMotorTorque * this.inertiaInverse * Time.fixedDeltaTime;
			if (this.currentAngularVelocity != 0f)
			{
				float num = this.localForce.y * this.rollingResistanceCoefficient;
				float num2 = num * this.wheelRadius;
				float num3 = num2 * this.inertiaInverse * Time.fixedDeltaTime;
				num3 = Mathf.Min(num3, Mathf.Abs(this.currentAngularVelocity)) * Mathf.Sign(this.currentAngularVelocity);
				this.currentAngularVelocity -= num3;
			}
			if (this.currentAngularVelocity != 0f)
			{
				this.currentAngularVelocity -= this.currentAngularVelocity * this.rotationalResistanceCoefficient * this.radiusInverse * this.inertiaInverse * Time.fixedDeltaTime;
			}
			float num4 = this.currentBrakeTorque * this.inertiaInverse * Time.fixedDeltaTime;
			float num5 = Mathf.Min(Mathf.Abs(this.currentAngularVelocity), num4);
			this.currentAngularVelocity += num5 * -Mathf.Sign(this.currentAngularVelocity);
			float num6 = num4 - num5;
			this.vWheel = this.currentAngularVelocity * this.wheelRadius;
			this.sLong = this.calcLongSlip(this.localVelocity.z, this.vWheel);
			this.sLat = this.calcLatSlip(this.localVelocity.z, this.localVelocity.x);
			this.vWheelDelta = this.vWheel - this.localVelocity.z;
			float num7 = this.localForce.y + this.extSpringForce;
			float num8 = this.fwdFrictionCurve.evaluate(this.sLong) * num7 * this.currentFwdFrictionCoef * this.currentSurfaceFrictionCoef;
			float num9 = this.sideFrictionCurve.evaluate(this.sLat) * num7 * this.currentSideFrictionCoef * this.currentSurfaceFrictionCoef;
			this.localForce.x = num9;
			if (this.localForce.x > Mathf.Abs(this.localVelocity.x) * num7)
			{
				this.localForce.x = Mathf.Abs(this.localVelocity.x) * num7;
			}
			this.localForce.x = this.localForce.x * -Mathf.Sign(this.localVelocity.x);
			float num10 = this.vWheelDelta * this.radiusInverse;
			float f = num10 * this.currentMomentOfInertia;
			float num11 = Mathf.Abs(f) / Time.fixedDeltaTime;
			float num12 = num11 * this.radiusInverse;
			num12 = Mathf.Min(num12, num8);
			float num13 = num12 * this.wheelRadius * -Mathf.Sign(this.vWheelDelta);
			this.localForce.z = num12 * Mathf.Sign(this.vWheelDelta);
			float num14 = num13 * this.inertiaInverse * Time.fixedDeltaTime;
			this.currentAngularVelocity += num14;
			if (Mathf.Abs(this.currentAngularVelocity) < num6)
			{
				this.currentAngularVelocity = 0f;
				num6 -= Mathf.Abs(this.currentAngularVelocity);
				float a = Mathf.Max(0f, Mathf.Abs(num8) - Mathf.Abs(this.localForce.z));
				float b = Mathf.Max(0f, num7 * Mathf.Abs(this.localVelocity.z) - Mathf.Abs(this.localForce.z));
				float num15 = Mathf.Min(a, b);
				this.localForce.z = this.localForce.z + num15 * -Mathf.Sign(this.localVelocity.z);
			}
			else
			{
				this.currentAngularVelocity += -Mathf.Sign(this.currentAngularVelocity) * num6;
			}
			this.combinatorialFriction(num9, num8, this.localForce.x, this.localForce.z, out this.localForce.x, out this.localForce.z);
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

		private void combinatorialFriction(float latMax, float longMax, float fLat, float fLong, out float combLat, out float combLong)
		{
			float num = (this.fwdFrictionCurve.max + this.sideFrictionCurve.max) * 0.5f * (this.localForce.y + this.extSpringForce);
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

		private GameObject wheel;

		private Rigidbody rigidBody;

		private float wheelMass = 1f;

		private float wheelRadius = 0.5f;

		private float suspensionLength = 1f;

		private float suspensionSpring = 10f;

		private float suspensionDamper = 2f;

		private float currentFwdFrictionCoef = 1f;

		private float currentSideFrictionCoef = 1f;

		private float currentSurfaceFrictionCoef = 1f;

		private float currentSteeringAngle;

		private float currentMotorTorque;

		private float currentBrakeTorque;

		private float currentMomentOfInertia = 0.125f;

		private int currentRaycastMask = -67108865;

		private WheelSweepType currentSweepType;

		private CustomWheelFrictionCurve fwdFrictionCurve = new CustomWheelFrictionCurve(0.06f, 1.2f, 0.065f, 1.25f, 0.7f);

		private CustomWheelFrictionCurve sideFrictionCurve = new CustomWheelFrictionCurve(0.03f, 1f, 0.04f, 1.05f, 0.7f);

		private bool automaticUpdates;

		private Vector3 gNorm = new Vector3(0f, -1f, 0f);

		private Action<Vector3> onImpactCallback;

		private Action<WheelCollider> preUpdateCallback;

		private Action<WheelCollider> postUpdateCallback;

		private float extSpringForce;

		private float rollingResistanceCoefficient = 0.005f;

		private float rotationalResistanceCoefficient;

		private bool grounded;

		private float inertiaInverse;

		private float radiusInverse;

		private float prevFLong;

		private float prevFLat;

		private float currentSuspensionCompression;

		private float prevSuspensionCompression;

		private float currentAngularVelocity;

		private float vSpring;

		private float fDamp;

		private Vector3 wF;

		private Vector3 wR;

		private Vector3 wheelForward;

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

		private Vector3 hitNormal;

		private Collider hitCollider;

		private AnimationCurve springcurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		[HideInInspector]
		public WheelCollider OppositeWheel;

		[HideInInspector]
		public WheelCollider AnotherAxleWheelL;

		[HideInInspector]
		public WheelCollider AnotherAxleWheelR;

		[HideInInspector]
		public bool DiffLock;

		[HideInInspector]
		public float DiffLockRatio;

		[HideInInspector]
		public float InteraxleDiffLockRatio;

		[HideInInspector]
		public bool InteraxleDifLock;

		[HideInInspector]
		public float FakeRPM;

		[HideInInspector]
		public float bias;

		private float MinClampedOffset = 0.3f;

		private float MaxClampedOffset = 0.5f;

		public float hitOffset;

		public float _hitOffsetSmooth;

		[HideInInspector]
		public float correctedSuspensionCompression;

		private float alignSpeed;

		[HideInInspector]
		public float rpmLimit;

		private float fakeForce;
	}
}
