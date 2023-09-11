using System;
using UnityEngine;

[Serializable]
public class Waves : MonoBehaviour
{
	public Waves()
	{
		this.scale = 10f;
		this.speed = 1f;
		this.power = 0.3f;
	}

	public virtual void Awake()
	{
		this.startPos = this.transform.position;
	}

	public virtual void Update()
	{
		this.transform.position = this.startPos + Vector3.up * Mathf.Sin(Time.time / (float)2) * this.scale;
	}

	public virtual void Main()
	{
	}

	public float scale;

	public float speed;

	public float power;

	public Vector3 startPos;
}
