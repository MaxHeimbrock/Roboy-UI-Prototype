using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/// <summary>
/// A pointer used with the Vive Controller, where a ray is coming from the controllers position and orientation.
/// </summary>
public class PointerViveController : Pointer
{
    GameObject controller;

    // Finds the mouse position in 2D screen coordinates
    /// <summary>
    /// Sends the controllers position and orientation to the UI Manager for pointing.
    /// </summary>
    public override void GetPointerPosition()
    {
        // Push the transform of the Controller
        PushPointerPosition(controller.transform.position, controller.transform.rotation.eulerAngles);        
    }

    /// <summary>
    /// Finds Vive Controller
    /// </summary>
    public override void SubclassStart()
    {
        // Finds the game object at the start. In my case, the right controller has to be tagged as ViveController 
        // in the Unity Inspector
        controller = GameObject.FindGameObjectWithTag("ViveController");
        Debug.Log("Controller found");
    }

    /// <summary>
    /// Gets pointer position and sends it every frame.
    /// </summary>
    public void Update()
    {
        GetPointerPosition();
    }
}
