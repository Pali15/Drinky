﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    public static GameMode _instance;

    /// <summary>
    /// Intro Panel
    /// </summary>
    [SerializeField] private GameObject Intro;

    /// <summary>
    /// Game panel
    /// </summary>
    [SerializeField] private GameObject GamePlay;

    /// <summary>
    /// the deck
    /// </summary>
    [SerializeField]protected Deck deck;


    /// <summary>
    /// number of pulled cards
    /// </summary>
    protected int pulledCards = 0;

    #region texts
    [SerializeField] protected Text nameText;
    [SerializeField] protected Text cardNumText;
    [SerializeField] protected Text shotsText;
    [SerializeField] protected Text cardsClearedText;
    [SerializeField] protected Text gameEndText;
    #endregion

    [SerializeField] protected GameObject inGameButtons;
    [SerializeField] protected GameObject continueButton;

    // Start is called before the first frame update
    IEnumerator Start() {
        yield return new WaitForEndOfFrame();
    }

    /// <summary>
    /// initalizing the gamemode
    /// </summary>
    /// <param name="b">the first card will be visible if it is true</param>
    public virtual void Init(bool b)
    {
        deck.Init(b, null);
    }

    public virtual void CheckGameEnd()
    {
        
    }
    
    public void Exit()
    {
        ButtonManager._instance.ExitFromGame();
    }

    public virtual void StartGame()
    {
        Intro.SetActive(false);
        GamePlay.SetActive(true);
    }

    //sets the next player and refreshes the name text
    protected void NextPlayer()
    {
        nameText.text = PlayerManager._instance.NextPlayer();
    }

    /// <summary>
    /// pulls a card
    /// </summary>
    protected virtual void PullCard()
    {
        deck.PullCard();
        pulledCards++;
    }

    public void HideCards()
    {
        deck.HideCards();
    }
    /// <summary>
    /// Animates a text
    /// </summary>
    /// <param name="t">the duration of the animation</param>
    /// <param name="text">the text we animate</param>
    protected IEnumerator TextAnimation(float t, Text text)
    {
        text.gameObject.SetActive(true);

        var rate = 1 / t;
        float i = 1;
        while (i > 0)
        {
            i -= Time.deltaTime * rate;
            text.color = new Color(text.color.r, text.color.g, text.color.b, i);
            yield return null;
        }

        text.gameObject.SetActive(false);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
