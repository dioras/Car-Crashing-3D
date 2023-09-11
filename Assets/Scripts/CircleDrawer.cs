using System;
using UnityEngine;

public class CircleDrawer : MonoBehaviour
{
	private void Awake()
	{
		this.terrain = Terrain.activeTerrain;
		base.gameObject.name = "CircleDrawer";
		this.lineRenderer = base.gameObject.AddComponent<LineRenderer>();
		this.lineRenderer.loop = true;
		this.lineRenderer.alignment = LineAlignment.TransformZ;
		this.UpdateCircle();
	}

	private void Update()
	{
		this.UpdateCircle();
	}

	private void UpdateCircle()
	{
		this.lineRenderer.positionCount = this.pointsCount;
		this.lineRenderer.widthMultiplier = this.width;
		this.lineRenderer.material = this.mat;
		base.transform.eulerAngles = new Vector3(90f, 0f, 0f);
		float num = 0f;
		for (int i = 0; i < this.pointsCount; i++)
		{
			num += 1f / (float)(this.pointsCount - 1) * 3.14159274f * 2f;
			float x = this.radius * Mathf.Cos(num);
			float z = this.radius * Mathf.Sin(num);
			Vector3 position = base.transform.position + new Vector3(x, 0f, z);
			this.lineRenderer.SetPosition(i, position);
		}
	}

	private void OnDisable()
	{
		this.lineRenderer.enabled = false;
	}

	private void OnEnable()
	{
		this.lineRenderer.enabled = true;
	}

	public int pointsCount = 20;

	public float radius = 3f;

	public float width = 0.5f;

	public Material mat;

	private LineRenderer lineRenderer;

	private Terrain terrain;
}
