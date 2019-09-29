using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowClosestPoint : MonoBehaviour
{
    [Tooltip("Specify the location you want to find the closest point at the collider for.")]
    public Vector3 location;

    /// <summary>
    /// Draws two spheres. One at the given "location"
    /// and one wire sphere at the closest point of the collider to the given "location". 
    /// </summary>
    public void OnDrawGizmos()
    {
        var collider = GetComponent<Collider>();

        if (!collider)
        {
            return; // nothing to do without a collider
        }

        Vector3 closestPoint = collider.ClosestPoint(location);

        Gizmos.DrawSphere(location, 0.1f);
        Gizmos.DrawWireSphere(closestPoint, 0.1f);
    }
}
