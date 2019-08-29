using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PointerViveController : Pointer
{
    GameObject controller;

    // Finds the mouse position in 2D screen coordinates
    public override void GetPointerPosition()
    {
        // Push the transform of the Controller
        PushPointerPosition(controller.transform.position, controller.transform.rotation.eulerAngles);        
    }

    public override void SubclassStart()
    {
        // Finds the game object at the start. In my case, the right controller has to be tagged as ViveController 
        // in the Unity Inspector
        controller = GameObject.FindGameObjectWithTag("ViveController");
        Debug.Log("Controller found");
    }

    public void Update()
    {
        GetPointerPosition();
    }
}
