using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlerpToCamera : MonoBehaviour
{
    private float startTime;

    private bool slerp = false;

    public float slerpSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("");

        CorrectRotation();

        if (slerp)
        {
            float slerpProgress = (Time.time - startTime) / slerpSpeed;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Camera.main.transform.rotation, slerpProgress);
        }
    }

    void CorrectRotation()
    {
        float rotationalDistance = Quaternion.Angle(this.transform.rotation, Camera.main.transform.rotation);

        //Debug.Log(rotationalDistance);

        if (rotationalDistance >= 20f && slerp == false)
        {
            slerp = true;
            startTime = Time.time;
            Debug.Log("Start Slerp");
        }
        else if (rotationalDistance <= 15f && slerp == true)
        {
            slerp = false;
            Debug.Log("Stop Slerp");
        }
    }
}
