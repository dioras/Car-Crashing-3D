using System;
using System.IO;
using UnityEngine;

namespace Gaia
{
	public class Scanner : MonoBehaviour
	{
		private void OnEnable()
		{
			MeshFilter meshFilter = base.GetComponent<MeshFilter>();
			if (meshFilter == null)
			{
				meshFilter = base.gameObject.AddComponent<MeshFilter>();
			}
			meshFilter.hideFlags = HideFlags.HideInInspector;
			MeshRenderer meshRenderer = base.GetComponent<MeshRenderer>();
			if (meshRenderer == null)
			{
				meshRenderer = base.gameObject.AddComponent<MeshRenderer>();
			}
			meshRenderer.hideFlags = HideFlags.HideInInspector;
		}

		private void Awake()
		{
			base.gameObject.SetActive(false);
		}

		public void Reset()
		{
			this.m_featureName = string.Empty;
			this.m_scanMap = null;
			this.m_scanBounds = new Bounds(base.transform.position, Vector3.one * 10f);
			this.m_scanWidth = (this.m_scanDepth = (this.m_scanHeight = 0));
			this.m_scanResolution = 0.1f;
			this.m_baseLevel = 0f;
			base.transform.localRotation = Quaternion.identity;
			base.transform.localScale = Vector3.one;
		}

		public void LoadRawFile(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				UnityEngine.Debug.LogError("Must supply a valid path. Raw load Aborted!");
			}
			this.Reset();
			this.m_featureName = Path.GetFileNameWithoutExtension(path);
			this.m_scanMap = new HeightMap();
			this.m_scanMap.LoadFromRawFile(path);
			if (!this.m_scanMap.HasData())
			{
				UnityEngine.Debug.LogError("Unable to load raw file. Raw load aborted.");
				return;
			}
			this.m_scanWidth = this.m_scanMap.Width();
			this.m_scanDepth = this.m_scanMap.Depth();
			this.m_scanHeight = this.m_scanWidth / 2;
			this.m_scanResolution = 0.1f;
			this.m_scanBounds = new Bounds(base.transform.position, new Vector3((float)this.m_scanWidth * this.m_scanResolution, (float)this.m_scanWidth * this.m_scanResolution * 0.4f, (float)this.m_scanDepth * this.m_scanResolution));
			this.m_baseLevel = this.m_scanMap.GetBaseLevel();
			MeshFilter meshFilter = base.GetComponent<MeshFilter>();
			if (meshFilter == null)
			{
				meshFilter = base.gameObject.AddComponent<MeshFilter>();
				meshFilter.hideFlags = HideFlags.HideInInspector;
			}
			MeshRenderer meshRenderer = base.GetComponent<MeshRenderer>();
			if (meshRenderer == null)
			{
				meshRenderer = base.gameObject.AddComponent<MeshRenderer>();
				meshRenderer.hideFlags = HideFlags.HideInInspector;
			}
			meshFilter.mesh = Utils.CreateMesh(this.m_scanMap.Heights(), this.m_scanBounds.size);
			if (this.m_previewMaterial != null)
			{
				this.m_previewMaterial.hideFlags = HideFlags.HideInInspector;
				meshRenderer.sharedMaterial = this.m_previewMaterial;
			}
		}

		public void LoadTextureFile(Texture2D texture)
		{
			if (texture == null)
			{
				UnityEngine.Debug.LogError("Must supply a valid texture! Texture load aborted.");
				return;
			}
			this.Reset();
			this.m_featureName = texture.name;
			this.m_scanMap = new UnityHeightMap(texture);
			if (!this.m_scanMap.HasData())
			{
				UnityEngine.Debug.LogError("Unable to load Texture file. Texture load aborted.");
				return;
			}
			this.m_scanWidth = this.m_scanMap.Width();
			this.m_scanDepth = this.m_scanMap.Depth();
			this.m_scanHeight = this.m_scanWidth / 2;
			this.m_scanResolution = 0.1f;
			this.m_scanBounds = new Bounds(base.transform.position, new Vector3((float)this.m_scanWidth * this.m_scanResolution, (float)this.m_scanWidth * this.m_scanResolution * 0.4f, (float)this.m_scanDepth * this.m_scanResolution));
			this.m_baseLevel = this.m_scanMap.GetBaseLevel();
			MeshFilter meshFilter = base.GetComponent<MeshFilter>();
			if (meshFilter == null)
			{
				meshFilter = base.gameObject.AddComponent<MeshFilter>();
				meshFilter.hideFlags = HideFlags.HideInInspector;
			}
			MeshRenderer meshRenderer = base.GetComponent<MeshRenderer>();
			if (meshRenderer == null)
			{
				meshRenderer = base.gameObject.AddComponent<MeshRenderer>();
				meshRenderer.hideFlags = HideFlags.HideInInspector;
			}
			meshFilter.mesh = Utils.CreateMesh(this.m_scanMap.Heights(), this.m_scanBounds.size);
			if (this.m_previewMaterial != null)
			{
				this.m_previewMaterial.hideFlags = HideFlags.HideInInspector;
				meshRenderer.sharedMaterial = this.m_previewMaterial;
			}
		}

		public void LoadTerain(Terrain terrain)
		{
			if (terrain == null)
			{
				UnityEngine.Debug.LogError("Must supply a valid terrain! Terrain load aborted.");
				return;
			}
			this.Reset();
			this.m_featureName = terrain.name;
			this.m_scanMap = new UnityHeightMap(terrain);
			if (!this.m_scanMap.HasData())
			{
				UnityEngine.Debug.LogError("Unable to load terrain file. Terrain load aborted.");
				return;
			}
			this.m_scanMap.Flip();
			this.m_scanWidth = this.m_scanMap.Width();
			this.m_scanDepth = this.m_scanMap.Depth();
			this.m_scanHeight = (int)terrain.terrainData.size.y;
			this.m_scanResolution = 0.1f;
			this.m_scanBounds = new Bounds(base.transform.position, new Vector3((float)this.m_scanWidth * this.m_scanResolution, (float)this.m_scanWidth * this.m_scanResolution * 0.4f, (float)this.m_scanDepth * this.m_scanResolution));
			this.m_baseLevel = this.m_scanMap.GetBaseLevel();
			MeshFilter meshFilter = base.GetComponent<MeshFilter>();
			if (meshFilter == null)
			{
				meshFilter = base.gameObject.AddComponent<MeshFilter>();
				meshFilter.hideFlags = HideFlags.HideInInspector;
			}
			MeshRenderer meshRenderer = base.GetComponent<MeshRenderer>();
			if (meshRenderer == null)
			{
				meshRenderer = base.gameObject.AddComponent<MeshRenderer>();
				meshRenderer.hideFlags = HideFlags.HideInInspector;
			}
			meshFilter.mesh = Utils.CreateMesh(this.m_scanMap.Heights(), this.m_scanBounds.size);
			if (this.m_previewMaterial != null)
			{
				this.m_previewMaterial.hideFlags = HideFlags.HideInInspector;
				meshRenderer.sharedMaterial = this.m_previewMaterial;
			}
		}

		public void LoadGameObject(GameObject go)
		{
			if (go == null)
			{
				UnityEngine.Debug.LogError("Must supply a valid gamem object! GameObject load aborted.");
				return;
			}
			this.Reset();
			this.m_featureName = go.name;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(go);
			gameObject.transform.position = base.transform.position;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			Collider[] componentsInChildren = gameObject.GetComponentsInChildren<Collider>();
			foreach (Collider obj in componentsInChildren)
			{
				UnityEngine.Object.DestroyImmediate(obj);
			}
			Transform[] componentsInChildren2 = gameObject.GetComponentsInChildren<Transform>();
			foreach (Transform transform in componentsInChildren2)
			{
				if (transform.gameObject.activeSelf)
				{
					transform.gameObject.AddComponent<MeshCollider>();
				}
			}
			this.m_scanBounds.center = gameObject.transform.position;
			this.m_scanBounds.size = Vector3.zero;
			foreach (MeshCollider meshCollider in gameObject.GetComponentsInChildren<MeshCollider>())
			{
				this.m_scanBounds.Encapsulate(meshCollider.bounds);
			}
			this.m_scanWidth = (int)Mathf.Ceil(this.m_scanBounds.size.x * (1f / this.m_scanResolution));
			this.m_scanHeight = (int)Mathf.Ceil(this.m_scanBounds.size.y * (1f / this.m_scanResolution));
			this.m_scanDepth = (int)Mathf.Ceil(this.m_scanBounds.size.z * (1f / this.m_scanResolution));
			this.m_scanMap = new HeightMap(this.m_scanWidth, this.m_scanDepth);
			Vector3 min = this.m_scanBounds.min;
			Vector3 origin = min;
			origin.y = this.m_scanBounds.max.y;
			for (int l = 0; l < this.m_scanWidth; l++)
			{
				origin.x = min.x + this.m_scanResolution * (float)l;
				for (int m = 0; m < this.m_scanDepth; m++)
				{
					origin.z = min.z + this.m_scanResolution * (float)m;
					RaycastHit raycastHit;
					if (Physics.Raycast(origin, Vector3.down, out raycastHit, this.m_scanBounds.size.y))
					{
						this.m_scanMap[l, m] = 1f - raycastHit.distance / this.m_scanBounds.size.y;
					}
				}
			}
			UnityEngine.Object.DestroyImmediate(gameObject);
			if (!this.m_scanMap.HasData())
			{
				UnityEngine.Debug.LogError("Unable to scan GameObject. GameObject load aborted.");
				return;
			}
			this.m_scanBounds = new Bounds(base.transform.position, new Vector3((float)this.m_scanWidth * this.m_scanResolution, (float)this.m_scanWidth * this.m_scanResolution * 0.4f, (float)this.m_scanDepth * this.m_scanResolution));
			this.m_baseLevel = this.m_scanMap.GetBaseLevel();
			MeshFilter meshFilter = base.GetComponent<MeshFilter>();
			if (meshFilter == null)
			{
				meshFilter = base.gameObject.AddComponent<MeshFilter>();
				meshFilter.hideFlags = HideFlags.HideInInspector;
			}
			MeshRenderer meshRenderer = base.GetComponent<MeshRenderer>();
			if (meshRenderer == null)
			{
				meshRenderer = base.gameObject.AddComponent<MeshRenderer>();
				meshRenderer.hideFlags = HideFlags.HideInInspector;
			}
			meshFilter.mesh = Utils.CreateMesh(this.m_scanMap.Heights(), this.m_scanBounds.size);
			if (this.m_previewMaterial != null)
			{
				this.m_previewMaterial.hideFlags = HideFlags.HideInInspector;
				meshRenderer.sharedMaterial = this.m_previewMaterial;
			}
		}

		public string SaveScan()
		{
			if (this.m_scanMap == null || !this.m_scanMap.HasData())
			{
				UnityEngine.Debug.LogWarning("Cant save scan as none has been loaded");
				return null;
			}
			string text = Utils.GetGaiaAssetPath(this.m_featureType, this.m_featureName);
			Utils.CompressToSingleChannelFileImage(this.m_scanMap.Heights(), text, TextureFormat.RGBA32, false, true);
			text = Utils.GetGaiaStampAssetPath(this.m_featureType, this.m_featureName);
			text += ".bytes";
			float[] array = new float[]
			{
				(float)this.m_scanWidth,
				(float)this.m_scanDepth,
				(float)this.m_scanHeight,
				this.m_scanResolution,
				this.m_baseLevel
			};
			byte[] array2 = new byte[array.Length * 4];
			Buffer.BlockCopy(array, 0, array2, 0, array2.Length);
			this.m_scanMap.SetMetaData(array2);
			this.m_scanMap.SaveToBinaryFile(text);
			return text;
		}

		private void UpdateScanner()
		{
			base.transform.localRotation = Quaternion.identity;
			base.transform.localScale = Vector3.one;
			this.m_scanBounds.center = base.transform.position;
		}

		private void OnDrawGizmosSelected()
		{
			this.UpdateScanner();
			Gizmos.color = Color.blue;
			Gizmos.DrawWireCube(this.m_scanBounds.center, this.m_scanBounds.size);
			this.m_groundOffset = this.m_scanBounds.center;
			this.m_groundOffset.y = this.m_scanBounds.min.y + (this.m_scanBounds.max.y - this.m_scanBounds.min.y) * this.m_baseLevel;
			this.m_groundSize = this.m_scanBounds.size;
			this.m_groundSize.y = 0.1f;
			Gizmos.color = Color.yellow;
			Gizmos.DrawCube(this.m_groundOffset, this.m_groundSize);
		}

		[Tooltip("The name of the stamp as it will be stored in the project. Initally based on the file name.")]
		public string m_featureName = string.Format("{0}", DateTime.Now);

		[Tooltip("The type of stamp, also controls which directory the stamp will be loaded into.")]
		public GaiaConstants.FeatureType m_featureType = GaiaConstants.FeatureType.Mountains;

		[Range(0f, 1f)]
		[Tooltip("Base level for this stamp, used by stamper to cut off all heights below the base. It is the highest point of the stamp around its edges.")]
		public float m_baseLevel;

		[HideInInspector]
		[Range(0.1f, 1f)]
		[Tooltip("Scan resolution in meters. Leave this at smaller values for higher quality scans.")]
		public float m_scanResolution = 0.1f;

		[Tooltip("The material that will be used to display the scan preview. This is just for visualisation and will not affect the scan.")]
		public Material m_previewMaterial;

		private HeightMap m_scanMap;

		private Bounds m_scanBounds;

		private int m_scanWidth = 1;

		private int m_scanDepth = 1;

		private int m_scanHeight = 500;

		private Vector3 m_groundOffset = Vector3.zero;

		private Vector3 m_groundSize = Vector3.zero;
	}
}
