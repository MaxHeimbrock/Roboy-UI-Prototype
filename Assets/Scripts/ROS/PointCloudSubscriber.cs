using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Messages.Roboy;
using RosSharp.RosBridgeClient.Messages.Sensor;
using System.IO;
using System;

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
        //print("Message Received");
        for (int i = 1; i < message.width; i += 16)
        {
            Get3DPoint(message, i, 0);
        }
        print("==");
        gameObjects.Clear();
    }

    private List<GameObject> gameObjects = new List<GameObject>();

    void Get3DPoint(PointCloud2 pointCloud, int u, int v)
    {
        //print("INIT: Get3DPoint, ValueTest=" + pointCloud.width);
        int width = pointCloud.width;
        int height = pointCloud.height;
        

        int arrayPos = v * pointCloud.row_step + u * pointCloud.point_step;

      

        int arrayPosX = arrayPos + pointCloud.fields[0].offset; // X has an offset of 0
        int arrayPosY = arrayPos + pointCloud.fields[1].offset; // Y has an offset of 4
        int arrayPosZ = arrayPos + pointCloud.fields[2].offset; // Z has an offset of 8
        
     

        MemoryStream stream = new MemoryStream(pointCloud.data);
        BinaryReader reader = new BinaryReader(stream);

        stream.Position = arrayPosX;
        float x = reader.ReadSingle();
        stream.Position = arrayPosY;
        float y = reader.ReadSingle();
        stream.Position = arrayPosY;
        float z = reader.ReadSingle();

        print("Coordinate: X=" + x + ", Y=" + y + ", Z=" + z);

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(x, y, z);
        sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        Instantiate(sphere);
        gameObjects.Add(sphere);

    }
}
