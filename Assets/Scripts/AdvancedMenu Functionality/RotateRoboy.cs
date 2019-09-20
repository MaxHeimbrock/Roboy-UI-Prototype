using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRoboy : MonoBehaviour
{
    public CustomSlider slider;
    
    /// <summary>
    /// Updates Roboy's rotation corresponding to the assigned slider's value,
    /// where the value 0-100 percent is mapped to 0-360 degree
    /// </summary>
    void Update()
    {
        this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, slider.GetValue() * 360, this.transform.localEulerAngles.z);
    }

    /// <summary>
    /// When this script gets enabled the slider is set to its default value,
    /// so that Roboy is always looking in the correct direction during the transition.
    /// </summary>
    private void OnEnable()
    {
        slider.setDefaultValue();
    }
}
