using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class ClickTest : MonoBehaviour
{
    //my event
    [Serializable]
    public class ButtonIsClicked : UnityEvent { }

    [SerializeField]
    private ButtonIsClicked buttonIsClicked = new ButtonIsClicked();
    public ButtonIsClicked onMyOwnEvent { get { return buttonIsClicked; } set { buttonIsClicked = value; } }

    public void MyOwnEventTriggered()
    {
        onMyOwnEvent.Invoke();
    }

}
///add a collider to the object as well so the OnPointerClick can work

