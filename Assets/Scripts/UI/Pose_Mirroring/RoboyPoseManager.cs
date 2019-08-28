using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// The Pose Manager class updates the pose of Roboy according to the current pose of the user.
//public class RoboyPoseManager : Singleton<RoboyPoseManager>
public class RoboyPoseManager: MonoBehaviour
{
    public Transform Roboy;
    // Key: Name of part, Value: Roboy Part. Key encoded as String for readability. 
    public Dictionary<string, RoboyPart> RoboyParts = new Dictionary<string, RoboyPart>();
    private bool mockMode = false;
    public RosSharp.RosBridgeClient.Messages.Roboy.Pose msg;
    public bool poseUpdated = false;

    void Start()
    {
        Debug.Log("Start Manager");
        foreach (Transform t in Roboy)
        {
            if (t != null & t.CompareTag("RoboyPart"))
            {
                RoboyParts.Add(t.name, t.GetComponent<RoboyPart>());
            }
        }
        if (mockMode) 
        { 
            StartCoroutine(ExecuteAfterTime(2));
        }
    }

    void Update()
    {
        UpdatePose();
    }

    public void UpdatePose()
    {
        if (poseUpdated) { 
            RoboyPart part = null;
            if (msg != null) {
            Debug.Log("Part with ID: " + msg.id + " received."); 
                switch (msg.id)
                {
                   case 0:
                        RoboyParts.TryGetValue("upper_arm_right", out part);               
                        break;
                    case 1:
                        RoboyParts.TryGetValue("forarm_right", out part);
                        break;
                    case 2:                
                        RoboyParts.TryGetValue("hand_right", out part);                
                        break;
                    //TODO: add mapping to all other Roboy parts
                    default:
                        Debug.Log("Part not recognized");
                        break;
                }
            }
            // Only update position if a valid part has been recognized.
            if (part != null) {        
                part.transform.localPosition = new Vector3(msg.position.x, msg.position.y, msg.position.z);
                part.transform.localRotation = new Quaternion(msg.orientation.x, msg.orientation.y, msg.orientation.z, msg.orientation.w);
                poseUpdated = false;
            }
        }
    }

    // Mock method for simulating Roboy Pose Mirroring using right hand only
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        MockRoboyPose pose = new MockRoboyPose();
        foreach (KeyValuePair<string, RoboyPart> roboyPart in RoboyParts)
        {
            string index = roboyPart.Key;
            if (index == "hand_right")
            {
                roboyPart.Value.transform.localPosition = pose.GetPoseForPart(index).Position;          
                roboyPart.Value.transform.localRotation = pose.GetPoseForPart(index).Rotation;
            }
        }
    }
}
