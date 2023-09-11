using System;
using System.Collections.Generic;
using UnityEngine;

namespace MeshCombineStudio
{
	public class CombinedLODManager : MonoBehaviour
	{
		private void Awake()
		{
			this.cameraMainT = Camera.main.transform;
		}

		private void InitOctree()
		{
			this.octree = new CombinedLODManager.Cell(this.octreeCenter, this.octreeSize, this.maxLevels);
		}

		private void Start()
		{
			if (this.search)
			{
				this.search = false;
				this.InitOctree();
				this.Search();
			}
		}

		private void Update()
		{
			if (this.octree.cellsUsed != null)
			{
				this.Lod(this.lodMode);
			}
		}

		public void UpdateLods(MeshCombiner meshCombiner, int lodAmount)
		{
			if (this.lods != null && this.lods.Length == lodAmount)
			{
				return;
			}
			this.lods = new CombinedLODManager.LOD[lodAmount];
			float[] array = new float[lodAmount];
			for (int i = 0; i < this.lods.Length; i++)
			{
				this.lods[i] = new CombinedLODManager.LOD();
				if (this.lodDistanceMode == CombinedLODManager.LodDistanceMode.Automatic)
				{
					array[i] = (float)(meshCombiner.cellSize * i);
				}
				else if (this.distances != null && i < this.distances.Length)
				{
					array[i] = this.distances[i];
				}
			}
			this.distances = array;
		}

		public void UpdateDistances(MeshCombiner meshCombiner)
		{
			if (this.lodDistanceMode != CombinedLODManager.LodDistanceMode.Automatic)
			{
				return;
			}
			for (int i = 0; i < this.distances.Length; i++)
			{
				this.distances[i] = (float)(meshCombiner.cellSize * i);
			}
		}

		public void Search()
		{
			for (int i = 0; i < this.lods.Length; i++)
			{
				this.lods[i].searchParent.gameObject.SetActive(true);
				MeshRenderer[] componentsInChildren = this.lods[i].searchParent.GetComponentsInChildren<MeshRenderer>();
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					this.octree.AddMeshRenderer(componentsInChildren[j], componentsInChildren[j].transform.position, i, this.lods.Length);
				}
			}
		}

		public void ResetOctree()
		{
			if (this.octree == null)
			{
				return;
			}
			this.octree.cells = null;
			this.octree.cellsUsed = null;
			for (int i = 0; i < this.lods.Length; i++)
			{
				if (this.lods[i].searchParent != null)
				{
					UnityEngine.Object.Destroy(this.lods[i].searchParent.gameObject);
				}
			}
		}

		public void Lod(CombinedLODManager.LodMode lodMode)
		{
			Vector3 position = this.cameraMainT.position;
			for (int i = 0; i < this.lods.Length - 1; i++)
			{
				this.lods[i].sphere.center = position;
				this.lods[i].sphere.radius = this.distances[i + 1];
			}
			if (lodMode == CombinedLODManager.LodMode.Automatic)
			{
				this.octree.AutoLodInternal(this.lods, (!this.lodCulled) ? -1f : this.lodCullDistance);
			}
			else
			{
				this.octree.LodInternal(this.lods, this.showLod);
			}
		}

		private void OnDrawGizmosSelected()
		{
			if (this.drawGizmos && this.octree != null && this.octree.cells != null)
			{
				this.octree.DrawGizmos(this.lods);
			}
		}

		public bool drawGizmos = true;

		public CombinedLODManager.LOD[] lods;

		public float[] distances;

		public CombinedLODManager.LodDistanceMode lodDistanceMode;

		public CombinedLODManager.LodMode lodMode;

		public int showLod;

		public bool lodCulled;

		public float lodCullDistance = 500f;

		public Vector3 octreeCenter = Vector3.zero;

		public Vector3 octreeSize = new Vector3(256f, 256f, 256f);

		public int maxLevels = 4;

		public bool search = true;

		private CombinedLODManager.Cell octree;

		private Transform cameraMainT;

		public enum LodMode
		{
			Automatic,
			DebugLod
		}

		public enum LodDistanceMode
		{
			Automatic,
			Manual
		}

		[Serializable]
		public class LOD
		{
			public LOD()
			{
			}

			public LOD(Transform searchParent)
			{
				this.searchParent = searchParent;
			}

			public Transform searchParent;

			public Sphere3 sphere = new Sphere3();
		}

		public class Cell : BaseOctree.Cell
		{
			public Cell()
			{
			}

			public Cell(Vector3 position, Vector3 size, int maxLevels) : base(position, size, maxLevels)
			{
			}

			public void AddMeshRenderer(MeshRenderer mr, Vector3 position, int lodLevel, int lodLevels)
			{
				if (base.InsideBounds(position))
				{
					this.AddMeshRendererInternal(mr, position, lodLevel, lodLevels);
				}
			}

			private void AddMeshRendererInternal(MeshRenderer mr, Vector3 position, int lodLevel, int lodLevels)
			{
				if (this.level == this.maxLevels)
				{
					CombinedLODManager.MaxCell maxCell = (CombinedLODManager.MaxCell)this;
					if (maxCell.mrList == null)
					{
						maxCell.mrList = new List<MeshRenderer>[lodLevels];
					}
					List<MeshRenderer>[] mrList = maxCell.mrList;
					if (mrList[lodLevel] == null)
					{
						mrList[lodLevel] = new List<MeshRenderer>();
					}
					mrList[lodLevel].Add(mr);
					maxCell.currentLod = -1;
				}
				else
				{
					bool flag;
					int num = base.AddCell<CombinedLODManager.Cell, CombinedLODManager.MaxCell>(ref this.cells, position, out flag);
					this.cells[num].box = new AABB3(this.cells[num].bounds.min, this.cells[num].bounds.max);
					this.cells[num].AddMeshRendererInternal(mr, position, lodLevel, lodLevels);
				}
			}

			public void AutoLodInternal(CombinedLODManager.LOD[] lods, float lodCulledDistance)
			{
				if (this.level == this.maxLevels)
				{
					CombinedLODManager.MaxCell maxCell = (CombinedLODManager.MaxCell)this;
					if (lodCulledDistance != -1f)
					{
						float sqrMagnitude = (this.bounds.center - lods[0].sphere.center).sqrMagnitude;
						if (sqrMagnitude > lodCulledDistance * lodCulledDistance)
						{
							if (maxCell.currentLod != -1)
							{
								for (int i = 0; i < lods.Length; i++)
								{
									for (int j = 0; j < maxCell.mrList[i].Count; j++)
									{
										maxCell.mrList[i][j].enabled = false;
									}
								}
								maxCell.currentLod = -1;
							}
							return;
						}
					}
					for (int k = 0; k < lods.Length; k++)
					{
						bool flag = k >= lods.Length - 1 || Mathw.IntersectAABB3Sphere3(this.box, lods[k].sphere);
						if (flag)
						{
							if (maxCell.currentLod != k)
							{
								for (int l = 0; l < lods.Length; l++)
								{
									bool enabled = l == k;
									for (int m = 0; m < maxCell.mrList[l].Count; m++)
									{
										maxCell.mrList[l][m].enabled = enabled;
									}
								}
								maxCell.currentLod = k;
							}
							break;
						}
					}
				}
				else
				{
					for (int n = 0; n < 8; n++)
					{
						if (this.cellsUsed[n])
						{
							this.cells[n].AutoLodInternal(lods, lodCulledDistance);
						}
					}
				}
			}

			public void LodInternal(CombinedLODManager.LOD[] lods, int lodLevel)
			{
				if (this.level == this.maxLevels)
				{
					CombinedLODManager.MaxCell maxCell = (CombinedLODManager.MaxCell)this;
					if (maxCell.currentLod != lodLevel)
					{
						for (int i = 0; i < lods.Length; i++)
						{
							bool enabled = i == lodLevel;
							for (int j = 0; j < maxCell.mrList[i].Count; j++)
							{
								maxCell.mrList[i][j].enabled = enabled;
							}
						}
						maxCell.currentLod = lodLevel;
					}
				}
				else
				{
					for (int k = 0; k < 8; k++)
					{
						if (this.cellsUsed[k])
						{
							this.cells[k].LodInternal(lods, lodLevel);
						}
					}
				}
			}

			public void DrawGizmos(CombinedLODManager.LOD[] lods)
			{
				for (int i = 0; i < lods.Length; i++)
				{
					if (i == 0)
					{
						Gizmos.color = Color.red;
					}
					else if (i == 1)
					{
						Gizmos.color = Color.green;
					}
					else if (i == 2)
					{
						Gizmos.color = Color.yellow;
					}
					else if (i == 3)
					{
						Gizmos.color = Color.blue;
					}
					Gizmos.DrawWireSphere(lods[i].sphere.center, lods[i].sphere.radius);
				}
				this.DrawGizmosInternal();
			}

			public void DrawGizmosInternal()
			{
				if (this.level == this.maxLevels)
				{
					CombinedLODManager.MaxCell maxCell = (CombinedLODManager.MaxCell)this;
					if (maxCell.currentLod == 0)
					{
						Gizmos.color = Color.red;
					}
					else if (maxCell.currentLod == 1)
					{
						Gizmos.color = Color.green;
					}
					else if (maxCell.currentLod == 2)
					{
						Gizmos.color = Color.yellow;
					}
					else if (maxCell.currentLod == 3)
					{
						Gizmos.color = Color.blue;
					}
					Gizmos.DrawWireCube(this.bounds.center, this.bounds.size - new Vector3(0.25f, 0.25f, 0.25f));
					Gizmos.color = Color.white;
				}
				else
				{
					for (int i = 0; i < 8; i++)
					{
						if (this.cellsUsed[i])
						{
							this.cells[i].DrawGizmosInternal();
						}
					}
				}
			}

			public new CombinedLODManager.Cell[] cells;

			private AABB3 box;
		}

		public class MaxCell : CombinedLODManager.Cell
		{
			public List<MeshRenderer>[] mrList;

			public int currentLod;
		}
	}
}
