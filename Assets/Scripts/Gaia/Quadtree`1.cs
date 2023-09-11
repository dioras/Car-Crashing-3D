using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gaia
{
	public class Quadtree<T>
	{
		public Quadtree(Rect boundaries, int nodeCapacity = 32)
		{
			this.boundaries = boundaries;
			this.nodeCapacity = nodeCapacity;
			this.nodes = new List<Quadtree<T>.QuadtreeNode>(nodeCapacity);
		}

		public int Count { get; private set; }

		public bool Insert(float x, float y, T value)
		{
			Vector2 position = new Vector2(x, y);
			Quadtree<T>.QuadtreeNode node = new Quadtree<T>.QuadtreeNode(position, value);
			return this.Insert(node);
		}

		public bool Insert(Vector2 position, T value)
		{
			Quadtree<T>.QuadtreeNode node = new Quadtree<T>.QuadtreeNode(position, value);
			return this.Insert(node);
		}

		private bool Insert(Quadtree<T>.QuadtreeNode node)
		{
			if (!this.boundaries.Contains(node.Position))
			{
				return false;
			}
			if (this.children != null)
			{
				Quadtree<T> quadtree;
				if (node.Position.y < this.children[2].boundaries.yMin)
				{
					if (node.Position.x < this.children[1].boundaries.xMin)
					{
						quadtree = this.children[0];
					}
					else
					{
						quadtree = this.children[1];
					}
				}
				else if (node.Position.x < this.children[1].boundaries.xMin)
				{
					quadtree = this.children[2];
				}
				else
				{
					quadtree = this.children[3];
				}
				if (quadtree.Insert(node))
				{
					this.Count++;
					return true;
				}
			}
			if (this.nodes.Count < this.nodeCapacity)
			{
				this.nodes.Add(node);
				this.Count++;
				return true;
			}
			this.Subdivide();
			return this.Insert(node);
		}

		public IEnumerable<T> Find(Rect range)
		{
			if (this.Count == 0)
			{
				yield break;
			}
			bool allowInverse = false;
			if (!this.boundaries.Overlaps(range, allowInverse))
			{
				yield break;
			}
			if (this.children == null)
			{
				for (int index = 0; index < this.nodes.Count; index++)
				{
					Quadtree<T>.QuadtreeNode node = this.nodes[index];
					if (range.Contains(node.Position))
					{
						yield return node.Value;
					}
				}
			}
			else
			{
				for (int index2 = 0; index2 < this.children.Length; index2++)
				{
					Quadtree<T> child = this.children[index2];
					foreach (T value in child.Find(range))
					{
						yield return value;
					}
				}
			}
			yield break;
		}

		public bool Remove(float x, float z, T value)
		{
			return this.Remove(new Vector2(x, z), value);
		}

		public bool Remove(Vector2 position, T value)
		{
			if (this.Count == 0)
			{
				return false;
			}
			if (!this.boundaries.Contains(position))
			{
				return false;
			}
			if (this.children != null)
			{
				bool result = false;
				Quadtree<T> quadtree;
				if (position.y < this.children[2].boundaries.yMin)
				{
					if (position.x < this.children[1].boundaries.xMin)
					{
						quadtree = this.children[0];
					}
					else
					{
						quadtree = this.children[1];
					}
				}
				else if (position.x < this.children[1].boundaries.xMin)
				{
					quadtree = this.children[2];
				}
				else
				{
					quadtree = this.children[3];
				}
				if (quadtree.Remove(position, value))
				{
					result = true;
					this.Count--;
				}
				if (this.Count <= this.nodeCapacity)
				{
					this.Combine();
				}
				return result;
			}
			for (int i = 0; i < this.nodes.Count; i++)
			{
				Quadtree<T>.QuadtreeNode quadtreeNode = this.nodes[i];
				if (quadtreeNode.Position.Equals(position))
				{
					this.nodes.RemoveAt(i);
					this.Count--;
					return true;
				}
			}
			return false;
		}

		private void Subdivide()
		{
			this.children = new Quadtree<T>[4];
			float num = this.boundaries.width * 0.5f;
			float num2 = this.boundaries.height * 0.5f;
			for (int i = 0; i < this.children.Length; i++)
			{
				Rect rect = new Rect(this.boundaries.xMin + num * (float)(i % 2), this.boundaries.yMin + num2 * (float)(i / 2), num, num2);
				this.children[i] = new Quadtree<T>(rect, 32);
			}
			this.Count = 0;
			for (int j = 0; j < this.nodes.Count; j++)
			{
				Quadtree<T>.QuadtreeNode node = this.nodes[j];
				this.Insert(node);
			}
			this.nodes.Clear();
		}

		private void Combine()
		{
			for (int i = 0; i < this.children.Length; i++)
			{
				Quadtree<T> quadtree = this.children[i];
				this.nodes.AddRange(quadtree.nodes);
			}
			this.children = null;
		}

		private readonly int nodeCapacity = 32;

		private readonly List<Quadtree<T>.QuadtreeNode> nodes;

		private Quadtree<T>[] children;

		private Rect boundaries;

		private class QuadtreeNode
		{
			public QuadtreeNode(Vector2 position, T value)
			{
				this.Position = position;
				this.Value = value;
			}

			public Vector2 Position { get; private set; }

			public T Value { get; private set; }
		}
	}
}
