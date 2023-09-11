using System;
using System.Collections.Generic;
using CustomVP;
using Photon;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PhotonView))]
[AddComponentMenu("Photon Networking/Photon Transform View")]
public class PhotonTransformView : Photon.MonoBehaviour, IPunObservable
{
	private Rigidbody rb
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
		if (!PhotonNetwork.inRoom || SceneManager.GetActiveScene().name.ToLower() == "menu")
		{
			base.enabled = false;
		}
	}

	private void Start()
	{
		this.m_firstTake = true;
		this.m_PhotonView = base.GetComponent<PhotonView>();
		this.m_PositionControl = new PhotonTransformViewPositionControl();
		this.m_RotationControl = new PhotonTransformViewRotationControl();
		this.rigidbodyView = base.GetComponent<PhotonRigidbodyView>();
		this.carController = base.GetComponent<CarController>();
		this.suspensionController = base.GetComponent<SuspensionController>();
		this.partsSwitcher = base.GetComponent<BodyPartsSwitcher>();
		this.vehicleDataManager = base.GetComponent<VehicleDataManager>();
		this.lightsController = base.GetComponent<LightsController>();
		this.motorcycleAssistant = base.GetComponent<MotorcycleAssistant>();
		if (PhotonNetwork.inRoom && !base.photonView.isMine && SceneManager.GetActiveScene().name.ToLower() != "menu")
		{
			if (this.suspensionController != null)
			{
				this.suspensionController.TurnToMultiplayerCar();
			}
			MultiplayerManager.RefreshCurrentPlayers();
			this.myInfoBox = CarUIControl.Instance.CreatePlayerInfoBox(base.photonView);
			this.myInfoBox.ToggleDroneBadge(this.droneMode);
		}
		if (WinchManager.Instance != null && !base.photonView.isMine && GameState.GameMode == GameMode.Multiplayer && (this.vehicleDataManager.vehicleType == VehicleType.Crawler || this.vehicleDataManager.vehicleType == VehicleType.Truck || this.vehicleDataManager.vehicleType == VehicleType.SideBySide))
		{
			WinchManager.Instance.AddWinchTarget(base.transform.position, base.transform, true);
		}
		this.captureTheFlagManager = CaptureTheFlagManager.Instance;
	}

	private void Update()
	{
		if (this.m_PhotonView != null && this.m_PhotonView.isMine && Time.frameCount % this.vehicleStatusInterval == 0)
		{
			float num = this.carController.Steering;
			if (this.motorcycleAssistant != null)
			{
				num = this.carController.xInput;
			}
			base.photonView.RPC("UpdateVehicleStatus", PhotonTargets.Others, new object[]
			{
				new VehicleStatus(num, this.partsSwitcher.Dirtiness, this.partsSwitcher.MudWetness, this.carController.AverageRPM).Serialize()
			});
		}
		if (this.m_PhotonView != null && this.m_PhotonView.isMine && PhotonNetwork.inRoom && !this.haveSentVehicleData)
		{
			string @string = DataStore.GetString(GameState.CurrentVehicleID);
			string text = Utility.CompressXMLData(@string);
			base.photonView.RPC("UpdateVehicleData", PhotonTargets.OthersBuffered, new object[]
			{
				text
			});
			this.haveSentVehicleData = true;
		}
		if (this.captureTheFlagManager != null)
		{
			if (!this.captureTheFlagManager.GameInProgress && GameState.GameType == GameType.CaptureTheFlag && !this.captureTheFlagManager.GameOver && PhotonNetwork.playerList.Length == this.captureTheFlagManager.PlayerCount)
			{
				this.captureTheFlagManager.GameOn();
			}
			else if (GameState.GameMode == GameMode.Multiplayer && GameState.GameType == GameType.CaptureTheFlag && !this.captureTheFlagManager.GameInProgress && !this.captureTheFlagManager.GameOver)
			{
				this.captureTheFlagManager.GameWaiting();
			}
		}
		if (this.m_PhotonView == null || this.m_PhotonView.isMine || !PhotonNetwork.connected)
		{
			return;
		}
		this.steeringAngle = Mathf.Lerp(this.steeringAngle, this.lastSteeringAngle, Time.deltaTime * 5f);
		this.smoothWheelsRPM = Mathf.Lerp(this.smoothWheelsRPM, this.lastWheelsRPM, Time.deltaTime * 5f);
		if (this.suspensionController != null && Time.timeScale != 0f)
		{
			this.suspensionController.UpdateSuspensions(this.steeringAngle, this.smoothWheelsRPM);
			if (this.motorcycleAssistant != null)
			{
				this.motorcycleAssistant.UpdateMpValues(this.steeringAngle);
			}
		}
		if (this.drone != null)
		{
			this.drone.rb.position = Vector3.Lerp(this.drone.rb.position, this.lastDronePos, Time.deltaTime * 10f);
			this.drone.transform.eulerAngles = this.lastDroneRot;
			this.drone.rb.velocity = Vector3.Lerp(this.drone.rb.velocity, Vector3.zero, Time.deltaTime * 10f);
			this.drone.rb.angularVelocity = Vector3.Lerp(this.drone.rb.angularVelocity, Vector3.zero, Time.deltaTime * 10f);
		}
	}

	private void FixedUpdate()
	{
		if (this.m_PhotonView == null || this.m_PhotonView.isMine || !PhotonNetwork.connected)
		{
			return;
		}
		if (!this.onOtherPlayerTrailer)
		{
			this.UpdatePosition();
			this.UpdateRotation();
			this.UpdateRigidbody();
		}
	}

	private void UpdatePosition()
	{
		base.transform.localPosition = this.m_PositionControl.UpdatePosition(base.transform.localPosition);
		if (this.trailer != null)
		{
			this.trailer.transform.position = Vector3.Lerp(this.trailer.transform.position, this.trailerPos, Time.deltaTime * 4f);
			if (Vector3.Distance(this.trailer.transform.position, this.trailerPos) > 15f)
			{
				this.trailer.transform.position = this.trailerPos;
			}
		}
	}

	private void UpdateRotation()
	{
		base.transform.localRotation = this.m_RotationControl.GetRotation(base.transform.localRotation);
		if (this.trailer != null)
		{
			this.trailer.transform.rotation = Quaternion.Lerp(this.trailer.transform.rotation, this.trailerRot, Time.deltaTime * 3f);
		}
	}

	private void UpdateRigidbody()
	{
		this.rb.velocity = Vector3.zero;
		this.rb.angularVelocity = Vector3.zero;
		if (this.rb == null || this.rigidbodyView == null)
		{
			return;
		}
		this.rb.velocity = this.rigidbodyView.Velocity;
		this.rb.angularVelocity = this.rigidbodyView.AngularVelocity;
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (this.m_PositionControl == null || this.m_RotationControl == null)
		{
			return;
		}
		this.m_PositionControl.OnPhotonSerializeView(base.transform.localPosition, stream, info);
		this.m_RotationControl.OnPhotonSerializeView(base.transform.localRotation, stream, info);
		if (stream.isWriting)
		{
			if (this.carController.myTrailer != null)
			{
				stream.SendNext(this.carController.myTrailer.transform.position);
				stream.SendNext(this.carController.myTrailer.transform.rotation);
				if (VehicleLoader.Instance.carOnTrailer != null)
				{
					stream.SendNext(this.carController.myTrailer.transform.InverseTransformPoint(VehicleLoader.Instance.carOnTrailer.transform.position).y);
				}
			}
			if (this.carController.loadedOnOtherPlayerTrailer)
			{
				stream.SendNext(this.carController.ownerOfTrailer.trailer.transform.InverseTransformPoint(base.transform.position).y);
			}
			if (VehicleLoader.Instance.droneMode)
			{
				stream.SendNext(VehicleLoader.Instance.playerDrone.transform.position);
				stream.SendNext(new Vector3(VehicleLoader.Instance.playerDrone.transform.eulerAngles.y, VehicleLoader.Instance.playerDrone.sideTilt, VehicleLoader.Instance.playerDrone.forwardTilt));
			}
		}
		if (stream.isReading)
		{
			if (this.trailer != null)
			{
				this.trailerPos = (Vector3)stream.ReceiveNext();
				this.trailerRot = (Quaternion)stream.ReceiveNext();
				if (this.carOnTrailer != null)
				{
					float y = (float)stream.ReceiveNext();
					Vector3 localPosition = this.carOnTrailer.transform.localPosition;
					localPosition.y = y;
					this.carOnTrailer.transform.localPosition = localPosition;
				}
			}
			if (this.onOtherPlayerTrailer)
			{
				float y2 = (float)stream.ReceiveNext();
				Vector3 localPosition2 = this.wantedPosOnTrailer;
				localPosition2.y = y2;
				base.transform.localPosition = localPosition2;
				base.transform.localRotation = Quaternion.identity;
			}
			if (this.droneMode)
			{
				Vector3 vector = (Vector3)stream.ReceiveNext();
				Vector3 vector2 = (Vector3)stream.ReceiveNext();
				this.lastDronePos = vector;
				this.lastDroneRot = new Vector3(0f, vector2.x, 0f);
				this.drone.UpdateMpTilt(vector2.y, vector2.z);
			}
		}
		if (stream.isReading && this.m_firstTake)
		{
			this.m_firstTake = false;
			base.transform.localPosition = this.m_PositionControl.m_NetworkPosition;
			base.transform.localRotation = this.m_RotationControl.m_NetworkRotation;
		}
	}

	public void ChangeWeather(bool rainy)
	{
		base.photonView.RPC("ChangeWeatherRPC", PhotonTargets.All, new object[]
		{
			rainy
		});
	}

	[PunRPC]
	private void ChangeWeatherRPC(bool rainy)
	{
		if (WeatherController.Instance != null)
		{
			WeatherController.Instance.ChangeRainState(rainy);
		}
	}

	public void RequestCurrentWeather()
	{
		base.photonView.RPC("RequestCurrentWeatherRPC", PhotonTargets.All, new object[]
		{
			base.photonView.viewID
		});
	}

	[PunRPC]
	private void RequestCurrentWeatherRPC(int requesterViewID)
	{
		if (PhotonNetwork.isMasterClient && WeatherController.Instance != null)
		{
			this.SendCurrentWeatherResponse(WeatherController.Instance.rainy, requesterViewID);
		}
	}

	private void SendCurrentWeatherResponse(bool rainy, int requesterViewID)
	{
		base.photonView.RPC("SendCurrentWeatherResponseRPC", PhotonTargets.All, new object[]
		{
			rainy,
			requesterViewID
		});
	}

	[PunRPC]
	private void SendCurrentWeatherResponseRPC(bool rainy, int requesterViewID)
	{
		if (base.photonView.viewID == requesterViewID && WeatherController.Instance != null)
		{
			WeatherController.Instance.ChangeWeatherImmediately(rainy);
		}
	}

	public void SendTraileringRequest(PhotonView trailerOwner)
	{
		base.photonView.RPC("SendTraileringRequestRPC", PhotonPlayer.Find(trailerOwner.ownerId), new object[]
		{
			base.photonView.viewID
		});
	}

	[PunRPC]
	private void SendTraileringRequestRPC(int requesterViewID)
	{
		if (VehicleLoader.Instance.droneMode)
		{
			return;
		}
		CarUIControl.Instance.traileringRequestWindow.SetActive(true);
		MultiplayerManager.Instance.traileringRequesterViewID = requesterViewID;
	}

	public void AcceptTraileringRequest()
	{
		CarUIControl.Instance.traileringRequestWindow.SetActive(false);
		PhotonView photonView = PhotonView.Find(MultiplayerManager.Instance.traileringRequesterViewID);
		if (photonView != null)
		{
			base.photonView.RPC("AcceptTraileringRequestRPC", PhotonPlayer.Find(photonView.ownerId), new object[0]);
		}
		MultiplayerManager.Instance.traileringRequesterViewID = -1;
	}

	[PunRPC]
	private void AcceptTraileringRequestRPC()
	{
		VehicleLoader.Instance.playerCarController.OnLoadOnTrailerResponseAccepted(base.photonView);
	}

	public void DeclineTraierlingRequest()
	{
		CarUIControl.Instance.traileringRequestWindow.SetActive(false);
		PhotonView photonView = PhotonView.Find(MultiplayerManager.Instance.traileringRequesterViewID);
		if (photonView != null)
		{
			base.photonView.RPC("DeclineTraileringRequestRPC", PhotonPlayer.Find(photonView.ownerId), new object[0]);
		}
		MultiplayerManager.Instance.traileringRequesterViewID = -1;
	}

	[PunRPC]
	private void DeclineTraileringRequestRPC()
	{
		VehicleLoader.Instance.playerCarController.OnLoadOnTrailerResponseDeclined(base.photonView);
	}

	public void TellEveryoneImOnTrailer(int pViewID)
	{
		base.photonView.RPC("TellEveryoneImOnTrailerRPC", PhotonTargets.OthersBuffered, new object[]
		{
			pViewID
		});
	}

	[PunRPC]
	private void TellEveryoneImOnTrailerRPC(int pViewID)
	{
		PhotonView photonView = PhotonView.Find(pViewID);
		if (photonView == null)
		{
			return;
		}
		if (!photonView.isMine)
		{
			this.wantedPosOnTrailer = this.vehicleDataManager.AlignOnTrailer(photonView.tView.trailer);
			UnityEngine.Debug.LogError("WANTED POS ON TRAILER: " + this.wantedPosOnTrailer);
			base.transform.parent = photonView.tView.trailer.transform;
			photonView.tView.trailer.mpCarOnMe = base.gameObject;
			this.trailerImOn = photonView.tView.trailer;
		}
		else
		{
			this.wantedPosOnTrailer = this.vehicleDataManager.AlignOnTrailer(photonView.tView.carController.myTrailer);
			base.transform.parent = photonView.tView.carController.myTrailer.transform;
			photonView.tView.carController.myTrailer.GetComponent<Rigidbody>().mass = 600f;
			photonView.tView.carController.myTrailer.mpCarOnMe = base.gameObject;
			this.trailerImOn = photonView.tView.carController.myTrailer;
		}
		this.rb.interpolation = RigidbodyInterpolation.None;
		this.rb.isKinematic = true;
		this.onOtherPlayerTrailer = true;
	}

	public void TellEveryoneImOuttaTrailer(int pViewID)
	{
		base.photonView.RPC("TellEveryoneImOuttaTrailerRPC", PhotonTargets.OthersBuffered, new object[]
		{
			pViewID
		});
	}

	[PunRPC]
	private void TellEveryoneImOuttaTrailerRPC(int pViewID)
	{
		PhotonView photonView = PhotonView.Find(pViewID);
		if (photonView == null)
		{
			return;
		}
		if (photonView.isMine)
		{
			photonView.tView.carController.myTrailer.GetComponent<Rigidbody>().mass = 200f;
			photonView.tView.carController.myTrailer.mpCarOnMe = null;
		}
		else
		{
			photonView.tView.trailer.mpCarOnMe = null;
		}
		this.trailerImOn = null;
		base.transform.parent = null;
		this.onOtherPlayerTrailer = false;
		this.rb.isKinematic = false;
		this.rb.interpolation = RigidbodyInterpolation.Interpolate;
	}

	public void SpawnTrailer(string trailerName)
	{
		base.photonView.RPC("SpawnTrailerRpc", PhotonTargets.OthersBuffered, new object[]
		{
			trailerName
		});
	}

	[PunRPC]
	private void SpawnTrailerRpc(string trailerName)
	{
		this.trailer = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Vehicles/" + trailerName))).GetComponent<TrailerController>();
		this.trailer.GetComponent<Rigidbody>().isKinematic = true;
		UnityEngine.Object.Destroy(this.trailer.GetComponent<VehicleDataManager>());
		foreach (TrailerWheelCollider obj in this.trailer.GetComponentsInChildren<TrailerWheelCollider>())
		{
			UnityEngine.Object.Destroy(obj);
		}
		this.trailer.multiplayerTrailer = true;
		this.trailer.playerView = base.photonView;
	}

	public void SpawnDrone(int droneID)
	{
		base.photonView.RPC("SpawnDroneRPC", PhotonTargets.OthersBuffered, new object[]
		{
			droneID
		});
	}

	[PunRPC]
	private void SpawnDroneRPC(int droneID)
	{
		this.drone = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Drones/Drone" + droneID), base.transform.position, base.transform.rotation)).GetComponent<DroneController>();
		this.drone.MakeMultiplayer(this);
		this.drone.gameObject.SetActive(this.droneMode);
		this.myDroneInfoBox = CarUIControl.Instance.CreateDroneInfoBox(this.drone, base.photonView);
		this.myDroneInfoBox.gameObject.SetActive(this.droneMode);
	}

	public void SendDroneModeChanged(bool mode)
	{
		base.photonView.RPC("SendDroneModeChangedRPC", PhotonTargets.OthersBuffered, new object[]
		{
			mode
		});
	}

	[PunRPC]
	private void SendDroneModeChangedRPC(bool mode)
	{
		this.droneMode = mode;
		if (this.drone != null)
		{
			this.drone.gameObject.SetActive(mode);
		}
		if (this.myInfoBox != null)
		{
			this.myInfoBox.ToggleDroneBadge(mode);
		}
		if (this.myDroneInfoBox != null)
		{
			this.myDroneInfoBox.gameObject.SetActive(this.droneMode);
		}
	}

	public void ChangeTrailerMpConnectedState(bool mpConnected)
	{
		base.photonView.RPC("ChangeTrailerMpConnectedStateRPC", PhotonTargets.OthersBuffered, new object[]
		{
			mpConnected
		});
	}

	[PunRPC]
	private void ChangeTrailerMpConnectedStateRPC(bool mpConnected)
	{
		if (this.trailer != null)
		{
			this.trailer.mpConnected = mpConnected;
		}
	}

	public void SpawnTraileredCar(string xmlData)
	{
		string text = Utility.CompressXMLData(xmlData);
		base.photonView.RPC("SpawnTraileredCarRPC", PhotonTargets.OthersBuffered, new object[]
		{
			text
		});
	}

	[PunRPC]
	private void SpawnTraileredCarRPC(string xmlData)
	{
		string text = xmlData;
		string text2 = Utility.DecompressXMLData(text);
		if (text2.Length != text.Length)
		{
			xmlData = text2;
		}
		VehicleData vehicleData = (VehicleData)XmlSerialization.DeserializeData<VehicleData>(xmlData);
		this.carOnTrailer = (UnityEngine.Object.Instantiate(Resources.Load("Vehicles/" + vehicleData.VehicleName, typeof(GameObject))) as GameObject);
		this.carOnTrailer.name = vehicleData.VehicleName;
		IKDriverController component = this.carOnTrailer.GetComponent<IKDriverController>();
		if (component != null)
		{
			component.ToggleDriver(false, false);
			component.enabled = false;
		}
		VehicleDataManager component2 = this.carOnTrailer.GetComponent<VehicleDataManager>();
		component2.LoadVehicleDataFromString(xmlData);
		this.carOnTrailer.GetComponent<BodyPartsSwitcher>().MergeBodyParts();
		this.carOnTrailer.GetComponent<BodyPartsSwitcher>().UpdateColor(false);
		component2.AlignOnTrailer(this.trailer.GetComponent<TrailerController>());
		this.carOnTrailer.transform.parent = this.trailer.transform;
		this.carOnTrailer.GetComponent<SuspensionController>().multiplayerTraileredCar = true;
		UnityEngine.Object.DestroyImmediate(this.carOnTrailer.GetComponent<Rigidbody>());
		UnityEngine.Object.DestroyImmediate(this.carOnTrailer.GetComponent<CarController>());
		UnityEngine.Object.DestroyImmediate(this.carOnTrailer.GetComponent<CarEffects>());
		UnityEngine.Object.DestroyImmediate(this.carOnTrailer.GetComponent<BodyPartsSwitcher>());
		UnityEngine.Object.DestroyImmediate(this.carOnTrailer.GetComponent<PhotonTransformView>());
		UnityEngine.Object.DestroyImmediate(this.carOnTrailer.GetComponent<PhotonView>());
		UnityEngine.Object.DestroyImmediate(this.carOnTrailer.GetComponent<IKDriverController>());
		UnityEngine.Object.DestroyImmediate(this.carOnTrailer.GetComponent<LightsController>());
		UnityEngine.Object.DestroyImmediate(this.carOnTrailer.GetComponent<RammingChecker>());
		UnityEngine.Object.DestroyImmediate(this.carOnTrailer.GetComponent<EngineController>());
		foreach (WheelComponent wheelComponent in this.carOnTrailer.GetComponentsInChildren<WheelComponent>())
		{
			UnityEngine.Object.Destroy(wheelComponent.gameObject);
		}
	}

	public void SendDisableMyCollidersEvent(PhotonPlayer player)
	{
		base.photonView.RPC("DisableMyColliders", player, new object[0]);
	}

	[PunRPC]
	private void DisableMyColliders()
	{
		foreach (Collider collider in base.GetComponentsInChildren<Collider>())
		{
			collider.enabled = false;
		}
	}

	public void SendLightsChangingEvent(float LightState)
	{
		base.photonView.RPC("UpdateLights", PhotonTargets.Others, new object[]
		{
			LightState.ToString()
		});
	}

	[PunRPC]
	private void UpdateLights(string LightsState)
	{
		if (this.lightsController != null)
		{
			this.lightsController.LightsState = (float)int.Parse(LightsState);
		}
	}

	public void SendChatMessage(string msg)
	{
		base.photonView.RPC("ReceiveChatMessage", PhotonTargets.All, new object[]
		{
			msg
		});
	}

	[PunRPC]
	public void ReceiveChatMessage(string msg)
	{
		if (ChatBox.Instance == null)
		{
			return;
		}
		ChatBox.Instance.ReceiveChatMessage(msg, base.photonView.owner);
	}

	public void SendWinchRequest(PhotonView targetCar)
	{
		base.photonView.RPC("GetWinchRequest", PhotonPlayer.Find(targetCar.ownerId), new object[]
		{
			base.photonView.ownerId.ToString()
		});
	}

	[PunRPC]
	public void GetWinchRequest(string requesterId)
	{
		if (VehicleLoader.Instance.droneMode)
		{
			return;
		}
		PhotonView photonView = this.FindPhotonViewByID(requesterId);
		if (photonView == null)
		{
			return;
		}
		string text = "Player " + photonView.owner.CustomProperties["DisplayName"].ToString() + " wants to attach winch to your vehicle";
		CarUIControl.Instance.ShowWinchRequestWindow(text);
		WinchManager.Instance.GetWinchRequest(requesterId);
	}

	public void SendWinchAcceptation(PhotonView requesterCar)
	{
		base.photonView.RPC("WinchRequestAccepted", PhotonPlayer.Find(requesterCar.ownerId), new object[]
		{
			base.photonView.ownerId.ToString()
		});
	}

	[PunRPC]
	public void WinchRequestAccepted(string AcceptingCarID)
	{
		PhotonView photonView = this.FindPhotonViewByID(AcceptingCarID);
		if (photonView == null)
		{
			return;
		}
		WinchManager.Instance.OnWinchRequestAccepted(photonView);
	}

	public void SendWinchDeclination(PhotonView requesterCar)
	{
		base.photonView.RPC("WinchRequestDeclined", PhotonPlayer.Find(requesterCar.ownerId), new object[0]);
	}

	[PunRPC]
	public void WinchRequestDeclined()
	{
		WinchManager.Instance.OnWinchRequestDeclined();
	}

	public void SendWinchAttachEvent(PhotonView TargetCar)
	{
		base.photonView.RPC("OtherCarAttachedToUs", PhotonPlayer.Find(TargetCar.ownerId), new object[]
		{
			base.photonView.ownerId.ToString()
		});
	}

	[PunRPC]
	public void OtherCarAttachedToUs(string AttachedCarID)
	{
		PhotonView photonView = this.FindPhotonViewByID(AttachedCarID);
		if (photonView == null)
		{
			return;
		}
		WinchManager.Instance.OnOtherCarAttachedToUs(photonView);
	}

	public void SendDynamicCableCreationEvent(string CableID, string TargetCarID)
	{
		string text = string.Concat(new object[]
		{
			CableID,
			"|",
			base.photonView.ownerId,
			"|",
			TargetCarID
		});
		base.photonView.RPC("DynamicCableCreated", PhotonTargets.Others, new object[]
		{
			text
		});
	}

	[PunRPC]
	public void DynamicCableCreated(string xmlData)
	{
		string[] array = xmlData.Split(new char[]
		{
			'|'
		});
		PhotonView car = this.FindPhotonViewByID(array[1]);
		PhotonView car2 = this.FindPhotonViewByID(array[2]);
		WinchManager.Instance.OnDynamicCableCreated(array[0], car, car2);
	}

	public void SendStaticCableCreationEvent(string CableID, Vector3 TargetPos)
	{
		string text = string.Concat(new string[]
		{
			TargetPos.x.ToString(),
			"?",
			TargetPos.y.ToString(),
			"?",
			TargetPos.z.ToString()
		});
		string text2 = string.Concat(new object[]
		{
			CableID,
			"|",
			base.photonView.ownerId,
			"|",
			text
		});
		base.photonView.RPC("StaticCableCreated", PhotonTargets.Others, new object[]
		{
			text2
		});
	}

	[PunRPC]
	public void StaticCableCreated(string xmlData)
	{
		string[] array = xmlData.Split(new char[]
		{
			'|'
		});
		string[] array2 = array[2].Split(new char[]
		{
			'?'
		});
		Vector3 target = new Vector3(float.Parse(array2[0]), float.Parse(array2[1]), float.Parse(array2[2]));
		PhotonView car = this.FindPhotonViewByID(array[1]);
		WinchManager.Instance.OnStaticCableCreated(array[0], car, target);
	}

	public void SendCableDestroyingEvent(string CableID)
	{
		base.photonView.RPC("CableDestroyed", PhotonTargets.Others, new object[]
		{
			CableID
		});
	}

	[PunRPC]
	public void CableDestroyed(string CableID)
	{
		WinchManager.Instance.OnCableDestroyed(CableID);
	}

	public void SendWinchDetachEvent(PhotonView targetCar)
	{
		base.photonView.RPC("OtherCarDetachedFromUs", PhotonPlayer.Find(targetCar.ownerId), new object[0]);
	}

	[PunRPC]
	public void OtherCarDetachedFromUs()
	{
		WinchManager.Instance.OnOtherCarDetachedFromUs();
	}

	public void RiderKnockOut(Vector3 force)
	{
		base.photonView.RPC("OtherPlayerKnockedOut", PhotonTargets.Others, new object[]
		{
			force
		});
	}

	[PunRPC]
	public void OtherPlayerKnockedOut(Vector3 force)
	{
		base.GetComponent<IKDriverController>().DoKnockOut(force);
	}

	public void ImReadyToRace()
	{
		base.photonView.RPC("OtherPlayerReadyToRace", PhotonTargets.Others, new object[0]);
	}

	[PunRPC]
	public void OtherPlayerReadyToRace()
	{
		TrailRaceManager.Instance.OnOtherPlayerReady();
	}

	public void ImTotallyLoaded()
	{
		base.photonView.RPC("OtherPlayerTotallyLoaded", PhotonTargets.OthersBuffered, new object[]
		{
			(!DataStore.GetBool("UseFBName", false)) ? DataStore.GetString("GeneratedName") : GameState.PlayerName
		});
	}

	[PunRPC]
	public void OtherPlayerTotallyLoaded(string name)
	{
		TrailRaceManager.Instance.OnOtherPlayerTotallyLoaded(name);
	}

	public void iFinishedTrailRace(float raceTime)
	{
		base.photonView.RPC("OpponentFinishedTrailRace", PhotonTargets.Others, new object[]
		{
			raceTime
		});
	}

	[PunRPC]
	private void OpponentFinishedTrailRace(float raceTime)
	{
		TrailRaceManager.Instance.OnOtherPlayerFinished(raceTime);
	}

	public void SendRestartOffering()
	{
		base.photonView.RPC("RestartOffered", PhotonTargets.Others, new object[0]);
	}

	[PunRPC]
	private void RestartOffered()
	{
		TrailRaceManager.Instance.OnRestartOfferingReceived();
	}

	public void SendRestartAcceptation()
	{
		base.photonView.RPC("RestartAccepted", PhotonTargets.Others, new object[0]);
	}

	[PunRPC]
	private void RestartAccepted()
	{
		TrailRaceManager.Instance.OnRestartAccepted();
	}

	private PhotonView FindPhotonViewByID(string ID)
	{
		foreach (KeyValuePair<int, PhotonView> keyValuePair in PhotonNetwork.networkingPeer.photonViewList)
		{
			if (keyValuePair.Value.ownerId.ToString() == ID)
			{
				return keyValuePair.Value;
			}
		}
		return null;
	}

	public void SendGameOverReport()
	{
		base.photonView.RPC("ReportGameOver", PhotonTargets.AllBuffered, new object[0]);
	}

	public void SendFlagCapturedBlue(int flagID)
	{
		base.photonView.RPC("SetFlagCapturedBlue", PhotonTargets.AllBuffered, new object[]
		{
			flagID
		});
	}

	public void SendFlagCapturedRed(int flagID)
	{
		base.photonView.RPC("SetFlagCapturedRed", PhotonTargets.AllBuffered, new object[]
		{
			flagID
		});
	}

	public void SendCTFGameOn()
	{
		base.photonView.RPC("CTFGameOn", PhotonTargets.AllBuffered, new object[0]);
	}

	[PunRPC]
	public void SetFlagCapturedBlue(int flagID)
	{
		this.captureTheFlagManager.SetFlagCaptured(flagID, PunTeams.Team.blue);
	}

	[PunRPC]
	public void SetFlagCapturedRed(int flagID)
	{
		this.captureTheFlagManager.SetFlagCaptured(flagID, PunTeams.Team.red);
	}

	[PunRPC]
	public void CTFGameOn()
	{
		this.captureTheFlagManager.GameOn();
	}

	[PunRPC]
	public void ReportGameOver()
	{
		this.captureTheFlagManager.ReportGameOver();
	}

	[PunRPC]
	private void UpdateVehicleData(string xmlData)
	{
		if (base.photonView.isMine)
		{
			return;
		}
		if (Utility.IsXMLDataCompressed(xmlData))
		{
			xmlData = Utility.DecompressXMLData(xmlData);
		}
		SuspensionController component = base.GetComponent<SuspensionController>();
		if (component != null)
		{
			component.TurnToMultiplayerCar();
		}
		this.vehicleDataManager = base.GetComponent<VehicleDataManager>();
		this.partsSwitcher = base.GetComponent<BodyPartsSwitcher>();
		try
		{
			if (this.vehicleDataManager != null)
			{
				this.vehicleDataManager.LoadVehicleDataFromString(xmlData);
				this.partsSwitcher.MergeBodyParts();
				this.partsSwitcher.UpdateColor(false);
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log("Could not load vehicle data from string RPC: " + ex.Message);
		}
	}

	[PunRPC]
	private void UpdateVehicleStatus(string status)
	{
		if (base.photonView.isMine)
		{
			return;
		}
		VehicleStatus vehicleStatus = VehicleStatus.DeSerialize(status);
		if (this.partsSwitcher != null)
		{
			this.partsSwitcher.Dirtiness = vehicleStatus.Dirtiness;
			this.partsSwitcher.MudWetness = vehicleStatus.Wetness;
			this.partsSwitcher.UpdateDirtiness();
		}
		this.lastSteeringAngle = vehicleStatus.SteeringAngle;
		this.lastWheelsRPM = vehicleStatus.WheelsRPM;
	}

	private PhotonTransformViewPositionControl m_PositionControl;

	private PhotonTransformViewRotationControl m_RotationControl;

	public int vehicleStatusInterval = 4;

	private float steeringAngle;

	private PhotonView m_PhotonView;

	private PhotonRigidbodyView rigidbodyView;

	private Rigidbody _rb;

	private CarController carController;

	private CarUIControl carUIControl;

	private LightsController lightsController;

	private SuspensionController suspensionController;

	private BodyPartsSwitcher partsSwitcher;

	private VehicleDataManager vehicleDataManager;

	private CaptureTheFlagManager captureTheFlagManager;

	private IKDriverController driver;

	private MotorcycleAssistant motorcycleAssistant;

	private bool haveSentVehicleData;

	[HideInInspector]
	public float lastSteeringAngle;

	private float lastWheelsRPM;

	private float smoothWheelsRPM;

	public bool m_firstTake = true;

	[HideInInspector]
	public TrailerController trailer;

	[HideInInspector]
	public GameObject carOnTrailer;

	private Vector3 trailerPos;

	private Quaternion trailerRot;

	private Vector3 wantedPosOnTrailer;

	[HideInInspector]
	public bool onOtherPlayerTrailer;

	[HideInInspector]
	public TrailerController trailerImOn;

	[HideInInspector]
	public DroneController drone;

	[HideInInspector]
	public bool droneMode;

	private PlayerInfoUI myInfoBox;

	private DroneInfoUI myDroneInfoBox;

	private Vector3 lastDronePos;

	private Vector3 lastDroneRot;
}
