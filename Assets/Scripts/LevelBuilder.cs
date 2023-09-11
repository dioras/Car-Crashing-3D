using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelBuilder : MonoBehaviour
{
	private LevelWebHandler GetWebHandler
	{
		get
		{
			if (base.GetComponent<LevelWebHandler>() != null)
			{
				return base.GetComponent<LevelWebHandler>();
			}
			return base.gameObject.AddComponent<LevelWebHandler>();
		}
	}

	private Terrain terrain
	{
		get
		{
			return Terrain.activeTerrain;
		}
	}

	private void Awake()
	{
		this.waterPlane = GameObject.Find("WaterPlane").transform;
		LevelEditorTools.ResetTerrain(this.terrain);
	}

	private void Start()
	{
		if (this.sceneLoadingWindow != null)
		{
			this.sceneLoadingWindow.SetActive(true);
		}
		if (GameState.mapToDownload != null && GameState.mapToDownload != string.Empty)
		{
			this.DownloadLevel(GameState.mapToDownload);
		}
		else
		{
			base.GetComponent<LevelEditor>().seed = UnityEngine.Random.Range(0, 10000);
			base.GetComponent<LevelEditorUI>().InitializeUI();
			base.GetComponent<LevelEditor>().GenerateTerrain(true);
			base.GetComponent<LevelEditor>().ChangeLevelCreationStep(LevelCreationStep.Generation);
			if (this.sceneLoadingWindow != null)
			{
				this.sceneLoadingWindow.SetActive(false);
			}
		}
	}

	[ContextMenu("Upload level")]
	private void UploadLevel()
	{
		this.UploadLevel(base.GetComponent<LevelEditor>(), null, null);
	}

	[ContextMenu("Upload 100 maps")]
	private void Upload100Maps()
	{
		base.StartCoroutine(this.Upload100MapsCor());
	}

	private IEnumerator Upload100MapsCor()
	{
		for (int i = 0; i < 3000; i++)
		{
			base.GetComponent<LevelEditor>().mapID = string.Empty;
			string chars = "ABCDEFGHIGKLMNOPQRSTUVWXYZ";
			string randMapName = string.Empty;
			randMapName += chars[UnityEngine.Random.Range(0, chars.Length)];
			randMapName += chars[UnityEngine.Random.Range(0, chars.Length)];
			randMapName += chars[UnityEngine.Random.Range(0, chars.Length)];
			randMapName += chars[UnityEngine.Random.Range(0, chars.Length)];
			randMapName += chars[UnityEngine.Random.Range(0, chars.Length)];
			base.GetComponent<LevelEditor>().mapName = randMapName;
			base.GetComponent<LevelEditor>().mapDescription = "Description";
			base.GetComponent<LevelEditor>().mapVisible = true;
			this.UploadLevel(base.GetComponent<LevelEditor>(), null, null);
			LevelEditorTools.AddMapToMyMaps(base.GetComponent<LevelEditor>().mapID);
			yield return null;
		}
		yield break;
	}

	public void RemoveLevel(LevelEditor editor, Action successCallback = null, Action errorCallback = null)
	{
		LevelWebHandler getWebHandler = this.GetWebHandler;
		getWebHandler.RemoveLevel(editor.mapID, successCallback, errorCallback);
	}

	public void UploadLevel(LevelEditor editor, Action successCallback = null, Action errorCallback = null)
	{
		editor.CheckRoutesLenghts();
		string text = editor.mapID;
		if (text == string.Empty || text == null)
		{
			text = LevelEditorTools.RandomName();
			editor.mapID = text;
		}
		string text2 = string.Empty;
		string text3 = editor.mapName.Replace("|", "/");
		string text4 = editor.mapDescription.Replace("|", "/");
		text3 = Utility.CleanBadWords(text3);
		text4 = Utility.CleanBadWords(text4);
		text2 = text2 + text3 + "\n";
		text2 = text2 + text4 + "\n";
		text2 = text2 + editor.mapRating + "\n";
		text2 = text2 + editor.mapVisible.ToString() + "\n";
		text2 = text2 + GameState.playerName + "\n";
		text2 = text2 + editor.seed.ToString() + "\n";
		text2 = text2 + ((int)editor.terrainSize).ToString() + "\n";
		text2 = text2 + editor.bumpsStrength.ToString() + "\n";
		text2 = text2 + editor.flatCenter.ToString() + "\n";
		text2 = text2 + editor.treesDensity.ToString() + "\n";
		text2 = text2 + editor.treesByEdgesOnly.ToString() + "\n";
		text2 = text2 + editor.waterEnabled.ToString() + "\n";
		text2 = text2 + editor.frozenWater.ToString() + "\n";
		text2 = text2 + editor.waterHeight.ToString() + "\n";
		text2 = text2 + editor.mainTextureID.ToString() + "\n";
		string str = string.Concat(new object[]
		{
			editor.usedExtraObjects[0].arrayID,
			"|",
			editor.usedExtraObjects[0].density,
			"|",
			editor.usedExtraObjects[0].onlyByEdges.ToString()
		});
		string str2 = string.Concat(new object[]
		{
			editor.usedExtraObjects[1].arrayID,
			"|",
			editor.usedExtraObjects[1].density,
			"|",
			editor.usedExtraObjects[1].onlyByEdges.ToString()
		});
		string str3 = string.Concat(new object[]
		{
			editor.usedExtraObjects[2].arrayID,
			"|",
			editor.usedExtraObjects[2].density,
			"|",
			editor.usedExtraObjects[2].onlyByEdges.ToString()
		});
		text2 = text2 + str + "\n";
		text2 = text2 + str2 + "\n";
		text2 = text2 + str3 + "\n";
		string text5 = string.Empty;
		foreach (int num in editor.ignoredExtraObjectIDs)
		{
			text5 = text5 + num + "|";
		}
		text2 = text2 + text5 + "\n";
		text2 = text2 + editor.mudStamps.Count + "\n";
		for (int i = 0; i < editor.mudStamps.Count; i++)
		{
			text2 = text2 + editor.mudStamps[i].Serialize() + "\n";
		}
		text2 = text2 + editor.stamps.Count.ToString() + "\n";
		for (int j = 0; j < editor.stamps.Count; j++)
		{
			string str4 = editor.stamps[j].Serialize();
			text2 = text2 + str4 + "\n";
		}
		text2 = text2 + editor.paths.Count.ToString() + "\n";
		for (int k = 0; k < editor.paths.Count; k++)
		{
			string str5 = editor.paths[k].Serialize();
			text2 = text2 + str5 + "\n";
		}
		Prop[] componentsInChildren = LevelEditorTools.PropsParent.GetComponentsInChildren<Prop>();
		text2 = text2 + componentsInChildren.Length.ToString() + "\n";
		for (int l = 0; l < componentsInChildren.Length; l++)
		{
			text2 = text2 + componentsInChildren[l].Serialize() + "\n";
		}
		text2 = text2 + editor.routes.Count.ToString() + "\n";
		for (int m = 0; m < editor.routes.Count; m++)
		{
			text2 = text2 + editor.routes[m].Serialize() + "\n";
		}
		LevelWebHandler getWebHandler = this.GetWebHandler;
		getWebHandler.UploadLevel(text2, text, successCallback, errorCallback);
	}

	public void DownloadLevel(string fileName)
	{
		if (this.editScene)
		{
			LevelEditor component = base.GetComponent<LevelEditor>();
			if (component.OnProcessingTerrainStarted != null)
			{
				component.OnProcessingTerrainStarted("Downloading map...");
			}
		}
		LevelWebHandler getWebHandler = this.GetWebHandler;
		getWebHandler.DownloadLevel(fileName, new Action<string>(this.OnLevelDownloaded), null);
	}

	public void OnLevelDownloaded(string levelData)
	{
		UnityEngine.Debug.Log("level loaded");
		UnityEngine.Debug.Log(levelData);
		this.editorResources = LevelEditorTools.editorResources;
		string[] array = levelData.Split(new char[]
		{
			'\n'
		});
		this.mapName = array[0];
		this.mapDescription = array[1];
		this.mapRating = int.Parse(array[2]);
		this.mapVisible = bool.Parse(array[3]);
		this.seed = int.Parse(array[5]);
		this.terrainSize = int.Parse(array[6]);
		this.bumpsStrength = float.Parse(array[7]);
		this.flatCenter = bool.Parse(array[8]);
		this.treesDensity = float.Parse(array[9]);
		this.treesByEdgesOnly = bool.Parse(array[10]);
		this.waterEnabled = bool.Parse(array[11]);
		this.frozenWater = bool.Parse(array[12]);
		this.waterHeight = float.Parse(array[13]);
		this.mainTextureID = int.Parse(array[14]);
		this.usedExtraObjects = new ExtraObjectReference[3];
		int num = 15;
		for (int i = 0; i < 3; i++)
		{
			string text = array[num];
			string[] array2 = text.Split(new char[]
			{
				'|'
			});
			this.usedExtraObjects[i] = new ExtraObjectReference();
			this.usedExtraObjects[i].arrayID = int.Parse(array2[0]);
			this.usedExtraObjects[i].density = float.Parse(array2[1]);
			this.usedExtraObjects[i].onlyByEdges = bool.Parse(array2[2]);
			num++;
		}
		string text2 = array[num];
		this.ignoredExtraObjectIDs = new List<int>();
		string[] array3 = text2.Split(new char[]
		{
			'|'
		});
		foreach (string text3 in array3)
		{
			if (text3 != string.Empty)
			{
				this.ignoredExtraObjectIDs.Add(int.Parse(text3));
			}
		}
		num++;
		int num2 = int.Parse(array[num]);
		num++;
		this.mudStamps = new List<MudStamp>();
		for (int k = 0; k < num2; k++)
		{
			string data = array[num];
			MudStamp mudStamp = new MudStamp();
			mudStamp.Deserialize(data);
			this.mudStamps.Add(mudStamp);
			num++;
		}
		this.stamps = new List<TerrainStamp>();
		int num3 = int.Parse(array[num]);
		num++;
		for (int l = 0; l < num3; l++)
		{
			string s = array[num];
			TerrainStamp terrainStamp = new TerrainStamp();
			terrainStamp.Deserialize(s);
			this.stamps.Add(terrainStamp);
			num++;
		}
		this.paths = new List<TerrainPath>();
		int num4 = int.Parse(array[num]);
		num++;
		for (int m = 0; m < num4; m++)
		{
			string data2 = array[num];
			TerrainPath terrainPath = new TerrainPath();
			terrainPath.Deserialize(data2);
			this.paths.Add(terrainPath);
			num++;
		}
		int num5 = int.Parse(array[num]);
		num++;
		this.propsStrings = new List<string>();
		for (int n = 0; n < num5; n++)
		{
			string item = array[num];
			this.propsStrings.Add(item);
			num++;
		}
		int num6 = int.Parse(array[num]);
		num++;
		this.routeStrings = new List<string>();
		for (int num7 = 0; num7 < num6; num7++)
		{
			string item2 = array[num];
			this.routeStrings.Add(item2);
			num++;
		}
		this.levelBuildingCoroutine = base.StartCoroutine(this.BuildMap());
	}

	public void CancelBuildingMap()
	{
		if (this.levelBuildingCoroutine != null)
		{
			base.StopCoroutine(this.levelBuildingCoroutine);
		}
		SceneManager.LoadScene("Menu");
	}

	private IEnumerator BuildMap()
	{
		float time = Time.realtimeSinceStartup;
		LevelEditor editor = base.GetComponent<LevelEditor>();
		if (this.editScene && editor.OnProcessingTerrainStarted != null)
		{
			editor.OnProcessingTerrainStarted("Building terrain...");
		}
		this.UpdateMapBuildingStatus("Generating terrain");
		yield return null;
		LevelEditorTools.ResetTerrain(this.terrain);
		LevelEditorTools.GenerateStampBasedTerrain(this.terrain, this.editorResources.terrainGenerationStamps, this.seed, (float)this.terrainSize, this.flatCenter, this.bumpsStrength);
		this.UpdateMapBuildingStatus("Pouring water");
		yield return null;
		LevelEditorTools.ApplyWater(this.terrain, this.waterPlane, this.waterEnabled, (float)this.terrainSize, this.waterHeight, this.frozenWater, this.editorResources.frozenWaterMaterial, this.editorResources.waterMaterial);
		this.UpdateMapBuildingStatus("Planting trees");
		yield return null;
		LevelEditorTools.PlaceTrees(this.terrain, this.waterPlane, this.seed, (float)this.terrainSize, this.editorResources.maxTreesCount, this.treesDensity, this.treesByEdgesOnly);
		this.UpdateMapBuildingStatus("Placing rocks");
		yield return null;
		this.lastPlacedExtraObject = LevelEditorTools.PlaceExtraObjects(this.terrain, this.waterPlane, this.seed, this.usedExtraObjects, this.editorResources.extraObjectsDictionary, (float)this.terrainSize, this.lastPlacedExtraObject);
		this.UpdateMapBuildingStatus("Placing cliffs");
		yield return null;
		LevelEditorTools.PlaceCliffs(this.terrain, this.editorResources.cliffPrefabs, (float)this.terrainSize, this.seed, this.editorResources.baseCliffsCount, this.editorResources.minHillAngle);
		this.UpdateMapBuildingStatus("Applying textures");
		yield return null;
		LevelEditorTools.PaintBaseTexture(this.terrain, this.mainTextureID);
		LevelEditorTools.PaintRockAndWaterTextures(this.terrain, this.waterPlane, this.waterEnabled, this.frozenWater, this.editorResources.underwaterTextureID, this.editorResources.rockTextureID, this.editorResources.minRockAngle, this.editorResources.maxRockAngle, this.mainTextureID);
		List<TerrainStamp> heightStamps = new List<TerrainStamp>();
		List<TerrainStamp> paintStamps = new List<TerrainStamp>();
		List<TerrainStamp> smoothStamps = new List<TerrainStamp>();
		List<TerrainStamp> extraObjectsStamps = new List<TerrainStamp>();
		foreach (TerrainStamp terrainStamp in this.stamps)
		{
			if (terrainStamp.stampAction == ModAction.LandscapeLowering || terrainStamp.stampAction == ModAction.LandscapeRaising)
			{
				heightStamps.Add(terrainStamp);
			}
			else if (terrainStamp.stampAction == ModAction.Painting)
			{
				paintStamps.Add(terrainStamp);
			}
			else if (terrainStamp.stampAction == ModAction.AddingExtraObjects || terrainStamp.stampAction == ModAction.RemovingExtraObjects)
			{
				extraObjectsStamps.Add(terrainStamp);
			}
			else if (terrainStamp.stampAction == ModAction.Smoothing)
			{
				smoothStamps.Add(terrainStamp);
			}
		}
		this.UpdateMapBuildingStatus("Applying height stamps");
		yield return null;
		this.defHeights = this.terrain.terrainData.GetHeights(0, 0, this.terrain.terrainData.heightmapResolution, this.terrain.terrainData.heightmapResolution);
		LevelEditorTools.ApplyAllHeightStamps(this.terrain, heightStamps, this.defHeights, this.editorResources.stampTextures);
		this.UpdateMapBuildingStatus("Applying smooth stamps");
		yield return null;
		LevelBuilder.TextureData[] texDatas = new LevelBuilder.TextureData[this.editorResources.stampTextures.Length];
		for (int i = 0; i < texDatas.Length; i++)
		{
			texDatas[i].colors = this.editorResources.stampTextures[i].GetPixels();
		}
		float[,] heights = this.terrain.terrainData.GetHeights(0, 0, this.terrain.terrainData.heightmapResolution, this.terrain.terrainData.heightmapResolution);
		int percentageThreshold = (int)((float)smoothStamps.Count / 20f);
		int ss = 0;
		foreach (TerrainStamp stamp in smoothStamps)
		{
			if (percentageThreshold > 0 && ss % percentageThreshold == 0)
			{
				int percent = (int)((float)ss / (float)smoothStamps.Count * 100f);
				this.UpdateMapBuildingStatus("Applying smooth stamps (" + percent + "%)");
				yield return null;
			}
			LevelEditorTools.ApplySmoothStampFast(this.terrain, ref heights, stamp, this.editorResources.stampTextures[stamp.stampTextureID].width, texDatas[stamp.stampTextureID].colors);
			ss++;
		}
		this.terrain.terrainData.SetHeights(0, 0, heights);
		this.CorrectExtraObjectsTransforms();
		this.UpdateMapBuildingStatus("Applying texture stamps");
		yield return null;
		TerrainData terData = this.terrain.terrainData;
		Vector3 terPos = this.terrain.GetPosition();
		float[,,] splatMap = terData.GetAlphamaps(0, 0, terData.alphamapWidth, terData.alphamapWidth);
		percentageThreshold = (int)((float)paintStamps.Count / 20f);
		int ps = 0;
		foreach (TerrainStamp stamp2 in paintStamps)
		{
			if (percentageThreshold > 0 && ps % percentageThreshold == 0)
			{
				int percent2 = (int)((float)ps / (float)paintStamps.Count * 100f);
				this.UpdateMapBuildingStatus("Applying texture stamps (" + percent2 + "%)");
				yield return null;
			}
			LevelEditorTools.ApplyPaintStampFast(terData, terPos, ref splatMap, texDatas[stamp2.stampTextureID].colors, this.editorResources.stampTextures[stamp2.stampTextureID].width, stamp2.stampRotation, stamp2.stampPosition, stamp2.stampSize, stamp2.extraInt);
			ps++;
		}
		terData.SetAlphamaps(0, 0, splatMap);
		this.PaintRockAndWaterTextures();
		this.UpdateMapBuildingStatus("Applying trees/props stamps");
		yield return null;
		int eos = 0;
		percentageThreshold = (int)((float)extraObjectsStamps.Count / 20f);
		foreach (TerrainStamp stamp3 in extraObjectsStamps)
		{
			if (percentageThreshold > 0 && eos % percentageThreshold == 0)
			{
				int percent3 = (int)((float)eos / (float)extraObjectsStamps.Count * 100f);
				this.UpdateMapBuildingStatus("Applying trees/props stamps (" + percent3 + "%)");
				yield return null;
			}
			this.ApplyStamp(stamp3);
			eos++;
		}
		List<TerrainPath> heightPaths = new List<TerrainPath>();
		List<TerrainPath> otherPaths = new List<TerrainPath>();
		foreach (TerrainPath terrainPath in this.paths)
		{
			if (terrainPath.pathAction == ModAction.LandscapeRaising || terrainPath.pathAction == ModAction.LandscapeLowering)
			{
				heightPaths.Add(terrainPath);
			}
			else
			{
				otherPaths.Add(terrainPath);
			}
		}
		this.UpdateMapBuildingStatus("Applying height paths");
		yield return null;
		LevelEditorTools.ApplyAllHeightPaths(this.terrain, heightPaths, this.defHeights, this.editorResources.pathPatterns);
		this.PaintRockAndWaterTextures();
		this.CorrectExtraObjectsTransforms();
		int rr = 0;
		foreach (TerrainPath path in otherPaths)
		{
			this.UpdateMapBuildingStatus(string.Concat(new object[]
			{
				"Applying other paths (",
				rr,
				"/",
				otherPaths.Count,
				")"
			}));
			yield return null;
			this.ApplyPath(path);
			rr++;
		}
		this.terrain.GetComponent<TerrainCollider>().enabled = false;
		this.terrain.GetComponent<TerrainCollider>().enabled = true;
		yield return null;
		this.UpdateMapBuildingStatus("Placing props");
		yield return null;
		this.PlaceProps();
		this.UpdateMapBuildingStatus("Placing routes");
		yield return null;
		this.PlaceRoutes();
		this.UpdateMapBuildingStatus("Finalizing map");
		yield return null;
		LevelEditorTools.RemoveIgnoredExtraObjects(this.ignoredExtraObjectIDs);
		this.CorrectExtraObjectsTransforms();
		this.CorrectCliffsPositions();
		yield return null;
		VehicleLoader.Instance.SpawnPoints = this.GetSpawnPoints();
		if (this.playingScene)
		{
			VehicleLoader.Instance.loadVehicle = true;
			PlayerRouteRacingManager.Instance.Initialize();
			Transform[] spawnPoints = this.GetSpawnPoints();
			if (spawnPoints != null)
			{
				foreach (Transform transform in spawnPoints)
				{
					transform.gameObject.SetActive(false);
				}
			}
			this.UpdateMapBuildingStatus("Adding roads");
			yield return null;
			foreach (Prop prop in LevelEditorTools.PropsParent.GetComponentsInChildren<Prop>())
			{
				prop.BakeProp();
				foreach (Renderer renderer in prop.GetComponentsInChildren<Renderer>())
				{
					renderer.gameObject.isStatic = true;
				}
			}
			foreach (Renderer renderer2 in LevelEditorTools.ExtraObjectsParent.GetComponentsInChildren<Renderer>())
			{
				renderer2.gameObject.isStatic = true;
			}
			LevelEditorTools.PropsParent.gameObject.isStatic = true;
			LevelEditorTools.ExtraObjectsParent.gameObject.isStatic = true;
			SurfaceManager.Instance.FindWater();
			this.UpdateMapBuildingStatus("Adding mud");
			yield return null;
			LevelEditorTools.ApplyMudStamps(this.mudStamps, this.terrain);
			SurfaceManager.Instance.CreateMudTerrains(this.mudStamps);
			SurfaceManager.Instance.FindWater();
			CustomMapStashManager sm = CustomMapStashManager.Instance;
			if (sm != null)
			{
				sm.Initialize();
			}
			StaticBatchingUtility.Combine(LevelEditorTools.PropsParent.gameObject);
			StaticBatchingUtility.Combine(LevelEditorTools.ExtraObjectsParent.gameObject);
		}
		if (this.editScene)
		{
			editor.lastPlacedExtraObjectID = this.lastPlacedExtraObject;
			editor.ignoredExtraObjectIDs = this.ignoredExtraObjectIDs;
			editor.mapName = this.mapName;
			editor.mapDescription = this.mapDescription;
			editor.mapRating = this.mapRating;
			editor.mapUploaded = true;
			editor.mapVisible = this.mapVisible;
			editor.seed = this.seed;
			editor.terrainSize = (float)this.terrainSize;
			editor.bumpsStrength = this.bumpsStrength;
			editor.flatCenter = this.flatCenter;
			editor.treesDensity = this.treesDensity;
			editor.treesByEdgesOnly = this.treesByEdgesOnly;
			editor.waterEnabled = this.waterEnabled;
			editor.frozenWater = this.frozenWater;
			editor.waterHeight = this.waterHeight;
			editor.mainTextureID = this.mainTextureID;
			editor.usedExtraObjects = this.usedExtraObjects;
			editor.paths = this.paths;
			editor.stamps = this.stamps;
			editor.routes = new List<PlayerRoute>(LevelEditorTools.RoutesParent.GetComponentsInChildren<PlayerRoute>(true));
			editor.mudStamps = this.mudStamps;
			foreach (MudStamp mudStamp in this.mudStamps)
			{
				mudStamp.stampIndicator = LevelEditorTools.CreateMudIndicator(mudStamp, this.terrain);
			}
			editor.SelectMudStamp(-1);
			editor.mapID = GameState.mapToDownload;
			base.GetComponent<LevelEditorUI>().InitializeUI();
			if (editor.OnTerrainGenerated != null)
			{
				editor.OnTerrainGenerated();
			}
			if (editor.OnProcessingTerrainFinished != null)
			{
				editor.OnProcessingTerrainFinished();
			}
		}
		this.UpdateMapBuildingStatus(string.Empty);
		if (this.sceneLoadingWindow != null)
		{
			this.sceneLoadingWindow.SetActive(false);
		}
		float newTime = Time.realtimeSinceStartup;
		float diff = newTime - time;
		UnityEngine.Debug.Log("Level build time: " + diff);
		yield break;
	}

	private void UpdateMapBuildingStatus(string status)
	{
		if (this.mapBuildStatusText == null)
		{
			return;
		}
		this.mapBuildStatusText.text = status;
	}

	private void ApplyAllStamps()
	{
		foreach (TerrainStamp stamp in this.stamps)
		{
			this.ApplyStamp(stamp);
		}
	}

	private void ApplyStamp(TerrainStamp stamp)
	{
		switch (stamp.stampAction)
		{
		case ModAction.LandscapeRaising:
			this.ApplyHeightStamp(stamp);
			this.PaintRockAndWaterTextures();
			this.CorrectExtraObjectsTransforms();
			break;
		case ModAction.LandscapeLowering:
			this.ApplyHeightStamp(stamp);
			this.PaintRockAndWaterTextures();
			this.CorrectExtraObjectsTransforms();
			break;
		case ModAction.Smoothing:
			this.ApplySmoothStamp(stamp);
			this.PaintRockAndWaterTextures();
			this.CorrectExtraObjectsTransforms();
			break;
		case ModAction.Painting:
			this.ApplyPaintStamp(stamp);
			this.PaintRockAndWaterTextures();
			break;
		case ModAction.AddingExtraObjects:
			this.lastPlacedExtraObject = this.ApplyAddExtraObjectsStamp(stamp, ref this.treesAddedForPreview, ref this.extraObjectsAddedForPreview);
			break;
		case ModAction.RemovingExtraObjects:
			this.ApplyRemovingExtraObjectsStamp(stamp, ref this.treesRemovedForPreview, ref this.extraObjectsRemovedForPreview, false);
			break;
		}
	}

	private void ApplyAllPaths()
	{
		foreach (TerrainPath path in this.paths)
		{
			this.ApplyPath(path);
		}
	}

	private void ApplyPath(TerrainPath path)
	{
		switch (path.pathAction)
		{
		case ModAction.LandscapeRaising:
			this.ApplyHeightPath(path);
			this.PaintRockAndWaterTextures();
			this.CorrectExtraObjectsTransforms();
			break;
		case ModAction.LandscapeLowering:
			this.ApplyHeightPath(path);
			this.PaintRockAndWaterTextures();
			this.CorrectExtraObjectsTransforms();
			break;
		case ModAction.Smoothing:
			this.ApplySmoothPath(path);
			this.PaintRockAndWaterTextures();
			this.CorrectExtraObjectsTransforms();
			break;
		case ModAction.Painting:
			this.ApplyPaintPath(path);
			this.PaintRockAndWaterTextures();
			break;
		case ModAction.AddingExtraObjects:
			this.lastPlacedExtraObject = this.ApplyAddExtraObjectsPath(path, ref this.treesAddedForPreview, ref this.extraObjectsAddedForPreview);
			break;
		case ModAction.RemovingExtraObjects:
			this.ApplyRemovingExtraObjectsPath(path, ref this.treesRemovedForPreview, ref this.extraObjectsRemovedForPreview, false);
			break;
		}
	}

	private void PlaceProps()
	{
		UnityEngine.Object.DestroyImmediate(LevelEditorTools.PropsParent.gameObject);
		for (int i = 0; i < this.propsStrings.Count; i++)
		{
			string text = this.propsStrings[i];
			string[] array = text.Split(new char[]
			{
				'|'
			});
			int num = int.Parse(array[0]);
			Prop component = UnityEngine.Object.Instantiate<GameObject>(this.editorResources.propsDictionary[num].gameObject).GetComponent<Prop>();
			component.Initialize();
			component.Deserialize(text);
			component.Highlight(false);
			component.transform.parent = LevelEditorTools.PropsParent;
		}
	}

	private void PlaceRoutes()
	{
		UnityEngine.Object.DestroyImmediate(LevelEditorTools.RoutesParent.gameObject);
		List<PlayerRoute> list = new List<PlayerRoute>();
		for (int i = 0; i < this.routeStrings.Count; i++)
		{
			PlayerRoute playerRoute = new GameObject("Route").AddComponent<PlayerRoute>();
			playerRoute.transform.parent = LevelEditorTools.RoutesParent;
			playerRoute.transform.position = Vector3.zero;
			playerRoute.InitializeLineRenderer(this.editorResources.routeMaterial);
			playerRoute.Deserialize(this.routeStrings[i]);
			playerRoute.UpdateCheckpointPrefabs();
			playerRoute.UpdateLineRenderer();
			playerRoute.BakeRoute();
			list.Add(playerRoute);
		}
	}

	public Transform[] GetSpawnPoints()
	{
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < LevelEditorTools.PropsParent.childCount; i++)
		{
			if (LevelEditorTools.PropsParent.GetChild(i).name.Contains("SpawnPoint"))
			{
				list.Add(LevelEditorTools.PropsParent.GetChild(i));
			}
		}
		return list.ToArray();
	}

	private void PaintBaseTexture()
	{
		LevelEditorTools.PaintBaseTexture(this.terrain, this.mainTextureID);
	}

	private void PaintRockAndWaterTextures()
	{
		LevelEditorTools.PaintRockAndWaterTextures(this.terrain, this.waterPlane, this.waterEnabled, this.frozenWater, this.editorResources.underwaterTextureID, this.editorResources.rockTextureID, this.editorResources.minRockAngle, this.editorResources.maxRockAngle, this.mainTextureID);
	}

	private void ApplyHeightStamp(TerrainStamp stamp)
	{
		LevelEditorTools.ApplyHeightStamp(this.terrain, stamp, this.defHeights, this.editorResources.stampTextures);
	}

	private void ApplySmoothStamp(TerrainStamp stamp)
	{
		LevelEditorTools.ApplySmoothStamp(this.terrain, stamp, this.editorResources.stampTextures);
	}

	private void ApplyPaintStamp(TerrainStamp stamp)
	{
		LevelEditorTools.ApplyPaintStamp(this.terrain, this.editorResources.stampTextures[stamp.stampTextureID], stamp.stampRotation, stamp.stampPosition, stamp.stampSize, stamp.extraInt);
	}

	private void ApplyRemovingExtraObjectsStamp(TerrainStamp stamp, ref List<TreeInstance> removedTrees, ref List<GameObject> removedExtraObjects, bool previewOnly)
	{
		LevelEditorTools.ApplyRemoveExtraObjectsStamp(this.terrain, stamp, this.seed, this.editorResources.stampTextures, ref removedTrees, ref removedExtraObjects, previewOnly);
	}

	private int ApplyAddExtraObjectsStamp(TerrainStamp stamp, ref List<TreeInstance> addedTrees, ref List<GameObject> addedExtraObjects)
	{
		return LevelEditorTools.ApplyAddExtraObjectsStamp(this.terrain, this.waterPlane, stamp, this.editorResources.stampTextures, this.seed, this.editorResources.extraObjectsDictionary, (float)this.terrainSize, this.waterEnabled, ref addedTrees, ref addedExtraObjects, this.lastPlacedExtraObject);
	}

	private void ApplyHeightPath(TerrainPath path)
	{
		LevelEditorTools.ApplyHeightPath(this.terrain, path, this.defHeights, this.editorResources.pathPatterns);
	}

	private void ApplySmoothPath(TerrainPath path)
	{
		LevelEditorTools.ApplySmoothPath(this.terrain, path, this.editorResources.pathPatterns);
	}

	private void ApplyPaintPath(TerrainPath path)
	{
		LevelEditorTools.ApplyPaintPath(this.terrain, path, this.editorResources.pathPatterns);
	}

	private void ApplyRemovingExtraObjectsPath(TerrainPath path, ref List<TreeInstance> removedTrees, ref List<GameObject> removedExtraObjects, bool previewOnly)
	{
		LevelEditorTools.ApplyRemoveExtraObjectsPath(this.terrain, path, this.seed, this.editorResources.pathPatterns, ref removedTrees, ref removedExtraObjects, previewOnly);
	}

	private int ApplyAddExtraObjectsPath(TerrainPath path, ref List<TreeInstance> addedTrees, ref List<GameObject> addedExtraObjects)
	{
		return LevelEditorTools.ApplyAddExtraObjectsPath(this.terrain, this.waterPlane, path, this.editorResources.pathPatterns, this.seed, this.editorResources.extraObjectsDictionary, (float)this.terrainSize, this.waterEnabled, ref addedTrees, ref addedExtraObjects, this.lastPlacedExtraObject);
	}

	private void CorrectExtraObjectsTransforms()
	{
		LevelEditorTools.CorrectExtraObjectsTransforms(this.terrain, this.seed);
	}

	private void CorrectCliffsPositions()
	{
		LevelEditorTools.CorrectCliffsPositions(this.terrain);
	}

	private LevelEditorResources editorResources;

	private string mapName;

	private string mapDescription;

	private int mapRating;

	private bool mapUploaded;

	private bool mapVisible;

	private int seed;

	private int terrainSize;

	private float bumpsStrength;

	private bool flatCenter;

	private float treesDensity;

	private bool treesByEdgesOnly;

	private bool waterEnabled;

	private bool frozenWater;

	private float waterHeight;

	private int mainTextureID;

	private int lastPlacedExtraObject;

	private List<int> ignoredExtraObjectIDs = new List<int>();

	private ExtraObjectReference[] usedExtraObjects;

	private List<TerrainStamp> stamps;

	private List<MudStamp> mudStamps = new List<MudStamp>();

	private List<TerrainPath> paths;

	private List<string> propsStrings;

	private List<string> routeStrings;

	private List<TreeInstance> treesAddedForPreview = new List<TreeInstance>();

	private List<TreeInstance> treesRemovedForPreview = new List<TreeInstance>();

	private List<GameObject> extraObjectsAddedForPreview = new List<GameObject>();

	private List<GameObject> extraObjectsRemovedForPreview = new List<GameObject>();

	private float[,] defHeights;

	private Transform waterPlane;

	public bool playingScene;

	public bool editScene;

	public Text mapBuildStatusText;

	public GameObject sceneLoadingWindow;

	private Coroutine levelBuildingCoroutine;

	private bool newApproach = true;

	public struct TextureData
	{
		public Color[] colors;
	}
}
