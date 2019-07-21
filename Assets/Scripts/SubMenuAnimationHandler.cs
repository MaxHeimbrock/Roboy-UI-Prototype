using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Leap.Unity;
using Leap.Unity.Interaction;

public class SubMenuAnimationHandler : MonoBehaviour
{
    struct InteractionPrefab
    {
        public string TagName;
        public UnityAction SafeModeOnAction;
        public UnityAction SafeModeOffAction;
        public List<GameObject> FoundObjects;

        public InteractionPrefab(string tag, UnityAction on, UnityAction off) {
            TagName = tag;
            SafeModeOnAction = on;
            SafeModeOffAction = off;
            FoundObjects = new List<GameObject>();
        }
    }

    public bool IsNested = false;
    private int currentState;
    private bool newRequest;
    private bool fadeIn;

    List<InteractionPrefab> interactionPrefabs;

    /*
    List<KeyValuePair<string, UnityAction[]>> interactionObjectTypes;
    List<List<GameObject>> foundObjects;*/

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentState = -1;
        newRequest = false;
        fadeIn = true;
        animator = transform.parent.GetComponent<Animator>();
        interactionPrefabs = new List<InteractionPrefab>();
        /*interactionObjectTypes.Add(new KeyValuePair<string, UnityAction[]>("Button3D", new UnityAction[] { safemodeOnButton3D, safemodeOffButton3D }));
        interactionObjectTypes.Add(new KeyValuePair<string, UnityAction[]>("Slider3D", new UnityAction[] { safemodeOnSlider3D, safemodeOffSlider3D }));*/

        interactionPrefabs.Add(new InteractionPrefab("Button3D", safemodeOnButton3D, safemodeOffButton3D));
        interactionPrefabs.Add(new InteractionPrefab("Slider3D", safemodeOnSlider3D, safemodeOffSlider3D));

        //foundObjects = new List<List<GameObject>>();
        if (!IsNested)
        {
            foreach (InteractionPrefab prefab in interactionPrefabs)
            {
                List<GameObject> currentList = new List<GameObject>();
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        if (transform.GetChild(i).CompareTag(prefab.TagName))
                        {
                            currentList.Add(transform.GetChild(i).gameObject);
                        }
                    }
                prefab.FoundObjects.AddRange(currentList);
            }
        }
        else
        {
            foreach (InteractionPrefab prefab in interactionPrefabs)
            {
                    prefab.FoundObjects.AddRange(findObjectsWithTagInAllChildren(prefab.TagName, transform));
            }
        }

        safeModeOn();
        animator.SetTrigger("Go");
    }

    private void Update()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        if (currentState != state.fullPathHash)
        {
            currentState = state.fullPathHash;
            if (state.IsName("Visible"))
            {
                safeModeOff();
                animator.SetBool("FadeIn", false);
            } else if(state.IsName("Invisible")) {
                deActivate(false);
                animator.SetBool("FadeOut", false);
            }
        }

        if (newRequest && (state.IsName("Visible") || state.IsName("Invisible")))
        {
            newRequest = false;
            if (fadeIn && state.IsName("Invisible"))
            {
                deActivate(true);
                animator.SetBool("FadeIn", true);
            }
            else if(!fadeIn && state.IsName("Visible")) {
                safeModeOn();
                animator.SetBool("FadeOut", true);
            } 
        }
    }

    List<GameObject> findObjectsWithTagInAllChildren(string tag, Transform parent)
    {
        List<GameObject> list = new List<GameObject>();
        Transform currentChild;
        for (int i = 0; i < transform.childCount; i++)
        {
            currentChild = transform.GetChild(i);
            if (currentChild.CompareTag(tag))
            {
                list.Add(currentChild.gameObject);
            }
            list.AddRange(findObjectsWithTagInAllChildren(tag, currentChild));
        }
        return list;
    }

    private void deActivate(bool activate)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(activate);
        }

        int myIndex = transform.GetSiblingIndex();
        Transform parent = transform.parent;
        for (int i = 0; i < parent.childCount; i++)
        {
            if (i != myIndex)
            {
                parent.GetChild(i).gameObject.SetActive(activate);
            }
        }
    }

    public void FadeIn()
    {
        newRequest = true;
        fadeIn = true;
    }
    public void FadeOut()
    {
        newRequest = true;
        fadeIn = false;
    }

    private void safeModeOn()
    {
        foreach (InteractionPrefab prefab in interactionPrefabs)
        {
            prefab.SafeModeOnAction.Invoke();
        }
    }
    private void safeModeOff()
    {
        foreach (InteractionPrefab prefab in interactionPrefabs)
        {
            prefab.SafeModeOffAction.Invoke();
        }
    }

    private void safemodeOnButton3D()
    {
        List<GameObject> allButtons = interactionPrefabs.Find(x => x.TagName.Equals("Button3D")).FoundObjects;
        foreach (GameObject obj in allButtons)
        {
            obj.transform.GetChild(1).GetComponent<Collider>().enabled = false;
            obj.transform.GetChild(1).GetComponent<FrameClickDetection>().enabled = false;
        }
    }
    private void safemodeOffButton3D()
    {
        List<GameObject> allButtons = interactionPrefabs.Find(x => x.TagName.Equals("Button3D")).FoundObjects;
        foreach (GameObject obj in allButtons)
        {
            obj.transform.GetChild(1).GetComponent<Collider>().enabled = true;
            obj.transform.GetChild(1).GetComponent<FrameClickDetection>().enabled = true;
        }
    }

    private void safemodeOnSlider3D()
    {
        List<GameObject> allSliders = interactionPrefabs.Find(x => x.TagName.Equals("Slider3D")).FoundObjects;
        foreach (GameObject obj in allSliders)
        {
             obj.transform.GetChild(0).GetComponent<Collider>().enabled = false;
             obj.transform.GetChild(0).GetComponent<CustomSlider>().enabled = false;
             Debug.Log("Turned off CustomSlider at: " + obj.transform.GetChild(0).name);

            
            foreach(GameObject hand in GameObject.FindGameObjectsWithTag("HandModel"))
            {
                foreach(FingerDirectionDetector script in hand.GetComponentsInChildren<FingerDirectionDetector>(true))
                {
                    if (script.TargetObject.Equals(obj.transform)){
                        script.enabled = false;
                    }
                }
            }
        }
    }
    private void safemodeOffSlider3D()
    {
        List<GameObject> allSliders = interactionPrefabs.Find(x => x.TagName.Equals("Slider3D")).FoundObjects;
        foreach (GameObject obj in allSliders)
        {
            Transform full = obj.transform.GetChild(0);
            CustomSlider customslider = full.GetComponent<CustomSlider>();
            customslider.ReturnToDefaultPos();
            full.GetComponent<Collider>().enabled = true;
            customslider.enabled = true;
            Debug.Log("Turned off CustomSlider at: " + obj.transform.GetChild(0).name);

            foreach (GameObject hand in GameObject.FindGameObjectsWithTag("HandModel"))
            {
                foreach (FingerDirectionDetector script in hand.GetComponentsInChildren<FingerDirectionDetector>(true))
                {
                    if (script.TargetObject.Equals(obj.transform))
                    {
                        script.enabled = true;
                    }
                }
            }
        }
    }
}



/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Leap.Unity;
using Leap.Unity.Interaction;

public class SubMenuAnimationHandler : MonoBehaviour
{
    struct InteractionPrefab
    {
        public string TagName;
        public UnityAction SafeModeOnAction;
        public UnityAction SafeModeOffAction;
        public List<GameObject> FoundObjects;

        public InteractionPrefab(string tag, UnityAction on, UnityAction off)
        {
            TagName = tag;
            SafeModeOnAction = on;
            SafeModeOffAction = off;
            FoundObjects = new List<GameObject>();
        }
    }

    public bool IsNested = false;
    private string currentState;

    List<InteractionPrefab> interactionPrefabs;

    /*
    List<KeyValuePair<string, UnityAction[]>> interactionObjectTypes;
    List<List<GameObject>> foundObjects;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentState = "Visible 0";
        animator = transform.parent.GetComponent<Animator>();
        interactionPrefabs = new List<InteractionPrefab>();
        /*interactionObjectTypes.Add(new KeyValuePair<string, UnityAction[]>("Button3D", new UnityAction[] { safemodeOnButton3D, safemodeOffButton3D }));
        interactionObjectTypes.Add(new KeyValuePair<string, UnityAction[]>("Slider3D", new UnityAction[] { safemodeOnSlider3D, safemodeOffSlider3D }));

        interactionPrefabs.Add(new InteractionPrefab("Button3D", safemodeOnButton3D, safemodeOffButton3D));
        interactionPrefabs.Add(new InteractionPrefab("Slider3D", safemodeOnSlider3D, safemodeOffSlider3D));

        //foundObjects = new List<List<GameObject>>();
        if (!IsNested)
        {
            foreach (InteractionPrefab in interactionObjectTypes)
            {
                List<GameObject> currentList = new List<GameObject>();
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).CompareTag(pair.Key))
                    {
                        currentList.Add(transform.GetChild(i).gameObject);
                    }
                }
                foundObjects.Add(currentList);
            }
        }
        else
        {
            foreach (KeyValuePair<string, UnityAction[]> pair in interactionObjectTypes)
            {
                foundObjects.Add(findObjectsWithTagInAllChildren(pair.Key, transform));
            }
        }

        safeModeOn();
        animator.SetTrigger("Go");
    }

    private void Update()
    {
        string newState = animator.GetCurrentAnimatorStateInfo(0).nameHash;
        if (currentState.IsName())
    }

    List<GameObject> findObjectsWithTagInAllChildren(string tag, Transform parent)
    {
        List<GameObject> list = new List<GameObject>();
        Transform currentChild;
        for (int i = 0; i < transform.childCount; i++)
        {
            currentChild = transform.GetChild(i);
            if (currentChild.CompareTag(tag))
            {
                list.Add(currentChild.gameObject);
            }
            list.AddRange(findObjectsWithTagInAllChildren(tag, currentChild));
        }
        return list;
    }

    public void FadeIn()
    {
        foreach (KeyValuePair<string, UnityAction[]> type in interactionObjectTypes)
        {
            type.Value[0].Invoke();
        }

        animator.SetTrigger(0);
    }

    public void safeModeOn()
    {
        foreach (KeyValuePair<string, UnityAction[]> type in interactionObjectTypes)
        {
            type.Value[0].Invoke();
        }
    }

    public void safeModeOff()
    {
        foreach (KeyValuePair<string, UnityAction[]> type in interactionObjectTypes)
        {
            type.Value[1].Invoke();
        }
    }

    private void safemodeOnButton3D()
    {
        List<GameObject>[] shittyArray = foundObjects.ToArray();
        foreach (GameObject obj in shittyArray[0])
        {
            obj.transform.GetChild(1).GetComponent<Collider>().enabled = false;
            obj.transform.GetChild(1).GetComponent<FrameClickDetection>().enabled = false;
            Debug.Log("Turned off FrameClickDetection at: " + obj.transform.GetChild(1).name);
        }
    }
    private void safemodeOffButton3D()
    {

        foreach (List<GameObject> type in foundObjects)
        {
            foreach (GameObject obj in type)
            {
                obj.transform.GetChild(1).GetComponent<FrameClickDetection>().enabled = true;
            }
        }
    }

    private void safemodeOnSlider3D()
    {
        List<GameObject>[] shittyArray = foundObjects.ToArray();

        foreach (GameObject obj in shittyArray[1])
        {
            obj.transform.GetChild(0).GetComponent<Collider>().enabled = false;
            obj.transform.GetChild(0).GetComponent<CustomSlider>().enabled = false;
            Debug.Log("Turned off CustomSlider at: " + obj.transform.GetChild(0).name);

            foreach (GameObject hand in GameObject.FindGameObjectsWithTag("HandModel"))
            {
                foreach (FingerDirectionDetector script in hand.GetComponentsInChildren<FingerDirectionDetector>(true))
                {
                    if (script.TargetObject.Equals(obj.transform))
                    {
                        script.enabled = false;
                    }
                }
            }
        }

    }
    private void safemodeOffSlider3D()
    {
        foreach (List<GameObject> type in foundObjects)
        {
            foreach (GameObject obj in type)
            {
                obj.transform.GetChild(0).GetComponent<CustomSlider>().enabled = true;
            }
        }
    }
}*/
