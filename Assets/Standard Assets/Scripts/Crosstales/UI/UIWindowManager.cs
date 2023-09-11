using System;
using UnityEngine;
using UnityEngine.UI;

namespace Crosstales.UI
{
	public class UIWindowManager : MonoBehaviour
	{
		public void Start()
		{
			foreach (GameObject gameObject in this.Windows)
			{
				this.image = gameObject.transform.Find("Panel/Header").GetComponent<Image>();
				Color color = this.image.color;
				color.a = 0.2f;
				this.image.color = color;
			}
		}

		public void ChangeState(GameObject x)
		{
			foreach (GameObject gameObject in this.Windows)
			{
				if (gameObject != x)
				{
					this.image = gameObject.transform.Find("Panel/Header").GetComponent<Image>();
					Color color = this.image.color;
					color.a = 0.2f;
					this.image.color = color;
				}
				this.DontTouch = gameObject.transform.Find("Panel/DontTouch").gameObject;
				this.DontTouch.SetActive(gameObject != x);
			}
		}

		[Tooltip("All Windows of the scene.")]
		public GameObject[] Windows;

		private Image image;

		private GameObject DontTouch;
	}
}
