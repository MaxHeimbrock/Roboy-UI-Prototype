using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerSenseGlove : Pointer
{
    private SenseGlove_Teleport teleport;
    public override void GetPointerPosition()
    {
        throw new System.NotImplementedException();
    }

    public override void SubclassStart()
    {
        Object[] objects = Resources.FindObjectsOfTypeAll(typeof(SenseGlove_Teleport));
        if (objects.Length > 0)
        {
            teleport = (SenseGlove_Teleport) objects[0];
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        PushPointerPosition(teleport.pointerOriginZ.position, teleport.pointerOriginZ.forward);
    }
}
