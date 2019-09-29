using UnityEngine;
using System.Collections;
/// <summary>
/// The Roboy part script is responsible for controlling the individual anatomical Roboy parts.
/// </summary>
public class RoboyPart : MonoBehaviour
{
    public Vector3 Position;
    public Quaternion Rotation;

    public RoboyPart(Vector3 pos, Quaternion rot)
    {
        this.Position = pos;
        this.Rotation = rot;
    }
}
