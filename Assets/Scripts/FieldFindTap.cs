using System;
using UnityEngine;

public class FieldFindTap : MonoBehaviour
{
	private void Start()
	{
		this.menuManager = MenuManager.Instance;
	}

	private void OnMouseDown()
	{
		if (this.menuManager != null)
		{
			this.menuManager.ShowFieldFindMessage();
		}
	}

	private MenuManager menuManager;
}
