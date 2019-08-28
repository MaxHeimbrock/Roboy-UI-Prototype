using System.Collections;
using System.Collections.Generic;
using Leap;
using UnityEngine;

public class PC_Mesh : MonoBehaviour {
    private Mesh mesh;

    /// <summary>
    /// Renders the given set of points on a mesh
    /// </summary>
    /// <param name="pointcloudPoints">given points, not more than 65535</param>
    public void renderMesh(List<Vector3> pointcloudPoints) {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        Vector3[] points = new Vector3[pointcloudPoints.Count];
        int[] indecies = new int[pointcloudPoints.Count];
        Color[] colors = new Color[pointcloudPoints.Count];
        for (int i = 0; i < points.Length; ++i) {
            points[i] = pointcloudPoints[i];
            indecies[i] = i;
            colors[i] = getColorForPoint(pointcloudPoints[i]);
        }

        mesh.vertices = points;
        mesh.colors = colors;
        mesh.SetIndices(indecies, MeshTopology.Points, 0);
    }

    /// <summary>
    /// Calculates a color for one point of a point cloud depening on its distance to the origin.
    /// You max change inValueMax in order to change, how fast the colors are lerping.
    /// </summary>
    /// <param name="point">the point for which to calculate the color</param>
    /// <returns>a color object</returns>
    Color getColorForPoint(Vector3 point) {
        float lerpFactor = mapValue(Vector3.Distance(Vector3.zero, point), 0, 50, 0f, 1f);
        Color lerpColor = Color.Lerp(Color.cyan, Color.red, lerpFactor);
        return lerpColor;
    }
    
    /// <summary>
    /// Maps values to a new range
    /// </summary>
    /// <param name="mainValue">the value to be mapped to a new range</param>
    /// <param name="inValueMin">minimum possible value of input</param>
    /// <param name="inValueMax">maximum possible value of input</param>
    /// <param name="outValueMin">minimum possible value of output</param>
    /// <param name="outValueMax">maximum possible value of output</param>
    /// <returns></returns>
    float mapValue(float mainValue, float inValueMin, float inValueMax, float outValueMin, float outValueMax) {
        return (mainValue - inValueMin) * (outValueMax - outValueMin) / (inValueMax - inValueMin) + outValueMin;
    }
}