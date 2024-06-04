using System.Runtime.InteropServices;
using System;

class CBridge
{
    [DllImport("user32.dll", CharSet = CharSet.Auto)] public static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll", CharSet = CharSet.Auto)] public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
    [DllImport("user32.dll", CharSet = CharSet.Auto)] public static extern bool GetCursorPos(out POINT lpPoint);
    [DllImport("user32.dll", CharSet = CharSet.Auto)] public static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);

    public const int WM_LBUTTONDOWN = 0x0201;
    public const int WM_LBUTTONUP = 0x0202;
    public const int WM_RBUTTONDOWN = 0x0204;
    public const int WM_RBUTTONUP = 0x0205;

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
    }
}