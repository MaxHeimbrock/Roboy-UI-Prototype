using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour, IRaycastSubscriber
{
    [Header("Select Interaction Techniques")]
    [Tooltip("Technique for pointing.")]
    public PointerTechnique pointerTechnique;
    [Tooltip("Technique for Clicking.")]
    public ClickerTechnique clickerTechnique;

    [Header("Dwell Time Settings")]
    [Tooltip("Change the time till selection.")]
    public double dwellTime = 2;

    [Header("Debugging Settings")]
    [Tooltip("Shows the pointers position.")]
    public Texture indicatorTexture;
    public int indicatorSize = 10;

    private Camera cam;
    private AudioSource clickSound;
    private RaycastManager raycastManager;

    private Pointer pointer;
    private Clicker clicker;

    private Vector2 pointerPos;

    public enum PointerTechnique { PointerMouse, PointerEye };
    public enum ClickerTechnique { ClickerMouse, ClickerDwellTime, ClickerWink, ClickerBlink, ClickerSound };

    #region Setup

    public void Start()
    {
        CreatePointer();
        CreateClicker();
        cam = Camera.main;
        clickSound = this.GetComponent<AudioSource>();
        raycastManager = this.GetComponent<RaycastManager>();
        SubscribeToRaycastManager();
    }

    #endregion

    public void Update()
    {

    }

    public void Point(Vector2 pos)
    {
        pointerPos = pos;
        
        raycastManager.GetRaycastHit(pos);
    }

    public void Click(int code)
    {
        Debug.Log("Click");
        clickSound.Play();
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

            default:
                throw new System.Exception("No pointer technique specified.");
        }
    }

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
            //print("UI_Manager is looking at " + hit.transform.name);
        }
    }

    public void SubscribeToRaycastManager()
    {
        gameObject.GetComponent<RaycastManager>().Subscribe(this);
    }

    public void OnGUI()
    {
        GUI.DrawTexture(new Rect(pointerPos.x, (cam.pixelHeight - pointerPos.y), 10, 10), indicatorTexture);
        //GUI.DrawTexture(new Rect(pointerPos.x - (indicatorSize / 2.0f), (cam.pixelHeight - (pointerPos.y - (indicatorSize / 2.0f))), 10, 10), indicatorTexture);
    }
}
