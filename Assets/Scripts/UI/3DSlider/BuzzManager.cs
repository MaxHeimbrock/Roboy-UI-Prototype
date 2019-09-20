using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzManager : Singleton<BuzzManager>
{

    private int[] fingers;
    private SenseGlove_Object senseGloveObject;

    /// <summary>
    /// Set the reference to the right SenseGlove
    /// and initialize the array determining the buzz intensity for each finger.
    /// </summary>
    void Start()
    {
        senseGloveObject = GameObject.FindGameObjectWithTag("SenseGloveRight").GetComponent<SenseGlove_Object>();
        fingers = new int[] { 0, 0, 0, 0, 0 };
    }

    /// <summary>
    /// Start the buzz motors according to the fingers array.
    /// </summary>
    void Update()
    {
        senseGloveObject.SendBuzzCmd(fingers, 500);
        fingers = new int[] {0,0,0,0,0};
    }

    /// <summary>
    /// With this method extern classes can schedule that the buzz motor for a finger gets activated.
    /// </summary>
    /// <param name="fingerindex">The finger for which the buzz motor shall be activated. 0-5 is mapped to thumb-pinky.</param>
    /// <param name="buzzintensity">Intensity of the buzz motor (0-100).</param>
    public void ActivateFinger(int fingerindex, int buzzintensity)
    {
        if(fingerindex < 5 && fingerindex >= 0)
        {
            fingers[fingerindex] = buzzintensity;
        }
    }
}
