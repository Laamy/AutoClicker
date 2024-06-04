using AutoClicker;

using System.Threading;
using System.Windows.Forms;

class Program
{
    static Keymap map;

    static int CPS = 18;

    static void Main(string[] args)
    {
        map = new Keymap();

        map.OnKeyPress += OnKeypress;

        while (true)
        {
            Thread.Sleep(1000 / CPS);

            map.Tick();
        }
    }

    private static void OnKeypress(KeyEvent @event)
    {
        if (@event.VKey != VKeyCodes.KeyHeld)
            return;

        if (@event.Key == 1 && map.GetDown((uint)Keys.LShiftKey)) //  && map.GetDown((uint)Keys.LShiftKey)
        {
            Game.LeftClick();
        }

        if (@event.Key == 2 && map.GetDown((uint)Keys.LShiftKey)) //  && map.GetDown((uint)Keys.LShiftKey)
        {
            Game.RightClick();
        }
    }
}