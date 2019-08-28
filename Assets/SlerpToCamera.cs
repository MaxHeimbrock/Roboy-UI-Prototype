using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlerpToCamera : MonoBehaviour
{
    private float startTime;

    private bool slerp = false;

    public float slerpSpeed = 10f;

    public float startSlerpAngle = 15f;
    public float stopSlerpAngle = 15f;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("");

        this.transform.position = Camera.main.transform.position;

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

        if (rotationalDistance >= startSlerpAngle && slerp == false)
        {
            slerp = true;
            startTime = Time.time;
            Debug.Log("Start Slerp");
        }
        else if (rotationalDistance <= stopSlerpAngle && slerp == true)
        {
            slerp = false;
            Debug.Log("Stop Slerp");
        }
    }
}
