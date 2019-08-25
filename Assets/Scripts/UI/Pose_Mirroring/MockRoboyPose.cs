using UnityEngine;
using System.Collections.Generic;

public class MockRoboyPose : RoboyPose
{
    // Use this for initialization
    void Start()
    {

    }

    public override Dictionary<string, RoboyPart> GetRoboyParts()
    {
        Dictionary<string, RoboyPart> RoboyParts = new Dictionary<string, RoboyPart>();

        Vector3 originPosition = new Vector3((float)-0.391, (float)-0.026, (float)-0.44);
        Quaternion originRotation = Quaternion.Euler((float)13.639, (float)-133.252, (float)-7.909);
        RoboyParts.Add("forarm_right", new RoboyPart(originPosition, originRotation));

        Vector3 originPosition_1 = new Vector3((float)-0.514, (float)-0.052, (float)-0.552);
        Quaternion originRotation_1 = Quaternion.Euler((float)-3.353, (float)-148.248, (float)-12.106);
        RoboyParts.Add("hand_right", new RoboyPart(originPosition, originRotation));

        return RoboyParts;
    }

    public override RoboyPart GetPoseForPart(string key)
    {
        if (key == "forarm_right")
        {
            Vector3 originPosition = new Vector3((float)-0.391, (float)-0.026, (float)-0.44);
            Quaternion originRotation = Quaternion.Euler((float)13.639, (float)-133.252, (float)-7.909);
            return new RoboyPart(originPosition, originRotation);
        }
        if (key == "hand_right")
        {
            Vector3 originPosition = new Vector3((float)-0.514, (float)-0.052, (float)-0.552);
            Quaternion originRotation = Quaternion.Euler((float)-3.353, (float)-148.248, (float)-12.106);
            return new RoboyPart(originPosition, originRotation);
        }
        else
        {
            throw new System.ArgumentException("There is no Roboy part with this key", nameof(key));
        }
    }
}
