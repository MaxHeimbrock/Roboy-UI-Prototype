using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerDwellTime : Clicker, IRaycastSubscriber
{
    private double dwellTime;

    private RaycastHit lastHit;

    private int timer = 0;

    public void SetDwellTime(double dwellTime)
    {
        this.dwellTime = dwellTime;
        Debug.Log("Set dwell time to " + dwellTime);
    }

    public void ReceivePushNotification(RaycastHit hit, bool isHit)
    {
        if (isHit == true)
        {
            if (!lastHit.Equals(new RaycastHit()) && hit.collider.name == lastHit.collider.name)
            {
                print("ClickerDwellTime is looking at " + hit.transform.name);

                timer++;
                //Debug.Log(timer);
            }
            else
            {
                lastHit = hit;

                Debug.Log("Raycast target changed to " + lastHit.collider.name);
                timer = 0;
            }
        }
        else
        {
            lastHit = hit;
            
            Debug.Log("Raycast target lost.");
            timer = 0;
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
