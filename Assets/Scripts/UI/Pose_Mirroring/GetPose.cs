using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class GetPose : MonoBehaviour
{
    public Transform Roboy;
    public TextAsset XML_FILE;

    void Start()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(XML_FILE.text);

        foreach (Transform t in Roboy)
        {
            if (t != null & t.CompareTag("RoboyPart"))
            {
                Debug.Log(t.name);
                XmlNode node = xmlDoc.SelectSingleNode("/sdf/model/link[@name='" + t.name + "']/pose");
                string pose = ""+ t.transform.localPosition.x + " " + t.transform.localPosition.y + " " + t.transform.localPosition.z + " " + t.transform.localRotation.eulerAngles.x + " " + t.transform.localRotation.eulerAngles.y + " "+t.transform.localRotation.eulerAngles.z;
                node.InnerText = pose;               
                xmlDoc.Save("mock_pose.xml");
               
            }
        }
    }
}
