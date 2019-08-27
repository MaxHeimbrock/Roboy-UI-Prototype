using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereInstantiate : MonoBehaviour
{
    public GameObject spherePrefab;
    PointCloudSubscriber p;

    // Start is called before the first frame update
    void Start()
    {

        GameObject g = GameObject.Find("ROS Connection");
        p = g.GetComponent<PointCloudSubscriber>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            runInstantiate();
        }
    }

    public void doInstantiate(Vector3 position) {
        Instantiate(spherePrefab, position, Quaternion.identity);
    }

    void runInstantiate() {
        /*GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Sphere");

        for (var i = 0; i < gameObjects.Length; i++) {
            Destroy(gameObjects[i]);
        }*/


        //print(p.allSpheres.Length);

        for(int i = 0; i < p.allSpheres.Length; i++) {
            if(i % 20 == 0) {
                Instantiate(spherePrefab, p.allSpheres[i], Quaternion.identity);
            }
        }
    }
}
