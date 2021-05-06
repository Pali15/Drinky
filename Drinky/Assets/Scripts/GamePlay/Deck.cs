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
        private set;
    }

    private List<int> pulledIdxs;

    //sprites of the cards
    public Sprite[] sprites;

    //card object
    [SerializeField] Card _card;

    //list of the cards
    public Card[] cards;

    //the current card
    public Card currentCard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sets the initial values to the cards attributes
    /// </summary>
    private void SetCards()
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

                cards[j + (i * 13)].gameObject.SetActive(false);//making it invisible
            }
        }

      
    }

    /// <summary>
    /// Sets the cards and shuffles them
    /// The first card gets visible
    /// </summary>
    public void Init()
    {
        SetCards();
        ResetCards();
    }

    /// <summary>
    /// shuffles the pack and resets every attribute to it's default value
    /// </summary>
    public void ResetCards()
    {
        Shuffle();
        CurrentIdx = 0;
        cards[0].gameObject.SetActive(true);
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
    /// <returns>Card - the next card from the deck</returns>
    public void PullCard()
    {
        if (CurrentIdx < cards.Length - 2)
        {
            currentCard = cards[++CurrentIdx];
        }
        else//if we reached the last card from the deck
        { 
            ResetCards();
        }
    }
}
