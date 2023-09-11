using System;
using System.Collections.Generic;
using CustomVP;
using UnityEngine;

public class TankController : MonoBehaviour
{
	private CarController cc
	{
		get
		{
			if (this._cc == null)
			{
				this._cc = base.GetComponent<CarController>();
			}
			return this._cc;
		}
	}

	public TankWheelCollider[] borderWheelColliders
	{
		get
		{
			if (this._borderWheelColliders == null)
			{
				this._borderWheelColliders = new TankWheelCollider[4];
				this._borderWheelColliders[0] = this.leftWheels[this.leftWheels.Length - 1];
				this._borderWheelColliders[1] = this.rightWheels[this.rightWheels.Length - 1];
				this._borderWheelColliders[2] = this.leftWheels[0];
				this._borderWheelColliders[3] = this.rightWheels[0];
			}
			return this._borderWheelColliders;
		}
	}

	public TankWheelCollider[] allWheelColliders
	{
		get
		{
			if (this._allWheelColliders == null)
			{
				List<TankWheelCollider> list = new List<TankWheelCollider>();
				foreach (TankWheelCollider item in this.leftWheels)
				{
					list.Add(item);
				}
				foreach (TankWheelCollider item2 in this.rightWheels)
				{
					list.Add(item2);
				}
				this._allWheelColliders = list.ToArray();
			}
			return this._allWheelColliders;
		}
	}

	public void Start()
	{
		this.rb = base.GetComponent<Rigidbody>();
		if (this.rb)
		{
			this.rb.centerOfMass = this.centerOfMass.localPosition;
		}
	}

	private void Update()
	{
		if (!this.rb)
		{
			return;
		}
		float num = this.cc.yInput;
		float num2 = this.cc.xInput;
		if ((VehicleLoader.Instance != null && VehicleLoader.Instance.droneMode) || !this.cc.vehicleIsActive)
		{
			num = 0f;
			num2 = 0f;
		}
		float num3 = 0f;
		if (this.cc.Speed > 0.1f)
		{
			num3 = -Mathf.Clamp(num, -1f, 0f);
		}
		else if (this.cc.Speed < -0.1f)
		{
			num3 = Mathf.Clamp(num, 0f, 1f);
		}
		num3 *= Mathf.InverseLerp(1f, 2f, Mathf.Abs(this.cc.Speed));
		float num4 = this.cc.RollingResistance * Mathf.InverseLerp(0.1f, 0.8f, Mathf.Abs(this.cc.Speed));
		num3 += Mathf.Clamp01(num4 * (1f - Mathf.Abs(this.gasInput) - Mathf.Abs(num2)));
		this.brakeInput = Mathf.MoveTowards(this.brakeInput, num3, Time.deltaTime * 2f);
		float num5 = this.maxSteerTorque * Mathf.Clamp(num2, 0f, 1f);
		float num6 = this.maxSteerTorque * -Mathf.Clamp(num2, -1f, 0f);
		float num7 = num;
		float min = -1f;
		float max = 1f;
		if (this.cc.Speed < 0f)
		{
			max = Mathf.InverseLerp(3f, 2f, Mathf.Abs(this.cc.Speed));
		}
		if (this.cc.Speed > 3f)
		{
			min = -Mathf.InverseLerp(3f, 2f, Mathf.Abs(this.cc.Speed));
		}
		num7 = Mathf.Clamp(num7, min, max);
		this.gasInput = Mathf.MoveTowards(this.gasInput, num7, Time.deltaTime * 2f);
		float num8 = this.cc.LeveledMaxTorque * this.torqueCurve.Evaluate(Mathf.Abs(this.cc.Speed) / this.cc.LeveledMaxSpeed) * this.gasInput;
		float num9 = 1.5f;
		float num10 = 1f;
		if (this.cc.Speed < 0f)
		{
			num9 = 1f;
			num10 = 1.5f;
		}
		int num11 = 0;
		foreach (TankWheelCollider tankWheelCollider in this.leftWheels)
		{
			tankWheelCollider.currentMotorTorque = num8 + num5 * num10 - num6 * num9;
			tankWheelCollider.currentBrakeTorque = this.cc.BrakeTorque * this.brakeInput;
			if (tankWheelCollider.grounded)
			{
				num11++;
			}
		}
		if (num11 > 0)
		{
			float num12 = (float)(this.leftWheels.Length - num11) / (float)num11;
			foreach (TankWheelCollider tankWheelCollider2 in this.leftWheels)
			{
				tankWheelCollider2.forwardTorqueMult = 1f + num12;
			}
		}
		num11 = 0;
		foreach (TankWheelCollider tankWheelCollider3 in this.rightWheels)
		{
			tankWheelCollider3.currentMotorTorque = num8 + num6 * num10 - num5 * num9;
			tankWheelCollider3.currentBrakeTorque = this.cc.BrakeTorque * this.brakeInput;
			if (tankWheelCollider3.grounded)
			{
				num11++;
			}
		}
		if (num11 > 0)
		{
			float num13 = (float)(this.rightWheels.Length - num11) / (float)num11;
			foreach (TankWheelCollider tankWheelCollider4 in this.rightWheels)
			{
				tankWheelCollider4.forwardTorqueMult = 1f + num13;
			}
		}
	}

	private CarController _cc;

	private Rigidbody rb;

	public Transform centerOfMass;

	public TankWheelCollider[] leftWheels;

	public TankWheelCollider[] rightWheels;

	private TankWheelCollider[] _borderWheelColliders;

	private TankWheelCollider[] _allWheelColliders;

	public float maxSteerTorque;

	public AnimationCurve torqueCurve;

	private float brakeInput;

	private float gasInput;
}
