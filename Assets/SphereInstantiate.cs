using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereInstantiate : MonoBehaviour
{
    public GameObject spherePrefab;
    PointCloudSubscriber p;

    private Mesh mesh;


    // Start is called before the first frame update
    void Start()
    {
        // Find ROS Connection in order to access point data
        GameObject g = GameObject.Find("ROS Connection");
        p = g.GetComponent<PointCloudSubscriber>();

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            createMesh();
        }
    }

    public void createMesh() {
        Vector3[] points = new Vector3[p.allSpheres.Count];
        int[] indecies = new int[p.allSpheres.Count];
        Color[] colors = new Color[p.allSpheres.Count];
        for (int i = 0; i < points.Length; ++i) {
            points[i] = p.allSpheres[i];
            indecies[i] = i;
            colors[i] = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
        }

        mesh.vertices = points;
        mesh.colors = colors;
        mesh.SetIndices(indecies, MeshTopology.Points, 0);
    }
}
