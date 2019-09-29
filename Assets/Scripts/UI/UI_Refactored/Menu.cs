using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// Menu class for organizing input, assemble submenu structures and activate/deactivate them, handle pointing.
/// Can be in an active or passive state. The a visual transition from passive to active is done with an animator.
/// </summary>
public class Menu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Menu parentMenu;

    [Header("All OUI-Buttons on this menu level")]
    public OUI_Button[] menuButtons;

    private bool pointed = false;
    private bool active = false;
    private Animator animator;

    private float startTimer;
    private float timer;
    private float currentTime = 0.0f;

    [Header("Timings of active state")]
    [Tooltip("How long menu stays active without pointing on it")]
    public float activeTime = 3.0f;
    [Tooltip("How long menu needs pointing to activate")]
    public float activationTime = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startTimer = Time.time;

        foreach (OUI_Button menuButton in menuButtons)
        {
            menuButton.SetMenu(this);
        }

        Deactivate();
    }

    /// <summary>
    /// Handles pointing, passes highligh on to parent menu and handles the timer for activation
    /// </summary>
    void Update()
    {
        // This is set with the event trigger system of unity
        if (pointed)
        {
            Highlight();
        }

        // as long as submenu is active, parent is highlighted to stay active
        if (parentMenu != null && active == true)
        {
            parentMenu.Highlight();
        }

        if (active == true)
        {
            // Check how long ago last highlight was
            timer = (Time.time - startTimer) / activeTime;

            // If longer than activeTime, deactivate
            if (timer >= 1)
            {
                Deactivate();
            }
        }

        else if (active == false)
        {
            // If last highlight was more than 0.1 seconds in the past, reset timer for continuous activation
            if (Time.time - currentTime > 0.1f)
                startTimer = Time.time;
        }
    }

    /// <summary>
    /// Highlight() is being called, if the menu or a submenu of this menu is pointed at. It means this menu stays active or will get activated by pointing (highlighting) long enough.
    /// </summary>
    public void Highlight()
    {
        // Debug.Log("Highlight");

        // When active, just reset start timer of last highlight to now, so it doesn't deactivate 
        if (active == true)
            startTimer = Time.time;

        // When passive, count continuous highlight time until longer than activationTime -> then activate 
        else if (active == false)
        {
            currentTime = Time.time;

            timer = (currentTime - startTimer) / activationTime;

            if (timer >= 1)
            {
                Activate();
                startTimer = Time.time;
            }
        }
    }

    #region Helper
    
    public void Activate()
    {
        animator.SetBool("Active", true);
        active = true;
    }

    public void Deactivate()
    {
        animator.SetBool("Active", false);
        active = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (pointed == false)
        {
            pointed = true;
            //Debug.Log("Enter");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (pointed == true)
        {
            pointed = false;
            //Debug.Log("Exit");
        }
    }

    public bool GetActive()
    {
        return active;
    }

    public bool GetPointed()
    {
        return pointed;
    }

    #endregion
}
