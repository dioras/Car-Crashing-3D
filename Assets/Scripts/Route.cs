using System;
using System.Collections.Generic;
using CustomVP;
using PlayFab;
using PlayFab.AdminModels;
using PlayFab.ClientModels;
using UnityEngine;

[Serializable]
public class Route : MonoBehaviour
{
	private VehicleDataManager vehicleDataManager
	{
		get
		{
			if (VehicleLoader.Instance != null)
			{
				return VehicleLoader.Instance.playerDataManager;
			}
			return null;
		}
	}

	private void Start()
	{
		RacingManager.Instance.RegisterRoute(this);
		if (!this.Circuit)
		{
			this.LapsNumber = 1;
		}
		for (int i = 0; i < this.SpawnedCheckpoints.Count; i++)
		{
			Checkpoint checkpoint = this.SpawnedCheckpoints[i].AddComponent<Checkpoint>();
			checkpoint.ID = i;
			BoxCollider boxCollider = checkpoint.gameObject.AddComponent<BoxCollider>();
			boxCollider.size = new Vector3(5f, 10f, 1f);
			boxCollider.isTrigger = true;
		}
		if (GameState.GameType == GameType.TrailRace)
		{
			GameObject gameObject = GameObject.Find("RouteIndicator(Clone)");
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = this.RouteColor;
		for (int i = 0; i < this.Waypoints.Count; i++)
		{
			if (this.Waypoints.Count > i + 1)
			{
				this.DrawThickLine(this.Waypoints[i].position, this.Waypoints[i + 1].position);
			}
			else if (this.Circuit)
			{
				this.DrawThickLine(this.Waypoints[i].position, this.Waypoints[0].position);
			}
		}
		for (int j = 0; j < this.Waypoints.Count; j++)
		{
			this.DrawNormalizedSphere(this.Waypoints[j].position, 0.5f);
		}
		if (this.DrawTextureOnTerrain)
		{
			Gizmos.DrawWireCube(base.transform.position + Vector3.up, new Vector3((float)(this.StampDiameter * 2), 0f, (float)(this.StampDiameter * 2)));
		}
	}

	private void DrawNormalizedSphere(Vector3 center, float radius)
	{
		if (!this.ThickLine)
		{
			Gizmos.DrawSphere(center, 0.25f);
			return;
		}
		Camera current = Camera.current;
		float num = Vector3.Distance(current.transform.position, center);
		Gizmos.DrawSphere(center, 0.02f * num);
	}

	private void DrawThickLine(Vector3 Point1, Vector3 Point2)
	{
		if (!this.ThickLine)
		{
			Gizmos.DrawLine(Point1, Point2);
			return;
		}
		Camera current = Camera.current;
		Vector3 normalized = (Point1 - Point2).normalized;
		Vector3 a = Quaternion.Euler(0f, 90f, 0f) * normalized;
		float d = Vector3.Distance(current.transform.position, (Point1 + Point2) / 2f);
		for (int i = 0; i < 5; i++)
		{
			Vector3 b = a / 100f * d * ((float)i / 9f);
			Gizmos.DrawLine(Point1 + b, Point2 + b);
			Gizmos.DrawLine(Point1 - b, Point2 - b);
		}
	}

	public void HideShowAllCheckpoints(bool Show)
	{
		if (this.SpawnedCheckpoints != null)
		{
			foreach (GameObject gameObject in this.SpawnedCheckpoints)
			{
				gameObject.SetActive(Show);
			}
		}
		if (this.SpawnedFences != null)
		{
			foreach (GameObject gameObject2 in this.SpawnedFences)
			{
				gameObject2.SetActive(Show);
			}
		}
		if (this.SpawnedRopes != null)
		{
			foreach (LineRenderer lineRenderer in this.SpawnedRopes)
			{
				lineRenderer.gameObject.SetActive(Show);
			}
		}
	}

	public void HideShowStartAndFinish(bool Show)
	{
		if (this.SpawnedFinish != null)
		{
			this.SpawnedFinish.SetActive(Show);
		}
		if (this.SpawnedStart != null)
		{
			this.SpawnedStart.SetActive(Show);
		}
	}

	public void HideShowEventMark(bool Show)
	{
		if (this.SpawnedEventMark != null)
		{
			this.SpawnedEventMark.SetActive(Show);
		}
	}

	public void ShowCheckpoint(int ID)
	{
		if (!this.AlwaysShowCheckpoints)
		{
			for (int i = 0; i < this.SpawnedCheckpoints.Count; i++)
			{
				this.SpawnedCheckpoints[i].SetActive(ID == i);
			}
		}
		if (this.AlwaysShowCheckpoints)
		{
			for (int j = 0; j < this.SpawnedCheckpoints.Count; j++)
			{
				this.SpawnedCheckpoints[j].SetActive(j >= ID);
			}
		}
		if (this.SpawnedFences != null)
		{
			foreach (GameObject gameObject in this.SpawnedFences)
			{
				gameObject.SetActive(true);
			}
		}
		if (this.SpawnedRopes != null)
		{
			foreach (LineRenderer lineRenderer in this.SpawnedRopes)
			{
				lineRenderer.gameObject.SetActive(true);
			}
		}
	}

	public void RedrawRoute()
	{
		foreach (Transform transform in this.Waypoints)
		{
			this.AlignObjectByGround(transform.gameObject);
			transform.position += Vector3.up;
		}
		for (int i = 0; i < this.Waypoints.Count; i++)
		{
			Vector3 from = Vector3.zero;
			Vector3 to = Vector3.zero;
			if (i + 1 < this.Waypoints.Count && i - 1 >= 0)
			{
				from = this.Waypoints[i - 1].position - this.Waypoints[i].position;
				to = this.Waypoints[i + 1].position - this.Waypoints[i].position;
				if (Vector3.Angle(from, to) < 90f)
				{
					UnityEngine.Debug.LogError("Waypoint #" + i + " has acute angle. To avoid bugs please make it obtuse.");
				}
				this.Waypoints[i].rotation = Quaternion.LookRotation(from.normalized + to.normalized);
				if (Vector3.Dot(this.Waypoints[i].forward, this.Waypoints[i - 1].forward) < 0f)
				{
					this.Waypoints[i].localEulerAngles += new Vector3(0f, 180f, 0f);
				}
			}
			if (i == 0)
			{
				if (!this.Circuit)
				{
					this.Waypoints[i].LookAt(this.Waypoints[i + 1]);
					this.Waypoints[i].eulerAngles += new Vector3(0f, 90f, 0f);
				}
				else
				{
					from = this.Waypoints[1].position - this.Waypoints[0].position;
					to = this.Waypoints[this.Waypoints.Count - 1].position - this.Waypoints[0].position;
					this.Waypoints[i].rotation = Quaternion.LookRotation(from.normalized + to.normalized);
				}
			}
			if (i == this.Waypoints.Count - 1)
			{
				if (!this.Circuit)
				{
					this.Waypoints[i].LookAt(this.Waypoints[i - 1]);
					this.Waypoints[i].eulerAngles += new Vector3(0f, -90f, 0f);
				}
				else
				{
					from = this.Waypoints[0].position - this.Waypoints[i].position;
					to = this.Waypoints[i - 1].position - this.Waypoints[i].position;
					this.Waypoints[i].rotation = Quaternion.LookRotation(from.normalized + to.normalized);
				}
			}
		}
	}

	private void FindCheckpointsHolder()
	{
		if (this.CheckpointsHolder == null)
		{
			this.CheckpointsHolder = base.transform.Find("Checkpoints");
		}
		if (this.CheckpointsHolder == null)
		{
			this.CheckpointsHolder = new GameObject("Checkpoints")
			{
				transform = 
				{
					parent = base.transform
				}
			}.transform;
		}
	}

	public void CreateFenceForAllWaypoints()
	{
		float segmentWidth = this.RopeFencedSegments[0].SegmentWidth;
		GameObject fencePrefab = this.RopeFencedSegments[0].FencePrefab;
		this.RopeFencedSegments = new List<RopeFencedSegment>();
		int num = (!this.Circuit) ? 1 : 0;
		for (int i = 0; i < this.Waypoints.Count - num; i++)
		{
			RopeFencedSegment ropeFencedSegment = new RopeFencedSegment();
			ropeFencedSegment.SegmentWidth = segmentWidth;
			ropeFencedSegment.FencePrefab = fencePrefab;
			ropeFencedSegment.StartWaypoint = i;
			this.RopeFencedSegments.Add(ropeFencedSegment);
		}
		this.PlaceRopeFence();
	}

	public void PlaceStartPrefab()
	{
		if (this.StartPrefab == null)
		{
			return;
		}
		if (this.SpawnedStart != null)
		{
			UnityEngine.Object.DestroyImmediate(this.SpawnedStart);
		}
		this.FindCheckpointsHolder();
		this.SpawnedStart = UnityEngine.Object.Instantiate<GameObject>(this.StartPrefab);
		this.SpawnedStart.transform.position = this.Waypoints[0].position;
		Vector3 position = this.Waypoints[1].position;
		position.y = this.SpawnedStart.transform.position.y;
		this.SpawnedStart.transform.rotation = Quaternion.LookRotation(position - this.SpawnedStart.transform.position, Vector3.up);
		this.SpawnedStart.transform.parent = this.CheckpointsHolder;
	}

	public void PlaceFinishPrefab()
	{
		if (this.FinishPrefab == null)
		{
			return;
		}
		if (this.Circuit)
		{
			return;
		}
		if (this.SpawnedFinish != null)
		{
			UnityEngine.Object.DestroyImmediate(this.SpawnedFinish);
		}
		this.FindCheckpointsHolder();
		this.SpawnedFinish = UnityEngine.Object.Instantiate<GameObject>(this.FinishPrefab);
		this.SpawnedFinish.transform.position = this.Waypoints[this.Waypoints.Count - 1].position;
		Vector3 position = this.Waypoints[this.Waypoints.Count - 2].position;
		position.y = this.SpawnedFinish.transform.position.y;
		this.SpawnedFinish.transform.rotation = Quaternion.LookRotation(position - this.SpawnedFinish.transform.position, Vector3.up);
		this.SpawnedFinish.transform.eulerAngles += new Vector3(0f, 180f, 0f);
		this.SpawnedFinish.transform.parent = this.CheckpointsHolder;
	}

	public void PlaceEventMarkPrefab()
	{
		if (this.EventMarkPrefab == null)
		{
			return;
		}
		if (this.SpawnedEventMark != null)
		{
			UnityEngine.Object.DestroyImmediate(this.SpawnedEventMark);
		}
		this.SpawnedEventMark = UnityEngine.Object.Instantiate<GameObject>(this.EventMarkPrefab);
		this.SpawnedEventMark.transform.position = this.Waypoints[0].position;
		Vector3 position = this.Waypoints[1].position;
		position.y = this.SpawnedEventMark.transform.position.y;
		this.SpawnedEventMark.transform.rotation = Quaternion.LookRotation(position - this.SpawnedEventMark.transform.position);
		this.SpawnedEventMark.transform.parent = base.transform;
		this.AlignObjectByGround(this.SpawnedEventMark);
	}

	public void UndoDrawingTexture()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position, Vector3.down, out raycastHit))
		{
			if (raycastHit.collider.GetType() != typeof(TerrainCollider))
			{
				UnityEngine.Debug.LogError("Terrain is not found under route");
				return;
			}
			Terrain component = raycastHit.collider.GetComponent<Terrain>();
			TerrainData terrainData = component.terrainData;
			terrainData.SetAlphamaps(0, 0, this.undoAlphaData);
		}
	}

	public void ClearUndo()
	{
		this.undoAlphaData = null;
	}

	public void DrawTexture()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position, Vector3.down, out raycastHit))
		{
			if (raycastHit.collider.GetType() != typeof(TerrainCollider))
			{
				UnityEngine.Debug.LogError("Terrain is not found under route");
				return;
			}
			Terrain component = raycastHit.collider.GetComponent<Terrain>();
			TerrainData terrainData = component.terrainData;
			if (this.StampStep == 0f)
			{
				this.StampStep = 2f;
			}
			float num = 0f;
			float[] array = new float[this.Waypoints.Count];
			for (int i = 0; i < this.Waypoints.Count; i++)
			{
				array[i] = num;
				if (this.Waypoints.Count > i + 1)
				{
					num += Vector3.Distance(this.Waypoints[i].position, this.Waypoints[i + 1].position);
				}
			}
			if (this.Circuit)
			{
				num += Vector3.Distance(this.Waypoints[this.Waypoints.Count - 1].position, this.Waypoints[0].position);
			}
			SplatPrototype[] splatPrototypes = terrainData.splatPrototypes;
			int num2 = -1;
			for (int j = 0; j < splatPrototypes.Length; j++)
			{
				if (splatPrototypes[j].texture.Equals(this.RouteTexture))
				{
					num2 = j;
				}
			}
			if (this.undoAlphaData == null)
			{
				this.undoAlphaData = terrainData.GetAlphamaps(0, 0, terrainData.alphamapWidth, terrainData.alphamapHeight);
			}
			if (num2 < 0)
			{
				List<SplatPrototype> list = new List<SplatPrototype>();
				for (int k = 0; k < splatPrototypes.Length; k++)
				{
					list.Add(splatPrototypes[k]);
				}
				list.Add(new SplatPrototype
				{
					texture = this.RouteTexture
				});
				terrainData.splatPrototypes = list.ToArray();
				num2 = list.Count - 1;
			}
			float num3 = (!this.RandomStep) ? this.StampStep : UnityEngine.Random.Range(this.MinStep, this.MaxStep);
			for (float num4 = 0f; num4 <= num; num4 += num3)
			{
				if (this.RandomStep)
				{
					num3 = UnityEngine.Random.Range(this.MinStep, this.MaxStep);
				}
				int num5 = 0;
				int num6 = 0;
				bool flag = false;
				for (int l = 0; l < this.Waypoints.Count; l++)
				{
					if (array[l] > num4)
					{
						num5 = l - 1;
						num6 = l;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					num5 = this.Waypoints.Count - 1;
					num6 = 0;
				}
				float t;
				if (flag)
				{
					t = (num4 - array[num5]) / (array[num6] - array[num5]);
				}
				else
				{
					t = (num4 - array[num5]) / (num - array[num5]);
				}
				Vector3 a = Vector3.Lerp(this.Waypoints[num5].position, this.Waypoints[num6].position, t);
				Vector2 vector = default(Vector3);
				vector.x = (a - component.GetPosition()).x / terrainData.size.x * (float)terrainData.heightmapResolution;
				vector.y = (a - component.GetPosition()).z / terrainData.size.z * (float)terrainData.heightmapResolution;
				int x = Mathf.Clamp((int)vector.x - this.StampDiameter / 2, 0, terrainData.alphamapResolution - this.StampDiameter / 2 - 2);
				int y = Mathf.Clamp((int)vector.y - this.StampDiameter / 2, 0, terrainData.alphamapResolution - this.StampDiameter / 2 - 2);
				float[,,] alphamaps = terrainData.GetAlphamaps(x, y, this.StampDiameter, this.StampDiameter);
				float num7 = (!this.RandomOpacity) ? this.TargetOpacity : UnityEngine.Random.Range(0f, 1f);
				for (int m = 0; m < this.StampDiameter; m++)
				{
					for (int n = 0; n < this.StampDiameter; n++)
					{
						int x2 = (int)((float)m / (float)this.StampDiameter * (float)this.StampTexture.width);
						int y2 = (int)((float)n / (float)this.StampDiameter * (float)this.StampTexture.height);
						float num8 = this.StampTexture.GetPixel(x2, y2).r * num7;
						for (int num9 = 0; num9 < terrainData.splatPrototypes.Length; num9++)
						{
							if (terrainData.splatPrototypes[num9].texture == this.RouteTexture)
							{
								alphamaps[n, m, num9] += num8;
							}
							else
							{
								alphamaps[n, m, num9] -= num8;
							}
							alphamaps[n, m, num9] = Mathf.Clamp01(alphamaps[n, m, num9]);
						}
					}
				}
				terrainData.SetAlphamaps(x, y, alphamaps);
			}
		}
	}

	public void PlaceCheckpointsPrefabs()
	{
		if (this.SpawnedCheckpoints != null)
		{
			for (int i = 0; i < this.SpawnedCheckpoints.Count; i++)
			{
				if (this.SpawnedCheckpoints[i] != null)
				{
					UnityEngine.Object.DestroyImmediate(this.SpawnedCheckpoints[i]);
				}
			}
		}
		if (this.CheckpointsPrefabs == null)
		{
			return;
		}
		if (this.CheckpointsPrefabs.Length == 0)
		{
			return;
		}
		foreach (GameObject x in this.CheckpointsPrefabs)
		{
			if (x == null)
			{
				UnityEngine.Debug.LogError("One of waypoints prefabs is null");
				return;
			}
		}
		if (this.CheckpointsNumber == 0)
		{
			return;
		}
		float num = 0f;
		float[] array = new float[this.Waypoints.Count];
		for (int k = 0; k < this.Waypoints.Count; k++)
		{
			array[k] = num;
			if (this.Waypoints.Count > k + 1)
			{
				num += Vector3.Distance(this.Waypoints[k].position, this.Waypoints[k + 1].position);
			}
		}
		if (this.Circuit)
		{
			num += Vector3.Distance(this.Waypoints[this.Waypoints.Count - 1].position, this.Waypoints[0].position);
		}
		float num2 = num / (float)(this.CheckpointsNumber + 1);
		this.SpawnedCheckpoints = new List<GameObject>();
		this.FindCheckpointsHolder();
		for (float num3 = num2; num3 <= num - num2; num3 += num2)
		{
			int num4 = 0;
			int num5 = 0;
			bool flag = false;
			for (int l = 0; l < this.Waypoints.Count; l++)
			{
				if (array[l] > num3)
				{
					num4 = l - 1;
					num5 = l;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				num4 = this.Waypoints.Count - 1;
				num5 = 0;
			}
			float t;
			if (flag)
			{
				t = (num3 - array[num4]) / (array[num5] - array[num4]);
			}
			else
			{
				t = (num3 - array[num4]) / (num - array[num4]);
			}
			Vector3 position = Vector3.Lerp(this.Waypoints[num4].position, this.Waypoints[num5].position, t);
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.CheckpointsPrefabs[UnityEngine.Random.Range(0, this.CheckpointsPrefabs.Length)]);
			gameObject.transform.position = position;
			this.AlignObjectByGround(gameObject);
			gameObject.transform.parent = this.CheckpointsHolder.transform;
			this.SpawnedCheckpoints.Add(gameObject);
		}
		for (int m = 0; m < this.SpawnedCheckpoints.Count; m++)
		{
			Vector3 position2 = this.SpawnedCheckpoints[m].transform.position;
			if (m + 1 < this.SpawnedCheckpoints.Count)
			{
				Vector3 position3 = this.SpawnedCheckpoints[m + 1].transform.position;
				position3.y = position2.y;
				this.SpawnedCheckpoints[m].transform.rotation = Quaternion.LookRotation(position3 - position2, Vector3.up);
			}
			else if (this.SpawnedFinish != null)
			{
				Vector3 position4 = this.SpawnedFinish.transform.position;
				position4.y = position2.y;
				this.SpawnedCheckpoints[m].transform.rotation = Quaternion.LookRotation(position4 - position2, Vector3.up);
			}
			else
			{
				Vector3 position5 = this.SpawnedStart.transform.position;
				position5.y = position2.y;
				this.SpawnedCheckpoints[m].transform.rotation = Quaternion.LookRotation(position5 - position2, Vector3.up);
			}
		}
		foreach (GameObject gameObject2 in this.SpawnedCheckpoints)
		{
			for (int n = 0; n < gameObject2.transform.childCount; n++)
			{
				this.AlignObjectByGround(gameObject2.transform.GetChild(n).gameObject);
			}
		}
	}

	private void AlignObjectByGround(GameObject go)
	{
		Vector3 position = go.transform.position;
		this.HideShowAllCheckpoints(false);
		this.HideShowStartAndFinish(false);
		this.HideShowEventMark(false);
		go.SetActive(false);
		RaycastHit raycastHit;
		if (Physics.Raycast(position + Vector3.up * 10f, Vector3.down, out raycastHit))
		{
			position.y = raycastHit.point.y;
		}
		go.SetActive(true);
		this.HideShowAllCheckpoints(true);
		this.HideShowStartAndFinish(true);
		this.HideShowEventMark(true);
		MeshFilter meshFilter = go.GetComponent<MeshFilter>();
		if (meshFilter == null)
		{
			meshFilter = go.GetComponentInChildren<MeshFilter>();
		}
		if (meshFilter != null)
		{
			Mesh sharedMesh = meshFilter.sharedMesh;
			position.y -= sharedMesh.bounds.min.y * go.transform.lossyScale.y;
		}
		go.transform.position = position;
	}

	public RoutePayment GetRoutePayment(VehicleType vehicleType, int completionTime, int winchCount, int flipCount, float damageCount)
	{
		RoutePayment result = null;
		RouteGoal routeGoal = this.RouteGoals.Find((RouteGoal goal) => goal.VehicleType == vehicleType);
		if (routeGoal == null)
		{
			routeGoal = RouteGoal.Default(this.RouteRecord, this, vehicleType);
		}
		if (routeGoal != null)
		{
			result = routeGoal.GetPayment(completionTime, winchCount, flipCount, damageCount);
		}
		return result;
	}

	private DateTime DateTimeFromMinutes(int minutes)
	{
		TimeSpan t = TimeSpan.FromMinutes((double)minutes);
		DateTime d = new DateTime(2018, 1, 1, 0, 0, 0);
		return d + t;
	}

	private int GetCurrentMinutes()
	{
		DateTime d = new DateTime(2018, 1, 1, 0, 0, 0);
		return (int)(DateTime.Now - d).TotalMinutes;
	}

	public void SubmitTime(int time, bool fromError = false)
	{
		int @int = DataStore.GetInt(this.Manager.mapName + this.RouteName + this.vehicleDataManager.vehicleType.ToString());
		UnityEngine.Debug.Log("This time: " + time);
		UnityEngine.Debug.Log("Stored time: " + @int);
		if (@int != 0 && @int < time)
		{
			return;
		}
		DataStore.SetInt(this.Manager.mapName + this.RouteName + this.vehicleDataManager.vehicleType.ToString(), time);
		UnityEngine.Debug.Log("Submitting time");
		if (PlayFabClientAPI.IsClientLoggedIn())
		{
			UnityEngine.Debug.Log("Client was logged in");
			PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
			{
				Statistics = new List<StatisticUpdate>
				{
					new StatisticUpdate
					{
						StatisticName = this.Manager.mapName + this.RouteName + this.vehicleDataManager.vehicleType.ToString(),
						Value = -time
					}
				}
			}, delegate(UpdatePlayerStatisticsResult success)
			{
				UnityEngine.Debug.Log("Time submitted");
			}, delegate(PlayFabError error)
			{
				UnityEngine.Debug.Log("Statistic not found!");
				if (error.Error == PlayFabErrorCode.StatisticNotFound && !fromError)
				{
					UnityEngine.Debug.Log("Creating statistic.");
					PlayFabAdminAPI.CreatePlayerStatisticDefinition(new CreatePlayerStatisticDefinitionRequest
					{
						AggregationMethod = new StatisticAggregationMethod?(StatisticAggregationMethod.Max),
						StatisticName = this.Manager.mapName + this.RouteName + this.vehicleDataManager.vehicleType.ToString(),
						VersionChangeInterval = new StatisticResetIntervalOption?(StatisticResetIntervalOption.Never)
					}, delegate(CreatePlayerStatisticDefinitionResult result)
					{
						UnityEngine.Debug.Log("Created Statistic, submitting again.");
						this.SubmitTime(time, true);
					}, delegate(PlayFabError statsError)
					{
						UnityEngine.Debug.Log("Could not create statistic: " + statsError.GenerateErrorReport());
					}, null, null);
				}
				else
				{
					UnityEngine.Debug.Log("Error: " + error.GenerateErrorReport());
				}
			}, null, null);
		}
	}

	public void RefreshRecord()
	{
		long @long = DataStore.GetLong(this.Manager.mapName + this.RouteName + "Refreshed");
		if (!PlayFabClientAPI.IsClientLoggedIn())
		{
			this.GetLeaderboardError(new PlayFabError());
			return;
		}
		if (@long < DateTime.Now.Ticks - TimeSpan.FromMinutes((double)this.Manager.RefreshInterval).Ticks)
		{
			UnityEngine.Debug.Log("Need to refresh route record!");
			this.RecordLoaded = false;
			if (PlayFabClientAPI.IsClientLoggedIn())
			{
				UnityEngine.Debug.Log("Getting Leaderboard: " + this.Manager.mapName + this.RouteName + this.vehicleDataManager.vehicleType.ToString());
				GetLeaderboardRequest request = new GetLeaderboardRequest
				{
					StatisticName = this.Manager.mapName + this.RouteName + this.vehicleDataManager.vehicleType.ToString(),
					StartPosition = 0,
					MaxResultsCount = new int?(10)
				};
				PlayFabClientAPI.GetLeaderboard(request, new Action<GetLeaderboardResult>(this.GotLeaderboard), new Action<PlayFabError>(this.GetLeaderboardError), null, null);
			}
		}
		else
		{
			this.TrailblazerEligible = false;
		}
	}

	public string GetRecordKeeper()
	{
		return DataStore.GetString(this.Manager.mapName + this.RouteName + this.vehicleDataManager.vehicleType.ToString() + "RecordKeeper");
	}

	public void GotLeaderboard(GetLeaderboardResult result)
	{
		if (result != null && result.Leaderboard != null && result.Leaderboard.Count > 0 && result.Leaderboard[0].StatValue < 0)
		{
			this.RouteRecord = (long)(-(long)result.Leaderboard[0].StatValue);
			UnityEngine.Debug.Log("DisplayName:" + result.Leaderboard[0].DisplayName + "; PlayFabID: " + result.Leaderboard[0].PlayFabId);
			DataStore.SetLong(this.Manager.mapName + this.RouteName + this.vehicleDataManager.vehicleType.ToString(), this.RouteRecord);
			DataStore.SetLong(this.Manager.mapName + this.RouteName + this.vehicleDataManager.vehicleType.ToString() + "Refreshed", DateTime.Now.Ticks);
			DataStore.SetString(this.Manager.mapName + this.RouteName + this.vehicleDataManager.vehicleType.ToString() + "RecordKeeper", result.Leaderboard[0].DisplayName);
		}
		if (result.Leaderboard.Count == 0)
		{
			this.TrailblazerEligible = true;
		}
		this.RecordLoaded = true;
		CarUIControl.Instance.DisplayEventInfo();
	}

	public void GetLeaderboardError(PlayFabError error)
	{
		if (error.Error == PlayFabErrorCode.StatisticNotFound)
		{
			this.TrailblazerEligible = true;
		}
		this.RecordLoaded = true;
		CarUIControl.Instance.DisplayEventInfo();
	}

	public void PlaceRopeFence()
	{
		if (this.RopeFencedSegments == null)
		{
			return;
		}
		if (this.SpawnedFences != null)
		{
			for (int i = 0; i < this.SpawnedFences.Count; i++)
			{
				if (this.SpawnedFences[i] != null)
				{
					UnityEngine.Object.DestroyImmediate(this.SpawnedFences[i]);
				}
			}
		}
		if (this.SpawnedRopes != null)
		{
			for (int j = 0; j < this.SpawnedRopes.Count; j++)
			{
				if (this.SpawnedRopes[j] != null)
				{
					UnityEngine.Object.DestroyImmediate(this.SpawnedRopes[j].gameObject);
				}
			}
		}
		this.SpawnedFences = new List<GameObject>();
		this.SpawnedRopes = new List<LineRenderer>();
		this.FindCheckpointsHolder();
		List<Transform> list = new List<Transform>();
		for (int k = 0; k < this.Waypoints.Count; k++)
		{
			list.Add(this.Waypoints[k]);
		}
		foreach (RopeFencedSegment ropeFencedSegment in this.RopeFencedSegments)
		{
			if (ropeFencedSegment.StartWaypoint <= list.Count - 1)
			{
				if (!(ropeFencedSegment.FencePrefab == null))
				{
					List<Vector3> list2 = new List<Vector3>();
					for (int l = 0; l < 4; l++)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ropeFencedSegment.FencePrefab);
						int num = (l <= 1) ? 0 : 1;
						int num2 = ((float)l % 2f != 0f) ? 1 : -1;
						Transform transform = list[0];
						if (ropeFencedSegment.StartWaypoint + num < list.Count)
						{
							transform = list[ropeFencedSegment.StartWaypoint + num];
						}
						gameObject.transform.position = transform.position + transform.forward * ropeFencedSegment.SegmentWidth * Mathf.Sign((float)num2);
						gameObject.transform.parent = this.CheckpointsHolder;
						this.SpawnedFences.Add(gameObject);
						this.AlignObjectByGround(gameObject);
						list2.Add(gameObject.transform.position);
					}
					for (int m = 0; m < 2; m++)
					{
						LineRenderer lineRenderer = new GameObject("Rope")
						{
							transform = 
							{
								parent = this.CheckpointsHolder
							}
						}.AddComponent<LineRenderer>();
						lineRenderer.material = (Resources.Load("Materials/Rope", typeof(Material)) as Material);
						lineRenderer.useWorldSpace = true;
						lineRenderer.positionCount = 2;
						lineRenderer.textureMode = LineTextureMode.Tile;
						lineRenderer.widthMultiplier = 0.3f;
						lineRenderer.SetPosition(0, list2[m]);
						lineRenderer.SetPosition(1, list2[m + 2]);
						this.SpawnedRopes.Add(lineRenderer);
					}
				}
			}
		}
	}

	public void UpdateRoute()
	{
		this.RedrawRoute();
		this.PlaceStartPrefab();
		this.PlaceFinishPrefab();
		this.PlaceCheckpointsPrefabs();
		this.PlaceEventMarkPrefab();
		this.PlaceRopeFence();
	}

	[SerializeField]
	public List<Transform> Waypoints;

	[SerializeField]
	public Color RouteColor;

	[SerializeField]
	public bool ThickLine = true;

	[SerializeField]
	public Transform StartWP;

	[SerializeField]
	public Transform EndWP;

	[SerializeField]
	public bool DrawTextureOnTerrain;

	[SerializeField]
	public Texture2D RouteTexture;

	[SerializeField]
	public Texture2D StampTexture;

	[SerializeField]
	public bool RandomStep;

	[SerializeField]
	public float MinStep;

	[SerializeField]
	public float MaxStep;

	[SerializeField]
	public float StampStep;

	[SerializeField]
	public int StampDiameter;

	[SerializeField]
	public bool RandomOpacity;

	[SerializeField]
	public float TargetOpacity;

	[SerializeField]
	public float[,,] undoAlphaData;

	[SerializeField]
	public GameObject StartPrefab;

	[SerializeField]
	public GameObject FinishPrefab;

	[SerializeField]
	public GameObject EventMarkPrefab;

	[SerializeField]
	public GameObject[] CheckpointsPrefabs;

	[SerializeField]
	public long RouteRecord;

	[SerializeField]
	public bool RecordLoaded;

	[SerializeField]
	public List<RouteGoal> RouteGoals;

	[SerializeField]
	public int MinimumXP;

	[SerializeField]
	public VehicleType RecommendedVehicleType;

	[SerializeField]
	public Transform CheckpointsHolder;

	[SerializeField]
	public GameObject SpawnedStart;

	[SerializeField]
	public GameObject SpawnedFinish;

	[SerializeField]
	public GameObject SpawnedEventMark;

	[SerializeField]
	public List<GameObject> SpawnedCheckpoints;

	[SerializeField]
	public List<GameObject> SpawnedFences;

	[SerializeField]
	public List<LineRenderer> SpawnedRopes;

	[SerializeField]
	public List<GameObject> SpawnedBlocks;

	[SerializeField]
	public List<RopeFencedSegment> RopeFencedSegments;

	[SerializeField]
	public List<BlockFencedSegment> BlockFencedSegments;

	[SerializeField]
	public int CheckpointsNumber;

	[SerializeField]
	public bool Circuit;

	[SerializeField]
	public bool Completed;

	[SerializeField]
	public int LapsNumber;

	[SerializeField]
	public string RouteName;

	[SerializeField]
	public string EventDescription;

	[SerializeField]
	public string FinishText;

	[SerializeField]
	public int DelayTime;

	[SerializeField]
	public bool AlwaysShowCheckpoints;

	[SerializeField]
	public float DistanceBetweenBlocks;

	[SerializeField]
	public RouteManager Manager;

	public bool TrailblazerEligible;

	[Header("Multiplayer trail")]
	public bool MultiplayerTrail;

	public string MultiplayerName;

	public Transform Player1StartPos;

	public Transform Player2StartPos;

	[SerializeField]
	public LightTree lightTree;
}
