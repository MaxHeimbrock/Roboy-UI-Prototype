using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : UI_Element
{
    TextMesh text;

    public override void Highlight()
    {
        // If the infopanel is part of a magic corner, keep the magic corner highlighted as well
        if (menuManager != null)
            menuManager.Highlight();
        else
            throw new System.Exception("No menu manager found");

    }

    protected override void SubclassStart()
    {
        text = this.GetComponentInChildren<TextMesh>();
    }

    protected override void SubclassUpdate()
    {
        text.text = "" + Time.time;
    }
}
