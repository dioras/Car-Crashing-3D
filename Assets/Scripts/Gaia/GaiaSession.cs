using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gaia
{
	[Serializable]
	public class GaiaSession : ScriptableObject, ISerializationCallbackReceiver
	{
		public void OnBeforeSerialize()
		{
			this.m_resourcesKeys.Clear();
			this.m_resourcesValues.Clear();
			foreach (KeyValuePair<string, ScriptableObjectWrapper> keyValuePair in this.m_resources)
			{
				this.m_resourcesKeys.Add(keyValuePair.Key);
				this.m_resourcesValues.Add(keyValuePair.Value);
			}
		}

		public void OnAfterDeserialize()
		{
			this.m_resources = new Dictionary<string, ScriptableObjectWrapper>();
			for (int num = 0; num != Math.Min(this.m_resourcesKeys.Count, this.m_resourcesValues.Count); num++)
			{
				this.m_resources.Add(this.m_resourcesKeys[num], this.m_resourcesValues[num]);
			}
		}

		public string GetSessionFileName()
		{
			return Utils.FixFileName(this.m_name);
		}

		public Texture2D GetPreviewImage()
		{
			if (this.m_previewImageBytes.GetLength(0) == 0)
			{
				return null;
			}
			Texture2D texture2D = new Texture2D(this.m_previewImageWidth, this.m_previewImageHeight, TextureFormat.ARGB32, false);
			texture2D.LoadRawTextureData(this.m_previewImageBytes);
			texture2D.Apply();
			texture2D.name = this.m_name;
			return texture2D;
		}

		[TextArea(1, 1)]
		public string m_name = string.Format("Session {0:yyyyMMdd-HHmmss}", DateTime.Now);

		[TextArea(3, 5)]
		public string m_description = string.Empty;

		[HideInInspector]
		public Texture2D m_previewImage;

		public string m_dateCreated = DateTime.Now.ToString();

		public int m_terrainWidth;

		public int m_terrainDepth;

		public int m_terrainHeight;

		public float m_seaLevel = 30f;

		public bool m_isLocked;

		[HideInInspector]
		public ScriptableObjectWrapper m_defaults;

		[HideInInspector]
		public Dictionary<string, ScriptableObjectWrapper> m_resources = new Dictionary<string, ScriptableObjectWrapper>();

		[HideInInspector]
		public List<string> m_resourcesKeys = new List<string>();

		[HideInInspector]
		public List<ScriptableObjectWrapper> m_resourcesValues = new List<ScriptableObjectWrapper>();

		[HideInInspector]
		public byte[] m_previewImageBytes = new byte[0];

		[HideInInspector]
		public int m_previewImageWidth;

		[HideInInspector]
		public int m_previewImageHeight;

		[HideInInspector]
		public List<GaiaOperation> m_operations = new List<GaiaOperation>();
	}
}
