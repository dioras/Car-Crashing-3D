using System;
using System.Collections.Generic;
using UnityEngine;

public class WMG_Graph_Manager : WMG_Events, IWMG_Data_Generators, IWMG_Caching_Functions, IWMG_Path_Finding
{
	public List<Vector2> GenLinear(int numPoints, float minX, float maxX, float a, float b)
	{
		return this.data_gen.GenLinear(numPoints, minX, maxX, a, b);
	}

	public List<Vector2> GenQuadratic(int numPoints, float minX, float maxX, float a, float b, float c)
	{
		return this.data_gen.GenQuadratic(numPoints, minX, maxX, a, b, c);
	}

	public List<Vector2> GenExponential(int numPoints, float minX, float maxX, float a, float b, float c)
	{
		return this.data_gen.GenExponential(numPoints, minX, maxX, a, b, c);
	}

	public List<Vector2> GenLogarithmic(int numPoints, float minX, float maxX, float a, float b, float c)
	{
		return this.data_gen.GenLogarithmic(numPoints, minX, maxX, a, b, c);
	}

	public List<Vector2> GenCircular(int numPoints, float a, float b, float c)
	{
		return this.data_gen.GenCircular(numPoints, a, b, c);
	}

	public List<Vector2> GenCircular2(int numPoints, float a, float b, float c, float degreeOffset)
	{
		return this.data_gen.GenCircular2(numPoints, a, b, c, degreeOffset);
	}

	public List<Vector2> GenRadar(List<float> data, float a, float b, float degreeOffset)
	{
		return this.data_gen.GenRadar(data, a, b, degreeOffset);
	}

	public List<Vector2> GenRandomXY(int numPoints, float minX, float maxX, float minY, float maxY)
	{
		return this.data_gen.GenRandomXY(numPoints, minX, maxX, minY, maxY);
	}

	public List<Vector2> GenRandomY(int numPoints, float minX, float maxX, float minY, float maxY)
	{
		return this.data_gen.GenRandomY(numPoints, minX, maxX, minY, maxY);
	}

	public List<float> GenRandomList(int numPoints, float min, float max)
	{
		return this.data_gen.GenRandomList(numPoints, min, max);
	}

	public void updateCacheAndFlagList<T>(ref List<T> cache, List<T> val, ref bool flag)
	{
		this.caching.updateCacheAndFlagList<T>(ref cache, val, ref flag);
	}

	public void updateCacheAndFlag<T>(ref T cache, T val, ref bool flag)
	{
		this.caching.updateCacheAndFlag<T>(ref cache, val, ref flag);
	}

	public void SwapVals<T>(ref T val1, ref T val2)
	{
		this.caching.SwapVals<T>(ref val1, ref val2);
	}

	public void SwapValsList<T>(ref List<T> val1, ref List<T> val2)
	{
		this.caching.SwapValsList<T>(ref val1, ref val2);
	}

	public List<WMG_Link> FindShortestPathBetweenNodes(WMG_Node fromNode, WMG_Node toNode)
	{
		this.path_find.nodesParent = this.nodesParent;
		return this.path_find.FindShortestPathBetweenNodes(fromNode, toNode);
	}

	public List<WMG_Link> FindShortestPathBetweenNodesWeighted(WMG_Node fromNode, WMG_Node toNode, bool includeRadii)
	{
		this.path_find.nodesParent = this.nodesParent;
		return this.path_find.FindShortestPathBetweenNodesWeighted(fromNode, toNode, includeRadii);
	}

	public string getLabelText(string text, WMG_Enums.labelTypes labelType, float value, float percent, int numDecimals)
	{
		string text2 = text;
		float num = Mathf.Pow(10f, (float)(numDecimals + 2));
		if (labelType == WMG_Enums.labelTypes.None)
		{
			text2 = string.Empty;
		}
		else if (labelType == WMG_Enums.labelTypes.Labels_Percents)
		{
			text2 = text2 + " (" + (Mathf.Round(percent * num) / num * 100f).ToString() + "%)";
		}
		else if (labelType == WMG_Enums.labelTypes.Labels_Values)
		{
			text2 = text2 + " (" + Mathf.Round(value).ToString() + ")";
		}
		else if (labelType == WMG_Enums.labelTypes.Labels_Values_Percents)
		{
			string text3 = text2;
			text2 = string.Concat(new string[]
			{
				text3,
				" - ",
				Mathf.Round(value).ToString(),
				" (",
				(Mathf.Round(percent * num) / num * 100f).ToString(),
				"%)"
			});
		}
		else if (labelType == WMG_Enums.labelTypes.Values_Only)
		{
			text2 = Mathf.Round(value).ToString();
		}
		else if (labelType == WMG_Enums.labelTypes.Percents_Only)
		{
			text2 = (Mathf.Round(percent * num) / num * 100f).ToString() + "%";
		}
		else if (labelType == WMG_Enums.labelTypes.Values_Percents)
		{
			text2 = Mathf.Round(value).ToString() + " (" + (Mathf.Round(percent * num) / num * 100f).ToString() + "%)";
		}
		return text2;
	}

	public List<GameObject> NodesParent
	{
		get
		{
			return this.nodesParent;
		}
	}

	public List<GameObject> LinksParent
	{
		get
		{
			return this.linksParent;
		}
	}

	public GameObject CreateNode(UnityEngine.Object prefabNode, GameObject parent)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(prefabNode) as GameObject;
		Vector3 localScale = gameObject.transform.localScale;
		Vector3 localEulerAngles = gameObject.transform.localEulerAngles;
		GameObject parent2 = parent;
		if (parent == null)
		{
			parent2 = base.gameObject;
		}
		base.changeSpriteParent(gameObject, parent2);
		gameObject.transform.localScale = localScale;
		gameObject.transform.localEulerAngles = localEulerAngles;
		WMG_Node component = gameObject.GetComponent<WMG_Node>();
		component.SetID(this.nodesParent.Count);
		this.nodesParent.Add(gameObject);
		return gameObject;
	}

	public GameObject CreateLink(WMG_Node fromNode, GameObject toNode, UnityEngine.Object prefabLink, GameObject parent)
	{
		GameObject gameObject = fromNode.CreateLink(toNode, prefabLink, this.linksParent.Count, parent, true);
		this.linksParent.Add(gameObject);
		return gameObject;
	}

	public GameObject CreateLinkNoRepos(WMG_Node fromNode, GameObject toNode, UnityEngine.Object prefabLink, GameObject parent)
	{
		GameObject gameObject = fromNode.CreateLink(toNode, prefabLink, this.linksParent.Count, parent, false);
		this.linksParent.Add(gameObject);
		return gameObject;
	}

	public WMG_Link GetLink(WMG_Node fromNode, WMG_Node toNode)
	{
		foreach (GameObject gameObject in fromNode.links)
		{
			WMG_Link component = gameObject.GetComponent<WMG_Link>();
			WMG_Node component2 = component.toNode.GetComponent<WMG_Node>();
			if (component2.id != toNode.id)
			{
				component2 = component.fromNode.GetComponent<WMG_Node>();
			}
			if (component2.id == toNode.id)
			{
				return component;
			}
		}
		return null;
	}

	public GameObject ReplaceNodeWithNewPrefab(WMG_Node theNode, UnityEngine.Object prefabNode)
	{
		GameObject gameObject = this.CreateNode(prefabNode, theNode.transform.parent.gameObject);
		WMG_Node component = gameObject.GetComponent<WMG_Node>();
		component.numLinks = theNode.numLinks;
		component.links = theNode.links;
		component.linkAngles = theNode.linkAngles;
		component.SetID(theNode.id);
		component.name = theNode.name;
		gameObject.transform.position = theNode.transform.position;
		for (int i = 0; i < theNode.numLinks; i++)
		{
			WMG_Link component2 = theNode.links[i].GetComponent<WMG_Link>();
			WMG_Node component3 = component2.fromNode.GetComponent<WMG_Node>();
			if (component3.id == theNode.id)
			{
				component2.fromNode = gameObject;
			}
			else
			{
				component2.toNode = gameObject;
			}
		}
		this.nodesParent.Remove(theNode.gameObject);
		UnityEngine.Object.Destroy(theNode.gameObject);
		return gameObject;
	}

	public void DeleteNode(WMG_Node theNode)
	{
		int id = theNode.id;
		foreach (GameObject gameObject in this.nodesParent)
		{
			WMG_Node component = gameObject.GetComponent<WMG_Node>();
			if (component != null && component.id == this.nodesParent.Count - 1)
			{
				component.SetID(id);
				while (theNode.numLinks > 0)
				{
					WMG_Link component2 = theNode.links[0].GetComponent<WMG_Link>();
					this.DeleteLink(component2);
				}
				this.nodesParent.Remove(theNode.gameObject);
				UnityEngine.Object.DestroyImmediate(theNode.gameObject);
				break;
			}
		}
	}

	public void DeleteLink(WMG_Link theLink)
	{
		WMG_Node component = theLink.fromNode.GetComponent<WMG_Node>();
		WMG_Node component2 = theLink.toNode.GetComponent<WMG_Node>();
		for (int i = 0; i < component.numLinks; i++)
		{
			WMG_Link component3 = component.links[i].GetComponent<WMG_Link>();
			if (component3.id == theLink.id)
			{
				component.numLinks--;
				component.links[i] = component.links[component.numLinks];
				break;
			}
		}
		for (int j = 0; j < component2.numLinks; j++)
		{
			WMG_Link component4 = component2.links[j].GetComponent<WMG_Link>();
			if (component4.id == theLink.id)
			{
				component2.numLinks--;
				component2.links[j] = component2.links[component2.numLinks];
				break;
			}
		}
		int id = theLink.id;
		foreach (GameObject gameObject in this.linksParent)
		{
			WMG_Link component5 = gameObject.GetComponent<WMG_Link>();
			if (component5 != null && component5.id == this.linksParent.Count - 1)
			{
				component5.SetId(id);
				this.linksParent.Remove(theLink.gameObject);
				UnityEngine.Object.DestroyImmediate(theLink.gameObject);
				component.links.RemoveAt(component.numLinks);
				component.linkAngles.RemoveAt(component.numLinks);
				component2.links.RemoveAt(component2.numLinks);
				component2.linkAngles.RemoveAt(component2.numLinks);
				break;
			}
		}
	}

	private List<GameObject> nodesParent = new List<GameObject>();

	private List<GameObject> linksParent = new List<GameObject>();

	private WMG_Data_Generators data_gen = new WMG_Data_Generators();

	private WMG_Caching_Functions caching = new WMG_Caching_Functions();

	private WMG_Path_Finding path_find = new WMG_Path_Finding();
}
