using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_Manager : MonoBehaviour
{
    public Image dwellTimeIndicator;
    float timeAmt = 10;
    float time;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            Debug.Log(time);
            time -= Time.deltaTime;
            dwellTimeIndicator.fillAmount = time / timeAmt;
        }
    }
}
