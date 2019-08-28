using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Experimental.PlayerLoop;

public class PC_MeshManager : MonoBehaviour
{
    public GameObject meshPrefab;
    private List<PC_Mesh> allMeshes = new List<PC_Mesh>();
    private PointCloudSubscriber p;

    // Start is called before the first frame update
    void Start()
    {
        // Find ROS Connection in order to access point data
        GameObject g = GameObject.Find("ROS Connection");
        p = g.GetComponent<PointCloudSubscriber>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            removeMeshes();
            processPointCloudData();
        }
    }

    private void FixedUpdate()
    {
        removeMeshes();
        processPointCloudData();
    }

    void processPointCloudData() {
        //int numberOfMeshes = (p.allSpheres.Count / 40000) + 1; // ToDo: Rounding
        List<List<Vector3>> chunkedSpheres = splitList<Vector3>(p.allPoints.ToList(), 40000);

        for (int i = 0; i < chunkedSpheres.Count; i++) {
            GameObject newMeshObject = Instantiate(meshPrefab);
            PC_Mesh newMesh = newMeshObject.GetComponent<PC_Mesh>();
            newMesh.renderMesh(chunkedSpheres[i]);
        }
    }

    void removeMeshes()
    {
        GameObject[] toRemove = GameObject.FindGameObjectsWithTag("PointCloud_Mesh");
        foreach (GameObject _toRemove in toRemove)
        {
            GameObject.Destroy(_toRemove);
        }
    }

    List<List<T>> splitList<T>(List<T> locations, int nSize = 30) {
        var list = new List<List<T>>();

        for (int i = 0; i < locations.Count; i += nSize) {
            list.Add(locations.GetRange(i, Math.Min(nSize, locations.Count - i)));
        }

        return list;
    }
}
