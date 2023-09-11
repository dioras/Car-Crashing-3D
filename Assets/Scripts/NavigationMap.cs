using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NavigationMap : MonoBehaviour
{
	private void Start()
	{
		Terrain[] activeTerrains = Terrain.activeTerrains;
		if (activeTerrains != null && activeTerrains.Length > 0)
		{
			for (int i = 0; i < activeTerrains.Length; i++)
			{
				if (i == 0)
				{
					this.terrain = activeTerrains[i];
				}
				else if (activeTerrains[i].terrainData.size.x > this.terrain.terrainData.size.x)
				{
					this.terrain = activeTerrains[i];
				}
			}
		}
		if (this.terrain == null)
		{
			UnityEngine.Debug.LogError("Terrain is not found");
			return;
		}
		this.terrainSide = this.terrain.terrainData.size.x;
		if (this.PlayerMarker != null)
		{
			this.PlayerMarker.GetComponent<Image>().color = this.PlayerMarkerColor;
		}
		Texture2D texture2D = (Texture2D)Resources.Load("Map/" + SceneManager.GetActiveScene().name + "_MapBackground");
		Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f));
		this.MapBackgroundImage.sprite = sprite;
		Texture2D texture2D2 = (Texture2D)Resources.Load("Map/" + SceneManager.GetActiveScene().name + "_MapElements");
		Sprite sprite2 = Sprite.Create(texture2D2, new Rect(0f, 0f, (float)texture2D2.width, (float)texture2D2.height), new Vector2(0.5f, 0.5f));
		this.MapElementsImage.sprite = sprite2;
	}

	private void Update()
	{
		if (this.PlayerCar == null)
		{
			this.FindPlayer();
		}
		else
		{
			this.AlignPlayerMarker();
		}
		if (this.OtherCarsMarkers != null)
		{
			if (this.OtherCarsMarkers.Length == this.OtherCars.Length)
			{
				this.AlignOtherCarsMarkers();
			}
			else
			{
				this.InitializeOtherCarsMarkers();
			}
		}
		else
		{
			this.InitializeOtherCarsMarkers();
		}
	}

	public void BakeMap()
	{
		Terrain[] activeTerrains = Terrain.activeTerrains;
		if (activeTerrains != null && activeTerrains.Length > 0)
		{
			for (int i = 0; i < activeTerrains.Length; i++)
			{
				if (i == 0)
				{
					this.terrain = activeTerrains[i];
				}
				else if (activeTerrains[i].terrainData.size.x > this.terrain.terrainData.size.x)
				{
					this.terrain = activeTerrains[i];
				}
			}
		}
		if (this.terrain == null)
		{
			UnityEngine.Debug.LogError("Terrain is not found");
			return;
		}
		this.terrainSide = this.terrain.terrainData.size.x;
		this.texture = new Texture2D(this.imageResolution, this.imageResolution);
		for (int j = 0; j < this.imageResolution; j++)
		{
			for (int k = 0; k < this.imageResolution; k++)
			{
				this.texture.SetPixel(j, k, Color.clear);
			}
		}
		Route[] array = UnityEngine.Object.FindObjectsOfType<Route>();
		for (int l = 0; l < array.Length; l++)
		{
			Vector3 position = array[l].Waypoints[0].position;
			this.DrawIcon(this.StartStampColor, position);
			Vector3 worldPos = array[l].Waypoints[array[l].Waypoints.Count - 1].position;
			if (array[l].Circuit)
			{
				worldPos = position;
			}
			this.DrawIcon(this.FinishStampColor, worldPos);
			for (int m = 0; m < array[l].Waypoints.Count; m++)
			{
				if (array[l].Waypoints.Count > m + 1)
				{
					this.DrawLine(array[l].RouteColor, array[l].Waypoints[m].position, array[l].Waypoints[m + 1].position);
				}
				else if (array[l].Circuit)
				{
					this.DrawLine(array[l].RouteColor, array[l].Waypoints[m].position, array[l].Waypoints[0].position);
				}
			}
		}
		this.texture.Apply();
		Sprite sprite = Sprite.Create(this.texture, new Rect(0f, 0f, (float)this.texture.width, (float)this.texture.height), new Vector2(0.5f, 0.5f));
		this.MapElementsImage.sprite = sprite;
		byte[] buffer = this.texture.EncodeToPNG();
		FileStream fileStream = File.Open(Application.dataPath + "/Resources/Map/" + SceneManager.GetActiveScene().name + "_MapElements.png", FileMode.Create);
		BinaryWriter binaryWriter = new BinaryWriter(fileStream);
		binaryWriter.Write(buffer);
		fileStream.Close();
	}

	private void DrawIcon(Color color, Vector3 WorldPos)
	{
		int x = this.GetTexCoords(WorldPos).x;
		int y = this.GetTexCoords(WorldPos).y;
		int num = x - this.EventStamp.width / 2;
		int num2 = y - this.EventStamp.height / 2;
		int num3 = x + this.EventStamp.width / 2;
		int num4 = y + this.EventStamp.height / 2;
		for (int i = num; i < num3; i++)
		{
			for (int j = num2; j < num4; j++)
			{
				int x2 = i - x + this.EventStamp.width / 2;
				int y2 = j - y + this.EventStamp.height / 2;
				this.texture.SetPixel(i, j, this.EventStamp.GetPixel(x2, y2) * color);
			}
		}
	}

	private void DrawLine(Color color, Vector3 A, Vector3 B)
	{
		Vector2Int texCoords = this.GetTexCoords(A);
		Vector2Int texCoords2 = this.GetTexCoords(B);
		this.DrawBresenhamsAlghorytmLine(texCoords.x, texCoords.y, texCoords2.x, texCoords2.y, color);
	}

	public void DrawBresenhamsAlghorytmLine(int x, int y, int x2, int y2, Color color)
	{
		int num = x2 - x;
		int num2 = y2 - y;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		if (num < 0)
		{
			num3 = -1;
		}
		else if (num > 0)
		{
			num3 = 1;
		}
		if (num2 < 0)
		{
			num4 = -1;
		}
		else if (num2 > 0)
		{
			num4 = 1;
		}
		if (num < 0)
		{
			num5 = -1;
		}
		else if (num > 0)
		{
			num5 = 1;
		}
		int num7 = Mathf.Abs(num);
		int num8 = Mathf.Abs(num2);
		if (num7 <= num8)
		{
			num7 = Mathf.Abs(num2);
			num8 = Mathf.Abs(num);
			if (num2 < 0)
			{
				num6 = -1;
			}
			else if (num2 > 0)
			{
				num6 = 1;
			}
			num5 = 0;
		}
		int num9 = num7 >> 1;
		for (int i = 0; i <= num7; i++)
		{
			this.texture.SetPixel(x, y, color);
			num9 += num8;
			if (num9 >= num7)
			{
				num9 -= num7;
				x += num3;
				y += num4;
			}
			else
			{
				x += num5;
				y += num6;
			}
		}
	}

	private Vector2Int GetTexCoords(Vector3 WorldPos)
	{
		int x = (int)((WorldPos - this.terrain.transform.position).x / this.terrainSide * (float)this.imageResolution);
		int y = (int)((WorldPos - this.terrain.transform.position).z / this.terrainSide * (float)this.imageResolution);
		return new Vector2Int(x, y);
	}

	private void AlignPlayerMarker()
	{
		if (this.PlayerMarker == null)
		{
			return;
		}
		this.PlayerMarker.gameObject.SetActive(true);
		this.PlayerMarker.anchoredPosition = this.GetTexCoords(this.PlayerCar.position);
		this.PlayerMarker.eulerAngles = new Vector3(0f, 0f, -this.PlayerCar.eulerAngles.y + 90f);
	}

	private void AlignOtherCarsMarkers()
	{
		if (this.OtherCars == null || this.OtherCars.Length == 0)
		{
			return;
		}
		for (int i = 0; i < this.OtherCarsMarkers.Length; i++)
		{
			if (this.OtherCars[i] != null && this.OtherCarsMarkers[i] != null)
			{
				this.OtherCarsMarkers[i].anchoredPosition = this.GetTexCoords(this.OtherCars[i].position);
				this.OtherCarsMarkers[i].eulerAngles = new Vector3(0f, 0f, -this.OtherCars[i].eulerAngles.y + 90f);
			}
		}
	}

	private void InitializeOtherCarsMarkers()
	{
		if (this.OtherCarsMarkers != null)
		{
			for (int i = 0; i < this.OtherCarsMarkers.Length; i++)
			{
				UnityEngine.Object.DestroyImmediate(this.OtherCarsMarkers[i].gameObject);
			}
		}
		this.OtherCarsMarkers = new RectTransform[0];
		if (this.OtherCars == null || this.OtherCars.Length == 0 || this.PlayerMarker == null)
		{
			return;
		}
		this.OtherCarsMarkers = new RectTransform[this.OtherCars.Length];
		for (int j = 0; j < this.OtherCarsMarkers.Length; j++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.PlayerMarker.gameObject);
			gameObject.transform.parent = this.PlayerMarker.transform.parent;
			this.OtherCarsMarkers[j] = gameObject.GetComponent<RectTransform>();
			this.OtherCarsMarkers[j].GetComponent<Image>().color = this.OtherCarsMarkerColor;
			this.OtherCarsMarkers[j].gameObject.SetActive(true);
		}
	}

	private void FindPlayer()
	{
		if (this.PlayerMarker != null)
		{
			this.PlayerMarker.gameObject.SetActive(false);
		}
		if (VehicleLoader.Instance.playerVehicle != null)
		{
			this.PlayerCar = VehicleLoader.Instance.playerVehicle.transform;
		}
	}

	public Transform[] OtherCars;

	public Texture2D EventStamp;

	public RectTransform PlayerMarker;

	public Image MapBackgroundImage;

	public Image MapElementsImage;

	public Color StartStampColor;

	public Color FinishStampColor;

	public Color PlayerMarkerColor;

	public Color OtherCarsMarkerColor;

	private Terrain terrain;

	private int imageResolution = 400;

	private Texture2D texture;

	private Transform PlayerCar;

	private RectTransform[] OtherCarsMarkers;

	private float terrainSide;
}
