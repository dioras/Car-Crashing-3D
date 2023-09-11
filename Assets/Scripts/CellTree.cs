using System;

public class CellTree
{
	public CellTree()
	{
	}

	public CellTree(CellTreeNode root)
	{
		this.RootNode = root;
	}

	public CellTreeNode RootNode { get; private set; }
}
