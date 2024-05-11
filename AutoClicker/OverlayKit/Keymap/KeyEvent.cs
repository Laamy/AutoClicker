using AutoClicker;

public class KeyEvent
{
    public uint Key;
    public VKeyCodes VKey;

    public KeyEvent(uint num, VKeyCodes keyDown)
    {
        this.Key = num;
        this.VKey = keyDown;
    }
}