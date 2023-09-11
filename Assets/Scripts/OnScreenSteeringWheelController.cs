using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

public class OnScreenSteeringWheelController : MonoBehaviour
{
	public float GetClampedValue()
	{
		return this.wheelAngle / this.maximumSteeringAngle;
	}

	private void Start()
	{
		this.rect = base.GetComponent<RectTransform>();
		this.InitEventsSystem();
		this.UpdateRect();
	}

	private void OnEnable()
	{
		if (!CrossPlatformInputManager.AxisExists("Horizontal"))
		{
			this.m_SteerAxis = new CrossPlatformInputManager.VirtualAxis("Horizontal");
			CrossPlatformInputManager.RegisterVirtualAxis(this.m_SteerAxis);
		}
		else
		{
			this.m_SteerAxis = CrossPlatformInputManager.VirtualAxisReference("Horizontal");
		}
	}

	private void OnDisable()
	{
		this.m_SteerAxis.Remove();
	}

	private void Update()
	{
		if (!this.wheelBeingHeld && !Mathf.Approximately(0f, this.wheelAngle))
		{
			float num = this.wheelReleasedSpeed * Time.deltaTime;
			if (Mathf.Abs(num) > Mathf.Abs(this.wheelAngle))
			{
				this.wheelAngle = 0f;
			}
			else if (this.wheelAngle > 0f)
			{
				this.wheelAngle -= num;
			}
			else
			{
				this.wheelAngle += num;
			}
		}
		this.rect.localEulerAngles = Vector3.back * this.wheelAngle;
		this.m_SteerAxis.Update(this.GetClampedValue());
	}

	private void InitEventsSystem()
	{
		EventTrigger component = base.GetComponent<EventTrigger>();
		if (component.triggers == null)
		{
			component.triggers = new List<EventTrigger.Entry>();
		}
		EventTrigger.Entry entry = new EventTrigger.Entry();
		EventTrigger.TriggerEvent triggerEvent = new EventTrigger.TriggerEvent();
		UnityAction<BaseEventData> call = new UnityAction<BaseEventData>(this.PressEvent);
		triggerEvent.AddListener(call);
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback = triggerEvent;
		component.triggers.Add(entry);
		entry = new EventTrigger.Entry();
		triggerEvent = new EventTrigger.TriggerEvent();
		call = new UnityAction<BaseEventData>(this.DragEvent);
		triggerEvent.AddListener(call);
		entry.eventID = EventTriggerType.Drag;
		entry.callback = triggerEvent;
		component.triggers.Add(entry);
		entry = new EventTrigger.Entry();
		triggerEvent = new EventTrigger.TriggerEvent();
		call = new UnityAction<BaseEventData>(this.ReleaseEvent);
		triggerEvent.AddListener(call);
		entry.eventID = EventTriggerType.PointerUp;
		entry.callback = triggerEvent;
		component.triggers.Add(entry);
	}

	private void UpdateRect()
	{
		Vector3[] array = new Vector3[4];
		this.rect.GetWorldCorners(array);
		for (int i = 0; i < 4; i++)
		{
			array[i] = RectTransformUtility.WorldToScreenPoint(null, array[i]);
		}
		Vector3 vector = array[0];
		Vector3 vector2 = array[2];
		float width = vector2.x - vector.x;
		float height = vector2.y - vector.y;
		Rect rect = new Rect(vector.x, vector2.y, width, height);
		this.centerPoint = new Vector2(rect.x + rect.width * 0.5f, rect.y - rect.height * 0.5f);
	}

	public void PressEvent(BaseEventData eventData)
	{
		Vector2 position = ((PointerEventData)eventData).position;
		this.wheelBeingHeld = true;
		this.wheelPrevAngle = Vector2.Angle(Vector2.up, position - this.centerPoint);
	}

	public void DragEvent(BaseEventData eventData)
	{
		Vector2 position = ((PointerEventData)eventData).position;
		float num = Vector2.Angle(Vector2.up, position - this.centerPoint);
		if (Vector2.Distance(position, this.centerPoint) > 20f)
		{
			if (position.x > this.centerPoint.x)
			{
				this.wheelAngle += num - this.wheelPrevAngle;
			}
			else
			{
				this.wheelAngle -= num - this.wheelPrevAngle;
			}
		}
		this.wheelAngle = Mathf.Clamp(this.wheelAngle, -this.maximumSteeringAngle, this.maximumSteeringAngle);
		this.wheelPrevAngle = num;
	}

	public void ReleaseEvent(BaseEventData eventData)
	{
		this.DragEvent(eventData);
		this.wheelBeingHeld = false;
	}

	private RectTransform rect;

	private Vector2 centerPoint;

	public float maximumSteeringAngle = 200f;

	public float wheelReleasedSpeed = 200f;

	private float wheelAngle;

	private float wheelPrevAngle;

	private bool wheelBeingHeld;

	private CrossPlatformInputManager.VirtualAxis m_SteerAxis;
}
