using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager: Singleton<StateManager>
{
    public enum MenuState {HUD, transitionToAdvancedMenu, advancedMenu, transitionToHUD};

    public GameObject HUD;
    public GameObject Transition;
    public GameObject AdvancedMenu;
    public GameObject Roboy;
    private MusicManager music;

    private MenuState currentMenuState = MenuState.HUD;

    private void Start()
    {
        music = MusicManager.Instance;
    }

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
                Transition.SetActive(true);
                Roboy.SetActive(true);
                TransitionChangeState.Instance.StartTransitionToAdvancedMenu();
                break;
            // From Transition to Advanced Menu
            case MenuState.transitionToAdvancedMenu:
                Transition.SetActive(false);
                AdvancedMenu.SetActive(true);
                music.startMusic();
                break;
            // From Advanced Menu to Transition
            case MenuState.advancedMenu:
                music.stopMusic();
                AdvancedMenu.SetActive(false);
                Transition.SetActive(true);
                TransitionChangeState.Instance.StartTransitionToHUD();
                break;
            // From Transition to HUD
            case MenuState.transitionToHUD:
                Transition.SetActive(false);
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
}
