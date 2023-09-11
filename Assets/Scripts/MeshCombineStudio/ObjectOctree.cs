using System;
using System.Collections.Generic;
using UnityEngine;

namespace MeshCombineStudio
{
	public class ObjectOctree
	{
		public static int lodCount;

		public class LOD
		{
			public int GetSortMeshIndex(Material mat)
			{
				for (int i = 0; i < this.sortedMeshes.Count; i++)
				{
					if (mat == null)
					{
						UnityEngine.Debug.Log("Material null");
					}
					if (this.sortedMeshes[i].mat == null)
					{
						UnityEngine.Debug.Log("Sorted mat null");
					}
					if (this.sortedMeshes[i].mat.name == mat.name && this.sortedMeshes[i].mat.shader == mat.shader && (!mat.HasProperty("_MainTex") || (mat.HasProperty("_MainTex") && this.sortedMeshes[i].mat.GetTexture("_MainTex") == mat.GetTexture("_MainTex"))))
					{
						return i;
					}
				}
				return -1;
			}

			public List<Transform> transforms = new List<Transform>();

			public List<Transform> singleTransforms = new List<Transform>();

			public List<SingleMeshes> sortedMeshes;

			public int vertCount;

			public int objectCount;
		}

		public class MaxCell : ObjectOctree.Cell
		{
			public static int maxCellCount;

			public ObjectOctree.LOD[] lods;
		}

		public class Cell : BaseOctree.Cell
		{
			public Cell()
			{
			}

			public Cell(Vector3 position, Vector3 size, int maxLevels) : base(position, size, maxLevels)
			{
			}

			public bool AddObject(Transform t, MeshRenderer mr, bool addToSingle, int lodLevel)
			{
				Vector3 position = t.position;
				if (base.InsideBounds(position))
				{
					this.AddObjectInternal(t, position, addToSingle, lodLevel);
					return true;
				}
				return false;
			}

			private void AddObjectInternal(Transform t, Vector3 position, bool addToSingle, int lodLevel)
			{
				if (this.level == this.maxLevels)
				{
					ObjectOctree.MaxCell maxCell = (ObjectOctree.MaxCell)this;
					if (maxCell.lods == null)
					{
						maxCell.lods = new ObjectOctree.LOD[ObjectOctree.lodCount];
					}
					if (maxCell.lods[lodLevel] == null)
					{
						maxCell.lods[lodLevel] = new ObjectOctree.LOD();
					}
					ObjectOctree.LOD lod = maxCell.lods[lodLevel];
					if (lod.transforms == null)
					{
						lod.transforms = new List<Transform>();
					}
					if (addToSingle)
					{
						lod.singleTransforms.Add(t);
					}
					else
					{
						lod.transforms.Add(t);
					}
					lod.objectCount++;
					MeshFilter component = t.GetComponent<MeshFilter>();
					Mesh sharedMesh = component.sharedMesh;
					lod.vertCount += sharedMesh.vertexCount;
					return;
				}
				bool flag;
				int num = base.AddCell<ObjectOctree.Cell, ObjectOctree.MaxCell>(ref this.cells, position, out flag);
				if (flag)
				{
					ObjectOctree.MaxCell.maxCellCount++;
				}
				this.cells[num].AddObjectInternal(t, position, addToSingle, lodLevel);
			}

			public void SortObjects(int lodLevel)
			{
				if (this.level == this.maxLevels)
				{
					ObjectOctree.MaxCell maxCell = (ObjectOctree.MaxCell)this;
					ObjectOctree.LOD lod = maxCell.lods[lodLevel];
					if (lod == null)
					{
						return;
					}
					lod.sortedMeshes = new List<SingleMeshes>();
					for (int i = 0; i < lod.singleTransforms.Count; i++)
					{
						Transform transform = lod.singleTransforms[i];
						MeshFilter component = transform.GetComponent<MeshFilter>();
						MeshRenderer component2 = transform.GetComponent<MeshRenderer>();
						Material sharedMaterial = component2.sharedMaterial;
						Mesh sharedMesh = component.sharedMesh;
						int sortMeshIndex = lod.GetSortMeshIndex(sharedMaterial);
						if (sortMeshIndex != -1)
						{
							lod.sortedMeshes[sortMeshIndex].meshes.Add(new MeshInfo(transform, sharedMesh));
						}
						else
						{
							lod.sortedMeshes.Add(new SingleMeshes(transform, sharedMaterial, sharedMesh));
						}
					}
				}
				else
				{
					for (int j = 0; j < 8; j++)
					{
						if (this.cellsUsed[j])
						{
							this.cells[j].SortObjects(lodLevel);
						}
					}
				}
			}

			public void SetObjectsActive(bool active, int lodLevel)
			{
				if (this.level == this.maxLevels)
				{
					ObjectOctree.MaxCell maxCell = (ObjectOctree.MaxCell)this;
					ObjectOctree.LOD lod = maxCell.lods[lodLevel];
					for (int i = 0; i < lod.sortedMeshes.Count; i++)
					{
					}
				}
				else
				{
					for (int j = 0; j < 8; j++)
					{
						if (this.cellsUsed[j])
						{
							this.cells[j].SetObjectsActive(active, lodLevel);
						}
					}
				}
			}

			public void CombineMeshes(MeshCombiner meshCombiner, int lodLevel)
			{
				if (this.level == this.maxLevels)
				{
					ObjectOctree.MaxCell maxCell = (ObjectOctree.MaxCell)this;
					ObjectOctree.LOD lod = maxCell.lods[lodLevel];
					if (lod == null)
					{
						return;
					}
					for (int i = 0; i < lod.sortedMeshes.Count; i++)
					{
						meshCombiner.CombineMeshes(lod.sortedMeshes[i], this.bounds.center);
					}
				}
				else
				{
					for (int j = 0; j < 8; j++)
					{
						if (this.cellsUsed[j])
						{
							this.cells[j].CombineMeshes(meshCombiner, lodLevel);
						}
					}
				}
			}

			public void UncombineMeshes(MeshCombiner meshCombiner, int lodLevel)
			{
				if (this.level == this.maxLevels)
				{
					ObjectOctree.MaxCell maxCell = (ObjectOctree.MaxCell)this;
					ObjectOctree.LOD lod = maxCell.lods[lodLevel];
					if (lod == null)
					{
						UnityEngine.Debug.Log("-------------");
						for (int i = 0; i < 3; i++)
						{
							if (maxCell.lods[i] == null)
							{
								UnityEngine.Debug.Log(i);
							}
						}
						UnityEngine.Debug.Log("-------------");
						return;
					}
					meshCombiner.UncombineMeshes(lod.transforms, lodLevel);
				}
				else
				{
					for (int j = 0; j < 8; j++)
					{
						if (this.cellsUsed[j])
						{
							if (this.cells[j] == null)
							{
								UnityEngine.Debug.Log(j);
							}
							this.cells[j].UncombineMeshes(meshCombiner, lodLevel);
						}
					}
				}
			}

			public void Draw(bool onlyMaxLevel)
			{
				if (!onlyMaxLevel || this.level == this.maxLevels)
				{
					Gizmos.DrawWireCube(this.bounds.center, this.bounds.size);
					if (this.level == this.maxLevels)
					{
						return;
					}
				}
				if (this.cells == null)
				{
					UnityEngine.Debug.Log(this.level);
				}
				if (this.cellsUsed == null)
				{
					UnityEngine.Debug.Log("f " + this.level);
				}
				for (int i = 0; i < 8; i++)
				{
					if (this.cellsUsed[i])
					{
						this.cells[i].Draw(onlyMaxLevel);
					}
				}
			}

			public new ObjectOctree.Cell[] cells;
		}
	}
}
