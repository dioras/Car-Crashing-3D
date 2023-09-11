using System;
using UnityEngine;

[Serializable]
public class WaterMovement : MonoBehaviour
{
	public WaterMovement()
	{
		this.speed = 0.7f;
		this.alpha = 0.5f;
		this.waveScale = (float)3;
	}

	public virtual void Start()
	{
		this.m = this.gameObject.GetComponent<Renderer>().material;
	}

	public virtual void Update()
	{
		float time = Time.time;
		float num = Mathf.PingPong(time * this.speed, (float)100) * 0.15f;
		this.m.mainTextureOffset = new Vector2(num, num);
		float a = this.alpha;
		Color color = this.m.color;
		float num2 = color.a = a;
		Color color2 = this.m.color = color;
		this.m.mainTextureScale = new Vector2(this.waveScale, this.waveScale);
	}

	public virtual void Main()
	{
	}

	public float speed;

	public float alpha;

	public float waveScale;

	public Material m;
}
