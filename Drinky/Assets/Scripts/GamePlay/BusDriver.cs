using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BusDriver : GameMode
{
    /// <summary>
    /// number of cards
    /// </summary>
    private int cardNum;

    /// <summary>
    /// number of pulled cards
    /// </summary>
    private int pulledCards=0;

    /// <summary>
    /// number of shots
    /// </summary>
    private int shots = 0;

    /// <summary>
    /// the previous card
    /// </summary>
    private Card previousCard = null;

    /// <summary>
    /// true if the player lost the round
    /// </summary>
    [SerializeField] private bool gameOver;

    /// <summary>
    /// true if the player cleared the round
    /// </summary>
    [SerializeField] private bool cleared;

    #region texts
    [SerializeField] private Text gameEndText;
    [SerializeField] private Text cardsClearedText;
    [SerializeField] private Text shotsText;
    
    [SerializeField] private Text cardNumText;
    [SerializeField] private Text winText;
    #endregion

    [SerializeField] private GameObject inGameButtons;
    [SerializeField] private GameObject continueButton;
    
    public void Start()
    {
        cardNumText.text = cardNum.ToString();
    }
    
    public override void StartGame()
    {
        base.StartGame();
        Init();
    }
    
    public override void Init()
    {
        base.Init();
        
        shotsText.text = shots.ToString();
        cardsClearedText.text = cardNum.ToString() + " / 0";

        nameText.text = PlayerManager._instance.getCurrentPlayer();
    }
    
    /// handles if the player lost the round
    /// <param name="t">time it waits</param>
    IEnumerator  GameOver(float t)
    {
        yield return new WaitForSeconds(t);
        
        //sets the shotText
        shots += pulledCards;
        shotsText.text = shots.ToString();

        //makeing the game end text invisible and setting the gameover to false
        gameEndText.gameObject.SetActive(false);
        gameOver = false;

        //calls the new round
        NewRound();
    }

    /// <summary>
    /// handles the win
    /// </summary>
    void Win()
    {
        //setting the wintext
        if (shots > 0)//if the player has to drink shots
        {
            winText.text = PlayerManager._instance.getCurrentPlayer() + " drink " + shots.ToString(); ;
        }
        else
        {
            winText.text = "Congratulation " + PlayerManager._instance.getCurrentPlayer() + "!";
        }

        inGameButtons.SetActive(false);
        continueButton.SetActive(true);
    }
    
    /// <summary>
    /// handles the game end conditions if needed
    /// </summary>
    public override void CheckGameEnd()
    {
        base.CheckGameEnd();

        if (gameOver)
        {
            StartCoroutine(GameOver(2f));
            StartCoroutine(TextAnimation(2f, gameEndText));
        }

        if (cleared)
        {
            Win();
            winText.gameObject.SetActive(true);
        }
    }

    public void ContinuePressed()
    {
        //setting the shots text
        shots = 0;
        shotsText.text = shots.ToString();

        NextPlayer();//settin the next player

        winText.gameObject.SetActive(false);//making the winText invisible

        cleared = false;//setting the cleared false

        NewRound();
    }

    /// <summary>
    /// pulls a card
    /// </summary>
    private void PullCard()
    {
        if (previousCard != null)//making the previous card invisible
        {
            previousCard.gameObject.SetActive(false);
        }

        //pulling a new card and setting the previous card
        previousCard = deck.currentCard;
        deck.PullCard();

        //moving the previous card under the current card
        previousCard.transform.position = new Vector3(-1, -1, 0);
        previousCard.GetComponent<SpriteRenderer>().sortingOrder = -11;

        //making the current card visible
        deck.currentCard.transform.position = new Vector3(0, 0, 0);
        deck.currentCard.GetComponent<SpriteRenderer>().sortingOrder = 0;
        deck.currentCard.gameObject.SetActive(true);

        pulledCards++;
    }

    /// <summary>
    /// handles if the lower or higher button is pressed
    /// </summary>
    /// <param name="higher">true if the higher button was pressed</param>
    public void LowerOrHigherPressed(bool higher)
    {
        if (gameOver || cleared)//if the new round has not yet begun, the buttton shouldn't do anything
        {
            return;
        }

        PullCard();//pulling a card

        //if the current or the previous card is ace then the game over is impossible
        if (deck.currentCard.value != 1 && previousCard.value != 1)//if the previous card is not ace nor the current card
        {
            /*
             * higher is true if we pressed the higher button and false we if pressed the lower button
             * if the two card's value are equals the player can't lose
             * if the previus card's value is greater then the current card's value and higher is true the player lost
             * if the previus card's value is smaller then the current card's value and higher is false the player lost
             */
            if ( previousCard.value > deck.currentCard.value == higher && previousCard.value != deck.currentCard.value)//lose condition
            {
                //game over
                gameOver = true;
                CheckGameEnd();
                return;
            }
        }

        cardsClearedText.text = cardNum.ToString() + " / " + (pulledCards).ToString();//refreshing the text

        if (pulledCards == cardNum)//win condition
        {
            cleared = true;
            CheckGameEnd();
        }
    }

    /// <summary>
    /// Makes a new round
    /// </summary>
    public void NewRound()
    {
        //resetting the cards and the cardClearedText
        pulledCards = 0;

        HideCards();

        deck.ResetCards();
       
        cardsClearedText.text = cardNum.ToString() + " / 0";

        previousCard = null;

        //resetting the buttons
        inGameButtons.SetActive(true);
        continueButton.SetActive(false);
    }

    /// <summary>
    /// increases the quantity of cards
    /// </summary>
    public void AddCard()
    {
        cardNum++;
        cardNumText.text = cardNum.ToString();
    }

    /// <summary>
    /// decreases the quantity of cards
    /// </summary>
    public void RemoveCard()
    {
        if(cardNum>2)
            cardNum--;
        cardNumText.text = cardNum.ToString();
    }
    
    /// <summary>
    /// Animates a text
    /// </summary>
    /// <param name="t">the duration of the animation</param>
    /// <param name="text">the text we animate</param>
    IEnumerator TextAnimation(float t, Text text)
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

}
