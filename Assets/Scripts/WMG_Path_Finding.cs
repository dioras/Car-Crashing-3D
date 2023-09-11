using System;
using System.Collections.Generic;
using UnityEngine;

public class WMG_Path_Finding : IWMG_Path_Finding
{
	public List<WMG_Link> FindShortestPathBetweenNodes(WMG_Node fromNode, WMG_Node toNode)
	{
		List<WMG_Link> list = new List<WMG_Link>();
		foreach (GameObject gameObject in this.nodesParent)
		{
			WMG_Node component = gameObject.GetComponent<WMG_Node>();
			if (component != null)
			{
				component.BFS_mark = false;
				component.BFS_depth = 0;
			}
		}
		Queue<WMG_Node> queue = new Queue<WMG_Node>();
		queue.Enqueue(fromNode);
		fromNode.BFS_mark = true;
		while (queue.Count > 0)
		{
			WMG_Node wmg_Node = queue.Dequeue();
			if (toNode.id == wmg_Node.id)
			{
				break;
			}
			for (int i = 0; i < wmg_Node.numLinks; i++)
			{
				WMG_Link component2 = wmg_Node.links[i].GetComponent<WMG_Link>();
				WMG_Node component3 = component2.toNode.GetComponent<WMG_Node>();
				if (component3.id == wmg_Node.id)
				{
					component3 = component2.fromNode.GetComponent<WMG_Node>();
				}
				if (!component3.BFS_mark)
				{
					component3.BFS_mark = true;
					component3.BFS_depth = wmg_Node.BFS_depth + 1;
					queue.Enqueue(component3);
				}
			}
		}
		queue.Clear();
		queue.Enqueue(toNode);
		while (queue.Count > 0)
		{
			WMG_Node wmg_Node2 = queue.Dequeue();
			if (fromNode.id == wmg_Node2.id)
			{
				break;
			}
			for (int j = 0; j < wmg_Node2.numLinks; j++)
			{
				WMG_Link component4 = wmg_Node2.links[j].GetComponent<WMG_Link>();
				WMG_Node component5 = component4.toNode.GetComponent<WMG_Node>();
				if (component5.id == wmg_Node2.id)
				{
					component5 = component4.fromNode.GetComponent<WMG_Node>();
				}
				if (wmg_Node2.BFS_depth == component5.BFS_depth + 1)
				{
					if (component5.BFS_depth != 0 || component5.id == fromNode.id)
					{
						list.Add(component4);
						if (!queue.Contains(component5))
						{
							queue.Enqueue(component5);
						}
					}
				}
			}
		}
		return list;
	}

	public List<WMG_Link> FindShortestPathBetweenNodesWeighted(WMG_Node fromNode, WMG_Node toNode, bool includeRadii)
	{
		List<WMG_Link> list = new List<WMG_Link>();
		List<WMG_Node> list2 = new List<WMG_Node>();
		foreach (GameObject gameObject in this.nodesParent)
		{
			WMG_Node component = gameObject.GetComponent<WMG_Node>();
			if (component != null)
			{
				if (component.id == fromNode.id)
				{
					component.Dijkstra_depth = 0f;
				}
				else
				{
					component.Dijkstra_depth = float.PositiveInfinity;
				}
				list2.Add(component);
			}
		}
		list2.Sort((WMG_Node x, WMG_Node y) => x.Dijkstra_depth.CompareTo(y.Dijkstra_depth));
		while (list2.Count > 0)
		{
			WMG_Node wmg_Node = list2[0];
			list2.RemoveAt(0);
			if (toNode.id == wmg_Node.id)
			{
				break;
			}
			if (wmg_Node.Dijkstra_depth == float.PositiveInfinity)
			{
				break;
			}
			for (int i = 0; i < wmg_Node.numLinks; i++)
			{
				WMG_Link component2 = wmg_Node.links[i].GetComponent<WMG_Link>();
				WMG_Node component3 = component2.toNode.GetComponent<WMG_Node>();
				if (component3.id == wmg_Node.id)
				{
					component3 = component2.fromNode.GetComponent<WMG_Node>();
				}
				float num = wmg_Node.Dijkstra_depth + component2.weight;
				if (includeRadii)
				{
					num += wmg_Node.radius + component3.radius;
				}
				if (num < component3.Dijkstra_depth)
				{
					component3.Dijkstra_depth = num;
					list2.Sort((WMG_Node x, WMG_Node y) => x.Dijkstra_depth.CompareTo(y.Dijkstra_depth));
				}
			}
		}
		Queue<WMG_Node> queue = new Queue<WMG_Node>();
		queue.Enqueue(toNode);
		while (queue.Count > 0)
		{
			WMG_Node wmg_Node2 = queue.Dequeue();
			if (fromNode.id == wmg_Node2.id)
			{
				break;
			}
			for (int j = 0; j < wmg_Node2.numLinks; j++)
			{
				WMG_Link component4 = wmg_Node2.links[j].GetComponent<WMG_Link>();
				WMG_Node component5 = component4.toNode.GetComponent<WMG_Node>();
				if (component5.id == wmg_Node2.id)
				{
					component5 = component4.fromNode.GetComponent<WMG_Node>();
				}
				float num2 = component5.Dijkstra_depth + component4.weight;
				if (includeRadii)
				{
					num2 += wmg_Node2.radius + component5.radius;
				}
				if (Mathf.Approximately(wmg_Node2.Dijkstra_depth, num2))
				{
					list.Add(component4);
					if (!queue.Contains(component5))
					{
						queue.Enqueue(component5);
					}
				}
			}
		}
		return list;
	}

	public List<GameObject> nodesParent;
}
