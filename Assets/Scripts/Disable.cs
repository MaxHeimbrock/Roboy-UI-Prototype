using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : MonoBehaviour
{
    private void OnDisable()
    {
        Debug.Log("Disabled");
    }

    private void OnEnable()
    {
        Debug.Log("Enabled");
    }
}
