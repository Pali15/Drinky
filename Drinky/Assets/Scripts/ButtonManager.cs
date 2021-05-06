using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager _instance;
    
    [SerializeField] private GameObject Menu;

    [SerializeField] private GameObject GameModeSelector;

    [SerializeField] private GameObject BusDriver;

    [SerializeField] private GameObject RussianRoulette;


    private void Awake()
    {
        _instance = this;
    }

    public void ExitGameModeS()
    {
        GameModeSelector.SetActive(false);
        Menu.SetActive(true);
    }

    public void ExitFromGame()
    {
        GameManager._instance.CurrentGameMode.HideCards();

        Destroy(GameManager._instance.CurrentGameMode.gameObject);
        GameManager._instance.CurrentGameMode = null;
        
        Menu.SetActive(true);
    }

    public void MainMenuPlayButton()
    {
        GameModeSelector.SetActive(true);
        Menu.SetActive(false);
    }

}
