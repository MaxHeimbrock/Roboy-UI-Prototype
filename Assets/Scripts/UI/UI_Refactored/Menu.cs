using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Menu parentMenu;
    //public Menu[] submenus;

    public Button[] menuButtons;

    private bool pointed = false;

    private float startTimer;
    private float timer;
    private float currentTime = 0.0f;
    public float activeTime = 3.0f;
    public float activationTime = 2f;

    private bool active = false;

    private Animator animator;
    
    // Event to trigger something, if menu is activated
    [Serializable]
    public class MenuActivated : UnityEvent { }

    [SerializeField]
    private MenuActivated menuActivatedTriggered = new MenuActivated();
    public MenuActivated menuActivatedEvent { get { return menuActivatedTriggered; } set { menuActivatedTriggered = value; } }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startTimer = Time.time;

        foreach (Button menuButton in menuButtons)
        {
            menuButton.SetMenu(this);
        }

        Deactivate();
    }

    // Update is called once per frame
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

        menuActivatedEvent.Invoke();
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

    #endregion
}
