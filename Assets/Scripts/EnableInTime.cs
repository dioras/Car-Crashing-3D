using System;
using UnityEngine;

public class EnableInTime : MonoBehaviour
{
	private void OnEnable()
	{
		foreach (GameObject gameObject in this.ItemsToEnable)
		{
			gameObject.SetActive(false);
		}
		this.lastEnabledTime = Time.time;
	}

	private void Update()
	{
		if (Time.time - this.lastEnabledTime > this.EnableInterval && this.itemID < this.ItemsToEnable.Length)
		{
			this.lastEnabledTime = Time.time;
			this.ItemsToEnable[this.itemID].SetActive(true);
			this.itemID++;
		}
	}

	public GameObject[] ItemsToEnable = new GameObject[0];

	public float EnableInterval = 3f;

	private float lastEnabledTime;

	private int itemID;
}
