using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CommunityMapsMenu : MonoBehaviour
{
	private LevelWebHandler webHandler
	{
		get
		{
			LevelWebHandler levelWebHandler = base.GetComponent<LevelWebHandler>();
			if (levelWebHandler == null)
			{
				levelWebHandler = base.gameObject.AddComponent<LevelWebHandler>();
			}
			return levelWebHandler;
		}
	}

	private void OnEnable()
	{
		this.selectedMapCategory = 4;
		this.raising = false;
		this.lastSortType = -1;
		this.currentSortType = SortType.None;
		this.searchField.text = string.Empty;
		this.loadingMaps = false;
		this.currentFavsListString = string.Empty;
		this.allMapsButton.color = this.selectedButtonColor;
		this.favMapsButton.color = this.deselectedButtonColor;
		this.newestMapsButton.color = this.deselectedButtonColor;
		this.myMapsButton.color = this.deselectedButtonColor;
		this.featuredMapsButton.color = this.deselectedButtonColor;
		this.regularMapsParent.SetActive(true);
		this.featuredMapsParent.SetActive(false);
		this.UpdateTriangles();
		this.ShowMapCategory(4);
	}

	public void LoadMapsPage(int page, SortType sortType, string searchString, string favMapsList)
	{
		if (this.loadingMaps)
		{
			return;
		}
		this.currentPage = page;
		this.regularMapsParent.SetActive(this.selectedMapCategory != 4);
		this.featuredMapsParent.SetActive(this.selectedMapCategory == 4);
		if (page == 0)
		{
			this.mapsLoadingRegularObject.SetActive(this.selectedMapCategory != 4);
			this.mapsLoadingFeaturedObject.SetActive(this.selectedMapCategory == 4);
			this.loadingMoreMapsMessage.SetActive(false);
			this.exampleFeaturedMapElement.gameObject.SetActive(false);
			this.exampleRegularMapElement.gameObject.SetActive(false);
			this.playButton.SetActive(false);
			this.editMapButton.SetActive(false);
			foreach (MapElement mapElement in this.mapElements)
			{
				UnityEngine.Object.Destroy(mapElement.gameObject);
			}
			this.mapElements.Clear();
		}
		else
		{
			this.loadingMoreMapsMessage.SetActive(true);
		}
		this.pullToLoadMessage.SetActive(false);
		this.nothingFoundFeaturedObject.SetActive(false);
		this.nothingFoundRegularObject.SetActive(false);
		int from = page * this.mapsPerPage;
		this.loadingMaps = true;
		int count = this.mapsPerPage;
		bool includeHidden = false;
		if (this.selectedMapCategory == 3 || this.selectedMapCategory == 4)
		{
			count = 1000;
			includeHidden = (this.selectedMapCategory == 3);
		}
		this.webHandler.DownloadLevelsMetadatasPage(from, count, sortType, searchString, favMapsList, includeHidden, new Action<string>(this.OnLevelsMetasDownloaded), new Action<string>(this.OnLevelListLoadError));
	}

	[ContextMenu("Get all maps")]
	private void GetAllMaps()
	{
		base.StartCoroutine(this.GetAllMapsCor());
	}

	private IEnumerator GetAllMapsCor()
	{
		WWW w = new WWW("https://keereedev.000webhostapp.com/GetMaps.php?meta&from=0&count=200000&sortType=None&search=&favsList=&includeHidden=0");
		yield return w;
		this.mapMetas = w.text.Split(new char[]
		{
			'\n'
		});
		for (int i = 0; i < this.mapMetas.Length; i++)
		{
			if (Utility.HasBadWord(this.mapMetas[i]))
			{
				UnityEngine.Debug.Log(this.mapMetas[i]);
			}
		}
		yield break;
	}

	private void OnLevelsMetasDownloaded(string rawData)
	{
		string[] array = rawData.Split(new char[]
		{
			'\n'
		});
		this.mapsLoadingFeaturedObject.SetActive(false);
		this.mapsLoadingRegularObject.SetActive(false);
		UnityEngine.Debug.Log(rawData);
		this.totalMapsCount = int.Parse(array[array.Length - 1]);
		MapElement mapElement = (this.selectedMapCategory != 4) ? this.exampleRegularMapElement : this.exampleFeaturedMapElement;
		for (int i = 0; i < array.Length - 1; i++)
		{
			MapElement component = UnityEngine.Object.Instantiate<GameObject>(mapElement.gameObject, mapElement.transform.parent).GetComponent<MapElement>();
			string[] array2 = array[i].Split(new char[]
			{
				'|'
			});
			string text = array2[0];
			string text2 = array2[1];
			string text3 = array2[2];
			int rating = int.Parse(array2[3]);
			bool flag = bool.Parse(array2[4]);
			string text4 = array2[5];
			if (!flag && !LevelEditorTools.IsMapMadeMyMe(text))
			{
				this.totalMapsCount--;
			}
			else
			{
				component.mapFileName = text;
				component.mapNameText.text = text2;
				component.mapDescriptionText.text = text3;
				component.mapRatingText.text = rating.ToString();
				component.rating = rating;
				if (component.rating > 0)
				{
					component.mapRatingText.color = Color.green;
				}
				else if (component.rating < 0)
				{
					component.mapRatingText.color = Color.red;
				}
				else
				{
					component.mapRatingText.color = Color.white;
				}
				component.mapAuthorText.text = text4;
				component.gameObject.SetActive(true);
				component.ToggleStar(LevelEditorTools.IsMapInFavs(text));
				component.ToggleSelection(false, !flag);
				this.mapElements.Add(component);
				if (this.selectedMapCategory == 4)
				{
					this.webHandler.DownloadMapImage(text, new Action<Texture2D, string>(this.AssignMapImage));
				}
			}
		}
		this.loadingMoreMapsMessage.transform.SetAsLastSibling();
		this.loadingMoreMapsMessage.SetActive(false);
		this.pullToLoadMessage.transform.SetAsLastSibling();
		this.pullToLoadMessage.SetActive(this.mapElements.Count < this.totalMapsCount);
		this.nothingFoundRegularObject.SetActive(this.mapElements.Count == 0 && this.selectedMapCategory != 4);
		this.nothingFoundFeaturedObject.SetActive(this.mapElements.Count == 0 && this.selectedMapCategory == 4);
		this.loadingMaps = false;
	}

	private void AssignMapImage(Texture2D tex, string mapID)
	{
		foreach (MapElement mapElement in this.mapElements)
		{
			if (mapElement.mapFileName == mapID)
			{
				mapElement.SetMapPic(tex);
			}
		}
	}

	private void OnLevelListLoadError(string error)
	{
	}

	public void SelectMap(string mapID)
	{
		foreach (MapElement mapElement in this.mapElements)
		{
			mapElement.ToggleSelection(mapID == mapElement.mapFileName);
		}
		this.selectedMapID = mapID;
		this.playButton.SetActive(true);
		bool flag = false;
		this.editMapButton.SetActive(LevelEditorTools.IsMapMadeMyMe(mapID) || GameState.devRights || flag);
	}

	public void SearchMap(string text)
	{
		this.LoadMapsPage(0, this.currentSortType, text, this.currentFavsListString);
	}

	public void SortMaps(int parameter)
	{
		if (this.loadingMaps)
		{
			return;
		}
		if (this.lastSortType == parameter && this.raising)
		{
			this.raising = false;
		}
		else
		{
			this.raising = true;
		}
		SortType sortType = SortType.None;
		if (parameter == 1)
		{
			if (this.raising)
			{
				sortType = SortType.nameAsc;
			}
			else
			{
				sortType = SortType.nameDesc;
			}
		}
		if (parameter == 2)
		{
			if (this.raising)
			{
				sortType = SortType.ratingAsc;
			}
			else
			{
				sortType = SortType.ratingDesc;
			}
		}
		if (parameter == 3)
		{
			if (this.raising)
			{
				sortType = SortType.authorAsc;
			}
			else
			{
				sortType = SortType.authorDesc;
			}
		}
		this.currentSortType = sortType;
		this.LoadMapsPage(0, sortType, this.searchField.text, this.currentFavsListString);
		this.lastSortType = parameter;
		this.UpdateTriangles();
	}

	private void UpdateTriangles()
	{
		this.nameSortDownTriangle.SetActive(this.lastSortType != 1 || (this.lastSortType == 1 && this.raising));
		this.nameSortUpTriangle.SetActive(this.lastSortType == 1 && !this.raising);
		this.ratingSortDownTriangle.SetActive(this.lastSortType != 2 || (this.lastSortType == 2 && this.raising));
		this.ratingSortUpTriangle.SetActive(this.lastSortType == 2 && !this.raising);
		this.authorSortDownTriangle.SetActive(this.lastSortType != 3 || (this.lastSortType == 3 && this.raising));
		this.authorSortUpTriangle.SetActive(this.lastSortType == 3 && !this.raising);
	}

	public void ShowMapCategory(int cat)
	{
		if (this.loadingMaps)
		{
			return;
		}
		this.selectedMapCategory = cat;
		this.allMapsButton.color = ((cat != 0) ? this.deselectedButtonColor : this.selectedButtonColor);
		this.newestMapsButton.color = ((cat != 1) ? this.deselectedButtonColor : this.selectedButtonColor);
		this.favMapsButton.color = ((cat != 2) ? this.deselectedButtonColor : this.selectedButtonColor);
		this.myMapsButton.color = ((cat != 3) ? this.deselectedButtonColor : this.selectedButtonColor);
		this.featuredMapsButton.color = ((cat != 4) ? this.deselectedButtonColor : this.selectedButtonColor);
		this.currentFavsListString = string.Empty;
		if (cat == 0 && (this.currentSortType == SortType.newest || this.currentSortType == SortType.featured))
		{
			this.currentSortType = SortType.None;
		}
		if (cat == 1)
		{
			this.currentSortType = SortType.newest;
			this.lastSortType = -1;
		}
		if (cat == 2)
		{
			if (this.currentSortType == SortType.featured)
			{
				this.currentSortType = SortType.None;
			}
			this.currentFavsListString = "blank999";
			if (LevelEditorTools.FavMapsList != null)
			{
				foreach (string str in LevelEditorTools.FavMapsList)
				{
					this.currentFavsListString = this.currentFavsListString + str + "999";
				}
			}
		}
		if (cat == 3)
		{
			if (this.currentSortType == SortType.featured)
			{
				this.currentSortType = SortType.None;
			}
			this.currentFavsListString = "blank999";
			if (LevelEditorTools.MyMapsList != null)
			{
				foreach (string str2 in LevelEditorTools.MyMapsList)
				{
					this.currentFavsListString = this.currentFavsListString + str2 + "999";
				}
			}
		}
		if (cat == 4)
		{
			this.currentSortType = SortType.featured;
		}
		this.LoadMapsPage(0, this.currentSortType, this.searchField.text, this.currentFavsListString);
		this.UpdateTriangles();
	}

	public void LoadLevelEditor()
	{
		if (MenuManager.Instance.CurrentVehicle == null)
		{
			MenuManager.Instance.ShowMessage("Buy a vehicle first!", true);
			return;
		}
		StatsData statsData = GameState.LoadStatsData();
		if (!statsData.IsMember)
		{
			MenuManager.Instance.ShowBecomeMember();
			return;
		}
		GameState.GameMode = GameMode.SinglePlayer;
		GameState.mapToDownload = string.Empty;
		MenuManager.Instance.SceneLoadingText.text = "Loading level editor...";
		MenuManager.Instance.SceneLoading.SetActive(true);
		SceneManager.LoadScene("LevelEditor");
	}

	public void LoadCustomMap()
	{
		if (MenuManager.Instance.CurrentVehicle == null)
		{
			MenuManager.Instance.ShowMessage("Buy a vehicle first!", true);
			return;
		}
		GameState.mapToDownload = this.selectedMapID;
		GameState.SceneName = "CustomMap";
		if (GameState.GameMode == GameMode.Multiplayer)
		{
			base.StartCoroutine(this.LoadCustomMapInMultiplayer());
		}
		else
		{
			MenuManager.Instance.SceneLoadingText.text = "Loading community map...";
			MenuManager.Instance.SceneLoading.SetActive(true);
			SceneManager.LoadScene("CustomMap");
		}
	}

	public void EditMap()
	{
		if (MenuManager.Instance.CurrentVehicle == null)
		{
			MenuManager.Instance.ShowMessage("Buy a vehicle first!", true);
			return;
		}
		GameState.mapToDownload = this.selectedMapID;
		GameState.GameMode = GameMode.SinglePlayer;
		MenuManager.Instance.SceneLoadingText.text = "Loading level editor...";
		MenuManager.Instance.SceneLoading.SetActive(true);
		SceneManager.LoadScene("LevelEditor");
	}

	private IEnumerator LoadCustomMapInMultiplayer()
	{
		MenuManager.Instance.SceneLoadingText.text = "Loading trailer...";
		MenuManager.Instance.SceneLoading.SetActive(true);
		if (!PhotonNetwork.connectedAndReady || !PhotonNetwork.insideLobby || PhotonNetwork.inRoom)
		{
			int num = 0;
			if (!PhotonNetwork.connectedAndReady)
			{
				num = 1;
			}
			if (!PhotonNetwork.insideLobby)
			{
				num = 2;
			}
			if (PhotonNetwork.inRoom)
			{
				num = 3;
			}
			MenuManager.Instance.ShowMessage("Multiplayer isn't ready yet. Please try again in a moment. Make sure you have Internet access! (" + num.ToString() + ")", true);
			MenuManager.Instance.SceneLoading.SetActive(false);
			yield break;
		}
		MultiplayerManager.JoinRoom();
		for (float time = 0f; time < 10f; time += 1f)
		{
			yield return new WaitForSeconds(1f);
		}
		if (!PhotonNetwork.inRoom)
		{
			MenuManager.Instance.ShowMessage("Can't connect to the room. Try again", true);
		}
		yield break;
	}

	public void AddMapToFavs(string mapID)
	{
		LevelEditorTools.AddMapToFavs(mapID);
	}

	public void RemoveFromFavs(string mapID)
	{
		LevelEditorTools.RemoveFromFavs(mapID);
	}

	private void Update()
	{
		if (!this.loadingMaps && this.pullToLoadMessage.gameObject.activeSelf && this.pullToLoadMessage.transform.position.y > 100f)
		{
			this.LoadMapsPage(this.currentPage + 1, this.currentSortType, this.searchField.text, this.currentFavsListString);
		}
	}

	public GameObject mapsLoadingRegularObject;

	public GameObject mapsLoadingFeaturedObject;

	public GameObject nothingFoundRegularObject;

	public GameObject nothingFoundFeaturedObject;

	public GameObject pullToLoadMessage;

	public GameObject loadingMoreMapsMessage;

	public MapElement exampleRegularMapElement;

	public MapElement exampleFeaturedMapElement;

	public GameObject playButton;

	public GameObject editMapButton;

	public InputField searchField;

	public GameObject nameSortUpTriangle;

	public GameObject nameSortDownTriangle;

	public GameObject ratingSortUpTriangle;

	public GameObject ratingSortDownTriangle;

	public GameObject authorSortUpTriangle;

	public GameObject authorSortDownTriangle;

	public Image allMapsButton;

	public Image newestMapsButton;

	public Image favMapsButton;

	public Image myMapsButton;

	public Image featuredMapsButton;

	public Color selectedButtonColor;

	public Color deselectedButtonColor;

	public Color hiddenButtonColor;

	public GameObject regularMapsParent;

	public GameObject featuredMapsParent;

	public int mapsPerPage = 20;

	private List<MapElement> mapElements = new List<MapElement>();

	private SortType currentSortType;

	private bool raising;

	private int lastSortType;

	private int selectedMapCategory;

	private string currentFavsListString;

	private string selectedMapID;

	private int currentMapCategory;

	private int currentPage;

	public int totalMapsCount;

	private bool loadingMaps;

	public string[] mapMetas;
}
