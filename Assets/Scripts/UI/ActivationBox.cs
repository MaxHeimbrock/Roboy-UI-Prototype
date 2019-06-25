using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Just a script for a hitbox, that sends a highlight to the corresponding menu manager
public class ActivationBox : UI_Element
{
    public override void Highlight()
    {
        menuManager.Highlight();
    }

    protected override void SubclassStart()
    {
        
    }

    protected override void SubclassUpdate()
    {
        
    }
}
