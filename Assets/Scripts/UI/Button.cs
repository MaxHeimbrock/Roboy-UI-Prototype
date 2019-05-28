using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : UI_Element , IClickable
{
    // For Highlighting
    Light highlight;

    public void Click()
    {
        Debug.Log(this.name + " was clicked.");
    }

    public override void Highlight()
    {
        Debug.Log("Highlight");
        highlight.enabled = true;
    }

    protected override void SubclassStart()
    {
        highlight = this.GetComponentInChildren<Light>();
    }

    protected override void SubclassUpdate()
    {
        highlight.enabled = false;
    }
}
