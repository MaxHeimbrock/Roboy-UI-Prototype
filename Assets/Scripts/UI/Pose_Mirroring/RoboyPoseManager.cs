using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// The Pose Manager class updates the pose of Roboy according to the current pose of the user.
public class RoboyPoseManager : Singleton<RoboyPoseManager>
{
    public Transform Roboy;
    public Dictionary<string, RoboyPart> RoboyParts = new Dictionary<string, RoboyPart>();
    public RoboyPose pose = new MockRoboyPose();

    void Start()
    {
        foreach (Transform t in Roboy)
        {
            if (t != null & t.CompareTag("RoboyPart"))
            {
                RoboyParts.Add(t.name, t.GetComponent<RoboyPart>());
            }
        }
        StartCoroutine(ExecuteAfterTime(3));
    }

    void Update()
    {

    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        foreach (KeyValuePair<string, RoboyPart> roboyPart in RoboyParts)
        {
            string index = roboyPart.Key;

            if (index == "forearm_right")
            {
                Debug.Log(index);
                Debug.Log(roboyPart.Value);
                /*//Vector3 originPosition = new Vector3(xPositionsDictionary[index], yPositionsDictionary[index], zPositionsDictionary[index]);
                 //Quaternion originRotation = new Quaternion(roboyPart.Value.Rotation.x, roboyPart.Value.Rotation.y, roboyPart.Value.Rotation.z, roboyPart.Value.Rotation.w);
                 Vector3 originPosition = new Vector3((float)-0.389, (float)-1.51182e-05, (float)-0.4624269);
                 Quaternion originRotation = Quaternion.Euler((float)-20.893, (float)-159.952, 0);

                 roboyPart.Value.transform.localPosition = (originPosition);
                 roboyPart.Value.transform.localRotation = (originRotation);
                 Debug.Log(originPosition);
                 Debug.Log(roboyPart.Value.transform.localPosition);
                 Debug.Log(originRotation);
                 Debug.Log(roboyPart.Value.transform.localRotation);*/
                roboyPart.Value.transform.localPosition = pose.GetPoseForPart("forarm_right").Position;
                roboyPart.Value.transform.localRotation = pose.GetPoseForPart("forarm_right").Rotation;


            }
            if (index == "hand_right")
            {
                roboyPart.Value.transform.localPosition = pose.GetPoseForPart("hand_right").Position;
                roboyPart.Value.transform.localRotation = pose.GetPoseForPart("hand_right").Rotation;
            }
        }
    }
}
