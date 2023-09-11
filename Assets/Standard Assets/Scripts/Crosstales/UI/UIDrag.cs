using System;
using UnityEngine;

namespace Crosstales.UI
{
	public class UIDrag : MonoBehaviour
	{
		public void BeginDrag()
		{
			this.offsetX = base.transform.position.x - UnityEngine.Input.mousePosition.x;
			this.offsetY = base.transform.position.y - UnityEngine.Input.mousePosition.y;
		}

		public void OnDrag()
		{
			base.transform.position = new Vector3(this.offsetX + UnityEngine.Input.mousePosition.x, this.offsetY + UnityEngine.Input.mousePosition.y);
		}

		private float offsetX;

		private float offsetY;
	}
}
