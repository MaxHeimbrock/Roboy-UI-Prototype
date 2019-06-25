using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHand : MonoBehaviour
{
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Successfully attached to " + this.gameObject.name);
        speed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Input.GetAxis("Vertical"), Input.GetAxis("Top"), Input.GetAxis("Horizontal"));
        Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Top"), Input.GetAxis("Vertical"));
        transform.position += Movement * speed * Time.deltaTime;
    }
}
