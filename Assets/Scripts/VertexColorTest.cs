using System;
using UnityEngine;

public class VertexColorTest : MonoBehaviour
{
	private void Start()
	{
		this.mesh = base.GetComponent<MeshFilter>().mesh;
		this.vertices = this.mesh.vertices;
		this.triangles = this.mesh.triangles;
		this.colors = new Color[this.vertices.Length];
		for (int i = 0; i < this.colors.Length; i++)
		{
			this.colors[i] = this.colorToSet;
		}
		this.mesh.colors = this.colors;
	}

	private void Update()
	{
		foreach (Transform transform in this.spheres)
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(transform.position, Vector3.down, out raycastHit))
			{
				int num = this.triangles[raycastHit.triangleIndex * 3];
				int num2 = this.triangles[raycastHit.triangleIndex * 3 + 1];
				int num3 = this.triangles[raycastHit.triangleIndex * 3 + 2];
				Vector3 vector = this.vertices[num];
				Vector3 vector2 = this.vertices[num2];
				Vector3 vector3 = this.vertices[num3];
				Color color = this.colors[num];
				color.r += this.step;
				color.r = Mathf.Clamp01(color.r);
				this.colors[num] = color;
				color = this.colors[num2];
				color.r += this.step;
				color.r = Mathf.Clamp01(color.r);
				this.colors[num2] = color;
				color = this.colors[num3];
				color.r += this.step;
				color.r = Mathf.Clamp01(color.r);
				this.colors[num3] = color;
			}
		}
		this.mesh.colors = this.colors;
	}

	[ContextMenu("Apply")]
	private void apply()
	{
		for (int i = 0; i < this.colors.Length; i++)
		{
			Vector3 vector = base.transform.TransformPoint(this.vertices[i]);
			Color color = this.colors[i];
			this.colors[i] = color;
		}
		this.mesh.colors = this.colors;
	}

	public float threshold;

	public Transform[] spheres;

	private Color[] colors;

	private Mesh mesh;

	private Vector3[] vertices;

	private int[] triangles;

	public float step = 0.1f;

	public Color colorToSet;
}
