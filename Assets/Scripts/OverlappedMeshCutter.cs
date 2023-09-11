using System;
using System.Collections.Generic;
using Battlehub.Integration;
using Battlehub.MeshTools;
using UnityEngine;

public class OverlappedMeshCutter : MonoBehaviour
{
	[ContextMenu("Cut overlapping meshes")]
	public void CutOverlappingMeshes()
	{
		if (this.GameobjectsToProcess == null || this.GameobjectsToProcess.Length == 0)
		{
			UnityEngine.Debug.LogError("Fill -GameObjects to process- array");
			return;
		}
		CutObject[] array = new CutObject[this.GameobjectsToProcess.Length];
		for (int i = 0; i < this.GameobjectsToProcess.Length; i++)
		{
			array[i] = new CutObject();
			array[i].GO = this.GameobjectsToProcess[i];
			array[i].meshFilter = this.GameobjectsToProcess[i].GetComponent<MeshFilter>();
			array[i].meshCollider = array[i].GO.AddComponent<MeshCollider>();
			array[i].meshCollider.convex = true;
			if (array[i].meshCollider.bounds.size == Vector3.zero)
			{
				array[i].meshCollider.inflateMesh = true;
			}
		}
		for (int j = 0; j < this.GameobjectsToProcess.Length; j++)
		{
			for (int k = 0; k < this.GameobjectsToProcess.Length; k++)
			{
				if (k != j && array[j].meshCollider.bounds.Intersects(array[k].meshCollider.bounds))
				{
					this.CutMesh(array[j].meshFilter, array[k].meshCollider);
				}
			}
		}
		for (int l = 0; l < this.GameobjectsToProcess.Length; l++)
		{
			UnityEngine.Object.DestroyImmediate(array[l].meshCollider);
		}
	}

	public void CutMeshesUnderTerrains()
	{
		if (this.GameobjectsToProcess == null || this.GameobjectsToProcess.Length == 0)
		{
			UnityEngine.Debug.LogError("Fill -GameObjects to process- array");
			return;
		}
		CutObject[] array = new CutObject[this.GameobjectsToProcess.Length];
		for (int i = 0; i < this.GameobjectsToProcess.Length; i++)
		{
			array[i] = new CutObject();
			if (this.GameobjectsToProcess[i] == null)
			{
				UnityEngine.Debug.LogError("One of objects in -Gameobjects To Process- array is null");
				return;
			}
			array[i].GO = this.GameobjectsToProcess[i];
			array[i].meshFilter = this.GameobjectsToProcess[i].GetComponent<MeshFilter>();
			array[i].meshCollider = array[i].GO.AddComponent<MeshCollider>();
			array[i].meshCollider.convex = true;
			if (array[i].meshCollider.bounds.size == Vector3.zero)
			{
				array[i].meshCollider.inflateMesh = true;
			}
		}
		Terrain[] activeTerrains = Terrain.activeTerrains;
		if (activeTerrains.Length == 0)
		{
			UnityEngine.Debug.LogError("No terrains in scene");
			return;
		}
		for (int j = 0; j < array.Length; j++)
		{
			foreach (Terrain terrain in activeTerrains)
			{
				Vector3 position = terrain.transform.position;
				Vector3 position2 = array[j].GO.transform.position;
				if (position2.x > position.x && position2.z > position.z && position2.x < position.x + terrain.terrainData.size.x && position2.z < position.z + terrain.terrainData.size.z)
				{
					this.currentTerrain = terrain;
				}
			}
			if (this.currentTerrain != null)
			{
				this.CutMeshByHeight(array[j].meshFilter, this.currentTerrain);
			}
		}
		for (int l = 0; l < this.GameobjectsToProcess.Length; l++)
		{
			UnityEngine.Object.DestroyImmediate(array[l].meshCollider);
		}
	}

	private void CutMeshByHeight(MeshFilter meshFilterToBeCut, Terrain terrain)
	{
		Mesh mesh = meshFilterToBeCut.mesh;
		int[] triangles = mesh.triangles;
		Vector3[] vertices = mesh.vertices;
		List<int> list = new List<int>();
		for (int i = 0; i < triangles.Length; i += 3)
		{
			Vector3 worldPosition = meshFilterToBeCut.transform.TransformPoint(vertices[triangles[i]]);
			Vector3 worldPosition2 = meshFilterToBeCut.transform.TransformPoint(vertices[triangles[i + 1]]);
			Vector3 worldPosition3 = meshFilterToBeCut.transform.TransformPoint(vertices[triangles[i + 2]]);
			if (worldPosition.y > terrain.SampleHeight(worldPosition) || worldPosition2.y > terrain.SampleHeight(worldPosition2) || worldPosition3.y > terrain.SampleHeight(worldPosition3))
			{
				list.Add(triangles[i]);
				list.Add(triangles[i + 1]);
				list.Add(triangles[i + 2]);
			}
		}
		mesh.triangles = list.ToArray();
	}

	private void CutMesh(MeshFilter meshFilterToBeCut, MeshCollider meshColliderThatOverlaps)
	{
		Mesh mesh = meshFilterToBeCut.mesh;
		int[] triangles = mesh.triangles;
		Vector3[] vertices = mesh.vertices;
		List<int> list = new List<int>();
		for (int i = 0; i < triangles.Length; i += 3)
		{
			Vector3 vector = meshFilterToBeCut.transform.TransformPoint(vertices[triangles[i]]);
			Vector3 vector2 = meshFilterToBeCut.transform.TransformPoint(vertices[triangles[i + 1]]);
			Vector3 vector3 = meshFilterToBeCut.transform.TransformPoint(vertices[triangles[i + 2]]);
			if (Vector3.Distance(meshColliderThatOverlaps.ClosestPoint(vector), vector) > 0.01f || Vector3.Distance(meshColliderThatOverlaps.ClosestPoint(vector2), vector2) > 0.01f || Vector3.Distance(meshColliderThatOverlaps.ClosestPoint(vector3), vector3) > 0.01f)
			{
				list.Add(triangles[i]);
				list.Add(triangles[i + 1]);
				list.Add(triangles[i + 2]);
			}
		}
		mesh.triangles = list.ToArray();
	}

	public void MergeMeshes()
	{
		foreach (GameObject x in this.GameobjectsToProcess)
		{
			if (x == null)
			{
				UnityEngine.Debug.LogError("One of objects in -Gameobjects To Process- array is null");
				return;
			}
		}
		CombineResult combineResult = MeshUtils.Combine(this.GameobjectsToProcess, null);
		if (combineResult != null)
		{
			MeshCombinerIntegration.RaiseCombined(combineResult.GameObject, combineResult.Mesh);
		}
	}

	public GameObject[] GameobjectsToProcess;

	private Terrain currentTerrain;
}
