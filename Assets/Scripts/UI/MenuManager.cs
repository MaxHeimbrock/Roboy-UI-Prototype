using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    UI_Element[] attatched_ui_elements;    
    Animator animator;

    private enum state {active, passive};
    private state currentState = state.passive;

    private float startTimer;
    private float timer;
    private float currentTime = 0.0f;
    public float activeTime = 3.0f;
    public float activationTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        FindAttatchedElements();
        animator = GetComponent<Animator>();
        startTimer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == state.active)
        {
            // Check how long ago last highlight was
            timer = (Time.time - startTimer) / activeTime;

            // If longer than activeTime, deactivate
            if (timer >= 1)
            {
                currentState = state.passive;
                animator.SetBool("Active", false);
                DeactivateAttatchedElements();
            }
        }

        else if (currentState == state.passive)
        {
            // If last highlight was more than 0.1 seconds in the past, reset timer for continuous activation
            if (Time.time - currentTime > 0.1f)
                startTimer = Time.time;
        }
    }

    private void FindAttatchedElements()
    {
        // Find all UI_Elements belonging to this parenting menu
        attatched_ui_elements = this.gameObject.GetComponentsInChildren<UI_Element>();

        // Set menu manager for children
        foreach (UI_Element ui_element in attatched_ui_elements)
        {
            ui_element.SetMenuManager(this);
        }

        Debug.Log("Attatched " + attatched_ui_elements.Length + " children to " + this.name);
    }

    // Deactivates all children, if menu is deactivated
    private void DeactivateAttatchedElements()
    {
        foreach (UI_Element ui_element in attatched_ui_elements)
        {
            if (ui_element.GetIsChild() == true)
            {
                ui_element.Deactivate();
            }
        }
    }

    public void Highlight()
    {
        // When active, just reset start timer of last highlight to now, so it doesn't deactivate 
        if (currentState == state.active)
            startTimer = Time.time;

        // When passive, count continuous highlight time until longer than activationTime -> then activate 
        else if (currentState == state.passive)
        {
            currentTime = Time.time;

            timer = (currentTime - startTimer) / activationTime;

            if (timer >= 1)
            {
                currentState = state.active;
                animator.SetBool("Active", true);                
            }
        }
    }
}
