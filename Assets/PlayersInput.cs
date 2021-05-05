using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayersInput : MonoBehaviour
{
    public static PlayersInput _instance;
    
    [SerializeField] private InputField inputField;
    
    public List<InputField> inputfields;
    
    [SerializeField] private GameObject menu;
    [SerializeField] GameObject GameModeSelector;

    [SerializeField] private GameObject content;

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
    public void AddPlayer()
    {
        if (GameManager._instance.playersNum < 20)
        {
            GameManager._instance.playersNum++;
            var newInput = Instantiate(inputField, content.transform);

            newInput.text = "";
            inputfields.Add(newInput);
            
            PlayerManager._instance.names.Add(newInput.text);
        }
    }

    public void StartButton()
    {
        if (GameManager._instance.playersNum > 0)
        {
            for (int i = 0; i < GameManager._instance.playersNum; i++)
            {
                if (PlayerManager._instance.names[i] == "")
                {
                    PlayerManager._instance.names[i] = "Player " + (i+1).ToString();
                    inputfields[i].text="Player " + (i+1).ToString();
                }
            }
            
            GameModeSelector.SetActive(true);
            menu.SetActive(false);
        }

        PlayerManager._instance.currentPlayer = 0;

    }
}
