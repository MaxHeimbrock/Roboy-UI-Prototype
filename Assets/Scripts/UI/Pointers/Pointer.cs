using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pointer : MonoBehaviour
{
    private UI_Manager UI_Manager;

    public void Start()
    {
        UI_Manager = GetComponent<UI_Manager>();
        SubclassStart();
    }

    public abstract void SubclassStart();

    // Returns the pointers position in 2D screen coordinates (pixels)
    public abstract void GetPointerPosition();

    // Pushes pointer position to UI_Manager every Update() call
    public void PushPointerPosition(Vector3 position, Vector3 rotation)
    {
        UI_Manager.Point(position, rotation);
    }
}
