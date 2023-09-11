using System;
using System.Collections.Generic;
using UnityEngine;

namespace MeshCombineStudio
{
	public class MeshCombiner : MonoBehaviour
	{
		private void Awake()
		{
			MeshCombiner.instance = this;
			this.StartRuntime();
		}

		private void StartRuntime()
		{
			if (this.combineInRuntime && this.combineOnStart)
			{
				this.CombineLods();
				this.ExecuteHandleObjects(false);
			}
			if (this.useCombineSwapKey)
			{
				base.gameObject.AddComponent<SwapCombineKey>();
			}
		}

		private void OnDestroy()
		{
			MeshCombiner.instance = null;
		}

		private void GetBounds()
		{
			this.bounds = new Bounds(base.transform.position + new Vector3(0f, base.transform.lossyScale.y * 0.5f, 0f), base.transform.lossyScale);
		}

		private void InitLists()
		{
			this.newVertices = new List<Vector3>(65534);
			this.newNormals = new List<Vector3>(65534);
			this.newTangents = new List<Vector4>(65534);
			this.newColors = new List<Color32>(65534);
			this.newTriangles = new List<int>(196602);
			this.newUvs1 = new List<Vector2>(65534);
			this.newUvs2 = new List<Vector2>(65534);
			this.newUvs3 = new List<Vector2>(65534);
			this.newUvs4 = new List<Vector2>(65534);
			this.vertices = new List<Vector3>(65534);
			this.normals = new List<Vector3>(65534);
			this.tangents = new List<Vector4>(65534);
			this.colors = new List<Color32>(65534);
			this.uvs1 = new List<Vector2>(65534);
			this.uvs2 = new List<Vector2>(65534);
			this.uvs3 = new List<Vector2>(65534);
			this.uvs4 = new List<Vector2>(65534);
			this.triangles = new List<int>(196602);
		}

		private void GarbageCollectLists()
		{
			this.newVertices = (this.newNormals = null);
			this.newTangents = null;
			this.newUvs1 = (this.newUvs2 = (this.newUvs3 = (this.newUvs4 = null)));
			this.newColors = null;
			this.newTriangles = null;
			this.vertices = (this.normals = null);
			this.tangents = null;
			this.uvs1 = (this.uvs2 = (this.uvs3 = (this.uvs4 = null)));
			this.colors = null;
			this.triangles = null;
		}

		public void CalcOctreeSize()
		{
			float x = base.transform.lossyScale.x;
			float num = x;
			int num2 = 0;
			while (num > (float)this.cellSize)
			{
				num /= 2f;
				num2++;
			}
			this.octree.maxLevels = num2;
			float num3 = (float)((int)Mathf.Pow(2f, (float)num2) * this.cellSize);
			this.octree.bounds.center = base.transform.position + new Vector3(0f, base.transform.lossyScale.y * 0.5f, 0f);
			this.octree.bounds.size = new Vector3(num3, num3, num3);
		}

		public void ResetOctree()
		{
			this.octreeCreated = false;
			if (this.octree == null)
			{
				this.octree = new ObjectOctree.Cell();
				return;
			}
			this.totalCombined = 0;
			BaseOctree.Cell[] cells = this.octree.cells;
			this.octree.Reset(ref cells);
		}

		public void AddToOctree()
		{
			this.originalObjectList.Clear();
			this.combinedMeshList.Clear();
			this.ResetOctree();
			this.CalcOctreeSize();
			this.GetBounds();
			ObjectOctree.lodCount = this.lodAmount;
			ObjectOctree.MaxCell.maxCellCount = 0;
			this.lodObjectCount = new int[this.lodAmount];
			this.lodObjectSearchCount = new int[this.lodAmount];
			for (int i = 0; i < this.lodAmount; i++)
			{
				this.AddObjects(i);
			}
		}

		public void AddCombinedLODManager()
		{
			this.combinedLODManager = base.GetComponent<CombinedLODManager>();
			if (this.combinedLODManager == null)
			{
				this.combinedLODManager = base.gameObject.AddComponent<CombinedLODManager>();
			}
			this.combinedLODManager.UpdateLods(this, this.lodAmount);
		}

		public void DestroyCombinedLODManager()
		{
			this.combinedLODManager = base.GetComponent<CombinedLODManager>();
			if (this.combinedLODManager != null)
			{
				UnityEngine.Object.DestroyImmediate(this.combinedLODManager);
			}
		}

		public void DestroyCombinedGameObjects()
		{
			this.combined = false;
			for (int i = 0; i < this.combinedList.Count; i++)
			{
				if (this.combinedList[i] != null)
				{
					UnityEngine.Object.DestroyImmediate(this.combinedList[i].gameObject);
				}
			}
			this.combinedList.Clear();
		}

		public void SetCombinedGameObjects(bool active)
		{
			if (this.combinedLODManager != null)
			{
				this.combinedLODManager.enabled = active;
			}
			for (int i = 0; i < this.combinedList.Count; i++)
			{
				if (this.combinedList[i] != null)
				{
					this.combinedList[i].gameObject.SetActive(active);
				}
			}
		}

		public void SwapCombine()
		{
			if (!this.combined)
			{
				this.CombineLods();
			}
			this.combinedActive = !this.combinedActive;
			this.SetCombinedGameObjects(this.combinedActive);
			this.ExecuteHandleObjects(!this.combinedActive);
		}

		public void ExecuteHandleObjects(bool active)
		{
			if (this.originalObjects == MeshCombiner.HandleObjects.DisableRenderes)
			{
				for (int i = 0; i < this.originalObjectList.Count; i++)
				{
					this.originalObjectList[i].mr.enabled = active;
				}
			}
			else if (this.originalObjects == MeshCombiner.HandleObjects.DisableGameObject)
			{
				for (int j = 0; j < this.originalObjectList.Count; j++)
				{
					this.originalObjectList[j].go.SetActive(active);
				}
			}
			else if (this.originalObjects == MeshCombiner.HandleObjects.DisableParentGameObject)
			{
				for (int k = 0; k < this.originalObjectList.Count; k++)
				{
					MeshCombiner.CachedGameObject cachedGameObject = this.originalObjectList[k];
					if (cachedGameObject.t.parent != null)
					{
						cachedGameObject.t.parent.gameObject.SetActive(active);
					}
				}
			}
			else if (this.originalObjects == MeshCombiner.HandleObjects.DeleteRenderers)
			{
				for (int l = 0; l < this.originalObjectList.Count; l++)
				{
					MeshCombiner.CachedGameObject cachedGameObject2 = this.originalObjectList[l];
					UnityEngine.Object.Destroy(cachedGameObject2.mf);
					UnityEngine.Object.Destroy(cachedGameObject2.mr);
				}
			}
			else if (this.originalObjects == MeshCombiner.HandleObjects.DeleteGameObject)
			{
				for (int m = 0; m < this.originalObjectList.Count; m++)
				{
					MeshCombiner.CachedGameObject cachedGameObject3 = this.originalObjectList[m];
					if (cachedGameObject3.go != null)
					{
						UnityEngine.Object.Destroy(cachedGameObject3.go);
					}
				}
			}
			else if (this.originalObjects == MeshCombiner.HandleObjects.DeleteParentGameObject)
			{
				for (int n = 0; n < this.originalObjectList.Count; n++)
				{
					MeshCombiner.CachedGameObject cachedGameObject4 = this.originalObjectList[n];
					if (cachedGameObject4.t != null && cachedGameObject4.t.parent != null)
					{
						UnityEngine.Object.Destroy(cachedGameObject4.t.parent.gameObject);
					}
				}
			}
			if (this.originalObjectsLODGroups == MeshCombiner.HandleLODGroups.Disable)
			{
				for (int num = 0; num < this.originalObjectList.Count; num++)
				{
					MeshCombiner.CachedGameObject cachedGameObject5 = this.originalObjectList[num];
					if (cachedGameObject5.t != null)
					{
						LODGroup componentInParent = cachedGameObject5.t.GetComponentInParent<LODGroup>();
						if (componentInParent != null)
						{
							componentInParent.enabled = active;
						}
					}
				}
			}
			else if (this.originalObjectsLODGroups == MeshCombiner.HandleLODGroups.Delete)
			{
				for (int num2 = 0; num2 < this.originalObjectList.Count; num2++)
				{
					MeshCombiner.CachedGameObject cachedGameObject6 = this.originalObjectList[num2];
					if (cachedGameObject6.t != null)
					{
						LODGroup componentInParent2 = cachedGameObject6.t.GetComponentInParent<LODGroup>();
						if (componentInParent2 != null)
						{
							UnityEngine.Object.Destroy(componentInParent2);
						}
					}
				}
			}
		}

		public void CombineLods()
		{
			this.DestroyCombinedGameObjects();
			if (!this.octreeCreated || this.combined)
			{
				this.AddToOctree();
			}
			if (!this.octreeCreated)
			{
				return;
			}
			if (this.newVertices == null)
			{
				this.InitLists();
			}
			for (int i = 0; i < this.lodAmount; i++)
			{
				this.Combine(i);
			}
			if (this.lodAmount > 1)
			{
				if (this.combinedLODManager == null)
				{
					this.combinedLODManager = base.gameObject.AddComponent<CombinedLODManager>();
				}
				this.combinedLODManager.lods = new CombinedLODManager.LOD[this.lodAmount];
				this.combinedLODManager.distances = new float[this.lodAmount];
				for (int j = 0; j < this.lodAmount; j++)
				{
					this.combinedLODManager.lods[j] = new CombinedLODManager.LOD(this.combinedList[j]);
					this.combinedLODManager.distances[j] = (float)(j * this.cellSize);
				}
				this.combinedLODManager.ResetOctree();
				this.combinedLODManager.octreeCenter = this.octree.bounds.center;
				this.combinedLODManager.octreeSize = this.octree.bounds.size;
				this.combinedLODManager.maxLevels = this.octree.maxLevels;
			}
			this.combinedActive = true;
			this.combined = true;
			this.GarbageCollectLists();
		}

		public void Combine(int lodLevel)
		{
			this.uncombinedGO = new GameObject("_Umcombined");
			this.octree.UncombineMeshes(this, lodLevel);
			this.octree.SortObjects(lodLevel);
			this.combinedGO = new GameObject("Combined" + ((this.lodAmount <= 1) ? string.Empty : (" " + this.lodSearchText + lodLevel.ToString())));
			this.combinedGO.transform.parent = base.transform;
			if (this.useVertexOutputLimit)
			{
				this._vertexOutputLimit = this.vertexOutputLimit;
			}
			else
			{
				this._vertexOutputLimit = 65534;
			}
			this.octree.CombineMeshes(this, lodLevel);
			UnityEngine.Object.DestroyImmediate(this.uncombinedGO);
			this.combinedList.Add(this.combinedGO.transform);
		}

		public void AddObjects(int lodLevel)
		{
			if (this.searchOptions.parent == null)
			{
				UnityEngine.Debug.Log("You need to assign a 'Parent' GameObject in which meshes will be searched");
				return;
			}
			Transform[] componentsInChildren = this.searchOptions.parent.GetComponentsInChildren<Transform>();
			this.AddTransforms(componentsInChildren, lodLevel);
		}

		private void AddTransforms(Transform[] transforms, int lodLevel)
		{
			string value = this.lodSearchText + lodLevel.ToString();
			this.lodObjectSearchCount[lodLevel] = transforms.Length;
			foreach (Transform transform in transforms)
			{
				int num = 1 << transform.gameObject.layer;
				if ((this.searchOptions.layerMask.value & num) == num)
				{
					if (!this.searchOptions.useTag || transform.CompareTag(this.searchOptions.tag))
					{
						MeshRenderer component = transform.GetComponent<MeshRenderer>();
						if (!(component == null))
						{
							if (this.bounds.Contains(component.bounds.center))
							{
								if (!this.searchOptions.onlyStatic || transform.gameObject.isStatic)
								{
									MeshFilter component2 = transform.GetComponent<MeshFilter>();
									if (!(component2 == null))
									{
										Mesh sharedMesh = component2.sharedMesh;
										if (!(sharedMesh == null))
										{
											if (!this.searchOptions.useVertexInputLimit || sharedMesh.vertexCount <= this.searchOptions.vertexInputLimit)
											{
												if (this.searchOptions.nameContains)
												{
													bool flag = false;
													for (int j = 0; j < this.searchOptions.nameContainList.Count; j++)
													{
														if (Methods.Contains(transform.name, this.searchOptions.nameContainList[j]))
														{
															flag = true;
															break;
														}
													}
													if (!flag)
													{
														goto IL_239;
													}
												}
												if (this.lodAmount <= 1 || transform.name.Contains(value))
												{
													this.lodObjectCount[lodLevel]++;
													int subMeshCount = sharedMesh.subMeshCount;
													bool flag2 = this.octree.AddObject(transform, component, subMeshCount <= 1, lodLevel);
													if (flag2)
													{
														this.originalObjectList.Add(new MeshCombiner.CachedGameObject(transform.gameObject, transform, component, component2));
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				IL_239:;
			}
			if (this.lodObjectCount[lodLevel] > 0)
			{
				this.octreeCreated = true;
			}
			else
			{
				UnityEngine.Debug.Log("No matching GameObjects with LOD " + lodLevel + " 'Search Options' are found for combining.");
			}
		}

		public void CombineMeshes(SingleMeshes sortedMesh, Vector3 center)
		{
			this.totalCombined += sortedMesh.meshes.Count;
			this.splitIndex = 0;
			this.totalVertexCount = 0;
			this.totalTriangleCount = 0;
			int num = 0;
			bool flag;
			do
			{
				flag = this.CountVertices(sortedMesh);
				this.CombineMesh(sortedMesh, center);
				this.CreateMesh(null, this.combinedGO.transform, sortedMesh, center, 0, false, num++);
			}
			while (flag);
			this.splitIndex = 0;
		}

		private bool CountVertices(SingleMeshes sortedMesh)
		{
			this.totalVertices = 0;
			this.totalTriangles = 0;
			this.startIndex = this.splitIndex;
			bool flag = false;
			for (int i = this.splitIndex; i < sortedMesh.meshes.Count; i++)
			{
				MeshInfo meshInfo = sortedMesh.meshes[i];
				Mesh mesh = meshInfo.mesh;
				if (this.totalVertices + mesh.vertexCount > this._vertexOutputLimit)
				{
					this.splitIndex = i;
					flag = true;
					break;
				}
				this.totalVertices += mesh.vertexCount;
				this.totalTriangles += (int)mesh.GetIndexCount(0);
			}
			if (!flag)
			{
				this.splitIndex = sortedMesh.meshes.Count;
			}
			return flag;
		}

		private void ClearNewLists()
		{
			this.newVertices.Clear();
			this.newNormals.Clear();
			this.newTangents.Clear();
			this.newUvs1.Clear();
			this.newUvs2.Clear();
			this.newUvs3.Clear();
			this.newUvs4.Clear();
			this.newColors.Clear();
			this.newTriangles.Clear();
		}

		private void CombineMesh(SingleMeshes sortedMesh, Vector3 center)
		{
			this.totalVertexCount = 0;
			this.totalTriangleCount = 0;
			this.ClearNewLists();
			for (int i = this.startIndex; i < this.splitIndex; i++)
			{
				MeshInfo meshInfo = sortedMesh.meshes[i];
				Transform t = meshInfo.t;
				Vector3 position = t.position;
				Quaternion rotation = t.rotation;
				Vector3 lossyScale = t.lossyScale;
				Vector3 b = position - center;
				Mesh mesh = meshInfo.mesh;
				mesh.GetVertices(this.vertices);
				mesh.GetTriangles(this.triangles, 0);
				mesh.GetNormals(this.normals);
				mesh.GetTangents(this.tangents);
				this.hasUv2 = (this.hasUv3 = (this.hasUv4 = (this.hasColors = false)));
				this.vertexCount = this.vertices.Count;
				this.triangleCount = this.triangles.Count;
				mesh.GetUVs(0, this.uvs1);
				mesh.GetUVs(1, this.uvs2);
				mesh.GetUVs(2, this.uvs3);
				mesh.GetUVs(3, this.uvs4);
				mesh.GetColors(this.colors);
				if (this.uvs2.Count > 0)
				{
					this.hasUv2 = true;
				}
				if (this.uvs3.Count > 0)
				{
					this.hasUv3 = true;
				}
				if (this.uvs4.Count > 0)
				{
					this.hasUv4 = true;
				}
				if (this.colors.Count > 0)
				{
					this.hasColors = true;
				}
				for (int j = 0; j < this.vertexCount; j++)
				{
					Vector3 a = t.TransformPoint(this.vertices[j]) - position;
					this.newVertices.Add(a + b);
					this.newNormals.Add(rotation * this.normals[j]);
					Vector4 item = rotation * this.tangents[j];
					item.w = this.tangents[j].w;
					this.newTangents.Add(item);
					this.newUvs1.Add(this.uvs1[j]);
					if (this.hasUv2)
					{
						this.newUvs2.Add(this.uvs2[j]);
					}
					if (this.hasUv3)
					{
						this.newUvs3.Add(this.uvs3[j]);
					}
					if (this.hasUv4)
					{
						this.newUvs4.Add(this.uvs4[j]);
					}
					if (this.hasColors)
					{
						this.newColors.Add(this.colors[j]);
					}
				}
				for (int k = 0; k < this.triangleCount; k++)
				{
					this.newTriangles.Add(this.triangles[k] + this.totalVertexCount);
				}
				this.totalVertexCount += this.vertexCount;
				this.totalTriangleCount += this.triangleCount;
			}
		}

		private string ClusterName(string name)
		{
			int length = name.Length;
			for (int i = length - 1; i >= 0; i -= 2)
			{
				name = name.Insert(i, "-");
			}
			return name;
		}

		private MeshRenderer CreateMesh(Transform t, Transform parent, SingleMeshes sortedMesh, Vector3 center, int matIndex, bool rotate, int meshIndex)
		{
			string name;
			if (t != null)
			{
				name = t.name;
			}
			else
			{
				name = sortedMesh.mat.name;
			}
			GameObject gameObject = new GameObject(name);
			Transform transform = gameObject.transform;
			transform.parent = parent;
			transform.position = center;
			if (rotate)
			{
				transform.rotation = t.rotation;
			}
			MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
			Mesh mesh = new Mesh();
			mesh.name = name + "_" + meshIndex;
			MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
			if (t != null)
			{
				meshRenderer.sharedMaterial = t.GetComponent<MeshRenderer>().sharedMaterials[matIndex];
			}
			else
			{
				meshRenderer.sharedMaterial = sortedMesh.mat;
			}
			mesh.SetVertices(this.newVertices);
			mesh.SetTriangles(this.newTriangles, 0);
			mesh.SetTangents(this.newTangents);
			mesh.SetNormals(this.newNormals);
			mesh.SetUVs(0, this.newUvs1);
			if (this.hasUv2)
			{
				mesh.SetUVs(1, this.newUvs2);
			}
			if (this.hasUv3)
			{
				mesh.SetUVs(2, this.newUvs3);
			}
			if (this.hasUv4)
			{
				mesh.SetUVs(3, this.newUvs4);
			}
			if (this.hasColors)
			{
				mesh.SetColors(this.newColors);
			}
			meshFilter.sharedMesh = mesh;
			if (this.addMeshColliders)
			{
				gameObject.AddComponent<MeshCollider>();
			}
			gameObject.AddComponent<GarbageCollectMesh>();
			if (rotate)
			{
				this.combinedMeshList.Add(new MeshCombiner.CachedGameObject(gameObject, transform, meshRenderer, meshFilter));
			}
			return meshRenderer;
		}

		public void DisplayUVs(List<Vector2> uv)
		{
			for (int i = 0; i < uv.Count; i++)
			{
				UnityEngine.Debug.Log(uv[i].x + " " + uv[i].y);
			}
		}

		public void DisplayColors(Mesh mesh)
		{
			Color32[] colors = mesh.colors32;
			for (int i = 0; i < colors.Length; i++)
			{
				UnityEngine.Debug.Log(string.Concat(new object[]
				{
					colors[i].r,
					" ",
					colors[i].g,
					" ",
					colors[i].b,
					" ",
					colors[i].a
				}));
			}
		}

		public void UncombineMeshes(List<Transform> transforms, int lodLevel)
		{
			for (int i = 0; i < transforms.Count; i++)
			{
				Transform t = transforms[i];
				this.UncombineMesh(t, lodLevel);
			}
		}

		public int UncombineMesh(Transform t, int lodLevel)
		{
			Mesh sharedMesh = t.GetComponent<MeshFilter>().sharedMesh;
			int num = t.GetComponent<MeshRenderer>().sharedMaterials.Length;
			int subMeshCount = sharedMesh.subMeshCount;
			int num2 = (subMeshCount <= num) ? subMeshCount : num;
			Vector3 lossyScale = t.lossyScale;
			if (num2 > 1)
			{
				this.vertices.Clear();
				this.normals.Clear();
				this.tangents.Clear();
				this.uvs1.Clear();
				this.uvs2.Clear();
				this.uvs3.Clear();
				this.uvs4.Clear();
				this.colors.Clear();
				sharedMesh.GetVertices(this.vertices);
				sharedMesh.GetNormals(this.normals);
				sharedMesh.GetTangents(this.tangents);
				this.hasUv2 = (this.hasUv3 = (this.hasUv4 = (this.hasColors = false)));
				sharedMesh.GetUVs(0, this.uvs1);
				sharedMesh.GetUVs(1, this.uvs2);
				sharedMesh.GetUVs(2, this.uvs3);
				sharedMesh.GetUVs(3, this.uvs4);
				sharedMesh.GetColors(this.colors);
				if (this.uvs2.Count > 0)
				{
					this.hasUv2 = true;
				}
				if (this.uvs3.Count > 0)
				{
					this.hasUv3 = true;
				}
				if (this.uvs4.Count > 0)
				{
					this.hasUv4 = true;
				}
				if (this.colors.Count > 0)
				{
					this.hasColors = true;
				}
				for (int i = 0; i < num2; i++)
				{
					this.ClearNewLists();
					int item = 0;
					this.triangles.Clear();
					sharedMesh.GetTriangles(this.triangles, i);
					for (int j = 0; j < this.triangles.Count; j++)
					{
						this.vertexTable[this.triangles[j]] = -1;
					}
					for (int k = 0; k < this.triangles.Count; k++)
					{
						int num3 = this.triangles[k];
						if (this.vertexTable[num3] == -1)
						{
							this.newVertices.Add(Vector3.Scale(this.vertices[num3], lossyScale));
							this.newNormals.Add(this.normals[num3]);
							this.newTangents.Add(this.tangents[num3]);
							this.newUvs1.Add(this.uvs1[num3]);
							if (this.hasUv2)
							{
								this.newUvs2.Add(this.uvs2[num3]);
							}
							if (this.hasUv3)
							{
								this.newUvs3.Add(this.uvs3[num3]);
							}
							if (this.hasUv4)
							{
								this.newUvs4.Add(this.uvs4[num3]);
							}
							if (this.hasColors)
							{
								this.newColors.Add(this.colors[num3]);
							}
							this.newTriangles.Add(item);
							this.vertexTable[num3] = item++;
						}
						else
						{
							this.newTriangles.Add(this.vertexTable[num3]);
						}
					}
					MeshRenderer meshRenderer = this.CreateMesh(t, this.uncombinedGO.transform, null, t.position, i, true, i);
					this.octree.AddObject(meshRenderer.transform, meshRenderer, true, lodLevel);
				}
			}
			return num2;
		}

		private void OnDrawGizmosSelected()
		{
			if (!this.searchOptions.drawGizmos)
			{
				return;
			}
			Vector3 lossyScale = base.transform.lossyScale;
			int num = Mathf.CeilToInt(Mathf.Ceil(lossyScale.x / (float)this.cellSize) / 2f) * 2;
			int num2 = Mathf.CeilToInt(lossyScale.y / (float)this.cellSize);
			int num3 = Mathf.CeilToInt(Mathf.Ceil(lossyScale.z / (float)this.cellSize) / 2f) * 2;
			lossyScale = new Vector3((float)(num * this.cellSize), (float)(num2 * this.cellSize), (float)(num3 * this.cellSize));
			Vector3 a = base.transform.position - new Vector3(lossyScale.x * 0.5f, 0f, lossyScale.z * 0.5f);
			Vector3 b = new Vector3((float)this.cellSize * 0.5f, 0f, (float)this.cellSize * 0.5f);
			Gizmos.color = Color.white;
			if (this.octree != null && this.octreeCreated)
			{
				this.octree.Draw(true);
			}
			else
			{
				if (this.searchOptions.searchBoxGridX)
				{
					for (int i = 0; i < num; i++)
					{
						for (int j = 0; j < num3; j++)
						{
							Gizmos.DrawWireCube(a + b + new Vector3((float)(i * this.cellSize), (float)(0 * this.cellSize), (float)(j * this.cellSize)), new Vector3((float)this.cellSize, 0f, (float)this.cellSize));
						}
					}
				}
				if (this.searchOptions.searchBoxGridZ)
				{
					for (int k = 0; k < num; k++)
					{
						for (int l = 0; l < num2; l++)
						{
							Gizmos.DrawWireCube(a + new Vector3((float)this.cellSize * 0.5f, (float)this.cellSize * 0.5f, 0f) + new Vector3((float)(k * this.cellSize), (float)(l * this.cellSize), (float)(num3 * this.cellSize)), new Vector3((float)this.cellSize, (float)this.cellSize, 0f));
						}
					}
				}
				if (this.searchOptions.searchBoxGridY)
				{
					for (int m = 0; m < num3; m++)
					{
						for (int n = 0; n < num2; n++)
						{
							Gizmos.DrawWireCube(a + new Vector3(0f, (float)this.cellSize * 0.5f, (float)this.cellSize * 0.5f) + new Vector3((float)(0 * this.cellSize), (float)(n * this.cellSize), (float)(m * this.cellSize)), new Vector3(0f, (float)this.cellSize, (float)this.cellSize));
						}
					}
				}
				Gizmos.color = new Color(1f, 1f, 1f, 0.25f);
				if (this.searchOptions.searchBoxGridX)
				{
					Gizmos.DrawCube(base.transform.position, new Vector3(lossyScale.x, 0f, lossyScale.z));
				}
				if (this.searchOptions.searchBoxGridY)
				{
					Gizmos.DrawCube(base.transform.position + new Vector3((float)(-(float)(num * this.cellSize)) * 0.5f, (float)(num2 * this.cellSize) * 0.5f, 0f), new Vector3(0f, lossyScale.y, lossyScale.z));
				}
				if (this.searchOptions.searchBoxGridZ)
				{
					Gizmos.DrawCube(base.transform.position + new Vector3(0f, (float)(num2 * this.cellSize) * 0.5f, (float)(num3 * this.cellSize) * 0.5f), new Vector3(lossyScale.x, lossyScale.y, 0f));
				}
				Gizmos.color = Color.white;
			}
			this.GetBounds();
			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(this.bounds.center, this.bounds.size);
			Gizmos.color = Color.white;
		}

		public static MeshCombiner instance;

		public List<Transform> combinedList = new List<Transform>();

		public ObjectOctree.Cell octree;

		[NonSerialized]
		public bool octreeCreated;

		public int cellSize = 32;

		public bool useVertexOutputLimit;

		public int vertexOutputLimit = 65534;

		public int[] lodObjectCount;

		public int[] lodObjectSearchCount;

		private int _vertexOutputLimit;

		public bool combineInRuntime;

		public bool combineOnStart = true;

		public bool useCombineSwapKey;

		public KeyCode combineSwapKey = KeyCode.Tab;

		public MeshCombiner.HandleObjects originalObjects = MeshCombiner.HandleObjects.DisableRenderes;

		public MeshCombiner.HandleLODGroups originalObjectsLODGroups = MeshCombiner.HandleLODGroups.Disable;

		public bool addMeshColliders;

		public int lodAmount = 1;

		public string lodSearchText = "LOD";

		public MeshCombiner.SearchOptions searchOptions;

		public Vector3 oldPosition;

		public Vector3 oldScale;

		private List<MeshCombiner.CachedGameObject> originalObjectList = new List<MeshCombiner.CachedGameObject>();

		private List<MeshCombiner.CachedGameObject> combinedMeshList = new List<MeshCombiner.CachedGameObject>();

		public bool combined;

		public CombinedLODManager combinedLODManager;

		public bool combinedActive;

		private List<Vector3> newVertices;

		private List<Vector3> newNormals;

		private List<Vector4> newTangents;

		private List<Color32> newColors;

		private List<int> newTriangles;

		private List<Vector2> newUvs1;

		private List<Vector2> newUvs2;

		private List<Vector2> newUvs3;

		private List<Vector2> newUvs4;

		private List<Vector3> vertices;

		private List<Vector3> normals;

		private List<Vector4> tangents;

		private List<Color32> colors;

		private List<Vector2> uvs1;

		private List<Vector2> uvs2;

		private List<Vector2> uvs3;

		private List<Vector2> uvs4;

		private List<int> triangles;

		private bool hasUv2;

		private bool hasUv3;

		private bool hasUv4;

		private bool hasColors;

		private int[] matTriangles;

		private int vertexCount;

		private int triangleCount;

		private int splitIndex;

		private int startIndex;

		private int totalVertexCount;

		private int totalTriangleCount;

		private int totalVertices;

		private int totalTriangles;

		private int totalCombined;

		private GameObject combinedGO;

		private GameObject uncombinedGO;

		private Bounds bounds;

		private int subTriangleCountOld;

		private int[] vertexTable = new int[65534];

		public enum HandleObjects
		{
			None,
			DisableRenderes,
			DisableGameObject,
			DisableParentGameObject,
			DeleteRenderers,
			DeleteGameObject,
			DeleteParentGameObject
		}

		public enum HandleLODGroups
		{
			None,
			Disable,
			Delete
		}

		[Serializable]
		public class SearchOptions
		{
			public SearchOptions(GameObject parent)
			{
				this.parent = parent;
			}

			public GameObject parent;

			public bool drawGizmos = true;

			public bool searchBoxGridX = true;

			public bool searchBoxGridY = true;

			public bool searchBoxGridZ = true;

			public bool searchBoxSquare;

			public bool useVertexInputLimit;

			public int vertexInputLimit = 8000;

			public LayerMask layerMask = -1;

			public bool useTag;

			public string tag;

			public bool nameContains;

			public List<string> nameContainList = new List<string>();

			public bool onlyStatic = true;
		}

		[Serializable]
		public class CachedGameObject
		{
			public CachedGameObject(GameObject go, Transform t, MeshRenderer mr, MeshFilter mf)
			{
				this.go = go;
				this.t = t;
				this.mr = mr;
				this.mf = mf;
			}

			public GameObject go;

			public Transform t;

			public MeshRenderer mr;

			public MeshFilter mf;
		}
	}
}
