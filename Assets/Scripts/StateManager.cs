using UnityEngine;
using UnityEngine.SpatialTracking;

/// <summary>
/// The StateManager handles the two menu states of HUD and advanced menu as well as the transition.
/// It activates and deactivates GameObjects, sends messages and triggers method calls.
/// We also use it as the single place to put debug input from keyboard.
/// </summary>
public class StateManager: Singleton<StateManager>
{
    public enum MenuState {HUD, transitionToAdvancedMenu, advancedMenu, transitionToHUD};

    [Header("The GameObjects needed share the name with the variable")]
    public GameObject HUD;
    public GameObject AdvancedMenu;
    public GameObject Roboy;
    public GameObject MirroredPlayerPositionWithRoboy;
    public GameObject HUD_Game_Objects;
    public GameObject rosManager;

    [Header("Just a helper if pointing with mouse")]
    [Tooltip("When the mouse is not moved after transition, the transition button is still in clicked state. Reference for resetting the state to not clicked.")]
    public OUI_Button TransitionButton;
    
    public MenuState currentMenuState = MenuState.HUD;
    
    /// <summary>
    /// This method changes the current state to the passed state.
    /// </summary>
    /// <param name="menuState">the state to go to</param>
    public void GoToState(MenuState menuState)
    {
        currentMenuState = menuState;
    }

    /// <summary>
    /// This method is called from outside to trigger transition of states. 
    /// </summary>
    public void GoToNextState()
    {
        switch (currentMenuState)
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            // FROM HUD TO TRANSITION ////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            case MenuState.HUD:
                HUD.SetActive(false);

                // Activate and freeze Roboy
                Roboy.SetActive(true);
                MirroredPlayerPositionWithRoboy.GetComponent<FollowTransform>().followPosition = false;
                MirroredPlayerPositionWithRoboy.GetComponent<FollowTransform>().followYRotation = false;

                // this is for all the positioning of the transition translation
                Vector3 previousPos = Camera.main.transform.localPosition;
                Quaternion previousRotation = Camera.main.transform.rotation;

                //Send trigger to VRPuppet           
                VRPuppetStateTransmissionServiceRequest.Instance.callService();

                // Freeze positional tracking while in transition
                Camera.main.GetComponent<TrackedPoseDriver>().trackingType = TrackedPoseDriver.TrackingType.RotationOnly;

                // Set offset position and rotation to move camera back in local z direction, even though tracking does not want it to move
                Camera.main.GetComponent<TransitionHelper_SetPosition>().SetOffset(previousPos);
                Camera.main.GetComponent<TransitionHelper_SetPosition>().SetOffsetRotation(previousRotation);

                // Camera transition animation triggered here
                CameraAnimatorScript.Instance.StartTransitionToAdvancedMenu();

                // Vest
                GameObject.FindGameObjectWithTag("VestTransition").GetComponent<VestTransition>().playTact();

                //Update Pose
                rosManager.GetComponent<UpdatePose>().GetInitParameters(0);
                rosManager.GetComponent<MockMotorStatusPublisher>().PublishMotorMessage();
                break;

            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            // FROM TRANSITION TO ADVANCED MENU //////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            case MenuState.transitionToAdvancedMenu:

                // Deactivates mock room, minimap ...
                HUD_Game_Objects.SetActive(false);

                // Set roboy in front of the camera after the transition, once the positional tracking starts again
                Roboy.transform.localPosition = new Vector3(0, -0.4f, 0.9f);

                // Set Button not pointed here for safety
                CameraAnimatorScript.Instance.SetButtonNotPointed();

                // Set TransitionButton to not clicked to fix bug with mouse not moved. This will not be needed with eye tracking.
                TransitionButton.OnPointerExit(null);

                // Fade in of advanced menu elements
                foreach(GameObject obj in GameObject.FindGameObjectsWithTag("SubMenu3D"))
                {
                    obj.transform.GetChild(2).GetComponent<SubMenuAnimationHandler>().FadeIn();
                }
                                
                Roboy.GetComponent<RotateRoboy>().enabled = true;

                // Normal tracking again for advanced menu
                Camera.main.GetComponent<TrackedPoseDriver>().trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
                break;

            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            // FROM ADVANCED MENU TO TRANSITION //////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            case MenuState.advancedMenu:
                Roboy.GetComponent<RotateRoboy>().enabled = false;

                // Fade out of advanced menu elements
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag("SubMenu3D"))
                {
                    obj.transform.GetChild(2).GetComponent<SubMenuAnimationHandler>().FadeOut();
                }
                // Camera transition animation triggered here
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
                // Deactivates mock room, minimap ...
                HUD_Game_Objects.SetActive(true);

                // Deactivate Roboy and let him follow the camera again
                Roboy.SetActive(false);
                MirroredPlayerPositionWithRoboy.GetComponent<FollowTransform>().followPosition = true;
                MirroredPlayerPositionWithRoboy.GetComponent<FollowTransform>().followYRotation = true;

                // Correct Roboy position offset from in front of the camera for the advanced menu to being behind the camera (deactivated) in HUD
                Roboy.transform.localPosition = new Vector3(0, -0.4f, -0.2f);

                // Normal tracking again
                Camera.main.GetComponent<TrackedPoseDriver>().trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;

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

    /// <summary>
    /// Here, hard coded debug input over the keyboard can be dumped.
    /// </summary>
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

    /// <summary>
    /// Returns the current state as a MenuState variable.
    /// </summary>
    /// <returns></returns>
    public MenuState GetCurrentState()
    {
        return currentMenuState;
    }

    /// <summary>
    /// Exits the whole Unity Play Mode
    /// </summary>
    public void ShutDownApplication()
    {
        Debug.Log("ShutDown: Bye Bye");
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
    }
}
