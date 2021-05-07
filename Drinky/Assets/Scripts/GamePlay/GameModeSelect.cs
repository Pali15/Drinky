using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeSelect : MonoBehaviour
{

    /// <summary>
    /// pointer to this
    /// </summary>
    public static GameModeSelect _instance;

    /// <summary>
    /// array of gamemodes
    /// </summary>
    public GameMode[] gameModes;

    /// <summary>
    /// the current gamemode
    /// </summary>
    public GameMode CurrentGameMode;

    public GameObject GameModeSelector;

    public GameObject MainMenu;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// loads a gamemode with the given index
    /// </summary>
    /// <param name="index">index</param>
    public void LoadGameMode(int index)
    {
        if (CurrentGameMode != null)//if there is an active game mode
        {
            Destroy(CurrentGameMode.gameObject);
        }

        if (index > gameModes.Length - 1)//if there is no game mode
            return;

        CurrentGameMode = Instantiate(gameModes[index], Vector3.zero, Quaternion.identity);//instantiating the game mode
    }

    public void startBusDriver()
    {
        GameModeSelector.SetActive(false);
        MainMenu.SetActive(false);
        LoadGameMode(0);
    }

    public void startRussianRoulette()
    {
        GameModeSelector.SetActive(false);
        MainMenu.SetActive(false);
        LoadGameMode(1);
    }
    
    public void startWhatsInThePocket()
    {
        GameModeSelector.SetActive(false);
        MainMenu.SetActive(false);
        LoadGameMode(2);
    }
    

    public void Exit()
    {
        GameModeSelector.SetActive(false);
        MainMenu.SetActive(true);
    }

    
}


