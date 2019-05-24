using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickerDwellTime : Clicker, IRaycastSubscriber
{
    private double dwellTime;

    private RaycastHit lastHit;

    private float timer = 0;
    private float startTimer = 0;

    public void SetDwellTime(double dwellTime)
    {
        this.dwellTime = dwellTime;
        Debug.Log("Set dwell time to " + dwellTime);
    }

    public void ReceivePushNotification(RaycastHit hit, bool isHit)
    {
        if (isHit == true)
        {
            // Same target as last frame 
            if (!lastHit.Equals(new RaycastHit()) && hit.collider.name == lastHit.collider.name)
            {
                //print("ClickerDwellTime is looking at " + hit.transform.name);

                timer = Time.time;
                
                // is time difference bigger than dwell time?
                if (timer - startTimer > dwellTime)
                {
                    UI_Manager.Click(1);
                    startTimer = Time.time;
                }                
            }
            // Target changed - start new startTimer
            else
            {
                lastHit = hit;
                startTimer = Time.time;

                //Debug.Log("Raycast target changed to " + lastHit.collider.name);
                // Debug.Log("Target changed at " + startTimer);
            }
        }
        // Raycast hit no UI target - start new startTimer 
        else
        {
            lastHit = hit;
            startTimer = Time.time;

            //Debug.Log("Raycast target lost.");
        }
    }

    public void SubscribeToRaycastManager()
    {
        gameObject.GetComponent<RaycastManager>().Subscribe(this);
    }

    // Just for Mock - Dwell time is managed in UI Manager
    protected override void SubclassStart()
    {
        SubscribeToRaycastManager();
    }
}
