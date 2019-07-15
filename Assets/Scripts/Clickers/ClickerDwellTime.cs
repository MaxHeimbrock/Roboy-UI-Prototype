using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickerDwellTime : Clicker, IRaycastSubscriber
{
    private float dwellTime;

    private RaycastHit lastHit;

    private Image dwellTimeIndicator;

    private float timer = 0;
    private float startTimer = 0;

    public void SetDwellTime(float dwellTime)
    {
        this.dwellTime = dwellTime;
    }

    public void ReceivePushNotification(RaycastHit hit, bool isHit)
    {
        // Check if something is hit and if it is clickable
        if (isHit == true && hit.transform.gameObject.GetComponent<UI_Element>() is IClickable)
        {
            // Same target as last frame 
            if (!lastHit.Equals(new RaycastHit()) && hit.collider.name == lastHit.collider.name)
            {
                //print("ClickerDwellTime is looking at " + hit.transform.name);

                timer = Time.time;
                dwellTimeIndicator.fillAmount = (timer - startTimer) / dwellTime;

                // is time difference bigger than dwell time?
                if (timer - startTimer > dwellTime)
                {
                    PushClick(1);
                    startTimer = Time.time;
                    dwellTimeIndicator.fillAmount = 0;
                }                
            }
            // Target changed - start new startTimer
            else
            {
                lastHit = hit;
                startTimer = Time.time;
                dwellTimeIndicator.fillAmount = 0;

                //Debug.Log("Raycast target changed to " + lastHit.collider.name);
                // Debug.Log("Target changed at " + startTimer);
            }
        }
        // Raycast hit no UI target - start new startTimer 
        else
        {
            lastHit = hit;
            startTimer = Time.time;
            dwellTimeIndicator.fillAmount = 0;

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
        //dwellTimeIndicator = UI_Manager.GetDwellTimeIndicator();
    }
}
