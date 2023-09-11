using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class DroneController : MonoBehaviour
{
	public Rigidbody rb
	{
		get
		{
			if (this._rb == null)
			{
				this._rb = base.GetComponent<Rigidbody>();
			}
			return this._rb;
		}
	}

	private void Awake()
	{
		this.engineSound = base.GetComponent<AudioSource>();
		this.engineSound.volume = 0f;
	}

	private void Start()
	{
		this.GetLimits();
	}

	private void GetLimits()
	{
		this.maxHeight = float.PositiveInfinity;
		this.minX = (this.minZ = float.NegativeInfinity);
		this.maxX = (this.maxZ = float.PositiveInfinity);
		if (SurfaceManager.Instance != null)
		{
			if (SurfaceManager.Instance.maxDroneHeight != 0f)
			{
				this.maxHeight = SurfaceManager.Instance.maxDroneHeight;
			}
			Terrain baseTerrain = SurfaceManager.Instance.BaseTerrain;
			if (baseTerrain != null)
			{
				this.minX = baseTerrain.GetPosition().x;
				this.minZ = baseTerrain.GetPosition().z;
				this.maxX = this.minX + baseTerrain.terrainData.size.x;
				this.maxZ = this.minZ + baseTerrain.terrainData.size.z;
			}
		}
	}

	public void MakeMultiplayer(PhotonTransformView tView)
	{
		this.multiplayerDrone = true;
		this.myTview = tView;
	}

	public void SetInactive()
	{
		this.active = false;
		foreach (Collider collider in base.GetComponentsInChildren<Collider>())
		{
			collider.enabled = false;
		}
	}

	public void StartFollowingObject(GameObject obj)
	{
		this.followedObject = obj;
		this.followingObject = true;
	}

	public void StopFollowingObject()
	{
		this.followedObject = null;
		this.followingObject = false;
	}

	private void Update()
	{
		if (this.followingObject && this.followedObject == null)
		{
			CarUIControl.Instance.StopFollowingVehicle();
		}
		if (this.multiplayerDrone && this.myTview == null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		if (!this.multiplayerDrone)
		{
			if (this.active)
			{
				this.DoInput();
			}
			if (this.followingObject)
			{
				this.DoFollowInput();
			}
			this.CalculateTilt();
			this.forceToAdd = this.GetForce();
			if (this.active)
			{
				this.engineSound.pitch = Mathf.Lerp(this.engineSound.pitch, Mathf.Lerp(1f, 1.5f, this.forceToAdd.magnitude / this.flyForce), Time.deltaTime * 5f);
				this.engineSound.volume = Mathf.Lerp(this.engineSound.volume, Mathf.Lerp(0.3f, 1f, this.forceToAdd.magnitude / this.flyForce), Time.deltaTime * 5f);
			}
			else
			{
				this.engineSound.volume = 0f;
			}
		}
		else
		{
			this.ClearInput();
			this.CalculateMpTilt();
			this.engineSound.pitch = Mathf.Lerp(this.engineSound.pitch, Mathf.Lerp(1f, 1.5f, (Mathf.Abs(this.sideTilt) + Mathf.Abs(this.forwardTilt)) / 1f), Time.deltaTime * 5f);
			this.engineSound.volume = Mathf.Lerp(this.engineSound.volume, Mathf.Lerp(0.3f, 1f, (Mathf.Abs(this.sideTilt) + Mathf.Abs(this.forwardTilt)) / 1f), Time.deltaTime * 5f);
		}
		foreach (Transform transform in this.rotors)
		{
			transform.Rotate(0f, 2000f * Time.deltaTime, 0f);
		}
		float num = (Mathf.PerlinNoise(Time.time * this.randomPosRate, 0f) - 0.5f) * 2f * this.randomPosPower;
		float num2 = (Mathf.PerlinNoise(Time.time * this.randomPosRate, 100.5f) - 0.5f) * 2f * this.randomPosPower;
		this.anchor.localPosition = new Vector3(num, num2 + this.heightTilt * this.maxHeightDeviation, 0f);
		float num3 = num / this.randomPosPower * this.randomRotAngle;
		Vector3 euler = new Vector3(this.forwardTilt * this.maxLeanAngle, 0f, -this.sideTilt * this.maxLeanAngle + num3);
		this.anchor.localRotation = Quaternion.Euler(euler);
	}

	private void FixedUpdate()
	{
		if (!this.multiplayerDrone)
		{
			this.rb.AddForce(this.forceToAdd);
			Vector3 euler = new Vector3(0f, this.wantedY, 0f);
			Quaternion b = Quaternion.Euler(euler);
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b, Time.deltaTime * this.steerSpeed);
		}
	}

	private void CalculateTilt()
	{
		this.sideTilt = Mathf.Lerp(this.sideTilt, this.xInput, Time.deltaTime * this.leanSpeed);
		this.forwardTilt = Mathf.Lerp(this.forwardTilt, this.yInput, Time.deltaTime * this.leanSpeed);
		this.heightTilt = Mathf.Lerp(this.heightTilt, this.heightInput, Time.deltaTime * this.leanSpeed / 2f);
	}

	private void CalculateMpTilt()
	{
		this.sideTilt = Mathf.Lerp(this.sideTilt, this.lastMpSideTilt, Time.deltaTime * this.leanSpeed);
		this.forwardTilt = Mathf.Lerp(this.forwardTilt, this.lastMpForwardTilt, Time.deltaTime * this.leanSpeed);
		this.heightTilt = 0f;
	}

	public void UpdateMpTilt(float side, float forward)
	{
		this.lastMpForwardTilt = forward;
		this.lastMpSideTilt = side;
	}

	public void FeedWantedYAngle(float angle)
	{
		this.wantedY = angle;
	}

	private void DoInput()
	{
		this.xInput = UnityEngine.Input.GetAxis("Horizontal") + CrossPlatformInputManager.GetAxis("Horizontal");
		this.yInput = UnityEngine.Input.GetAxis("Vertical") + CrossPlatformInputManager.GetAxis("Vertical");
		this.heightInput = CrossPlatformInputManager.GetAxis("DroneHeight");
		if (UnityEngine.Input.GetKey(KeyCode.LeftShift))
		{
			this.heightInput = -1f;
		}
		if (UnityEngine.Input.GetKey(KeyCode.Space))
		{
			this.heightInput = 1f;
		}
		if (base.transform.position.y > this.maxHeight)
		{
			this.heightInput = Mathf.Clamp(this.heightInput, -1f, 0f);
		}
	}

	private void DoFollowInput()
	{
		Vector3 position = base.transform.position;
		position.y = this.followedObject.transform.position.y;
		float value = Vector3.Distance(position, this.followedObject.transform.position);
		float f = this.followedObject.transform.position.y + this.followHeight - base.transform.position.y;
		float num = Mathf.InverseLerp(3f, 5f, Mathf.Abs(f)) * Mathf.Sign(f);
		this.heightInput += num;
		Vector3 vector = this.followedObject.transform.position - base.transform.position;
		vector.y = 0f;
		Vector3 vector2 = base.transform.InverseTransformVector(vector);
		float num2 = Mathf.InverseLerp(this.followDistance, this.followDistance + 20f, value);
		this.xInput += vector2.normalized.x * num2;
		this.yInput += vector2.normalized.z * num2;
		this.xInput = Mathf.Clamp(this.xInput, -1f, 1f);
		this.yInput = Mathf.Clamp(this.yInput, -1f, 1f);
	}

	private void ClearInput()
	{
		this.xInput = 0f;
		this.yInput = 0f;
		this.heightInput = 0f;
	}

	private Vector3 GetForce()
	{
		Vector3 a = base.transform.forward * this.yInput * this.flyForce;
		Vector3 b = base.transform.right * this.xInput * this.flyForce;
		Vector3 b2 = base.transform.up * this.heightTilt * this.heightForce;
		Vector3 vector = Vector3.zero;
		if (base.transform.position.x > this.maxX)
		{
			vector += new Vector3(-this.flyForce, 0f, 0f);
		}
		if (base.transform.position.x < this.minX)
		{
			vector += new Vector3(this.flyForce, 0f, 0f);
		}
		if (base.transform.position.z > this.maxZ)
		{
			vector += new Vector3(0f, 0f, -this.flyForce);
		}
		if (base.transform.position.z < this.minZ)
		{
			vector += new Vector3(0f, 0f, this.flyForce);
		}
		return Vector3.ClampMagnitude(a + b + b2, this.flyForce) + vector;
	}

	private Rigidbody _rb;

	public Transform[] rotors;

	public Transform anchor;

	public Transform cameraPos;

	[Space(20f)]
	public float flyForce;

	public float heightForce;

	[Space(20f)]
	public float steerSpeed;

	public float leanSpeed;

	public float maxLeanAngle;

	public float maxHeightDeviation;

	[Space(20f)]
	public float randomPosPower;

	public float randomPosRate;

	public float randomRotAngle;

	[Space(20f)]
	public float followDistance = 30f;

	public float followHeight = 10f;

	public GameObject followedObject;

	private bool followingObject;

	private float xInput;

	private float yInput;

	private float heightInput;

	private float wantedY;

	[HideInInspector]
	public float sideTilt;

	[HideInInspector]
	public float forwardTilt;

	[HideInInspector]
	public float heightTilt;

	private float lastMpSideTilt;

	private float lastMpForwardTilt;

	private float maxHeight;

	private float minX;

	private float maxX;

	private float minZ;

	private float maxZ;

	private Vector3 forceToAdd;

	private bool multiplayerDrone;

	private bool active = true;

	private PhotonTransformView myTview;

	private AudioSource engineSound;
}
