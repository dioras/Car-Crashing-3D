using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainScene : MonoBehaviour
{
	private void Start()
	{
		SceneManager.LoadScene("Menu");
	}

	private void Update()
	{
	}
}
