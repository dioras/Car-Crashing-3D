using System;
using CustomVP;
using UnityEngine;

public class VolumetricTireTrackRenderer : MonoBehaviour
{
	private void Start()
	{
		this.resultMesh = new Mesh();
		this.vertices = new Vector3[this.maxMarks * 6 + 6];
		this.triangles = new int[this.maxMarks * 30 + 30];
		Vector2[] array = new Vector2[this.maxMarks * 6 + 6];
		Vector3[] array2 = new Vector3[this.maxMarks * 6 + 6];
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i] = Vector3.up;
		}
		for (int j = 0; j < this.maxMarks; j++)
		{
			int[] array3 = new int[6];
			for (int k = 0; k < 6; k++)
			{
				array3[k] = j * 6 + k;
			}
			float y = (float)j * this.step;
			array[array3[0]] = new Vector2(0f, y);
			array[array3[1]] = new Vector2(0.1f, y);
			array[array3[2]] = new Vector2(0.2f, y);
			array[array3[3]] = new Vector2(0.8f, y);
			array[array3[4]] = new Vector2(0.9f, y);
			array[array3[5]] = new Vector2(1f, y);
		}
		this.resultMesh.vertices = this.vertices;
		this.resultMesh.normals = array2;
		this.resultMesh.uv = array;
		this.resultMesh.MarkDynamic();
		base.gameObject.AddComponent<MeshFilter>().mesh = this.resultMesh;
		base.gameObject.AddComponent<MeshRenderer>().material = this.material;
		this.lastPos = base.transform.position;
		this.ChangeRandomVectors();
	}

	private void ChangeRandomVectors()
	{
		this.randomVectors = new Vector3[4];
		for (int i = 0; i < 4; i++)
		{
			this.randomVectors[i] = UnityEngine.Random.insideUnitSphere * this.Randomness;
		}
	}

	private void Spawn3DStamp()
	{
		if (!this.wheelCollider.IsGrounded)
		{
			return;
		}
		Vector3 realHitPoint = this.wheelCollider.wheelCollider.realHitPoint;
		Vector3 b = this.wheelCollider.transform.forward * this.spawnPointOffset * (float)((this.wheelCollider.rpm < 0f) ? -1 : 1);
		Vector3 vector = Vector3.Cross(this.wheelCollider.wheelCollider.contactNormal, this.wheelCollider.transform.forward);
		Vector3 contactNormal = this.wheelCollider.wheelCollider.contactNormal;
		Vector3 to = realHitPoint - this.lastPos;
		if (Vector3.Distance(realHitPoint, this.lastPos) > this.step * 3f || Vector3.Angle(vector, to) < 45f || Vector3.Angle(-vector, to) < 45f)
		{
			this.lastPos = realHitPoint + b;
			this.BreakStamp();
			return;
		}
		if (this.currentMark % 4 == 0)
		{
			this.ChangeRandomVectors();
		}
		int[] array = new int[6];
		for (int i = 0; i < 6; i++)
		{
			array[i] = this.currentMark * 6 + i;
		}
		int num = this.currentMark + 1;
		if (num > this.maxMarks)
		{
			num = 0;
		}
		int num2 = this.currentMark + 2;
		if (num2 == this.maxMarks + 1)
		{
			num2 = 0;
		}
		if (num2 == this.maxMarks + 2)
		{
			num2 = 1;
		}
		this.LastVerticlesPositions = new Vector3[6];
		this.LastVerticlesIDs = new int[6];
		for (int j = 0; j < 6; j++)
		{
			this.LastVerticlesIDs[j] = num * 6 + j;
			this.LastVerticlesPositions[j] = this.vertices[this.LastVerticlesIDs[j]];
		}
		this.LastVerticlesTargetsIDs = new int[6];
		for (int k = 0; k < 6; k++)
		{
			this.LastVerticlesTargetsIDs[k] = num2 * 6 + k;
		}
		float d = 0f;
		float d2 = 0f;
		if (Vector3.Distance(base.transform.position, this.lastPos) > this.step * 3f)
		{
			d = -this.BumpHeight;
			d2 = -this.VerticalOffset;
		}
		this.vertices[array[0]] = realHitPoint + b - vector * (this.TireWidth / 2f + this.BumpWidth) + Vector3.ProjectOnPlane(this.randomVectors[0], contactNormal);
		this.vertices[array[1]] = realHitPoint + b - vector * (this.TireWidth / 2f + this.BumpWidth * 0.5f) + Vector3.ProjectOnPlane(this.randomVectors[1], contactNormal) + contactNormal * d;
		this.vertices[array[2]] = realHitPoint + b - vector * (this.TireWidth / 2f) + Vector3.up * d2;
		this.vertices[array[3]] = realHitPoint + b + vector * (this.TireWidth / 2f) + Vector3.up * d2;
		this.vertices[array[4]] = realHitPoint + b + vector * (this.TireWidth / 2f + this.BumpWidth * 0.5f) + Vector3.ProjectOnPlane(this.randomVectors[2], contactNormal) + contactNormal * d;
		this.vertices[array[5]] = realHitPoint + b + vector * (this.TireWidth / 2f + this.BumpWidth) + Vector3.ProjectOnPlane(this.randomVectors[3], contactNormal);
		if (this.PrevVerticesIDs != null && this.PrevVerticesIDs.Length == 6 && !this.breakStamp)
		{
			this.vertices[this.PrevVerticesIDs[0]] = this.lastPos + b - vector * (this.TireWidth / 2f + this.BumpWidth) + Vector3.ProjectOnPlane(this.randomVectors[0], contactNormal);
			this.vertices[this.PrevVerticesIDs[1]] = this.lastPos + b - vector * (this.TireWidth / 2f + this.BumpWidth * 0.5f) + Vector3.ProjectOnPlane(this.randomVectors[1], contactNormal) + contactNormal * this.BumpHeight + this.randomVectors[1];
			this.vertices[this.PrevVerticesIDs[2]] = this.lastPos + b - vector * (this.TireWidth / 2f) + Vector3.up * this.VerticalOffset;
			this.vertices[this.PrevVerticesIDs[3]] = this.lastPos + b + vector * (this.TireWidth / 2f) + Vector3.up * this.VerticalOffset;
			this.vertices[this.PrevVerticesIDs[4]] = this.lastPos + b + vector * (this.TireWidth / 2f + this.BumpWidth * 0.5f) + Vector3.ProjectOnPlane(this.randomVectors[2], contactNormal) + contactNormal * this.BumpHeight + this.randomVectors[2];
			this.vertices[this.PrevVerticesIDs[5]] = this.lastPos + b + vector * (this.TireWidth / 2f + this.BumpWidth) + Vector3.ProjectOnPlane(this.randomVectors[3], contactNormal);
		}
		if (!this.breakStamp)
		{
			this.PrevVerticesIDs = this.CurrentVerticesIDs;
		}
		this.CurrentVerticesIDs = array;
		if (this.currentMark > 0 || (this.currentMark == 0 && this.firstLapPassed))
		{
			int num3 = (this.currentMark <= 0) ? this.maxMarks : (this.currentMark - 1);
			int[] array2 = new int[6];
			for (int l = 0; l < 6; l++)
			{
				array2[l] = num3 * 6 + l;
			}
			for (int m = 0; m < 5; m++)
			{
				if (!this.breakStamp)
				{
					if (this.wheelCollider.rpm >= 0f)
					{
						this.triangles[num3 * 30 + m * 6] = array2[m];
						this.triangles[num3 * 30 + m * 6 + 1] = array[m];
						this.triangles[num3 * 30 + m * 6 + 2] = array[m + 1];
						this.triangles[num3 * 30 + m * 6 + 3] = array2[m];
						this.triangles[num3 * 30 + m * 6 + 4] = array[m + 1];
						this.triangles[num3 * 30 + m * 6 + 5] = array2[m + 1];
					}
					else
					{
						this.triangles[num3 * 30 + m * 6] = array2[m];
						this.triangles[num3 * 30 + m * 6 + 1] = array[m + 1];
						this.triangles[num3 * 30 + m * 6 + 2] = array[m];
						this.triangles[num3 * 30 + m * 6 + 3] = array2[m];
						this.triangles[num3 * 30 + m * 6 + 4] = array2[m + 1];
						this.triangles[num3 * 30 + m * 6 + 5] = array[m + 1];
					}
				}
				this.triangles[this.currentMark * 30 + m * 6] = 0;
				this.triangles[this.currentMark * 30 + m * 6 + 1] = 0;
				this.triangles[this.currentMark * 30 + m * 6 + 2] = 0;
				this.triangles[this.currentMark * 30 + m * 6 + 3] = 0;
				this.triangles[this.currentMark * 30 + m * 6 + 4] = 0;
				this.triangles[this.currentMark * 30 + m * 6 + 5] = 0;
			}
		}
		this.breakStamp = false;
		this.resultMesh.vertices = this.vertices;
		this.resultMesh.triangles = this.triangles;
		this.resultMesh.RecalculateBounds();
		this.currentMark++;
		this.lastPos = realHitPoint;
		if (this.currentMark > this.maxMarks)
		{
			this.currentMark = 0;
			this.firstLapPassed = true;
		}
	}

	private void UpdatePosition()
	{
		if (this.CurrentVerticesIDs == null || this.PrevVerticesIDs == null)
		{
			return;
		}
		Vector3 realHitPoint = this.wheelCollider.wheelCollider.realHitPoint;
		Vector3 b = this.wheelCollider.transform.forward * this.spawnPointOffset * (float)((this.wheelCollider.rpm >= -1f) ? 1 : -1);
		Vector3 a = Vector3.Cross(this.wheelCollider.wheelCollider.contactNormal, this.wheelCollider.transform.forward);
		Vector3 contactNormal = this.wheelCollider.wheelCollider.contactNormal;
		float num = Mathf.InverseLerp(0f, this.step, Vector3.Distance(realHitPoint + b, this.lastPos + b));
		float d = this.BumpHeight * Mathf.InverseLerp(0f, this.step, Vector3.Distance(realHitPoint + b, this.lastPos + b));
		this.vertices[this.PrevVerticesIDs[0]] = this.lastPos + b - a * (this.TireWidth / 2f + this.BumpWidth) * num + Vector3.ProjectOnPlane(this.randomVectors[0], contactNormal);
		this.vertices[this.PrevVerticesIDs[1]] = this.lastPos + b - a * (this.TireWidth / 2f + this.BumpWidth * 0.5f) * num + Vector3.ProjectOnPlane(this.randomVectors[1], contactNormal) + contactNormal * d + this.randomVectors[1];
		this.vertices[this.PrevVerticesIDs[2]] = this.lastPos + b - a * (this.TireWidth / 2f) * num + Vector3.up * this.VerticalOffset;
		this.vertices[this.PrevVerticesIDs[3]] = this.lastPos + b + a * (this.TireWidth / 2f) * num + Vector3.up * this.VerticalOffset;
		this.vertices[this.PrevVerticesIDs[4]] = this.lastPos + b + a * (this.TireWidth / 2f + this.BumpWidth * 0.5f) * num + Vector3.ProjectOnPlane(this.randomVectors[2], contactNormal) + contactNormal * d + this.randomVectors[2];
		this.vertices[this.PrevVerticesIDs[5]] = this.lastPos + b + a * (this.TireWidth / 2f + this.BumpWidth) * num + Vector3.ProjectOnPlane(this.randomVectors[3], contactNormal);
		this.vertices[this.CurrentVerticesIDs[0]] = realHitPoint + b;
		this.vertices[this.CurrentVerticesIDs[1]] = realHitPoint + b;
		this.vertices[this.CurrentVerticesIDs[2]] = realHitPoint + b + Vector3.up * this.VerticalOffset;
		this.vertices[this.CurrentVerticesIDs[3]] = realHitPoint + b + Vector3.up * this.VerticalOffset;
		this.vertices[this.CurrentVerticesIDs[4]] = realHitPoint + b;
		this.vertices[this.CurrentVerticesIDs[5]] = realHitPoint + b;
		for (int i = 0; i < this.LastVerticlesIDs.Length; i++)
		{
			this.vertices[this.LastVerticlesIDs[i]] = Vector3.Lerp(this.LastVerticlesPositions[i], this.vertices[this.LastVerticlesTargetsIDs[i]], num);
		}
		this.resultMesh.vertices = this.vertices;
	}

	private void BreakStamp()
	{
		this.CurrentVerticesIDs = null;
		this.PrevVerticesIDs = null;
		this.breakStamp = true;
	}

	private void Update()
	{
		if (this.wheelCollider == null)
		{
			return;
		}
		float num = (float)((this.wheelCollider.rpm >= -1f) ? 1 : -1);
		if (num != this.lastDirection)
		{
			this.BreakStamp();
		}
		this.lastDirection = num;
		if (Vector3.Distance(this.wheelCollider.wheelCollider.realHitPoint, this.lastPos) > this.step && this.RenderTrack)
		{
			this.Spawn3DStamp();
		}
		if (this.wheelCollider.IsGrounded && this.RenderTrack)
		{
			this.UpdatePosition();
		}
		else
		{
			this.BreakStamp();
		}
	}

	public bool RenderTrack;

	public Material material;

	public WheelComponent wheelCollider;

	public int maxMarks = 100;

	public float TireWidth = 0.4f;

	public float BumpHeight = 0.2f;

	public float BumpWidth = 0.4f;

	public float VerticalOffset = 0.05f;

	public float step = 0.5f;

	public float spawnPointOffset = 0.2f;

	public float Randomness = 0.1f;

	private Mesh resultMesh;

	private Vector3[] vertices;

	private int[] triangles;

	private int currentMark;

	private Vector3 lastPos;

	private Vector3[] randomVectors;

	private bool breakStamp;

	private bool firstLapPassed;

	private int[] CurrentVerticesIDs;

	private int[] PrevVerticesIDs;

	private int[] LastVerticlesIDs;

	private int[] LastVerticlesTargetsIDs;

	private Vector3[] LastVerticlesPositions;

	private float lastDirection;
}
