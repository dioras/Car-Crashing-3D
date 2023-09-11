using System;
using UnityEngine;

namespace CustomVP
{
	public class CustomWheelFrictionCurve
	{
		public CustomWheelFrictionCurve() : this(0.06f, 1.2f, 0.08f, 1f, 0.6f)
		{
		}

		public CustomWheelFrictionCurve(float extSlip, float extVal, float asSlip, float asVal, float tailVal)
		{
			this.keyframes = new Keyframe[4];
			this.curveData = new AnimationCurve();
			this.extSlip = extSlip;
			this.extVal = extVal;
			this.asSlip = asSlip;
			this.asVal = asVal;
			this.tailVal = tailVal;
			this.setupCurve();
		}

		public float extremumSlip
		{
			get
			{
				return this.extSlip;
			}
			set
			{
				this.extSlip = value;
				this.setupCurve();
			}
		}

		public float extremumValue
		{
			get
			{
				return this.extVal;
			}
			set
			{
				this.extVal = value;
				this.setupCurve();
			}
		}

		public float asymptoteSlip
		{
			get
			{
				return this.asSlip;
			}
			set
			{
				this.asSlip = value;
				this.setupCurve();
			}
		}

		public float asymptoteValue
		{
			get
			{
				return this.asVal;
			}
			set
			{
				this.asVal = value;
				this.setupCurve();
			}
		}

		public float tailValue
		{
			get
			{
				return this.tailVal;
			}
			set
			{
				this.tailVal = value;
				this.setupCurve();
			}
		}

		public float max
		{
			get
			{
				return Mathf.Max(this.asVal, this.extVal);
			}
		}

		public float evaluate(float slipRatio)
		{
			return this.curveData.Evaluate(this.clampRatio(slipRatio));
		}

		private void setupCurve()
		{
			this.keyframes[0].time = 0f;
			this.keyframes[0].value = 0f;
			this.keyframes[1].time = this.extSlip;
			this.keyframes[1].value = this.extVal;
			this.keyframes[2].time = this.asSlip;
			this.keyframes[2].value = this.asVal;
			this.keyframes[3].time = 1f;
			this.keyframes[3].value = this.tailVal;
			int length = this.curveData.length;
			for (int i = length - 1; i >= 0; i--)
			{
				this.curveData.RemoveKey(i);
			}
			this.curveData.AddKey(this.keyframes[0]);
			this.curveData.AddKey(this.keyframes[1]);
			this.curveData.AddKey(this.keyframes[2]);
			this.curveData.AddKey(this.keyframes[3]);
		}

		private float clampRatio(float slipRatio)
		{
			slipRatio = Mathf.Abs(slipRatio);
			slipRatio = Mathf.Min(1f, slipRatio);
			slipRatio = Mathf.Max(0f, slipRatio);
			return slipRatio;
		}

		private AnimationCurve curveData;

		private float extSlip;

		private float extVal;

		private float asSlip;

		private float asVal;

		private float tailVal;

		private Keyframe[] keyframes;
	}
}
