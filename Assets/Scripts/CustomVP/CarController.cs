using System;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace CustomVP
{
	public class CarController : MonoBehaviour
	{
		public float AverageRPM
		{
			get
			{
				float num = 0f;
				if (this.tankController == null)
				{
					for (int i = 0; i < this.wheels.Count; i++)
					{
						num += this.wheels[i].wc.wheelCollider.perFrameRotation;
					}
					if (num > 0f)
					{
						num /= (float)this.wheels.Count;
					}
				}
				else
				{
					for (int j = 0; j < this.tankController.borderWheelColliders.Length; j++)
					{
						num += this.tankController.borderWheelColliders[j].perFrameRotation;
					}
					if (num > 0f)
					{
						num /= (float)this.tankController.borderWheelColliders.Length;
					}
				}
				return num;
			}
		}

		public int WheelsCount
		{
			get
			{
				if (this.tankController == null)
				{
					return this.wheels.Count;
				}
				return this.tankController.borderWheelColliders.Length;
			}
		}

		private void Start()
		{
			this.carUIControl = CarUIControl.Instance;
			this.m_Rigidbody = base.GetComponent<Rigidbody>();
			this.bodyPartsSwitcher = base.GetComponent<BodyPartsSwitcher>();
			this.surfaceManager = SurfaceManager.Instance;
			this.engine = base.GetComponent<EngineController>();
			this.Driver = base.GetComponent<IKDriverController>();
			this.vehicleDataManager = base.GetComponent<VehicleDataManager>();
			this.myTransformView = base.GetComponent<PhotonTransformView>();
			this.tankController = base.GetComponent<TankController>();
			this.lowestPointOfCollider = Vector3.zero;
			this.lowestPointOfCollider = this.BodyColliders[0].ClosestPoint(this.BodyColliders[0].transform.position - base.transform.up * 10f);
			if (this.BodyColliders.Length > 1)
			{
				for (int i = 0; i < this.BodyColliders.Length; i++)
				{
					if (this.BodyColliders[i].ClosestPoint(this.BodyColliders[i].transform.position - base.transform.up * 10f).y < this.lowestPointOfCollider.y)
					{
						this.lowestPointOfCollider = this.BodyColliders[i].ClosestPoint(this.BodyColliders[i].transform.position - base.transform.up * 10f);
					}
				}
			}
			this.lowestPointOfCollider = base.transform.InverseTransformPoint(this.lowestPointOfCollider);
			if (this.tankController == null)
			{
				this.SetCalculatedCOM();
			}
			int @int = DataStore.GetInt("AirControl");
			this.airControlPower = Mathf.Lerp(0f, 1f, Mathf.InverseLerp(-2f, 0f, (float)@int));
			this.SetupFrictionValues();
			if (this.carUIControl != null && (GameState.GameMode != GameMode.Multiplayer || this.myTransformView.photonView.isMine))
			{
				int selectedPosition = (!this.LowGear) ? 1 : 0;
				this.carUIControl.SetupGearButton(selectedPosition);
				int selectedPosition2 = 0;
				if (this.RearDiffLock)
				{
					selectedPosition2 = 1;
				}
				if (this.FrontDiffLock)
				{
					selectedPosition2 = 2;
				}
				if (this.InteraxleDiffLock)
				{
					selectedPosition2 = 3;
				}
				this.carUIControl.SetupDiffLockButton(selectedPosition2);
				int selectedPosition3 = 0;
				if (this.FWD)
				{
					selectedPosition3 = 1;
				}
				if (this.FWD && this.RWD)
				{
					selectedPosition3 = 2;
				}
				this.carUIControl.SetupDriveButton(selectedPosition3);
				if (this.vehicleDataManager.vehicleType == VehicleType.Bike || this.tankController != null)
				{
					this.carUIControl.HideAllDrivetrainOptions();
				}
			}
			PartGroup partGroup = null;
			if (this.bodyPartsSwitcher != null && this.bodyPartsSwitcher.partGroups != null)
			{
				for (int j = 0; j < this.bodyPartsSwitcher.partGroups.Length; j++)
				{
					if (this.bodyPartsSwitcher.partGroups[j].partType == PartType.Snorkel)
					{
						partGroup = this.bodyPartsSwitcher.partGroups[j];
						break;
					}
				}
				if (partGroup != null && partGroup.InstalledPart > 0)
				{
					this.HasSnorkel = true;
				}
			}
			if (this.wheels.Count > 2)
			{
				this.wheels[0].steer = (this.wheels[1].steer = true);
				this.wheels[2].inverseSteer = (this.wheels[3].inverseSteer = true);
				this.wheels[2].handbrake = (this.wheels[3].handbrake = true);
				if (this.wheels.Count > 4)
				{
					this.wheels[4].inverseSteer = (this.wheels[5].inverseSteer = true);
					this.wheels[4].handbrake = (this.wheels[5].handbrake = true);
				}
			}
			if (this.wheels.Count == 2)
			{
				this.wheels[0].steer = true;
				this.wheels[1].handbrake = true;
			}
			this.OnValidate();
			if (this.engine != null)
			{
				this.engine.SetDiesel(this.DieselStage == 4);
				this.engine.PurchasedTurbo = (this.TurboStage > 0);
			}
			foreach (Collider collider in this.BodyColliders)
			{
				collider.material = (PhysicMaterial)Resources.Load("Physics/TruckCollider");
				collider.gameObject.layer = 26;
			}
			this.IsSlideThrottle = DataStore.GetBool("SlideAccelerator", false);
		}

		private void OnTriggerEnter(Collider other)
		{
			Checkpoint component = other.GetComponent<Checkpoint>();
			if (component == null)
			{
				return;
			}
			if (GameState.GameType == GameType.TrailRace)
			{
				TrailRaceManager.Instance.CollidedWithCheckpoint(component);
			}
			else
			{
				RacingManager.Instance.CollidedWithCheckpoint(component);
			}
		}

		private void OnDrawGizmos()
		{
			Color blue = Color.blue;
			blue.a = 0.3f;
			Gizmos.color = blue;
			if (this.DamageWaterline != null)
			{
				Gizmos.DrawCube(this.DamageWaterline.transform.position, new Vector3(3f, 0f, 3f));
			}
		}

		private void OnDisable()
		{
			foreach (_Wheel wheel in this.wheels)
			{
				wheel.wc.BrakeTorque = this.BrakeTorque;
				wheel.wc.MotorTorque = 0f;
			}
		}

		private void FixedUpdate()
		{
			if (!this.vehicleIsActive)
			{
				return;
			}
			if (this.PreventFromSideSliding)
			{
				this.PreventFromSideSlide();
			}
			if (VehicleLoader.Instance == null || (VehicleLoader.Instance != null && !VehicleLoader.Instance.droneMode))
			{
				this.DoAirForces();
			}
			if (this.tankController == null)
			{
				this.DoAntiroll();
			}
			else
			{
				this.DoTankAntiroll();
			}
			this.acceleration = (this.m_Rigidbody.velocity - this.lastVelocity) / Time.fixedDeltaTime;
			this.acceleration = base.transform.InverseTransformVector(this.acceleration);
			this.lastVelocity = this.m_Rigidbody.velocity;
		}

		private void OnCollisionEnter(Collision collision)
		{
			this.TouchingGround = true;
			this.GotHit(collision);
		}

		private void OnCollisionStay(Collision collision)
		{
			this.TouchingGround = true;
		}

		private void OnCollisionExit(Collision collision)
		{
			this.TouchingGround = false;
		}

		private void SendLoadOnTrailerRequest(PhotonTransformView otherTView)
		{
			this.ownerOfTrailerWeWantToLoadOn = otherTView;
			this.waitingForTrailerResponse = true;
			this.carUIControl.waitingForLoadOnTrailerResponseWindow.SetActive(true);
			this.carUIControl.loadOnOtherPlayerTrailerButton.SetActive(false);
			this.carUIControl.ToggleCarExtras(false);
			this.carUIControl.ToggleCarControls(false);
			this.carUIControl.ToggleWinchControls(false);
			this.vehicleIsActive = false;
			this.myTransformView.SendTraileringRequest(this.ownerOfTrailerWeWantToLoadOn.photonView);
		}

		public void OnLoadOnTrailerResponseDeclined(PhotonView sender)
		{
			if (this.ownerOfTrailerWeWantToLoadOn != null && sender.tView == this.ownerOfTrailerWeWantToLoadOn && this.waitingForTrailerResponse)
			{
				this.CancelTrailerLoadWaiting();
			}
		}

		public void OnLoadOnTrailerResponseAccepted(PhotonView sender)
		{
			if (this.ownerOfTrailerWeWantToLoadOn != null && sender.tView == this.ownerOfTrailerWeWantToLoadOn)
			{
				this.LoadOnOtherTrailer(this.ownerOfTrailerWeWantToLoadOn);
			}
			if (this.waitingForTrailerResponse)
			{
				this.CancelTrailerLoadWaiting();
			}
		}

		public void CancelTrailerLoadWaiting()
		{
			this.ownerOfTrailerWeWantToLoadOn = null;
			this.waitingForTrailerResponse = false;
			this.carUIControl.waitingForLoadOnTrailerResponseWindow.SetActive(false);
			this.carUIControl.ToggleCarExtras(true);
			this.carUIControl.ToggleCarControls(true);
			this.carUIControl.ToggleWinchControls(true);
			this.vehicleIsActive = true;
		}

		public void LoadOnOtherTrailer(PhotonTransformView trailerOwner)
		{
			this.vehicleDataManager.LoadOnTrailer(trailerOwner.trailer, false);
			this.myTransformView.TellEveryoneImOnTrailer(trailerOwner.photonView.viewID);
			this.loadedOnOtherPlayerTrailer = true;
			this.m_Rigidbody.interpolation = RigidbodyInterpolation.None;
		}

		public void UnloadFromOtherTrailer()
		{
			ConfigurableJoint component = base.GetComponent<ConfigurableJoint>();
			if (component != null)
			{
				UnityEngine.Object.DestroyImmediate(component);
			}
			int pViewID = -1;
			if (this.ownerOfTrailer != null)
			{
				pViewID = this.ownerOfTrailer.photonView.viewID;
			}
			this.myTransformView.TellEveryoneImOuttaTrailer(pViewID);
			this.loadedOnOtherPlayerTrailer = false;
			this.m_Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		}

		public void Update()
		{
			if (this.carUIControl != null && PhotonNetwork.inRoom)
			{
				this.carUIControl.loadOnOtherPlayerTrailerButton.SetActive(false);
				this.carUIControl.unloadFromOtherPlayerTrailerButton.SetActive(this.loadedOnOtherPlayerTrailer);
				if (this.vehicleDataManager.vehicleType != VehicleType.Bike && !this.loadedOnOtherPlayerTrailer && !this.waitingForTrailerResponse && !WinchManager.Instance.WinchMode && (this.myTrailer == null || (this.myTrailer != null && !this.myTrailer.connected)))
				{
					foreach (PhotonView photonView in MultiplayerManager.CurrentPlayerViews)
					{
						if (photonView != null && photonView.tView.trailer != null && photonView.tView.carOnTrailer == null && photonView.tView.trailer.mpCarOnMe == null && photonView.tView.trailer.mpConnected && Vector3.Distance(base.transform.position, photonView.tView.trailer.transform.position) < 8f)
						{
							this.carUIControl.loadOnOtherPlayerTrailerButton.SetActive(true);
							this.ownerOfTrailer = photonView.tView;
						}
					}
				}
				if (CrossPlatformInputManager.GetButtonUp("LoadOnOtherTrailer"))
				{
					this.SendLoadOnTrailerRequest(this.ownerOfTrailer);
				}
				if (CrossPlatformInputManager.GetButtonUp("UnloadFromOtherTrailer"))
				{
					this.UnloadFromOtherTrailer();
				}
				if (CrossPlatformInputManager.GetButtonUp("CancelTrailerLoadWaiting"))
				{
					this.CancelTrailerLoadWaiting();
				}
				if (CrossPlatformInputManager.GetButtonUp("AcceptTrailering"))
				{
					this.myTransformView.AcceptTraileringRequest();
				}
				if (CrossPlatformInputManager.GetButtonUp("DeclineTrailering"))
				{
					this.myTransformView.DeclineTraierlingRequest();
				}
				if (this.loadedOnOtherPlayerTrailer && this.ownerOfTrailer == null)
				{
					this.UnloadFromOtherTrailer();
				}
				if (this.waitingForTrailerResponse && this.ownerOfTrailerWeWantToLoadOn == null)
				{
					this.CancelTrailerLoadWaiting();
				}
			}
			if (this.InteraxleDiffLock && (!this.FrontDiffLock || !this.RearDiffLock))
			{
				this.InteraxleDiffLock = false;
			}
			if (this.tankController == null)
			{
				Vector3 vector = this.CalculateCOMPosition();
				if (vector != Vector3.zero && Vector3.Distance(vector, this.m_Rigidbody.centerOfMass) > 0.1f)
				{
					this.SetCOM(vector);
				}
			}
			UnityEngine.Debug.DrawRay(base.transform.TransformPoint(this.m_Rigidbody.centerOfMass), Vector3.up);
			this.WheelsOffTheGround = this.NotGroundedWheels();
			this.Grounded = ((!(this.tankController == null)) ? (this.WheelsOffTheGround < this.tankController.borderWheelColliders.Length) : (this.WheelsOffTheGround < this.wheels.Count));
			if (QualitySettings.GetQualityLevel() > 2 && this.Shadow.gameObject.activeInHierarchy)
			{
				this.Shadow.gameObject.SetActive(false);
			}
			else if (this.Shadow != null && QualitySettings.GetQualityLevel() <= 2)
			{
				if (!this.Shadow.gameObject.activeInHierarchy)
				{
					this.Shadow.gameObject.SetActive(true);
				}
				Vector3 a = this.Shadow.position + Vector3.down;
				this.Shadow.rotation = Quaternion.LookRotation(a - this.Shadow.position, base.transform.forward);
			}
			if (this.Shadow != null)
			{
				Vector3 a2 = this.Shadow.position + Vector3.down;
				this.Shadow.rotation = Quaternion.LookRotation(a2 - this.Shadow.position, base.transform.forward);
			}
			this.Speed = base.transform.InverseTransformDirection(this.m_Rigidbody.velocity).z * 2.23f;
			this.AngularSpeed = this.m_Rigidbody.angularVelocity.magnitude;
			this.DoInput();
			if (!this.vehicleIsActive || (VehicleLoader.Instance != null && VehicleLoader.Instance.droneMode))
			{
				foreach (_Wheel wheel in this.wheels)
				{
					wheel.wc.MotorTorque = 0f;
					wheel.wc.BrakeTorque = this.BrakeTorque;
				}
				return;
			}
			if (Time.time >= this.nextSurfaceManagerDataUpdateTime)
			{
				this.GetDataFromSurfaceManager();
			}
			this.DoCarHandling();
		}

		public void OnValidate()
		{
			this.SetDiffLock();
			this.SetupFrictionValues();
			this.UpdateMotorPower();
			if (this.wheels.Count >= 4)
			{
				for (int i = 0; i < this.wheels.Count; i++)
				{
					this.wheels[i].power = ((i <= 1) ? this.FWD : this.RWD);
					if (this.wheels[i].wc != null && this.wheels[i].wc.wheelCollider != null)
					{
						this.wheels[i].wc.wheelCollider.FakeRPM = this.FakeRPM;
					}
				}
			}
			if (this.wheels.Count == 2)
			{
				for (int j = 0; j < this.wheels.Count; j++)
				{
					this.wheels[j].power = (j == 1);
					if (this.wheels[j].wc != null && this.wheels[j].wc.wheelCollider != null)
					{
						this.wheels[j].wc.wheelCollider.FakeRPM = this.FakeRPM;
					}
				}
			}
			if (this.tankController != null)
			{
				for (int k = 0; k < this.tankController.allWheelColliders.Length; k++)
				{
					this.tankController.allWheelColliders[k].FakeRPM = this.FakeRPM;
				}
			}
		}

		private void PreventFromSideSlide()
		{
			if (this.DontPreventFromSliding)
			{
				return;
			}
			if (Mathf.Abs(this.m_Rigidbody.velocity.magnitude) < 0.5f && this.Throttle == 0f && this.WheelsOffTheGround == 0 && !this.TouchingGround)
			{
				Vector3 a = new Vector3(this.m_Rigidbody.velocity.x, 0f, this.m_Rigidbody.velocity.z);
				this.m_Rigidbody.AddForce(-a * 100000f);
			}
		}

		public void FlipCar()
		{
			if (this.loadedOnOtherPlayerTrailer)
			{
				return;
			}
			base.transform.rotation = Quaternion.LookRotation(base.transform.forward, Vector3.up);
			Utility.AlignVehicleByGround(base.transform, false);
			this.m_Rigidbody.velocity = Vector3.zero;
			this.m_Rigidbody.angularVelocity = Vector3.zero;
			this.m_Rigidbody.isKinematic = true;
			if (this.myTrailer != null && this.myTrailer.connected)
			{
				this.myTrailer.Detach();
				this.myTrailer.Attach();
				this.myTrailer.rb.isKinematic = true;
			}
			base.Invoke("UnfreezeCar", 0.5f);
			this.carUIControl.SwitchFlipButton(false);
		}

		private void RepairVehicle()
		{
			this.CarHealth = 100f;
		}

		public void RespawnCar()
		{
			if (this.DontPreventFromSliding)
			{
				return;
			}
			if (this.loadedOnOtherPlayerTrailer)
			{
				return;
			}
			Transform availableSpawnPoint = VehicleLoader.Instance.GetAvailableSpawnPoint();
			base.transform.position = availableSpawnPoint.position;
			base.transform.rotation = availableSpawnPoint.rotation;
			Utility.AlignVehicleByGround(base.transform, VehicleLoader.Instance.customMap || VehicleLoader.Instance.levelEditor);
			if (RacingManager.Instance != null && RacingManager.Instance.IsPlayerBusy)
			{
				RacingManager.Instance.CancelRace();
			}
			this.carUIControl.SwitchFlipButton(false);
			this.m_Rigidbody.velocity = Vector3.zero;
			this.m_Rigidbody.angularVelocity = Vector3.zero;
			this.m_Rigidbody.isKinematic = true;
			if (this.myTrailer != null)
			{
				bool flag = false;
				if (this.bodyPartsSwitcher == null)
				{
					flag = true;
				}
				if (this.vehicleDataManager.vehicleType == VehicleType.ATV)
				{
					flag = true;
				}
				if (this.myTrailer.gooseneck && this.bodyPartsSwitcher != null && this.bodyPartsSwitcher.gooseneckMount == null)
				{
					flag = true;
				}
				if (!flag)
				{
					this.myTrailer.Detach();
					this.myTrailer.Attach();
					this.myTrailer.rb.isKinematic = true;
				}
			}
			base.Invoke("UnfreezeCar", 0.5f);
		}

		private void UnfreezeCar()
		{
			this.m_Rigidbody.isKinematic = false;
			if (this.myTrailer != null)
			{
				this.myTrailer.rb.isKinematic = false;
			}
		}

		public void SetCalculatedCOM()
		{
			if (this.m_Rigidbody == null)
			{
				this.m_Rigidbody = base.GetComponent<Rigidbody>();
			}
			this.SetCOM(this.CalculateCOMPosition());
		}

		private void SetCOM(Vector3 comPos)
		{
			if (this.tankController != null)
			{
				return;
			}
			this.m_Rigidbody.centerOfMass = comPos;
		}

		private Vector3 CalculateCOMPosition()
		{
			if (this.monsterTruckCOM != null && this.monsterTruckCOM.gameObject.activeSelf && this.monsterTruckCOM.gameObject.activeInHierarchy)
			{
				return base.transform.InverseTransformPoint(this.monsterTruckCOM.position);
			}
			if (this.BodyColliders == null)
			{
				return Vector3.zero;
			}
			if (this.BodyColliders.Length == 0)
			{
				return Vector3.zero;
			}
			Vector3 vector = Vector3.zero;
			for (int i = 0; i < this.wheels.Count; i++)
			{
				vector += this.wheels[i].wc.transform.position;
			}
			if (this.wheels.Count > 0)
			{
				vector /= (float)this.wheels.Count;
			}
			else
			{
				vector = base.transform.position;
			}
			vector = base.transform.InverseTransformPoint(vector);
			Vector3 result = new Vector3(this.lowestPointOfCollider.x, this.lowestPointOfCollider.y, vector.z);
			return result;
		}

		private void DoAntiroll()
		{
			if (this.wheels.Count < 4)
			{
				return;
			}
			foreach (_Wheel wheel in this.wheels)
			{
				if (wheel.wc.wheelCollider == null)
				{
					return;
				}
			}
			float num = (this.wheels[0].wc.Compression - this.wheels[1].wc.Compression) * this.LateralAntiroll;
			float num2 = (this.wheels[2].wc.Compression - this.wheels[3].wc.Compression) * this.LateralAntiroll;
			this.m_Rigidbody.AddForceAtPosition(this.wheels[0].wc.transform.up * num, this.wheels[0].wc.transform.position);
			this.m_Rigidbody.AddForceAtPosition(this.wheels[1].wc.transform.up * -num, this.wheels[1].wc.transform.position);
			this.m_Rigidbody.AddForceAtPosition(this.wheels[2].wc.transform.up * num2, this.wheels[2].wc.transform.position);
			this.m_Rigidbody.AddForceAtPosition(this.wheels[3].wc.transform.up * -num2, this.wheels[3].wc.transform.position);
			float num3 = (this.wheels[0].wc.Compression + this.wheels[1].wc.Compression) / 2f;
			float num4 = (this.wheels[2].wc.Compression + this.wheels[3].wc.Compression) / 2f;
			float num5 = num3 - num4;
			Vector3 position = (this.wheels[0].wc.transform.position + this.wheels[1].wc.transform.position) / 2f;
			Vector3 position2 = (this.wheels[2].wc.transform.position + this.wheels[3].wc.transform.position) / 2f;
			float d = Mathf.InverseLerp(1f, 0.75f, base.transform.up.y);
			this.m_Rigidbody.AddForceAtPosition(num5 * base.transform.up * this.LongitudinalAntiroll * d, position);
			this.m_Rigidbody.AddForceAtPosition(-num5 * base.transform.up * this.LongitudinalAntiroll * d, position2);
		}

		private void DoTankAntiroll()
		{
			float num = (this.tankController.borderWheelColliders[0].correctedSuspensionCompression - this.tankController.borderWheelColliders[1].correctedSuspensionCompression) * this.LateralAntiroll;
			float num2 = (this.tankController.borderWheelColliders[2].correctedSuspensionCompression - this.tankController.borderWheelColliders[3].correctedSuspensionCompression) * this.LateralAntiroll;
			this.m_Rigidbody.AddForceAtPosition(this.tankController.borderWheelColliders[0].transform.up * num, this.tankController.borderWheelColliders[0].transform.position);
			this.m_Rigidbody.AddForceAtPosition(this.tankController.borderWheelColliders[1].transform.up * -num, this.tankController.borderWheelColliders[1].transform.position);
			this.m_Rigidbody.AddForceAtPosition(this.tankController.borderWheelColliders[2].transform.up * num2, this.tankController.borderWheelColliders[2].transform.position);
			this.m_Rigidbody.AddForceAtPosition(this.tankController.borderWheelColliders[3].transform.up * -num2, this.tankController.borderWheelColliders[3].transform.position);
			float num3 = (this.tankController.borderWheelColliders[0].correctedSuspensionCompression + this.tankController.borderWheelColliders[1].correctedSuspensionCompression) / 2f;
			float num4 = (this.tankController.borderWheelColliders[2].correctedSuspensionCompression + this.tankController.borderWheelColliders[3].correctedSuspensionCompression) / 2f;
			float num5 = num3 - num4;
			Vector3 position = (this.tankController.borderWheelColliders[0].transform.position + this.tankController.borderWheelColliders[1].transform.position) / 2f;
			Vector3 position2 = (this.tankController.borderWheelColliders[2].transform.position + this.tankController.borderWheelColliders[3].transform.position) / 2f;
			float d = Mathf.InverseLerp(1f, 0.75f, base.transform.up.y);
			this.m_Rigidbody.AddForceAtPosition(num5 * base.transform.up * this.LongitudinalAntiroll * d, position);
			this.m_Rigidbody.AddForceAtPosition(-num5 * base.transform.up * this.LongitudinalAntiroll * d, position2);
		}

		private int NotGroundedWheels()
		{
			int num = 0;
			if (this.tankController == null)
			{
				for (int i = 0; i < this.wheels.Count; i++)
				{
					if (!this.wheels[i].wc.IsGrounded)
					{
						num++;
					}
				}
			}
			if (this.tankController != null)
			{
				for (int j = 0; j < this.tankController.borderWheelColliders.Length; j++)
				{
					if (!this.tankController.borderWheelColliders[j].grounded)
					{
						num++;
					}
				}
			}
			return num;
		}

		private void DoAirForces()
		{
			bool flag = !this.Grounded;
			if (flag)
			{
				this.FlyingTime += Time.fixedDeltaTime;
			}
			else
			{
				this.StartAngularVelocity = base.transform.InverseTransformVector(this.m_Rigidbody.angularVelocity);
				this.FlyingTime = 0f;
			}
			if (this.TouchingGround)
			{
				this.StartAngularVelocity = Vector3.zero;
			}
			Vector3 vector = Vector3.ProjectOnPlane(base.transform.forward, Vector3.up);
			this.LongTilt = (base.transform.forward - vector).y;
			Vector3 b = Vector3.ProjectOnPlane(base.transform.right, Vector3.up);
			this.LatTilt = (base.transform.right - b).y;
			if (base.transform.up.y < 0f)
			{
				this.LatTilt = -this.LatTilt;
			}
			float num = (!flag) ? 0f : this.AirForceCurve.Evaluate(this.FlyingTime);
			float x = this.StartAngularVelocity.x;
			float y = (this.AirControlForce == 0f || this.TouchingGround || this.airControlPower <= 0f) ? this.StartAngularVelocity.y : (this.AirControlForce * this.airControlPower * this.xInput * num / 10f);
			float num2 = -this.StartAngularVelocity.z;
			if (this.TouchingGround && this.yInput == 0f)
			{
				x = base.transform.InverseTransformVector(this.m_Rigidbody.angularVelocity).x;
				num2 = base.transform.InverseTransformVector(this.m_Rigidbody.angularVelocity).z;
			}
			Vector3 vector2 = new Vector3(x - this.AirControlForce * this.airControlPower * this.yInput * num / 10f, y, -num2);
			Vector3 target = base.transform.TransformVector(vector2);
			if ((this.SelfAlignForceX > 0f || this.SelfAlignForceZ > 0f || this.AirControlForce * this.airControlPower > 0f) && flag)
			{
				this.m_Rigidbody.angularVelocity = Vector3.MoveTowards(this.m_Rigidbody.angularVelocity, target, Time.fixedDeltaTime * this.AlignSpeed);
			}
			bool flag2 = false;
			if (this.Driver != null)
			{
				flag2 = this.Driver.KnockedOut;
			}
			if (flag && !flag2)
			{
				if (base.transform.up.y < 0f && !this.Passed90Degrees)
				{
					this.Passed90Degrees = true;
				}
				this.BackFlip = (vector2.x < 0f);
				if (Vector3.Angle(Vector3.up, -base.transform.up) < 5f)
				{
					this.PassedVerticalState = true;
				}
				if (this.Passed90Degrees && this.PassedVerticalState && base.transform.up.y > 0f)
				{
					this.Passed90Degrees = false;
					this.PassedVerticalState = false;
					this.carUIControl.ShowNotification((!this.BackFlip) ? "Frontflip!" : "Backflip!", false);
					this.BackFlip = false;
					this.AngleCounter = 0f;
					this.prevForward = Vector3.zero;
				}
				if (this.Passed90Degrees || this.PassedVerticalState)
				{
					this.AngleCounter = 0f;
				}
			}
			else
			{
				this.Passed90Degrees = false;
				this.PassedVerticalState = false;
				this.BackFlip = false;
			}
			if (flag && !flag2)
			{
				if (this.prevForward == Vector3.zero)
				{
					this.prevForward = vector;
				}
				this.AngleCounter += Vector3.Angle(vector, this.prevForward) * Mathf.Sign(vector2.y);
				this.prevForward = vector;
				if (this.AngleCounter > 320f || this.AngleCounter < -320f)
				{
					this.AngleCounter = 0f;
					this.carUIControl.ShowNotification("Roll over!", false);
				}
			}
			else
			{
				this.AngleCounter = 0f;
				this.prevForward = Vector3.zero;
			}
		}

		private void UpdateFriction()
		{
			float num = (100f + PowerParts.GetPart(base.GetComponent<VehicleDataManager>().vehicleType, PowerPartType.Grip, this.GripStage).IncrementPercantage) / 100f;
			for (int i = 0; i < this.wheels.Count; i++)
			{
				this.wheels[i].wc.surfaceFrictionCoefficient = this.surfaceManager.GetTireFriction(i, (i <= 1) ? this.FrontInstalledTiresID : this.RearInstalledTiresID) * num;
				this.wheels[i].wc.UpdateFriction();
			}
		}

		public void SetZeroFriction()
		{
			foreach (_Wheel wheel in this.wheels)
			{
				wheel.wc.forwardFrictionCoefficient = (wheel.wc.sideFrictionCoefficient = (wheel.wc.surfaceFrictionCoefficient = 0f));
				wheel.wc.UpdateFriction();
			}
		}

		public void SetDefaultFriction()
		{
			foreach (_Wheel wheel in this.wheels)
			{
				wheel.wc.forwardFrictionCoefficient = (wheel.wc.sideFrictionCoefficient = (wheel.wc.surfaceFrictionCoefficient = 1f));
				wheel.wc.UpdateFriction();
			}
		}

		public void UpdateEngineModel()
		{
			EngineType engineType = EngineType.Stock;
			if (this.BlowerStage > 0)
			{
				engineType = EngineType.Blower;
			}
			if (this.TurboStage > 0 || this.DieselStage == 4)
			{
				engineType = EngineType.Turbo;
			}
			base.GetComponent<BodyPartsSwitcher>().UpdateEngineModel(engineType);
		}

		public float GetMaxTorque()
		{
			if (this.vehicleDataManager == null)
			{
				return 0f;
			}
			PowerPart part = PowerParts.GetPart(this.vehicleDataManager.vehicleType, PowerPartType.EngineBlock, this.EngineBlockStage);
			PowerPart part2 = PowerParts.GetPart(this.vehicleDataManager.vehicleType, PowerPartType.Head, this.HeadStage);
			PowerPart part3 = PowerParts.GetPart(this.vehicleDataManager.vehicleType, PowerPartType.Valvetrain, this.ValvetrainStage);
			PowerPart part4 = PowerParts.GetPart(this.vehicleDataManager.vehicleType, PowerPartType.Turbo, this.TurboStage);
			PowerPart part5 = PowerParts.GetPart(this.vehicleDataManager.vehicleType, PowerPartType.Blower, this.BlowerStage);
			float num = 0f;
			if (part != null)
			{
				num += part.IncrementPercantage;
			}
			if (part2 != null)
			{
				num += part2.IncrementPercantage;
			}
			if (part3 != null)
			{
				num += part3.IncrementPercantage;
			}
			if (part4 != null)
			{
				num += part4.IncrementPercantage;
			}
			if (part5 != null)
			{
				num += part5.IncrementPercantage;
			}
			float f = this.PerfectFuelRatio - this.FuelRatio;
			float num2 = Mathf.Lerp(5f, -15f, Mathf.Abs(f) / 10f);
			num += num2;
			float f2 = this.PerfectTimingRatio - this.TimingRatio;
			float num3 = Mathf.Lerp(5f, -15f, Mathf.Abs(f2) / 10f);
			num += num3;
			float num4 = this.ModsAdditionalBoost;
			if (num < 0f)
			{
				num4 = 1f;
			}
			return this.BaseTorque / 100f * (100f + num * num4);
		}

		private void UpdateMotorPower()
		{
			if (this.vehicleDataManager == null)
			{
				return;
			}
			PowerPart part = PowerParts.GetPart(this.vehicleDataManager.vehicleType, PowerPartType.EngineBlock, this.EngineBlockStage);
			PowerPart part2 = PowerParts.GetPart(this.vehicleDataManager.vehicleType, PowerPartType.Head, this.HeadStage);
			PowerPart part3 = PowerParts.GetPart(this.vehicleDataManager.vehicleType, PowerPartType.Valvetrain, this.ValvetrainStage);
			PowerPart part4 = PowerParts.GetPart(this.vehicleDataManager.vehicleType, PowerPartType.Weight, this.WeightStage);
			PowerPart part5 = PowerParts.GetPart(this.vehicleDataManager.vehicleType, PowerPartType.Weight, this.DurabilityStage);
			PowerPart part6 = PowerParts.GetPart(this.vehicleDataManager.vehicleType, PowerPartType.Turbo, this.TurboStage);
			PowerPart part7 = PowerParts.GetPart(this.vehicleDataManager.vehicleType, PowerPartType.Blower, this.BlowerStage);
			this.FinalTorquePercentage = 0f;
			if (part != null)
			{
				this.FinalTorquePercentage += part.IncrementPercantage;
			}
			if (part2 != null)
			{
				this.FinalTorquePercentage += part2.IncrementPercantage;
			}
			if (part3 != null)
			{
				this.FinalTorquePercentage += part3.IncrementPercantage;
			}
			if (part4 != null)
			{
				this.FinalTorquePercentage += part4.IncrementPercantage;
			}
			if (part6 != null)
			{
				this.FinalTorquePercentage += part6.IncrementPercantage;
			}
			if (part7 != null)
			{
				this.FinalTorquePercentage += part7.IncrementPercantage;
			}
			float f = this.PerfectFuelRatio - this.FuelRatio;
			float num = Mathf.Lerp(5f, -15f, Mathf.Abs(f) / 10f);
			this.FinalTorquePercentage += num;
			float f2 = this.PerfectTimingRatio - this.TimingRatio;
			float num2 = Mathf.Lerp(5f, -15f, Mathf.Abs(f2) / 10f);
			this.FinalTorquePercentage += num2;
			float num3 = Mathf.Lerp(0.5f, 1f, this.CarHealth / 100f);
			float num4 = this.ModsAdditionalBoost;
			float num5 = this.ModsMaxSpeedBoost;
			if (this.FinalTorquePercentage < 0f)
			{
				num4 = 1f;
				num5 = 1f;
			}
			float num6 = this.BaseTorque / 100f * (100f + this.FinalTorquePercentage);
			this.LeveledMaxTorque = this.BaseTorque * num3 / 100f * (100f + this.FinalTorquePercentage * num4);
			this.LeveledMaxSpeed = this.BaseMaxSpeed * num3 / 100f * (100f + this.FinalTorquePercentage * num4 * num5);
			if (this.tankController == null)
			{
				this.engine.TopGear = 9f - 4f * ((num6 - 80f) / 280f);
			}
			else
			{
				this.engine.TopGear = 9f;
			}
		}

		private void SetupCounterWheels()
		{
			if (this.wheels.Count < 4)
			{
				return;
			}
			this.wheels[0].wc.wheelCollider.OppositeWheel = this.wheels[1].wc.wheelCollider;
			this.wheels[1].wc.wheelCollider.OppositeWheel = this.wheels[0].wc.wheelCollider;
			this.wheels[2].wc.wheelCollider.OppositeWheel = this.wheels[3].wc.wheelCollider;
			this.wheels[3].wc.wheelCollider.OppositeWheel = this.wheels[2].wc.wheelCollider;
			this.wheels[0].wc.wheelCollider.AnotherAxleWheelL = (this.wheels[1].wc.wheelCollider.AnotherAxleWheelL = this.wheels[2].wc.wheelCollider);
			this.wheels[0].wc.wheelCollider.AnotherAxleWheelR = (this.wheels[1].wc.wheelCollider.AnotherAxleWheelR = this.wheels[3].wc.wheelCollider);
			this.wheels[2].wc.wheelCollider.AnotherAxleWheelL = (this.wheels[3].wc.wheelCollider.AnotherAxleWheelL = this.wheels[0].wc.wheelCollider);
			this.wheels[2].wc.wheelCollider.AnotherAxleWheelR = (this.wheels[3].wc.wheelCollider.AnotherAxleWheelR = this.wheels[1].wc.wheelCollider);
			if (this.wheels.Count > 4)
			{
				this.wheels[4].wc.wheelCollider.OppositeWheel = this.wheels[5].wc.wheelCollider;
				this.wheels[5].wc.wheelCollider.OppositeWheel = this.wheels[4].wc.wheelCollider;
			}
		}

		private void SetupFrictionValues()
		{
			for (int i = 0; i < this.wheels.Count; i++)
			{
				if (this.wheels[i].wc != null)
				{
					this.wheels[i].wc.f_extSlip = ((i <= 1) ? this.FrontFriction.f_ExtremumSlip : this.RearFriction.f_ExtremumSlip);
					this.wheels[i].wc.f_extVal = ((i <= 1) ? this.FrontFriction.f_ExtremumValue : this.RearFriction.f_ExtremumValue);
					this.wheels[i].wc.f_asSlip = ((i <= 1) ? this.FrontFriction.f_AsymptoteSlip : this.RearFriction.f_AsymptoteSlip);
					this.wheels[i].wc.f_asVal = ((i <= 1) ? this.FrontFriction.f_AsymptoteValue : this.RearFriction.f_AsymptoteValue);
					this.wheels[i].wc.f_tailVal = ((i <= 1) ? this.FrontFriction.f_TailValue : this.RearFriction.f_TailValue);
					this.wheels[i].wc.s_extSlip = ((i <= 1) ? this.FrontFriction.s_ExtremumSlip : this.RearFriction.s_ExtremumSlip);
					this.wheels[i].wc.s_extVal = ((i <= 1) ? this.FrontFriction.s_ExtremumValue : this.RearFriction.s_ExtremumValue);
					this.wheels[i].wc.s_asSlip = ((i <= 1) ? this.FrontFriction.s_AsymptoteSlip : this.RearFriction.s_AsymptoteSlip);
					this.wheels[i].wc.s_asVal = ((i <= 1) ? this.FrontFriction.s_AsymptoteValue : this.RearFriction.s_AsymptoteValue);
					this.wheels[i].wc.s_tailVal = ((i <= 1) ? this.FrontFriction.s_TailValue : this.RearFriction.s_TailValue);
					this.wheels[i].wc.SpringCurve = ((i <= 1) ? this.frontSpringCurve : this.rearSpringCurve);
				}
			}
		}

		private void GetDataFromSurfaceManager()
		{
			if (this.carUIControl != null && !VehicleLoader.Instance.droneMode)
			{
				this.carUIControl.SwitchFlipButton(base.transform.up.y < 0f && Mathf.Abs(this.Speed) < 2f);
			}
			this.nextSurfaceManagerDataUpdateTime = Time.time + this.SurfaceManagerDataUpdateInterval;
			this.UpdateMotorPower();
			this.CheckOverheating();
			if (this.surfaceManager != null)
			{
				this.CheckWaterDamage();
				this.UpdateFriction();
			}
		}

		private void SetDiffLock()
		{
			foreach (_Wheel wheel in this.wheels)
			{
				if (wheel.wc.wheelCollider == null)
				{
					return;
				}
			}
			for (int i = 0; i < this.wheels.Count; i++)
			{
				this.wheels[i].wc.wheelCollider.DiffLock = ((i <= 1) ? this.FrontDiffLock : this.RearDiffLock);
				this.wheels[i].wc.wheelCollider.InteraxleDifLock = this.InteraxleDiffLock;
				this.wheels[i].wc.wheelCollider.DiffLockRatio = ((i <= 1) ? this.FrontDiffLockRatio : this.RearDiffLockRatio);
				this.wheels[i].wc.wheelCollider.InteraxleDiffLockRatio = this.InteraxleDiffLockRatio;
			}
		}

		private void SetDiffLock(int TypeID)
		{
			this.RearDiffLock = (this.FrontDiffLock = (this.InteraxleDiffLock = false));
			if (TypeID > 0)
			{
				this.RearDiffLock = true;
			}
			if (TypeID > 1)
			{
				this.FrontDiffLock = true;
			}
			if (TypeID > 2)
			{
				this.InteraxleDiffLock = true;
			}
			this.OnValidate();
		}

		private void SetDrive(int TypeID)
		{
			this.FWD = (this.RWD = false);
			if (TypeID == 0)
			{
				this.RWD = true;
			}
			if (TypeID == 1)
			{
				this.FWD = true;
			}
			if (TypeID == 2)
			{
				this.FWD = true;
				this.RWD = true;
			}
			this.OnValidate();
		}

		public void SteerTowards(Vector3 pos)
		{
			Vector3 vector = base.transform.InverseTransformDirection(pos - base.transform.position);
			float num = -Mathf.Atan2(-vector.x, vector.z) * 57.29578f;
			this.xInput = Mathf.Clamp(num / this.maxSteeringAngle, -1f, 1f);
		}

		private void DoInput()
		{
			List<Touch> touches = InputHelper.GetTouches();
			this.xInput = UnityEngine.Input.GetAxis("Horizontal") + CrossPlatformInputManager.GetAxis("Horizontal");
			this.yInput = UnityEngine.Input.GetAxis("Vertical") + CrossPlatformInputManager.GetAxis("Vertical");
			
			
			#if UNITY_EDITOR
			if (touches.Count == 0)
			{
				this.yInput = 0f;
			}
			#else
if (UnityEngine.Input.touchCount == 0)
			{
				this.yInput = 0f;
			}

#endif
			
			
			if (CrossPlatformInputManager.GetButtonDown("SetDiffLock0"))
			{
				this.SetDiffLock(0);
			}
			if (CrossPlatformInputManager.GetButtonDown("SetDiffLock1"))
			{
				this.SetDiffLock(1);
			}
			if (CrossPlatformInputManager.GetButtonDown("SetDiffLock2"))
			{
				this.SetDiffLock(2);
			}
			if (CrossPlatformInputManager.GetButtonDown("SetDiffLock3"))
			{
				this.SetDiffLock(3);
			}
			if (CrossPlatformInputManager.GetButtonDown("SetLowGear"))
			{
				this.LowGear = true;
			}
			if (CrossPlatformInputManager.GetButtonDown("SetHighGear"))
			{
				this.LowGear = false;
			}
			if (CrossPlatformInputManager.GetButtonDown("SetDrive0"))
			{
				this.SetDrive(0);
			}
			if (CrossPlatformInputManager.GetButtonDown("SetDrive1"))
			{
				this.SetDrive(1);
			}
			if (CrossPlatformInputManager.GetButtonDown("SetDrive2"))
			{
				this.SetDrive(2);
			}
			if (CrossPlatformInputManager.GetButtonDown("Repair"))
			{
				this.RepairVehicle();
			}
		}

		private void DoCarHandling()
		{
			this.Handbraking = (float)((!CrossPlatformInputManager.GetButton("Ebrake")) ? 0 : 1);
			float target = 0f;
			if (this.Speed > 1f || this.transmissionType == TransmissionType.Manual)
			{
				target = -Mathf.Clamp(this.yInput, -1f, 0f);
			}
			this.Braking = Mathf.MoveTowards(this.Braking, target, Time.deltaTime * 50f);
			this.Braking = Mathf.Max(this.Braking, this.ExtremeBraking);
			float num = this.yInput;
			if ((this.Speed > 1f && this.Grounded) || this.transmissionType == TransmissionType.Manual)
			{
				num = Mathf.Clamp(this.yInput, 0f, 1f);
			}
			if (this.transmissionType == TransmissionType.Manual && this.engine.ReverseGear)
			{
				num = -num;
			}
			this.Throttle = num;
			float leveledMaxSpeed = this.LeveledMaxSpeed;
			float rpm = this.engine.RPM;
			float maxRpm = this.engine.maxRpm;
			float num2 = this.engine.Gears[this.engine.Gear];
			float topGear = this.engine.TopGear;
			float num3 = (!this.LowGear) ? 1f : this.LowGearRatio;
			float num4 = Mathf.Clamp01(1f - Mathf.Abs(this.Speed) / leveledMaxSpeed) / 2f;
			float num5 = (float)((!this.FWD || !this.RWD) ? 2 : 1);
			this.CurrentTorque = this.LeveledMaxTorque * this.DynoCurve.Evaluate(rpm / maxRpm) * num2 * topGear * num3 * num4 * num5;
			if (float.IsNaN(this.CurrentTorque))
			{
				this.CurrentTorque = 0f;
			}
			if (this.Throttle == 0f)
			{
				this.CurrentTorque = 0f;
			}
			float target2 = Mathf.Lerp(this.maxSteeringAngle * 0.1f * this.xInput, this.maxSteeringAngle * this.xInput, 1f - this.Speed / leveledMaxSpeed * this.SteerLimitOnSpeed);
			this.Steering = Mathf.MoveTowards(this.Steering, target2, Time.deltaTime * 100f);
			Quaternion b = Quaternion.Euler(0f, 0f, Mathf.LerpUnclamped(this.SteeringWheelMaxAngle, 0f, this.Steering / this.maxSteeringAngle + 1f));
			if (this.SteeringWheel != null)
			{
				this.SteeringWheel.localRotation = Quaternion.Lerp(this.SteeringWheel.localRotation, b, Time.deltaTime * 5f);
			}
			if (this.engine != null && this.engine.NeutralGear)
			{
				this.CurrentTorque = 0f;
			}
			this.currentBrakeTorque = this.BrakeTorque * this.Braking;
			foreach (_Wheel wheel in this.wheels)
			{
				if (wheel.wc.wheelCollider == null)
				{
					break;
				}
				if (wheel.wc.wheelCollider.OppositeWheel == null)
				{
					this.SetupCounterWheels();
				}
				wheel.wc.MotorTorque = ((!wheel.power) ? 0f : (this.CurrentTorque * this.Throttle));
				if (wheel.steer)
				{
					wheel.wc.Steer = this.Steering;
				}
				if (wheel.inverseSteer)
				{
					wheel.wc.Steer = -this.Steering * this.InverseSteerMultiplier;
				}
				wheel.wc.BrakeTorque = this.currentBrakeTorque;
				if (wheel.handbrake)
				{
					wheel.wc.BrakeTorque = this.BrakeTorque * Mathf.Max(this.Handbraking * 3f, this.Braking);
				}
				if (this.CurrentTorque * this.Throttle == 0f && this.Braking == 0f && this.Handbraking == 0f && this.ExtremeBraking == 0f)
				{
					wheel.wc.BrakeTorque = this.BrakeTorque / 2f * this.RollingResistance;
				}
			}
		}

		private void GotHit(Collision col)
		{
			bool flag = false;
			foreach (ContactPoint contactPoint in col.contacts)
			{
				foreach (Collider obj in this.BodyColliders)
				{
					if (contactPoint.thisCollider.Equals(obj))
					{
						flag = true;
						break;
					}
				}
			}
			if (Vector3.Angle(base.transform.up, col.impulse) < 20f)
			{
				flag = false;
			}
			if (!flag)
			{
				return;
			}
			if (col.impulse.magnitude < 100f)
			{
				return;
			}
			if (col.gameObject.GetPhotonView() != null)
			{
				return;
			}
			float num = Mathf.InverseLerp(0f, this.MaximumHitDamageForce, col.impulse.magnitude);
			float num2 = this.MaximumHitDamage * num;
			num2 *= 1f - (float)this.DurabilityStage * 0.01f;
			if (GameState.GameMode == GameMode.Multiplayer || GameState.SceneName == "StuntPark")
			{
				num2 *= 0.5f;
			}
			this.CarHealth = Mathf.Clamp(this.CarHealth - num2, 0f, 100f);
		}

		private void CheckWaterDamage()
		{
			if (!this.surfaceManager.IsCarInWater() || this.HasSnorkel)
			{
				return;
			}
			Vector3 position = this.surfaceManager.WaterMeshes[this.surfaceManager.WhatWaterMeshIsCarOn()].transform.position;
			if (this.DamageWaterline.position.y < position.y)
			{
				this.DoWaterDamage(this.WaterDamage);
			}
		}

		private void DoWaterDamage(float Value)
		{
			this.CarHealth = Mathf.Clamp(this.CarHealth - Value, 0f, 100f);
			this.carUIControl.ShowNotification("Water damage!", false);
			CameraController.Instance.Shake();
		}

		private void CheckOverheating()
		{
			float num = 0f;
			if (Mathf.Abs(this.Speed) > 1f)
			{
				num += ((!this.FWD || !this.RWD) ? 0f : this.FullWDTemperatureStep);
				num += ((!this.FrontDiffLock && !this.RearDiffLock) ? 0f : this.DiffLockTemperatureStep);
				num += ((!this.LowGear) ? 0f : this.LowGearTemperatureStep);
			}
			num -= this.CoolingStep + this.CoolingStep * (1f - (float)this.DurabilityStage * 0.01f);
			this.DrivetrainTemperature = Mathf.Clamp(this.DrivetrainTemperature + num, 0f, this.MaxTemperature);
			if (this.DrivetrainTemperature > this.DamageTemperature)
			{
				this.DoOverheatDamage(this.OverheatDamage);
			}
			float temperatureRatio = Mathf.InverseLerp(0f, this.DamageTemperature, this.DrivetrainTemperature);
			if (this.carUIControl != null)
			{
				this.carUIControl.UpdateThermometer(temperatureRatio);
			}
		}

		private void DoOverheatDamage(float Value)
		{
			this.CarHealth = Mathf.Clamp(this.CarHealth - Value, 0f, 100f);
			this.carUIControl.ShowNotification("Overheating!", false);
			CameraController.Instance.Shake();
		}

		private CarControllerData GetCarControllerData()
		{
			return new CarControllerData
			{
				CarHealth = this.CarHealth,
				EngineBlockStage = this.EngineBlockStage,
				GripStage = this.GripStage,
				HeadStage = this.HeadStage,
				ValvetrainStage = this.ValvetrainStage,
				WeightStage = this.WeightStage,
				DurabilityStage = this.DurabilityStage,
				TurboStage = this.TurboStage,
				BlowerStage = this.BlowerStage,
				GearingStage = this.GearingStage,
				DieselStage = this.DieselStage,
				TransmissionType = (int)this.transmissionType,
				ManualTransmissionPurchased = this.ManualTransmissionPurchased,
				DieselPurchased = this.DieselPurchased,
				PurchasedBlowerStage = this.PurchasedBlowerStage,
				PurchasedTurboStage = this.PurchasedTurboStage,
				TankTracksPurchased = this.TankTracksPurchased,
				TuningEnginePurchased = this.TuningEnginePurchased,
				PerfectSetupPurchased = this.PerfectSetupPurchased,
				GearRatios = this.GearRatios,
				LowGearRatio = this.LowGearRatio,
				Ebrake = this.Ebrake,
				FuelRatio = this.FuelRatio,
				TimingRatio = this.TimingRatio,
				PerfectFuelRatio = this.PerfectFuelRatio,
				PerfectTimingRatio = this.PerfectTimingRatio,
				frontDuallyPurchased = this.frontDuallyPurchased,
				rearDuallyPurchased = this.rearDuallyPurchased
			};
		}

		public void SetCarControllerData(CarControllerData cData)
		{
			this.CarHealth = cData.CarHealth;
			this.EngineBlockStage = cData.EngineBlockStage;
			this.GripStage = cData.GripStage;
			this.HeadStage = cData.HeadStage;
			this.ValvetrainStage = cData.ValvetrainStage;
			this.WeightStage = cData.WeightStage;
			this.DurabilityStage = cData.DurabilityStage;
			this.TurboStage = cData.TurboStage;
			this.BlowerStage = cData.BlowerStage;
			this.GearingStage = cData.GearingStage;
			this.DieselStage = cData.DieselStage;
			this.transmissionType = (TransmissionType)cData.TransmissionType;
			this.ManualTransmissionPurchased = cData.ManualTransmissionPurchased;
			this.PurchasedTurboStage = cData.PurchasedTurboStage;
			this.PurchasedBlowerStage = cData.PurchasedBlowerStage;
			this.DieselPurchased = cData.DieselPurchased;
			this.TankTracksPurchased = cData.TankTracksPurchased;
			this.TuningEnginePurchased = cData.TuningEnginePurchased;
			this.PerfectSetupPurchased = cData.PerfectSetupPurchased;
			this.FuelRatio = cData.FuelRatio;
			this.TimingRatio = cData.TimingRatio;
			this.PerfectFuelRatio = cData.PerfectFuelRatio;
			this.PerfectTimingRatio = cData.PerfectTimingRatio;
			this.frontDuallyPurchased = cData.frontDuallyPurchased;
			this.rearDuallyPurchased = cData.rearDuallyPurchased;
			if (cData.GearRatios != null && cData.GearRatios.Length == 5)
			{
				this.GearRatios = cData.GearRatios;
				this.LowGearRatio = cData.LowGearRatio;
			}
			if (this.PerfectFuelRatio == 0f)
			{
				this.PerfectFuelRatio = UnityEngine.Random.Range(-10f, 10f);
			}
			if (this.PerfectTimingRatio == 0f)
			{
				this.PerfectTimingRatio = UnityEngine.Random.Range(-10f, 10f);
			}
			this.PerfectFuelRatio = Mathf.Round(this.PerfectFuelRatio / 0.5f) * 0.5f;
			this.PerfectTimingRatio = Mathf.Round(this.PerfectTimingRatio / 0.5f) * 0.5f;
			this.Ebrake = cData.Ebrake;
			this.OnValidate();
			this.UpdateEngineModel();
		}

		public string ExportData()
		{
			CarControllerData carControllerData = this.GetCarControllerData();
			return XmlSerialization.SerializeData<CarControllerData>(carControllerData);
		}

		public void ImportData(string XMLString)
		{
			CarControllerData carControllerData = (CarControllerData)XmlSerialization.DeserializeData<CarControllerData>(XMLString);
			this.SetCarControllerData(carControllerData);
		}

		public bool vehicleIsActive;

		[HideInInspector]
		public Rigidbody m_Rigidbody;

		private BodyPartsSwitcher bodyPartsSwitcher;

		private CarUIControl carUIControl;

		private EngineController engine;

		private IKDriverController Driver;

		[HideInInspector]
		public VehicleDataManager vehicleDataManager;

		private PhotonTransformView myTransformView;

		[HideInInspector]
		public TrailerController myTrailer;

		[HideInInspector]
		public TankController tankController;

		[Header("Setup")]
		[SerializeField]
		public List<_Wheel> wheels;

		public Collider[] BodyColliders;

		public Transform monsterTruckCOM;

		public Transform SteeringWheel;

		public float SteeringWheelMaxAngle;

		public Transform Shadow;

		[Header("Camera settings")]
		public Transform FirstPersonPoint;

		public float FollowDistance = 4f;

		public float FollowYAngle = 20f;

		public float SideDistance = 4f;

		public float GarageMaxDistance = 6f;

		public float GarageMinDistance = 3f;

		[Header("Damaging")]
		public float CarHealth = 100f;

		public float MaximumHitDamageForce = 20000f;

		public float MaximumHitDamage = 10f;

		[Space(20f)]
		public Transform DamageWaterline;

		public float WaterDamage = 5f;

		[Space(20f)]
		public float OverheatDamage = 5f;

		public float MaxTemperature = 60f;

		public float DamageTemperature = 50f;

		public float LowGearTemperatureStep = 1f;

		public float DiffLockTemperatureStep = 1f;

		public float FullWDTemperatureStep = 0.5f;

		public float CoolingStep = 1f;

		private float DrivetrainTemperature;

		[Header("Differential locks")]
		public bool FrontDiffLock;

		public bool RearDiffLock;

		public bool InteraxleDiffLock;

		private float FrontDiffLockRatio = 1f;

		private float RearDiffLockRatio = 1f;

		private float InteraxleDiffLockRatio = 1f;

		[Space(10f)]
		[Header("Handling")]
		public bool FWD;

		public bool RWD;

		public bool LowGear;

		public float maxSteeringAngle = 30f;

		public float BrakeTorque = 1000f;

		[HideInInspector]
		public float currentBrakeTorque;

		[Space(10f)]
		public float BaseTorque = 500f;

		public float BaseMaxSpeed = 70f;

		public float ModsMaxSpeedBoost = 1.3f;

		public float ModsAdditionalBoost = 1.1f;

		public AnimationCurve DynoCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0.2f),
			new Keyframe(1f, 1f)
		});

		[HideInInspector]
		public float LeveledMaxTorque;

		[HideInInspector]
		public float LeveledMaxSpeed;

		[Space(10f)]
		[Range(0f, 4f)]
		public int EngineBlockStage;

		[Range(0f, 4f)]
		public int HeadStage;

		[Range(0f, 4f)]
		public int ValvetrainStage;

		[Range(0f, 4f)]
		public int GripStage;

		[Range(0f, 4f)]
		public int WeightStage;

		[Range(0f, 4f)]
		public int DurabilityStage;

		[Range(0f, 4f)]
		public int GearingStage;

		[Range(0f, 4f)]
		public int TurboStage;

		[Range(0f, 4f)]
		public int BlowerStage;

		[Range(0f, 1f)]
		public int DieselStage = 3;

		public int PurchasedTurboStage;

		public int PurchasedBlowerStage;

		[HideInInspector]
		public bool ManualTransmissionPurchased;

		[HideInInspector]
		public bool DieselPurchased;

		[HideInInspector]
		public bool TankTracksPurchased;

		[HideInInspector]
		public bool frontDuallyPurchased;

		[HideInInspector]
		public bool rearDuallyPurchased;

		public TransmissionType transmissionType;

		public float[] GearRatios = GearsManager.DefaultGears;

		public float LowGearRatio = GearsManager.DefaultLowGear;

		public int MaxGear = 5;

		[Range(0f, 1f)]
		public int Ebrake;

		[Space(10f)]
		[Header("Engine tuning")]
		public bool TuningEnginePurchased;

		public bool PerfectSetupPurchased;

		[Range(-10f, 10f)]
		public float FuelRatio;

		[Range(-10f, 10f)]
		public float TimingRatio;

		public float PerfectFuelRatio;

		public float PerfectTimingRatio;

		[Space(10f)]
		[Header("Stability")]
		[Range(0f, 10000f)]
		public float LateralAntiroll = 5000f;

		[Range(0f, 10000f)]
		public float LongitudinalAntiroll = 5000f;

		[Range(0f, 1f)]
		public float RollingResistance = 0.2f;

		[Range(0f, 1f)]
		public float SteerLimitOnSpeed = 0.5f;

		public bool PreventFromSideSliding = true;

		[Space(10f)]
		[Range(0f, 50f)]
		public float SelfAlignForceX;

		[Range(0f, 50f)]
		public float SelfAlignForceZ;

		[Range(0f, 20f)]
		public float AlignSpeed = 5f;

		[Range(0f, 50f)]
		public float AirControlForce = 5f;

		public AnimationCurve AirForceCurve;

		[HideInInspector]
		public int WheelsOffTheGround;

		[HideInInspector]
		public bool Grounded;

		private Vector3 StartAngularVelocity;

		private float FlyingTime;

		[HideInInspector]
		public float LongTilt;

		[HideInInspector]
		public float LatTilt;

		private float airControlPower;

		private float AngleCounter;

		private Vector3 prevForward;

		private bool PassedVerticalState;

		private bool Passed90Degrees;

		private bool BackFlip;

		private bool TouchingGround;

		[Space(10f)]
		[Header("Friction")]
		public float SurfaceManagerDataUpdateInterval = 1f;

		[HideInInspector]
		public int FrontInstalledTiresID;

		[HideInInspector]
		public int RearInstalledTiresID;

		private SurfaceManager surfaceManager;

		public FrictionSettings FrontFriction;

		public FrictionSettings RearFriction;

		public AnimationCurve frontSpringCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		public AnimationCurve rearSpringCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		private Vector3 lastVelocity;

		[HideInInspector]
		public Vector3 acceleration;

		[HideInInspector]
		public float Handbraking;

		[HideInInspector]
		public float Braking;

		[HideInInspector]
		public float ExtremeBraking;

		[HideInInspector]
		public float Throttle;

		[HideInInspector]
		public float Speed;

		[HideInInspector]
		public float AngularSpeed;

		[HideInInspector]
		public float Steering;

		[HideInInspector]
		public float InverseSteerMultiplier;

		[HideInInspector]
		public float xInput;

		[HideInInspector]
		public float yInput;

		private float nextSurfaceManagerDataUpdateTime;

		[HideInInspector]
		public float FakeRPM;

		private bool HasSnorkel;

		private bool IsSlideThrottle;

		private Vector3 lowestPointOfCollider;

		[HideInInspector]
		public bool DontPreventFromSliding;

		[HideInInspector]
		public bool loadedOnOtherPlayerTrailer;

		[HideInInspector]
		public PhotonTransformView ownerOfTrailer;

		[HideInInspector]
		public PhotonTransformView ownerOfTrailerWeWantToLoadOn;

		private bool waitingForTrailerResponse;

		public float FinalTorquePercentage;

		public float CurrentTorque;
	}
}
