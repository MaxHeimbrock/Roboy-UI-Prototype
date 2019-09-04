using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzManager : Singleton<BuzzManager>
{

    private int[] fingers;
    private SenseGlove_Object senseGloveObject;
    // Start is called before the first frame update
    void Start()
    {
        senseGloveObject = GameObject.FindGameObjectWithTag("SenseGloveRight").GetComponent<SenseGlove_Object>();
        fingers = new int[] { 0, 0, 0, 0, 0 };
    }

    // Update is called once per frame
    void Update()
    {
        senseGloveObject.SendBuzzCmd(fingers, 500);
        fingers = new int[] {0,0,0,0,0};
    }

    public void ActivateFinger(int fingerindex, int buzzintensity)
    {
        if(fingerindex < 5 && fingerindex >= 0)
        {
            fingers[fingerindex] = buzzintensity;
        }
    }
}
