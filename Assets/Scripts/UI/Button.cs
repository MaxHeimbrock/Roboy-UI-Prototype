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
        throw new System.NotImplementedException();
    }

    public override void Highlight()
    {
        Debug.Log("Highlight " + this.name);
        startTime = Time.time;
    }

    protected override void SubclassStart()
    {
        highlight = this.GetComponentInChildren<Light>();
        startTime = Time.time;
    }

    protected override void SubclassUpdate()
    {
        float t = (Time.time - startTime) / duration;
        highlight.intensity = Mathf.SmoothStep(minimum, maximum, t);
    }
}
