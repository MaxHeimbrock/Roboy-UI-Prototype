using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    Camera cam;
    int width;
    int height;

    void Start()
    {
        cam = Camera.main;        
    }

    // Gets pointer position in 
    public void GetRaycastHit(Vector2 indicatorPos)
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(indicatorPos.x, indicatorPos.y, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            print("I'm looking at " + hit.transform.name);
            //hit.collider.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
            print("I'm looking at nothing!");
    }

    void Update()
    {
        
    }
}
