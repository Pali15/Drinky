using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager _instance;
    
    /// <summary>
    /// Main menu
    /// </summary>
    [SerializeField] private GameObject Menu;

    /// <summary>
    /// Panel where we can select the game mode
    /// </summary>
    [SerializeField] private GameObject GameModeSelector;


    private void Awake()
    {
        _instance = this;
    }

    /// <summary>
    /// Exists from the Game mode selector
    /// </summary>
    public void ExitGameModeSelector()
    {
        GameModeSelector.SetActive(false);
        Menu.SetActive(true);
    }

    /// <summary>
    /// Exists from the current game mode
    /// </summary>
    public void ExitFromGame()
    {
        GameModeSelect._instance.CurrentGameMode.HideCards();

        Destroy(GameModeSelect._instance.CurrentGameMode.gameObject);
        GameModeSelect._instance.CurrentGameMode = null;
        
        Menu.SetActive(true);
    }

    /// <summary>
    /// Goes to the game mode selector from main menu
    /// </summary>
    public void MainMenuPlayButton()
    {
        bool canStart = PlayersInput._instance.MainMenuPlayButton();
        GameModeSelector.SetActive(canStart);
        Menu.SetActive(!canStart);
    }

}
