using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager: Singleton<StateManager>
{
    public enum MenuState {HUD, transitionToAdvancedMenu, advancedMenu, transitionToHUD};

    private MenuState currentMenuState = MenuState.HUD;

    public void GoToState(MenuState menuState)
    {
        currentMenuState = menuState;
    }

    public void GoToNextState()
    {
        if (currentMenuState != MenuState.transitionToHUD)
            currentMenuState++;

        else
            currentMenuState = MenuState.HUD;

        Debug.Log("Changed state to " + currentMenuState);
    }
}
