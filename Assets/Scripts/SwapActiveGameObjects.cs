using System;
using UnityEngine;

public class SwapActiveGameObjects : MonoBehaviour
{
	private void Start()
	{
		this.SetActive(this.active);
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(this.key))
		{
			this.active = !this.active;
			this.SetActive(this.active);
		}
	}

	private void OnGUI()
	{
		GUI.color = Color.red;
		GUI.Label(new Rect(10f, 10f, 200f, 20f), "Toggle with '" + this.key.ToString() + "' key.");
		if (this.active)
		{
			GUI.Label(new Rect(10f, 50f, 300f, 20f), "MeshCombineStudio is Enabled.");
		}
		else
		{
			GUI.Label(new Rect(10f, 50f, 300f, 20f), "MeshCombineStudio is Disabled.");
		}
		GUI.color = Color.white;
	}

	private void SetActive(bool active)
	{
		this.go1.SetActive(active);
		this.go2.SetActive(!active);
	}

	public GameObject go1;

	public GameObject go2;

	public KeyCode key;

	private bool active = true;
}
