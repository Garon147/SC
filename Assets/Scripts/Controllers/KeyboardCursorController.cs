using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;




public class KeyboardCursorController : MonoBehaviour {

	/*public struct Point
	{
		public int x;
		public int y;
	}

	[DllImport("user32.dll")]
	public static extern bool SetCursorPos(int x, int y);
	[DllImport("user32.dll")]
	public static extern bool GetCursorPos(out Point pos);

	Point cursorPos;

	// Use this for initialization
	void Start () 
	{
//		cursorPos = new Point ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		cursorPos = new Point ();
		GetCursorPos (out cursorPos);
		SetCursorPos (cursorPos.x, cursorPos.y);
	}*/

	public enum MouseEventFlags
	{
		LeftDown = 0x00000002,
		LeftUp = 0x00000004,
		MiddleDown = 0x00000020,
		MiddleUp = 0x00000040,
		RightDown = 0x00000008,
		RightUp = 0x00000010,
		Absolute = 0x00008000,
		Move = 0x00000001
	}

	[DllImport("user32.dll", EntryPoint = "SetCursorPos")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool SetCursorPos(int X, int Y);

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GetCursorPos(out MousePoint lpMousePoint);

	[DllImport("user32.dll")]
	private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

	public static void SetCursorPosition(int X, int Y) 
	{
		SetCursorPos(X, Y);
	}

	public static void SetCursorPosition(MousePoint point)
	{
		SetCursorPos(point.X, point.Y);
	}

	public static MousePoint GetCursorPosition()
	{
		MousePoint currentMousePoint;
		var gotPoint = GetCursorPos(out currentMousePoint);
		if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
		return currentMousePoint;
	}

	public static void MouseEvent(MouseEventFlags value)
	{
		MousePoint position = GetCursorPosition();

		mouse_event
		((int)value,
			position.X,
			position.Y,
			0,
			0)
		;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct MousePoint
	{
		public int X;
		public int Y;

		public MousePoint(int x, int y)
		{
			X = x;
			Y = y;
		}

	}
}
