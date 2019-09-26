using UnityEngine;
using System.Collections;
using System.Xml;
using System;

/// <summary>
/// Gets the current pose of the Unity Model.
/// </summary>
public class GetPose : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Set the Roboy model to be updated.")]
    private Transform Roboy;
    [SerializeField]
    [Tooltip("Set the XML file where the pose should be saved.")]
    private TextAsset XML_FILE;

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
