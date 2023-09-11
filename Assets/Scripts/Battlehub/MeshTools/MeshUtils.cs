using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlehub.MeshTools
{
	public static class MeshUtils
	{
		public static CombineResult Combine(GameObject[] gameObjects, GameObject target = null)
		{
			if (gameObjects == null)
			{
				throw new ArgumentNullException("gameObjects");
			}
			if (gameObjects.Length == 0)
			{
				return null;
			}
			if (target == null)
			{
				target = gameObjects[0];
			}
			Transform[] array = new Transform[gameObjects.Length];
			for (int i = 0; i < gameObjects.Length; i++)
			{
				GameObject gameObject = gameObjects[i];
				array[i] = gameObject.transform.parent;
				gameObject.transform.SetParent(null, true);
			}
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(target);
			MeshUtils.DestroyChildren(gameObject2);
			Collider[] components = gameObject2.GetComponents<Collider>();
			foreach (Collider obj in components)
			{
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(obj);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(obj);
				}
			}
			Matrix4x4 matrix4x = Matrix4x4.TRS(Vector3.zero, target.transform.rotation, target.transform.localScale);
			Matrix4x4 worldToLocalMatrix = gameObject2.transform.worldToLocalMatrix;
			List<MeshFilter> list = new List<MeshFilter>();
			List<SkinnedMeshRenderer> list2 = new List<SkinnedMeshRenderer>();
			foreach (GameObject gameObject3 in gameObjects)
			{
				MeshFilter[] components2 = gameObject3.GetComponents<MeshFilter>();
				list.AddRange(components2);
				SkinnedMeshRenderer[] components3 = gameObject3.GetComponents<SkinnedMeshRenderer>();
				list2.AddRange(components3);
			}
			target.SetActive(false);
			List<CombineInstance> list3 = new List<CombineInstance>();
			foreach (GameObject gameObject4 in gameObjects)
			{
				List<Mesh> colliderMeshes = MeshUtils.GetColliderMeshes(gameObject4);
				for (int m = 0; m < colliderMeshes.Count; m++)
				{
					Mesh mesh = colliderMeshes[m];
					list3.Add(new CombineInstance
					{
						mesh = mesh,
						transform = worldToLocalMatrix * gameObject4.transform.localToWorldMatrix
					});
				}
			}
			string name = target.name;
			gameObject2.name = name;
			if (list3.Count != 0)
			{
				Mesh mesh2 = new Mesh();
				mesh2.CombineMeshes(list3.ToArray());
				CombineInstance[] array3 = new CombineInstance[1];
				array3[0].mesh = mesh2;
				array3[0].transform = matrix4x;
				new Mesh
				{
					name = name + "Collider"
				}.CombineMeshes(array3);
				Rigidbody component = gameObject2.GetComponent<Rigidbody>();
				if (component != null)
				{
					if (!component.isKinematic)
					{
					}
				}
			}
			CombineInstance[] combine;
			Material[] sharedMaterials;
			bool flag = MeshUtils.BuildCombineInstance(worldToLocalMatrix, list, list2, out combine, out sharedMaterials);
			Mesh mesh3 = new Mesh();
			mesh3.name = name;
			mesh3.CombineMeshes(combine, flag);
			Mesh mesh4 = MeshUtils.RemoveRotation(mesh3, matrix4x, flag);
			mesh4.name = name;
			gameObject2.transform.rotation = Quaternion.identity;
			gameObject2.transform.localScale = Vector3.one;
			for (int n = 0; n < gameObjects.Length; n++)
			{
				GameObject gameObject5 = gameObjects[n];
				gameObject5.transform.SetParent(array[n], true);
				gameObject5.SetActive(false);
			}
			if (target.transform.parent != null && target.transform.parent.gameObject.activeInHierarchy)
			{
				gameObject2.transform.SetParent(target.transform.parent);
			}
			SkinnedMeshRenderer component2 = gameObject2.GetComponent<SkinnedMeshRenderer>();
			if (component2 != null)
			{
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(component2);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(component2);
				}
			}
			MeshFilter meshFilter = gameObject2.GetComponent<MeshFilter>();
			if (meshFilter == null)
			{
				meshFilter = gameObject2.AddComponent<MeshFilter>();
			}
			meshFilter.sharedMesh = mesh4;
			MeshRenderer meshRenderer = gameObject2.GetComponent<MeshRenderer>();
			if (meshRenderer == null)
			{
				meshRenderer = gameObject2.AddComponent<MeshRenderer>();
			}
			meshRenderer.sharedMaterials = sharedMaterials;
			return new CombineResult(gameObject2, mesh4);
		}

		public static Mesh[] Separate(Mesh mesh)
		{
			if (mesh.subMeshCount < 2)
			{
				return new Mesh[]
				{
					mesh
				};
			}
			Mesh[] array = new Mesh[mesh.subMeshCount];
			for (int i = 0; i < mesh.subMeshCount; i++)
			{
				array[i] = MeshUtils.ExtractSubmesh(mesh, i);
			}
			return array;
		}

		public static Mesh ExtractSubmesh(Mesh mesh, int submeshIndex)
		{
			MeshTopology topology = mesh.GetTopology(submeshIndex);
			if (topology != MeshTopology.Triangles)
			{
				UnityEngine.Debug.LogWarningFormat("Extract Submesh method could handle triangle topology only. Current topology is {0}. Mesh name {1} submeshIndex {2}", new object[]
				{
					topology,
					mesh,
					submeshIndex
				});
				return mesh;
			}
			int[] triangles = mesh.GetTriangles(submeshIndex);
			int[] array = new int[triangles.Length];
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			int num = 0;
			for (int i = 0; i < triangles.Length; i++)
			{
				int key = triangles[i];
				if (!dictionary.ContainsKey(key))
				{
					array[i] = num;
					dictionary.Add(key, num);
					num++;
				}
				else
				{
					array[i] = dictionary[key];
				}
			}
			Vector3[] vertices = mesh.vertices;
			Vector3[] array2 = new Vector3[num];
			foreach (KeyValuePair<int, int> keyValuePair in dictionary)
			{
				array2[keyValuePair.Value] = vertices[keyValuePair.Key];
			}
			Mesh mesh2 = new Mesh();
			mesh2.vertices = array2;
			Color[] colors = mesh.colors;
			if (colors.Length == vertices.Length)
			{
				Color[] array3 = new Color[num];
				foreach (KeyValuePair<int, int> keyValuePair2 in dictionary)
				{
					array3[keyValuePair2.Value] = colors[keyValuePair2.Key];
				}
				mesh2.colors = array3;
			}
			else if (colors.Length != 0)
			{
				UnityEngine.Debug.LogWarning("colors.Length != vertices.Length");
			}
			Color32[] colors2 = mesh.colors32;
			if (colors2.Length == vertices.Length)
			{
				Color32[] array4 = new Color32[num];
				foreach (KeyValuePair<int, int> keyValuePair3 in dictionary)
				{
					array4[keyValuePair3.Value] = colors2[keyValuePair3.Key];
				}
				mesh2.colors32 = array4;
			}
			else if (colors2.Length != 0)
			{
				UnityEngine.Debug.LogWarning("colors32.Length != vertices.Length");
			}
			BoneWeight[] boneWeights = mesh.boneWeights;
			if (boneWeights.Length == vertices.Length)
			{
				BoneWeight[] array5 = new BoneWeight[num];
				foreach (KeyValuePair<int, int> keyValuePair4 in dictionary)
				{
					array5[keyValuePair4.Value] = boneWeights[keyValuePair4.Key];
				}
				mesh2.boneWeights = array5;
			}
			else if (boneWeights.Length != 0)
			{
				UnityEngine.Debug.LogWarning("boneWeights.Length != vertices.Length");
			}
			Vector3[] normals = mesh.normals;
			if (normals.Length == vertices.Length)
			{
				Vector3[] array6 = new Vector3[num];
				foreach (KeyValuePair<int, int> keyValuePair5 in dictionary)
				{
					array6[keyValuePair5.Value] = normals[keyValuePair5.Key];
				}
				mesh2.normals = array6;
			}
			else if (normals.Length != 0)
			{
				UnityEngine.Debug.LogWarning("normals.Length != vertices.Length");
			}
			Vector4[] tangents = mesh.tangents;
			if (tangents.Length == vertices.Length)
			{
				Vector4[] array7 = new Vector4[num];
				foreach (KeyValuePair<int, int> keyValuePair6 in dictionary)
				{
					array7[keyValuePair6.Value] = tangents[keyValuePair6.Key];
				}
				mesh2.tangents = array7;
			}
			else if (tangents.Length != 0)
			{
				UnityEngine.Debug.LogWarning("tangents.Length != vertices.Length");
			}
			Vector2[] uv = mesh.uv;
			if (uv.Length == vertices.Length)
			{
				Vector2[] array8 = new Vector2[num];
				foreach (KeyValuePair<int, int> keyValuePair7 in dictionary)
				{
					array8[keyValuePair7.Value] = uv[keyValuePair7.Key];
				}
				mesh2.uv = array8;
			}
			else if (uv.Length != 0)
			{
				UnityEngine.Debug.LogWarning("uv.Length != vertices.Length");
			}
			Vector2[] uv2 = mesh.uv2;
			if (uv2.Length == vertices.Length)
			{
				Vector2[] array9 = new Vector2[num];
				foreach (KeyValuePair<int, int> keyValuePair8 in dictionary)
				{
					array9[keyValuePair8.Value] = uv2[keyValuePair8.Key];
				}
				mesh2.uv2 = array9;
			}
			else if (uv2.Length != 0)
			{
				UnityEngine.Debug.LogWarning("uv2.Length != vertices.Length");
			}
			Vector2[] uv3 = mesh.uv3;
			if (uv3.Length == vertices.Length)
			{
				Vector2[] array10 = new Vector2[num];
				foreach (KeyValuePair<int, int> keyValuePair9 in dictionary)
				{
					array10[keyValuePair9.Value] = uv3[keyValuePair9.Key];
				}
				mesh2.uv3 = array10;
			}
			else if (uv3.Length != 0)
			{
				UnityEngine.Debug.LogWarning("uv3.Length != vertices.Length");
			}
			Vector2[] uv4 = mesh.uv4;
			if (uv4.Length == vertices.Length)
			{
				Vector2[] array11 = new Vector2[num];
				foreach (KeyValuePair<int, int> keyValuePair10 in dictionary)
				{
					array11[keyValuePair10.Value] = uv4[keyValuePair10.Key];
				}
				mesh2.uv4 = array11;
			}
			else if (uv4.Length != 0)
			{
				UnityEngine.Debug.LogWarning("uv4.Length != vertices.Length");
			}
			mesh2.triangles = array;
			return mesh2;
		}

		private static Mesh RemoveRotation(Mesh mesh, Matrix4x4 targetRotationMatrix, bool merge)
		{
			Mesh[] array = MeshUtils.Separate(mesh);
			CombineInstance[] array2 = new CombineInstance[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = new CombineInstance
				{
					mesh = array[i],
					subMeshIndex = 0,
					transform = targetRotationMatrix
				};
			}
			mesh = new Mesh();
			mesh.CombineMeshes(array2, merge);
			mesh.RecalculateBounds();
			return mesh;
		}

		private static bool BuildCombineInstance(Matrix4x4 targetToLocal, List<MeshFilter> allMeshFilters, List<SkinnedMeshRenderer> allSkinned, out CombineInstance[] combineInstances, out Material[] materials)
		{
			bool flag = true;
			Dictionary<Material, List<MeshTransform>> dictionary = new Dictionary<Material, List<MeshTransform>>();
			for (int i = 0; i < allMeshFilters.Count; i++)
			{
				MeshFilter meshFilter = allMeshFilters[i];
				MeshUtils.PopulateMeshGroups(dictionary, meshFilter.gameObject, meshFilter.sharedMesh);
			}
			for (int j = 0; j < allSkinned.Count; j++)
			{
				MeshUtils.PopulateMeshGroups(dictionary, allSkinned[j].gameObject, allSkinned[j].sharedMesh);
			}
			List<Material> list = new List<Material>(dictionary.Count);
			List<CombineInstance> list2 = new List<CombineInstance>(allMeshFilters.Count + allSkinned.Count);
			foreach (KeyValuePair<Material, List<MeshTransform>> keyValuePair in dictionary)
			{
				List<MeshTransform> value = keyValuePair.Value;
				List<List<CombineInstance>> list3 = new List<List<CombineInstance>>();
				List<CombineInstance> list4 = new List<CombineInstance>();
				int k = 0;
				int num = 0;
				while (k < value.Count)
				{
					MeshTransform meshTransform = value[k];
					if (meshTransform.Mesh.subMeshCount > 1)
					{
						flag = false;
					}
					num += meshTransform.Mesh.vertexCount;
					if (num > 65534 && list3.Count > 0)
					{
						list3.Add(list4);
						list4 = new List<CombineInstance>();
					}
					list4.Add(new CombineInstance
					{
						mesh = meshTransform.Mesh,
						transform = targetToLocal * meshTransform.Transform
					});
					k++;
				}
				list3.Add(list4);
				for (int l = 0; l < list3.Count; l++)
				{
					List<CombineInstance> list5 = list3[l];
					Mesh mesh = new Mesh();
					mesh.CombineMeshes(list5.ToArray(), true);
					list.Add(keyValuePair.Key);
					list2.Add(new CombineInstance
					{
						mesh = mesh,
						transform = Matrix4x4.identity
					});
				}
			}
			combineInstances = list2.ToArray();
			materials = list.ToArray();
			return flag && materials.Length <= 1;
		}

		private static void PopulateMeshGroups(Dictionary<Material, List<MeshTransform>> meshGroups, GameObject go, Mesh mesh)
		{
			Mesh[] array = MeshUtils.Separate(mesh);
			Renderer component = go.GetComponent<Renderer>();
			Material[] array2;
			if (component == null)
			{
				array2 = new Material[array.Length];
			}
			else
			{
				array2 = component.sharedMaterials;
				Array.Resize<Material>(ref array2, array.Length);
			}
			for (int i = 0; i < array2.Length; i++)
			{
				Material material = array2[i];
				if (material != null)
				{
					if (!meshGroups.ContainsKey(material))
					{
						meshGroups.Add(material, new List<MeshTransform>());
					}
					List<MeshTransform> list = meshGroups[material];
					list.Add(new MeshTransform(array[i], go.transform.localToWorldMatrix));
				}
			}
		}

		private static void DestroyChildren(GameObject gameObject)
		{
			if (Application.isPlaying)
			{
				IEnumerator enumerator = gameObject.transform.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Transform transform = (Transform)obj;
						UnityEngine.Object.Destroy(transform.gameObject);
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
			else
			{
				GameObject[] array = new GameObject[gameObject.transform.childCount];
				int num = 0;
				IEnumerator enumerator2 = gameObject.transform.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						object obj2 = enumerator2.Current;
						Transform transform2 = (Transform)obj2;
						array[num] = transform2.gameObject;
						num++;
					}
				}
				finally
				{
					IDisposable disposable2;
					if ((disposable2 = (enumerator2 as IDisposable)) != null)
					{
						disposable2.Dispose();
					}
				}
				for (int i = 0; i < array.Length; i++)
				{
					UnityEngine.Object.DestroyImmediate(array[i]);
				}
			}
		}

		private static List<Mesh> GetColliderMeshes(GameObject obj)
		{
			List<Mesh> list = new List<Mesh>();
			Collider[] components = obj.GetComponents<Collider>();
			if (components.Length == 0)
			{
				MeshFilter component = obj.GetComponent<MeshFilter>();
				if (component != null)
				{
				}
			}
			else
			{
				for (int i = 0; i < components.Length; i++)
				{
					MeshCollider meshCollider = components[i] as MeshCollider;
					if (meshCollider != null)
					{
						list.AddRange(MeshUtils.Separate(meshCollider.sharedMesh));
					}
					else
					{
						MeshFilter component2 = obj.GetComponent<MeshFilter>();
						if (component2 != null)
						{
							list.AddRange(MeshUtils.Separate(component2.sharedMesh));
						}
					}
				}
			}
			return list;
		}
	}
}
