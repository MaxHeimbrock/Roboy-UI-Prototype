using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : UI_Element
{
    UI_Element[] attatched_ui_elements;    
    
    private float startTimer;
    private float timer;
    private float currentTime = 0.0f;
    public float activeTime = 3.0f;
    public float activationTime = 2f;

    private bool pointed = false;

    private void FindAttatchedElements()
    {
        // Find all UI_Elements belonging to this parenting menu
        attatched_ui_elements = this.gameObject.GetComponentsInChildren<UI_Element>();

        // Set menu manager for children
        foreach (UI_Element ui_element in attatched_ui_elements)
        {
            ui_element.SetMenuManager(this);
        }

//        Debug.Log("Attatched " + attatched_ui_elements.Length + " children to " + this.name);
    }
    
    public override void Highlight()
    {
        // When active, just reset start timer of last highlight to now, so it doesn't deactivate 
        if (GetActive() == true)
            startTimer = Time.time;

        // When passive, count continuous highlight time until longer than activationTime -> then activate 
        else if (GetActive() == false)
        {
            currentTime = Time.time;

            timer = (currentTime - startTimer) / activationTime;

            if (timer >= 1)
            {
                Activate();
                startTimer = Time.time;
            }
        }

        //Debug.Log("highlighted: " + this.name);
    }

    // For UI Event Trigger
    public void PointerEnter()
    {
        if (pointed == false)
        {
            pointed = true;
            //Debug.Log("Enter");
        }
    }

    // For UI Event Trigger
    public void PointerExit()
    {
        if (pointed == true)
        {
            pointed = false;
            //Debug.Log("Exit");
        }
    }

    protected override void SubclassStart()
    {
        FindAttatchedElements();
        animator = GetComponent<Animator>();
        startTimer = Time.time;

        Deactivate();
    }

    protected override void SubclassUpdate()
    {
        // This is set with the event trigger system of unity
        if (pointed)
            Highlight();

        // As long as a submenu is active, the parent menu stays active
        if (GetActive() == true && GetIsChild() == true && menuManager.Equals(this) == false)
            menuManager.Highlight();

        if (GetActive() == true)
        {
            // Check how long ago last highlight was
            timer = (Time.time - startTimer) / activeTime;

            //Debug.Log("timer of: " + this.name + " - " + timer);

            // If longer than activeTime, deactivate
            if (timer >= 1)
            {
                Deactivate();
            }
        }

        else if (GetActive() == false)
        {
            // If last highlight was more than 0.1 seconds in the past, reset timer for continuous activation
            if (Time.time - currentTime > 0.1f)
                startTimer = Time.time;
        }
    }
}
