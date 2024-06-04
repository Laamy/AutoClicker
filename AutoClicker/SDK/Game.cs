using System;

using static CBridge;

class Game
{
    public static IntPtr CursorToLParam(IntPtr Hwnd)
    {
        if (Hwnd == IntPtr.Zero)
            return IntPtr.Zero;

        POINT cursorPos;

        if (!GetCursorPos(out cursorPos))
            return IntPtr.Zero;

        if (!ScreenToClient(Hwnd, ref cursorPos))
            return IntPtr.Zero;

        IntPtr lParam = (IntPtr)((cursorPos.Y << 16) | (cursorPos.X & 0xFFFF)); // shift into lParam

        return lParam;
    }

    public static void LeftClick()
    {
        IntPtr Hwnd = GetForegroundWindow();

        IntPtr lParam = CursorToLParam(Hwnd);

        PostMessage(Hwnd, WM_LBUTTONDOWN, new IntPtr(1), lParam);
        PostMessage(Hwnd, WM_LBUTTONUP, new IntPtr(1), lParam);
    }

    public static void RightClick()
    {
        IntPtr Hwnd = GetForegroundWindow();

        IntPtr lParam = CursorToLParam(Hwnd);

        PostMessage(Hwnd, WM_RBUTTONDOWN, new IntPtr(1), lParam);
        PostMessage(Hwnd, WM_RBUTTONUP, new IntPtr(1), lParam);
    }
}