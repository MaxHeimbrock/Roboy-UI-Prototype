using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using System.Collections;

public class UpdatePose : MonoBehaviour
{
    public TextAsset XML_FILE;
    public TextAsset XML_FILE_WAVE1;
    public TextAsset XML_FILE_WAVE2;
    public Transform Roboy;

    bool whichPose = true;

    public void Start()
    {       
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            GetInitParameters(1);
        if (Input.GetKeyDown(KeyCode.O))
            GetInitParameters(2);
    }

    public void GetInitParameters(int poseCode)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(XML_FILE_WAVE1.text);

        switch (poseCode)
        {
            case 0:
                xmlDoc.LoadXml(XML_FILE.text);
                //Debug.Log("XML_FILE");
                break;
            case 1:
                xmlDoc.LoadXml(XML_FILE_WAVE1.text);
                //Debug.Log("XML_FILE2");
                break;
            case 2:
                xmlDoc.LoadXml(XML_FILE_WAVE2.text);
                //Debug.Log("XML_FILE2");
                break;
            default:
                break;
        }

        foreach (Transform t in Roboy)
        {
            if (t != null & t.CompareTag("RoboyPart"))
            {               
                XmlNode node = xmlDoc.SelectSingleNode("/sdf/model/link[@name='" + t.name + "']/pose");

                string[] poseString = node.InnerText.Split(null);

                float x = float.Parse(poseString[0]);
                float y = float.Parse(poseString[1]);
                float z = float.Parse(poseString[2]);

                float alpha = float.Parse(poseString[3]);
                float beta = float.Parse(poseString[4]);
                float gamma = float.Parse(poseString[5]);

                Vector3 pos = new Vector3(x, y, z);
                Quaternion q = Quaternion.Euler(new Vector3(alpha, beta, gamma));

                // Conversion between ROS Quaternions/Point and Unity Quaternion/Point
                RosSharp.RosBridgeClient.Messages.Geometry.Quaternion or = new RosSharp.RosBridgeClient.Messages.Geometry.Quaternion();
                or.x = q.x;
                or.y = q.y;
                or.z = q.z;
                or.w = q.w;
                RosSharp.RosBridgeClient.Messages.Geometry.Point po = new RosSharp.RosBridgeClient.Messages.Geometry.Point();
                po.x = pos.x;
                po.y = pos.y;
                po.z = pos.z;

                //Create Message
                RosSharp.RosBridgeClient.Messages.Roboy.Pose message = new RosSharp.RosBridgeClient.Messages.Roboy.Pose();
                message.orientation = or;
                message.position = po;
                switch (t.name)
                {
                    case "upper_arm_right":
                        message.id = 0;
                        break;
                    case "forearm_right":
                        message.id = 1;
                        break;
                    case "hand_right":
                        message.id = 2;
                        break;
                    case "elbow_right":
                        message.id = 3;
                        break;
                    case "upper_arm_left":
                        message.id = 4;
                        break;
                    case "forearm_left":
                        message.id = 5;
                        break;
                    case "hand_left":
                        message.id = 6;
                        break;
                    case "elbow_left":
                        message.id = 7;
                        break;
                    case "head":
                        message.id = 8;
                        break;                    
                    default:
                        continue;
                }
                gameObject.GetComponent<MockPosePublisher>().PublishMessage(message);
            }
        }
    }

}