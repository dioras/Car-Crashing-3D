using System;
using UnityEngine;

[ExecuteInEditMode]
public class ObjectSpawner : MonoBehaviour
{
	private void Awake()
	{
		this.t = base.transform;
	}

	private void Start()
	{
		if (this.spawnInRuntime && this.spawnOnStart)
		{
			this.Spawn();
		}
	}

	private void Update()
	{
		if (this.spawn)
		{
			this.spawn = false;
			this.Spawn();
		}
		if (this.deleteChildren)
		{
			this.deleteChildren = false;
			this.DeleteChildren();
		}
	}

	public void DeleteChildren()
	{
		Transform[] componentsInChildren = base.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (this.t != componentsInChildren[i] && componentsInChildren[i] != null)
			{
				UnityEngine.Object.DestroyImmediate(componentsInChildren[i].gameObject);
			}
		}
	}

	public void Spawn()
	{
		Bounds bounds = default(Bounds);
		bounds.center = base.transform.position;
		bounds.size = base.transform.lossyScale;
		float x = bounds.min.x;
		float x2 = bounds.max.x;
		float y = bounds.min.y;
		float y2 = bounds.max.y;
		float z = bounds.min.z;
		float z2 = bounds.max.z;
		int max = this.objects.Length;
		float num = this.resolutionPerMeter * 0.5f;
		float num2 = base.transform.lossyScale.y * 0.5f;
		int num3 = 0;
		for (float num4 = z; num4 < z2; num4 += this.resolutionPerMeter)
		{
			for (float num5 = x; num5 < x2; num5 += this.resolutionPerMeter)
			{
				for (float num6 = y; num6 < y2; num6 += this.resolutionPerMeter)
				{
					int num7 = UnityEngine.Random.Range(0, max);
					float value = UnityEngine.Random.value;
					if (value < this.density)
					{
						Vector3 position = new Vector3(num5 + UnityEngine.Random.Range(-num, num), (num6 + UnityEngine.Random.Range(-num, num)) * UnityEngine.Random.Range(this.heightRange.x, this.heightRange.y), num4 + UnityEngine.Random.Range(-num, num));
						if (position.x >= x && position.x <= x2 && position.y >= y && position.y <= y2 && position.z >= z && position.z <= z2)
						{
							position.y += num2;
							Vector3 euler = new Vector3(UnityEngine.Random.Range(0f, this.rotationRange.x), UnityEngine.Random.Range(0f, this.rotationRange.y), UnityEngine.Random.Range(0f, this.rotationRange.z));
							GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objects[num7], position, Quaternion.Euler(euler));
							float num8 = UnityEngine.Random.Range(this.scaleRange.x, this.scaleRange.y) * this.scaleMulti;
							gameObject.transform.localScale = new Vector3(num8, num8, num8);
							gameObject.transform.parent = this.t;
							num3++;
						}
					}
				}
			}
		}
		UnityEngine.Debug.Log("Spawned " + num3);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireCube(base.transform.position + new Vector3(0f, base.transform.lossyScale.y * 0.5f, 0f), base.transform.lossyScale);
	}

	public GameObject[] objects;

	public float density = 0.5f;

	public Vector2 scaleRange = new Vector2(0.5f, 2f);

	public Vector3 rotationRange = new Vector3(5f, 360f, 5f);

	public Vector2 heightRange = new Vector2(0f, 1f);

	public float scaleMulti = 1f;

	public float resolutionPerMeter = 2f;

	public bool spawnInRuntime;

	public bool spawnOnStart;

	public bool spawn;

	public bool deleteChildren;

	private Transform t;
}
