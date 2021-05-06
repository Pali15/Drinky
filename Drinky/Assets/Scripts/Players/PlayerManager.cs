using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    /// <summary>
    /// Pointer to itself
    /// </summary>
    public static PlayerManager _instance;

    /// <summary>
    /// List of names
    /// </summary>
    private List<string> names;

    /// <summary>
    /// Index of the current player
    /// </summary>
    public int CurrentPlayerIdx
    {
        get;
        private set;
    }

    /// <summary>
    /// Number of players
    /// </summary>
    public int PlayersNum
    {
        get;
        private set;
    }

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        names=new List<string>();
        CurrentPlayerIdx = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /// <summary>
    /// returns the player's name with the index
    /// </summary>
    /// <param name="index">index of the player</param>
    /// <returns>player's name</returns>
    public string getName(int index)
    {
        if (index < 0 || index >= names.Count) return null;

        return names[index];
    }

    /// <summary>
    /// Sets the player's name with the index, if the name string is empty it will generate a name
    /// </summary>
    /// <param name="index">Index of the player</param>
    /// <param name="name">New name of the player</param>
    public void setName(int index, string name)
    {
        if (name.Length == 0)
        {
            names[index] = "Player " + (index + 1).ToString();
        }
        else
        {
            names[index] = name;
        }
    }

    /// <summary>
    /// returns the next player's name
    /// </summary>
    /// <returns>next player's name</returns>
    public string NextPlayer()
    {
        if (CurrentPlayerIdx < PlayersNum - 1 && PlayersNum != 1)
        {
            return names[++CurrentPlayerIdx];
        }
        else
        {
            CurrentPlayerIdx = 0;
            return names[CurrentPlayerIdx];
        } 
    }

    /// <summary>
    /// Adds a player
    /// </summary>
    /// <param name="name"></param>
    public void AddPlayer(string name)
    {
        PlayersNum++;
        names.Add(name);
    }

    /// <summary>
    /// Removes the player with the idx index
    /// </summary>
    /// <param name="idx">the index</param>
    public void RemovePlayer(int idx)
    {
        names.RemoveAt(idx);
        PlayersNum--;
    }

    /// <summary>
    /// Gets the current player's name
    /// </summary>
    /// <returns>The current player's name</returns>
    public string getCurrentPlayer()
    {
        return names[CurrentPlayerIdx];
    }
}
