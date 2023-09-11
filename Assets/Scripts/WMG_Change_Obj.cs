using System;
using System.Diagnostics;
using UnityEngine;

public class WMG_Change_Obj
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event WMG_Change_Obj.ObjChangedHandler OnChange;

	public void Changed()
	{
		WMG_Change_Obj.ObjChangedHandler onChange = this.OnChange;
		if (onChange != null && this.changeOk())
		{
			onChange();
		}
	}

	private bool changeOk()
	{
		if (!Application.isPlaying)
		{
			return false;
		}
		if (this.changesPaused)
		{
			this.changePaused = true;
			return false;
		}
		return true;
	}

	public void UnsubscribeAllHandlers()
	{
		if (this.OnChange != null)
		{
			foreach (Delegate @delegate in this.OnChange.GetInvocationList())
			{
				this.OnChange -= (@delegate as WMG_Change_Obj.ObjChangedHandler);
			}
		}
	}

	public bool changesPaused;

	public bool changePaused;

	public delegate void ObjChangedHandler();
}
