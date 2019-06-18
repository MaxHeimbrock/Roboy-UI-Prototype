using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : UI_Element, IClickable
{
    // For Highlighting
    Light highlight;

    // Minimum and maximum values for the transition.
    float minimum = 1f;
    float maximum = 0.0f;

    // Time taken for the transition.
    float duration = 0.5f;

    float startTime;

    public void Click()
    {
        Debug.Log("Clicked at " + this.name + " inside Button class");

        foreach (UI_Element child in children)
        {
            child.gameObject.SetActive(true);
            child.Activate();
        }
    }

    public override void Highlight()
    {
        //Debug.Log("Highlight " + this.name);
        startTime = Time.time;

        // If the button is part of a magic corner, keep the magic corner highlighted as well
        if (menuManager != null)
            menuManager.Highlight();
    }

    protected override void SubclassStart()
    {
        highlight = this.GetComponentInChildren<Light>();
        startTime = Time.time;

        foreach (UI_Element child in children)
        {
            child.SetIsChild();
        }
    }

    protected override void SubclassUpdate()
    {
        // Sets the highlight as a smooth transition of intensity
        float t = (Time.time - startTime) / duration;
        highlight.intensity = Mathf.SmoothStep(minimum, maximum, t);
    }
}
