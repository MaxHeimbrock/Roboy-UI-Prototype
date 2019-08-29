using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [Header("Select Interaction Techniques")]
    [Tooltip("Technique for pointing.")]
    public PointerTechnique pointerTechnique;
    /*
    [Tooltip("Technique for Clicking.")]
    public ClickerTechnique clickerTechnique;

    [Header("Dwell Time Settings")]
    [Tooltip("Change the time till selection.")]
    public float dwellTime = 1.5f;
    public Image dwellTimeIndicator;

    [Header("Debugging Settings")]
    [Tooltip("Shows the pointers position.")]
    public Texture indicatorTexture;
    private RectTransform canvasTransform;
    */

    private Camera cam;
    private AudioSource clickSound;
    private RaycastManager raycastManager;

    private Pointer pointer;
    /*
    private Clicker clicker;
    private UI_Element elementLookingAt;
    private bool isLookingAt = false;
    */

    private Vector2 pointerPos;

    public enum PointerTechnique { PointerMouse, PointerEye, PointerViveController, PointerSenseGlove};
    public enum ClickerTechnique { ClickerMouse, ClickerDwellTime, ClickerWink, ClickerBlink, ClickerSound };

    #region Setup

    public void Start()
    {
        CreatePointer();
        //CreateClicker();
        cam = Camera.main;
        clickSound = this.GetComponent<AudioSource>();
        raycastManager = this.GetComponent<RaycastManager>();
        //SubscribeToRaycastManager();
        //canvasTransform = GameObject.Find("DwellTimeIndicatorCanvas").GetComponent<RectTransform>();
    }

    #endregion

    public void Update()
    {
        /*
        CurvedUIInputModule.CustomControllerButtonState = false;
        */                
    }

    // Gets the position and rotation of the pointer and passes it to the curved UI input module
    public void Point(Vector3 position, Vector3 rotation)
    {
        this.transform.position = position;
        this.transform.rotation = Quaternion.Euler(rotation);

        // Send pointer information to Curved UI Input Module to use event system
        CurvedUIInputModule.CustomControllerRay = new Ray(this.transform.position, this.transform.forward);

        // this draws the ray - the rest of the raycast Manager is not being used
        raycastManager.GetRaycastHit(position, rotation);
    }

    public void Click(int code)
    {
        /*
        CurvedUIInputModule.CustomControllerButtonState = true;

        //Debug.Log("Click");
        clickSound.Play();

        if (isLookingAt == true && elementLookingAt is IClickable)
        {
            //Debug.Log("Clicked at " + elementLookingAt.name);
            ((IClickable)elementLookingAt).Click();
        }
        */
    }

    
    // Creates Pointer script for the pointer technique selected.
    private void CreatePointer()
    {
        switch (pointerTechnique)
        {
            case PointerTechnique.PointerMouse:
                pointer = this.gameObject.AddComponent<PointerMouse>();
                return;

            case PointerTechnique.PointerEye:
                pointer = this.gameObject.AddComponent<PointerEye>();
                return;

            case PointerTechnique.PointerViveController:
                pointer = this.gameObject.AddComponent<PointerViveController>();
                return;
            case PointerTechnique.PointerSenseGlove:
                pointer = this.gameObject.AddComponent<PointerSenseGlove>();
                break;
            default:
                throw new System.Exception("No pointer technique specified.");
        }
    }

    /*

    // Creates Clicker script for the clicker technique selected.
    private void CreateClicker()
    {
        switch (clickerTechnique)
        {
            case ClickerTechnique.ClickerMouse:
                clicker = this.gameObject.AddComponent<ClickerMouse>();
                return;

            case ClickerTechnique.ClickerDwellTime:
                clicker = this.gameObject.AddComponent<ClickerDwellTime>();
                ((ClickerDwellTime)clicker).SetDwellTime(dwellTime);
                return;

            case ClickerTechnique.ClickerWink:
                clicker = this.gameObject.AddComponent<ClickerWink>();
                return;

            case ClickerTechnique.ClickerBlink:
                break;

            case ClickerTechnique.ClickerSound:
                break;

            default:
                throw new System.Exception("No clicker technique specified.");
        }

        throw new System.Exception("Not implemented yet.");
    }

    public void ReceivePushNotification(RaycastHit hit, bool isHit)
    {
        if (isHit == true)
        {
            isLookingAt = true;
            //print("UI_Manager is looking at " + hit.transform.name);

            elementLookingAt = hit.transform.gameObject.GetComponent<UI_Element>();

            // Moves indicator canvas to hitpoint for dwell time indicator
            canvasTransform.position = hit.point;
            canvasTransform.rotation = hit.transform.rotation;
            // Moves it to the front a little bit to be seen better
            canvasTransform.localPosition += new Vector3(0, 0, -0.1f);
        }
        else
        {
            isLookingAt = false;
        }
    }

    public void SubscribeToRaycastManager()
    {
        raycastManager.Subscribe(this);
    }

    
    public void OnGUI()
    {
        // Draw pointer indicator
        GUI.DrawTexture(new Rect(pointerPos.x, (cam.pixelHeight - pointerPos.y), 10, 10), indicatorTexture);        
    }

    public Image GetDwellTimeIndicator()
    {
        return dwellTimeIndicator;
    }
    */
}
