using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRoboy : MonoBehaviour
{
    CustomSlider slider;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] sliders = GameObject.FindGameObjectsWithTag("Slider3D");
        for (int i = 0; i < sliders.Length; i++)
        {
            if(sliders[i].name.Equals("RotateRoboySlider"))
            {
                slider = sliders[i].GetComponent<CustomSlider>();
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, slider.GetValue() * 360, this.transform.localEulerAngles.z);
    }
}
