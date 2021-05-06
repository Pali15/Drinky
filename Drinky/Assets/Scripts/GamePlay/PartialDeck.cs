﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a Deck with costum cards
/// </summary>
public class PartialDeck : Deck
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// Sets the array 
    /// </summary>
    /// <param name="cardList">the elements of the array</param>
    protected virtual void SetCards(List<Card> cardList)
    {
        cards = cardList.ToArray();
        currentCard = cards[0];
    }

    /// <summary>
    /// Initalizes of the deck
    /// </summary>
    /// <param name="b">true if the first card will be visible</param>
    /// <param name="deck">the card of the deck</param>
    public override void Init(bool b, List<Card> deck = null)
    {
        SetCards(deck);
        CurrentIdx = 0;
        a = CurrentIdx;
    }

    /// <summary>
    /// Pulls a card
    /// </summary>
    public override void PullCard()
    {
        if (previousCard != null)//making the previous card invisible
        {
            previousCard.gameObject.SetActive(false);
        }

        previousCard = currentCard;//setting the previous card

        if (++CurrentIdx >= cards.Length)//cant go off limits
            CurrentIdx = 0;

        currentCard = cards[CurrentIdx];//setting the new currentcard
        
        //setting the positions of the cards
        previousCard.transform.position = new Vector3(-1, -1, 0);
        previousCard.GetComponent<SpriteRenderer>().sortingOrder = -11;

        //making the current card visible
        currentCard.transform.position = new Vector3(0, 0, 0);
        currentCard.GetComponent<SpriteRenderer>().sortingOrder = 0;
        currentCard.gameObject.SetActive(true);
    }
}
