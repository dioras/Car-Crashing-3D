using System;
using UnityEngine;

public class WinchCable
{
	public bool IsCarMissing()
	{
		CableType cableType = this.cableType;
		if (cableType != CableType.CarToCar)
		{
			return cableType == CableType.CarToStatic && this.Car == null;
		}
		return this.t1 == null || this.t2 == null;
	}

	public void UpdateCable()
	{
		if (this.cableType == CableType.CarToCar)
		{
			this.lineRenderer.SetPosition(0, this.t1.position);
			this.lineRenderer.SetPosition(1, this.t2.position);
		}
		if (this.cableType == CableType.CarToStatic)
		{
			this.lineRenderer.SetPosition(0, this.Car.position);
			this.lineRenderer.SetPosition(1, this.CarTargetPos);
		}
	}

	public CableType cableType;

	public LineRenderer lineRenderer;

	public string CableID;

	public Transform t1;

	public Transform t2;

	public Transform Car;

	public Vector3 CarTargetPos;
}
