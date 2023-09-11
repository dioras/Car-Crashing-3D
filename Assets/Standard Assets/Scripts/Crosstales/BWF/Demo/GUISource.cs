using System;
using System.Collections;
using System.Collections.Generic;
using Crosstales.BWF.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Crosstales.BWF.Demo
{
	[HelpURL("https://www.crosstales.com/media/data/assets/badwordfilter/api/class_crosstales_1_1_b_w_f_1_1_demo_1_1_g_u_i_source.html")]
	public class GUISource : MonoBehaviour
	{
		public void Start()
		{
			base.StartCoroutine(this.buildLanguageList());
		}

		private IEnumerator buildLanguageList()
		{
			if (this.ItemPrefab != null && this.Scroll != null)
			{
				while (!BWFManager.isReady)
				{
					yield return null;
				}
				RectTransform rowRectTransform = this.ItemPrefab.GetComponent<RectTransform>();
				RectTransform containerRectTransform = this.Target.GetComponent<RectTransform>();
				for (int j = this.Target.transform.childCount - 1; j >= 0; j--)
				{
					Transform child = this.Target.transform.GetChild(j);
					child.SetParent(null);
					UnityEngine.Object.Destroy(child.gameObject);
				}
				List<Source> items = BWFManager.Sources(ManagerMask.BadWord);
				float width = containerRectTransform.rect.width / (float)this.ColumnCount - (this.SpaceWidth.x + this.SpaceWidth.y) * (float)this.ColumnCount;
				float height = rowRectTransform.rect.height - (this.SpaceHeight.x + this.SpaceHeight.y);
				int rowCount = items.Count / this.ColumnCount;
				if (rowCount > 0 && items.Count % rowCount > 0)
				{
					rowCount++;
				}
				float scrollHeight = height * (float)rowCount;
				containerRectTransform.offsetMin = new Vector2(containerRectTransform.offsetMin.x, -scrollHeight / 2f);
				containerRectTransform.offsetMax = new Vector2(containerRectTransform.offsetMax.x, scrollHeight / 2f);
				int i = 0;
				for (int k = 0; k < items.Count; k++)
				{
					if (k % this.ColumnCount == 0)
					{
						i++;
					}
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemPrefab);
					gameObject.name = string.Concat(new object[]
					{
						this.Target.name,
						" item at (",
						k,
						",",
						i,
						")"
					});
					gameObject.transform.SetParent(this.Target.transform);
					gameObject.transform.localScale = Vector3.one;
					gameObject.GetComponent<SourceEntry>().Source = items[k];
					gameObject.GetComponent<SourceEntry>().GuiMain = this.GuiMain;
					RectTransform component = gameObject.GetComponent<RectTransform>();
					float x = -containerRectTransform.rect.width / 2f + (width + this.SpaceWidth.x) * (float)(k % this.ColumnCount) + this.SpaceWidth.x * (float)this.ColumnCount;
					float y = containerRectTransform.rect.height / 2f - (height + this.SpaceHeight.y) * (float)i;
					component.offsetMin = new Vector2(x, y);
					x = component.offsetMin.x + width;
					y = component.offsetMin.y + height;
					component.offsetMax = new Vector2(x, y);
				}
				this.Scroll.value = 1f;
			}
			yield break;
		}

		public GameObject ItemPrefab;

		public GameObject Target;

		public Scrollbar Scroll;

		public GUIMain GuiMain;

		public int ColumnCount = 1;

		public Vector2 SpaceWidth = new Vector2(8f, 8f);

		public Vector2 SpaceHeight = new Vector2(8f, 8f);
	}
}
