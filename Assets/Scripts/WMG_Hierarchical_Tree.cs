using System;
using System.Collections.Generic;
using UnityEngine;

public class WMG_Hierarchical_Tree : WMG_Graph_Manager
{
	public WMG_Hierarchical_Tree.ResizeProperties resizeProperties
	{
		get
		{
			return this._resizeProperties;
		}
		set
		{
			if (this._resizeProperties != value)
			{
				this._resizeProperties = value;
				this.resizeC.Changed();
			}
		}
	}

	public bool resizeEnabled
	{
		get
		{
			return this._resizeEnabled;
		}
		set
		{
			if (this._resizeEnabled != value)
			{
				this._resizeEnabled = value;
				this.resizeC.Changed();
			}
		}
	}

	public int nodeWidthHeight
	{
		get
		{
			return this._nodeWidthHeight;
		}
		set
		{
			if (this._nodeWidthHeight != value)
			{
				this._nodeWidthHeight = value;
				this.treeC.Changed();
			}
		}
	}

	public float nodeRadius
	{
		get
		{
			return this._nodeRadius;
		}
		set
		{
			if (this._nodeRadius != value)
			{
				this._nodeRadius = value;
				this.treeC.Changed();
			}
		}
	}

	public bool squareNodes
	{
		get
		{
			return this._squareNodes;
		}
		set
		{
			if (this._squareNodes != value)
			{
				this._squareNodes = value;
				this.treeC.Changed();
			}
		}
	}

	private void Start()
	{
		this.Init();
		this.PauseCallbacks();
		this.CreateNodes();
		this.CreatedLinks();
		this.treeC.Changed();
	}

	public void Init()
	{
		if (this.hasInit)
		{
			return;
		}
		this.hasInit = true;
		this.changeObjs.Add(this.treeC);
		this.changeObjs.Add(this.resizeC);
		this.treeC.OnChange += this.refresh;
		this.resizeC.OnChange += this.ResizeChanged;
		this.setOriginalPropertyValues();
		this.PauseCallbacks();
	}

	private void Update()
	{
		this.updateFromResize();
		this.Refresh();
	}

	public void setOriginalPropertyValues()
	{
		this.cachedContainerWidth = base.getSpriteWidth(base.gameObject);
		this.cachedContainerHeight = base.getSpriteHeight(base.gameObject);
		this.origWidth = base.getSpriteWidth(base.gameObject);
		this.origHeight = base.getSpriteHeight(base.gameObject);
		this.origNodeWidthHeight = this.nodeWidthHeight;
		this.origNodeRadius = this.nodeRadius;
	}

	private void ResizeChanged()
	{
		this.UpdateFromContainer();
	}

	private void UpdateFromContainer()
	{
		if (this.resizeEnabled)
		{
			Vector2 vector = new Vector2(this.cachedContainerWidth / this.origWidth, this.cachedContainerHeight / this.origHeight);
			float num = vector.x;
			if (vector.y < num)
			{
				num = vector.y;
			}
			if ((this.resizeProperties & WMG_Hierarchical_Tree.ResizeProperties.NodeWidthHeight) == WMG_Hierarchical_Tree.ResizeProperties.NodeWidthHeight)
			{
				this.nodeWidthHeight = Mathf.RoundToInt(this.getNewResizeVariable(num, (float)this.origNodeWidthHeight));
			}
			if ((this.resizeProperties & WMG_Hierarchical_Tree.ResizeProperties.NodeRadius) == WMG_Hierarchical_Tree.ResizeProperties.NodeRadius)
			{
				this.nodeRadius = this.getNewResizeVariable(num, this.origNodeRadius);
			}
		}
	}

	private float getNewResizeVariable(float sizeFactor, float variable)
	{
		return variable + (sizeFactor - 1f) * variable;
	}

	private void updateFromResize()
	{
		bool flag = false;
		base.updateCacheAndFlag<float>(ref this.cachedContainerWidth, base.getSpriteWidth(base.gameObject), ref flag);
		base.updateCacheAndFlag<float>(ref this.cachedContainerHeight, base.getSpriteHeight(base.gameObject), ref flag);
		if (flag)
		{
			this.treeC.Changed();
			this.resizeC.Changed();
		}
	}

	public void Refresh()
	{
		this.ResumeCallbacks();
		this.PauseCallbacks();
	}

	private void PauseCallbacks()
	{
		for (int i = 0; i < this.changeObjs.Count; i++)
		{
			this.changeObjs[i].changesPaused = true;
			this.changeObjs[i].changePaused = false;
		}
	}

	private void ResumeCallbacks()
	{
		for (int i = 0; i < this.changeObjs.Count; i++)
		{
			this.changeObjs[i].changesPaused = false;
			if (this.changeObjs[i].changePaused)
			{
				this.changeObjs[i].Changed();
			}
		}
	}

	public List<GameObject> getNodesInRow(int rowNum)
	{
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < this.treeNodes.Count; i++)
		{
			if (Mathf.Approximately(base.getSpritePositionY(this.treeNodes[i]), (float)(-(float)rowNum) * this._gridLengthY))
			{
				list.Add(this.treeNodes[i]);
			}
		}
		return list;
	}

	public List<GameObject> getNodesInColumn(int colNum)
	{
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < this.treeNodes.Count; i++)
		{
			if (Mathf.Approximately(base.getSpritePositionX(this.treeNodes[i]), (float)colNum * this._gridLengthX))
			{
				list.Add(this.treeNodes[i]);
			}
		}
		return list;
	}

	private void refresh()
	{
		int num = -1;
		int num2 = -1;
		for (int i = 0; i < this.treeNodes.Count; i++)
		{
			if (this.nodeRows[i] > num)
			{
				num = this.nodeRows[i];
			}
			if (this.nodeColumns[i] > num2)
			{
				num2 = this.nodeColumns[i];
			}
		}
		Vector2 spriteSize = base.getSpriteSize(base.gameObject);
		this._gridLengthX = spriteSize.x / (float)num2;
		this._gridLengthY = spriteSize.y / (float)num;
		for (int j = 0; j < this.treeNodes.Count; j++)
		{
			base.changeSpriteSize(this.treeNodes[j], this.nodeWidthHeight, this.nodeWidthHeight);
			this.treeNodes[j].GetComponent<WMG_Node>().radius = this.nodeRadius;
			this.treeNodes[j].GetComponent<WMG_Node>().isSquare = this.squareNodes;
			float x = ((float)this.nodeColumns[j] - 0.5f) * this._gridLengthX - spriteSize.x / 2f;
			float num3 = ((float)this.nodeRows[j] - 0.5f) * this._gridLengthY - spriteSize.y / 2f;
			this.treeNodes[j].GetComponent<WMG_Node>().Reposition(x, -num3);
		}
		for (int k = 0; k < this.treeInvisibleNodes.Count; k++)
		{
			base.changeSpritePivot(this.treeInvisibleNodes[k], WMG_Text_Functions.WMGpivotTypes.Center);
			base.changeSpriteSize(this.treeInvisibleNodes[k], this.nodeWidthHeight, this.nodeWidthHeight);
			float x2 = ((float)this.invisibleNodeColumns[k] - 0.5f) * this._gridLengthX - spriteSize.x / 2f;
			float num4 = ((float)this.invisibleNodeRows[k] - 0.5f) * this._gridLengthY - spriteSize.y / 2f;
			this.treeInvisibleNodes[k].GetComponent<WMG_Node>().Reposition(x2, -num4);
		}
	}

	public void CreateNodes()
	{
		for (int i = 0; i < this.numNodes; i++)
		{
			if (this.treeNodes.Count <= i)
			{
				UnityEngine.Object prefabNode = this.defaultNodePrefab;
				if (this.nodePrefabs.Count > i)
				{
					prefabNode = this.nodePrefabs[i];
				}
				WMG_Node component = base.CreateNode(prefabNode, this.nodeParent).GetComponent<WMG_Node>();
				this.treeNodes.Add(component.gameObject);
			}
		}
		for (int j = 0; j < this.numInvisibleNodes; j++)
		{
			if (this.treeInvisibleNodes.Count <= j)
			{
				WMG_Node component2 = base.CreateNode(this.invisibleNodePrefab, this.nodeParent).GetComponent<WMG_Node>();
				this.treeInvisibleNodes.Add(component2.gameObject);
			}
		}
	}

	public void CreatedLinks()
	{
		for (int i = 0; i < this.numLinks; i++)
		{
			if (this.treeLinks.Count <= i)
			{
				GameObject gameObject;
				if (this.linkNodeFromIDs[i] > 0)
				{
					gameObject = this.treeNodes[this.linkNodeFromIDs[i] - 1];
				}
				else
				{
					gameObject = this.treeInvisibleNodes[this.linkNodeFromIDs[i] + 1];
				}
				GameObject toNode;
				if (this.linkNodeToIDs[i] > 0)
				{
					toNode = this.treeNodes[this.linkNodeToIDs[i] - 1];
				}
				else
				{
					toNode = this.treeInvisibleNodes[this.linkNodeToIDs[i] + 1];
				}
				this.treeLinks.Add(base.CreateLinkNoRepos(gameObject.GetComponent<WMG_Node>(), toNode, this.linkPrefab, this.linkParent));
			}
		}
	}

	public GameObject nodeParent;

	public GameObject linkParent;

	public UnityEngine.Object defaultNodePrefab;

	public UnityEngine.Object linkPrefab;

	public int numNodes;

	public int numLinks;

	public List<UnityEngine.Object> nodePrefabs;

	public List<int> nodeColumns;

	public List<int> nodeRows;

	public List<int> linkNodeFromIDs;

	public List<int> linkNodeToIDs;

	public UnityEngine.Object invisibleNodePrefab;

	public int numInvisibleNodes;

	public List<int> invisibleNodeColumns;

	public List<int> invisibleNodeRows;

	private float _gridLengthX;

	private float _gridLengthY;

	[SerializeField]
	private int _nodeWidthHeight;

	[SerializeField]
	private float _nodeRadius;

	[SerializeField]
	private bool _squareNodes;

	[SerializeField]
	private bool _resizeEnabled;

	[WMG_EnumFlag]
	[SerializeField]
	private WMG_Hierarchical_Tree.ResizeProperties _resizeProperties;

	private float cachedContainerWidth;

	private float cachedContainerHeight;

	private float origWidth;

	private float origHeight;

	private int origNodeWidthHeight;

	private float origNodeRadius;

	private List<WMG_Change_Obj> changeObjs = new List<WMG_Change_Obj>();

	private WMG_Change_Obj treeC = new WMG_Change_Obj();

	public WMG_Change_Obj resizeC = new WMG_Change_Obj();

	private List<GameObject> treeNodes = new List<GameObject>();

	private List<GameObject> treeLinks = new List<GameObject>();

	private List<GameObject> treeInvisibleNodes = new List<GameObject>();

	private bool hasInit;

	[Flags]
	public enum ResizeProperties
	{
		NodeWidthHeight = 1,
		NodeRadius = 2
	}
}
