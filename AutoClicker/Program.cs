using AutoClicker;

using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

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
    d

    private static void OnKeypress(KeyEvent @event)
    {
        if (@event.VKey != VKeyCodes.KeyHeld)
            return;

        if (@event.Key == 1) //  && map.GetDown((uint)Keys.LShiftKey)
        {
            HitPlayer();
        }
        if (@event.Key == 2) //  && map.GetDown((uint)Keys.LShiftKey)
        {
            PlaceBlock();
        }
    }

    public static void HitPlayer()
    {
        IntPtr Hwnd = GetForegroundWindow();

        PostMessage(Hwnd, WM_LBUTTONDOWN, 0, 0);
        //Thread.Sleep(15); // minimal sleep
        PostMessage(Hwnd, WM_LBUTTONUP, 0, 0);
    }

    public static void PlaceBlock()
    {
        IntPtr Hwnd = GetForegroundWindow();

        PostMessage(Hwnd, WM_RBUTTONDOWN, 0, 0);
        //Thread.Sleep(15); // minimal sleep
        PostMessage(Hwnd, WM_RBUTTONUP, 0, 0);
    }

    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

    const int WM_LBUTTONDOWN = 0x0201;
    const int WM_LBUTTONUP = 0x0202;
    const int WM_RBUTTONDOWN = 0x0204;
    const int WM_RBUTTONUP = 0x0205;
}