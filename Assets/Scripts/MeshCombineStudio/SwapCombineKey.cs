using System;
using System.Collections.Generic;
using UnityEngine;

namespace MeshCombineStudio
{
	public class SwapCombineKey : MonoBehaviour
	{
		private void Awake()
		{
			this.meshCombiner = base.GetComponent<MeshCombiner>();
			SwapCombineKey.meshCombinerList.Add(this.meshCombiner);
		}

		private void Update()
		{
			if (UnityEngine.Input.GetKeyDown(this.meshCombiner.combineSwapKey))
			{
				this.meshCombiner.SwapCombine();
			}
		}

		private void OnGUI()
		{
			GUI.color = Color.red;
			GUI.Label(new Rect(10f, 10f, 200f, 20f), "Toggle with '" + this.meshCombiner.combineSwapKey.ToString() + "' key.");
			for (int i = 0; i < SwapCombineKey.meshCombinerList.Count; i++)
			{
				MeshCombiner meshCombiner = SwapCombineKey.meshCombinerList[i];
				if (meshCombiner.combinedActive)
				{
					GUI.Label(new Rect(10f, (float)(30 + i * 20), 300f, 20f), meshCombiner.gameObject.name + " is Enabled.");
				}
				else
				{
					GUI.Label(new Rect(10f, (float)(30 + i * 20), 300f, 20f), meshCombiner.gameObject.name + " is Disabled.");
				}
			}
			GUI.color = Color.white;
		}

		public static List<MeshCombiner> meshCombinerList = new List<MeshCombiner>();

		private MeshCombiner meshCombiner;
	}
}
