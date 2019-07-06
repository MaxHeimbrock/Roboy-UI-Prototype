using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_Element : MonoBehaviour
{
    protected bool active = true;
    public UI_Element[] children;
    // is overwritten by parent at Start(), if element is child
    protected bool isChild = false;
    // Every UI_Element has a parent menuManager, except for the top menu
    protected MenuManager menuManager;
    Animator animator;

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
        Debug.Log("Activated");
        animator.SetBool("Active", true);
        active = true;
    }

    public void Deactivate()
    {
        Debug.Log("Deactivated");
        animator.SetBool("Active", false);
        active = false;
    }

    public void SetMenuManager(MenuManager menuManager)
    {
        this.menuManager = menuManager;
    }

    public void SetIsChild()
    {
        isChild = true;
        try
        {
            animator = this.GetComponent<Animator>();
        }
        catch (Exception e)
        {
            Debug.LogError("Children must have animator");
        }        
        Deactivate();
    }

    public bool GetIsChild()
    {
        return isChild;
    }

    public bool GetActive()
    {
        return active;
    }

    protected abstract void SubclassStart();

    protected abstract void SubclassUpdate();

    public abstract void Highlight();
}
