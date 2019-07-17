using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Button : UI_Element , IClickable
{
    public float dwellTime = 2.0f;

    // Minimum and maximum values for the transition.
    float minimum = 1f;
    float maximum = 0.0f;

    // Time taken for the transition.
    float duration = 0.5f;

    float startTime;
    float currentTimer;

    bool pointed = false;

    bool clicked = false;

    Image dwellTimeImage;

    //my event
    [Serializable]
    public class ButtonIsClicked : UnityEvent { }

    [SerializeField]
    private ButtonIsClicked buttonIsClickedEvent = new ButtonIsClicked();
    public ButtonIsClicked TriggeredEvent { get { return buttonIsClickedEvent; } set { buttonIsClickedEvent = value; } }

    public void Click()
    {
        if (clicked == false)
        {
            //Debug.Log("Clicked at " + this.name + " inside Button class");

            LogText.Instance.addToLogText("Clicked at " + this.name + " inside Button class");

            foreach (UI_Element child in children)
            {
                child.gameObject.SetActive(true);
                child.Activate();
            }

            TriggeredEvent.Invoke();
        }

        clicked = true;
    }

    public override void Highlight()
    {
        // If the button is part of a menu, highlight the menu as well
        if (menuManager != null)
            menuManager.Highlight();
    }

    protected override void SubclassStart()
    {
        // The script is attatched to the dwell time indicator
        dwellTimeImage = this.GetComponent<Image>();

        startTime = Time.time;

        foreach (UI_Element child in children)
        {
            child.SetIsChild();
        }
    }

    protected override void SubclassUpdate()
    {
        // Draw dwell time indicator
        float f;
        if (pointed)
        {
            currentTimer = Time.time;
            f = (currentTimer - startTime) / dwellTime;
            dwellTimeImage.fillAmount = f;

            if (f >= 1.0f)
            {
                //startTime = Time.time;
                Click();
            }

            Highlight();
        }
    }

    // For UI Event Trigger
    public void PointerEnter()
    {
        if (pointed == false)
        {
            startTime = Time.time;
            pointed = true;
        }
    }

    // For UI Event Trigger
    public void PointerExit()
    {
        if (pointed == true)
        {
            pointed = false;
            dwellTimeImage.fillAmount = 0;
        }

        clicked = false;
    }
}
