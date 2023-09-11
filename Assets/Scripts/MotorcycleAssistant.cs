using System;
using CustomVP;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MotorcycleAssistant : MonoBehaviour
{
	private void Start()
	{
		this.rb = base.GetComponent<Rigidbody>();
		this.ikDriver = base.GetComponent<IKDriverController>();
	}

	private void Update()
	{
		if (this.multiplayer)
		{
			float deltaTime = Time.deltaTime;
			if (deltaTime > 0f)
			{
				this.speed = (base.transform.position - this.lastPos).magnitude / deltaTime;
			}
			this.lastPos = base.transform.position;
			float num = (float)((Mathf.Abs(this.speed) >= 2f) ? 0 : -10);
			float num2 = Mathf.InverseLerp(0f, 20f, this.speed);
			float b = this.lastSteerInput * -this.MaxLean * num2 + num;
			this.lean = Mathf.Lerp(this.lean, b, deltaTime * 5f);
			this.BikeBody.localEulerAngles = new Vector3(0f, 0f, this.lean);
		}
		else
		{
			if (!this.fullyGrounded)
			{
				this.flyingTime += Time.deltaTime;
			}
			else
			{
				this.flyingTime = 0f;
			}
			this.flyingTime = Mathf.Clamp01(this.flyingTime);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		this.touchingGround = true;
	}

	private void OnCollisionExit(Collision collision)
	{
		this.touchingGround = false;
	}

	public void UpdateMpValues(float steerInput)
	{
		this.lastSteerInput = steerInput;
	}

	private void FixedUpdate()
	{
		if (this.multiplayer)
		{
			return;
		}
		this.fullyGrounded = (this.FrontWC.IsGrounded && this.RearWC.IsGrounded);
		this.halfGrounded = (this.FrontWC.IsGrounded || this.RearWC.IsGrounded);
		this.speed = 0f;
		if (this.rb != null)
		{
			this.speed = base.transform.InverseTransformDirection(this.rb.velocity).z * 3.6f;
			this.rb.angularDrag = ((!this.halfGrounded) ? 0f : this.angularDrag);
		}
		float num = UnityEngine.Input.GetAxis("Horizontal") + CrossPlatformInputManager.GetAxis("Horizontal");
		float num2 = UnityEngine.Input.GetAxis("Vertical") + CrossPlatformInputManager.GetAxis("Vertical");
		this.xInput = Mathf.MoveTowards(this.xInput, num, Time.fixedDeltaTime * 5f);
		this.yInput = Mathf.MoveTowards(this.yInput, num2, Time.fixedDeltaTime * 5f);
		this.smoothxInput = Mathf.Lerp(this.smoothxInput, num, Time.fixedDeltaTime * 4f);
		this.smoothyInput = Mathf.Lerp(this.smoothyInput, num2, Time.fixedDeltaTime * 4f);
		float num3 = (float)((Mathf.Abs(this.speed) >= 2f || !this.fullyGrounded || !this.ikDriver.enabled) ? 0 : -10);
		float num4 = Mathf.InverseLerp(0f, 20f, this.speed);
		float num5 = this.xInput * -this.MaxLean * num4 + num3;
		if (!this.fullyGrounded)
		{
			num5 = 0f;
		}
		this.lean = Mathf.Lerp(this.lean, num5, Time.deltaTime * 5f);
		this.BikeBody.localEulerAngles = new Vector3(0f, 0f, this.lean);
		Vector3 a = base.transform.right * (-num5 + num3) / this.MaxLean;
		if (this.speed < -1f)
		{
			a = base.transform.right * -this.xInput * 0.5f;
		}
		Vector3 vector = base.transform.right;
		if (!this.fullyGrounded)
		{
			if (this.touchingGround)
			{
				vector = -Vector3.Cross(base.transform.forward, Vector3.up);
			}
			else
			{
				vector = Vector3.zero;
			}
			a = base.transform.right * this.smoothxInput * 2f;
		}
		Vector3 a2 = this.halfGrounded ? Vector3.zero : (base.transform.up * this.smoothyInput * this.flyingTime);
		if (this.ikDriver != null)
		{
			this.stabilization = !this.ikDriver.KnockedOut;
		}
		else
		{
			this.stabilization = true;
		}
		if (this.stabilization && this.rb != null)
		{
			Vector3 a3 = base.transform.position + Vector3.ProjectOnPlane(base.transform.up, Vector3.ProjectOnPlane(vector, Vector3.up));
			Vector3 vector2 = a3 - base.transform.position;
			if (vector2.y < 0f && !this.halfGrounded && this.touchingGround)
			{
				vector2 = -vector2;
			}
			Quaternion a4 = Quaternion.LookRotation(base.transform.forward + a * 2f * Time.fixedDeltaTime + a2 * 3f * Time.fixedDeltaTime, base.transform.up);
			Quaternion b = Quaternion.LookRotation(base.transform.forward + a * 2f * Time.fixedDeltaTime + a2 * 3f * Time.fixedDeltaTime, vector2);
			base.transform.rotation = Quaternion.Lerp(a4, b, Mathf.Abs(base.transform.up.z));
			Vector3 angularVelocity = this.rb.angularVelocity;
			Vector3 vector3 = base.transform.InverseTransformVector(angularVelocity);
			vector3.y = 0f;
			vector3.z = 0f;
			this.rb.angularVelocity = base.transform.TransformVector(vector3);
		}
	}

	private IKDriverController ikDriver;

	private Rigidbody rb;

	public WheelComponent FrontWC;

	public WheelComponent RearWC;

	public float angularDrag = 10f;

	public float MaxLean;

	public Transform BikeBody;

	[HideInInspector]
	public float lean;

	private float vel;

	private float flyingTime;

	private float speed;

	private float xInput;

	private float yInput;

	private float smoothxInput;

	private float smoothyInput;

	private bool stabilization;

	private bool touchingGround;

	[HideInInspector]
	public bool fullyGrounded;

	[HideInInspector]
	public bool halfGrounded;

	[HideInInspector]
	public bool multiplayer;

	private Vector3 lastPos;

	private float lastSteerInput;
}
