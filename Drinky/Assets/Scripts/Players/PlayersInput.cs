using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayersInput : MonoBehaviour
{
    /// <summary>
    /// Pointer witch points to this
    /// </summary>
    public static PlayersInput _instance;
    
    /// <summary>
    /// InputField which gets instantiated when the class generates a new inputfield
    /// </summary>
    [SerializeField] private InputField inputField;
    
    /// <summary>
    /// List of inputfields
    /// </summary>
    private List<InputField> inputfields;
    
    /// <summary>
    /// content of the scroll view
    /// </summary>
    [SerializeField] private GameObject content;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        inputfields = new List<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Adds a new player with a new InputField
    /// </summary>
    public void AddPlayer()
    {
        if (PlayerManager._instance.PlayersNum < 20)
        {
            var newInput = Instantiate(inputField, content.transform);

            newInput.text = "";
            inputfields.Add(newInput);
            
            PlayerManager._instance.AddPlayer(newInput.text);
        }
    }

    /// <summary>
    /// Removes a player with the idx index
    /// </summary>
    /// <param name="idx">the index</param>
    public void RemovePlayer(int idx)
    {
        PlayerManager._instance.RemovePlayer(idx);//remove the name
        inputfields.RemoveAt(idx);//remove the inputfield

    }

    /// <summary>
    /// If the InputField text attribute is empty, it generates a name for that player, and sets it as the text of the inputfield
    /// called when the play button is pressed in the main menu
    /// </summary>
    public void MainMenuPlayButton()
    {
        if (inputfields.Count > 0)//if there is at least one player
        {
            for (int i = 0; i < inputfields.Count; i++)
            {
                if (inputfields[i].text.Length==0)//if the text is empty
                {
                    PlayerManager._instance.setName(i, "");//setting the players name
                    inputfields[i].text="Player " + (i+1).ToString();//setting the InputField text attribute
                }
            }
        }
    }

    /// <summary>
    /// return the index of the inputfield
    /// </summary>
    /// <param name="inputField">Inputfield</param>
    /// <returns>the index</returns>
    public int GetIdxOfInputfield(InputField inputField)
    {
        int index = 0;

        foreach (var b in inputfields)//iterating through the inputfields
        {
            if (b == inputField)
                return index;

            index++;
        }

        return -1;
    }

}
