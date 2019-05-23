using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [Header("Select Interaction Techniques")]
    [Tooltip("Technique for pointing.")]
    public PointerTechnique pointerTechnique;
    [Tooltip("Technique for Clicking.")]
    public ClickerTechnique clickerTechnique;

    [Header("Dwell Time Settings")]
    [Tooltip("Change the time till selection.")]
    public double dwellTime;

    [Header("Debugging Settings")]
    [Tooltip("Shows the pointers position.")]
    public GameObject indicator;

    private Camera cam;
    private AudioSource clickSound;

    private Pointer pointer;
    private Clicker clicker;

    private Vector2 pos;

    public enum PointerTechnique { PointerMouse, PointerEye };
    public enum ClickerTechnique { ClickerMouse, ClickerDwellTime, ClickerWink, ClickerBlink, ClickerSound };

    #region Setup

    public void Start()
    {
        CreatePointer();
        CreateClicker();
        cam = Camera.main;
        clickSound = gameObject.GetComponent<AudioSource>();
    }

    #endregion

    public void Update()
    {

    }

    public void Point(Vector2 pos)
    {
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 1));

        indicator.transform.position = point;
    }

    public void Click(int code)
    {
        Debug.Log("Click");
        clickSound.Play();
    }

    // Creates Pointer script for the pointer technique selected.
    private void CreatePointer()
    {
        if (pointer != null)
            DestroyImmediate(pointer);

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
        if (clicker != null)
            DestroyImmediate(clicker);

        switch (clickerTechnique)
        {
            case ClickerTechnique.ClickerMouse:
                clicker = this.gameObject.AddComponent<ClickerMouse>();
                return;

            case ClickerTechnique.ClickerDwellTime:
                clicker = this.gameObject.AddComponent<ClickerDwellTime>();
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

    private void OnValidate()
    {
        CreatePointer();
        CreateClicker();
    }
}
