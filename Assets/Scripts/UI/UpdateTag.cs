using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTag : MonoBehaviour
{
    public void updateTag(string name)
    {
        this.transform.GetChild(1).GetComponent<TextMesh>().text = name;

        Transform plate = this.transform.GetChild(0);
        Vector3 scale = plate.localScale;
        if (name.Equals("Point Cloud"))
        {
            plate.localScale = new Vector3(0.075f, scale.y, scale.z);
        } else
        {
            plate.localScale = new Vector3(0.065f, scale.y, scale.z);
        }
    }
}
