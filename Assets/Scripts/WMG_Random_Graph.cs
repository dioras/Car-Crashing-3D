using System;
using System.Collections.Generic;
using UnityEngine;

public class WMG_Random_Graph : WMG_Graph_Manager
{
	private void Awake()
	{
		if (this.createOnStart)
		{
			this.GenerateGraph();
		}
	}

	public List<GameObject> GenerateGraph()
	{
		GameObject gameObject = base.CreateNode(this.nodePrefab, null);
		WMG_Node component = gameObject.GetComponent<WMG_Node>();
		return this.GenerateGraphFromNode(component);
	}

	public List<GameObject> GenerateGraphFromNode(WMG_Node fromNode)
	{
		List<GameObject> list = new List<GameObject>();
		list.Add(fromNode.gameObject);
		GameObject[] array = new GameObject[this.numNodes];
		bool[] array2 = new bool[this.numNodes];
		GameObject gameObject = fromNode.gameObject;
		int num = 0;
		int num2 = 0;
		int num3 = base.NodesParent.Count - 1;
		array[num] = gameObject;
		while (base.NodesParent.Count - num3 < this.numNodes)
		{
			WMG_Node component = array[num].GetComponent<WMG_Node>();
			int num4 = UnityEngine.Random.Range(this.minRandomNumberNeighbors, this.maxRandomNumberNeighbors);
			if (this.debugRandomGraph)
			{
				UnityEngine.Debug.Log(string.Concat(new object[]
				{
					"Processesing Node: ",
					component.id,
					" with ",
					num4,
					" neighbors."
				}));
			}
			for (int i = 0; i < num4; i++)
			{
				int j = 0;
				while (j < this.maxNeighborAttempts)
				{
					float num5 = UnityEngine.Random.Range(this.minAngleRange, this.maxAngleRange);
					float num6 = UnityEngine.Random.Range(this.minRandomLinkLength, this.maxRandomLinkLength);
					bool flag = false;
					if (this.debugRandomGraph)
					{
						UnityEngine.Debug.Log(string.Concat(new object[]
						{
							"Neighbor: ",
							i,
							" Attempt: ",
							j,
							" angle: ",
							Mathf.Round(num5)
						}));
					}
					if (this.minAngle > 0f)
					{
						for (int k = 0; k < component.numLinks; k++)
						{
							float num7 = Mathf.Abs(component.linkAngles[k] - num5);
							if (num7 > 180f)
							{
								num7 = Mathf.Abs(num7 - 360f);
							}
							if (num7 < this.minAngle)
							{
								flag = true;
								break;
							}
						}
					}
					if (flag)
					{
						if (this.debugRandomGraph)
						{
							UnityEngine.Debug.Log("Failed: Angle within minAngle of existing neighbor");
						}
						j++;
					}
					else
					{
						if (this.noLinkIntersection)
						{
							float p1y = component.transform.localPosition.y + (num6 + component.radius) * Mathf.Sin(0.0174532924f * num5);
							float p1x = component.transform.localPosition.x + (num6 + component.radius) * Mathf.Cos(0.0174532924f * num5);
							float p2y = component.transform.localPosition.y + component.radius * Mathf.Sin(0.0174532924f * num5);
							float p2x = component.transform.localPosition.x + component.radius * Mathf.Cos(0.0174532924f * num5);
							foreach (GameObject gameObject2 in base.LinksParent)
							{
								WMG_Link component2 = gameObject2.GetComponent<WMG_Link>();
								if (component2.id != -1)
								{
									WMG_Node component3 = component2.fromNode.GetComponent<WMG_Node>();
									WMG_Node component4 = component2.toNode.GetComponent<WMG_Node>();
									float y = component3.transform.localPosition.y;
									float x = component3.transform.localPosition.x;
									float y2 = component4.transform.localPosition.y;
									float x2 = component4.transform.localPosition.x;
									if (WMG_Util.LineSegmentsIntersect(p1x, p1y, p2x, p2y, x, y, x2, y2))
									{
										if (this.debugRandomGraph)
										{
											UnityEngine.Debug.Log("Failed: Link intersected with existing link: " + component2.id);
										}
										flag = true;
										break;
									}
								}
							}
						}
						if (flag)
						{
							j++;
						}
						else
						{
							if (this.noNodeIntersection)
							{
								float num8 = component.transform.localPosition.y + num6 * Mathf.Sin(0.0174532924f * num5);
								float num9 = component.transform.localPosition.x + num6 * Mathf.Cos(0.0174532924f * num5);
								foreach (GameObject gameObject3 in base.NodesParent)
								{
									WMG_Node component5 = gameObject3.GetComponent<WMG_Node>();
									if (component5.id != -1)
									{
										if (Mathf.Pow(num9 - gameObject3.transform.localPosition.x, 2f) + Mathf.Pow(num8 - gameObject3.transform.localPosition.y, 2f) <= Mathf.Pow(2f * (component.radius + this.noNodeIntersectionRadiusPadding), 2f))
										{
											if (this.debugRandomGraph)
											{
												UnityEngine.Debug.Log("Failed: Node intersected with existing node: " + component5.id);
											}
											flag = true;
											break;
										}
									}
								}
							}
							if (flag)
							{
								j++;
							}
							else
							{
								if (this.noLinkNodeIntersection)
								{
									float y3 = component.transform.localPosition.y + (num6 + component.radius) * Mathf.Sin(0.0174532924f * num5);
									float x3 = component.transform.localPosition.x + (num6 + component.radius) * Mathf.Cos(0.0174532924f * num5);
									float y4 = component.transform.localPosition.y + component.radius * Mathf.Sin(0.0174532924f * num5);
									float x4 = component.transform.localPosition.x + component.radius * Mathf.Cos(0.0174532924f * num5);
									foreach (GameObject gameObject4 in base.NodesParent)
									{
										WMG_Node component6 = gameObject4.GetComponent<WMG_Node>();
										if (component.id != component6.id)
										{
											if (WMG_Util.LineIntersectsCircle(x3, y3, x4, y4, gameObject4.transform.localPosition.x, gameObject4.transform.localPosition.y, component6.radius + this.noLinkNodeIntersectionRadiusPadding))
											{
												if (this.debugRandomGraph)
												{
													UnityEngine.Debug.Log("Failed: Link intersected with existing node: " + component6.id);
												}
												flag = true;
												break;
											}
										}
									}
								}
								if (flag)
								{
									j++;
								}
								else
								{
									if (this.noLinkNodeIntersection)
									{
										float y5 = component.transform.localPosition.y + (num6 + 2f * component.radius) * Mathf.Sin(0.0174532924f * num5);
										float x5 = component.transform.localPosition.x + (num6 + 2f * component.radius) * Mathf.Cos(0.0174532924f * num5);
										foreach (GameObject gameObject5 in base.LinksParent)
										{
											WMG_Link component7 = gameObject5.GetComponent<WMG_Link>();
											if (component7.id != -1)
											{
												WMG_Node component8 = component7.fromNode.GetComponent<WMG_Node>();
												WMG_Node component9 = component7.toNode.GetComponent<WMG_Node>();
												float y6 = component8.transform.localPosition.y;
												float x6 = component8.transform.localPosition.x;
												float y7 = component9.transform.localPosition.y;
												float x7 = component9.transform.localPosition.x;
												if (WMG_Util.LineIntersectsCircle(x6, y6, x7, y7, x5, y5, component.radius + this.noLinkNodeIntersectionRadiusPadding))
												{
													if (this.debugRandomGraph)
													{
														UnityEngine.Debug.Log("Failed: Node intersected with existing link: " + component7.id);
													}
													flag = true;
													break;
												}
											}
										}
									}
									if (!flag)
									{
										gameObject = base.CreateNode(this.nodePrefab, fromNode.transform.parent.gameObject);
										list.Add(gameObject);
										array[base.NodesParent.Count - num3 - 1] = gameObject;
										float num10 = Mathf.Cos(0.0174532924f * num5) * num6;
										float num11 = Mathf.Sin(0.0174532924f * num5) * num6;
										gameObject.transform.localPosition = new Vector3(component.transform.localPosition.x + num10, component.transform.localPosition.y + num11, 0f);
										list.Add(base.CreateLink(component, gameObject, this.linkPrefab, null));
										break;
									}
									j++;
								}
							}
						}
					}
				}
				if (base.NodesParent.Count - num3 == this.numNodes)
				{
					break;
				}
			}
			array2[num] = true;
			num2++;
			if (this.centerPropogate)
			{
				num++;
			}
			else
			{
				int num12 = base.NodesParent.Count - num3 - num2;
				if (num12 > 0)
				{
					int[] array3 = new int[num12];
					int num13 = 0;
					for (int l = 0; l < this.numNodes; l++)
					{
						if (!array2[l] && l < base.NodesParent.Count - num3)
						{
							array3[num13] = l;
							num13++;
						}
					}
					num = array3[UnityEngine.Random.Range(0, num13 - 1)];
				}
			}
			if (base.NodesParent.Count - num3 == num2)
			{
				UnityEngine.Debug.Log("WMG - Warning: Only generated " + (base.NodesParent.Count - num3 - 1) + " nodes with the given parameters.");
				break;
			}
		}
		return list;
	}

	public UnityEngine.Object nodePrefab;

	public UnityEngine.Object linkPrefab;

	public int numNodes;

	public float minAngle;

	public float minAngleRange;

	public float maxAngleRange;

	public int minRandomNumberNeighbors;

	public int maxRandomNumberNeighbors;

	public float minRandomLinkLength;

	public float maxRandomLinkLength;

	public bool centerPropogate;

	public bool noLinkIntersection;

	public bool noNodeIntersection;

	public float noNodeIntersectionRadiusPadding;

	public int maxNeighborAttempts;

	public bool noLinkNodeIntersection;

	public float noLinkNodeIntersectionRadiusPadding;

	public bool createOnStart;

	public bool debugRandomGraph;
}
