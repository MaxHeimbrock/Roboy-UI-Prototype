using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Clicker : MonoBehaviour
{
    public UI_Manager UI_Manager;

    public void Start()
    {
        UI_Manager = GetComponent<UI_Manager>();
        SubclassStart();
    }

    public void PushClick(int code)
    {
        UI_Manager.Click(code);
    }

    protected abstract void SubclassStart();
}
