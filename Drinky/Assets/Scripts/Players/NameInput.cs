using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{

    /// <summary>
    /// In the end of editing the inputfield it sets name of the player
    /// </summary>
    public void OnEndEdit()
    {
       int i=PlayersInput._instance.GetIdxOfInputfield(this.GetComponent<InputField>());

       PlayerManager._instance.setName(i,this.GetComponent<InputField>().text);
    }

    /// <summary>
    /// removes itself
    /// </summary>
    public void Remove()
    {
        PlayersInput._instance.RemovePlayer(PlayersInput._instance.GetIdxOfInputfield(this.GetComponent<InputField>()));

        Destroy(gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {    
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
