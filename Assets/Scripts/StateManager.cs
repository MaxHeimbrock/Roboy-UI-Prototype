using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;

public class StateManager: Singleton<StateManager>
{
    public enum MenuState {HUD, transitionToAdvancedMenu, advancedMenu, transitionToHUD};

    public GameObject HUD;
    public GameObject AdvancedMenu;
    public GameObject Roboy;

    [Header("Just a helper if pointing with mouse")]
    [Tooltip("When the mouse is not moved after transition, the transition button is still in clicked state. Reference for resetting the state to not clicked.")]
    public OUI_Button TransitionButton;
    
    public MenuState currentMenuState = MenuState.HUD;
    
    public void GoToState(MenuState menuState)
    {
        currentMenuState = menuState;
    }

    public void GoToNextState()
    {
        switch (currentMenuState)
        {
            // From HUD to Transition
            case MenuState.HUD:
                HUD.SetActive(false);
                //Roboy.SetActive(true);
                Camera.main.GetComponent<TrackedPoseDriver>().trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
                Roboy.GetComponent<RoboyPositioning>().followCamera = false;
                CameraAnimatorScript.Instance.StartTransitionToAdvancedMenu();
                break;
            // From Transition to Advanced Menu
            case MenuState.transitionToAdvancedMenu:
                // Set Button not pointed here for safety
                CameraAnimatorScript.Instance.SetButtonNotPointed();
                // Set TransitionButton to not clicked to fix bug with mouse not moved. This will not be needed with eye tracking.
                TransitionButton.OnPointerExit(null);
                AdvancedMenu.SetActive(true);
                break;
            // From Advanced Menu to Transition
            case MenuState.advancedMenu:
                AdvancedMenu.SetActive(false);
                CameraAnimatorScript.Instance.StartTransitionToHUD();
                break;
            // From Transition to HUD
            case MenuState.transitionToHUD:
                //Roboy.SetActive(false);
                Camera.main.GetComponent<TrackedPoseDriver>().trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
                Roboy.GetComponent<RoboyPositioning>().followCamera = true;
                HUD.SetActive(true);
                break;
        }

        // Go to next state, transitionToHUD is last state so loop around
        if (currentMenuState != MenuState.transitionToHUD)
            currentMenuState++;
        else
            currentMenuState = MenuState.HUD;

        Debug.Log("Changed state to " + currentMenuState);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StateManager.Instance.GoToNextState();
        }
    }

    public MenuState GetCurrentState()
    {
        return currentMenuState;
    }
}
