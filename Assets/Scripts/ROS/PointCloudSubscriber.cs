using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Messages.Roboy;
using RosSharp.RosBridgeClient.Messages.Sensor;
using System.IO;
using RosSharp.RosBridgeClient;
using System;

public class PointCloudSubscriber : Subscriber<RosSharp.RosBridgeClient.Messages.Sensor.PointCloud2>
{
    public GameObject spherePrefab;
    SphereInstantiate s;

    /// <summary>
    /// Holds the currently received data for other objects to read
    /// </summary>
    private string messageData;

    /// <summary>
    /// Start method of TestSubscriber.
    /// Starts a coroutine to initialize the subscriber after 1 second to prevent race conditions.
    /// </summary>
    protected override void Start()
    {

        GameObject g = GameObject.Find("ROS Connection");
        s = g.GetComponent<SphereInstantiate>();

        StartCoroutine(startSubscriber(1.0f));
    }

    /// <summary>
    /// Initializes the subscriber.
    /// </summary>
    /// <param name="waitTime"> defines the time, after that subscriber is initialized.</param>
    /// <returns></returns>
    private IEnumerator startSubscriber(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            print("Starting Listener");
            base.Start();
            break;
        }
    }

    /// <summary>
    /// This handler is called, whenever a message on the subscribed topic is received.
    /// </summary>
    /// <param name="message"> is the received message.</param>
    protected override void ReceiveMessage(PointCloud2 message)
    {
        processPointCloud(message);
        gameObjects.Clear();
    }

    private List<GameObject> gameObjects = new List<GameObject>();
    public Vector3[] allSpheres = new Vector3[5000];

    void processPointCloud(PointCloud2 pointCloud2) {
        PointCloud pointCloud = new PointCloud(pointCloud2);

        for(int i = 0; i < pointCloud.Points.Length; i++) {
            //Debug.Log("Coordinate: X=" + pointCloud.Points[i].x + ", Y=" + pointCloud.Points[i].y + ", Z=" + pointCloud.Points[i].z);
            allSpheres[i] = new Vector3(-pointCloud.Points[i].y, pointCloud.Points[i].z, pointCloud.Points[i].x);

            if(i % 10 == 0) {
                s.doInstantiate(allSpheres[i]);
            }
        }
    }
}
