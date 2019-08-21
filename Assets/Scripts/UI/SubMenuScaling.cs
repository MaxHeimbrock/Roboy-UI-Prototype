using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SubMenuScaling : MonoBehaviour
{
    public float width;
    public float height;
    private float oldWidth;
    private float oldHeight;

    private Transform adjustableSides;

    private void Reset()
    {
        width = 1;
        height = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.runInEditMode = true;

        adjustableSides = this.transform.GetChild(2);

        oldWidth = width;
        oldHeight = height;
    }

    // Update is called once per frame
    void Update()
    {
        adjustableSides = this.transform.GetChild(2);

        Debug.Log("Update Call");
        if(oldWidth != width)
        {
            adjustableSides.localScale = new Vector3(width / (oldWidth + Mathf.Pow(10, -20)) * adjustableSides.localScale.x, adjustableSides.localScale.y, adjustableSides.localScale.z);
            oldWidth = width;
            Debug.Log("Change Width");
        }
        if(oldHeight != height)
        {
            adjustableSides.localScale = new Vector3(adjustableSides.localScale.x, height / (oldHeight + Mathf.Pow(10, -20)) * adjustableSides.localScale.y, adjustableSides.localScale.z);
            oldHeight = height;
            Debug.Log("Change Height");
        }
    }
}
