using System;
using System.Collections.Generic;
using UnityEngine;

public class WMG_X_Tutorial_1 : MonoBehaviour
{
	private void Start()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.emptyGraphPrefab);
		gameObject.transform.SetParent(base.transform, false);
		this.graph = gameObject.GetComponent<WMG_Axis_Graph>();
		this.series1 = this.graph.addSeries();
		this.graph.xAxis.AxisMaxValue = 5f;
		if (this.useData2)
		{
			List<string> list = new List<string>();
			List<Vector2> list2 = new List<Vector2>();
			for (int i = 0; i < this.series1Data2.Count; i++)
			{
				string[] array = this.series1Data2[i].Split(new char[]
				{
					','
				});
				list.Add(array[0]);
				if (!string.IsNullOrEmpty(array[1]))
				{
					float y = float.Parse(array[1]);
					list2.Add(new Vector2((float)(i + 1), y));
				}
			}
			this.graph.groups.SetList(list);
			this.graph.useGroups = true;
			this.graph.xAxis.LabelType = WMG_Axis.labelTypes.groups;
			this.graph.xAxis.AxisNumTicks = list.Count;
			this.series1.seriesName = "Fruit Data";
			this.series1.UseXDistBetweenToSpace = true;
			this.series1.AutoUpdateXDistBetween = true;
			this.series1.pointValues.SetList(list2);
		}
		else
		{
			this.series1.pointValues.SetList(this.series1Data);
		}
	}

	public GameObject emptyGraphPrefab;

	public WMG_Axis_Graph graph;

	public WMG_Series series1;

	public List<Vector2> series1Data;

	public bool useData2;

	public List<string> series1Data2;
}
