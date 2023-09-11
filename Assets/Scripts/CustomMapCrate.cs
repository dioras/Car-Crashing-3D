using System;
using UnityEngine;

public class CustomMapCrate : MonoBehaviour
{
	private CustomMapStashManager stashManager
	{
		get
		{
			if (this.sm == null)
			{
				this.sm = CustomMapStashManager.Instance;
			}
			return this.sm;
		}
	}

	public int m_money { get; internal set; }

	public int m_gold { get; internal set; }

	private void Start()
	{
		this.m_money = UnityEngine.Random.Range(this.minMoney, this.maxMoney);
		this.m_gold = UnityEngine.Random.Range(this.minGold, this.maxGold);
		if (this.stashManager != null)
		{
			this.stashManager.RegisterCrate(this);
		}
	}

	private void OnMouseDown()
	{
		this.stashManager.PickupCrate(this);
	}

	private CustomMapStashManager sm;

	private int minMoney = 300;

	private int maxMoney = 1000;

	private int minGold;

	private int maxGold = 10;
}
