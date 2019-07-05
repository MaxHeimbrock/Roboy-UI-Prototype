using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    Image dwellTimeImage;

    public void Click()
    {
        //Debug.Log("Clicked at " + this.name + " inside Button class");

        foreach (UI_Element child in children)
        {
            child.gameObject.SetActive(true);
            child.Activate();
        }
    }

    public override void Highlight()
    {
        // If the button is part of a magic corner, keep the magic corner highlighted as well
        if (menuManager != null)
            menuManager.Highlight();
    }

    protected override void SubclassStart()
    {
        dwellTimeImage = this.GetComponent<Image>();

        startTime = Time.time;

        foreach (UI_Element child in children)
        {
            child.SetIsChild();
        }
    }

    protected override void SubclassUpdate()
    {
        float f;

        if (pointed)
        {
            currentTimer = Time.time;
            f = (currentTimer - startTime) / dwellTime;
            dwellTimeImage.fillAmount = f;
            Debug.Log(f);
            if (f >= 1.0f)
                Click();
            Highlight();
        }
    }

    public void PointerEnter()
    {
        if (pointed == false)
        {
            startTime = Time.time;
            pointed = true;
        }
    }

    public void PointerExit()
    {
        if (pointed == true)
        {
            pointed = false;
            dwellTimeImage.fillAmount = 0;
        }
    }
}
