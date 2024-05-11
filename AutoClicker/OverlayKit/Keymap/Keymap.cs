using AutoClicker;
using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;

class Keymap
{
    private ConcurrentDictionary<uint, bool> prevBuff = new ConcurrentDictionary<uint, bool>();

    public event Action<KeyEvent> OnKeyPress;

    [DllImport("User32.dll")]
    private static extern bool GetAsyncKeyState(uint key);

    public Keymap()
    {
        for (uint i = 0; i < 0xFF; i++)
        {
            prevBuff.TryAdd(i, value: false);
        }
    }

    public bool GetDown(uint key)
    {
        bool value = false;
        prevBuff.TryGetValue(key, out value);
        return value;
    }

    public void Tick()
    {
        if (this.OnKeyPress == null)
        {
            return;
        }

        for (uint i = 0; i < 0xFF; i++)
        {
            bool asyncKeyState = GetAsyncKeyState(i);
            if (asyncKeyState && !prevBuff[i])
            {
                this.OnKeyPress(new KeyEvent(i, VKeyCodes.KeyDown));
            }
            else if (asyncKeyState && prevBuff[i])
            {
                this.OnKeyPress(new KeyEvent(i, VKeyCodes.KeyHeld));
            }
            else if (!asyncKeyState && prevBuff[i])
            {
                this.OnKeyPress(new KeyEvent(i, VKeyCodes.KeyUp));
            }

            prevBuff[i] = asyncKeyState;
        }
    }
}

namespace AutoClicker
{
    public enum VKeyCodes
    {
        KeyUp,
        KeyHeld,
        KeyDown
    }
}