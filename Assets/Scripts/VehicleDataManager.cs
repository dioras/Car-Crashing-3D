using System;
using System.Collections.Generic;
using CustomVP;
using UnityEngine;

public class VehicleDataManager : MonoBehaviour
{
	public bool IsAvailable
	{
		get
		{
			DateTime dateTime = new DateTime(DataStore.GetLong("UpdateOpenedOn" + this.UpdateID.ToString()));
			StatsData statsData = GameState.LoadStatsData();
			return statsData.IsMember || ((this.vehicleAvailability != Availability.MembersAndEveryoneAfterDate || !(this.CurrentDateTime() < dateTime.AddDays((double)this.AvailableAfter.Days))) && (this.vehicleAvailability != Availability.MembersOnly || statsData.IsMember));
		}
	}

	[ContextMenu("Turn into dummy car")]
	public void TurnIntoDummyCar()
	{
		UnityEngine.Object.DestroyImmediate(base.GetComponent<CarController>());
		TankController component = base.GetComponent<TankController>();
		if (component != null)
		{
			UnityEngine.Object.DestroyImmediate(component);
		}
		UnityEngine.Object.DestroyImmediate(base.GetComponent<CarEffects>());
		UnityEngine.Object.DestroyImmediate(base.GetComponent<SuspensionController>());
		UnityEngine.Object.DestroyImmediate(base.GetComponent<PhotonTransformView>());
		UnityEngine.Object.DestroyImmediate(base.GetComponent<PhotonView>());
		UnityEngine.Object.DestroyImmediate(base.GetComponent<IKDriverController>());
		UnityEngine.Object.DestroyImmediate(base.GetComponent<LightsController>());
		UnityEngine.Object.DestroyImmediate(base.GetComponent<RammingChecker>());
		UnityEngine.Object.DestroyImmediate(base.GetComponent<EngineController>());
	}

	public void LoadOnTrailer(TrailerController trailer, bool turnToDummy = true)
	{
		this.AlignOnTrailer(trailer);
		this.joint = base.gameObject.AddComponent<ConfigurableJoint>();
		this.joint.connectedBody = trailer.GetComponent<Rigidbody>();
		ConfigurableJoint configurableJoint = this.joint;
		ConfigurableJointMotion configurableJointMotion = ConfigurableJointMotion.Locked;
		this.joint.zMotion = configurableJointMotion;
		configurableJoint.xMotion = configurableJointMotion;
		this.joint.yMotion = ConfigurableJointMotion.Limited;
		if (turnToDummy)
		{
			ConfigurableJoint configurableJoint2 = this.joint;
			configurableJointMotion = ConfigurableJointMotion.Limited;
			this.joint.angularZMotion = configurableJointMotion;
			configurableJoint2.angularXMotion = configurableJointMotion;
		}
		else
		{
			ConfigurableJoint configurableJoint3 = this.joint;
			configurableJointMotion = ConfigurableJointMotion.Locked;
			this.joint.angularZMotion = configurableJointMotion;
			configurableJoint3.angularXMotion = configurableJointMotion;
		}
		this.joint.angularYMotion = ConfigurableJointMotion.Locked;
		SoftJointLimit lowAngularXLimit = this.joint.lowAngularXLimit;
		lowAngularXLimit.limit = -15f;
		this.joint.lowAngularXLimit = lowAngularXLimit;
		SoftJointLimit highAngularXLimit = this.joint.highAngularXLimit;
		highAngularXLimit.limit = 15f;
		this.joint.highAngularXLimit = highAngularXLimit;
		SoftJointLimit angularZLimit = this.joint.angularZLimit;
		angularZLimit.limit = 25f;
		this.joint.angularZLimit = angularZLimit;
		SoftJointLimit linearLimit = this.joint.linearLimit;
		linearLimit.limit = 0.5f;
		this.joint.linearLimit = linearLimit;
		this.joint.enableCollision = true;
		if (turnToDummy)
		{
			this.TurnIntoDummyCar();
			if (this.startMass == 0f)
			{
				this.startMass = base.GetComponent<Rigidbody>().mass;
			}
			base.GetComponent<Rigidbody>().mass = this.startMass * this.massOnTrailerModifier;
		}
		this.trailerImOn = trailer;
		trailer.VehicleLoadedOnMe(base.gameObject);
	}

	public Vector3 AlignOnTrailer(TrailerController trailer)
	{
		base.transform.rotation = trailer.transform.rotation;
		Vector3 vector = Vector3.zero;
		SuspensionController component = base.GetComponent<SuspensionController>();
		TankController component2 = base.GetComponent<TankController>();
		if (component2 == null)
		{
			if (component != null)
			{
				GameObject[] getAllWheels = component.GetAllWheels;
				foreach (GameObject gameObject in getAllWheels)
				{
					vector += gameObject.transform.position;
				}
				vector /= (float)getAllWheels.Length;
			}
			else
			{
				WheelComponent[] componentsInChildren = base.GetComponentsInChildren<WheelComponent>();
				if (componentsInChildren != null && componentsInChildren.Length > 0)
				{
					foreach (WheelComponent wheelComponent in componentsInChildren)
					{
						vector += wheelComponent.transform.position;
					}
					vector /= (float)componentsInChildren.Length;
				}
			}
		}
		if (vector == Vector3.zero)
		{
			vector = base.transform.position;
		}
		Vector3 b = base.transform.position - vector;
		base.transform.position = trailer.transform.TransformPoint(trailer.center) + b;
		Utility.AlignHeightOnTrailer(base.transform, trailer);
		return trailer.transform.InverseTransformPoint(base.transform.position);
	}

	public void SaveOnlyGlossinessData()
	{
		VehicleData vehicleData = new VehicleData();
		string @string = DataStore.GetString(this.VehicleID);
		if (@string == string.Empty)
		{
			return;
		}
		vehicleData = (VehicleData)XmlSerialization.DeserializeData<VehicleData>(@string);
		BodyPartsData bodyPartsData = (BodyPartsData)XmlSerialization.DeserializeData<BodyPartsData>(vehicleData.BodyPartsSwitcherXMLData);
		bodyPartsData.GlossyPaint = base.GetComponent<BodyPartsSwitcher>().GlossyPaint;
		bodyPartsData.GlossyPaintPurchased = base.GetComponent<BodyPartsSwitcher>().GlossyPaintPurchased;
		string bodyPartsSwitcherXMLData = XmlSerialization.SerializeData<BodyPartsData>(bodyPartsData);
		vehicleData.BodyPartsSwitcherXMLData = bodyPartsSwitcherXMLData;
		string value = XmlSerialization.SerializeData<VehicleData>(vehicleData);
		DataStore.SetString(this.VehicleID, value);
	}

	public void SaveDualliesData()
	{
		VehicleData vehicleData = new VehicleData();
		string @string = DataStore.GetString(this.VehicleID);
		if (@string == string.Empty)
		{
			return;
		}
		vehicleData = (VehicleData)XmlSerialization.DeserializeData<VehicleData>(@string);
		CarControllerData carControllerData = (CarControllerData)XmlSerialization.DeserializeData<CarControllerData>(vehicleData.CarControllerXMLData);
		carControllerData.frontDuallyPurchased = base.GetComponent<CarController>().frontDuallyPurchased;
		carControllerData.rearDuallyPurchased = base.GetComponent<CarController>().rearDuallyPurchased;
		string carControllerXMLData = XmlSerialization.SerializeData<CarControllerData>(carControllerData);
		vehicleData.CarControllerXMLData = carControllerXMLData;
		string value = XmlSerialization.SerializeData<VehicleData>(vehicleData);
		DataStore.SetString(this.VehicleID, value);
	}

	public TimeSpan TimeLeft
	{
		get
		{
			DateTime dateTime = new DateTime(DataStore.GetLong("UpdateOpenedOn" + this.UpdateID.ToString()));
			return dateTime.AddDays((double)this.AvailableAfter.Days) - this.CurrentDateTime();
		}
	}

	private DateTime CurrentDateTime()
	{
		return DateTime.Now;
	}

	private void Start()
	{
		this.camController = CameraController.Instance;
		this.menuManager = MenuManager.Instance;
	}

	public VehicleData GetVehicleData()
	{
		VehicleData vehicleData = new VehicleData();
		if (this.vehicleType != VehicleType.Trailer)
		{
			vehicleData.SuspensionControllerXMLData = base.GetComponent<SuspensionController>().ExportData();
			vehicleData.CarControllerXMLData = base.GetComponent<CarController>().ExportData();
		}
		vehicleData.VehicleName = base.gameObject.name;
		return vehicleData;
	}

	public void SaveDirtinessOnly(int dirtiness)
	{
	}

	[ContextMenu("Save vehicle data")]
	public void SaveVehicleData()
	{
		VehicleData vehicleData = new VehicleData();
		if (this.vehicleType != VehicleType.Trailer)
		{
			vehicleData.SuspensionControllerXMLData = base.GetComponent<SuspensionController>().ExportData();
			vehicleData.CarControllerXMLData = base.GetComponent<CarController>().ExportData();
			vehicleData.BodyPartsSwitcherXMLData = base.GetComponent<BodyPartsSwitcher>().ExportData();
		}
		vehicleData.PurchasedPartsList = this.PurchasedPartsList;
		vehicleData.VehicleName = base.gameObject.name;
		vehicleData.equippedTrailer = this.equipped;
		string value = XmlSerialization.SerializeData<VehicleData>(vehicleData);
		DataStore.SetString(this.VehicleID, value);
	}

	[ContextMenu("Load vehicle data")]
	public void LoadVehicleData()
	{
		VehicleData vehicleData = new VehicleData();
		string @string = DataStore.GetString(this.VehicleID);
		if (@string == string.Empty)
		{
			return;
		}
		vehicleData = (VehicleData)XmlSerialization.DeserializeData<VehicleData>(@string);
		if (this.vehicleType != VehicleType.Trailer)
		{
			base.GetComponent<SuspensionController>().ImportData(vehicleData.SuspensionControllerXMLData);
			base.GetComponent<BodyPartsSwitcher>().ImportData(vehicleData.BodyPartsSwitcherXMLData);
			base.GetComponent<CarController>().ImportData(vehicleData.CarControllerXMLData);
		}
		else
		{
			this.equipped = vehicleData.equippedTrailer;
		}
		this.PurchasedPartsList = vehicleData.PurchasedPartsList;
	}

	public void LoadVehicleDataFromString(string vehicleDataString)
	{
		VehicleData vehicleData = new VehicleData();
		try
		{
			vehicleData = (VehicleData)XmlSerialization.DeserializeData<VehicleData>(vehicleDataString);
			if (this.vehicleType != VehicleType.Trailer)
			{
				base.GetComponent<SuspensionController>().ImportData(vehicleData.SuspensionControllerXMLData);
				base.GetComponent<BodyPartsSwitcher>().ImportData(vehicleData.BodyPartsSwitcherXMLData);
			}
			else
			{
				this.equipped = vehicleData.equippedTrailer;
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError("Could not load vehicle data from string: " + ex.Message);
		}
	}

	private void OnMouseDown()
	{
		if (this.menuManager != null)
		{
			if (this.trailerImOn == null)
			{
				this.menuManager.ChangeCurrentVehicle(this, false);
			}
			else
			{
				this.menuManager.ChangeCurrentVehicle(this.trailerImOn.GetComponent<VehicleDataManager>(), false);
			}
		}
		else
		{
			if (Time.time - this.lastTapTime < 0.5f && VehicleLoader.Instance.droneMode)
			{
				PhotonView component = base.GetComponent<PhotonView>();
				if (component != null && !component.isMine)
				{
					VehicleLoader.Instance.carToFollow = base.gameObject;
					CarUIControl.Instance.ShowFollowVehicleWindow(component.owner.NickName);
				}
			}
			this.lastTapTime = Time.time;
		}
	}

	public VehicleType vehicleType;

	public int MoneyPrice;

	public int GoldPrice;

	public float CashPrice;

	public int UpdateID;

	[HideInInspector]
	public int GaragePlaceID;

	public string VehicleID;

	[HideInInspector]
	public bool Bought;

	[HideInInspector]
	public List<string> PurchasedPartsList;

	private CameraController camController;

	private MenuManager menuManager;

	public PunTeams.Team Team = PunTeams.Team.blue;

	[Header("Availability")]
	public Availability vehicleAvailability;

	public DateClass AvailableAfter;

	[Header("Trailer")]
	public bool equipped;

	private TrailerController trailerImOn;

	public float massOnTrailerModifier = 1f;

	private float startMass;

	private ConfigurableJoint joint;

	private float lastTapTime;
}
