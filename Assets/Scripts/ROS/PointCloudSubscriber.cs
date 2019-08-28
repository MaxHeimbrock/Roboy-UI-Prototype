using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Messages.Roboy;
using RosSharp.RosBridgeClient.Messages.Sensor;
using System.IO;
using RosSharp.RosBridgeClient;
using System;
using System.Linq;

public class PointCloudSubscriber : Subscriber<RosSharp.RosBridgeClient.Messages.Sensor.PointCloud2>
{
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
    }

    public PC_CircularBuffer<Vector3> allPoints = new PC_CircularBuffer<Vector3>(160000);
    /// <summary>
    /// Extracts the 3D coordinates of the point cloud and converts them to the Unity coordinate system.
    /// For further processing, all points are added to a circular buffer.
    /// </summary>
    /// <param name="pointCloud2">the ros pointcloud message</param>
    void processPointCloud(PointCloud2 pointCloud2) {
        PointCloud pointCloud = new PointCloud(pointCloud2);

        for(int i = 0; i < pointCloud.Points.Length; i++) {
            // To increase performance, just take every 10th point. This works, because the point cloud is very dense anyways.
            if (i % 10 == 0) {
                allPoints.Add(new Vector3(-pointCloud.Points[i].y, pointCloud.Points[i].z, pointCloud.Points[i].x));
            }
        }
    }
}
