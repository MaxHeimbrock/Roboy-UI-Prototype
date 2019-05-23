using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerMouse : Pointer
{
    // Finds the mouse position in 2D screen coordinates
    public override Vector2 GetPointerPosition()
    {
        return Input.mousePosition;
    }

    public void Update()
    {
        PushPointerPosition(GetPointerPosition());
    }
}
