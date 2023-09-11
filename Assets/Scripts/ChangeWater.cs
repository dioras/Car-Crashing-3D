using System;
using UnityEngine;

[Serializable]
public class ChangeWater : MonoBehaviour
{
	public virtual void Update()
	{
		if (Input.anyKeyDown)
		{
			if (this.currentIndex < this.waters.Length - 1)
			{
				this.currentIndex++;
			}
			else
			{
				this.currentIndex = 0;
			}
			int i = 0;
			GameObject[] array = this.waters;
			int length = array.Length;
			while (i < length)
			{
				array[i].SetActiveRecursively(false);
				i++;
			}
			this.waters[this.currentIndex].SetActiveRecursively(true);
		}
	}

	public virtual void Main()
	{
	}

	public GameObject[] waters;

	private int currentIndex;
}
