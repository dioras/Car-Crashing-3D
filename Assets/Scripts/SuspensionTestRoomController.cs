using System;
using CustomVP;
using UnityEngine;

public class SuspensionTestRoomController : MonoBehaviour
{
	public SuspensionTestRoomController()
	{
		if (SuspensionTestRoomController.Instance == null)
		{
			SuspensionTestRoomController.Instance = this;
		}
	}

	private void Start()
	{
		SuspensionTestRoomController.Instance = this;
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.bumpTerrain0.gameObject);
		gameObject.transform.position = this.bumpTerrain0.transform.position + new Vector3(0f, 0f, 50f);
		gameObject.transform.parent = this.bumpTerrain0.transform.parent;
		this.terrains0 = new GameObject[2];
		this.terrains0[0] = this.bumpTerrain0;
		this.terrains0[1] = gameObject;
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.bumpTerrain1.gameObject);
		gameObject2.transform.position = this.bumpTerrain1.transform.position + new Vector3(0f, 0f, 50f);
		gameObject2.transform.parent = this.bumpTerrain1.transform.parent;
		this.terrains1 = new GameObject[2];
		this.terrains1[0] = this.bumpTerrain1;
		this.terrains1[1] = gameObject2;
		GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.bumpTerrain2.gameObject);
		gameObject3.transform.position = this.bumpTerrain2.transform.position + new Vector3(0f, 0f, 50f);
		gameObject3.transform.parent = this.bumpTerrain2.transform.parent;
		this.terrains2 = new GameObject[2];
		this.terrains2[0] = this.bumpTerrain2;
		this.terrains2[1] = gameObject3;
	}

	public void InitializeSuspensionTest(GameObject Vehicle)
	{
		this.car = Vehicle.GetComponent<CarController>();
		this.carRigidbody = Vehicle.GetComponent<Rigidbody>();
		this.car.transform.position = this.CarPositionPoint.position;
		this.car.transform.rotation = this.CarPositionPoint.rotation;
		this.car.enabled = true;
		this.car.FWD = (this.car.RWD = true);
		this.car.vehicleIsActive = true;
		this.car.PreventFromSideSliding = false;
		this.car.OnValidate();
		this.CarInitialized = true;
	}

	public void DeinitializeSuspensionTest()
	{
		this.StopTest();
		this.CarInitialized = false;
		this.car.vehicleIsActive = false;
	}

	public void SetPattern(int ID)
	{
		this.TerrainPattern = ID;
		if (this.TestStarted)
		{
			this.StopTest();
			this.StartTest();
		}
	}

	public void SetMoveSpeed(float value)
	{
		this.MoveSpeed = value;
		this.car.FakeRPM = this.MoveSpeed * 2f;
		this.car.OnValidate();
	}

	[ContextMenu("Start test")]
	public void StartTest()
	{
		this.car.FakeRPM = this.MoveSpeed * 2f;
		this.car.SetZeroFriction();
		this.car.OnValidate();
		foreach (GameObject gameObject in this.terrains0)
		{
			gameObject.SetActive(this.TerrainPattern == 0);
		}
		foreach (GameObject gameObject2 in this.terrains1)
		{
			gameObject2.SetActive(this.TerrainPattern == 1);
		}
		foreach (GameObject gameObject3 in this.terrains2)
		{
			gameObject3.SetActive(this.TerrainPattern == 2);
		}
		this.TestStarted = true;
		if (this.MoveSpeed == 0f)
		{
			this.MoveSpeed = 2f;
		}
	}

	[ContextMenu("Stop test")]
	public void StopTest()
	{
		this.car.FakeRPM = 0f;
		this.car.SetDefaultFriction();
		this.car.OnValidate();
		Transform transform = this.terrains0[0].transform;
		Vector3 vector = Vector3.zero;
		this.terrains2[0].transform.localPosition = vector;
		vector = vector;
		this.terrains1[0].transform.localPosition = vector;
		transform.localPosition = vector;
		Transform transform2 = this.terrains0[1].transform;
		vector = new Vector3(0f, 0f, 50f);
		this.terrains2[1].transform.localPosition = vector;
		vector = vector;
		this.terrains1[1].transform.localPosition = vector;
		transform2.localPosition = vector;
		this.TestStarted = false;
	}

	private void MoveTerrains()
	{
		foreach (GameObject gameObject in this.terrains0)
		{
			gameObject.transform.position -= new Vector3(0f, 0f, this.MoveSpeed * Time.deltaTime);
			if (gameObject.transform.localPosition.z < -99f)
			{
				gameObject.transform.localPosition = Vector3.zero;
			}
		}
		foreach (GameObject gameObject2 in this.terrains1)
		{
			gameObject2.transform.position -= new Vector3(0f, 0f, this.MoveSpeed * Time.deltaTime);
			if (gameObject2.transform.localPosition.z < -99f)
			{
				gameObject2.transform.localPosition = Vector3.zero;
			}
		}
		foreach (GameObject gameObject3 in this.terrains2)
		{
			gameObject3.transform.position -= new Vector3(0f, 0f, this.MoveSpeed * Time.deltaTime);
			if (gameObject3.transform.localPosition.z < -99f)
			{
				gameObject3.transform.localPosition = Vector3.zero;
			}
		}
	}

	private void HoldCarAtPosition()
	{
		Vector3 position = this.CarPositionPoint.position;
		position.y = this.car.transform.position.y;
		this.carRigidbody.velocity /= 1.05f;
		this.carRigidbody.AddForce((position - this.car.transform.position) * 5000f);
		this.carRigidbody.angularVelocity /= 1.05f;
		this.carRigidbody.AddTorque(Vector3.Cross(this.car.transform.forward, Vector3.forward) * 5000f);
	}

	private void Update()
	{
		if (!this.CarInitialized)
		{
			return;
		}
		if (this.TestStarted)
		{
			this.HoldCarAtPosition();
			this.MoveTerrains();
		}
	}

	public static SuspensionTestRoomController Instance;

	public GameObject bumpTerrain0;

	public GameObject bumpTerrain1;

	public GameObject bumpTerrain2;

	public Transform CarPositionPoint;

	public float MoveSpeed;

	private int TerrainPattern;

	private GameObject[] terrains0;

	private GameObject[] terrains1;

	private GameObject[] terrains2;

	private CarController car;

	private Rigidbody carRigidbody;

	private bool TestStarted;

	private bool CarInitialized;
}
