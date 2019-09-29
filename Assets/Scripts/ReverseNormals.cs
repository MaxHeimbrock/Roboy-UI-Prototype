using UnityEngine;
using System.Collections;

/// <summary>
/// This script lets you reverse normals. The normals will only be reversed in playmode so it will still look inverted when viewing in edit mode.
/// Usage: Attach the script to the mesh you want normals to be reversed
/// </summary>
[RequireComponent(typeof(MeshFilter))]
public class ReverseNormals : MonoBehaviour
{

    void Start()
    {
        MeshFilter filter = GetComponent(typeof(MeshFilter)) as MeshFilter;
        if (filter != null)
        {
            Mesh mesh = filter.mesh;

            Vector3[] normals = mesh.normals;
            for (int i = 0; i < normals.Length; i++)
                normals[i] = -normals[i];
            mesh.normals = normals;

            for (int m = 0; m < mesh.subMeshCount; m++)
            {
                int[] triangles = mesh.GetTriangles(m);
                for (int i = 0; i < triangles.Length; i += 3)
                {
                    int temp = triangles[i + 0];
                    triangles[i + 0] = triangles[i + 1];
                    triangles[i + 1] = temp;
                }
                mesh.SetTriangles(triangles, m);
            }
        }
    }
}
