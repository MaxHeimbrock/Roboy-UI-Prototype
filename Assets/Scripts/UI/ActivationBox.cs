using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Just a script for a hitbox, that sends a highlight to the corresponding menu manager
public class ActivationBox : UI_Element
{
    private bool pointedAt;

    public void PointerEnter()
    {
        Debug.Log("Enter");
        pointedAt = true;
    }

    public void PointerExit()
    {
        Debug.Log("Exit");
        pointedAt = false;
    }

    public void Click()
    {

    }

    protected override void SubclassStart()
    {
        
    }

    protected override void SubclassUpdate()
    {
        if (pointedAt)
        {
            
            menuManager.Highlight();
        }
    }

    public override void Highlight()
    {
        throw new System.NotImplementedException();
    }
}
