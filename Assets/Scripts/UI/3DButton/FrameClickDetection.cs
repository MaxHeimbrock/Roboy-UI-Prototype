using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameClickDetection : MonoBehaviour
{
    public int clickcount;
    public bool wait;
    private Collider pressurePlateCollider;
    private Transform pressurePlateTransform;
    private MeshRenderer meshRenderer;
    public Color defaultColor;

    private void Start()
    {
        clickcount = 0;
        wait = false;
        pressurePlateTransform = transform.parent.GetChild(0);
        pressurePlateCollider = pressurePlateTransform.GetComponent<Collider>();
        defaultColor = transform.GetChild(0).GetComponent<MeshRenderer>().material.GetColor("_Color");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (pressurePlateCollider.Equals(other))
        {
            if (!wait)
            { 
                Click();
                highlightOn();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (pressurePlateCollider.Equals(other))
        {
            if (pressurePlateTransform.position.z < transform.position.z)
            {
                wait = false;
                highlightOff();
            }
            else
            {
                wait = true;
            }
        }
    }

    void Click()
    {
        clickcount++;
    }

    void highlightOn()
    {
        foreach(MeshRenderer childMeshRenderer in transform.GetComponentsInChildren<MeshRenderer>())
        {
            childMeshRenderer.material.SetColor("_Color", Color.red);
        }
    }

    void highlightOff()
    {
        foreach(MeshRenderer childMeshRenderer in transform.GetComponentsInChildren<MeshRenderer>())
        {
            childMeshRenderer.material.SetColor("_Color", defaultColor);
        }
    }
}
