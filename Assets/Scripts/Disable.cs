using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject instance;

    private void OnDisable()
    {
        Debug.Log("Disabled");
        componentDisEnable(this.gameObject, false);
        recursiveDisEnable(this.gameObject, false);
    }

    private void OnEnable()
    {
        Debug.Log("Enabled");
        componentDisEnable(this.gameObject, true);
        recursiveDisEnable(this.gameObject, true);

        instance = Instantiate(buttonPrefab, this.transform.position + new Vector3(3f,0,0), this.transform.rotation);
    }

    private void recursiveDisEnable(GameObject parent, bool enable)
    {
        Debug.Log("Childcount: " + parent.transform.childCount);
        for(int i = 0; i < parent.transform.childCount; i++)
        {
            recursiveDisEnable(parent.transform.GetChild(i).gameObject, enable);
        }
        parent.SetActive(enable);
    }

    private void componentDisEnable(GameObject parent, bool enable)
    {
        foreach(Collider coll in parent.GetComponentsInChildren<Collider>())
        {
            coll.enabled = enable;
        }

        foreach(FrameClickDetection fcd in parent.GetComponentsInChildren<FrameClickDetection>())
        {
            fcd.enabled = enable;
        }
    }
}
