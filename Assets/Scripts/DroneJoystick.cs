using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

public class DroneJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEventSystemHandler
{
	private float relativeMovementRange
	{
		get
		{
			return (float)(Screen.width * this.MovementRange) * 0.01f;
		}
	}

	private void Awake()
	{
		DroneJoystick.Instance = this;
	}

	private void Start()
	{
		this.m_StartPos = base.transform.position;
	}

	private void OnEnable()
	{
		if (!CrossPlatformInputManager.AxisExists(this.horizontalAxisName))
		{
			this.m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(this.horizontalAxisName);
			CrossPlatformInputManager.RegisterVirtualAxis(this.m_HorizontalVirtualAxis);
		}
		else
		{
			this.m_HorizontalVirtualAxis = CrossPlatformInputManager.VirtualAxisReference(this.horizontalAxisName);
		}
		if (!CrossPlatformInputManager.AxisExists(this.verticalAxisName))
		{
			this.m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(this.verticalAxisName);
			CrossPlatformInputManager.RegisterVirtualAxis(this.m_VerticalVirtualAxis);
		}
		else
		{
			this.m_VerticalVirtualAxis = CrossPlatformInputManager.VirtualAxisReference(this.verticalAxisName);
		}
	}

	private void OnDisable()
	{
		this.m_VerticalVirtualAxis.Remove();
		this.m_HorizontalVirtualAxis.Remove();
	}

	private void UpdateVirtualAxes(Vector3 value)
	{
		this.delta = this.m_StartPos - value;
		this.delta.y = -this.delta.y;
		this.delta /= this.relativeMovementRange;
		this.delta *= this.AxisMultiplier;
		this.m_HorizontalVirtualAxis.Update(-this.delta.x);
		this.m_VerticalVirtualAxis.Update(this.delta.y);
	}

	public void OnDrag(PointerEventData data)
	{
		Vector3 a = new Vector3(data.position.x, data.position.y, 0f);
		Vector3 b = a - this.m_StartPos;
		if (b.magnitude > this.relativeMovementRange)
		{
			b = b.normalized * this.relativeMovementRange;
		}
		base.transform.position = this.m_StartPos + b;
	}

	public void OnPointerUp(PointerEventData data)
	{
		this.dragging = false;
	}

	public void OnPointerDown(PointerEventData data)
	{
		this.dragging = true;
	}

	private void Update()
	{
		if (!this.dragging)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, this.m_StartPos, Time.deltaTime * 10f);
		}
		this.UpdateVirtualAxes(base.transform.position);
	}

	private void OnDestroy()
	{
		this.m_HorizontalVirtualAxis.Remove();
		this.m_VerticalVirtualAxis.Remove();
	}

	public static DroneJoystick Instance;

	public int MovementRange = 5;

	public string horizontalAxisName = "Horizontal";

	public string verticalAxisName = "Vertical";

	[Range(0f, 1f)]
	public float AxisMultiplier = 1f;

	private Vector3 m_StartPos;

	public bool dragging;

	private CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis;

	private CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis;

	[HideInInspector]
	public Vector3 delta;
}
