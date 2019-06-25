using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerMouse : Pointer
{
    Camera cam;
    float scaling = 100.0f;

    // Finds the mouse position in 2D screen coordinates
    public override void GetPointerPosition()
    {
        //Debug.Log(Input.mousePosition.x);
        PushPointerPosition(cam.transform.position + new Vector3(0.1f, 0.1f, 0.1f), 
            cam.transform.rotation.eulerAngles + new Vector3((-Input.mousePosition.y + (Screen.width/2)) * scaling / Screen.width, (Input.mousePosition.x - (Screen.width/2)) * scaling / Screen.width, 0));
    }

    public override void SubclassStart()
    {
        cam = Camera.main;
    }

    public void Update()
    {
        GetPointerPosition();        
    }
}
