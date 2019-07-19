using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SubMenuAnimationHandler : MonoBehaviour
{
    public bool IsNested;

    List<KeyValuePair<string, UnityAction[]>> interactionObjectTypes;
    List<GameObject> foundObjects;

    // Start is called before the first frame update
    void Start()
    {
        foundObjects = new List<GameObject>();
        interactionObjectTypes = new List<KeyValuePair<string, UnityAction[]>>();
        interactionObjectTypes.Add(new KeyValuePair<string, UnityAction[]>("Button3D", new UnityAction[] { safemodeOnButton3D, safemodeOffButton3D }));
        interactionObjectTypes.Add(new KeyValuePair<string, UnityAction[]>("Slider3D", new UnityAction[] { safemodeOnSlider3D, safemodeOffSlider3D }));
    }

    // Update is called once per frame
    void Update()
    {
        foreach(KeyValuePair<string, UnityAction[]> pair in interactionObjectTypes)
        {
            if (!IsNested)
            {
                for(int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).CompareTag(pair.Key))
                    {
                        foundObjects.Add(transform.GetChild(i).gameObject);
                    }
                }
            }
            else
            {
                findObjectsWithTagInAllChildren(pair.Key, transform);
            }
            pair.Value[0].Invoke();
            foundObjects = new List<GameObject>();
        }
    }

    void findObjectsWithTagInAllChildren(string tag, Transform parent)
    {
        Transform currentChild;
        for (int i = 0; i < transform.childCount; i++)
        {
            currentChild = transform.GetChild(i);
            if (currentChild.CompareTag(tag))
            {
                foundObjects.Add(currentChild.gameObject);
            }
            findObjectsWithTagInAllChildren(tag, currentChild);
        }
    }

    private void safemodeOnButton3D()
    {
        foreach(GameObject obj in foundObjects)
        {
            obj.transform.GetChild(1).GetComponent<FrameClickDetection>().enabled = false;
            Debug.Log("Turned off FrameClickDetection at: " + obj.transform.GetChild(1).name);
        }
    }
    private void safemodeOffButton3D()
    {
        foreach (GameObject obj in foundObjects)
        {
            obj.transform.GetChild(1).GetComponent<FrameClickDetection>().enabled = true;
        }
    }

    private void safemodeOnSlider3D()
    {
        foreach (GameObject obj in foundObjects)
        {
            obj.transform.GetChild(0).GetComponent<CustomSlider>().enabled = false;
            Debug.Log("Turned off CustomSlider at: " + obj.transform.GetChild(0).name);
        }
    }
    private void safemodeOffSlider3D()
    {
        foreach (GameObject obj in foundObjects)
        {
            obj.transform.GetChild(0).GetComponent<CustomSlider>().enabled = true;
        }
    }
}
