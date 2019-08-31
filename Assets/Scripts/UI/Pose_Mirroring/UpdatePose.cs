using UnityEngine;
using System.Xml;

public class UpdatePose : MonoBehaviour
{
    public TextAsset XML_FILE;

    void Awake()
    {
        getInitParameters();
    }

    private void getInitParameters()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(XML_FILE.text);

        XmlNode node = xmlDoc.SelectSingleNode("/sdf/model/link[@name='" + gameObject.name + "']/pose");

        string[] poseString = node.InnerText.Split(null);

        float x = float.Parse(poseString[0]);
        float y = float.Parse(poseString[1]);
        float z = float.Parse(poseString[2]);

        float alpha = float.Parse(poseString[3]);
        float beta = float.Parse(poseString[4]);
        float gamma = float.Parse(poseString[5]);

        Vector3 pos = new Vector3(x, y, z);
        Quaternion q = Quaternion.Euler(new Vector3(alpha, beta, gamma));

        // TESTING: Direct transform without ROS
        //transform.localPosition = pos;
        //transform.localRotation = q;
    }
}