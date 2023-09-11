using System;
using UnityEngine;

[Serializable]
public class Rotate : MonoBehaviour
{
	public Rotate()
	{
		this.speed = 10f;
	}

	public virtual void Update()
	{
		this.transform.Rotate(Vector3.up, this.speed * Time.deltaTime);
	}

	public virtual void Main()
	{
	}

	public float speed;
}
