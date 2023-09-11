using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPlacer : MonoBehaviour
{
	public void PlaceObjects()
	{
		Terrain activeTerrain = Terrain.activeTerrain;
		if (activeTerrain == null)
		{
			UnityEngine.Debug.LogError("No terrain found!");
			return;
		}
		GameObject gameObject = GameObject.Find("_ObjectsPlacer");
		if (gameObject != null)
		{
			UnityEngine.Object.DestroyImmediate(gameObject);
		}
		gameObject = new GameObject("_ObjectsPlacer");
		string text = "Placed: \n";
		foreach (ObjectGroup objectGroup in this.objectGroups)
		{
			if (objectGroup.prefab == null)
			{
				UnityEngine.Debug.LogError("Assign prefab!");
				return;
			}
			Transform transform = new GameObject(objectGroup.prefab.name).transform;
			transform.parent = gameObject.transform;
			List<Vector3> list = new List<Vector3>();
			int num = 0;
			int num2 = 0;
			while ((float)num2 < objectGroup.count)
			{
				bool flag = false;
				int num3 = 0;
				do
				{
					num3++;
					Vector3 vector = this.RandomTerrainPoint(activeTerrain);
					flag = true;
					foreach (Vector3 a in list)
					{
						if (Vector3.Distance(a, vector) < objectGroup.minDistanceInterval)
						{
							flag = false;
						}
					}
					if (flag && objectGroup.terrainTextures != null && objectGroup.terrainTextures.Length > 0)
					{
						Texture2D mainTextureAtPosition = this.GetMainTextureAtPosition(activeTerrain, vector);
						foreach (Texture2D texture2D in objectGroup.terrainTextures)
						{
							if (!mainTextureAtPosition.name.Equals(texture2D.name))
							{
								flag = false;
								break;
							}
						}
					}
					if (flag)
					{
						float steepnessAngle = this.GetSteepnessAngle(activeTerrain, vector);
						if (objectGroup.minSteepness > steepnessAngle)
						{
							flag = false;
						}
					}
					if (flag)
					{
						Vector3 vector2 = activeTerrain.GetPosition() + Vector3.up * objectGroup.minHeight * activeTerrain.terrainData.size.y;
						Vector3 vector3 = activeTerrain.GetPosition() + Vector3.up * objectGroup.maxHeight * activeTerrain.terrainData.size.y;
						if (vector.y < vector2.y || vector.y > vector3.y)
						{
							flag = false;
						}
					}
					if (flag)
					{
						float x = (float)((!objectGroup.randomXRotation) ? 0 : UnityEngine.Random.Range(0, 360));
						float y = (float)((!objectGroup.randomYRotation) ? 0 : UnityEngine.Random.Range(0, 360));
						float z = (float)((!objectGroup.randomZRotation) ? 0 : UnityEngine.Random.Range(0, 360));
						Quaternion rotation = Quaternion.Euler(x, y, z);
						if (objectGroup.alignByNormal)
						{
							rotation = Quaternion.LookRotation(this.GetNormalAtPosition(activeTerrain, vector));
						}
						GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(objectGroup.prefab, vector + Vector3.up * objectGroup.heightOffset, rotation);
						gameObject2.transform.parent = transform;
						list.Add(vector);
						num++;
					}
				}
				while (!flag && num3 < 1000);
				num2++;
			}
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				objectGroup.prefab.name,
				": ",
				num,
				"/",
				objectGroup.count,
				"\n"
			});
		}
		UnityEngine.Debug.Log(text);
	}

	private Vector3 GetNormalAtPosition(Terrain terrain, Vector3 pos)
	{
		float x = (pos.x - terrain.GetPosition().x) / terrain.terrainData.size.x;
		float y = (pos.z - terrain.GetPosition().z) / terrain.terrainData.size.z;
		return terrain.terrainData.GetInterpolatedNormal(x, y);
	}

	private float GetSteepnessAngle(Terrain terrain, Vector3 position)
	{
		Vector3 normalAtPosition = this.GetNormalAtPosition(terrain, position);
		Vector3 to = Vector3.ProjectOnPlane(normalAtPosition, Vector3.up);
		return 90f - Vector3.Angle(normalAtPosition, to);
	}

	private Texture2D GetMainTextureAtPosition(Terrain terrain, Vector3 position)
	{
		Vector3Int splatMapCoords = this.GetSplatMapCoords(position, terrain);
		float[,,] alphamaps = terrain.terrainData.GetAlphamaps(splatMapCoords.x, splatMapCoords.z, 1, 1);
		float[] array = new float[alphamaps.GetUpperBound(2) + 1];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = alphamaps[0, 0, i];
		}
		int num = 0;
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j] > array[num])
			{
				num = j;
			}
		}
		SplatPrototype splatPrototype = terrain.terrainData.splatPrototypes[num];
		return splatPrototype.texture;
	}

	private Vector3Int GetSplatMapCoords(Vector3 worldPos, Terrain terrain)
	{
		Vector3Int zero = Vector3Int.zero;
		zero.x = (int)((worldPos.x - terrain.GetPosition().x) / terrain.terrainData.size.x * (float)terrain.terrainData.alphamapWidth);
		zero.z = (int)((worldPos.z - terrain.GetPosition().z) / terrain.terrainData.size.z * (float)terrain.terrainData.alphamapHeight);
		zero.x = Mathf.Clamp(zero.x, 0, terrain.terrainData.alphamapWidth);
		zero.z = Mathf.Clamp(zero.z, 0, terrain.terrainData.alphamapHeight);
		return zero;
	}

	private Vector3 RandomTerrainPoint(Terrain terrain)
	{
		Vector3 position = terrain.GetPosition();
		float x = terrain.terrainData.size.x;
		float z = terrain.terrainData.size.z;
		float x2 = UnityEngine.Random.Range(position.x, position.x + x);
		float z2 = UnityEngine.Random.Range(position.z, position.z + z);
		float y = terrain.SampleHeight(new Vector3(x2, 0f, z2));
		return new Vector3(x2, y, z2);
	}

	private Vector3 TerrainCenter(Terrain terrain)
	{
		Vector3 position = terrain.GetPosition();
		return position + new Vector3(terrain.terrainData.size.z / 2f, 0f, terrain.terrainData.size.z / 2f);
	}

	private void OnValidate()
	{
		foreach (ObjectGroup objectGroup in this.objectGroups)
		{
			if (objectGroup.prefab != null)
			{
				objectGroup.name = objectGroup.prefab.name;
			}
			else
			{
				objectGroup.name = "Element";
			}
			if (objectGroup.minHeight == 0f && objectGroup.maxHeight == 0f)
			{
				objectGroup.maxHeight = 1f;
			}
			if (objectGroup.maxHeight <= objectGroup.minHeight)
			{
				objectGroup.maxHeight = objectGroup.minHeight + 0.01f;
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (Application.isPlaying)
		{
			return;
		}
		Terrain activeTerrain = Terrain.activeTerrain;
		if (activeTerrain == null)
		{
			return;
		}
		foreach (ObjectGroup objectGroup in this.objectGroups)
		{
			if (objectGroup.displayHeightPlanes)
			{
				Vector3 size = activeTerrain.terrainData.size;
				size.y = 1f;
				Vector3 center = this.TerrainCenter(activeTerrain) + Vector3.up * objectGroup.minHeight * activeTerrain.terrainData.size.y;
				Color color = Color.green;
				color.a = 0.6f;
				Gizmos.color = color;
				Gizmos.DrawCube(center, size);
				Vector3 center2 = this.TerrainCenter(activeTerrain) + Vector3.up * objectGroup.maxHeight * activeTerrain.terrainData.size.y;
				color = Color.red;
				color.a = 0.6f;
				Gizmos.color = color;
				Gizmos.DrawCube(center2, size);
			}
		}
	}

	public ObjectGroup[] objectGroups;
}
