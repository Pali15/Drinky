using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaModeSelect : MonoBehaviour
{
    public GameObject GameModeSelector;

    public GameObject MainMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startBusDriver()
    {
        GameModeSelector.SetActive(false);
        MainMenu.SetActive(false);
        GameManager._instance.LoadGameMode(0);
    }

    public void startRussianRoulette()
    {
        GameModeSelector.SetActive(false);
        MainMenu.SetActive(false);
        GameManager._instance.LoadGameMode(1);
    }
    
    public void startWhatsInThePocket()
    {
        GameModeSelector.SetActive(false);
        MainMenu.SetActive(false);
        GameManager._instance.LoadGameMode(2);
    }
    

    public void Exit()
    {
        GameModeSelector.SetActive(false);
        MainMenu.SetActive(true);
    }

    
}


