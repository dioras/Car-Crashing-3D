using System;
using UnityEngine;

public class GetToStorage : MonoBehaviour
{
	private void Start()
	{
		this.menuManager = MenuManager.Instance;
	}

	private void OnMouseDown()
	{
		this.menuManager.ShowStorage();
	}

	private MenuManager menuManager;
}
