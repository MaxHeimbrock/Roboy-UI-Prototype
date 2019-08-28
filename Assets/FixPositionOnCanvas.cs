using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPositionOnCanvas : MonoBehaviour
{
    private Menu menu;

    private bool fixRotation = false;

    private Transform myTransform;

    public GameObject dummy;

    public GameObject dummyCanvas;

    private GameObject instDummy;

    // Start is called before the first frame update
    void Start()
    {
        menu = this.GetComponent<Menu>();
        myTransform = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Pointed " + menu.GetPointed());
        //Debug.Log("Fixed " + fixRotation);

        
        if (menu.GetPointed() != fixRotation)
        {
            fixRotation = !fixRotation;

            Debug.Log("Changed follow rotation to " + fixRotation);
            
            if (fixRotation == true)
                FreezeRotation();

            else if (fixRotation == false)
                UnfreezeRotation();            
        }

        if (fixRotation)
        {
            Debug.Log("Copy Transform");
            myTransform.position = instDummy.transform.position;
            myTransform.rotation = instDummy.transform.rotation;
        }
        
        // freeze rotation
        if (Input.GetKeyDown(KeyCode.M))
        {
            FreezeRotation();            
        }

        // unfreeze rotation
        if (Input.GetKeyDown(KeyCode.N))
        {
            UnfreezeRotation();
        }        
    }

    private void FreezeRotation()
    {
        fixRotation = true;
        instDummy = Instantiate(dummy, myTransform.position, myTransform.rotation, dummyCanvas.transform);
    }

    private void UnfreezeRotation()
    {
        fixRotation = false;
        Destroy(instDummy);
    }
}
