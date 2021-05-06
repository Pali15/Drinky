using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RussianRoulette : GameMode
{
    /// <summary>
    /// true if the player stoppped
    /// </summary>
    [SerializeField] private bool stop;

    /// <summary>
    /// true if the player pulled a black card
    /// </summary>
    [SerializeField] private bool gameOver;

    /// <summary>
    /// number of cards in the deck
    /// </summary>
    [SerializeField] private int bullets = 6;

    /// <summary>
    /// the ingame deck
    /// </summary>
    [SerializeField] private PartialDeck partailDeck;


    public void Start()
    {
        cardNumText.text = bullets.ToString();
    }

    public void StartGame()
    {
        base.StartGame();
        Init(false);
    }

    /// <summary>
    /// Initalizes the game to start
    /// </summary>
    /// <param name="b"></param>
    public override void Init(bool b)
    {
        base.Init(b);

        SetCards();//setting the partialDeck

        //setting the texts
        nameText.text = PlayerManager._instance.getCurrentPlayer();
           
        cardsClearedText.text = bullets.ToString()+" / "+(pulledCards).ToString();;
        shotsText.text = (pulledCards).ToString();
    }
    
    /// <summary>
    /// Sets the inGameDeck
    /// </summary>
    void SetCards()
    {
        List<Card> inGameDeck = new List<Card>();//List of cards which are going to get in the partial deck

        //placeing a black card in the inGameDeck
        int blackCardIdx = Random.Range(0, 52);

        while (deck.cards[blackCardIdx].isRed)
        {
            blackCardIdx = Random.Range(0, 52);
        }

        inGameDeck.Add(deck.cards[blackCardIdx]);//adding the black card

        //filling the rest place with red cards
        for(int i=1; i<bullets; i++)
        {
            //choosing a new red card 
            int redCardIdx = Random.Range(0, 52);

            while((!deck.cards[redCardIdx].isRed || inGameDeck.Contains(deck.cards[redCardIdx])))
            {
                redCardIdx = Random.Range(0, 52);

            }

            inGameDeck.Add(deck.cards[redCardIdx]);
        }

        partailDeck.Init(true, inGameDeck);//initalizes the deck
        partailDeck.Shuffle();
    }

    /// <summary>
    /// Handles if the round is over
    /// </summary>
    protected void GameOver()
    {
        inGameButtons.SetActive(false);
        continueButton.SetActive(true);

        shotsText.text = (pulledCards+1).ToString();

        gameOver = false;
        stop = false;
        
    }

    /// <summary>
    /// Checks if the round is ended and handles that
    /// </summary>
    public override void CheckGameEnd()
    {
        base.CheckGameEnd();
        
        if (gameOver)//pulled a black card
        {
            gameEndText.text = PlayerManager._instance.getCurrentPlayer() + " drink " + (pulledCards+1).ToString();
            StartCoroutine(TextAnimation(2f, gameEndText));
            GameOver();
        }

        if (stop)//stopped
        {
            gameEndText.text = PlayerManager._instance.getCurrentPlayer() + " drink 2";
            StartCoroutine(TextAnimation(2f, gameEndText));
            GameOver();
        }       
    }

    /// <summary>
    /// if the player pressed the stop button
    /// </summary>
    public void Stop()
    {
        if((gameOver || stop) || pulledCards==bullets-1)//if the player cant stop
            return;
        
        stop = true;
        CheckGameEnd();
    }

    /// <summary>
    /// if the player pressed the go button
    /// </summary>
    public void Go()
    {
        if(gameOver || stop)//if the player can't press the go button
            return;

        PullCard();//pulls a card

        cardsClearedText.text = bullets.ToString()+" / "+(pulledCards).ToString();//refreshes the texts
        
        if (!partailDeck.currentCard.isRed)//checking lose condition
        {
            gameOver = true;
            CheckGameEnd();
        }
        else
        {
            NextPlayer();//go for the next player
        }
    }

    /// <summary>
    /// Pulling from partial deck
    /// </summary>
    protected override void PullCard()
    {
        partailDeck.PullCard();
        pulledCards++;
    }

    /// <summary>
    /// Start a new round
    /// </summary>
    public void NewRound()
    {
        //setting the next playeer
        NextPlayer();
        
        //setting back to default value
        gameOver = false;
        stop = false;

        //setting the deck
        partailDeck.HideCards();
        partailDeck.previousCard = null;

        pulledCards = 0;

        Init(false);

        //setting the texts
        gameEndText.gameObject.SetActive(false);
        shotsText.text = pulledCards.ToString();

        //resetting the buttons
        inGameButtons.SetActive(true);
        continueButton.SetActive(false);
    }

    /// <summary>
    /// Adds more card in partialDeck
    /// </summary>
    public void plus()
    {
        bullets++;
        cardNumText.text = bullets.ToString();
    }

    /// <summary>
    /// Removes a card from partialDeck
    /// </summary>
    public void minus()
    {
        if (bullets > 6)
        {
            bullets--;
            cardNumText.text = bullets.ToString();
        }    
    }
}
