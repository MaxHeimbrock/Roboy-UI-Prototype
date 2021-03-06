﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;
/// <summary>
///  The Pose Manager class updates the pose of Roboy according to the current pose of the user.
/// </summary>
public class RoboyPoseManager: MonoBehaviour
{
    [SerializeField]
    [Tooltip("Set the Roboy model to be updated.")]
    private GameObject Roboy;
    RosSharp.RosBridgeClient.Messages.Roboy.Pose message;

    void Start()
    {
        Debug.Log("Start Pose Manager");       
    }

    public void Update()
    {
        if(gameObject.GetComponent<PoseSubscriber>().MessageQueueCount() != 0)
        {   
            message = gameObject.GetComponent<PoseSubscriber>().DequeuPoseMessage();
            UpdatePose();
        }
    }

    /// <summary>
    /// Updates the pose according to the updates received via ROS.
    /// </summary>
    public void UpdatePose()
    {   
        if (message != null) {
            Debug.Log("Part with ID: " + message.id + " received."); 
                switch (message.id)
                {
                    // Mapping is fixes but is "random". Look at UpdatePose Script.
                    case 0:
                        updateNode(Roboy.gameObject.transform.Find("upper_arm_right"),message);
                    break;
                    case 1:
                        updateNode(Roboy.gameObject.transform.Find("forearm_right"),message);
                    break;
                    case 2:               
                        updateNode(Roboy.gameObject.transform.Find("hand_right"),message);
                    break;
                    case 3:
                        updateNode(Roboy.gameObject.transform.Find("elbow_right"),message);
                        break;
                    case 4:
                        updateNode(Roboy.gameObject.transform.Find("upper_arm_left"),message);
                        break;
                    case 5:
                        updateNode(Roboy.gameObject.transform.Find("forearm_left"),message);
                        break;
                    case 6:
                        updateNode(Roboy.gameObject.transform.Find("hand_left"),message);
                        break;
                    case 7:                   
                        updateNode(Roboy.gameObject.transform.Find("elbow_left"),message);
                        break;
                    case 8:
                        updateNode(Roboy.gameObject.transform.Find("head"),message);
                        break;
                    //TODO: add mapping to all other Roboy parts
                    default:
                        Debug.Log("Part not recognized");
                        break;
                }         
            }
        }
    /// <summary>
    /// Updates the specific nodes in the Roboy 3D model.
    /// </summary>
    /// <param name="child">Child.</param>
    /// <param name="msg">Message.</param>
    public void updateNode(Transform child, RosSharp.RosBridgeClient.Messages.Roboy.Pose msg)
    {   
        if(child == null)
        {
            Debug.Log("null");
        }
        Debug.Log(child.name);
        child.transform.localPosition = new Vector3(msg.position.x, msg.position.y, msg.position.z);
        child.transform.localRotation = new Quaternion(msg.orientation.x, msg.orientation.y, msg.orientation.z, msg.orientation.w);
    }
}

