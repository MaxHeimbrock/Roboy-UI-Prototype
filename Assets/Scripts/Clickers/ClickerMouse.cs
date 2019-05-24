using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerMouse : Clicker
{
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            PushClick(0);
    }

    protected override void SubclassStart()
    {
        // empty
    }
}
