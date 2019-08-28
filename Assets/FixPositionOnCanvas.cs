using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPositionOnCanvas : MonoBehaviour
{
    private Menu menu;

    public GameObject fixedRotationParent;

    public GameObject followingRotationParent;

    private bool fixRotation = true;

    private bool checkForChange = true;

    // Start is called before the first frame update
    void Start()
    {
        menu = this.GetComponent<Menu>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Pointed " + menu.GetPointed());

        if (menu.GetPointed() != fixRotation)// && checkForChange == true)
        {
            fixRotation = !fixRotation;

            checkForChange = false;

            //Debug.Log("Changed follow rotation to " + fixRotation);

            /*
            if (fixRotation == true)
                StartCoroutine(FixRotation());

            else if (fixRotation == false)
                StartCoroutine(FollowRotation());
            */
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            this.transform.SetParent(followingRotationParent.transform);
            fixRotation = !fixRotation;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            this.transform.SetParent(fixedRotationParent.transform);
            fixRotation = !fixRotation;
        }
    }

    IEnumerator FixRotation()
    {
        this.transform.SetParent(fixedRotationParent.transform);

        yield return new WaitForSeconds(1f);

        checkForChange = true;

        //Debug.Log("GO");
    }


    IEnumerator FollowRotation()
    {
        this.transform.SetParent(followingRotationParent.transform);

        yield return new WaitForSeconds(1f);

        checkForChange = true;

        //Debug.Log("GO");
    }
}
