using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    private Collider[] colliders;
    // Start is called before the first frame update
    void Start()
    {
        colliders = this.GetComponents<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("ActiveArea"))
        {
            Debug.Log(this.name + " TriggerEnter detected");
            colliders[0].enabled = false;
            colliders[1].enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("ActiveArea"))
        {
            Debug.Log(this.name + " TriggerExit detected");
            colliders[0].enabled = true;
            colliders[1].enabled = false;
        }
    }
}
