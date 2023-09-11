using System;
using UnityEngine;

namespace Gaia
{
	public class RuntimeStamper : MonoBehaviour
	{
		private void Awake()
		{
			this.m_currentPosition.height = 20f;
			this.m_currentPosition.width = 300f;
		}

		private void Start()
		{
			this.CreateStamper();
		}

		private void LateUpdate()
		{
			this.m_currentPosition.center = new Vector2((float)Screen.width / 2f, (float)Screen.height - 20f);
			if (this.m_stamper != null)
			{
				this.m_currentProgress = string.Format("Stamp progress: " + this.m_stamper.m_stampProgress.ToString(), new object[0]);
			}
		}

		private void OnGUI()
		{
			if (!this.m_showGUI)
			{
				return;
			}
			if (this.m_showGUI)
			{
				GUI.Label(this.m_currentPosition, this.m_currentProgress);
			}
		}

		private void CreateStamper()
		{
			string text = this.m_stampAddress;
			text = text.Replace("\\", "/");
			TextAsset textAsset = Resources.Load<TextAsset>(text);
			if (textAsset == null)
			{
				this.m_currentProgress = "Failed to load stamp at " + text;
				if (this.m_showDebug)
				{
					UnityEngine.Debug.Log(this.m_currentProgress);
				}
			}
			else
			{
				this.m_currentProgress = "Loaded stamp at " + textAsset.name;
				if (this.m_showDebug)
				{
					if (this.m_showDebug)
					{
						UnityEngine.Debug.Log(this.m_currentProgress);
					}
					GameObject gameObject = new GameObject("Runtime Stamper");
					this.m_stamper = gameObject.AddComponent<Stamper>();
					if (this.m_stamper.LoadRuntimeStamp(textAsset))
					{
						this.m_currentProgress = "Loaded Stamp";
						this.m_stamper.FlattenTerrain();
						this.m_stamper.FitToTerrain();
						this.m_stamper.m_height = 6f;
						this.m_stamper.m_distanceMask = AnimationCurve.Linear(0f, 1f, 1f, 0f);
						this.m_stamper.m_rotation = 0f;
						this.m_stamper.m_stickBaseToGround = true;
						this.m_stamper.m_updateTimeAllowed = 0.06666667f;
						this.m_stamper.UpdateStamp();
						this.m_stamper.Stamp();
					}
					else
					{
						this.m_currentProgress = "Failed to load stamp";
					}
					if (this.m_showDebug)
					{
						UnityEngine.Debug.Log(this.m_currentProgress);
					}
				}
			}
		}

		public bool m_showGUI = true;

		public bool m_showDebug = true;

		public string m_stampAddress = "Gaia/Stamps/RuggedHills 1810 4";

		public string m_currentProgress = string.Empty;

		private Rect m_currentPosition;

		private Stamper m_stamper;
	}
}
