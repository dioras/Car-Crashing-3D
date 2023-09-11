using System;
using System.Collections.Generic;
using UnityEngine;

public class WMG_Grid : WMG_Graph_Manager
{
	private void Update()
	{
		if (this.autoRefresh)
		{
			this.Refresh();
		}
	}

	public void Refresh()
	{
		this.checkCache();
		if (this.gridChanged)
		{
			this.refresh();
		}
		this.setCacheFlags(false);
	}

	private void checkCache()
	{
		base.updateCacheAndFlag<WMG_Grid.gridTypes>(ref this.cachedGridType, this.gridType, ref this.gridChanged);
		base.updateCacheAndFlag<UnityEngine.Object>(ref this.cachedNodePrefab, this.nodePrefab, ref this.gridChanged);
		base.updateCacheAndFlag<UnityEngine.Object>(ref this.cachedLinkPrefab, this.linkPrefab, ref this.gridChanged);
		base.updateCacheAndFlag<int>(ref this.cachedGridNumNodesX, this.gridNumNodesX, ref this.gridChanged);
		base.updateCacheAndFlag<int>(ref this.cachedGridNumNodesY, this.gridNumNodesY, ref this.gridChanged);
		base.updateCacheAndFlag<float>(ref this.cachedGridLinkLengthX, this.gridLinkLengthX, ref this.gridChanged);
		base.updateCacheAndFlag<float>(ref this.cachedGridLinkLengthY, this.gridLinkLengthY, ref this.gridChanged);
		base.updateCacheAndFlag<bool>(ref this.cachedCreateLinks, this.createLinks, ref this.gridChanged);
		base.updateCacheAndFlag<bool>(ref this.cachedNoVerticalLinks, this.noVerticalLinks, ref this.gridChanged);
		base.updateCacheAndFlag<bool>(ref this.cachedNoHorizontalLinks, this.noHorizontalLinks, ref this.gridChanged);
		base.updateCacheAndFlag<Color>(ref this.cachedLinkColor, this.linkColor, ref this.gridChanged);
		base.updateCacheAndFlag<int>(ref this.cachedLinkWidth, this.linkWidth, ref this.gridChanged);
	}

	private void setCacheFlags(bool val)
	{
		this.gridChanged = val;
	}

	public List<WMG_Node> getColumn(int colNum)
	{
		if (this.gridNodesXY.Count <= colNum)
		{
			return new List<WMG_Node>();
		}
		return this.gridNodesXY[colNum];
	}

	public void setActiveColumn(bool active, int colNum)
	{
		if (this.gridNodesXY.Count <= colNum)
		{
			return;
		}
		for (int i = 0; i < this.gridNodesXY[colNum].Count; i++)
		{
			base.SetActive(this.gridNodesXY[colNum][i].gameObject, active);
		}
	}

	public List<WMG_Node> getRow(int rowNum)
	{
		List<WMG_Node> list = new List<WMG_Node>();
		for (int i = 0; i < this.gridNodesXY.Count; i++)
		{
			list.Add(this.gridNodesXY[i][rowNum]);
		}
		return list;
	}

	public void setActiveRow(bool active, int rowNum)
	{
		for (int i = 0; i < this.gridNodesXY.Count; i++)
		{
			base.SetActive(this.gridNodesXY[i][rowNum].gameObject, active);
		}
	}

	public List<GameObject> GetNodesAndLinks()
	{
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < this.gridNodesXY.Count; i++)
		{
			for (int j = 0; j < this.gridNodesXY[i].Count; j++)
			{
				list.Add(this.gridNodesXY[i][j].gameObject);
			}
		}
		for (int k = 0; k < this.gridLinks.Count; k++)
		{
			list.Add(this.gridLinks[k]);
		}
		return list;
	}

	private void refresh()
	{
		for (int i = 0; i < this.gridNumNodesX; i++)
		{
			if (this.gridNodesXY.Count <= i)
			{
				List<WMG_Node> item = new List<WMG_Node>();
				this.gridNodesXY.Add(item);
				for (int j = 0; j < this.gridNumNodesY; j++)
				{
					WMG_Node component = base.CreateNode(this.nodePrefab, base.gameObject).GetComponent<WMG_Node>();
					this.gridNodesXY[i].Add(component);
				}
			}
		}
		for (int k = 0; k < this.gridNumNodesX; k++)
		{
			for (int l = 0; l < this.gridNumNodesY; l++)
			{
				if (this.gridNodesXY[k].Count <= l)
				{
					WMG_Node component2 = base.CreateNode(this.nodePrefab, base.gameObject).GetComponent<WMG_Node>();
					this.gridNodesXY[k].Add(component2);
				}
			}
		}
		for (int m = 0; m < this.gridNumNodesX; m++)
		{
			for (int n = this.gridNodesXY[m].Count - 1; n >= 0; n--)
			{
				if (n >= this.gridNumNodesY)
				{
					base.DeleteNode(this.gridNodesXY[m][n]);
					this.gridNodesXY[m].RemoveAt(n);
				}
			}
		}
		for (int num = this.gridNodesXY.Count - 1; num >= 0; num--)
		{
			if (num >= this.gridNumNodesX)
			{
				for (int num2 = this.gridNumNodesY - 1; num2 >= 0; num2--)
				{
					base.DeleteNode(this.gridNodesXY[num][num2]);
					this.gridNodesXY[num].RemoveAt(num2);
				}
				this.gridNodesXY.RemoveAt(num);
			}
		}
		for (int num3 = 0; num3 < this.gridNumNodesX; num3++)
		{
			for (int num4 = 0; num4 < this.gridNumNodesY; num4++)
			{
				if (num4 + 1 < this.gridNumNodesY)
				{
					this.CreateOrDeleteLink(this.gridNodesXY[num3][num4], this.gridNodesXY[num3][num4 + 1], this.noVerticalLinks);
				}
				if (num3 + 1 < this.gridNumNodesX)
				{
					this.CreateOrDeleteLink(this.gridNodesXY[num3][num4], this.gridNodesXY[num3 + 1][num4], this.noHorizontalLinks);
					if (this.gridType == WMG_Grid.gridTypes.hexagonal)
					{
						if (num3 % 2 == 1)
						{
							if (num4 + 1 < this.gridNumNodesY)
							{
								this.CreateOrDeleteLink(this.gridNodesXY[num3][num4], this.gridNodesXY[num3 + 1][num4 + 1], this.noHorizontalLinks);
							}
						}
						else if (num4 > 0)
						{
							this.CreateOrDeleteLink(this.gridNodesXY[num3][num4], this.gridNodesXY[num3 + 1][num4 - 1], this.noHorizontalLinks);
						}
					}
					else if (this.gridType == WMG_Grid.gridTypes.quadrilateral)
					{
						if (num3 % 2 == 1)
						{
							if (num4 + 1 < this.gridNumNodesY)
							{
								this.CreateOrDeleteLink(this.gridNodesXY[num3][num4], this.gridNodesXY[num3 + 1][num4 + 1], true);
							}
						}
						else if (num4 > 0)
						{
							this.CreateOrDeleteLink(this.gridNodesXY[num3][num4], this.gridNodesXY[num3 + 1][num4 - 1], true);
						}
					}
				}
			}
		}
		for (int num5 = 0; num5 < this.gridNumNodesY; num5++)
		{
			for (int num6 = 0; num6 < this.gridNumNodesX; num6++)
			{
				float x = 0f;
				float y = 0f;
				if (this.gridType == WMG_Grid.gridTypes.quadrilateral)
				{
					x = (float)num6 * this.gridLinkLengthX + (float)(num6 * 2) * this.gridNodesXY[num6][num5].radius;
					y = (float)num5 * this.gridLinkLengthY + (float)(num5 * 2) * this.gridNodesXY[num6][num5].radius;
				}
				else if (this.gridType == WMG_Grid.gridTypes.hexagonal)
				{
					int num7 = num6 % 2;
					x = (float)num6 * this.gridLinkLengthX * Mathf.Cos(0.5235988f) + (float)num6 * Mathf.Sqrt(3f) * this.gridNodesXY[num6][num5].radius;
					y = (float)num5 * this.gridLinkLengthY + (float)(num5 * 2) * this.gridNodesXY[num6][num5].radius + (float)num7 * this.gridNodesXY[num6][num5].radius + (float)num7 * this.gridLinkLengthY * Mathf.Sin(0.5235988f);
				}
				this.gridNodesXY[num6][num5].Reposition(x, y);
			}
		}
		for (int num8 = 0; num8 < this.gridLinks.Count; num8++)
		{
			if (this.gridLinks[num8] != null)
			{
				base.changeSpriteColor(this.gridLinks[num8], this.linkColor);
				base.changeSpriteWidth(this.gridLinks[num8], this.linkWidth);
			}
		}
	}

	private void CreateOrDeleteLink(WMG_Node fromNode, WMG_Node toNode, bool noVertHoriz)
	{
		WMG_Link link = base.GetLink(fromNode, toNode);
		if (link == null)
		{
			if (this.createLinks && !noVertHoriz)
			{
				this.gridLinks.Add(base.CreateLink(fromNode, toNode.gameObject, this.linkPrefab, base.gameObject));
			}
		}
		else if (!this.createLinks || noVertHoriz)
		{
			this.gridLinks.Remove(link.gameObject);
			base.DeleteLink(link);
		}
	}

	public bool autoRefresh = true;

	public WMG_Grid.gridTypes gridType;

	public UnityEngine.Object nodePrefab;

	public UnityEngine.Object linkPrefab;

	public int gridNumNodesX;

	public int gridNumNodesY;

	public float gridLinkLengthX;

	public float gridLinkLengthY;

	public bool createLinks;

	public bool noVerticalLinks;

	public bool noHorizontalLinks;

	public Color linkColor = Color.white;

	public int linkWidth;

	private List<List<WMG_Node>> gridNodesXY = new List<List<WMG_Node>>();

	private List<GameObject> gridLinks = new List<GameObject>();

	private WMG_Grid.gridTypes cachedGridType;

	private UnityEngine.Object cachedNodePrefab;

	private UnityEngine.Object cachedLinkPrefab;

	private int cachedGridNumNodesX;

	private int cachedGridNumNodesY;

	private float cachedGridLinkLengthX;

	private float cachedGridLinkLengthY;

	private bool cachedCreateLinks;

	private bool cachedNoVerticalLinks;

	private bool cachedNoHorizontalLinks;

	private Color cachedLinkColor;

	private int cachedLinkWidth;

	private bool gridChanged = true;

	public enum gridTypes
	{
		quadrilateral,
		hexagonal
	}
}
