using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    public LayerMask UI_Mask;
    public Color color;
    public float thickness = 0.002f;
    public float length = 100f;

    Camera cam;
    int width;
    int height;

    GameObject holder;
    GameObject pointer;
    GameObject cursor;

    Vector3 cursorScale = new Vector3(0.05f, 0.05f, 0.05f);
    float contactDistance = 0f;
    Transform contactTarget = null;

    List<IRaycastSubscriber> subscribers = new List<IRaycastSubscriber>();

    void Start()
    {
        cam = Camera.main;

        Material newMaterial = new Material(Shader.Find("Unlit/Color"));
        newMaterial.SetColor("_Color", color);

        holder = new GameObject();
        holder.transform.parent = this.transform;
        holder.transform.localPosition = Vector3.zero;

        // The raycast line is called pointer in this script and is actually a cube primitive 
        pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pointer.transform.parent = holder.transform;
        pointer.GetComponent<MeshRenderer>().material = newMaterial;

        pointer.GetComponent<BoxCollider>().isTrigger = true;
        pointer.AddComponent<Rigidbody>().isKinematic = true;
        pointer.layer = 2;

        SetPointerTransform(length, thickness);
    }

    public void GetRaycastHit(Vector3 position, Vector3 rotation)
    {
        // Set the Parent transform and rotation to the pointing device 
        this.transform.position = position;
        this.transform.rotation = Quaternion.Euler(rotation);

        // This is the raycast
        Ray raycast = new Ray(transform.position, transform.forward);

        RaycastHit hitObject;
        bool rayHit = Physics.Raycast(raycast, out hitObject, length, UI_Mask);
        //Debug.Log(rayHit);

        float beamLength = GetBeamLength(rayHit, hitObject);

        // This draws the line of the raycast
        SetPointerTransform(beamLength, thickness);

    }

    void Update()
    {
        
    }

    public void Subscribe(IRaycastSubscriber subscriber)
    {
        subscribers.Add(subscriber);
        //Debug.Log(subscriber.ToString() + " has subscribed to RaycastManager");
    }

    private void SendPushNotification(RaycastHit hit, bool isHit)
    {
        foreach (IRaycastSubscriber subscriber in subscribers)
        {
            subscriber.ReceivePushNotification(hit, isHit);
        }
    }

    void SetPointerTransform(float setLength, float setThicknes)
    {
        //if the additional decimal isn't added then the beam position glitches
        float beamPosition = setLength / (2 + 0.00001f);

        pointer.transform.localScale = new Vector3(setLength, setThicknes, setThicknes);
        pointer.transform.localPosition = new Vector3(beamPosition, 0f, 0f);            
      
        pointer.transform.localScale = new Vector3(setThicknes, setThicknes, setLength);
        pointer.transform.localPosition = new Vector3(0f, 0f, beamPosition);
    }

    float GetBeamLength(bool bHit, RaycastHit hit)
    {
        float actualLength = length;

        //reset if beam not hitting or hitting new target
        if (!bHit || (contactTarget && contactTarget != hit.transform))
        {
            contactDistance = 0f;
            contactTarget = null;
        }

        //check if beam has hit a new target
        if (bHit)
        {
            if (hit.distance <= 0)
            {

            }
            contactDistance = hit.distance;
            contactTarget = hit.transform;

            /*
             * New Code
             * */

            print("I'm looking at " + hit.transform.name);
            //hit.transform.gameObject.GetComponent<UI_Element>().Highlight();
            SendPushNotification(hit, true);
        }
        else
        {
            SendPushNotification(new RaycastHit(), false);
            //print("I'm looking at nothing!");
        }

        //adjust beam length if something is blocking it
        if (bHit && contactDistance < length)
        {
            actualLength = contactDistance;
        }

        if (actualLength <= 0)
        {
            actualLength = length;
        }

        return actualLength; ;
    }
}
