using System;
using System.Collections.Generic;
using UnityEngine;

namespace GeNa
{
	public class Gravity : ScriptableObject
	{
		public void UpdateInstances()
		{
			foreach (Gravity.GravityInstance gravityInstance in this.m_instances)
			{
				if (gravityInstance.m_instance != null)
				{
					gravityInstance.m_endPosition = gravityInstance.m_instance.transform.position;
					gravityInstance.m_endRotation = gravityInstance.m_instance.transform.rotation.eulerAngles;
				}
				this.m_haveGravity = true;
			}
		}

		public void AddInstances(List<Gravity.GravityInstance> instanceList)
		{
			this.m_haveGravity = false;
			this.m_instances.AddRange(instanceList);
		}

		public void UpdateOriginalsToStart()
		{
			foreach (Gravity.GravityInstance gravityInstance in this.m_instances)
			{
				if (gravityInstance.m_instance != null)
				{
					gravityInstance.m_instance.transform.position = gravityInstance.m_startPosition;
					gravityInstance.m_instance.transform.rotation = Quaternion.Euler(gravityInstance.m_startRotation.x, gravityInstance.m_startRotation.y, gravityInstance.m_startRotation.z);
				}
			}
		}

		public void UpdateOriginalsToEnd()
		{
			foreach (Gravity.GravityInstance gravityInstance in this.m_instances)
			{
				if (gravityInstance.m_instance != null)
				{
					gravityInstance.m_instance.transform.position = gravityInstance.m_endPosition;
					gravityInstance.m_instance.transform.rotation = Quaternion.Euler(gravityInstance.m_endRotation.x, gravityInstance.m_endRotation.y, gravityInstance.m_endRotation.z);
				}
			}
		}

		public void FinaliseGravity(Spawner spawner)
		{
			spawner.LoadLightProbes();
			foreach (Gravity.GravityInstance gravityInstance in this.m_instances)
			{
				if (gravityInstance.m_instance != null)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(gravityInstance.m_resource.m_prefab);
					gameObject.name = "_Sp_" + gravityInstance.m_resource.m_name;
					if (gravityInstance.m_resource.m_conformToSlope)
					{
						gameObject.name = "_Sp_" + gravityInstance.m_resource.m_name + " C";
					}
					gameObject.transform.position = gravityInstance.m_endPosition;
					gameObject.transform.localScale = gravityInstance.m_instance.transform.localScale;
					gameObject.transform.rotation = Quaternion.Euler(gravityInstance.m_endRotation.x, gravityInstance.m_endRotation.y, gravityInstance.m_endRotation.z);
					gameObject.transform.parent = gravityInstance.m_instance.transform.parent;
					spawner.AutoOptimiseGameObject(gravityInstance.m_resource, gameObject);
					spawner.AutoProbeGameObject(gravityInstance.m_resource, gameObject);
					UnityEngine.Object.DestroyImmediate(gravityInstance.m_instance);
				}
			}
			this.m_haveGravity = false;
			this.m_instances.Clear();
		}

		public bool m_haveGravity;

		public List<Gravity.GravityInstance> m_instances = new List<Gravity.GravityInstance>();

		[Serializable]
		public class GravityInstance
		{
			public Resource m_resource;

			public GameObject m_instance;

			public Vector3 m_startPosition;

			public Vector3 m_endPosition;

			public Vector3 m_startRotation;

			public Vector3 m_endRotation;
		}
	}
}
