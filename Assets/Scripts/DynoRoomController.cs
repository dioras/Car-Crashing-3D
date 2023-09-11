using System;
using System.Collections;
using System.Collections.Generic;
using CustomVP;
using UnityEngine;

public class DynoRoomController : MonoBehaviour
{
	public DynoRoomController()
	{
		if (DynoRoomController.Instance == null)
		{
			DynoRoomController.Instance = this;
		}
	}

	private void Awake()
	{
		DynoRoomController.Instance = this;
	}

	public void InitializeDyno(GameObject Vehicle)
	{
		this.car = Vehicle.GetComponent<CarController>();
		this.carRigidbody = Vehicle.GetComponent<Rigidbody>();
		this.engineController = Vehicle.GetComponent<EngineController>();
		this.engineController.enabled = true;
		this.car.transform.position = this.CarPos.position;
		this.car.transform.rotation = this.CarPos.rotation;
		Vector3 position = this.FrontDynoStand.position;
		position.z = this.car.wheels[0].wc.transform.position.z;
		this.FrontDynoStand.position = position;
		Vector3 position2 = this.RearDynoStand.position;
		position2.z = this.car.wheels[this.car.wheels.Count - 1].wc.transform.position.z;
		this.RearDynoStand.position = position2;
		this.car.enabled = true;
		this.car.FWD = (this.car.RWD = true);
		this.car.PreventFromSideSliding = false;
		this.car.OnValidate();
	}

	private void BuildCurve(float PeakHP)
	{
		VehicleDataManager component = this.car.GetComponent<VehicleDataManager>();
		AnimationCurve animationCurve = null;
		float num = 0f;
		switch (component.vehicleType)
		{
		case VehicleType.Truck:
			num = this.TrucksHPMultiplier;
			if (this.engineController.Diesel)
			{
				animationCurve = this.DieselTruckCurve;
			}
			else
			{
				animationCurve = this.GasTruckCurve;
			}
			break;
		case VehicleType.ATV:
			animationCurve = this.AtvCurve;
			num = this.AtvsHPMultiplier;
			break;
		case VehicleType.SideBySide:
			animationCurve = this.UtvCurve;
			num = this.UtvsHPMultiplier;
			break;
		case VehicleType.Crawler:
			animationCurve = this.CrawlersCurve;
			num = this.CrawlersHPMultiplier;
			break;
		case VehicleType.Bike:
			animationCurve = this.BikeCurve;
			num = this.BikesHPMultiplier;
			break;
		}
		this.HPCurve = new List<Vector2>();
		this.TQCurve = new List<Vector2>();
		PeakHP *= num;
		float axisMaxValue = PeakHP * 1.1f;
		float num2 = 0f;
		for (int i = 0; i < animationCurve.keys.Length; i++)
		{
			if (animationCurve.keys[i].value > num2)
			{
				num2 = animationCurve.keys[i].value;
			}
		}
		float num3 = PeakHP / num2;
		for (int j = 0; j < animationCurve.keys.Length; j++)
		{
			float time = animationCurve.keys[j].time;
			float num4 = animationCurve.keys[j].value * num3;
			float y = 5252f * num4 / time * this.TorqueCurveMultiplier;
			this.HPCurve.Add(new Vector2(time, num4));
			this.TQCurve.Add(new Vector2(time, y));
		}
		float num5 = 0f;
		foreach (Vector2 vector in this.TQCurve)
		{
			if (vector.y > num5)
			{
				num5 = vector.y;
			}
		}
		float num6 = 0f;
		foreach (Vector2 vector2 in this.TQCurve)
		{
			num6 += vector2.y;
		}
		num6 /= (float)this.TQCurve.Count;
		float num7 = 0f;
		foreach (Vector2 vector3 in this.HPCurve)
		{
			num7 += vector3.y;
		}
		num7 /= (float)this.HPCurve.Count;
		this.DynoFinished(PeakHP, num7, num5, num6);
		this.graph.Start();
		this.Line0.pointValues = new WMG_List<Vector2>();
		this.Line1.pointValues = new WMG_List<Vector2>();
		this.graph.yAxis.AxisMaxValue = axisMaxValue;
		foreach (Vector2 item in this.HPCurve)
		{
			this.Line0.pointValues.Add(item);
		}
		foreach (Vector2 item2 in this.TQCurve)
		{
			this.Line1.pointValues.Add(item2);
		}
	}

	private void DynoFinished(float maxHP, float avgHP, float maxTQ, float avgTQ)
	{
		MenuManager.Instance.DynoFinished(maxHP, avgHP, maxTQ, avgTQ);
	}

	private void Update()
	{
		if (this.car == null)
		{
			return;
		}
		foreach (_Wheel wheel in this.car.wheels)
		{
			wheel.wc.wheelCollider.FakeRPM = 15f * (this.DynoRatio + 0.3f);
		}
		if (this.car.wheels[0].wc.IsGrounded)
		{
			this.carRigidbody.AddRelativeTorque(-Vector3.right * 5000f * this.DynoRatio);
		}
		this.engineController.FakeRPMTarget = Mathf.Lerp(800f, 6000f, this.DynoRatio);
		foreach (Transform transform in this.Rollers)
		{
			transform.Rotate(0f, 0f, 20f * (this.DynoRatio + 0.3f));
		}
		Vector3 position = Vector3.zero;
		Vector3 position2 = Vector3.zero;
		if (this.car.GetComponent<BodyPartsSwitcher>().RearWinchPoint != null)
		{
			position = this.car.GetComponent<BodyPartsSwitcher>().RearWinchPoint.position;
		}
		else
		{
			if (this.car.wheels.Count == 4)
			{
				position = (this.car.wheels[2].wc.transform.position + this.car.wheels[3].wc.transform.position) / 2f;
			}
			if (this.car.wheels.Count == 2)
			{
				position = this.car.wheels[1].wc.transform.position;
			}
		}
		if (this.car.GetComponent<BodyPartsSwitcher>().FrontWinchPoint != null)
		{
			position2 = this.car.GetComponent<BodyPartsSwitcher>().FrontWinchPoint.position;
		}
		else
		{
			if (this.car.wheels.Count == 4)
			{
				position2 = (this.car.wheels[0].wc.transform.position + this.car.wheels[1].wc.transform.position) / 2f;
			}
			if (this.car.wheels.Count == 2)
			{
				position2 = this.car.wheels[0].wc.transform.position;
			}
		}
		foreach (LineRenderer lineRenderer in this.RStraps)
		{
			lineRenderer.SetPosition(1, position);
		}
		foreach (LineRenderer lineRenderer2 in this.FStraps)
		{
			lineRenderer2.SetPosition(1, position2);
		}
	}

	[ContextMenu("Start")]
	public void StartDyno()
	{
		if (this.dynoRoutine != null)
		{
			base.StopCoroutine(this.dynoRoutine);
		}
		this.dynoRoutine = base.StartCoroutine(this.DoDyno());
	}

	private IEnumerator DoDyno()
	{
		for (float f = 0f; f <= 1f; f += 0.01f)
		{
			this.DynoRatio = f;
			yield return null;
		}
		this.DynoRatio = 1f;
		yield return new WaitForSeconds(3f);
		float maxHP = this.car.GetMaxTorque();
		this.BuildCurve(maxHP);
		for (float f2 = 1f; f2 >= 0f; f2 -= 0.005f)
		{
			this.DynoRatio = f2;
			yield return null;
		}
		this.DynoRatio = 0f;
		this.dynoRoutine = null;
		yield break;
	}

	public static DynoRoomController Instance;

	public WMG_Axis_Graph graph;

	public WMG_Series Line0;

	public WMG_Series Line1;

	public Transform CarPos;

	public LineRenderer[] RStraps;

	public LineRenderer[] FStraps;

	public Transform FrontDynoStand;

	public Transform RearDynoStand;

	public Transform[] Rollers;

	public AnimationCurve GasTruckCurve;

	public AnimationCurve DieselTruckCurve;

	public AnimationCurve AtvCurve;

	public AnimationCurve BikeCurve;

	public AnimationCurve CrawlersCurve;

	public AnimationCurve UtvCurve;

	public float TorqueCurveMultiplier = 0.5f;

	public float TrucksHPMultiplier = 2f;

	public float AtvsHPMultiplier = 2f;

	public float UtvsHPMultiplier = 2f;

	public float BikesHPMultiplier = 2f;

	public float CrawlersHPMultiplier = 2f;

	private float DynoRatio;

	private CarController car;

	private Rigidbody carRigidbody;

	private EngineController engineController;

	private Coroutine dynoRoutine;

	private List<Vector2> HPCurve;

	private List<Vector2> TQCurve;
}
