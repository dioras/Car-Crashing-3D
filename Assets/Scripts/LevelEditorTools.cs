using System;
using System.Collections.Generic;
using UnityEngine;

public static class LevelEditorTools
{
	public static Transform PropsParent
	{
		get
		{
			GameObject gameObject = GameObject.Find("PropsParent");
			if (gameObject == null)
			{
				gameObject = new GameObject("PropsParent");
			}
			return gameObject.transform;
		}
	}

	public static Transform ExtraObjectsParent
	{
		get
		{
			GameObject gameObject = GameObject.Find("ExtraObjects");
			if (gameObject == null)
			{
				gameObject = new GameObject("ExtraObjects");
			}
			return gameObject.transform;
		}
	}

	public static Transform RoutesParent
	{
		get
		{
			GameObject gameObject = GameObject.Find("Routes");
			if (gameObject == null)
			{
				gameObject = new GameObject("Routes");
			}
			return gameObject.transform;
		}
	}

	public static Transform CliffsParent
	{
		get
		{
			GameObject gameObject = GameObject.Find("CliffsParent");
			if (gameObject == null)
			{
				gameObject = new GameObject("CliffsParent");
			}
			return gameObject.transform;
		}
	}

	public static Transform MudStampsParent
	{
		get
		{
			GameObject gameObject = GameObject.Find("MudStamps");
			if (gameObject == null)
			{
				gameObject = new GameObject("MudStamps");
			}
			return gameObject.transform;
		}
	}

	public static LevelEditorResources editorResources
	{
		get
		{
			return Resources.Load<LevelEditorResources>("LevelEditorResources");
		}
	}

	public static void ResetTerrain(Terrain terrain)
	{
		TerrainData terrainData = terrain.terrainData;
		float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
		for (int i = 0; i < terrainData.heightmapResolution; i++)
		{
			for (int j = 0; j < terrainData.heightmapResolution; j++)
			{
				heights[i, j] = 0.5f;
			}
		}
		float[,,] alphamaps = terrainData.GetAlphamaps(0, 0, terrainData.alphamapHeight, terrainData.alphamapHeight);
		for (int k = 0; k < terrainData.alphamapHeight; k++)
		{
			for (int l = 0; l < terrainData.alphamapHeight; l++)
			{
				for (int m = 0; m < alphamaps.GetUpperBound(2) + 1; m++)
				{
					alphamaps[l, k, m] = (float)((m != 0) ? 0 : 1);
				}
			}
		}
		terrainData.SetAlphamaps(0, 0, alphamaps);
		terrainData.SetHeights(0, 0, heights);
		terrainData.treeInstances = new TreeInstance[0];
	}

	public static void GeneratePerlinTerrain(Terrain terrain, int seed, float terrainSize, bool flatCenter, float bumpsStrength)
	{
		UnityEngine.Random.InitState(seed);
		TerrainData terrainData = terrain.terrainData;
		terrainData.size = new Vector3(terrainSize, 500f, terrainSize);
		float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
		float num = 500f / terrainSize;
		float num2 = UnityEngine.Random.Range(0f, 10f);
		float num3 = UnityEngine.Random.Range(0f, 10f);
		for (int i = 0; i < terrainData.heightmapResolution; i++)
		{
			for (int j = 0; j < terrainData.heightmapResolution; j++)
			{
				float num4 = LevelEditorTools.DistanceFromCenter(terrain, i, j);
				num4 *= num4;
				if (!flatCenter)
				{
					num4 = 1f;
				}
				heights[i, j] = 0.5f + Mathf.PerlinNoise((float)i / (float)terrainData.heightmapResolution * 10f + num2, (float)j / (float)terrainData.heightmapResolution * 10f + num3) * bumpsStrength * num4 / num;
			}
		}
		terrainData.SetHeights(0, 0, heights);
	}

	public static void GenerateStampBasedTerrain(Terrain terrain, Texture2D[] stampTextures, int seed, float terrainSize, bool flatCenter, float bumpsStrength)
	{
		UnityEngine.Random.InitState(seed);
		int min = 3;
		int max = 10;
		float min2 = 0.1f;
		float max2 = 0.3f;
		float min3 = 100f;
		float max3 = 500f;
		TerrainData terrainData = terrain.terrainData;
		terrainData.size = new Vector3(terrainSize, 500f, terrainSize);
		float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
		int num = UnityEngine.Random.Range(min, max);
		for (int i = 0; i < num; i++)
		{
			Vector3 vector = terrain.transform.position + terrain.terrainData.size.x * UnityEngine.Random.Range(0f, 1f) * Vector3.right + terrain.terrainData.size.x * UnityEngine.Random.Range(0f, 1f) * Vector3.forward;
			vector.y = terrain.SampleHeight(vector);
			float stampRot = (float)UnityEngine.Random.Range(0, 360);
			float num2 = UnityEngine.Random.Range(min3, max3);
			float num3 = UnityEngine.Random.Range(min2, max2);
			Texture2D stampTexture = stampTextures[UnityEngine.Random.Range(0, stampTextures.Length)];
			float num4 = terrainSize / num2;
			float num5 = 500f / terrainSize;
			float[,] array = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
			for (int j = 0; j < terrain.terrainData.heightmapResolution; j++)
			{
				for (int k = 0; k < terrain.terrainData.heightmapResolution; k++)
				{
					Vector3 vector2 = terrain.GetPosition() + new Vector3((float)j, 0f, (float)k) / (float)terrainData.heightmapResolution * terrainData.size.x;
					vector2.y = terrain.SampleHeight(vector2);
					float stampStrength = LevelEditorTools.GetStampStrength(stampRot, vector, num2, vector2, stampTexture);
					if (stampStrength != 0f)
					{
						float num6 = LevelEditorTools.DistanceFromCenter(terrain, j, k);
						num6 *= num6;
						if (!flatCenter)
						{
							num6 = 1f;
						}
						float num7 = heights[k, j] + stampStrength * num3 / num4 / num5 * bumpsStrength * num6;
						float num8 = array[k, j] - heights[k, j];
						float num9 = num7 - heights[k, j];
						float num10 = num8 + num9;
						if (num8 > 0f && num9 > 0f)
						{
							num10 = Mathf.Max(num8, num9);
						}
						if (num8 < 0f && num9 < 0f)
						{
							num10 = Mathf.Min(num8, num9);
						}
						float num11 = heights[k, j] + num10;
						array[k, j] = num11;
					}
				}
			}
			array = LevelEditorTools.SmoothTerrain(array);
			terrain.terrainData.SetHeights(0, 0, array);
		}
	}

	public static float[,] SmoothTerrain(float[,] heightMap)
	{
		int upperBound = heightMap.GetUpperBound(0);
		int upperBound2 = heightMap.GetUpperBound(1);
		for (int i = 0; i < upperBound2; i++)
		{
			int num;
			int num2;
			int num3;
			if (i == 0)
			{
				num = 2;
				num2 = 0;
				num3 = 0;
			}
			else if (i == upperBound2 - 1)
			{
				num = 2;
				num2 = -1;
				num3 = 1;
			}
			else
			{
				num = 3;
				num2 = -1;
				num3 = 1;
			}
			for (int j = 0; j < upperBound; j++)
			{
				int num4;
				int num5;
				int num6;
				if (j == 0)
				{
					num4 = 2;
					num5 = 0;
					num6 = 0;
				}
				else if (j == upperBound - 1)
				{
					num4 = 2;
					num5 = -1;
					num6 = 1;
				}
				else
				{
					num4 = 3;
					num5 = -1;
					num6 = 1;
				}
				float num7 = 0f;
				int num8 = 0;
				for (int k = 0; k < num; k++)
				{
					for (int l = 0; l < num4; l++)
					{
						float num9 = heightMap[j + l + num5, i + k + num2];
						num7 += num9;
						num8++;
					}
				}
				float num10 = num7 / (float)num8;
				heightMap[j + num6 + num5, i + num3 + num2] = num10;
			}
		}
		return heightMap;
	}

	public static void PlaceTrees(Terrain terrain, Transform waterPlane, int seed, float terrainSize, int maxTreesCount, float treesDensity, bool treesByEdgesOnly)
	{
		UnityEngine.Random.InitState(seed);
		TerrainData terrainData = terrain.terrainData;
		terrainData.treeInstances = new TreeInstance[0];
		int num = 0;
		float num2 = terrainSize * terrainSize * 0.05f;
		if (num2 > (float)maxTreesCount)
		{
			num2 = (float)maxTreesCount;
		}
		num2 *= treesDensity;
		int num3 = 0;
		while ((float)num < num2 && num3 < 100000)
		{
			num3++;
			Vector3 vector = new Vector3(UnityEngine.Random.Range(0f, 1f), 1f, UnityEngine.Random.Range(0f, 1f));
			float num4 = LevelEditorTools.DistanceFromCenter(vector.x, vector.z);
			if (num4 < 0.4f)
			{
				num4 = 0f;
			}
			num4 *= num4;
			if (!treesByEdgesOnly)
			{
				num4 = 1f;
			}
			bool flag = UnityEngine.Random.Range(0f, 1f) < num4;
			if (waterPlane.gameObject.activeSelf)
			{
				Vector3 worldPosition = terrain.GetPosition() + terrainData.size.x * vector;
				worldPosition.y = terrain.SampleHeight(worldPosition);
				if (worldPosition.y < waterPlane.position.y)
				{
					flag = false;
				}
			}
			if (flag)
			{
				TreeInstance instance = default(TreeInstance);
				instance.prototypeIndex = UnityEngine.Random.Range(0, terrainData.treePrototypes.Length);
				instance.color = Color.white;
				instance.position = vector;
				instance.rotation = UnityEngine.Random.Range(0f, 360f) * (float)num * 0.0174532924f;
				instance.heightScale = UnityEngine.Random.Range(0.5f, 1.5f);
				instance.widthScale = instance.heightScale;
				terrain.AddTreeInstance(instance);
				num++;
			}
		}
	}

	public static int PlaceExtraObjects(Terrain terrain, Transform waterPlane, int seed, ExtraObjectReference[] usedExtraObjects, ExtraObjectArray[] extraObjectsDictionary, float terrainSize, int lastPlacedExtraObjectID)
	{
		UnityEngine.Random.InitState(seed);
		lastPlacedExtraObjectID = 0;
		LevelEditorResources editorResources = LevelEditorTools.editorResources;
		UnityEngine.Object.DestroyImmediate(LevelEditorTools.ExtraObjectsParent.gameObject);
		if (usedExtraObjects != null)
		{
			foreach (ExtraObjectReference extraObjectReference in usedExtraObjects)
			{
				if (extraObjectReference.arrayID != -1)
				{
					ExtraObjectArray extraObjectArray = extraObjectsDictionary[extraObjectReference.arrayID];
					List<Vector3> list = new List<Vector3>();
					float num = 500f / terrainSize;
					float num2 = num * num;
					float num3 = (float)extraObjectArray.baseObjectsCount / num;
					num3 *= extraObjectReference.density;
					int num4 = 0;
					int num5 = 0;
					while ((float)num4 < num3 && num5 < 1000)
					{
						num5++;
						Vector3 randomPointOnTerrain = LevelEditorTools.GetRandomPointOnTerrain(terrain);
						float num6 = LevelEditorTools.DistanceFromCenter(terrain, randomPointOnTerrain);
						if (num6 < 0.4f)
						{
							num6 = 0f;
						}
						num6 *= num6;
						if (!extraObjectReference.onlyByEdges)
						{
							num6 = 1f;
						}
						bool flag = UnityEngine.Random.Range(0f, 1f) < num6;
						foreach (Vector3 a in list)
						{
							if (Vector3.Distance(a, randomPointOnTerrain) < extraObjectArray.minDistanceInterval)
							{
								flag = false;
							}
						}
						if (waterPlane.gameObject.activeSelf && randomPointOnTerrain.y < waterPlane.position.y)
						{
							flag = false;
						}
						if (flag)
						{
							int num7 = UnityEngine.Random.Range(0, extraObjectArray.prefabs.Length);
							GameObject gameObject = extraObjectArray.prefabs[num7];
							Quaternion rotation = extraObjectArray.prefabs[num7].transform.rotation;
							GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject, randomPointOnTerrain, rotation, LevelEditorTools.ExtraObjectsParent);
							gameObject2.AddComponent<ExtraObject>().ID = lastPlacedExtraObjectID;
							lastPlacedExtraObjectID++;
							Prop component = gameObject2.GetComponent<Prop>();
							int propID = -1;
							for (int j = 0; j < editorResources.propsDictionary.Length; j++)
							{
								if (editorResources.propsDictionary[j].gameObject.Equals(gameObject))
								{
									propID = j;
									break;
								}
							}
							component.propID = propID;
							PlacementSettings component2 = gameObject2.GetComponent<PlacementSettings>();
							if (component2 != null)
							{
								component2.ApplyRandomRotation();
							}
							component.Highlight(false);
							num4++;
							list.Add(randomPointOnTerrain);
						}
					}
				}
			}
		}
		LevelEditorTools.CorrectExtraObjectsTransforms(terrain, seed);
		return lastPlacedExtraObjectID;
	}

	public static void RemoveIgnoredExtraObjects(List<int> ignoredExtraObjectsList)
	{
		GameObject[] allChildren = LevelEditorTools.GetAllChildren(LevelEditorTools.ExtraObjectsParent.gameObject);
		for (int i = 0; i < allChildren.Length; i++)
		{
			if (ignoredExtraObjectsList.Contains(i))
			{
				UnityEngine.Object.Destroy(allChildren[i].gameObject);
			}
		}
	}

	public static void CorrectExtraObjectsTransforms(Terrain terrain, int seed)
	{
		UnityEngine.Random.InitState(seed);
		GameObject[] allChildren = LevelEditorTools.GetAllChildren(LevelEditorTools.ExtraObjectsParent.gameObject);
		for (int i = 0; i < allChildren.Length; i++)
		{
			PlacementSettings component = allChildren[i].GetComponent<PlacementSettings>();
			if (component != null)
			{
				component.Align();
			}
			else
			{
				Vector3 position = allChildren[i].transform.position;
				position.y = terrain.SampleHeight(position);
				allChildren[i].transform.position = position;
			}
		}
	}

	public static void CorrectCliffsPositions(Terrain terrain)
	{
		GameObject[] allChildren = LevelEditorTools.GetAllChildren(LevelEditorTools.CliffsParent.gameObject);
		for (int i = 0; i < allChildren.Length; i++)
		{
			PlacementSettings component = allChildren[i].GetComponent<PlacementSettings>();
			if (component != null)
			{
				component.Align();
			}
			else
			{
				Vector3 position = allChildren[i].transform.position;
				position.y = terrain.SampleHeight(position);
				allChildren[i].transform.position = position;
			}
		}
	}

	public static void PlaceCliffs(Terrain terrain, GameObject[] cliffPrefabs, float terrainSize, int seed, int baseCliffsCount, float minHillAngle)
	{
		if (cliffPrefabs.Length == 0)
		{
			return;
		}
		UnityEngine.Random.InitState(seed);
		float num = 500f / terrainSize;
		int num2 = (int)((float)baseCliffsCount / num);
		int num3 = 0;
		int num4 = 0;
		UnityEngine.Object.DestroyImmediate(LevelEditorTools.CliffsParent.gameObject);
		List<Vector3> list = new List<Vector3>();
		while (num3 < num2 && num4 < 10000)
		{
			num4++;
			Vector3 randomPointOnTerrain = LevelEditorTools.GetRandomPointOnTerrain(terrain);
			Vector3 normalAtPosition = LevelEditorTools.GetNormalAtPosition(terrain, randomPointOnTerrain);
			float num5 = Vector3.Angle(Vector3.up, normalAtPosition);
			bool flag = num5 > minHillAngle;
			foreach (Vector3 a in list)
			{
				if (Vector3.Distance(a, randomPointOnTerrain) < 20f)
				{
					flag = false;
				}
			}
			if (flag)
			{
				int num6 = UnityEngine.Random.Range(0, cliffPrefabs.Length);
				GameObject gameObject = cliffPrefabs[num6];
				Quaternion quaternion = gameObject.transform.rotation;
				quaternion *= Quaternion.Euler(0f, (float)UnityEngine.Random.Range(0, 360), 0f);
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject, randomPointOnTerrain, quaternion, LevelEditorTools.CliffsParent);
				PlacementSettings component = gameObject2.GetComponent<PlacementSettings>();
				if (component != null)
				{
					component.ApplyRandomRotation();
					component.Align();
				}
				num3++;
				list.Add(randomPointOnTerrain);
			}
		}
	}

	public static void ApplyWater(Terrain terrain, Transform waterPlane, bool waterEnabled, float terrainSize, float waterHeight, bool frozenWater, Material frozenWaterMaterial, Material baseWaterMaterial)
	{
		TerrainData terrainData = terrain.terrainData;
		waterPlane.gameObject.SetActive(waterEnabled);
		if (waterEnabled)
		{
			waterPlane.transform.localScale = Vector3.one * terrainSize / 10f;
			Vector3 position = terrain.GetPosition() + terrainData.size / 2f;
			position.y = terrain.GetPosition().y + waterHeight * terrainData.size.y;
			waterPlane.transform.position = position;
			waterPlane.GetComponent<MeshRenderer>().material = ((!frozenWater) ? baseWaterMaterial : frozenWaterMaterial);
		}
	}

	public static void PaintBaseTexture(Terrain terrain, int mainTextureID)
	{
		TerrainData terrainData = terrain.terrainData;
		float[,,] alphamaps = terrainData.GetAlphamaps(0, 0, terrainData.alphamapHeight, terrainData.alphamapHeight);
		for (int i = 0; i < terrainData.alphamapHeight; i++)
		{
			for (int j = 0; j < terrainData.alphamapHeight; j++)
			{
				for (int k = 0; k < alphamaps.GetUpperBound(2) + 1; k++)
				{
					alphamaps[j, i, k] = (float)((k != mainTextureID) ? 0 : 1);
				}
			}
		}
		terrainData.SetAlphamaps(0, 0, alphamaps);
	}

	public static void PaintRockAndWaterTextures(Terrain terrain, Transform waterPlane, bool waterEnabled, bool frozenWater, int underwaterTextureID, int rockTextureID, float minRockAngle, float maxRockAngle, int mainTextureID)
	{
		TerrainData terrainData = terrain.terrainData;
		float[,,] alphamaps = terrainData.GetAlphamaps(0, 0, terrainData.alphamapHeight, terrainData.alphamapHeight);
		for (int i = 0; i < terrainData.alphamapHeight; i++)
		{
			for (int j = 0; j < terrainData.alphamapHeight; j++)
			{
				Vector3 vector = terrain.GetPosition();
				vector += new Vector3((float)i / (float)terrainData.alphamapWidth * terrainData.size.x, 0f, (float)j / (float)terrainData.alphamapWidth * terrainData.size.z);
				vector.y = terrain.SampleHeight(vector) - 0.3f;
				float num = 0f;
				if (vector.y < waterPlane.position.y)
				{
					num = 0.9f;
				}
				if (!waterEnabled || frozenWater)
				{
					num = 0f;
				}
				alphamaps[j, i, underwaterTextureID] = num;
				float num2 = 0f;
				for (int k = 0; k < alphamaps.GetUpperBound(2) + 1; k++)
				{
					if (k != underwaterTextureID)
					{
						num2 += alphamaps[j, i, k];
					}
				}
				for (int l = 0; l < alphamaps.GetUpperBound(2) + 1; l++)
				{
					if (l != underwaterTextureID)
					{
						alphamaps[j, i, l] /= num2;
						alphamaps[j, i, l] *= 1f - num;
					}
				}
				float x = (float)i / (float)terrainData.alphamapHeight;
				float y = (float)j / (float)terrainData.alphamapHeight;
				Vector3 interpolatedNormal = terrainData.GetInterpolatedNormal(x, y);
				float value = Vector3.Angle(Vector3.up, interpolatedNormal);
				float num3 = Mathf.InverseLerp(minRockAngle, maxRockAngle, value);
				alphamaps[j, i, rockTextureID] = num3;
				num2 = 0f;
				for (int m = 0; m < alphamaps.GetUpperBound(2) + 1; m++)
				{
					if (m != rockTextureID)
					{
						num2 += alphamaps[j, i, m];
					}
				}
				for (int n = 0; n < alphamaps.GetUpperBound(2) + 1; n++)
				{
					if (n != rockTextureID)
					{
						alphamaps[j, i, n] /= num2;
						alphamaps[j, i, n] *= 1f - num3;
					}
				}
				if (num2 == 0f)
				{
					alphamaps[j, i, mainTextureID] = 1f - num3;
				}
			}
		}
		terrainData.SetAlphamaps(0, 0, alphamaps);
	}

	public static void ApplyHeightStamp(Terrain terrain, TerrainStamp stamp, float[,] defHeights, Texture2D[] stampTextures)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		TerrainData terrainData = terrain.terrainData;
		float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
		for (int i = 0; i < terrain.terrainData.heightmapResolution; i++)
		{
			for (int j = 0; j < terrain.terrainData.heightmapResolution; j++)
			{
				Vector3 vector = terrain.GetPosition() + new Vector3((float)i, 0f, (float)j) / (float)terrainData.heightmapResolution * terrainData.size.x;
				vector.y = terrain.SampleHeight(vector);
				float num = LevelEditorTools.GetStampStrength(stamp.stampRotation, stamp.stampPosition, stamp.stampSize, vector, stampTextures[stamp.stampTextureID]);
				if (num != 0f)
				{
					if (stamp.stampAction == ModAction.LandscapeLowering)
					{
						num = -num;
					}
					float num2 = defHeights[j, i] + num * stamp.stampStrength * 0.05f;
					float num3 = heights[j, i] - defHeights[j, i];
					float num4 = num2 - defHeights[j, i];
					float num5 = num3 + num4;
					if (num3 > 0f && num4 > 0f)
					{
						num5 = Mathf.Max(num3, num4);
					}
					if (num3 < 0f && num4 < 0f)
					{
						num5 = Mathf.Min(num3, num4);
					}
					float num6 = defHeights[j, i] + num5;
					heights[j, i] = num6;
				}
			}
		}
		terrain.terrainData.SetHeights(0, 0, heights);
		float realtimeSinceStartup2 = Time.realtimeSinceStartup;
		float num7 = realtimeSinceStartup2 - realtimeSinceStartup;
		UnityEngine.Debug.Log(num7);
	}

	public static void ApplyAllHeightStamps(Terrain terrain, List<TerrainStamp> stamps, float[,] defHeights, Texture2D[] stampTextures)
	{
		TerrainData terrainData = terrain.terrainData;
		float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
		for (int i = 0; i < terrain.terrainData.heightmapResolution; i++)
		{
			for (int j = 0; j < terrain.terrainData.heightmapResolution; j++)
			{
				Vector3 vector = terrain.GetPosition() + new Vector3((float)i, 0f, (float)j) / (float)terrainData.heightmapResolution * terrainData.size.x;
				vector.y = terrain.SampleHeight(vector);
				float allHeightStampsWeight = LevelEditorTools.GetAllHeightStampsWeight(stamps, vector, stampTextures);
				if (allHeightStampsWeight != 0f)
				{
					float num = defHeights[j, i] + allHeightStampsWeight * 0.05f;
					float num2 = heights[j, i] - defHeights[j, i];
					float num3 = num - defHeights[j, i];
					float num4 = num2 + num3;
					if (num2 > 0f && num3 > 0f)
					{
						num4 = Mathf.Max(num2, num3);
					}
					if (num2 < 0f && num3 < 0f)
					{
						num4 = Mathf.Min(num2, num3);
					}
					float num5 = defHeights[j, i] + num4;
					heights[j, i] = num5;
				}
			}
		}
		terrain.terrainData.SetHeights(0, 0, heights);
	}

	public static float GetAllHeightStampsWeight(List<TerrainStamp> stamps, Vector3 worldPos, Texture2D[] stampTextures)
	{
		float num = 0f;
		foreach (TerrainStamp terrainStamp in stamps)
		{
			Vector3 vector = Quaternion.Euler(0f, terrainStamp.stampRotation, 0f) * Vector3.forward;
			Vector3 planeNormal = Vector3.Cross(Vector3.up, vector);
			Vector3 planePoint = new Vector3(terrainStamp.stampPosition.x, 0f, terrainStamp.stampPosition.y);
			float num2 = LevelEditorTools.SignedDistancePlanePoint(vector, planePoint, worldPos);
			float num3 = LevelEditorTools.SignedDistancePlanePoint(planeNormal, planePoint, worldPos);
			if (Mathf.Abs(num2) <= terrainStamp.stampSize && Mathf.Abs(num3) <= terrainStamp.stampSize)
			{
				Vector2 a = new Vector2(num3, num2) / terrainStamp.stampSize / 2f;
				a += new Vector2(0.5f, 0.5f);
				Vector2 vector2 = a * (float)stampTextures[terrainStamp.stampTextureID].width;
				Color pixel = stampTextures[terrainStamp.stampTextureID].GetPixel((int)vector2.x, (int)vector2.y);
				float num4 = 1f;
				if (terrainStamp.stampAction == ModAction.LandscapeLowering)
				{
					num4 = -1f;
				}
				float num5 = pixel.r * terrainStamp.stampStrength * num4;
				if (num == 0f)
				{
					num = num5;
				}
				if (num > 0f && num5 > 0f)
				{
					num = Mathf.Max(num, num5);
				}
				else if (num < 0f && num5 < 0f)
				{
					num = Mathf.Min(num, num5);
				}
				else
				{
					num += num5;
				}
			}
		}
		return Mathf.Clamp(num, -1f, 1f);
	}

	public static void ApplySmoothStamp(Terrain terrain, TerrainStamp stamp, Texture2D[] stampTextures)
	{
		TerrainData terrainData = terrain.terrainData;
		float num = 0f;
		int num2 = 0;
		float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
		for (int i = 0; i < terrainData.heightmapResolution; i++)
		{
			for (int j = 0; j < terrainData.heightmapResolution; j++)
			{
				Vector3 vector = terrain.GetPosition() + new Vector3((float)i, 0f, (float)j) / (float)terrainData.heightmapResolution * terrainData.size.x;
				vector.y = terrain.SampleHeight(vector);
				float stampStrength = LevelEditorTools.GetStampStrength(stamp.stampRotation, stamp.stampPosition, stamp.stampSize, vector, stampTextures[stamp.stampTextureID]);
				if (stampStrength != 0f)
				{
					num += heights[j, i];
					num2++;
				}
			}
		}
		num /= (float)num2;
		for (int k = 0; k < terrain.terrainData.heightmapResolution; k++)
		{
			for (int l = 0; l < terrain.terrainData.heightmapResolution; l++)
			{
				Vector3 vector2 = terrain.GetPosition() + new Vector3((float)k, 0f, (float)l) / (float)terrainData.heightmapResolution * terrainData.size.x;
				vector2.y = terrain.SampleHeight(vector2);
				float stampStrength2 = LevelEditorTools.GetStampStrength(stamp.stampRotation, stamp.stampPosition, stamp.stampSize, vector2, stampTextures[stamp.stampTextureID]);
				if (stampStrength2 != 0f)
				{
					heights[l, k] = Mathf.Lerp(heights[l, k], num, stamp.stampStrength * stampStrength2);
				}
			}
		}
		terrain.terrainData.SetHeights(0, 0, heights);
	}

	public static void ApplySmoothStampFast(Terrain terrain, ref float[,] heights, TerrainStamp stamp, int stampTexWidth, Color[] stampColors)
	{
		TerrainData terrainData = terrain.terrainData;
		Vector3 vector = Vector3.forward;
		vector = Quaternion.Euler(0f, stamp.stampRotation, 0f) * vector;
		Vector3 right = Vector3.Cross(Vector3.up, vector);
		Vector3 stampWorldPos = new Vector3(stamp.stampPosition.x, 0f, stamp.stampPosition.y);
		float num = 0f;
		int num2 = 0;
		for (int i = 0; i < terrainData.heightmapResolution; i++)
		{
			for (int j = 0; j < terrainData.heightmapResolution; j++)
			{
				Vector3 worldPos = terrain.GetPosition() + new Vector3((float)i, 0f, (float)j) / (float)terrainData.heightmapResolution * terrainData.size.x;
				float stampStrengthFast = LevelEditorTools.GetStampStrengthFast(vector, right, stampWorldPos, stamp.stampSize, worldPos, stampTexWidth, stampColors);
				if (stampStrengthFast != 0f)
				{
					num += heights[j, i];
					num2++;
				}
			}
		}
		num /= (float)num2;
		for (int k = 0; k < terrain.terrainData.heightmapResolution; k++)
		{
			for (int l = 0; l < terrain.terrainData.heightmapResolution; l++)
			{
				Vector3 worldPos2 = terrain.GetPosition() + new Vector3((float)k, 0f, (float)l) / (float)terrainData.heightmapResolution * terrainData.size.x;
				float stampStrengthFast2 = LevelEditorTools.GetStampStrengthFast(vector, right, stampWorldPos, stamp.stampSize, worldPos2, stampTexWidth, stampColors);
				if (stampStrengthFast2 != 0f)
				{
					heights[l, k] = Mathf.Lerp(heights[l, k], num, stamp.stampStrength * stampStrengthFast2);
				}
			}
		}
	}

	public static void ApplyMudStamps(List<MudStamp> mudStamps, Terrain terrain)
	{
		foreach (MudStamp mudStamp in mudStamps)
		{
			Texture2D stampTex = LevelEditorTools.editorResources.mudStampTextures[mudStamp.stampTextureID];
			Vector2 v = new Vector2(mudStamp.stampPosition.x, mudStamp.stampPosition.z);
			LevelEditorTools.ApplyPaintStamp(terrain, stampTex, mudStamp.stampRotation, v, mudStamp.stampSize, LevelEditorTools.editorResources.mudTextureID);
		}
	}

	public static void ToggleMudIndicators(bool enable)
	{
		foreach (MeshRenderer meshRenderer in LevelEditorTools.MudStampsParent.GetComponentsInChildren<MeshRenderer>(true))
		{
			meshRenderer.gameObject.SetActive(enable);
		}
	}

	public static GameObject CreateMudIndicator(MudStamp stamp, Terrain terrain)
	{
		GameObject mudIndicatorPrefab = LevelEditorTools.editorResources.mudIndicatorPrefab;
		Vector3 stampPosition = stamp.stampPosition;
		stampPosition.y = terrain.SampleHeight(stampPosition);
		Quaternion rotation = Quaternion.Euler(0f, stamp.stampRotation, 0f);
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(mudIndicatorPrefab, stampPosition, rotation, LevelEditorTools.MudStampsParent);
		Material material = gameObject.GetComponent<MeshRenderer>().material;
		material.SetTexture("_MainTex", LevelEditorTools.editorResources.mudStampTextures[stamp.stampTextureID]);
		gameObject.transform.localScale = Vector3.one * stamp.stampSize / 5f;
		return gameObject;
	}

	public static void ApplyPaintStamp(Terrain terrain, Texture2D stampTex, float stampRotation, Vector3 stampPosition, float stampSize, int paintTextureID)
	{
		TerrainData terrainData = terrain.terrainData;
		float[,,] alphamaps = terrainData.GetAlphamaps(0, 0, terrainData.alphamapWidth, terrainData.alphamapWidth);
		for (int i = 0; i < terrainData.alphamapWidth; i++)
		{
			for (int j = 0; j < terrainData.alphamapWidth; j++)
			{
				Vector3 vector = terrain.GetPosition() + new Vector3((float)i, 0f, (float)j) / (float)terrainData.alphamapWidth * terrainData.size.x;
				vector.y = terrain.SampleHeight(vector);
				float stampStrength = LevelEditorTools.GetStampStrength(stampRotation, stampPosition, stampSize, vector, stampTex);
				if (stampStrength != 0f)
				{
					alphamaps[j, i, paintTextureID] += stampStrength;
					for (int k = 0; k < alphamaps.GetUpperBound(2) + 1; k++)
					{
						alphamaps[j, i, k] -= ((k != paintTextureID) ? stampStrength : 0f);
						alphamaps[j, i, k] = Mathf.Clamp01(alphamaps[j, i, k]);
					}
					float num = 0f;
					for (int l = 0; l < alphamaps.GetUpperBound(2) + 1; l++)
					{
						num += alphamaps[j, i, l];
					}
					for (int m = 0; m < alphamaps.GetUpperBound(2) + 1; m++)
					{
						alphamaps[j, i, m] /= num;
					}
				}
			}
		}
		terrainData.SetAlphamaps(0, 0, alphamaps);
	}

	public static void ApplyPaintStampFast(TerrainData terData, Vector3 terrainPos, ref float[,,] splatMap, Color[] stampColors, int stampWidth, float stampRotation, Vector3 stampPosition, float stampSize, int paintTextureID)
	{
		Vector3 vector = Vector3.forward;
		vector = Quaternion.Euler(0f, stampRotation, 0f) * vector;
		Vector3 right = Vector3.Cross(Vector3.up, vector);
		Vector3 stampWorldPos = new Vector3(stampPosition.x, 0f, stampPosition.y);
		for (int i = 0; i < terData.alphamapWidth; i++)
		{
			for (int j = 0; j < terData.alphamapWidth; j++)
			{
				Vector3 worldPos = terrainPos + new Vector3((float)i, 0f, (float)j) / (float)terData.alphamapWidth * terData.size.x;
				float stampStrengthFast = LevelEditorTools.GetStampStrengthFast(vector, right, stampWorldPos, stampSize, worldPos, stampWidth, stampColors);
				if (stampStrengthFast != 0f)
				{
					splatMap[j, i, paintTextureID] += stampStrengthFast;
					for (int k = 0; k < splatMap.GetUpperBound(2) + 1; k++)
					{
						splatMap[j, i, k] -= ((k != paintTextureID) ? stampStrengthFast : 0f);
						splatMap[j, i, k] = Mathf.Clamp01(splatMap[j, i, k]);
					}
					float num = 0f;
					for (int l = 0; l < splatMap.GetUpperBound(2) + 1; l++)
					{
						num += splatMap[j, i, l];
					}
					for (int m = 0; m < splatMap.GetUpperBound(2) + 1; m++)
					{
						splatMap[j, i, m] /= num;
					}
				}
			}
		}
	}

	public static void ApplyRemoveExtraObjectsStamp(Terrain terrain, TerrainStamp stamp, int seed, Texture2D[] stampTextures, ref List<TreeInstance> removedTrees, ref List<GameObject> removedExtraObjects, bool previewOnly)
	{
		TerrainData terrainData = terrain.terrainData;
		UnityEngine.Random.InitState(seed);
		removedTrees.Clear();
		removedExtraObjects.Clear();
		List<TreeInstance> list = new List<TreeInstance>(terrainData.treeInstances);
		int count = list.Count;
		for (int i = 0; i < list.Count; i++)
		{
			Vector3 position = list[i].position;
			Vector3 worldPos = terrain.GetPosition() + position * terrainData.size.x;
			float stampStrength = LevelEditorTools.GetStampStrength(stamp.stampRotation, stamp.stampPosition, stamp.stampSize, worldPos, stampTextures[stamp.stampTextureID]);
			if (stampStrength != 0f)
			{
				if (UnityEngine.Random.Range(0f, 1f) < stampStrength)
				{
					if (previewOnly)
					{
						removedTrees.Add(list[i]);
					}
					list.RemoveAt(i);
					i--;
				}
			}
		}
		terrainData.treeInstances = list.ToArray();
		GameObject[] allChildren = LevelEditorTools.GetAllChildren(LevelEditorTools.ExtraObjectsParent.gameObject);
		for (int j = 0; j < allChildren.Length; j++)
		{
			float stampStrength2 = LevelEditorTools.GetStampStrength(stamp.stampRotation, stamp.stampPosition, stamp.stampSize, allChildren[j].transform.position, stampTextures[stamp.stampTextureID]);
			if (stampStrength2 != 0f)
			{
				if (UnityEngine.Random.Range(0f, 1f) < stampStrength2)
				{
					if (!previewOnly)
					{
						UnityEngine.Object.Destroy(allChildren[j]);
					}
					else
					{
						removedExtraObjects.Add(allChildren[j]);
						allChildren[j].SetActive(false);
					}
				}
			}
		}
	}

	public static int ApplyAddExtraObjectsStamp(Terrain terrain, Transform waterPlane, TerrainStamp stamp, Texture2D[] stampTextures, int seed, ExtraObjectArray[] extraObjectsDictionary, float terrainSize, bool waterEnabled, ref List<TreeInstance> addedTrees, ref List<GameObject> addedExtraObjects, int lastPlacedExtraObjectID)
	{
		TerrainData terrainData = terrain.terrainData;
		UnityEngine.Random.InitState(seed);
		addedTrees.Clear();
		addedExtraObjects.Clear();
		int extraInt = stamp.extraInt;
		if (extraInt == -1)
		{
			int num = (int)(stamp.stampSize / 2f * stamp.stampStrength);
			int num2 = 0;
			int num3 = 0;
			while (num3 < num && num2 < 1000)
			{
				num2++;
				Vector3 randomPointWithinStamp = LevelEditorTools.GetRandomPointWithinStamp(terrain, stamp);
				Vector3 position = (randomPointWithinStamp - terrain.GetPosition()) / terrainData.size.x;
				float stampStrength = LevelEditorTools.GetStampStrength(stamp.stampRotation, stamp.stampPosition, stamp.stampSize, randomPointWithinStamp, stampTextures[stamp.stampTextureID]);
				if (stampStrength != 0f)
				{
					bool flag = UnityEngine.Random.Range(0f, 1f) < stampStrength;
					if (waterPlane.gameObject.activeSelf && randomPointWithinStamp.y < waterPlane.position.y)
					{
						flag = false;
					}
					if (flag)
					{
						TreeInstance treeInstance = default(TreeInstance);
						treeInstance.prototypeIndex = UnityEngine.Random.Range(0, terrainData.treePrototypes.Length);
						treeInstance.color = Color.white;
						treeInstance.position = position;
						treeInstance.rotation = UnityEngine.Random.Range(0f, 360f) * (float)num3 * 0.0174532924f;
						treeInstance.heightScale = UnityEngine.Random.Range(0.5f, 1.5f);
						treeInstance.widthScale = treeInstance.heightScale;
						terrain.AddTreeInstance(treeInstance);
						num3++;
						addedTrees.Add(treeInstance);
					}
				}
			}
		}
		else
		{
			ExtraObjectArray extraObjectArray = extraObjectsDictionary[extraInt];
			float num4 = terrainSize / stamp.stampSize;
			int num5 = (int)((float)extraObjectArray.baseObjectsCount / num4 * stamp.stampStrength);
			int num6 = 0;
			int num7 = 0;
			List<Vector3> list = new List<Vector3>();
			LevelEditorResources editorResources = LevelEditorTools.editorResources;
			while (num7 < num5 && num6 < 1000)
			{
				num6++;
				Vector3 randomPointWithinStamp2 = LevelEditorTools.GetRandomPointWithinStamp(terrain, stamp);
				float stampStrength2 = LevelEditorTools.GetStampStrength(stamp.stampRotation, stamp.stampPosition, stamp.stampSize, randomPointWithinStamp2, stampTextures[stamp.stampTextureID]);
				bool flag2 = UnityEngine.Random.Range(0f, 1f) < stampStrength2;
				foreach (Vector3 a in list)
				{
					if (Vector3.Distance(a, randomPointWithinStamp2) < extraObjectArray.minDistanceInterval)
					{
						flag2 = false;
					}
				}
				if (waterEnabled && randomPointWithinStamp2.y < waterPlane.position.y)
				{
					flag2 = false;
				}
				if (flag2)
				{
					int num8 = UnityEngine.Random.Range(0, extraObjectArray.prefabs.Length);
					GameObject obj = extraObjectArray.prefabs[num8];
					Quaternion rotation = extraObjectArray.prefabs[num8].transform.rotation;
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(extraObjectArray.prefabs[num8], randomPointWithinStamp2, rotation, LevelEditorTools.ExtraObjectsParent.transform);
					gameObject.AddComponent<ExtraObject>().ID = lastPlacedExtraObjectID;
					lastPlacedExtraObjectID++;
					int propID = -1;
					for (int i = 0; i < editorResources.propsDictionary.Length; i++)
					{
						if (editorResources.propsDictionary[i].gameObject.Equals(obj))
						{
							propID = i;
							break;
						}
					}
					gameObject.GetComponent<Prop>().propID = propID;
					PlacementSettings component = gameObject.GetComponent<PlacementSettings>();
					if (component != null)
					{
						component.ApplyRandomRotation();
					}
					Prop component2 = gameObject.GetComponent<Prop>();
					if (component2 != null)
					{
						component2.Highlight(false);
					}
					num7++;
					list.Add(randomPointWithinStamp2);
					addedExtraObjects.Add(gameObject);
				}
			}
			LevelEditorTools.CorrectExtraObjectsTransforms(terrain, seed);
		}
		return lastPlacedExtraObjectID;
	}

	public static float GetStampStrength(float stampRot, Vector3 stampPos, float stampSize, Vector3 worldPos, Texture2D stampTexture)
	{
		Vector3 vector = Vector3.forward;
		vector = Quaternion.Euler(0f, stampRot, 0f) * vector;
		Vector3 planeNormal = Vector3.Cross(Vector3.up, vector);
		Vector3 planePoint = new Vector3(stampPos.x, 0f, stampPos.y);
		float num = LevelEditorTools.SignedDistancePlanePoint(vector, planePoint, worldPos);
		float num2 = LevelEditorTools.SignedDistancePlanePoint(planeNormal, planePoint, worldPos);
		if (Mathf.Abs(num) > stampSize || Mathf.Abs(num2) > stampSize)
		{
			return 0f;
		}
		Vector2 a = new Vector2(num2, num) / stampSize / 2f;
		a += new Vector2(0.5f, 0.5f);
		Vector2 vector2 = a * (float)stampTexture.width;
		return stampTexture.GetPixel((int)vector2.x, (int)vector2.y).r;
	}

	public static float GetStampStrengthFast(Vector3 forward, Vector3 right, Vector3 stampWorldPos, float stampSize, Vector3 worldPos, int stampTexWidth, Color[] stampPixels)
	{
		float num = LevelEditorTools.SignedDistancePlanePoint(forward, stampWorldPos, worldPos);
		if (Mathf.Abs(num) > stampSize)
		{
			return 0f;
		}
		float num2 = LevelEditorTools.SignedDistancePlanePoint(right, stampWorldPos, worldPos);
		if (Mathf.Abs(num2) > stampSize)
		{
			return 0f;
		}
		Vector2 a = new Vector2(num2, num) / stampSize / 2f + Vector2.one / 2f;
		Vector2 vector = a * (float)stampTexWidth;
		int num3 = (int)vector.x;
		int num4 = (int)vector.y;
		int num5 = num4 * stampTexWidth + num3;
		if (stampPixels.Length <= num5)
		{
			return 0f;
		}
		Color color = stampPixels[num5];
		return color.r;
	}

	public static void ApplyHeightPath(Terrain terrain, TerrainPath path, float[,] defHeights, Texture2D[] pathPatterns)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		TerrainData terrainData = terrain.terrainData;
		float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
		for (int i = 0; i < terrain.terrainData.heightmapResolution; i++)
		{
			for (int j = 0; j < terrain.terrainData.heightmapResolution; j++)
			{
				Vector3 vector = terrain.GetPosition() + new Vector3((float)i, 0f, (float)j) / (float)terrainData.heightmapResolution * terrainData.size.x;
				vector.y = terrain.SampleHeight(vector);
				float num = LevelEditorTools.GetPathWeight(path, vector, pathPatterns);
				if (num != 0f)
				{
					if (path.pathAction == ModAction.LandscapeLowering)
					{
						num = -num;
					}
					float num2 = defHeights[j, i] + num * path.pathStrength * 0.05f;
					float num3 = heights[j, i] - defHeights[j, i];
					float num4 = num2 - defHeights[j, i];
					float num5 = num3 + num4;
					if (num3 > 0f && num4 > 0f)
					{
						num5 = Mathf.Max(num3, num4);
					}
					if (num3 < 0f && num4 < 0f)
					{
						num5 = Mathf.Min(num3, num4);
					}
					float num6 = defHeights[j, i] + num5;
					heights[j, i] = num6;
				}
			}
		}
		terrain.terrainData.SetHeights(0, 0, heights);
		float realtimeSinceStartup2 = Time.realtimeSinceStartup;
		float num7 = realtimeSinceStartup2 - realtimeSinceStartup;
		UnityEngine.Debug.Log(num7);
	}

	public static void ApplyAllHeightPaths(Terrain terrain, List<TerrainPath> paths, float[,] defHeights, Texture2D[] pathPatterns)
	{
		TerrainData terrainData = terrain.terrainData;
		float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
		for (int i = 0; i < terrain.terrainData.heightmapResolution; i++)
		{
			for (int j = 0; j < terrain.terrainData.heightmapResolution; j++)
			{
				Vector3 vector = terrain.GetPosition() + new Vector3((float)i, 0f, (float)j) / (float)terrainData.heightmapResolution * terrainData.size.x;
				vector.y = terrain.SampleHeight(vector);
				float allPathWeights = LevelEditorTools.GetAllPathWeights(paths, vector, pathPatterns);
				if (allPathWeights != 0f)
				{
					float num = defHeights[j, i] + allPathWeights * 0.05f;
					float num2 = heights[j, i] - defHeights[j, i];
					float num3 = num - defHeights[j, i];
					float num4 = num2 + num3;
					if (num2 > 0f && num3 > 0f)
					{
						num4 = Mathf.Max(num2, num3);
					}
					if (num2 < 0f && num3 < 0f)
					{
						num4 = Mathf.Min(num2, num3);
					}
					float num5 = defHeights[j, i] + num4;
					heights[j, i] = num5;
				}
			}
		}
		terrain.terrainData.SetHeights(0, 0, heights);
	}

	public static float GetAllPathWeights(List<TerrainPath> paths, Vector3 worldPos, Texture2D[] pathPatterns)
	{
		float num = 0f;
		worldPos.y = 0f;
		foreach (TerrainPath terrainPath in paths)
		{
			if (terrainPath.pathPositions.Count != 0)
			{
				float num2 = 0f;
				for (int i = 0; i < terrainPath.pathPositions.Count - 1; i++)
				{
					Vector3 vector = terrainPath.pathPositions[i];
					vector.y = 0f;
					Vector3 vector2 = terrainPath.pathPositions[i + 1];
					vector2.y = 0f;
					Vector3 vector3 = vector2 - vector;
					Vector3 vector4 = Vector3.Cross(vector3.normalized, Vector3.up);
					float magnitude = vector3.magnitude;
					Vector3 vector5 = (vector + vector2) / 2f;
					Vector3 b = vector2 - vector4 * terrainPath.pathWidth / 2f;
					float num3 = Vector3.Distance(vector5, b);
					bool flag = true;
					if (Vector3.Distance(worldPos, vector5) > num3)
					{
						flag = false;
					}
					float num4 = 0f;
					float num5 = 0f;
					if (flag)
					{
						float num6 = Mathf.Abs(LevelEditorTools.SignedDistancePlanePoint(vector4, vector5, worldPos));
						if (num6 > terrainPath.pathWidth / 2f)
						{
							flag = false;
						}
						if (flag)
						{
							float num7 = terrainPath.pathWidth / 2f - num6;
							num4 = num7 / (terrainPath.pathWidth / 2f);
						}
					}
					float num8 = Vector3.Distance(vector, worldPos);
					if (num8 < terrainPath.pathWidth / 2f)
					{
						num5 = 1f - num8 / (terrainPath.pathWidth / 2f);
					}
					num2 = Mathf.Max(new float[]
					{
						num2,
						num4,
						num5
					});
				}
				int y = (int)((float)pathPatterns[terrainPath.pathPattern].height / 2f * num2);
				float num9 = pathPatterns[terrainPath.pathPattern].GetPixel(0, y).a;
				if (terrainPath.pathAction == ModAction.LandscapeLowering)
				{
					num9 = -num9;
				}
				num9 *= terrainPath.pathStrength;
				if (num == 0f)
				{
					num = num9;
				}
				if (num > 0f && num9 > 0f)
				{
					num = Mathf.Max(num, num9);
				}
				else if (num < 0f && num9 < 0f)
				{
					num = Mathf.Min(num, num9);
				}
				else
				{
					num += num9;
				}
			}
		}
		return num;
	}

	public static void ApplySmoothPath(Terrain terrain, TerrainPath path, Texture2D[] pathPatterns)
	{
		TerrainData terrainData = terrain.terrainData;
		float num = 0f;
		int num2 = 0;
		float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
		for (int i = 0; i < terrainData.heightmapResolution; i++)
		{
			for (int j = 0; j < terrainData.heightmapResolution; j++)
			{
				Vector3 vector = terrain.GetPosition() + new Vector3((float)i, 0f, (float)j) / (float)terrainData.heightmapResolution * terrainData.size.x;
				vector.y = terrain.SampleHeight(vector);
				float pathWeight = LevelEditorTools.GetPathWeight(path, vector, pathPatterns);
				if (pathWeight != 0f)
				{
					num += heights[j, i];
					num2++;
				}
			}
		}
		num /= (float)num2;
		for (int k = 0; k < terrain.terrainData.heightmapResolution; k++)
		{
			for (int l = 0; l < terrain.terrainData.heightmapResolution; l++)
			{
				Vector3 vector2 = terrain.GetPosition() + new Vector3((float)k, 0f, (float)l) / (float)terrainData.heightmapResolution * terrainData.size.x;
				vector2.y = terrain.SampleHeight(vector2);
				float pathWeight2 = LevelEditorTools.GetPathWeight(path, vector2, pathPatterns);
				UnityEngine.Debug.DrawRay(vector2, Vector3.up * pathWeight2, Color.red, 5f);
				if (pathWeight2 != 0f)
				{
					heights[l, k] = Mathf.Lerp(heights[l, k], num, path.pathStrength);
				}
			}
		}
		terrain.terrainData.SetHeights(0, 0, heights);
	}

	public static void ApplyPaintPath(Terrain terrain, TerrainPath path, Texture2D[] pathPatterns)
	{
		TerrainData terrainData = terrain.terrainData;
		float[,,] alphamaps = terrainData.GetAlphamaps(0, 0, terrainData.alphamapWidth, terrainData.alphamapWidth);
		for (int i = 0; i < terrainData.alphamapWidth; i++)
		{
			for (int j = 0; j < terrainData.alphamapWidth; j++)
			{
				Vector3 vector = terrain.GetPosition() + new Vector3((float)i, 0f, (float)j) / (float)terrainData.alphamapWidth * terrainData.size.x;
				vector.y = terrain.SampleHeight(vector);
				float num = LevelEditorTools.GetPathWeight(path, vector, pathPatterns) * path.pathStrength;
				if (num != 0f)
				{
					alphamaps[j, i, path.extraInt] += num;
					for (int k = 0; k < alphamaps.GetUpperBound(2) + 1; k++)
					{
						alphamaps[j, i, k] -= ((k != path.extraInt) ? num : 0f);
						alphamaps[j, i, k] = Mathf.Clamp01(alphamaps[j, i, k]);
					}
					float num2 = 0f;
					for (int l = 0; l < alphamaps.GetUpperBound(2) + 1; l++)
					{
						num2 += alphamaps[j, i, l];
					}
					for (int m = 0; m < alphamaps.GetUpperBound(2) + 1; m++)
					{
						alphamaps[j, i, m] /= num2;
					}
				}
			}
		}
		terrainData.SetAlphamaps(0, 0, alphamaps);
	}

	public static void ApplyRemoveExtraObjectsPath(Terrain terrain, TerrainPath path, int seed, Texture2D[] pathPatterns, ref List<TreeInstance> removedTrees, ref List<GameObject> removedExtraObjects, bool previewOnly)
	{
		TerrainData terrainData = terrain.terrainData;
		UnityEngine.Random.InitState(seed);
		removedTrees.Clear();
		removedExtraObjects.Clear();
		List<TreeInstance> list = new List<TreeInstance>(terrainData.treeInstances);
		int count = list.Count;
		for (int i = 0; i < list.Count; i++)
		{
			Vector3 position = list[i].position;
			Vector3 vector = terrain.GetPosition() + position * terrainData.size.x;
			float pathWeight = LevelEditorTools.GetPathWeight(path, vector, pathPatterns);
			UnityEngine.Debug.DrawRay(vector, Vector3.up * 2f * pathWeight, Color.red, 5f);
			if (pathWeight != 0f)
			{
				if (UnityEngine.Random.Range(0f, 1f) < pathWeight)
				{
					if (previewOnly)
					{
						removedTrees.Add(list[i]);
					}
					list.RemoveAt(i);
					i--;
				}
			}
		}
		terrainData.treeInstances = list.ToArray();
		GameObject[] allChildren = LevelEditorTools.GetAllChildren(LevelEditorTools.ExtraObjectsParent.gameObject);
		for (int j = 0; j < allChildren.Length; j++)
		{
			float pathWeight2 = LevelEditorTools.GetPathWeight(path, allChildren[j].transform.position, pathPatterns);
			if (pathWeight2 != 0f)
			{
				if (UnityEngine.Random.Range(0f, 1f) < pathWeight2)
				{
					if (!previewOnly)
					{
						UnityEngine.Object.Destroy(allChildren[j]);
					}
					else
					{
						removedExtraObjects.Add(allChildren[j]);
						allChildren[j].SetActive(false);
					}
				}
			}
		}
	}

	public static int ApplyAddExtraObjectsPath(Terrain terrain, Transform waterPlane, TerrainPath path, Texture2D[] pathTextures, int seed, ExtraObjectArray[] extraObjectsDictionary, float terrainSize, bool waterEnabled, ref List<TreeInstance> addedTrees, ref List<GameObject> addedExtraObjects, int lastPlacedExtraObjectID)
	{
		TerrainData terrainData = terrain.terrainData;
		UnityEngine.Random.InitState(seed);
		addedTrees.Clear();
		addedExtraObjects.Clear();
		int extraInt = path.extraInt;
		float pathAreaSize = LevelEditorTools.GetPathAreaSize(path);
		LevelEditorResources editorResources = LevelEditorTools.editorResources;
		if (extraInt == -1)
		{
			int num = (int)(pathAreaSize / 2f * path.pathStrength);
			int num2 = 0;
			int num3 = 0;
			while (num3 < num && num2 < 1000)
			{
				num2++;
				Vector3 randomPointWithinPath = LevelEditorTools.GetRandomPointWithinPath(terrain, path);
				Vector3 position = (randomPointWithinPath - terrain.GetPosition()) / terrainData.size.x;
				float pathWeight = LevelEditorTools.GetPathWeight(path, randomPointWithinPath, pathTextures);
				if (pathWeight != 0f)
				{
					bool flag = UnityEngine.Random.Range(0f, 1f) < pathWeight;
					if (waterPlane.gameObject.activeSelf && randomPointWithinPath.y < waterPlane.position.y)
					{
						flag = false;
					}
					if (flag)
					{
						TreeInstance treeInstance = default(TreeInstance);
						treeInstance.prototypeIndex = UnityEngine.Random.Range(0, terrainData.treePrototypes.Length);
						treeInstance.color = Color.white;
						treeInstance.position = position;
						treeInstance.rotation = UnityEngine.Random.Range(0f, 360f) * (float)num3 * 0.0174532924f;
						treeInstance.heightScale = UnityEngine.Random.Range(0.5f, 1.5f);
						treeInstance.widthScale = treeInstance.heightScale;
						terrain.AddTreeInstance(treeInstance);
						num3++;
						addedTrees.Add(treeInstance);
					}
				}
			}
		}
		else
		{
			ExtraObjectArray extraObjectArray = extraObjectsDictionary[extraInt];
			float num4 = terrainSize / pathAreaSize;
			int num5 = (int)((float)extraObjectArray.baseObjectsCount / num4 * path.pathStrength);
			int num6 = 0;
			int num7 = 0;
			List<Vector3> list = new List<Vector3>();
			while (num7 < num5 && num6 < 1000)
			{
				num6++;
				Vector3 randomPointWithinPath2 = LevelEditorTools.GetRandomPointWithinPath(terrain, path);
				float pathWeight2 = LevelEditorTools.GetPathWeight(path, randomPointWithinPath2, pathTextures);
				bool flag2 = UnityEngine.Random.Range(0f, 1f) < pathWeight2;
				foreach (Vector3 a in list)
				{
					if (Vector3.Distance(a, randomPointWithinPath2) < extraObjectArray.minDistanceInterval)
					{
						flag2 = false;
					}
				}
				if (waterEnabled && randomPointWithinPath2.y < waterPlane.position.y)
				{
					flag2 = false;
				}
				if (flag2)
				{
					int num8 = UnityEngine.Random.Range(0, extraObjectArray.prefabs.Length);
					GameObject obj = extraObjectArray.prefabs[num8];
					Quaternion rotation = extraObjectArray.prefabs[num8].transform.rotation;
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(extraObjectArray.prefabs[num8], randomPointWithinPath2, rotation, LevelEditorTools.ExtraObjectsParent.transform);
					gameObject.AddComponent<ExtraObject>().ID = lastPlacedExtraObjectID;
					lastPlacedExtraObjectID++;
					int propID = -1;
					for (int i = 0; i < editorResources.propsDictionary.Length; i++)
					{
						if (editorResources.propsDictionary[i].gameObject.Equals(obj))
						{
							propID = i;
						}
					}
					gameObject.GetComponent<Prop>().propID = propID;
					PlacementSettings component = gameObject.GetComponent<PlacementSettings>();
					if (component != null)
					{
						component.ApplyRandomRotation();
					}
					Prop component2 = gameObject.GetComponent<Prop>();
					if (component2 != null)
					{
						component2.Highlight(false);
					}
					num7++;
					list.Add(randomPointWithinPath2);
					addedExtraObjects.Add(gameObject);
				}
			}
			LevelEditorTools.CorrectExtraObjectsTransforms(terrain, seed);
		}
		return lastPlacedExtraObjectID;
	}

	public static float GetPathWeight(TerrainPath path, Vector3 worldPos, Texture2D[] pathPatterns)
	{
		if (path.pathPositions.Count == 0)
		{
			return 0f;
		}
		float num = 0f;
		worldPos.y = 0f;
		for (int i = 0; i < path.pathPositions.Count - 1; i++)
		{
			Vector3 vector = path.pathPositions[i];
			vector.y = 0f;
			Vector3 vector2 = path.pathPositions[i + 1];
			vector2.y = 0f;
			Vector3 vector3 = vector2 - vector;
			Vector3 vector4 = Vector3.Cross(vector3.normalized, Vector3.up);
			float magnitude = vector3.magnitude;
			Vector3 vector5 = (vector + vector2) / 2f;
			Vector3 b = vector2 - vector4 * path.pathWidth / 2f;
			float num2 = Vector3.Distance(vector5, b);
			bool flag = true;
			if (Vector3.Distance(worldPos, vector5) > num2)
			{
				flag = false;
			}
			float num3 = 0f;
			float num4 = 0f;
			if (flag)
			{
				float num5 = Mathf.Abs(LevelEditorTools.SignedDistancePlanePoint(vector4, vector5, worldPos));
				if (num5 > path.pathWidth / 2f)
				{
					flag = false;
				}
				if (flag)
				{
					float num6 = path.pathWidth / 2f - num5;
					num3 = num6 / (path.pathWidth / 2f);
				}
			}
			float num7 = Vector3.Distance(vector, worldPos);
			if (num7 < path.pathWidth / 2f)
			{
				num4 = 1f - num7 / (path.pathWidth / 2f);
			}
			num = Mathf.Max(new float[]
			{
				num,
				num3,
				num4
			});
		}
		int y = (int)((float)pathPatterns[path.pathPattern].height / 2f * num);
		return pathPatterns[path.pathPattern].GetPixel(0, y).a;
	}

	public static float SignedDistancePlanePoint(Vector3 planeNormal, Vector3 planePoint, Vector3 point)
	{
		return Vector3.Dot(planeNormal, point - planePoint);
	}

	private static Vector3 GetNormalAtPosition(Terrain terrain, Vector3 pos)
	{
		float x = (pos.x - terrain.GetPosition().x) / terrain.terrainData.size.x;
		float y = (pos.z - terrain.GetPosition().z) / terrain.terrainData.size.z;
		return terrain.terrainData.GetInterpolatedNormal(x, y);
	}

	private static GameObject[] GetAllChildren(GameObject parent)
	{
		GameObject[] array = new GameObject[parent.transform.childCount];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = parent.transform.GetChild(i).gameObject;
		}
		return array;
	}

	public static float DistanceFromCenter(Terrain terrain, int x, int y)
	{
		Vector2 a = new Vector2((float)x, (float)y);
		Vector2 b = Vector2.one * (float)terrain.terrainData.heightmapResolution / 2f;
		float num = Vector2.Distance(a, b);
		return num / ((float)terrain.terrainData.heightmapResolution / 2f);
	}

	public static float DistanceFromCenter(float x, float y)
	{
		Vector2 a = new Vector2(x, y);
		Vector2 b = Vector2.one / 2f;
		return Vector2.Distance(a, b);
	}

	public static float DistanceFromCenter(Terrain terrain, Vector3 worldPos)
	{
		Vector3 vector = terrain.GetPosition() + terrain.terrainData.size / 2f;
		Vector2 a = new Vector2(vector.x, vector.z);
		Vector2 b = new Vector2(worldPos.x, worldPos.z);
		float num = Vector2.Distance(a, b);
		return num / (terrain.terrainData.size.x / 2f);
	}

	private static Vector3 GetRandomPointOnTerrain(Terrain terrain)
	{
		Vector3 vector = terrain.GetPosition();
		vector += UnityEngine.Random.Range(0f, terrain.terrainData.size.x) * Vector3.right;
		vector += UnityEngine.Random.Range(0f, terrain.terrainData.size.z) * Vector3.forward;
		vector.y = terrain.SampleHeight(vector);
		return vector;
	}

	private static Vector3 GetRandomPointWithinStamp(Terrain terrain, TerrainStamp stamp)
	{
		Vector3 vector = new Vector3(stamp.stampPosition.x, 0f, stamp.stampPosition.y);
		Vector3 normalized = Vector3.ProjectOnPlane(UnityEngine.Random.onUnitSphere, Vector3.up).normalized;
		vector += normalized * stamp.stampSize * UnityEngine.Random.Range(0f, 1f);
		vector.y = terrain.SampleHeight(vector);
		return vector;
	}

	private static float GetPathAreaSize(TerrainPath path)
	{
		float num = 0f;
		for (int i = 0; i < path.pathPositions.Count - 1; i++)
		{
			num += Vector3.Distance(path.pathPositions[i], path.pathPositions[i + 1]);
		}
		float num2 = num * path.pathWidth;
		float num3 = (num + path.pathWidth) / 2f;
		return num2 / num3;
	}

	private static Vector3 GetRandomPointWithinPath(Terrain terrain, TerrainPath path)
	{
		int num = UnityEngine.Random.Range(0, path.pathPositions.Count);
		int num2 = num + 1;
		if (num2 >= path.pathPositions.Count)
		{
			num2 = num - 1;
		}
		float t = UnityEngine.Random.Range(0f, 1f);
		Vector3 vector = Vector3.Lerp(path.pathPositions[num], path.pathPositions[num2], t);
		Vector3 a = Vector3.Cross((path.pathPositions[num] - path.pathPositions[num2]).normalized, Vector3.up);
		float d = UnityEngine.Random.Range(-1f, 1f) * (path.pathWidth / 2f);
		vector += a * d;
		vector.y = terrain.SampleHeight(vector);
		return vector;
	}

	public static Vector3 ProjectPointOnPlane(Vector3 planeNormal, Vector3 planePoint, Vector3 point)
	{
		float d = -LevelEditorTools.SignedDistancePlanePoint(planeNormal, planePoint, point);
		Vector3 b = planeNormal.normalized * d;
		return point + b;
	}

	public static void AlignByNormal(Terrain terrain, Transform obj, Transform frontSupport = null, Transform rearSupport = null)
	{
		Vector3 right = obj.right;
		Vector3 vector = Vector3.zero;
		Vector3 forward = Vector3.zero;
		if (frontSupport == null || rearSupport == null)
		{
			float x = (obj.position.x - terrain.GetPosition().x) / terrain.terrainData.size.x;
			float y = (obj.position.z - terrain.GetPosition().z) / terrain.terrainData.size.z;
			Vector3 interpolatedNormal = terrain.terrainData.GetInterpolatedNormal(x, y);
			vector = interpolatedNormal;
			forward = Vector3.Cross(right, vector);
		}
		else
		{
			Vector3 position = frontSupport.position;
			position.y = terrain.SampleHeight(position);
			Vector3 position2 = rearSupport.position;
			position2.y = terrain.SampleHeight(position2);
			Vector3 normalized = (position - position2).normalized;
			vector = Vector3.Cross(normalized, right);
			forward = Vector3.ProjectOnPlane(obj.forward, vector);
			vector = Vector3.up;
		}
		obj.rotation = Quaternion.LookRotation(forward, vector);
	}

	public static string RandomName()
	{
		UnityEngine.Random.InitState(Environment.TickCount);
		string text = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
		string text2 = string.Empty;
		for (int i = 0; i < 10; i++)
		{
			text2 += text[UnityEngine.Random.Range(0, text.Length)];
		}
		return text2;
	}

	public static void AddMapToFavs(string mapID)
	{
		List<string> list = LevelEditorTools.FavMapsList;
		if (list == null)
		{
			list = new List<string>();
			list.Add(mapID);
		}
		else if (!list.Contains(mapID))
		{
			list.Add(mapID);
		}
		LevelEditorTools.WriteFavMaps(list);
	}

	public static void RemoveFromFavs(string mapID)
	{
		List<string> favMapsList = LevelEditorTools.FavMapsList;
		if (favMapsList == null)
		{
			return;
		}
		if (favMapsList.Contains(mapID))
		{
			favMapsList.Remove(mapID);
		}
		LevelEditorTools.WriteFavMaps(favMapsList);
	}

	public static void AddMapToMyMaps(string mapID)
	{
		List<string> list = LevelEditorTools.MyMapsList;
		if (list == null)
		{
			list = new List<string>();
			list.Add(mapID);
		}
		else if (!list.Contains(mapID))
		{
			list.Add(mapID);
		}
		LevelEditorTools.WriteMyMaps(list);
	}

	public static void RemoveFromMyMaps(string mapID)
	{
		List<string> favMapsList = LevelEditorTools.FavMapsList;
		if (favMapsList == null)
		{
			return;
		}
		if (favMapsList.Contains(mapID))
		{
			favMapsList.Remove(mapID);
		}
		LevelEditorTools.WriteFavMaps(favMapsList);
	}

	public static List<string> FavMapsList
	{
		get
		{
			string @string = PlayerPrefs.GetString("FavMaps", string.Empty);
			if (@string == string.Empty)
			{
				return null;
			}
			string[] collection = @string.Split(new char[]
			{
				'#'
			});
			return new List<string>(collection);
		}
	}

	public static void GetVotedMaps(out List<string> maps, out List<int> amounts)
	{
		maps = new List<string>();
		amounts = new List<int>();
		string @string = PlayerPrefs.GetString("VotedMaps", string.Empty);
		if (@string == string.Empty)
		{
			return;
		}
		string[] array = @string.Split(new char[]
		{
			'#'
		});
		for (int i = 0; i < array.Length; i++)
		{
			if (!(array[i] == string.Empty))
			{
				string[] array2 = array[i].Split(new char[]
				{
					'/'
				});
				if (array2.Length == 2)
				{
					maps.Add(array2[0]);
					amounts.Add(int.Parse(array2[1]));
				}
			}
		}
	}

	public static void WriteFavMaps(List<string> maps)
	{
		string text = string.Empty;
		for (int i = 0; i < maps.Count; i++)
		{
			text += maps[i];
			if (i < maps.Count - 1)
			{
				text += "#";
			}
		}
		PlayerPrefs.SetString("FavMaps", text);
	}

	public static void WriteMyMaps(List<string> maps)
	{
		string text = string.Empty;
		foreach (string text2 in maps)
		{
			if (text2 != string.Empty)
			{
				text = text + text2 + "#";
			}
		}
		PlayerPrefs.SetString("MyMaps", text);
	}

	public static void WriteVotedMaps(List<string> maps, List<int> amounts)
	{
		string text = string.Empty;
		for (int i = 0; i < maps.Count; i++)
		{
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				maps[i],
				"/",
				amounts[i],
				"#"
			});
		}
		PlayerPrefs.SetString("VotedMaps", text);
	}

	public static List<string> MyMapsList
	{
		get
		{
			string @string = PlayerPrefs.GetString("MyMaps", string.Empty);
			if (@string == string.Empty)
			{
				return null;
			}
			string[] collection = @string.Split(new char[]
			{
				'#'
			});
			return new List<string>(collection);
		}
	}

	public static bool IsMapInFavs(string mapID)
	{
		return LevelEditorTools.FavMapsList != null && LevelEditorTools.FavMapsList.Contains(mapID);
	}

	public static bool IsMapMadeMyMe(string mapID)
	{
		return LevelEditorTools.MyMapsList != null && LevelEditorTools.MyMapsList.Contains(mapID);
	}

	public static bool DidIVoteForMap(string mapID, out int amount)
	{
		amount = 0;
		List<string> list;
		List<int> list2;
		LevelEditorTools.GetVotedMaps(out list, out list2);
		if (list.Contains(mapID))
		{
			amount = list2[list.IndexOf(mapID)];
			return true;
		}
		return false;
	}

	public static void AddMapToVoted(string mapID, bool up)
	{
		List<string> list;
		List<int> list2;
		LevelEditorTools.GetVotedMaps(out list, out list2);
		if (!list.Contains(mapID))
		{
			list.Add(mapID);
			list2.Add((!up) ? -1 : 1);
		}
		else
		{
			list2[list.IndexOf(mapID)] = ((!up) ? -1 : 1);
		}
		LevelEditorTools.WriteVotedMaps(list, list2);
	}
}
