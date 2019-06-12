using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_Element : MonoBehaviour
{
    public UI_Element[] children;
    public MagicCorner magicCorner;

    // Start is called before the first frame update
    void Start()
    {
        SubclassStart();
    }

    // Update is called once per frame
    void Update()
    {
        SubclassUpdate();   
    }

    protected abstract void SubclassStart();

    protected abstract void SubclassUpdate();

    public abstract void Highlight();
}
