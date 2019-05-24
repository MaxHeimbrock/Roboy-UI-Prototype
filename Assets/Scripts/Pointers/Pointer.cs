using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pointer : MonoBehaviour
{
    private UI_Manager UI_Manager;

    public void Start()
    {
        UI_Manager = GetComponent<UI_Manager>();
    }

    // Returns the pointers position in 2D screen coordinates (pixels)
    public abstract Vector2 GetPointerPosition();

    // Pushes pointer position to UI_Manager every Update() call
    public void PushPointerPosition(Vector2 pos)
    {
        UI_Manager.Point(pos);
    }
}
