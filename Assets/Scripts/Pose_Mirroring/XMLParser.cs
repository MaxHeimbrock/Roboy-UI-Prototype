using UnityEngine;
using System.Xml;
/// <summary>
/// This classes parses the SDF XML file containing the position and orientation of Roboy's parts. These are needed in order to reconstruct poses.
/// </summary>
public class XMLParser : MonoBehaviour
{
    public TextAsset XML_FILE;

    void Awake()
    {
        getInitParameters();
    }
    /// <summary>
    /// Gets the init parameters from the file and updates the attached game object.
    /// </summary>
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

        transform.localPosition = gazeboPositionToUnity(pos);
        transform.localRotation = gazeboRotationToUnity(q);
    }
    /// <summary>
    /// Convert Gazebos rotation to Unity.
    /// </summary>
    /// <returns>The rotation to unity.</returns>
    /// <param name="gazeboRot">Gazebo rotation</param>
    Quaternion gazeboRotationToUnity(Quaternion gazeboRot)
    {
        Quaternion rotX = Quaternion.AngleAxis(180f, Vector3.right);
        Quaternion rotZ = Quaternion.AngleAxis(180f, Vector3.forward);     
        Quaternion tempRot = new Quaternion(-gazeboRot.x, -gazeboRot.z, -gazeboRot.y, gazeboRot.w);
        Quaternion finalRot = gazeboRot * rotZ * rotX;
        return finalRot;
    }

    /// <summary>
    /// Convert Gazebo rotation to Unity.
    /// </summary>
    /// <returns>The position to unity.</returns>
    /// <param name="gazeboPos">Gazebo position</param>
    Vector3 gazeboPositionToUnity(Vector3 gazeboPos)
    {
        return new Vector3(gazeboPos.x, gazeboPos.z, gazeboPos.y);
    }
}