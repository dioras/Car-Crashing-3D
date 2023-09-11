using System;
using UnityEngine;

public class WaterBasic : MonoBehaviour
{
	private void Start()
	{
		this.r = base.GetComponent<Renderer>();
		this.m = this.r.sharedMaterial;
	}

	private void Update()
	{
		if (!this.r || !this.m)
		{
			return;
		}
		this.waveSpeed = this.m.GetVector("WaveSpeed");
		this.waveScale = this.m.GetFloat("_WaveScale");
		this.t = Time.time / 20f;
		this.offset4 = this.waveSpeed * (this.t * this.waveScale);
		this.offsetClamped = new Vector4(Mathf.Repeat(this.offset4.x, 1f), Mathf.Repeat(this.offset4.y, 1f), Mathf.Repeat(this.offset4.z, 1f), Mathf.Repeat(this.offset4.w, 1f));
		this.m.SetVector("_WaveOffset", this.offsetClamped);
	}

	private Renderer r;

	private Material m;

	private float waveScale;

	private float t;

	private Vector4 waveSpeed;

	private Vector4 offset4;

	private Vector4 offsetClamped;
}
