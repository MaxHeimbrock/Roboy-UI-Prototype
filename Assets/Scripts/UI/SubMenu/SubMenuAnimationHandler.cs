using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SubMenuAnimationHandler : MonoBehaviour
{
    /// <summary>
    /// Representing a interaction element category.
    /// Each instance of this interaction element needs to have the same tag.
    /// For this interaction element has to be a SafeModeOn and SafeModeOff method implemented in this class.
    /// This InteractionPrefab holds a reference to every instance of this interaction element in the scene.
    /// </summary>
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

    #region properties

    public bool MButtonTransition;
    public bool IsNested = false;
    private int currentState;
    private bool newRequest;
    private bool fadeIn;
    private Animator animator;

    List<InteractionPrefab> interactionPrefabs;

    #endregion

    /// <summary>
    /// Initializes the variables.
    /// Finds all instances of the InteractionPrefabs in the scene.
    /// Activates safemode and starts SubMenu Animation.
    /// 
    /// Important: Add your new InteractionPrefab to the interactionPrefabs list in this method.
    /// </summary>
    void Start()
    {
        currentState = -1;
        newRequest = false;
        fadeIn = true;
        animator = transform.parent.GetComponent<Animator>();
        interactionPrefabs = new List<InteractionPrefab>();

        interactionPrefabs.Add(new InteractionPrefab("Button3D", safemodeOnButton3D, safemodeOffButton3D));
        interactionPrefabs.Add(new InteractionPrefab("Slider3D", safemodeOnSlider3D, safemodeOffSlider3D));
        
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

    /// <summary>
    /// If the SubMenu Animation State has changed, de/activate safemode accordingly
    /// If a new request has been scheduled, initiate Animation transition if necessary.
    /// If MButtonTransition has been enabled in the inspector, you can let the menu fade in/out manually by pressing the keyboard button 'M'
    /// </summary>
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

        if(MButtonTransition && Input.GetKeyDown(KeyCode.M))
        {
            if (fadeIn)
            {
                FadeOut();
            }
            else
            {
                FadeIn();
            }
        }
    }

    /// <summary>
    /// Recursively find all GameObjects with the given tag among the parent's children.
    /// </summary>
    /// <param name="tag">The tag name to look for.</param>
    /// <param name="parent">The parent whose children are screened.</param>
    /// <returns></returns>
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

    /// <summary>
    /// Activate or deactivate all instances for every InteractionPrefab in interactionPrefabs.
    /// </summary>
    /// <param name="activate">Specifying whether to activate or deactivate</param>
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

    #region de/activating safe mode

    /// <summary>
    /// Activate safe mode for every InteractionPrefab in interactionPrefabs.
    /// </summary>
    private void safeModeOn()
    {
        foreach (InteractionPrefab prefab in interactionPrefabs)
        {
            prefab.SafeModeOnAction.Invoke();
        }
    }
    /// <summary>
    /// Deactivate safe mode for every InteractionPrefab in interactionPrefabs.
    /// </summary>
    private void safeModeOff()
    {
        foreach (InteractionPrefab prefab in interactionPrefabs)
        {
            prefab.SafeModeOffAction.Invoke();
        }
    }

    #endregion

    #region fade

    /// <summary>
    /// An extern class can request the fade in of the menu.
    /// </summary>
    public void FadeIn()
    {
        newRequest = true;
        fadeIn = true;
    }
    /// <summary>
    /// An extern class can request the fade out of the menu.
    /// </summary>
    public void FadeOut()
    {
        newRequest = true;
        fadeIn = false;
    }

    #endregion

    #region safe mode methods for each interactionPrefab

    /// <summary>
    /// Specific commands how to turn the 3D button into a safe mode.
    /// </summary>
    private void safemodeOnButton3D()
    {
        List<GameObject> allButtons = interactionPrefabs.Find(x => x.TagName.Equals("Button3D")).FoundObjects;
        foreach (GameObject obj in allButtons)
        {
            Transform frame = obj.transform.GetChild(1);
            frame.GetComponent<FrameClickDetection>().highlightOff();
            frame.GetComponent<Collider>().enabled = false;
            frame.GetComponent<FrameClickDetection>().enabled = false;

            Transform activeArea = obj.transform.GetChild(2);
            activeArea.GetComponent<Collider>().enabled = false;
        }
    }
    /// <summary>
    /// Specific commands how to return the 3D button from safe mode to an active state.
    /// </summary>
    private void safemodeOffButton3D()
    {
        List<GameObject> allButtons = interactionPrefabs.Find(x => x.TagName.Equals("Button3D")).FoundObjects;
        foreach (GameObject obj in allButtons)
        {
            Transform frame = obj.transform.GetChild(1);
            frame.GetComponent<Collider>().enabled = true;
            frame.GetComponent<FrameClickDetection>().enabled = true;
        }
    }

    /// <summary>
    /// Specific commands how to turn the 3D button into a safe mode.
    /// </summary>
    private void safemodeOnSlider3D()
    {
        List<GameObject> allSliders = interactionPrefabs.Find(x => x.TagName.Equals("Slider3D")).FoundObjects;
        foreach (GameObject obj in allSliders)
        {
            obj.transform.GetChild(0).GetComponent<Collider>().enabled = false;
            obj.transform.GetChild(0).GetComponent<CustomSlider>().enabled = false;
        }
    }
    /// <summary>
    /// Specific commands how to return the 3D slider from safe mode to an active state.
    /// </summary>
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
        }
    }

    #endregion
}