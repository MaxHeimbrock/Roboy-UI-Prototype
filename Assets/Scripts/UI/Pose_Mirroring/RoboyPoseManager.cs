using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// The Pose Manager class updates the pose of Roboy according to the current pose of the user.
public class RoboyPoseManager : Singleton<RoboyPoseManager>
{
    public Transform Roboy;
    // Key: Name of part, Value: Roboy Part. Key encoded as String for readability. 
    public Dictionary<string, RoboyPart> RoboyParts = new Dictionary<string, RoboyPart>();
    private bool mockMode = false;

    void Start()
    {
        foreach (Transform t in Roboy)
        {
            if (t != null & t.CompareTag("RoboyPart"))
            {
                RoboyParts.Add(t.name, t.GetComponent<RoboyPart>());
            }
        }
        if (mockMode) 
        { 
            StartCoroutine(ExecuteAfterTime(3));
        }
}

    public void UpdatePose(RosSharp.RosBridgeClient.Messages.Roboy.Pose message)
    {
        RoboyPart part = null;
        switch (message.id)
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
        // Only update position if a valid part has been recognized.
        if (part != null) { 
            part.transform.localPosition = new Vector3(message.position.x, message.position.y, message.position.z);
            part.transform.localRotation = new Quaternion(message.orientation.x, message.orientation.y, message.orientation.z, message.orientation.w);
        }
    }

    // Mock method for simulating Roboy Pose Mirroring
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        MockRoboyPose pose = new MockRoboyPose();
        foreach (KeyValuePair<string, RoboyPart> roboyPart in RoboyParts)
        {
            string index = roboyPart.Key;

            Debug.Log(index);
            Debug.Log(roboyPart.Value);
            roboyPart.Value.transform.localPosition = pose.GetPoseForPart(index).Position;
            roboyPart.Value.transform.localRotation = pose.GetPoseForPart(index).Rotation;           
        }
    }
}
