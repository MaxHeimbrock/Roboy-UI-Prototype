using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboyPositioning : MonoBehaviour
{
    public bool followCamera = true;

    private Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (followCamera)
            position = Camera.main.transform.position;

        this.transform.position = position + new Vector3(0,0,-1);
    }
}
