using UnityEngine;
using UnityEngine.SpatialTracking;

public class StateManager: Singleton<StateManager>
{
    public enum MenuState {HUD, transitionToAdvancedMenu, advancedMenu, transitionToHUD};

    public GameObject HUD;
    public GameObject AdvancedMenu;
    public GameObject Roboy;
    public GameObject MirroredPlayerPositionWithRoboy;
    //public GameObject SnapshotCamera;
    public GameObject HUD_Game_Objects;

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
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            // FROM HUD TO TRANSITION ////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            case MenuState.HUD:
                //SnapshotCamera.SetActive(false);
                HUD.SetActive(false);
                Roboy.SetActive(true);
                MirroredPlayerPositionWithRoboy.GetComponent<FollowTransform>().followPosition = false;
                MirroredPlayerPositionWithRoboy.GetComponent<FollowTransform>().followYRotation = false;

                // this is for all the positioning of the transition translation
                Vector3 previousPos = Camera.main.transform.localPosition;
                Quaternion previousRotation = Camera.main.transform.rotation;

                //Send trigger to VRPuppet           
                VRPuppetStateTransmissionServiceRequest.Instance.callService();               
                //Roboy.SetActive(true);
                Camera.main.GetComponent<TrackedPoseDriver>().trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
                Camera.main.GetComponent<TransitionHelper_SetPosition>().SetOffset(previousPos);
                Camera.main.GetComponent<TransitionHelper_SetPosition>().SetOffsetRotation(previousRotation);

                CameraAnimatorScript.Instance.StartTransitionToAdvancedMenu();
                // Vest
                GameObject.FindGameObjectWithTag("VestTransition").GetComponent<VestTransition>().playTact();
                break;

            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            // FROM TRANSITION TO ADVANCED MENU //////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            case MenuState.transitionToAdvancedMenu:
                HUD_Game_Objects.SetActive(false);
                Roboy.transform.localPosition = new Vector3(0, -0.4f, 0.9f);
                // Set Button not pointed here for safety
                CameraAnimatorScript.Instance.SetButtonNotPointed();
                // Set TransitionButton to not clicked to fix bug with mouse not moved. This will not be needed with eye tracking.
                TransitionButton.OnPointerExit(null);
                AdvancedMenu.SetActive(true);
                Camera.main.GetComponent<TrackedPoseDriver>().trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
                break;

            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            // FROM ADVANCED MENU TO TRANSITION //////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            case MenuState.advancedMenu:
                //SnapshotCamera.SetActive(true);
                AdvancedMenu.SetActive(false);
                CameraAnimatorScript.Instance.StartTransitionToHUD();

                // this is for all the positioning of the transition translation
                previousPos = Camera.main.transform.localPosition;
                previousRotation = MirroredPlayerPositionWithRoboy.transform.rotation;
                Camera.main.GetComponent<TrackedPoseDriver>().trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
                Camera.main.GetComponent<TransitionHelper_SetPosition>().SetOffset(previousPos);
                Camera.main.GetComponent<TransitionHelper_SetPosition>().SetOffsetRotation(previousRotation);
                break;

            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            // FROM TRANSITION TO HUD ////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            case MenuState.transitionToHUD:
                HUD_Game_Objects.SetActive(true);
                Roboy.SetActive(false);
                Roboy.transform.localPosition = new Vector3(0, -0.4f, -0.2f);
                Camera.main.GetComponent<TrackedPoseDriver>().trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
                MirroredPlayerPositionWithRoboy.GetComponent<FollowTransform>().followPosition = true;
                MirroredPlayerPositionWithRoboy.GetComponent<FollowTransform>().followYRotation = true;
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

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Camera.main.GetComponent<TrackedPoseDriver>().trackingType == TrackedPoseDriver.TrackingType.RotationOnly)
                Camera.main.GetComponent<TrackedPoseDriver>().trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
            else if (Camera.main.GetComponent<TrackedPoseDriver>().trackingType == TrackedPoseDriver.TrackingType.RotationAndPosition)
                Camera.main.GetComponent<TrackedPoseDriver>().trackingType = TrackedPoseDriver.TrackingType.RotationOnly;

            Debug.Log("Changed tracking to " + Camera.main.GetComponent<TrackedPoseDriver>().trackingType);
        }
    }

    public MenuState GetCurrentState()
    {
        return currentMenuState;
    }
}
