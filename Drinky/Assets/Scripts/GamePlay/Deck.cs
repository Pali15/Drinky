using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A deck of cards
/// </summary>
public class Deck : MonoBehaviour
{
    //the index of the current card
    public int CurrentIdx
    {
        get;
        protected set;
    }

    //sprites of the cards
    public Sprite[] sprites;

    //card object
   [SerializeField] protected Card _card;

    //list of the cards
    public Card[] cards;

    //the current card
    public Card currentCard;
    public Card previousCard=null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sets the deck
    /// </summary>
    /// <param name="cardsList">the costum cards which will be in the deck</param>
    protected virtual void SetCards(List<Card> cardsList=null)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                cards[j + (i * 13)] = Instantiate(_card, this.transform);

                cards[j + (i * 13)].GetComponent<SpriteRenderer>().sprite = sprites[j + (i * 13)];//setting the card's sprite

                cards[j + (i * 13)].value = j + 1;//setting the card's value

                if (i % 2 != 1)
                {
                    cards[j + (i * 13)].isRed = true;//setting isRead true if the card is red
                }
                else
                {
                    cards[j + (i * 13)].isRed = false;
                }

                cards[j + (i * 13)].gameObject.SetActive(false);//making it invisible
            }
        }      
    }

    /// <summary>
    /// Sets the cards and shuffles them
    /// The first card gets visible if b is true
    /// </summary>
    /// <param name="b">firts card will be visible if it is true</param>
    /// <param name="cardsList">the costum cards which will be in the deck</param>
    public virtual void Init(bool b, List<Card> cardList = null)
    {
       SetCards(cardList);
       ResetCards(b);
    }

    /// <summary>
    /// shuffles the pack and resets every attribute to it's default value, and making the frist card visible if b is true
    /// </summary>
    /// <param name="b">firts card will be visible if it is true</param>
    public void ResetCards(bool b)
    {
        Shuffle();

        CurrentIdx = 0;

        cards[0].gameObject.SetActive(b);
        currentCard = cards[0];
        currentCard.transform.position = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// hides the cards
    /// </summary>
    public void HideCards()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// shuffles the cards
    /// </summary>
    public void Shuffle()
    {
        for (int i=0; i<300; i++)
        {
            //getting two uniqe random index
            int idx_1 = UnityEngine.Random.Range(0, cards.Length);
            int idx_2 = UnityEngine.Random.Range(0, cards.Length);

            while (idx_1 == idx_2)
            {
                idx_2 = UnityEngine.Random.Range(0, cards.Length);
            }

            //swapping the two cards
            _card.Copy(cards[idx_1]);
            cards[idx_1].Copy(cards[idx_2]);
            cards[idx_2].Copy(_card);
        }
    }

    /// <summary>
    /// return the next card from the deck
    /// </summary>
    public virtual void PullCard()
    {
        if (previousCard != null)//making the previous card invisible
        {
            previousCard.gameObject.SetActive(false);
        }

        //pulling a new card and setting the previous card
        previousCard = currentCard;

        if (CurrentIdx < cards.Length - 2)
        {
            currentCard = cards[++CurrentIdx];
        }
        else//if we reached the last card from the deck
        { 
            ResetCards(true);
        }

        previousCard.transform.position = new Vector3(-1, -1, 0);
        previousCard.GetComponent<SpriteRenderer>().sortingOrder = -11;

        //making the current card visible
        currentCard.transform.position = new Vector3(0, 0, 0);
        currentCard.GetComponent<SpriteRenderer>().sortingOrder = 0;
        currentCard.gameObject.SetActive(true);
    }
}
