using System;
using UnityEngine;

[Serializable]
public struct DebrisRect
{
	public Vector3 GetRandomPos()
	{
		Vector3 a = this.FL.position - this.RL.position;
		Vector3 a2 = this.FR.position - this.FL.position;
		return this.RL.position + a * UnityEngine.Random.Range(0f, 1f) + a2 * UnityEngine.Random.Range(0f, 1f);
	}

	public Transform FL;

	public Transform FR;

	public Transform RL;

	public Transform RR;
}
