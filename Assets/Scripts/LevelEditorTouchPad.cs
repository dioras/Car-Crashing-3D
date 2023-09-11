using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class LevelEditorTouchPad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
{
	private void Start()
	{
		this.levelEditor = LevelEditor.Instance;
		this.CreateVirtualAxes();
		this.prevTouchPos = new Vector2[2];
		this.pointerDeltas = new Vector2[2];
		this.startTouchPos = new Vector2[2];
		SceneManager.sceneUnloaded += this.DestroyVirtualAxes<Scene>;
	}

	private void CreateVirtualAxes()
	{
		this.m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis("Drag X");
		CrossPlatformInputManager.RegisterVirtualAxis(this.m_HorizontalVirtualAxis);
		this.m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis("Drag Y");
		CrossPlatformInputManager.RegisterVirtualAxis(this.m_VerticalVirtualAxis);
		this.m_ZoomVirtualAxis = new CrossPlatformInputManager.VirtualAxis("Zoom");
		CrossPlatformInputManager.RegisterVirtualAxis(this.m_ZoomVirtualAxis);
	}

	private void DestroyVirtualAxes<Scene>(Scene scene)
	{
		CrossPlatformInputManager.UnRegisterVirtualAxis("Drag Y");
		CrossPlatformInputManager.UnRegisterVirtualAxis("Drag X");
		CrossPlatformInputManager.UnRegisterVirtualAxis("Zoom");
		SceneManager.sceneUnloaded -= DestroyVirtualAxes;
	}

	private void UpdateDragAxes(Vector3 value)
	{
		this.m_HorizontalVirtualAxis.Update(value.x);
		this.m_VerticalVirtualAxis.Update(value.y);
	}

	private void UpdateZoomAxis(float value)
	{
		this.m_ZoomVirtualAxis.Update(value);
	}

	public void OnPointerDown(PointerEventData data)
	{
		
		#if UNITY_EDITOR

		List<Touch> touches = InputHelper.GetTouches();
		this.dragging = true;
		this.levelEditor.draggingScreen = true;
		this.prevMousePos = UnityEngine.Input.mousePosition;
		this.lastTouchID = data.pointerId;
		this.prevTouchPos[this.lastTouchID] = touches[this.lastTouchID].position;
		this.startTouchPos[this.lastTouchID] = touches[this.lastTouchID].position;
		if (UnityEngine.Input.touchCount == 2)
		{
			this.startTouchDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
			this.lastTouchDistance = this.startTouchDistance;
		}
		#else
		this.dragging = true;
		this.levelEditor.draggingScreen = true;
		this.prevMousePos = UnityEngine.Input.mousePosition;
		this.lastTouchID = data.pointerId;
		this.prevTouchPos[this.lastTouchID] = Input.touches[this.lastTouchID].position;
		this.startTouchPos[this.lastTouchID] = Input.touches[this.lastTouchID].position;
		if (UnityEngine.Input.touchCount == 2)
		{
			this.startTouchDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
			this.lastTouchDistance = this.startTouchDistance;
		}
		#endif
		
	}

	private void Update()
	{
		if (UnityEngine.Input.touchCount == 1 && this.lastTouchID > -1 && this.lastTouchID < 2)
		{
			this.pointerDeltas[this.lastTouchID] = new Vector2(Input.touches[this.lastTouchID].position.x - this.prevTouchPos[this.lastTouchID].x, Input.touches[this.lastTouchID].position.y - this.prevTouchPos[this.lastTouchID].y);
			this.prevTouchPos[this.lastTouchID] = Input.touches[this.lastTouchID].position;
			this.UpdateDragAxes(new Vector3(this.pointerDeltas[this.lastTouchID].x, this.pointerDeltas[this.lastTouchID].y, 0f));
		}
		if (UnityEngine.Input.touchCount == 2 && this.lastTouchID > -1 && this.lastTouchID < 2)
		{
			for (int i = 0; i < 2; i++)
			{
				this.pointerDeltas[i] = new Vector2(Input.touches[i].position.x - this.prevTouchPos[i].x, Input.touches[i].position.y - this.prevTouchPos[i].y);
				this.prevTouchPos[i] = Input.touches[i].position;
			}
			Vector3 vector = (this.pointerDeltas[0] + this.pointerDeltas[1]) / 2f;
			float num = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
			float num2 = num - this.lastTouchDistance;
			this.lastTouchDistance = num;
			this.UpdateDragAxes(new Vector3(vector.x, vector.y, 0f));
			this.UpdateZoomAxis(num2 * 3f);
		}
	}

	public void OnPointerUp(PointerEventData data)
	{
		this.dragging = false;
		this.levelEditor.draggingScreen = false;
		this.lastTouchID = -1;
		this.UpdateDragAxes(Vector3.zero);
		this.UpdateZoomAxis(0f);
		bool flag = false;
		if ((data.position - this.lastTapPos).magnitude < 20f && Time.realtimeSinceStartup - this.lastTapTime < 0.5f)
		{
			flag = true;
			this.lastTapTime = 0f;
		}
		if (!flag)
		{
			this.lastTapTime = Time.realtimeSinceStartup;
			this.lastTapPos = data.position;
		}
		bool fingerMoved = Vector2.Distance(data.position, this.startTouchPos[data.pointerId]) > 20f;
		this.levelEditor.OnTouchTap(data.position, fingerMoved, flag);
	}

	private void OnDestroy()
	{
		if (CrossPlatformInputManager.AxisExists("Drag X"))
		{
			CrossPlatformInputManager.UnRegisterVirtualAxis("Drag X");
		}
		if (CrossPlatformInputManager.AxisExists("Drag Y"))
		{
			CrossPlatformInputManager.UnRegisterVirtualAxis("Drag Y");
		}
	}

	private LevelEditor levelEditor;

	private const string horizontalAxisName = "Drag X";

	private const string verticalAxisName = "Drag Y";

	private const string zoomAxisName = "Zoom";

	private CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis;

	private CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis;

	private CrossPlatformInputManager.VirtualAxis m_ZoomVirtualAxis;

	private bool dragging;

	public int lastTouchID = -1;

	private Vector2[] prevTouchPos;

	private Vector2[] pointerDeltas;

	private Vector2[] startTouchPos;

	private float startTouchDistance;

	private float lastTouchDistance;

	private Vector3 prevMousePos;

	private Vector2 mouseDelta;

	private Vector2 lastTapPos;

	private float lastTapTime;
}
