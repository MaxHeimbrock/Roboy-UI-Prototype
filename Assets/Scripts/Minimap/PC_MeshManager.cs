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

    
    /// <summary>
    /// Called before first frame.
    /// Sets up variables in order to communicate with PointCloudSubscriber
    /// </summary>
    void Start()
    {
        // Find ROS Connection in order to access point data
        GameObject g = GameObject.Find("ROS Connection");
        p = g.GetComponent<PointCloudSubscriber>();
        
    }

    /// <summary>
    /// Updates the displayed point cloud at a fixed interval
    /// </summary>
    private void FixedUpdate()
    {
        removeMeshes();
        processPointCloudData();
    }

    /// <summary>
    /// Splits the pointcloud points into chunks of 40000 for performance reasons and because a mesh can only display 65535 points.
    /// Gives them to a specific mesh object for further processing.
    /// </summary>
    void processPointCloudData() {
        //int numberOfMeshes = (p.allSpheres.Count / 40000) + 1; // ToDo: Rounding
        List<List<Vector3>> chunkedSpheres = splitList<Vector3>(p.allPoints.ToList(), 40000);

        for (int i = 0; i < chunkedSpheres.Count; i++) {
            GameObject newMeshObject = Instantiate(meshPrefab);
            PC_Mesh newMesh = newMeshObject.GetComponent<PC_Mesh>();
            newMesh.renderMesh(chunkedSpheres[i]);
        }
    }

    /// <summary>
    /// Removes all displayed meshes (identified by tag)
    /// </summary>
    void removeMeshes()
    {
        GameObject[] toRemove = GameObject.FindGameObjectsWithTag("PointCloud_Mesh");
        foreach (GameObject _toRemove in toRemove)
        {
            GameObject.Destroy(_toRemove);
        }
    }

    /// <summary>
    /// Splits a list into chunks of nSize
    /// </summary>
    /// <param name="locations">initial list</param>
    /// <param name="nSize">size of chunks</param>
    /// <typeparam name="T">chunked list (list of lists)</typeparam>
    /// <returns></returns>
    List<List<T>> splitList<T>(List<T> locations, int nSize = 30) {
        var list = new List<List<T>>();

        for (int i = 0; i < locations.Count; i += nSize) {
            list.Add(locations.GetRange(i, Math.Min(nSize, locations.Count - i)));
        }

        return list;
    }
}
