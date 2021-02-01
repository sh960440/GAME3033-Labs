using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppEvents
{
    public delegate void MouseCursorEnalbe(bool enable);
    
    public static event MouseCursorEnalbe mouseCursorEnalbe;

    public static void Invoke_MouseCursorEnable(bool enabled)
    {
        mouseCursorEnalbe?.Invoke(enabled);
    }
}