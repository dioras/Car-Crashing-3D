using System;
using UnityEngine;
using UnityEngine.Rendering;

public class TireMarksRenderer : MonoBehaviour
{
	private void Start()
	{
		base.transform.position = Vector3.zero;
		base.transform.rotation = Quaternion.identity;
		MeshFilter meshFilter = base.gameObject.GetComponent<MeshFilter>();
		if (meshFilter == null)
		{
			meshFilter = base.gameObject.AddComponent<MeshFilter>();
		}
		MeshRenderer meshRenderer = base.gameObject.GetComponent<MeshRenderer>();
		if (meshRenderer == null)
		{
			meshRenderer = base.gameObject.AddComponent<MeshRenderer>();
			meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
			meshRenderer.material = this.material;
		}
		this.m_markPoints = new MarkPoint[this.maxMarks * 2];
		int i = 0;
		int num = this.m_markPoints.Length;
		while (i < num)
		{
			this.m_markPoints[i] = new MarkPoint();
			i++;
		}
		this.m_markCount = 0;
		this.m_markArraySize = this.m_markPoints.Length;
		this.m_vertices = new Vector3[this.maxMarks * 4];
		this.m_normals = new Vector3[this.maxMarks * 4];
		this.m_tangents = new Vector4[this.maxMarks * 4];
		this.m_colors = new Color[this.maxMarks * 4];
		this.m_uvs = new Vector2[this.maxMarks * 4];
		this.m_triangles = new int[this.maxMarks * 6];
		this.m_values = new Vector2[this.maxMarks];
		this.m_segmentCount = 0;
		this.m_segmentArraySize = this.maxMarks;
		this.m_segmentsUpdated = false;
		for (int j = 0; j < this.m_segmentArraySize; j++)
		{
			this.m_uvs[j * 4] = new Vector2(0f, 0.05f);
			this.m_uvs[j * 4 + 1] = new Vector2(1f, 0.05f);
			this.m_uvs[j * 4 + 2] = new Vector2(0f, 0.95f);
			this.m_uvs[j * 4 + 3] = new Vector2(1f, 0.95f);
			this.m_triangles[j * 6] = j * 4;
			this.m_triangles[j * 6 + 2] = j * 4 + 1;
			this.m_triangles[j * 6 + 1] = j * 4 + 2;
			this.m_triangles[j * 6 + 3] = j * 4 + 2;
			this.m_triangles[j * 6 + 5] = j * 4 + 1;
			this.m_triangles[j * 6 + 4] = j * 4 + 3;
		}
		this.m_mesh = new Mesh();
		this.m_mesh.MarkDynamic();
		this.m_mesh.vertices = this.m_vertices;
		this.m_mesh.normals = this.m_normals;
		this.m_mesh.tangents = this.m_tangents;
		this.m_mesh.colors = this.m_colors;
		this.m_mesh.triangles = this.m_triangles;
		this.m_mesh.uv = this.m_uvs;
		this.m_mesh.RecalculateBounds();
		meshFilter.mesh = this.m_mesh;
	}

	private void OnValidate()
	{
		if (this.m_uvs != null)
		{
			for (int i = 0; i < this.m_uvs.Length / 4; i++)
			{
				this.m_uvs[i * 4] = new Vector2(0f, 0.05f);
				this.m_uvs[i * 4 + 1] = new Vector2(1f, 0.05f);
				this.m_uvs[i * 4 + 2] = new Vector2(0f, 0.95f);
				this.m_uvs[i * 4 + 3] = new Vector2(1f, 0.95f);
			}
		}
		this.m_segmentsUpdated = true;
	}

	public int AddMark(Vector3 pos, Vector3 normal, float width, int lastIndex)
	{
		if (!base.isActiveAndEnabled || this.m_markArraySize == 0)
		{
			return -1;
		}
		Vector3 vector = pos + normal * 0.02f;
		if (lastIndex >= 0 && Vector3.Distance(vector, this.m_markPoints[lastIndex % this.m_markArraySize].pos) < 0.1f)
		{
			return lastIndex;
		}
		if (lastIndex >= 0 && Vector3.Distance(vector, this.m_markPoints[lastIndex % this.m_markArraySize].pos) > 10f)
		{
			return -1;
		}
		MarkPoint markPoint = this.m_markPoints[this.m_markCount % this.m_markArraySize];
		markPoint.pos = vector;
		markPoint.normal = normal;
		markPoint.lastIndex = lastIndex;
		if (lastIndex >= 0 && lastIndex > this.m_markCount - this.m_markArraySize)
		{
			MarkPoint markPoint2 = this.m_markPoints[lastIndex % this.m_markArraySize];
			Vector3 lhs = markPoint.pos - markPoint2.pos;
			Vector3 normalized = Vector3.Cross(lhs, normal).normalized;
			Vector3 b = 0.5f * width * normalized;
			markPoint.posl = markPoint.pos + b;
			markPoint.posr = markPoint.pos - b;
			markPoint.tangent = new Vector4(normalized.x, normalized.y, normalized.z, 1f);
			if (markPoint2.lastIndex < 0)
			{
				markPoint2.tangent = markPoint.tangent;
				markPoint2.posl = markPoint.pos + b;
				markPoint2.posr = markPoint.pos - b;
			}
			this.AddSegment(markPoint2, markPoint);
		}
		this.m_markCount++;
		return this.m_markCount - 1;
	}

	private void AddSegment(MarkPoint first, MarkPoint second)
	{
		int num = this.m_segmentCount % this.m_segmentArraySize * 4;
		this.m_vertices[num] = first.posl;
		this.m_vertices[num + 1] = first.posr;
		this.m_vertices[num + 2] = second.posl;
		this.m_vertices[num + 3] = second.posr;
		this.m_normals[num] = first.normal;
		this.m_normals[num + 1] = first.normal;
		this.m_normals[num + 2] = second.normal;
		this.m_normals[num + 3] = second.normal;
		this.m_tangents[num] = first.tangent;
		this.m_tangents[num + 1] = first.tangent;
		this.m_tangents[num + 2] = second.tangent;
		this.m_tangents[num + 3] = second.tangent;
		this.m_colors[num].a = 1f;
		this.m_colors[num + 1].a = 1f;
		this.m_colors[num + 2].a = 1f;
		this.m_colors[num + 3].a = 1f;
		this.m_values[num / 4] = Vector2.one;
		if (this.m_segmentCount == 0)
		{
			Vector3 vector = this.m_vertices[0];
			int i = 4;
			int num2 = this.m_vertices.Length;
			while (i < num2)
			{
				this.m_vertices[i] = vector;
				i++;
			}
		}
		this.m_segmentCount++;
		this.m_segmentsUpdated = true;
	}

	private void LateUpdate()
	{
		if (!this.m_segmentsUpdated)
		{
			return;
		}
		this.m_segmentsUpdated = false;
		int num = (int)((float)this.m_segmentArraySize * 0.5f);
		if (num > 0)
		{
			int num2 = this.m_segmentCount - this.m_segmentArraySize;
			int num3 = 0;
			if (num2 < 0)
			{
				num3 = -num2;
				num2 = 0;
			}
			float num4 = 1f / (float)num;
			for (int i = num3; i < num; i++)
			{
				int num5 = num2 % this.m_segmentArraySize;
				int num6 = num5 * 4;
				float num7 = (float)i * num4;
				float a = this.m_values[num5].x * num7;
				float a2 = this.m_values[num5].y * num7 + num4;
				this.m_colors[num6].a = a;
				this.m_colors[num6 + 1].a = a;
				this.m_colors[num6 + 2].a = a2;
				this.m_colors[num6 + 3].a = a2;
				num2++;
			}
		}
		this.m_mesh.MarkDynamic();
		this.m_mesh.vertices = this.m_vertices;
		this.m_mesh.normals = this.m_normals;
		this.m_mesh.tangents = this.m_tangents;
		this.m_mesh.colors = this.m_colors;
		this.m_mesh.RecalculateBounds();
	}

	public int maxMarks = 1024;

	public Material material;

	private int m_markCount;

	private int m_markArraySize;

	private MarkPoint[] m_markPoints;

	private bool m_segmentsUpdated;

	private int m_segmentCount;

	private int m_segmentArraySize;

	private Mesh m_mesh;

	private Vector3[] m_vertices;

	private Vector3[] m_normals;

	private Vector4[] m_tangents;

	private Color[] m_colors;

	private Vector2[] m_uvs;

	private int[] m_triangles;

	private Vector2[] m_values;
}
