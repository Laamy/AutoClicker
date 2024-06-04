using AutoClicker;

using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

class Program
{
    static Keymap map;

    static int CPS = 18;

    static void Main(string[] args)
    {
        map = new Keymap();

        Task.Factory.StartNew(() =>
        {
            while (true)
            {
                Thread.Sleep(1000 / CPS);

                map.Tick();
            }
        });

        map.OnKeyPress += OnKeypress;

        Console.ReadKey();
    }

    private static void OnKeypress(KeyEvent @event)
    {
        if (@event.VKey != VKeyCodes.KeyHeld)
            return;

        if (@event.Key == 1 && map.GetDown((uint)Keys.LShiftKey)) //  && map.GetDown((uint)Keys.LShiftKey)
        {
            HitPlayer();
        }
        if (@event.Key == 2 && map.GetDown((uint)Keys.LShiftKey)) //  && map.GetDown((uint)Keys.LShiftKey)
        {
            PlaceBlock();
        }
    }

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

    public static void HitPlayer()
    {
        IntPtr Hwnd = GetForegroundWindow();

        IntPtr lParam = CursorToLParam(Hwnd);

        PostMessage(Hwnd, WM_LBUTTONDOWN, new IntPtr(1), lParam);
        //Thread.Sleep(15); // minimal sleep
        PostMessage(Hwnd, WM_LBUTTONUP, new IntPtr(1), lParam);
    }

    public static void PlaceBlock()
    {
        IntPtr Hwnd = GetForegroundWindow();

        IntPtr lParam = CursorToLParam(Hwnd);

        PostMessage(Hwnd, WM_RBUTTONDOWN, new IntPtr(1), lParam);
        //Thread.Sleep(15); // minimal sleep
        PostMessage(Hwnd, WM_RBUTTONUP, new IntPtr(1), lParam);
    }

    // SYSTEM32 SHIT
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool GetCursorPos(out POINT lpPoint);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);

    const int WM_LBUTTONDOWN = 0x0201;
    const int WM_LBUTTONUP = 0x0202;
    const int WM_RBUTTONDOWN = 0x0204;
    const int WM_RBUTTONUP = 0x0205;

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
    }
}