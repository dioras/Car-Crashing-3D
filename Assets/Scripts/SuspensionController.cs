using System;
using System.Collections;
using System.Collections.Generic;
using CustomVP;
using UnityEngine;

public class SuspensionController : MonoBehaviour
{
	public GameObject[] GetAllWheels
	{
		get
		{
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < this.FrontRims.Length; i++)
			{
				list.Add(this.FrontRims[i]);
			}
			for (int j = 0; j < this.RearRims.Length; j++)
			{
				list.Add(this.RearRims[j]);
			}
			return list.ToArray();
		}
	}

	private void Update()
	{
		if (this.UpdateSuspensionInUpdate && this.carController == null)
		{
			this.UpdateSuspensions(0f, 0f);
		}
		if (this.multiplayerTraileredCar)
		{
			this.UpdateSuspensions(0f, 0f);
		}
	}

	private void Awake()
	{
		this.carController = base.GetComponent<CarController>();
	}

	public void SetStockWheels()
	{
		this.FrontWheelsControls.SetStock();
		this.RearWheelsControls.SetStock();
		this.LoadWheels();
		this.DoWheelsSize();
	}

	public void SetRandomWheels()
	{
		this.FrontWheelsControls.SetRandom(this.CurrentFrontSuspension);
		this.RearWheelsControls.SetRandom(this.CurrentRearSuspension);
		this.LoadWheels();
		this.DoWheelsSize();
	}

	public void SetStockSuspensionsValues()
	{
		Suspension[] allSuspensions = this.getAllSuspensions();
		foreach (Suspension suspension in allSuspensions)
		{
			SuspensionValue[] controlValues = suspension.GetControlValues();
			for (int j = 0; j < controlValues.Length; j++)
			{
				SuspensionControlLimit limit = SuspensionControlLimits.getLimit(suspension.gameObject.name, controlValues[j].ValueName);
				if (limit != null && limit.ModifiableByPlayer)
				{
					controlValues[j].FloatValue = limit.fDef;
					controlValues[j].IntValue = limit.iDef;
				}
			}
			suspension.OnValidate();
		}
		this.SetStockWheels();
	}

	public void NextFrontSuspension()
	{
		if (this.frontSuspension > this.FrontSuspensions.Count - 2)
		{
			return;
		}
		this.frontSuspension++;
		this.SetFrontSuspension(this.frontSuspension);
	}

	public void PrevFrontSuspension()
	{
		if (this.frontSuspension == 0)
		{
			return;
		}
		this.frontSuspension--;
		this.SetFrontSuspension(this.frontSuspension);
	}

	public void NextRearSuspension()
	{
		if (this.rearSuspension > this.RearSuspensions.Count - 2)
		{
			return;
		}
		this.rearSuspension++;
		this.SetRearSuspension(this.rearSuspension);
	}

	public void PrevRearSuspension()
	{
		if (this.rearSuspension == 0)
		{
			return;
		}
		this.rearSuspension--;
		this.SetRearSuspension(this.rearSuspension);
	}

	public void TurnToMultiplayerCar()
	{
		if (this.multiplayerCar)
		{
			return;
		}
		WheelComponent[] componentsInChildren = base.GetComponentsInChildren<WheelComponent>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			UnityEngine.Object.Destroy(componentsInChildren[i].gameObject);
		}
		TankWheelCollider[] componentsInChildren2 = base.GetComponentsInChildren<TankWheelCollider>(true);
		for (int j = 0; j < componentsInChildren2.Length; j++)
		{
			UnityEngine.Object.Destroy(componentsInChildren2[j].gameObject);
		}
		base.GetComponent<Rigidbody>().mass = 300f;
		base.GetComponent<Rigidbody>().useGravity = false;
		UnityEngine.Object.Destroy(base.GetComponent<CarController>());
		UnityEngine.Object.Destroy(base.GetComponent<TankController>());
		if (base.GetComponent<MotorcycleAssistant>() != null)
		{
			base.GetComponent<MotorcycleAssistant>().multiplayer = true;
		}
		this.multiplayerCar = true;
	}

	public void UpdateSuspensions(float SteerAngle, float rpm)
	{
		if (this.CurrentFrontSuspension != null)
		{
			this.CurrentFrontSuspension.UpdateSuspension(SteerAngle, this.FrontWheelsControls.WheelsRadius.FloatValue * this.FrontWheelsControls.DefaultWheelColliderRadius, rpm);
		}
		if (this.CurrentRearSuspension != null)
		{
			this.CurrentRearSuspension.UpdateSuspension(SteerAngle, this.RearWheelsControls.WheelsRadius.FloatValue * this.RearWheelsControls.DefaultWheelColliderRadius, rpm);
		}
	}

	public void FindSuspensions()
	{
		UnityEngine.Debug.Log("FIND SUSPENSIONS");
		this.carController = base.GetComponent<CarController>();
		Suspension[] array = new Suspension[base.GetComponentsInChildren<Suspension>(true).Length];
		array = base.GetComponentsInChildren<Suspension>(true);
		this.FrontSuspensions = new List<Suspension>();
		this.RearSuspensions = new List<Suspension>();
		foreach (Suspension suspension in array)
		{
			if (suspension.side == Side.Front)
			{
				this.FrontSuspensions.Add(suspension);
			}
			else
			{
				this.RearSuspensions.Add(suspension);
			}
		}
		this.frontSuspension = 0;
		this.rearSuspension = 0;
		this.SetFrontSuspension(this.frontSuspension);
		this.SetRearSuspension(this.rearSuspension);
		base.GetComponent<BodyPartsSwitcher>().CheckDynamicParts();
		base.GetComponent<BodyPartsSwitcher>().CheckTriggerPartGroups();
	}

	public void SetRearSuspension(int ID)
	{
		if (this.RearSuspensions == null)
		{
			return;
		}
		if (this.RearSuspensions.Count == 0)
		{
			return;
		}
		for (int i = 0; i < this.RearSuspensions.Count; i++)
		{
			this.RearSuspensions[i].gameObject.SetActive(i == ID);
		}
		this.CurrentRearSuspension = this.RearSuspensions[ID];
		if (this.carController != null && this.CurrentRearSuspension.wheelColliders.Length > 0)
		{
			if (!this.CurrentRearSuspension.DirtBikeWheels && this.carController.GetComponent<VehicleDataManager>().vehicleType != VehicleType.Bike)
			{
				this.carController.wheels[2].wc = this.CurrentRearSuspension.wheelColliders[0];
				this.carController.wheels[3].wc = this.CurrentRearSuspension.wheelColliders[1];
				this.SetupCarController(this.CurrentRearSuspension.wheelColliders.Length + 2);
				if (this.CurrentRearSuspension.wheelColliders.Length > 2)
				{
					this.carController.wheels[4].wc = this.CurrentRearSuspension.wheelColliders[2];
					this.carController.wheels[5].wc = this.CurrentRearSuspension.wheelColliders[3];
				}
			}
			else
			{
				this.carController.wheels[1].wc = this.CurrentRearSuspension.wheelColliders[0];
			}
		}
		this.rearSuspension = ID;
		this.CurrentRearSuspension.OnValidate();
		this.LoadWheels();
		this.DoWheelsSize();
		base.GetComponent<BodyPartsSwitcher>().CheckDynamicParts();
		base.GetComponent<BodyPartsSwitcher>().CheckTriggerPartGroups();
	}

	public void SetFrontSuspension(int ID)
	{
		if (this.FrontSuspensions == null)
		{
			return;
		}
		if (this.FrontSuspensions.Count == 0)
		{
			return;
		}
		for (int i = 0; i < this.FrontSuspensions.Count; i++)
		{
			this.FrontSuspensions[i].gameObject.SetActive(i == ID);
		}
		this.CurrentFrontSuspension = this.FrontSuspensions[ID];
		if (this.carController != null && this.CurrentFrontSuspension.wheelColliders.Length > 0)
		{
			this.carController.wheels[0].wc = this.CurrentFrontSuspension.wheelColliders[0];
			if (this.CurrentFrontSuspension.wheelColliders.Length > 1)
			{
				this.carController.wheels[1].wc = this.CurrentFrontSuspension.wheelColliders[1];
			}
			this.carController.OnValidate();
		}
		this.frontSuspension = ID;
		this.CurrentFrontSuspension.OnValidate();
		this.LoadWheels();
		this.DoWheelsSize();
		base.GetComponent<BodyPartsSwitcher>().CheckTriggerPartGroups();
	}

	public void UpdatePrefabs()
	{
		Suspension[] allSuspensions = this.getAllSuspensions();
		foreach (Suspension suspension in allSuspensions)
		{
			if (Resources.Load("Suspensions/" + suspension.name) != null)
			{
				Vector3 position = suspension.transform.position;
				Quaternion rotation = suspension.transform.rotation;
				Vector3 localScale = suspension.transform.localScale;
				Transform parent = suspension.transform.parent;
				string name = suspension.transform.name;
				GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Suspensions/" + suspension.name, typeof(GameObject))) as GameObject;
				gameObject.layer = base.gameObject.layer;
				foreach (Renderer renderer in gameObject.GetComponentsInChildren<Renderer>())
				{
					renderer.gameObject.layer = base.gameObject.layer;
				}
				gameObject.transform.parent = parent;
				gameObject.transform.position = position;
				gameObject.transform.rotation = rotation;
				gameObject.transform.localScale = localScale;
				gameObject.name = name;
				gameObject.GetComponent<Suspension>().SetControlValues(suspension.GetControlValues());
				suspension.transform.parent = null;
				base.StartCoroutine(this.ForcedDestroy(suspension.gameObject));
			}
		}
		this.FindSuspensions();
		Resources.UnloadUnusedAssets();
	}

	[ContextMenu("Load wheels")]
	public void LoadWheels()
	{
		if (this.CurrentFrontSuspension != null && (!this.CurrentFrontSuspension.DontLoadWheels || this.CurrentFrontSuspension.DirtBikeWheels || this.CurrentFrontSuspension.ATVWheels))
		{
			if (!this.FrontWheelsControls.TankTracks)
			{
				this.LoadFrontWheels();
			}
			else
			{
				this.LoadFrontTankTracks();
			}
		}
		if (this.CurrentRearSuspension != null && (!this.CurrentRearSuspension.DontLoadWheels || this.CurrentRearSuspension.DirtBikeWheels || this.CurrentFrontSuspension.ATVWheels))
		{
			SuspensionControlLimit limit = SuspensionControlLimits.getLimit(this.CurrentRearSuspension.gameObject.name, "Rim");
			if (this.RearWheelsControls.Rim.IntValue > limit.iMax)
			{
				this.RearWheelsControls.Rim.IntValue = 0;
			}
			if (!this.RearWheelsControls.TankTracks)
			{
				this.LoadRearWheels();
			}
			else
			{
				this.LoadRearTankTracks();
			}
			this.LoadSpareWheels();
		}
		this.DoWheelsSize();
		if (this.carController != null)
		{
			this.carController.FrontInstalledTiresID = this.FrontWheelsControls.Tire.IntValue;
			this.carController.RearInstalledTiresID = this.RearWheelsControls.Tire.IntValue;
		}
		if (Application.isPlaying)
		{
			base.GetComponent<BodyPartsSwitcher>().UpdateDirtiness();
		}
	}

	private void LoadFrontTankTracks()
	{
		if (this.CurrentFrontSuspension == null)
		{
			return;
		}
		if (this.CurrentFrontSuspension.WheelHolders == null)
		{
			return;
		}
		foreach (Transform transform in this.CurrentFrontSuspension.WheelHolders)
		{
			if (transform != null)
			{
				for (int j = 0; j < transform.childCount; j++)
				{
					if (!Application.isPlaying)
					{
						base.StartCoroutine(this.ForcedDestroy(transform.GetChild(j).gameObject));
					}
					else
					{
						UnityEngine.Object.DestroyImmediate(transform.GetChild(j).gameObject);
					}
				}
			}
		}
		if (this.FrontRims != null && this.FrontRims.Length > 0)
		{
			for (int k = 0; k < this.FrontRims.Length; k++)
			{
				if (!Application.isPlaying)
				{
					base.StartCoroutine(this.ForcedDestroy(this.FrontRims[k]));
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(this.FrontRims[k]);
				}
			}
		}
		if (this.FrontTires != null && this.FrontTires.Length > 0)
		{
			for (int l = 0; l < this.FrontTires.Length; l++)
			{
				if (!Application.isPlaying)
				{
					base.StartCoroutine(this.ForcedDestroy(this.FrontTires[l]));
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(this.FrontTires[l]);
				}
			}
		}
		this.FrontRims = new GameObject[this.CurrentFrontSuspension.WheelHolders.Length];
		this.FrontTires = null;
		this.FrontTireSizeBones = null;
		for (int m = 0; m < this.FrontRims.Length; m++)
		{
			this.FrontRims[m] = (UnityEngine.Object.Instantiate(Resources.Load("TankTracks/TankTracks", typeof(GameObject))) as GameObject);
			this.FrontRims[m].layer = base.gameObject.layer;
			this.FrontRims[m].transform.parent = base.transform;
			this.FrontRims[m].transform.position = this.CurrentFrontSuspension.WheelHolders[m].position;
			this.FrontRims[m].transform.localRotation = Quaternion.identity;
			TankTracksController component = this.FrontRims[m].GetComponent<TankTracksController>();
			component.wc = this.CurrentFrontSuspension.wheelColliders[m];
			component.wheelHolder = this.CurrentFrontSuspension.WheelHolders[m];
			component.MasterWheel.parent = this.CurrentFrontSuspension.WheelHolders[m];
			component.MasterWheel.localPosition = Vector3.zero;
			component.MasterWheel.localEulerAngles = Vector3.zero;
		}
	}

	private void LoadRearTankTracks()
	{
		if (this.CurrentRearSuspension == null)
		{
			return;
		}
		if (this.CurrentRearSuspension.WheelHolders == null)
		{
			return;
		}
		foreach (Transform transform in this.CurrentRearSuspension.WheelHolders)
		{
			if (transform != null)
			{
				for (int j = 0; j < transform.childCount; j++)
				{
					if (!Application.isPlaying)
					{
						base.StartCoroutine(this.ForcedDestroy(transform.GetChild(j).gameObject));
					}
					else
					{
						UnityEngine.Object.DestroyImmediate(transform.GetChild(j).gameObject);
					}
				}
			}
		}
		if (this.RearRims != null && this.RearRims.Length > 0)
		{
			for (int k = 0; k < this.RearRims.Length; k++)
			{
				if (!Application.isPlaying)
				{
					base.StartCoroutine(this.ForcedDestroy(this.RearRims[k]));
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(this.RearRims[k]);
				}
			}
		}
		if (this.RearTires != null && this.RearTires.Length > 0)
		{
			for (int l = 0; l < this.RearTires.Length; l++)
			{
				if (!Application.isPlaying)
				{
					base.StartCoroutine(this.ForcedDestroy(this.RearTires[l]));
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(this.RearTires[l]);
				}
			}
		}
		this.RearRims = new GameObject[this.CurrentRearSuspension.WheelHolders.Length];
		this.RearTires = null;
		this.RearTireSizeBones = null;
		for (int m = 0; m < this.RearRims.Length; m++)
		{
			this.RearRims[m] = (UnityEngine.Object.Instantiate(Resources.Load("TankTracks/TankTracks", typeof(GameObject))) as GameObject);
			this.RearRims[m].layer = base.gameObject.layer;
			this.RearRims[m].transform.parent = base.transform;
			this.RearRims[m].transform.position = this.CurrentRearSuspension.WheelHolders[m].position;
			this.RearRims[m].transform.localRotation = Quaternion.identity;
			TankTracksController component = this.RearRims[m].GetComponent<TankTracksController>();
			component.wc = this.CurrentRearSuspension.wheelColliders[m];
			component.wheelHolder = this.CurrentRearSuspension.WheelHolders[m];
			component.MasterWheel.parent = this.CurrentRearSuspension.WheelHolders[m];
			component.MasterWheel.localPosition = Vector3.zero;
			component.MasterWheel.localEulerAngles = Vector3.zero;
		}
	}

	private void LoadFrontWheels()
	{
		if (this.CurrentFrontSuspension == null)
		{
			return;
		}
		if (this.CurrentFrontSuspension.WheelHolders == null)
		{
			return;
		}
		bool duallyTires = this.FrontWheelsControls.duallyTires;
		foreach (Transform transform in this.CurrentFrontSuspension.WheelHolders)
		{
			if (transform != null)
			{
				for (int j = 0; j < transform.childCount; j++)
				{
					UnityEngine.Object.DestroyImmediate(transform.GetChild(j).gameObject);
				}
			}
		}
		if (this.FrontRims != null && this.FrontRims.Length > 0)
		{
			for (int k = 0; k < this.FrontRims.Length; k++)
			{
				UnityEngine.Object.DestroyImmediate(this.FrontRims[k]);
			}
		}
		this.FrontRims = new GameObject[this.CurrentFrontSuspension.WheelHolders.Length];
		string str = "Rim";
		if (this.CurrentFrontSuspension.DirtBikeWheels)
		{
			str = "BikeRim";
		}
		if (this.CurrentFrontSuspension.ATVWheels)
		{
			str = "ATVRim";
		}
		for (int l = 0; l < this.FrontRims.Length; l++)
		{
			this.FrontRims[l] = (UnityEngine.Object.Instantiate(Resources.Load("Rims/" + str + this.FrontWheelsControls.Rim.IntValue.ToString(), typeof(GameObject))) as GameObject);
			this.FrontRims[l].layer = base.gameObject.layer;
			this.FrontRims[l].transform.parent = this.CurrentFrontSuspension.WheelHolders[l];
			this.FrontRims[l].transform.localPosition = Vector3.zero;
			this.FrontRims[l].transform.localRotation = Quaternion.identity;
			this.FrontRims[l].transform.localScale = new Vector3((!duallyTires) ? 1f : 2.05f, 1f, 1f);
		}
		if (this.FrontTires != null && this.FrontTires.Length > 0)
		{
			for (int m = 0; m < this.FrontTires.Length; m++)
			{
				UnityEngine.Object.DestroyImmediate(this.FrontTires[m]);
			}
		}
		this.FrontTires = new GameObject[this.CurrentFrontSuspension.WheelHolders.Length + ((!duallyTires) ? 0 : 2)];
		this.FrontTireSizeBones = new List<Transform>();
		string str2 = "Tire";
		if (this.CurrentFrontSuspension.DirtBikeWheels)
		{
			str2 = "BikeTire";
		}
		if (this.CurrentFrontSuspension.ATVWheels)
		{
			str2 = "ATVTire";
		}
		for (int n = 0; n < this.FrontTires.Length; n++)
		{
			this.FrontTires[n] = (UnityEngine.Object.Instantiate(Resources.Load("Tires/" + str2 + this.FrontWheelsControls.Tire.IntValue.ToString(), typeof(GameObject))) as GameObject);
			this.FrontTires[n].layer = base.gameObject.layer;
			foreach (Renderer renderer in this.FrontTires[n].GetComponentsInChildren<Renderer>())
			{
				renderer.gameObject.layer = base.gameObject.layer;
			}
			int num2 = n;
			if (duallyTires)
			{
				num2 = ((n <= 1) ? 0 : 1);
			}
			float x = 0f;
			if (duallyTires)
			{
				float num3 = 0.053f;
				if (n == 0 || n == 2)
				{
					x = num3;
				}
				else
				{
					x = -num3;
				}
			}
			this.FrontTires[n].transform.parent = this.CurrentFrontSuspension.WheelHolders[num2];
			this.FrontTires[n].transform.localPosition = new Vector3(x, 0f, 0f);
			this.FrontTires[n].transform.localRotation = Quaternion.identity;
			this.FrontTires[n].transform.localScale = Vector3.one;
			this.FrontTireSizeBones.Add(this.FrontTires[n].transform.Find("[BONE]Size"));
		}
	}

	private void LoadRearWheels()
	{
		if (this.CurrentRearSuspension == null)
		{
			return;
		}
		if (this.CurrentRearSuspension.WheelHolders == null)
		{
			return;
		}
		bool duallyTires = this.RearWheelsControls.duallyTires;
		foreach (Transform transform in this.CurrentRearSuspension.WheelHolders)
		{
			if (transform != null)
			{
				for (int j = 0; j < transform.childCount; j++)
				{
					UnityEngine.Object.DestroyImmediate(transform.GetChild(j).gameObject);
				}
			}
		}
		if (this.RearRims != null && this.RearRims.Length > 0)
		{
			for (int k = 0; k < this.RearRims.Length; k++)
			{
				UnityEngine.Object.DestroyImmediate(this.RearRims[k]);
			}
		}
		this.RearRims = new GameObject[this.CurrentRearSuspension.WheelHolders.Length];
		string str = "Rim";
		if (this.CurrentRearSuspension.DirtBikeWheels)
		{
			str = "BikeRim";
		}
		if (this.CurrentRearSuspension.ATVWheels)
		{
			str = "ATVRim";
		}
		for (int l = 0; l < this.RearRims.Length; l++)
		{
			this.RearRims[l] = (UnityEngine.Object.Instantiate(Resources.Load("Rims/" + str + this.RearWheelsControls.Rim.IntValue.ToString(), typeof(GameObject))) as GameObject);
			this.RearRims[l].layer = base.gameObject.layer;
			this.RearRims[l].transform.parent = this.CurrentRearSuspension.WheelHolders[l];
			this.RearRims[l].transform.localPosition = Vector3.zero;
			this.RearRims[l].transform.localRotation = Quaternion.identity;
			this.RearRims[l].transform.localScale = new Vector3((!duallyTires) ? 1f : 2.05f, 1f, 1f);
		}
		if (this.RearTires != null && this.RearTires.Length > 0)
		{
			for (int m = 0; m < this.RearTires.Length; m++)
			{
				UnityEngine.Object.DestroyImmediate(this.RearTires[m]);
			}
		}
		int num = 0;
		if (duallyTires)
		{
			num = this.CurrentRearSuspension.WheelHolders.Length;
		}
		this.RearTires = new GameObject[this.CurrentRearSuspension.WheelHolders.Length + num];
		this.RearTireSizeBones = new List<Transform>();
		string str2 = "Tire";
		if (this.CurrentRearSuspension.DirtBikeWheels)
		{
			str2 = "BikeTire";
		}
		if (this.CurrentRearSuspension.ATVWheels)
		{
			str2 = "ATVTire";
		}
		for (int n = 0; n < this.RearTires.Length; n++)
		{
			this.RearTires[n] = (UnityEngine.Object.Instantiate(Resources.Load("Tires/" + str2 + this.RearWheelsControls.Tire.IntValue.ToString())) as GameObject);
			this.RearTires[n].layer = base.gameObject.layer;
			foreach (Renderer renderer in this.RearTires[n].GetComponentsInChildren<Renderer>())
			{
				renderer.gameObject.layer = base.gameObject.layer;
			}
			int num3 = n;
			if (duallyTires)
			{
				if (this.CurrentRearSuspension.WheelHolders.Length == 2)
				{
					num3 = ((n < this.RearTires.Length / 2) ? 0 : 1);
				}
				if (this.CurrentRearSuspension.WheelHolders.Length == 4)
				{
					num3 = n / 2;
				}
			}
			float x = 0f;
			if (duallyTires)
			{
				float num4 = 0.053f;
				if (n == 0 || n == 2)
				{
					x = num4;
				}
				else
				{
					x = -num4;
				}
			}
			this.RearTires[n].transform.parent = this.CurrentRearSuspension.WheelHolders[num3];
			this.RearTires[n].transform.localScale = Vector3.one;
			this.RearTires[n].transform.localPosition = new Vector3(x, 0f, 0f);
			this.RearTires[n].transform.localRotation = Quaternion.identity;
			this.RearTireSizeBones.Add(this.RearTires[n].transform.Find("[BONE]Size"));
		}
	}

	private void LoadSpareWheels()
	{
		BodyPartsSwitcher component = base.GetComponent<BodyPartsSwitcher>();
		if (this.SpareWheelHolders == null)
		{
			return;
		}
		if (this.SpareWheelHolders.Length == 0)
		{
			return;
		}
		if (component != null)
		{
			foreach (GameObject gameObject in this.SpareWheelHolders)
			{
				if (gameObject != null)
				{
					for (int j = 0; j < gameObject.transform.childCount; j++)
					{
						if (!Application.isPlaying)
						{
							base.StartCoroutine(this.ForcedDestroy(gameObject.transform.GetChild(j).gameObject));
						}
						else
						{
							UnityEngine.Object.DestroyImmediate(gameObject.transform.GetChild(j).gameObject);
						}
					}
				}
			}
		}
		if (this.SpareRims != null && this.SpareRims.Length > 0)
		{
			for (int k = 0; k < this.SpareRims.Length; k++)
			{
				if (!Application.isPlaying)
				{
					base.StartCoroutine(this.ForcedDestroy(this.SpareRims[k]));
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(this.SpareRims[k]);
				}
			}
		}
		this.SpareRims = new GameObject[this.SpareWheelHolders.Length];
		for (int l = 0; l < this.SpareRims.Length; l++)
		{
			if (this.SpareWheelHolders[l] != null)
			{
				this.SpareRims[l] = (UnityEngine.Object.Instantiate(Resources.Load("Rims/Rim" + this.RearWheelsControls.Rim.IntValue.ToString(), typeof(GameObject))) as GameObject);
				this.SpareRims[l].layer = base.gameObject.layer;
				this.SpareRims[l].transform.parent = this.SpareWheelHolders[l].transform;
				this.SpareRims[l].transform.localPosition = Vector3.zero;
				this.SpareRims[l].transform.localRotation = Quaternion.identity;
				this.SpareRims[l].transform.localScale = Vector3.one;
			}
		}
		if (this.SpareTires != null && this.SpareTires.Length > 0)
		{
			for (int m = 0; m < this.SpareTires.Length; m++)
			{
				if (!Application.isPlaying)
				{
					base.StartCoroutine(this.ForcedDestroy(this.SpareTires[m]));
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(this.SpareTires[m]);
				}
			}
		}
		this.SpareTires = new GameObject[this.SpareWheelHolders.Length];
		for (int n = 0; n < this.SpareTires.Length; n++)
		{
			if (this.SpareWheelHolders[n] != null)
			{
				this.SpareTires[n] = (UnityEngine.Object.Instantiate(Resources.Load("Tires/Tire" + this.RearWheelsControls.Tire.IntValue.ToString(), typeof(GameObject))) as GameObject);
				this.SpareTires[n].layer = base.gameObject.layer;
				this.SpareTires[n].transform.parent = this.SpareWheelHolders[n].transform;
				this.SpareTires[n].transform.localPosition = Vector3.zero;
				this.SpareTires[n].transform.localRotation = Quaternion.identity;
				this.SpareTires[n].transform.localScale = Vector3.one;
			}
		}
	}

	public void DoWheelsSize()
	{
		if (this.CurrentFrontSuspension != null && !this.CurrentFrontSuspension.DontLoadWheels)
		{
			float floatValue = this.FrontWheelsControls.WheelsWidth.FloatValue;
			float floatValue2 = this.FrontWheelsControls.RimSize.FloatValue;
			float floatValue3 = this.FrontWheelsControls.WheelsRadius.FloatValue;
			if (this.FrontRims != null && !this.FrontWheelsControls.TankTracks)
			{
				foreach (GameObject gameObject in this.FrontRims)
				{
					gameObject.transform.localScale = new Vector3(floatValue * ((!this.FrontWheelsControls.duallyTires) ? 1f : 2.1f), floatValue2 * floatValue3, floatValue2 * floatValue3);
				}
			}
			if (this.FrontTireSizeBones != null && !this.FrontWheelsControls.TankTracks)
			{
				foreach (Transform transform in this.FrontTireSizeBones)
				{
					transform.localScale = new Vector3(1f, floatValue2, floatValue2);
				}
			}
			if (this.FrontTires != null && !this.FrontWheelsControls.TankTracks)
			{
				for (int j = 0; j < this.FrontTires.Length; j++)
				{
					this.FrontTires[j].transform.localScale = new Vector3(floatValue, floatValue3, (base.transform.InverseTransformPoint(this.FrontTires[j].transform.position).x <= 0f) ? floatValue3 : (-floatValue3));
					this.FrontTires[j].transform.localPosition = new Vector3(floatValue * 0.053f * (float)((!this.FrontWheelsControls.duallyTires) ? 0 : 1) * (float)((j != 1 && j != 3) ? -1 : 1), 0f, 0f);
				}
			}
			if (this.carController != null)
			{
				foreach (WheelComponent wheelComponent in this.CurrentFrontSuspension.wheelColliders)
				{
					wheelComponent.wheelRadius = this.FrontWheelsControls.DefaultWheelColliderRadius * floatValue3;
					if (!this.FrontWheelsControls.TankTracks)
					{
						wheelComponent.wheelRadius = this.FrontWheelsControls.DefaultWheelColliderRadius * floatValue3;
					}
					else
					{
						wheelComponent.wheelRadius = this.FrontWheelsControls.DefaultWheelColliderRadius * this.FrontWheelsControls.TankTracksWheelCollidersRadius;
					}
					wheelComponent.OnValidate();
				}
			}
			if ((this.FrontRims == null && this.FrontTires == null) || (this.FrontRims.Length == 0 && this.FrontTires.Length == 0))
			{
				if (this.CurrentFrontSuspension.WheelHolders[0] != null)
				{
					this.CurrentFrontSuspension.WheelHolders[0].localScale = new Vector3(floatValue, floatValue3, floatValue3);
				}
				if (this.CurrentFrontSuspension.WheelHolders[1] != null)
				{
					this.CurrentFrontSuspension.WheelHolders[1].localScale = new Vector3(floatValue, floatValue3, floatValue3);
				}
			}
		}
		if (this.CurrentRearSuspension != null && !this.CurrentRearSuspension.DontLoadWheels)
		{
			float floatValue4 = this.RearWheelsControls.WheelsWidth.FloatValue;
			float floatValue5 = this.RearWheelsControls.RimSize.FloatValue;
			float floatValue6 = this.RearWheelsControls.WheelsRadius.FloatValue;
			if (this.RearRims != null && !this.RearWheelsControls.TankTracks)
			{
				foreach (GameObject gameObject2 in this.RearRims)
				{
					gameObject2.transform.localScale = new Vector3(floatValue4 * ((!this.RearWheelsControls.duallyTires) ? 1f : 2.1f), floatValue5 * floatValue6, floatValue5 * floatValue6);
				}
			}
			if (this.RearTireSizeBones != null && !this.RearWheelsControls.TankTracks)
			{
				foreach (Transform transform2 in this.RearTireSizeBones)
				{
					transform2.localScale = new Vector3(1f, floatValue5, floatValue5);
				}
			}
			if (this.RearTires != null && !this.RearWheelsControls.TankTracks)
			{
				for (int m = 0; m < this.RearTires.Length; m++)
				{
					this.RearTires[m].transform.localScale = new Vector3(floatValue4, floatValue6, (base.transform.InverseTransformPoint(this.RearTires[m].transform.position).x <= 0f) ? floatValue6 : (-floatValue6));
					float x = floatValue4 * 0.053f * (float)((!this.RearWheelsControls.duallyTires) ? 0 : 1) * (float)((m != 1 && m != 3 && m != 5 && m != 7) ? -1 : 1);
					this.RearTires[m].transform.localPosition = new Vector3(x, 0f, 0f);
				}
			}
			if (this.carController != null)
			{
				foreach (WheelComponent wheelComponent2 in this.CurrentRearSuspension.wheelColliders)
				{
					if (!this.RearWheelsControls.TankTracks)
					{
						wheelComponent2.wheelRadius = this.RearWheelsControls.DefaultWheelColliderRadius * floatValue6;
					}
					else
					{
						wheelComponent2.wheelRadius = this.RearWheelsControls.DefaultWheelColliderRadius * this.RearWheelsControls.TankTracksWheelCollidersRadius;
					}
					wheelComponent2.OnValidate();
				}
			}
			if ((this.RearRims == null && this.RearTires == null) || (this.RearRims.Length == 0 && this.RearTires.Length == 0))
			{
				if (this.CurrentRearSuspension.WheelHolders[0] != null)
				{
					this.CurrentRearSuspension.WheelHolders[0].localScale = new Vector3(floatValue4, floatValue6, floatValue6);
				}
				if (this.CurrentRearSuspension.WheelHolders[1] != null)
				{
					this.CurrentRearSuspension.WheelHolders[1].localScale = new Vector3(floatValue4, floatValue6, floatValue6);
				}
			}
		}
	}

	private IEnumerator ForcedDestroy(GameObject go)
	{
		yield return new WaitForSeconds(0f);
		UnityEngine.Object.DestroyImmediate(go);
		yield break;
	}

	private void SetupCarController(int WheelsNumber)
	{
		if (WheelsNumber > this.carController.wheels.Count)
		{
			while (this.carController.wheels.Count < WheelsNumber)
			{
				this.carController.wheels.Add(new _Wheel());
				this.carController.wheels[this.carController.wheels.Count - 1].power = this.carController.RWD;
				this.carController.wheels[this.carController.wheels.Count - 1].inverseSteer = true;
				this.carController.wheels[this.carController.wheels.Count - 1].handbrake = true;
			}
		}
		if (WheelsNumber < this.carController.wheels.Count)
		{
			while (this.carController.wheels.Count > WheelsNumber)
			{
				this.carController.wheels.RemoveAt(this.carController.wheels.Count - 1);
			}
		}
	}

	private Suspension[] getAllSuspensions()
	{
		List<Suspension> list = new List<Suspension>();
		for (int i = 0; i < this.FrontSuspensions.Count; i++)
		{
			list.Add(this.FrontSuspensions[i]);
		}
		for (int j = 0; j < this.RearSuspensions.Count; j++)
		{
			list.Add(this.RearSuspensions[j]);
		}
		return list.ToArray();
	}

	private SuspensionControllerData getSuspensionControllerData()
	{
		SuspensionControllerData suspensionControllerData = new SuspensionControllerData();
		Suspension[] allSuspensions = this.getAllSuspensions();
		suspensionControllerData.AllSuspensionsDatas = new SuspensionData[allSuspensions.Length];
		for (int i = 0; i < allSuspensions.Length; i++)
		{
			suspensionControllerData.AllSuspensionsDatas[i] = new SuspensionData();
			suspensionControllerData.AllSuspensionsDatas[i].AllValues = allSuspensions[i].GetControlValues();
			suspensionControllerData.AllSuspensionsDatas[i].UpgradeStage = allSuspensions[i].UpgradeStage;
			suspensionControllerData.AllSuspensionsDatas[i].SuspensionName = allSuspensions[i].SuspensionName;
		}
		suspensionControllerData.SelectedFrontSuspension = this.frontSuspension;
		suspensionControllerData.SelectedRearSuspension = this.rearSuspension;
		suspensionControllerData.FrontWheelsControls = this.FrontWheelsControls;
		suspensionControllerData.RearWheelsControls = this.RearWheelsControls;
		return suspensionControllerData;
	}

	private SuspensionValue[] ClampSuspensionValues(SuspensionValue[] input, string suspensionName)
	{
		foreach (SuspensionValue suspensionValue in input)
		{
			SuspensionControlLimit limit = SuspensionControlLimits.getLimit(suspensionName, suspensionValue.ValueName);
			if (limit != null)
			{
				if (suspensionValue.valueType == global::ValueType.Float)
				{
					suspensionValue.FloatValue = Mathf.Clamp(suspensionValue.FloatValue, limit.fMin, limit.fMax);
				}
			}
		}
		return input;
	}

	private WheelsControls ClampWheelSizes(WheelsControls wheelControls, string suspensionName)
	{
		SuspensionControlLimit limit = SuspensionControlLimits.getLimit(suspensionName, "Wheels radius");
		if (limit != null)
		{
			wheelControls.WheelsRadius.FloatValue = Mathf.Clamp(wheelControls.WheelsRadius.FloatValue, limit.fMin, limit.fMax);
		}
		SuspensionControlLimit limit2 = SuspensionControlLimits.getLimit(suspensionName, "Wheels width");
		if (limit2 != null)
		{
			wheelControls.WheelsWidth.FloatValue = Mathf.Clamp(wheelControls.WheelsWidth.FloatValue, limit2.fMin, limit2.fMax);
		}
		SuspensionControlLimit limit3 = SuspensionControlLimits.getLimit(suspensionName, "Rim size");
		if (limit3 != null)
		{
			wheelControls.RimSize.FloatValue = Mathf.Clamp(wheelControls.RimSize.FloatValue, limit3.fMin, limit3.fMax);
		}
		return wheelControls;
	}

	private void SetSuspensionControllerData(SuspensionControllerData sData)
	{
		this.frontSuspension = sData.SelectedFrontSuspension;
		this.rearSuspension = sData.SelectedRearSuspension;
		float defaultWheelColliderRadius = this.FrontWheelsControls.DefaultWheelColliderRadius;
		float defaultWheelColliderRadius2 = this.RearWheelsControls.DefaultWheelColliderRadius;
		this.FrontWheelsControls = sData.FrontWheelsControls;
		this.RearWheelsControls = sData.RearWheelsControls;
		this.FrontWheelsControls.DefaultWheelColliderRadius = defaultWheelColliderRadius;
		this.RearWheelsControls.DefaultWheelColliderRadius = defaultWheelColliderRadius2;
		Suspension[] allSuspensions = this.getAllSuspensions();
		for (int i = 0; i < allSuspensions.Length; i++)
		{
			for (int j = 0; j < sData.AllSuspensionsDatas.Length; j++)
			{
				if (allSuspensions[i].SuspensionName == sData.AllSuspensionsDatas[j].SuspensionName)
				{
					SuspensionValue[] array = sData.AllSuspensionsDatas[j].AllValues;
					array = this.ClampSuspensionValues(array, allSuspensions[i].SuspensionName);
					allSuspensions[i].SetControlValues(array);
					allSuspensions[i].UpgradeStage = sData.AllSuspensionsDatas[j].UpgradeStage;
				}
			}
		}
		this.SetFrontSuspension(this.frontSuspension);
		this.SetRearSuspension(this.rearSuspension);
		this.LoadWheels();
		this.FrontWheelsControls = this.ClampWheelSizes(this.FrontWheelsControls, this.CurrentFrontSuspension.name);
		this.RearWheelsControls = this.ClampWheelSizes(this.RearWheelsControls, this.CurrentRearSuspension.name);
		this.DoWheelsSize();
	}

	public string ExportData()
	{
		SuspensionControllerData suspensionControllerData = this.getSuspensionControllerData();
		return XmlSerialization.SerializeData<SuspensionControllerData>(suspensionControllerData);
	}

	public void ImportData(string xmlString)
	{
		SuspensionControllerData suspensionControllerData = new SuspensionControllerData();
		suspensionControllerData = (SuspensionControllerData)XmlSerialization.DeserializeData<SuspensionControllerData>(xmlString);
		this.SetSuspensionControllerData(suspensionControllerData);
	}

	[HideInInspector]
	public CarController carController;

	[HideInInspector]
	public bool UpdateSuspensionInUpdate;

	public Suspension CurrentFrontSuspension;

	public Suspension CurrentRearSuspension;

	public WheelsControls FrontWheelsControls;

	public WheelsControls RearWheelsControls;

	public GameObject[] SpareWheelHolders;

	public List<Suspension> FrontSuspensions;

	public List<Suspension> RearSuspensions;

	public int frontSuspension;

	public int rearSuspension;

	public GameObject[] FrontTires;

	public GameObject[] FrontRims;

	public List<Transform> FrontTireSizeBones;

	public GameObject[] RearTires;

	public GameObject[] RearRims;

	public List<Transform> RearTireSizeBones;

	public GameObject[] SpareTires;

	public GameObject[] SpareRims;

	public bool multiplayerTraileredCar;

	public bool multiplayerCar;
}
