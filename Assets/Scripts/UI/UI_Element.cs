using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_Element : MonoBehaviour
{
    public UI_Element[] children;
    // is overwritten by parent at Start(), if element is child
    protected bool isChild = false;
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
    }

    public void Deactivate()
    {
        Debug.Log("Deactivated");
        animator.SetBool("Active", false);
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

    protected abstract void SubclassStart();

    protected abstract void SubclassUpdate();

    public abstract void Highlight();
}
