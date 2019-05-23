using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoCapture : MonoBehaviour
{
    WebCamTexture webcamTexture;
    Color32[] frame;

    void Start()
    {
        // Start web cam feed
        webcamTexture = new WebCamTexture();
        webcamTexture.Play();
        frame = new Color32[webcamTexture.width * webcamTexture.height];
    }

    void Update()
    {
        webcamTexture.GetPixels32(frame);

        Debug.Log("VideoCapture active");
        unsafe
        {
            fixed (Color32* fframe = frame)
            {
                // Accessing colors works here, but the Colors come from pointers, they are not saved consecutive in the RAM
                Debug.Log(*fframe);
            }
        }

        // Do processing of data here.
    }

    public Color32[] GetCameraFrame()
    {
        return frame;
    }
}
