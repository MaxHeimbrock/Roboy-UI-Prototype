using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager: Singleton<StateManager>
{
    public enum MenuState {HUD, transitionToAdvancedMenu, advancedMenu, transitionToHUD};

    public GameObject HUD;
    public GameObject AdvancedMenu;
    public GameObject Roboy;
    
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
                Roboy.SetActive(true);                
                CameraAnimatorScript.Instance.StartTransitionToAdvancedMenu();
                break;
            // From Transition to Advanced Menu
            case MenuState.transitionToAdvancedMenu:
                // Set Button not pointed here for safety
                CameraAnimatorScript.Instance.SetButtonNotPointed();
                AdvancedMenu.SetActive(true);
                break;
            // From Advanced Menu to Transition
            case MenuState.advancedMenu:
                AdvancedMenu.SetActive(false);
                CameraAnimatorScript.Instance.StartTransitionToHUD();
                break;
            // From Transition to HUD
            case MenuState.transitionToHUD:
                Roboy.SetActive(false);
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
