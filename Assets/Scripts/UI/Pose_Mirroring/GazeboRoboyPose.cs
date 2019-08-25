using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GazeboRoboyPose : RoboyPose
{
    // Use this for initialization
    void Start()
    {

    }

    public override Dictionary<string, RoboyPart> GetRoboyParts()
    {
        return null;
    }

    public override RoboyPart GetPoseForPart(string key)
    {
        return null;
    }
}
