using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_Element : MonoBehaviour
{
    public UI_Element[] children;
    // is overwritten by parent at Start(), if element is child
    protected bool isChild = false;
    protected MenuManager menuManager;

    // Start is called before the first frame update
    void Start()
    {
        SubclassStart();
    }

    // Update is called once per frame
    void Update()
    {
        SubclassUpdate();   
    }

    public void Activate()
    {

    }

    public void Deactivate()
    {        
        this.gameObject.SetActive(false);
    }

    public void SetMenuManager(MenuManager menuManager)
    {
        this.menuManager = menuManager;
    }

    public void SetIsChild()
    {
        isChild = true;        
        Deactivate();
    }

    public bool GetIsChild()
    {
        return isChild;
    }

    protected abstract void SubclassStart();

    protected abstract void SubclassUpdate();

    public abstract void Highlight();
}
