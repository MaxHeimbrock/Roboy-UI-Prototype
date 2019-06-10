using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerMouse : Pointer
{
    // Finds the mouse position in 2D screen coordinates
    public override void GetPointerPosition()
    {
        PushPointerPosition(new Vector3(Input.mousePosition.x/100, Input.mousePosition.y/100, 1), Vector3.forward);
    }

    public override void SubclassStart()
    {
        
    }

    public void Update()
    {
        GetPointerPosition();        
    }
}
