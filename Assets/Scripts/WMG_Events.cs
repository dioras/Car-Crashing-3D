using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class WMG_Events : WMG_GUI_Functions
{
	private void AddEventTrigger(UnityAction<GameObject> action, EventTriggerType triggerType, GameObject go)
	{
		EventTrigger eventTrigger = go.GetComponent<EventTrigger>();
		if (eventTrigger == null)
		{
			eventTrigger = go.AddComponent<EventTrigger>();
			eventTrigger.triggers = new List<EventTrigger.Entry>();
		}
		EventTrigger.TriggerEvent triggerEvent = new EventTrigger.TriggerEvent();
		triggerEvent.AddListener(delegate(BaseEventData eventData)
		{
			action(go);
		});
		EventTrigger.Entry item = new EventTrigger.Entry
		{
			callback = triggerEvent,
			eventID = triggerType
		};
		eventTrigger.triggers.Add(item);
	}

	private void AddEventTrigger(UnityAction<GameObject, bool> action, EventTriggerType triggerType, GameObject go, bool state)
	{
		EventTrigger eventTrigger = go.GetComponent<EventTrigger>();
		if (eventTrigger == null)
		{
			eventTrigger = go.AddComponent<EventTrigger>();
			eventTrigger.triggers = new List<EventTrigger.Entry>();
		}
		EventTrigger.TriggerEvent triggerEvent = new EventTrigger.TriggerEvent();
		triggerEvent.AddListener(delegate(BaseEventData eventData)
		{
			action(go, state);
		});
		EventTrigger.Entry item = new EventTrigger.Entry
		{
			callback = triggerEvent,
			eventID = triggerType
		};
		eventTrigger.triggers.Add(item);
	}

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event WMG_Events.WMG_Click_H WMG_Click;

	public void addNodeClickEvent(GameObject go)
	{
		this.AddEventTrigger(new UnityAction<GameObject>(this.WMG_Click_2), EventTriggerType.PointerClick, go);
	}

	private void WMG_Click_2(GameObject go)
	{
		if (this.WMG_Click != null)
		{
			WMG_Series component = go.transform.parent.parent.GetComponent<WMG_Series>();
			this.WMG_Click(component, go.GetComponent<WMG_Node>());
		}
	}

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event WMG_Events.WMG_Link_Click_H WMG_Link_Click;

	public void addLinkClickEvent(GameObject go)
	{
		this.AddEventTrigger(new UnityAction<GameObject>(this.WMG_Link_Click_2), EventTriggerType.PointerClick, go);
	}

	private void WMG_Link_Click_2(GameObject go)
	{
		if (this.WMG_Link_Click != null)
		{
			WMG_Series component = go.transform.parent.parent.GetComponent<WMG_Series>();
			this.WMG_Link_Click(component, go.GetComponent<WMG_Link>());
		}
	}

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event WMG_Events.WMG_Click_Leg_H WMG_Click_Leg;

	public void addNodeClickEvent_Leg(GameObject go)
	{
		this.AddEventTrigger(new UnityAction<GameObject>(this.WMG_Click_Leg_2), EventTriggerType.PointerClick, go);
	}

	private void WMG_Click_Leg_2(GameObject go)
	{
		if (this.WMG_Click_Leg != null)
		{
			WMG_Series seriesRef = go.transform.parent.GetComponent<WMG_Legend_Entry>().seriesRef;
			this.WMG_Click_Leg(seriesRef, go.GetComponent<WMG_Node>());
		}
	}

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event WMG_Events.WMG_Link_Click_Leg_H WMG_Link_Click_Leg;

	public void addLinkClickEvent_Leg(GameObject go)
	{
		this.AddEventTrigger(new UnityAction<GameObject>(this.WMG_Link_Click_Leg_2), EventTriggerType.PointerClick, go);
	}

	private void WMG_Link_Click_Leg_2(GameObject go)
	{
		if (this.WMG_Link_Click_Leg != null)
		{
			WMG_Series seriesRef = go.transform.parent.GetComponent<WMG_Legend_Entry>().seriesRef;
			this.WMG_Link_Click_Leg(seriesRef, go.GetComponent<WMG_Link>());
		}
	}

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event WMG_Events.WMG_MouseEnter_H WMG_MouseEnter;

	public void addNodeMouseEnterEvent(GameObject go)
	{
		this.AddEventTrigger(new UnityAction<GameObject, bool>(this.WMG_MouseEnter_2), EventTriggerType.PointerEnter, go, true);
		this.AddEventTrigger(new UnityAction<GameObject, bool>(this.WMG_MouseEnter_2), EventTriggerType.PointerExit, go, false);
	}

	private void WMG_MouseEnter_2(GameObject go, bool state)
	{
		if (this.WMG_MouseEnter != null)
		{
			WMG_Series component = go.transform.parent.parent.GetComponent<WMG_Series>();
			this.WMG_MouseEnter(component, go.GetComponent<WMG_Node>(), state);
		}
	}

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event WMG_Events.WMG_Link_MouseEnter_H WMG_Link_MouseEnter;

	public void addLinkMouseEnterEvent(GameObject go)
	{
		this.AddEventTrigger(new UnityAction<GameObject, bool>(this.WMG_Link_MouseEnter_2), EventTriggerType.PointerEnter, go, true);
		this.AddEventTrigger(new UnityAction<GameObject, bool>(this.WMG_Link_MouseEnter_2), EventTriggerType.PointerExit, go, false);
	}

	private void WMG_Link_MouseEnter_2(GameObject go, bool state)
	{
		if (this.WMG_Link_MouseEnter != null)
		{
			WMG_Series component = go.transform.parent.parent.GetComponent<WMG_Series>();
			this.WMG_Link_MouseEnter(component, go.GetComponent<WMG_Link>(), state);
		}
	}

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event WMG_Events.WMG_MouseEnter_Leg_H WMG_MouseEnter_Leg;

	public void addNodeMouseEnterEvent_Leg(GameObject go)
	{
		this.AddEventTrigger(new UnityAction<GameObject, bool>(this.WMG_MouseEnter_Leg_2), EventTriggerType.PointerEnter, go, true);
		this.AddEventTrigger(new UnityAction<GameObject, bool>(this.WMG_MouseEnter_Leg_2), EventTriggerType.PointerExit, go, false);
	}

	private void WMG_MouseEnter_Leg_2(GameObject go, bool state)
	{
		if (this.WMG_MouseEnter_Leg != null)
		{
			WMG_Series seriesRef = go.transform.parent.GetComponent<WMG_Legend_Entry>().seriesRef;
			this.WMG_MouseEnter_Leg(seriesRef, go.GetComponent<WMG_Node>(), state);
		}
	}

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event WMG_Events.WMG_Link_MouseEnter_Leg_H WMG_Link_MouseEnter_Leg;

	public void addLinkMouseEnterEvent_Leg(GameObject go)
	{
		this.AddEventTrigger(new UnityAction<GameObject, bool>(this.WMG_Link_MouseEnter_Leg_2), EventTriggerType.PointerEnter, go, true);
		this.AddEventTrigger(new UnityAction<GameObject, bool>(this.WMG_Link_MouseEnter_Leg_2), EventTriggerType.PointerExit, go, false);
	}

	private void WMG_Link_MouseEnter_Leg_2(GameObject go, bool state)
	{
		if (this.WMG_Link_MouseEnter_Leg != null)
		{
			WMG_Series seriesRef = go.transform.parent.GetComponent<WMG_Legend_Entry>().seriesRef;
			this.WMG_Link_MouseEnter_Leg(seriesRef, go.GetComponent<WMG_Link>(), state);
		}
	}

	public void addNodeMouseLeaveEvent(GameObject go)
	{
	}

	public void addLinkMouseLeaveEvent(GameObject go)
	{
	}

	public void addNodeMouseLeaveEvent_Leg(GameObject go)
	{
	}

	public void addLinkMouseLeaveEvent_Leg(GameObject go)
	{
	}

	public delegate void WMG_Click_H(WMG_Series aSeries, WMG_Node aNode);

	public delegate void WMG_Link_Click_H(WMG_Series aSeries, WMG_Link aLink);

	public delegate void WMG_Click_Leg_H(WMG_Series aSeries, WMG_Node aNode);

	public delegate void WMG_Link_Click_Leg_H(WMG_Series aSeries, WMG_Link aLink);

	public delegate void WMG_MouseEnter_H(WMG_Series aSeries, WMG_Node aNode, bool state);

	public delegate void WMG_Link_MouseEnter_H(WMG_Series aSeries, WMG_Link aLink, bool state);

	public delegate void WMG_MouseEnter_Leg_H(WMG_Series aSeries, WMG_Node aNode, bool state);

	public delegate void WMG_Link_MouseEnter_Leg_H(WMG_Series aSeries, WMG_Link aLink, bool state);
}
