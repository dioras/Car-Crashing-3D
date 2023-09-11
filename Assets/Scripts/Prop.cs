using System;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
	private int snappedPointsCount
	{
		get
		{
			int num = 0;
			foreach (SnapPoint snapPoint in this.snapPoints)
			{
				if (snapPoint.busy)
				{
					num++;
				}
			}
			return num;
		}
	}

	public virtual void Start()
	{
		if (!this.initialized)
		{
			this.Initialize();
		}
	}

	[ContextMenu("Disable")]
	private void disableHL()
	{
		this.highlightedObject.SetActive(false);
	}

	[ContextMenu("enable")]
	private void ebableHL()
	{
		this.highlightedObject.SetActive(true);
	}

	public void Initialize()
	{
		this.initialized = true;
		this.debrisSeed = 0;
		this.defaultScale = base.transform.localScale.x;
		this.offsets = new float[this.snapPoints.Length];
		for (int i = 0; i < this.snapPoints.Length; i++)
		{
			this.offsets[i] = Vector3.SignedAngle(base.transform.forward, this.snapPoints[i].transform.forward, base.transform.up);
		}
		foreach (SnapPoint snapPoint in this.snapPoints)
		{
			if (snapPoint.leftAffector != null)
			{
				snapPoint.leftAffectorDefPos = snapPoint.leftAffector.localPosition;
			}
			if (snapPoint.rightAffector != null)
			{
				snapPoint.rightAffectorDefPos = snapPoint.rightAffector.localPosition;
			}
		}
		this.PlaceDebris(-1, -1);
	}

	private void PlaceDebris(int seed = -1, int _debrisCount = -1)
	{
		if (this.debrisPrefabs.Length == 0)
		{
			return;
		}
		if (this.debrisParent == null)
		{
			return;
		}
		if (this.debrisRects.Length == 0)
		{
			return;
		}
		for (int i = 0; i < this.debrisParent.childCount; i++)
		{
			UnityEngine.Object.Destroy(this.debrisParent.GetChild(i).gameObject);
		}
		if (seed == -1)
		{
			this.debrisSeed = UnityEngine.Random.Range(0, 1000);
		}
		else
		{
			this.debrisSeed = seed;
		}
		if (_debrisCount == -1)
		{
			this.debrisCount = (int)((float)this.maxDebrisCount * UnityEngine.Random.Range(0f, 1f));
			if (this.debrisCount == 0)
			{
				this.debrisCount = 3;
			}
		}
		else
		{
			this.debrisCount = _debrisCount;
		}
		UnityEngine.Random.InitState(this.debrisSeed);
		for (int j = 0; j < this.debrisCount; j++)
		{
			int num = UnityEngine.Random.Range(0, this.debrisPrefabs.Length);
			int num2 = UnityEngine.Random.Range(0, this.debrisRects.Length);
			Vector3 randomPos = this.debrisRects[num2].GetRandomPos();
			Quaternion rotation = Quaternion.Euler(UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f));
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.debrisPrefabs[num], randomPos, rotation, this.debrisParent);
			gameObject.transform.localScale = Vector3.one * Mathf.Lerp(this.minDebrisScale, this.maxDebrisScale, UnityEngine.Random.Range(0f, 1f));
		}
	}

	public virtual void Update()
	{
	}

	public SnapPoint ClosestFreeSnapPoint(Vector3 toPosition)
	{
		if (this.snapPoints == null)
		{
			return null;
		}
		if (this.snapPoints.Length == 0)
		{
			return null;
		}
		SnapPoint snapPoint = null;
		for (int i = 0; i < this.snapPoints.Length; i++)
		{
			if (snapPoint == null)
			{
				if (!this.snapPoints[i].busy)
				{
					snapPoint = this.snapPoints[i];
				}
			}
			else if (Vector3.Distance(toPosition, this.snapPoints[i].transform.position) < Vector3.Distance(toPosition, snapPoint.transform.position))
			{
				snapPoint = this.snapPoints[i];
			}
		}
		return snapPoint;
	}

	public int GetSuitableSnapPointID(SnapPoint otherSnapPoint)
	{
		if (this.snapPoints == null)
		{
			return -1;
		}
		if (this.snapPoints.Length == 0)
		{
			return -1;
		}
		for (int i = 0; i < this.snapPoints.Length; i++)
		{
			if (Vector3.Angle(this.snapPoints[i].transform.forward, otherSnapPoint.transform.forward) > 90f)
			{
				return i;
			}
		}
		return -1;
	}

	public void ToggleExtra0(bool on)
	{
		this.extra0Enabled = on;
		if (this.extra0 != null)
		{
			this.extra0.SetActive(on);
		}
	}

	public void ToggleExtra1(bool on)
	{
		this.extra1Enabled = on;
		if (this.extra1 != null)
		{
			this.extra1.SetActive(on);
		}
	}

	public void ResetSnapping()
	{
		if (this.snapPoints.Length == 0)
		{
			return;
		}
		if (this.offsets == null)
		{
			return;
		}
		foreach (SnapPoint snapPoint in this.snapPoints)
		{
			snapPoint.ResetAffectors();
			snapPoint.busy = false;
		}
		foreach (SnapPoint snapPoint2 in this.attachedSnapPoints)
		{
			snapPoint2.ResetAffectors();
			snapPoint2.busy = false;
			snapPoint2.transform.GetComponentInParent<Prop>().OnPropDetached(this);
		}
		this.attachedSnapPoints.Clear();
	}

	public bool DoSnapping()
	{
		if (this.snapPoints.Length == 0)
		{
			return false;
		}
		if (this.offsets == null)
		{
			return false;
		}
		List<SnapPoint> list = new List<SnapPoint>();
		bool result = false;
		for (int i = 0; i < this.snapPoints.Length; i++)
		{
			SnapPoint snapPoint = this.snapPoints[i];
			snapPoint.ResetAffectors();
			snapPoint.busy = false;
			Collider[] array = Physics.OverlapSphere(snapPoint.transform.position, this.snapRadius);
			if (array.Length > 0)
			{
				foreach (Collider collider in array)
				{
					Prop componentInParent = collider.GetComponentInParent<Prop>();
					if (!(componentInParent == this) && !(componentInParent == null))
					{
						if (componentInParent.propType == this.propType)
						{
							SnapPoint[] array3 = componentInParent.snapPoints;
							if (array3.Length > 0)
							{
								SnapPoint snapPoint2 = array3[0];
								foreach (SnapPoint snapPoint3 in array3)
								{
									if (Vector3.Distance(snapPoint3.transform.position, snapPoint.transform.position) < Vector3.Distance(snapPoint2.transform.position, snapPoint.transform.position))
									{
										snapPoint2 = snapPoint3;
									}
								}
								if (Vector3.Angle(snapPoint2.transform.right, -snapPoint.transform.right) <= 90f)
								{
									if (snapPoint2.busy)
									{
										bool flag = false;
										foreach (SnapPoint snapPoint4 in this.attachedSnapPoints)
										{
											if (snapPoint4.transform.Equals(snapPoint2.transform))
											{
												flag = true;
											}
										}
										if (!flag)
										{
											goto IL_804;
										}
									}
									snapPoint2.ResetAffectors();
									list.Add(snapPoint2);
									if (!this.attachedSnapPoints.Contains(snapPoint2))
									{
										this.attachedSnapPoints.Add(snapPoint2);
									}
									snapPoint2.busy = true;
									if (!componentInParent.attachedSnapPoints.Contains(snapPoint))
									{
										componentInParent.attachedSnapPoints.Add(snapPoint);
									}
									snapPoint.busy = true;
									if (this.snapAngle)
									{
										Vector3 position = snapPoint.transform.position;
										Vector3 vector = -snapPoint2.transform.forward;
										vector = Vector3.ProjectOnPlane(vector, base.transform.up);
										Vector3 b = Quaternion.AngleAxis(-this.offsets[i], base.transform.up) * vector;
										base.transform.rotation *= Quaternion.FromToRotation(base.transform.forward, Vector3.Lerp(base.transform.forward, b, 0.3f));
										Vector3 position2 = snapPoint.transform.position;
										base.transform.position -= position2 - position;
									}
									bool flag2 = this.propType == PropType.Road && array3.Length == 2 && this.snappedPointsCount == 1;
									if (this.snapPosition || flag2)
									{
										Vector3 vector2 = snapPoint2.transform.InverseTransformPoint(snapPoint.transform.position);
										vector2 = snapPoint.transform.position - snapPoint2.transform.position;
										vector2 /= 3f;
										if (flag2)
										{
											vector2.y = 0f;
										}
										base.transform.position -= vector2;
										result = true;
									}
									if (snapPoint.leftAffector != null && snapPoint.rightAffector != null && snapPoint2.leftAffector != null && snapPoint2.rightAffector != null)
									{
										Vector3 vector3 = (snapPoint.leftAffector.position + snapPoint2.rightAffector.position) / 2f;
										Vector3 vector4 = (snapPoint.rightAffector.position + snapPoint2.leftAffector.position) / 2f;
										Transform leftAffector = snapPoint.leftAffector;
										Vector3 position3 = vector3;
										snapPoint2.rightAffector.position = position3;
										leftAffector.position = position3;
										Transform rightAffector = snapPoint.rightAffector;
										position3 = vector4;
										snapPoint2.leftAffector.position = position3;
										rightAffector.position = position3;
										this.Xangle = Vector3.SignedAngle(snapPoint.transform.up, snapPoint2.transform.up, snapPoint.transform.right);
										Vector3 axis = (snapPoint.transform.up + snapPoint2.transform.up) / 2f;
										UnityEngine.Debug.DrawRay(snapPoint.transform.position, axis.normalized * 5f, Color.red);
										this.Yangle = Vector3.SignedAngle(snapPoint.transform.right, -snapPoint2.transform.right, axis);
										Vector3 vector5 = (snapPoint.transform.right - snapPoint2.transform.right) / 2f;
										UnityEngine.Debug.DrawRay(snapPoint.transform.position, vector5.normalized * 5f, Color.cyan);
										float d = Mathf.Abs(this.Xangle) / 90f;
										float d2 = Mathf.Abs(this.Yangle) / 90f;
										snapPoint.leftAffector.position += axis.normalized * this.maxHeightIncrement * -Mathf.Sign(this.Xangle) * d;
										snapPoint.rightAffector.position += axis.normalized * this.maxHeightIncrement * -Mathf.Sign(this.Xangle) * d;
										snapPoint2.leftAffector.position += axis.normalized * this.maxHeightIncrement * -Mathf.Sign(this.Xangle) * d;
										snapPoint2.rightAffector.position += axis.normalized * this.maxHeightIncrement * -Mathf.Sign(this.Xangle) * d;
										snapPoint.leftAffector.position += vector5.normalized * this.maxSideIncrement * Mathf.Sign(this.Yangle) * d2 - vector5.normalized * d2 * this.maxCorrectingSizeIncrement;
										snapPoint.rightAffector.position += vector5.normalized * this.maxSideIncrement * Mathf.Sign(this.Yangle) * d2 + vector5.normalized * d2 * this.maxCorrectingSizeIncrement;
										snapPoint2.leftAffector.position += vector5.normalized * this.maxSideIncrement * Mathf.Sign(this.Yangle) * d2 + vector5.normalized * d2 * this.maxCorrectingSizeIncrement;
										snapPoint2.rightAffector.position += vector5.normalized * this.maxSideIncrement * Mathf.Sign(this.Yangle) * d2 - vector5.normalized * d2 * this.maxCorrectingSizeIncrement;
									}
								}
							}
						}
					}
					IL_804:;
				}
			}
		}
		for (int l = 0; l < this.attachedSnapPoints.Count; l++)
		{
			bool flag3 = false;
			for (int m = 0; m < list.Count; m++)
			{
				if (list[m].transform.Equals(this.attachedSnapPoints[l].transform))
				{
					flag3 = true;
				}
			}
			if (!flag3)
			{
				this.attachedSnapPoints[l].ResetAffectors();
				this.attachedSnapPoints[l].busy = false;
				this.attachedSnapPoints[l].transform.GetComponentInParent<Prop>().OnPropDetached(this);
				this.attachedSnapPoints.RemoveAt(l);
			}
		}
		return result;
	}

	public void SnapToSnapPoint(Prop otherProp, SnapPoint otherSnapPoint)
	{
	}

	public void OnPropDetached(Prop otherProp)
	{
		for (int i = 0; i < this.attachedSnapPoints.Count; i++)
		{
			foreach (SnapPoint snapPoint in otherProp.snapPoints)
			{
				if (i < this.attachedSnapPoints.Count && this.attachedSnapPoints[i].transform.Equals(snapPoint.transform))
				{
					this.attachedSnapPoints.RemoveAt(i);
				}
			}
		}
	}

	[ContextMenu("Pre bake")]
	public void PreBakeProp()
	{
		SkinnedMeshRenderer[] componentsInChildren = base.GetComponentsInChildren<SkinnedMeshRenderer>(false);
		this.preBakedMeshes = new MeshRenderer[componentsInChildren.Length];
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (!(componentsInChildren[i].gameObject == this.highlightedObject))
			{
				Mesh mesh = new Mesh();
				componentsInChildren[i].BakeMesh(mesh);
				this.preBakedMeshes[i] = new GameObject("PreBakedMesh:" + componentsInChildren[i].name).AddComponent<MeshRenderer>();
				this.preBakedMeshes[i].gameObject.AddComponent<MeshFilter>().mesh = mesh;
				this.preBakedMeshes[i].gameObject.AddComponent<MeshCollider>().material = this.physicMaterial;
				this.preBakedMeshes[i].transform.position = componentsInChildren[i].transform.position;
				this.preBakedMeshes[i].transform.rotation = componentsInChildren[i].transform.rotation;
				this.preBakedMeshes[i].materials = componentsInChildren[i].materials;
				this.preBakedMeshes[i].transform.parent = base.transform;
				this.preBakedMeshes[i].transform.localScale = new Vector3(1f / base.transform.localScale.x, 1f / base.transform.localScale.y, 1f / base.transform.localScale.z);
				componentsInChildren[i].enabled = false;
			}
		}
		if (this.placementCollider != null)
		{
			this.placementCollider.enabled = false;
		}
		if (this.debrisParent != null)
		{
			Transform[] componentsInChildren2 = this.debrisParent.gameObject.GetComponentsInChildren<Transform>();
			foreach (Transform transform in componentsInChildren2)
			{
				if (!(transform == this.debrisParent))
				{
					transform.position += Vector3.up;
					Ray ray = new Ray(transform.position, Vector3.down);
					RaycastHit raycastHit;
					if (Physics.Raycast(ray, out raycastHit, 1000f))
					{
						transform.position = raycastHit.point;
					}
				}
			}
		}
	}

	[ContextMenu("Cancel pre bake")]
	public void CancelPreBake()
	{
		if (this.preBakedMeshes != null)
		{
			foreach (MeshRenderer meshRenderer in this.preBakedMeshes)
			{
				UnityEngine.Object.Destroy(meshRenderer.gameObject);
			}
		}
		SkinnedMeshRenderer[] componentsInChildren = base.GetComponentsInChildren<SkinnedMeshRenderer>(false);
		foreach (SkinnedMeshRenderer skinnedMeshRenderer in componentsInChildren)
		{
			skinnedMeshRenderer.enabled = true;
		}
		if (this.placementCollider != null)
		{
			this.placementCollider.enabled = true;
		}
	}

	[ContextMenu("Bake prop")]
	public void BakeProp()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		foreach (SkinnedMeshRenderer skinnedMeshRenderer in base.GetComponentsInChildren<SkinnedMeshRenderer>(true))
		{
			foreach (Transform transform in skinnedMeshRenderer.bones)
			{
				UnityEngine.Object.Destroy(transform.gameObject);
			}
			if (!skinnedMeshRenderer.gameObject.activeSelf)
			{
				UnityEngine.Object.Destroy(skinnedMeshRenderer.gameObject);
			}
			else
			{
				Mesh mesh = new Mesh();
				skinnedMeshRenderer.BakeMesh(mesh);
				MeshRenderer meshRenderer = skinnedMeshRenderer.gameObject.AddComponent<MeshRenderer>();
				MeshFilter meshFilter = meshRenderer.gameObject.AddComponent<MeshFilter>();
				meshFilter.mesh = mesh;
				meshRenderer.sharedMaterials = skinnedMeshRenderer.sharedMaterials;
				UnityEngine.Object.Destroy(skinnedMeshRenderer);
				MeshCollider meshCollider = meshRenderer.gameObject.AddComponent<MeshCollider>();
				meshRenderer.transform.localScale = new Vector3(1f / base.transform.localScale.x, 1f / base.transform.localScale.y, 1f / base.transform.localScale.z);
			}
		}
		foreach (SnapPoint snapPoint in this.snapPoints)
		{
			if (snapPoint.leftAffector != null)
			{
				UnityEngine.Object.Destroy(snapPoint.leftAffector.gameObject);
			}
			if (snapPoint.rightAffector != null)
			{
				UnityEngine.Object.Destroy(snapPoint.rightAffector.gameObject);
			}
			if (snapPoint.transform != null)
			{
				UnityEngine.Object.Destroy(snapPoint.transform.gameObject);
			}
		}
		if (this.frontSupport != null)
		{
			UnityEngine.Object.Destroy(this.frontSupport.gameObject);
		}
		if (this.rearSupport != null)
		{
			UnityEngine.Object.Destroy(this.rearSupport.gameObject);
		}
		if (this.highlightedObject != null)
		{
			UnityEngine.Object.Destroy(this.highlightedObject);
		}
		if (this.debrisParent != null)
		{
			Transform[] componentsInChildren2 = this.debrisParent.gameObject.GetComponentsInChildren<Transform>();
			foreach (Transform transform2 in componentsInChildren2)
			{
				if (!(transform2 == this.debrisParent))
				{
					transform2.position += Vector3.up;
					Ray ray = new Ray(transform2.position, Vector3.down);
					RaycastHit raycastHit;
					if (Physics.Raycast(ray, out raycastHit, 1000f))
					{
						transform2.position = raycastHit.point;
					}
					else
					{
						UnityEngine.Object.Destroy(transform2.gameObject);
					}
				}
			}
		}
		float realtimeSinceStartup2 = Time.realtimeSinceStartup;
		float num = realtimeSinceStartup2 - realtimeSinceStartup;
		UnityEngine.Object.Destroy(this);
	}

	public void Highlight(bool on)
	{
		if (this.highlightedObject != null)
		{
			this.highlightedObject.SetActive(on);
		}
	}

	private void OnDestroy()
	{
		if (this.placementCollider != null)
		{
			UnityEngine.Object.Destroy(this.placementCollider);
		}
		if (this.highlightedObject != null)
		{
			UnityEngine.Object.Destroy(this.highlightedObject);
		}
	}

	public string Serialize()
	{
		string text = string.Empty;
		text = text + this.propID + "|";
		text = text + base.transform.position.x + "|";
		text = text + base.transform.position.y + "|";
		text = text + base.transform.position.z + "|";
		text = text + (int)base.transform.eulerAngles.x + "|";
		text = text + (int)base.transform.eulerAngles.y + "|";
		text = text + (int)base.transform.eulerAngles.z + "|";
		text = text + base.transform.localScale.x + "|";
		text = text + base.transform.localScale.y + "|";
		text = text + base.transform.localScale.z + "|";
		text = text + this.extra0Enabled.ToString() + "|";
		text = text + this.extra1Enabled.ToString() + "|";
		text = text + this.debrisSeed.ToString() + "|";
		text = text + this.debrisCount.ToString() + "|";
		text = text + this.currentLift + "|";
		for (int i = 0; i < this.snapPoints.Length; i++)
		{
			if (!(this.snapPoints[i].leftAffector == null) && !(this.snapPoints[i].rightAffector == null))
			{
				Vector3 position = this.snapPoints[i].leftAffector.transform.position;
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					position.x,
					"/",
					position.y,
					"/",
					position.z,
					"!"
				});
				Vector3 position2 = this.snapPoints[i].rightAffector.transform.position;
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					position2.x,
					"/",
					position2.y,
					"/",
					position2.z,
					"|"
				});
			}
		}
		return text;
	}

	public void Deserialize(string data)
	{
		string[] array = data.Split(new char[]
		{
			'|'
		});
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		Vector3 zero3 = Vector3.zero;
		this.propID = int.Parse(array[0]);
		zero.x = float.Parse(array[1]);
		zero.y = float.Parse(array[2]);
		zero.z = float.Parse(array[3]);
		zero2.x = float.Parse(array[4]);
		zero2.y = float.Parse(array[5]);
		zero2.z = float.Parse(array[6]);
		zero3.x = float.Parse(array[7]);
		zero3.y = float.Parse(array[8]);
		zero3.z = float.Parse(array[9]);
		this.extra0Enabled = bool.Parse(array[10]);
		this.extra1Enabled = bool.Parse(array[11]);
		this.debrisSeed = int.Parse(array[12]);
		this.debrisCount = int.Parse(array[13]);
		this.currentLift = float.Parse(array[14]);
		base.transform.position = zero;
		base.transform.eulerAngles = zero2;
		base.transform.localScale = zero3;
		if (array.Length > 16)
		{
			for (int i = 0; i < this.snapPoints.Length; i++)
			{
				if (!(this.snapPoints[i].leftAffector == null) && !(this.snapPoints[i].rightAffector == null))
				{
					string text = array[15 + i];
					string[] array2 = text.Split(new char[]
					{
						'!'
					});
					string text2 = array2[0];
					string[] array3 = text2.Split(new char[]
					{
						'/'
					});
					float x = float.Parse(array3[0]);
					float y = float.Parse(array3[1]);
					float z = float.Parse(array3[2]);
					this.snapPoints[i].leftAffector.transform.position = new Vector3(x, y, z);
					string text3 = array2[1];
					string[] array4 = text3.Split(new char[]
					{
						'/'
					});
					float x2 = float.Parse(array4[0]);
					float y2 = float.Parse(array4[1]);
					float z2 = float.Parse(array4[2]);
					this.snapPoints[i].rightAffector.transform.position = new Vector3(x2, y2, z2);
				}
			}
		}
		this.ToggleExtra0(this.extra0Enabled);
		this.ToggleExtra1(this.extra1Enabled);
		this.PlaceDebris(this.debrisSeed, this.debrisCount);
	}

	public int propID;

	public PropType propType;

	public string propName;

	public Sprite propImage;

	[Space(10f)]
	public float circleDrawerSizeMultiplier = 1f;

	[Space(10f)]
	public Collider placementCollider;

	public GameObject highlightedObject;

	[Header("Snapping")]
	public SnapPoint[] snapPoints;

	public float snapRadius;

	public bool snapAngle;

	public bool snapPosition;

	private float Xangle;

	private float Yangle;

	public float maxHeightIncrement;

	public float maxSideIncrement;

	public float maxCorrectingSizeIncrement;

	[Header("Alignment")]
	public Transform frontSupport;

	public Transform rearSupport;

	[Space(10f)]
	public float minScale = 1f;

	public float maxScale = 5f;

	[HideInInspector]
	public float defaultScale;

	[Space(10f)]
	public float minLift;

	public float maxLift = 5f;

	[HideInInspector]
	public float currentLift;

	[Header("Extras")]
	public GameObject extra0;

	public string extra0Name;

	public GameObject extra1;

	public string extra1Name;

	[HideInInspector]
	public bool extra0Enabled;

	[HideInInspector]
	public bool extra1Enabled;

	[Header("Debris")]
	public GameObject[] debrisPrefabs;

	public DebrisRect[] debrisRects;

	public Transform debrisParent;

	public float minDebrisScale = 0.2f;

	public float maxDebrisScale = 1f;

	public int maxDebrisCount;

	private int debrisSeed;

	private int debrisCount;

	[Header("Surface material")]
	public PhysicMaterial physicMaterial;

	[HideInInspector]
	public List<SnapPoint> attachedSnapPoints = new List<SnapPoint>();

	private float[] offsets;

	private bool initialized;

	private MeshRenderer[] preBakedMeshes;
}
