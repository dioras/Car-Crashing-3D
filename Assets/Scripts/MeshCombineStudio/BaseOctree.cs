using System;
using UnityEngine;

namespace MeshCombineStudio
{
	public class BaseOctree
	{
		public class Cell
		{
			public Cell()
			{
			}

			public Cell(Vector3 position, Vector3 size, int maxLevels)
			{
				this.bounds = new Bounds(position, size);
				this.maxLevels = maxLevels;
			}

			public Cell(BaseOctree.Cell parent, int cellIndex, Bounds bounds)
			{
				if (parent != null)
				{
					this.maxLevels = parent.maxLevels;
					this.mainParent = parent.mainParent;
					this.level = parent.level + 1;
				}
				this.parent = parent;
				this.cellIndex = cellIndex;
				this.bounds = bounds;
			}

			public void SetCell(BaseOctree.Cell parent, int cellIndex, Bounds bounds)
			{
				if (parent != null)
				{
					this.maxLevels = parent.maxLevels;
					this.mainParent = parent.mainParent;
					this.level = parent.level + 1;
				}
				this.parent = parent;
				this.cellIndex = cellIndex;
				this.bounds = bounds;
			}

			protected int AddCell<T, U>(ref T[] cells, Vector3 position, out bool maxCellCreated) where T : BaseOctree.Cell, new() where U : BaseOctree.Cell, new()
			{
				Vector3 vector = position - this.bounds.min;
				int num = (int)(vector.x / this.bounds.extents.x);
				int num2 = (int)(vector.y / this.bounds.extents.y);
				int num3 = (int)(vector.z / this.bounds.extents.z);
				int num4 = num + num2 * 4 + num3 * 2;
				if (cells == null)
				{
					cells = new T[8];
				}
				if (this.cellsUsed == null)
				{
					this.cellsUsed = new bool[8];
				}
				if (!this.cellsUsed[num4])
				{
					Bounds bounds = new Bounds(new Vector3(this.bounds.min.x + this.bounds.extents.x * ((float)num + 0.5f), this.bounds.min.y + this.bounds.extents.y * ((float)num2 + 0.5f), this.bounds.min.z + this.bounds.extents.z * ((float)num3 + 0.5f)), this.bounds.extents);
					if (this.level == this.maxLevels - 1)
					{
						cells[num4] = (Activator.CreateInstance<U>() as T);
						cells[num4].SetCell(this, num4, bounds);
						maxCellCreated = true;
					}
					else
					{
						maxCellCreated = false;
						cells[num4] = Activator.CreateInstance<T>();
						cells[num4].SetCell(this, num4, bounds);
					}
					this.cellsUsed[num4] = true;
					this.cellCount++;
				}
				else
				{
					maxCellCreated = false;
				}
				return num4;
			}

			public void RemoveCell(int index)
			{
				this.cells[index] = null;
				this.cellsUsed[index] = false;
				this.cellCount--;
				if (this.cellCount == 0 && this.parent != null)
				{
					this.parent.RemoveCell(this.cellIndex);
				}
			}

			public bool InsideBounds(Vector3 position)
			{
				position -= this.bounds.min;
				return position.x < this.bounds.size.x && position.y < this.bounds.size.y && position.z < this.bounds.size.z && position.x > 0f && position.y > 0f && position.z > 0f;
			}

			public void Reset(ref BaseOctree.Cell[] cells)
			{
				cells = null;
				this.cellsUsed = null;
			}

			public BaseOctree.Cell mainParent;

			public BaseOctree.Cell parent;

			public BaseOctree.Cell[] cells;

			public bool[] cellsUsed;

			public Bounds bounds;

			public int cellIndex;

			public int cellCount;

			public int level;

			public int maxLevels;
		}
	}
}
