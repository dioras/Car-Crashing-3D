using System;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSettings : MonoBehaviour
{
	private void OnDrawGizmosSelected()
	{
		if (!this.HideLowerPartUnderTerrain)
		{
			return;
		}
		Vector3 size = Vector3.one * this.planeSize;
		size.y = 0.001f;
		Color red = Color.red;
		red.a = 0.5f;
		Gizmos.color = red;
		Gizmos.DrawCube(base.transform.position + new Vector3(0f, this.cullingPlaneHeight, 0f), size);
		MeshFilter componentInChildren = base.GetComponentInChildren<MeshFilter>();
		Vector3[] vertices = componentInChildren.sharedMesh.vertices;
		if (componentInChildren != null)
		{
			foreach (Vector3 vertice in vertices)
			{
				Axis axis = this.upAxis;
				if (axis != Axis.X)
				{
					if (axis != Axis.Y)
					{
						if (axis == Axis.Z)
						{
							if (this.RootLocalVerticePos(vertice, componentInChildren).z < this.RootLocalPlanePos().z)
							{
								Gizmos.DrawSphere(this.WorldVerticePos(vertice, componentInChildren), this.verticesSize);
							}
						}
					}
					else if (this.RootLocalVerticePos(vertice, componentInChildren).y < this.RootLocalPlanePos().y)
					{
						Gizmos.DrawSphere(this.WorldVerticePos(vertice, componentInChildren), this.verticesSize);
					}
				}
				else if (this.RootLocalVerticePos(vertice, componentInChildren).x < this.RootLocalPlanePos().x)
				{
					Gizmos.DrawSphere(this.WorldVerticePos(vertice, componentInChildren), this.verticesSize);
				}
			}
		}
	}

	[ContextMenu("Align myself")]
	public void Align()
	{
		if (this.alignByNormal)
		{
			LevelEditorTools.AlignByNormal(Terrain.activeTerrain, base.transform, null, null);
		}
		if (this.HideLowerPartUnderTerrain)
		{
			this.MakeAllVerticesBeUnderTerrain();
		}
		else
		{
			this.AlignByTerrainHeight();
		}
		this.ApplyVerticalOffset();
	}

	private void ApplyVerticalOffset()
	{
		base.transform.position += Vector3.up * this.verticalOffset;
	}

	private void MakeAllVerticesBeUnderTerrain()
	{
		if (this.randomX || this.randomZ)
		{
			return;
		}
		Terrain activeTerrain = Terrain.activeTerrain;
		base.transform.position += Vector3.up * 10f;
		MeshFilter componentInChildren = base.GetComponentInChildren<MeshFilter>();
		Mesh sharedMesh = componentInChildren.sharedMesh;
		List<int> list = new List<int>();
		Vector3[] vertices = sharedMesh.vertices;
		for (int i = 0; i < vertices.Length; i++)
		{
			Axis axis = this.upAxis;
			if (axis != Axis.X)
			{
				if (axis != Axis.Y)
				{
					if (axis == Axis.Z)
					{
						if (this.RootLocalVerticePos(vertices[i], componentInChildren).z < this.RootLocalPlanePos().z)
						{
							list.Add(i);
						}
					}
				}
				else if (this.RootLocalVerticePos(vertices[i], componentInChildren).y < this.RootLocalPlanePos().y)
				{
					list.Add(i);
				}
			}
			else if (this.RootLocalVerticePos(vertices[i], componentInChildren).x < this.RootLocalPlanePos().x)
			{
				list.Add(i);
			}
		}
		int num = 0;
		if (!this.AreAllVerticesUnderTerrain(componentInChildren, vertices, list, activeTerrain))
		{
			do
			{
				num++;
				base.transform.position -= Vector3.up * 0.1f;
			}
			while (!this.AreAllVerticesUnderTerrain(componentInChildren, vertices, list, activeTerrain) && num < 10000);
		}
	}

	private void AlignByTerrainHeight()
	{
		Terrain activeTerrain = Terrain.activeTerrain;
		Vector3 position = base.transform.position;
		position.y = activeTerrain.SampleHeight(position);
		base.transform.position = position;
	}

	public void ApplyRandomRotation()
	{
		Vector3 eulerAngles = base.transform.eulerAngles;
		Vector3 vector = new Vector3((float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 360));
		if (this.randomX)
		{
			eulerAngles.x = vector.x;
		}
		if (this.randomY)
		{
			eulerAngles.y = vector.y;
		}
		if (this.randomZ)
		{
			eulerAngles.z = vector.z;
		}
		base.transform.eulerAngles = eulerAngles;
	}

	private Vector3 WorldVerticePos(Vector3 vertice, MeshFilter filter)
	{
		return filter.transform.TransformPoint(vertice) + (filter.transform.position - base.transform.position);
	}

	private Vector3 RootLocalVerticePos(Vector3 vertice, MeshFilter filter)
	{
		return base.transform.InverseTransformPoint(this.WorldVerticePos(vertice, filter));
	}

	private Vector3 RootLocalPlanePos()
	{
		switch (this.upAxis)
		{
		case Axis.X:
			return base.transform.InverseTransformPoint(base.transform.position + base.transform.right * this.cullingPlaneHeight);
		case Axis.Y:
			return base.transform.InverseTransformPoint(base.transform.position + base.transform.up * this.cullingPlaneHeight);
		case Axis.Z:
			return base.transform.InverseTransformPoint(base.transform.position + base.transform.forward * this.cullingPlaneHeight);
		default:
			return Vector3.zero;
		}
	}

	private bool AreAllVerticesUnderTerrain(MeshFilter filter, Vector3[] allVertices, List<int> verticeIDs, Terrain terrain)
	{
		foreach (int num in verticeIDs)
		{
			Vector3 worldPosition = this.WorldVerticePos(allVertices[num], filter);
			float num2 = terrain.SampleHeight(worldPosition);
			if (worldPosition.y > num2)
			{
				return false;
			}
		}
		return true;
	}

	public Axis upAxis = Axis.Y;

	[Header("Position settings")]
	public float verticalOffset;

	public bool HideLowerPartUnderTerrain;

	public float cullingPlaneHeight;

	[Header("Rotation settings")]
	public bool randomX;

	public bool randomY;

	public bool randomZ;

	public bool alignByNormal;

	[Header("Debug")]
	[Range(5f, 50f)]
	public float planeSize = 5f;

	[Range(0.2f, 2f)]
	public float verticesSize = 0.5f;
}
