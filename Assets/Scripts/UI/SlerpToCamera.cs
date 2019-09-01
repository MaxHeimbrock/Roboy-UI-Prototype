using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlerpToCamera : MonoBehaviour
{
    private float startTime;

    private bool slerp = false;

    public float slerpSpeed = 10f;

    public float startSlerpAnglePointed = 15f;
    public float startSlerpAngleNotPointed = 4f;
    public float stopSlerpAnglePointed = 4f;
    public float stopSlerpAngleNotPointed = 1f;

    // Right now only one menu, so ill only look for bottom menu pointed
    public Menu bottomMenu;

    private float flexibleStartSlerpAngle;
    private float flexibleStopSlerpAngle;

    void Start()
    {
        flexibleStartSlerpAngle = startSlerpAngleNotPointed;
        flexibleStopSlerpAngle = stopSlerpAngleNotPointed;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("");

        if (bottomMenu.GetPointed())
        {
            flexibleStartSlerpAngle = startSlerpAnglePointed;
            flexibleStopSlerpAngle = stopSlerpAnglePointed;
        }
        else
        {
            flexibleStartSlerpAngle = startSlerpAngleNotPointed;
            flexibleStopSlerpAngle = stopSlerpAngleNotPointed;
        }

        this.transform.position = Camera.main.transform.position;

        CorrectRotation();        
    }

    void CorrectRotation()
    {
        float rotationalDistance = Quaternion.Angle(this.transform.rotation, Camera.main.transform.rotation);

        //Debug.Log(rotationalDistance);

        if (rotationalDistance >= flexibleStartSlerpAngle && slerp == false)
        {
            slerp = true;
            startTime = Time.time;
            Debug.Log("Start Slerp");
        }
        else if (rotationalDistance <= flexibleStopSlerpAngle && slerp == true)
        {
            slerp = false;
            Debug.Log("Stop Slerp");
        }

        // If slerp is active, use Quaternion.Slerp to correct rotation
        if (slerp)
        {
            float slerpProgress = (Time.time - startTime) / slerpSpeed;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Camera.main.transform.rotation, slerpProgress);
        }
    }
}
