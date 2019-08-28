using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeCanvasPos : MonoBehaviour
{
    bool freeze = false;

    RectTransform pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            freeze = true;
        

    }
}
