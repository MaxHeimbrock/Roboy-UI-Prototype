using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTag : MonoBehaviour
{
    public void updateTag(string name)
    {
        this.transform.GetChild(1).GetComponent<TextMesh>().text = name;
    }
}
