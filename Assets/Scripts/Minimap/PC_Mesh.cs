using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Mesh : MonoBehaviour
{

    private Mesh mesh;

    public void renderMesh(List<Vector3> pointcloudPoints) {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        
        Vector3[] points = new Vector3[pointcloudPoints.Count];
        int[] indecies = new int[pointcloudPoints.Count];
        Color[] colors = new Color[pointcloudPoints.Count];
        for (int i = 0; i < points.Length; ++i) {
            points[i] = pointcloudPoints[i];
            indecies[i] = i;
            //colors[i] = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
            colors[i] = new Color(255, 255, 255);
        }

        mesh.vertices = points;
        mesh.colors = colors;
        mesh.SetIndices(indecies, MeshTopology.Points, 0);
    }
}
