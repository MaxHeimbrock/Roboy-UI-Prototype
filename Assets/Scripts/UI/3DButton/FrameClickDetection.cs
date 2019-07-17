using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FrameClickDetection : MonoBehaviour
{
    public UnityEvent[] onPress;
    public UnityEvent[] onUnpress;
    private int onPressIndex;
    private int onUnpressIndex;
    private bool wait;
    private Collider pressurePlateCollider;
    private Transform pressurePlateTransform;
    private MeshRenderer meshRenderer;
    private Color defaultColor;

    /**
     * Initialize variables
     */
    private void Start()
    {
        wait = false;
        onPressIndex = 0;
        onUnpressIndex = 0;
        pressurePlateTransform = transform.parent.GetChild(0);
        pressurePlateCollider = pressurePlateTransform.GetComponent<Collider>();
        defaultColor = transform.GetChild(0).GetComponent<MeshRenderer>().material.GetColor("_Color");
    }

    /**
     * Checks if the PressurePlate goes through the frame.
     * If "wait" is set to true, then the button is already pressed.
     * Otherwise the button gets activated.
     */
    private void OnTriggerEnter(Collider other)
    {
        if (pressurePlateCollider.Equals(other))
        {
            if (!wait)
            { 
                press();
                highlightOn();
            }
        }
    }

    /**
     * When the PressurePlate exits the frame collider, its direction is checked.
     * It is either pushed further in, the button remains pressed
     * or it is on its way back to its default position, so the button gets released
     */
    private void OnTriggerExit(Collider other)
    {
        if (pressurePlateCollider.Equals(other))
        {
            if (pressurePlateTransform.position.z < transform.position.z)
            {
                wait = false;
                unpress();
                highlightOff();
            }
            else
            {
                wait = true;
            }
        }
    }

    /**
     * Implement all functionality when the button is pressed here.
     */
    void press()
    {
        if(onPress.Length > 0)
        {
            onPress[onPressIndex].Invoke();
            onPressIndex++;
            if (onPressIndex.Equals(onPress.Length))
            {
                onPressIndex = 0;
            }
        }
    }

    /**
     * Implement all functionality when the button is unpressed here.
     */
     void unpress()
    {
        if (onUnpress.Length > 0)
        {
            onUnpress[onUnpressIndex].Invoke();
            onUnpressIndex++;
            if (onUnpressIndex.Equals(onUnpress.Length))
            {
                onUnpressIndex = 0;
            }
        }
    }

    /**
     * Highlights the Button when pressed as feedback for the user.
     * Default: The frame changes its color to light blue.
     */
    void highlightOn()
    {
        foreach(MeshRenderer childMeshRenderer in transform.GetComponentsInChildren<MeshRenderer>())
        {
            Color lightBlue = new Color();
            lightBlue.a = 1.0f;
            lightBlue.b = 1.0f;
            lightBlue.g = 0.968f;
            lightBlue.r = 0.0f;
            childMeshRenderer.material.SetColor("_Color", lightBlue);
        }
    }

    /**
     * Turn off highlight for the button when it is released.
     */
    void highlightOff()
    {
        foreach(MeshRenderer childMeshRenderer in transform.GetComponentsInChildren<MeshRenderer>())
        {
            childMeshRenderer.material.SetColor("_Color", defaultColor);
        }
    }
}
