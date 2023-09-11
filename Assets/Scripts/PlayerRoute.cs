using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerRoute : MonoBehaviour
{
	public void InitializeLineRenderer(Material lineRendererMaterial)
	{
		this.lineRenderer = new GameObject("Line renderer").AddComponent<LineRenderer>();
		this.lineRenderer.transform.parent = base.transform;
		this.lineRenderer.material = lineRendererMaterial;
		this.lineRenderer.widthMultiplier = 1f;
		this.lineRenderer.numCapVertices = 3;
		this.lineRenderer.numCornerVertices = 3;
		this.lineRenderer.transform.eulerAngles = new Vector3(90f, 0f, 0f);
		this.lineRenderer.alignment = LineAlignment.TransformZ;
		this.lineRenderer.positionCount = 0;
	}

	public void UpdateLineRenderer()
	{
		this.lineRenderer.positionCount = this.checkpoints.Count;
		for (int i = 0; i < this.checkpoints.Count; i++)
		{
			this.lineRenderer.SetPosition(i, this.checkpoints[i].transform.position);
		}
	}

	public void AddCheckpoint(Vector3 position)
	{
		GameObject gameObject = new GameObject("Checkpoint");
		gameObject.transform.parent = base.transform;
		gameObject.transform.position = position;
		this.checkpoints.Add(gameObject.transform);
		this.UpdateLineRenderer();
		this.AlignCheckpoints();
	}

	public void UpdateCheckpointPrefabs()
	{
		LevelEditorResources editorResources = LevelEditorTools.editorResources;
		this.ToggleCheckpoints(true);
		for (int i = 0; i < this.checkpoints.Count; i++)
		{
			for (int j = 0; j < this.checkpoints[i].transform.childCount; j++)
			{
				UnityEngine.Object.Destroy(this.checkpoints[i].transform.GetChild(j).gameObject);
			}
			GameObject original = editorResources.routeStartPrefab;
			if (i > 0)
			{
				original = editorResources.routeCheckpointPrefab;
			}
			if (i == this.checkpoints.Count - 1 && i > 0)
			{
				original = editorResources.routeFinishPrefab;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, this.checkpoints[i].transform.position, this.checkpoints[i].transform.rotation, this.checkpoints[i].transform);
			gameObject.GetComponent<PlayerRouteCheckpoint>().checkpointID = i;
			if (i == 0)
			{
				this.startCheckpoint = gameObject;
			}
		}
		if (this.checkpoints.Count > 0)
		{
			UnityEngine.Object.DestroyImmediate(this.routeIndicator);
			this.routeIndicator = UnityEngine.Object.Instantiate<GameObject>(editorResources.routeIndicatorPrefab, this.checkpoints[0].transform.position, this.checkpoints[0].transform.rotation, this.checkpoints[0].transform);
			this.routeIndicator.SetActive(false);
		}
		this.UpdateLineRenderer();
		this.AlignCheckpoints();
	}

	[ContextMenu("Bake")]
	public void BakeRoute()
	{
		this.lineRenderer.enabled = false;
		this.ToggleCheckpoints(false);
		foreach (Transform transform in this.checkpoints)
		{
			PlayerRouteCheckpoint componentInChildren = transform.GetComponentInChildren<PlayerRouteCheckpoint>(true);
			if (componentInChildren != null)
			{
				componentInChildren.ToggleTapTarget(false);
			}
		}
	}

	public void UnBakeRoute()
	{
		this.lineRenderer.enabled = true;
		this.ToggleCheckpoints(true);
		foreach (Transform transform in this.checkpoints)
		{
			PlayerRouteCheckpoint componentInChildren = transform.GetComponentInChildren<PlayerRouteCheckpoint>(true);
			if (componentInChildren != null)
			{
				componentInChildren.ToggleTapTarget(true);
			}
		}
	}

	public void ToggleCheckpoints(bool on)
	{
		if (this.routeIndicator != null)
		{
			this.routeIndicator.SetActive(!on);
		}
		if (this.startCheckpoint != null)
		{
			this.startCheckpoint.SetActive(on);
		}
		for (int i = 1; i < this.checkpoints.Count; i++)
		{
			this.checkpoints[i].gameObject.SetActive(on);
		}
	}

	public void AlignCheckpoints()
	{
		for (int i = 0; i < this.checkpoints.Count; i++)
		{
			Ray ray = new Ray(this.checkpoints[i].position + Vector3.up * 50f, Vector3.down);
			RaycastHit[] array = (from h in Physics.RaycastAll(ray)
			orderby h.distance
			select h).ToArray<RaycastHit>();
			for (int j = 0; j < array.Length; j++)
			{
				if (!(array[j].collider.GetComponentInParent<PlayerRoute>() != null))
				{
					if (!array[j].collider.name.Contains("MudIndicator"))
					{
						this.checkpoints[i].position = array[j].point;
						break;
					}
				}
			}
			if (i < this.checkpoints.Count - 1)
			{
				Vector3 position = this.checkpoints[i + 1].transform.position;
				position.y = this.checkpoints[i].transform.position.y;
				Vector3 normalized = (position - this.checkpoints[i].transform.position).normalized;
				if (i > 0)
				{
					Vector3 position2 = this.checkpoints[i - 1].transform.position;
					position2.y = this.checkpoints[i].transform.position.y;
					Vector3 normalized2 = (this.checkpoints[i].transform.position - position2).normalized;
					Vector3 forward = (normalized + normalized2) / 2f;
					this.checkpoints[i].transform.rotation = Quaternion.LookRotation(forward);
				}
				else
				{
					this.checkpoints[i].transform.rotation = Quaternion.LookRotation(normalized);
				}
			}
			foreach (Renderer renderer in this.checkpoints[i].GetComponentsInChildren<Renderer>())
			{
				renderer.gameObject.SetActive(false);
				ray = new Ray(renderer.transform.position + Vector3.up * 50f, Vector3.down);
				array = (from h in Physics.RaycastAll(ray)
				orderby h.distance
				select h).ToArray<RaycastHit>();
				for (int l = 0; l < array.Length; l++)
				{
					if (!(array[l].collider.GetComponentInParent<PlayerRoute>() != null))
					{
						renderer.transform.position = array[l].point;
						break;
					}
				}
				renderer.gameObject.SetActive(true);
			}
		}
	}

	public float RouteLength()
	{
		float num = 0f;
		if (this.checkpoints != null)
		{
			for (int i = 0; i < this.checkpoints.Count - 1; i++)
			{
				if (!(this.checkpoints[i] == null) && !(this.checkpoints[i + 1] == null))
				{
					num += Vector3.Distance(this.checkpoints[i].position, this.checkpoints[i + 1].position);
				}
			}
		}
		return num;
	}

	public string Serialize()
	{
		string text = this.routeName + "|" + this.routeID + "|";
		for (int i = 0; i < this.checkpoints.Count; i++)
		{
			string str = string.Concat(new object[]
			{
				Mathf.Round(this.checkpoints[i].position.x * 10f) / 10f,
				";",
				Mathf.Round(this.checkpoints[i].position.y * 10f) / 10f,
				";",
				Mathf.Round(this.checkpoints[i].position.z * 10f) / 10f
			});
			text = text + str + "|";
		}
		return text;
	}

	public void Deserialize(string str)
	{
		this.checkpoints = new List<Transform>();
		string[] array = str.Split(new char[]
		{
			'|'
		});
		this.routeName = array[0];
		this.routeID = array[1];
		for (int i = 2; i < array.Length - 1; i++)
		{
			string text = array[i];
			string[] array2 = text.Split(new char[]
			{
				';'
			});
			float x = float.Parse(array2[0]);
			float y = float.Parse(array2[1]);
			float z = float.Parse(array2[2]);
			Vector3 position = new Vector3(x, y, z);
			this.AddCheckpoint(position);
		}
	}

	public static float completionMoney = 750f;

	public static float bronzeMoney = 1000f;

	public static float silverMoney = 1500f;

	public static float goldMoney = 2000f;

	public static float completionXP = 10f;

	public static float bronzeXP = 20f;

	public static float silverXP = 30f;

	public static float goldXP = 40f;

	public static float completionGolds = 5f;

	public static float bronzeGolds = 10f;

	public static float silverGolds = 15f;

	public static float goldGolds = 20f;

	public string routeName;

	public List<Transform> checkpoints = new List<Transform>();

	public LineRenderer lineRenderer;

	public string routeID;

	private GameObject routeIndicator;

	private GameObject startCheckpoint;

	public float routeRecord;

	public string routeRecordKeeper;
}
