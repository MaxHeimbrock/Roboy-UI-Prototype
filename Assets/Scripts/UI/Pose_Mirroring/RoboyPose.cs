using System;
using System.Collections.Generic;

public abstract class RoboyPose {

        public abstract Dictionary<string, RoboyPart> GetRoboyParts();
    public abstract RoboyPart GetPoseForPart(string key);
        }

