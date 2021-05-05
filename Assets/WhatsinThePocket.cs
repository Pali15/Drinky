using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class WhatsinThePocket : GameMode
{
    [SerializeField] private List<Card> pulledCards;
    [SerializeField] private Text taskText;
    [SerializeField] private Text actualPlayerText;
    [SerializeField] private Text nextPlayer;
    
    public override void Init()
    {
        base.Init();
        pulledCards=new List<Card>();
    }

    // Start is called before the first frame update
    public override void StartGame()
    {
        base.StartGame();
        Init();
        nextPlayer.text = "next player: "+PlayerManager._instance.names[PlayerManager._instance.currentPlayer];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NewTask(Card c)
    {
        switch (c.value)
        {
            case 1:
            {
                taskText.text =
                    "Everybody has to start drinking. You can only stop when, the player on you right stopped. " +
                    PlayerManager._instance.names[PlayerManager._instance.currentPlayer] +
                    " you are the first in the circle, so you can decide when you stop. ";
                break;
            }

            case 2:
            {
                taskText.text = "Your 2 neighbors has to drink.";
                break;
            }
            case 3:
            {
                taskText.text = "Drink 3!";
                break;
            }
            case 4:
            {
                taskText.text = "Choose 1 person to drink 4!";
                break;
            }

            case 5:
            {
                taskText.text = "Boys drink 1!";
                break;
            }
            case 6:
            {
                taskText.text="Girls drink 1";
                break;
            }
            case 7:
            {
                taskText.text = "Truth or Dare. If the person who you choosed won't anserw your question or won't do the task has to drink 5";
                break;
            }

            case 8:
            {
                taskText.text = "Choose your slave(Your slave has to drink with you every time).";
                break;
            }
            case 9:
            {
                taskText.text = "Split 9 shots. You have to choose at least 2 people.";
                break;
            }
            case 10:
            {
                taskText.text = "Put down your little finger to the desk. The last people has to drink 2!";
                break;
            }

            case 11:
            {
                taskText.text = "Choose a topic. Everybody has to say a brand which connects somehow to the topic. The first people who can't say a new brand has to drink 3.";
                break;
            }
            case 12:
            {
                taskText.text = "Never have I ever. Who has ever, drink 3.";
                break;
            }
            case 13:
            {
                if(KingsINPulledCard()<4)
                    taskText.text = "Create the King's mug. Pour from you drink to the King's mug. If you pulled the fourth King, you have the drink everything from the King's mug. Kings pulled: "+KingsINPulledCard().ToString();
                else
                {
                    taskText.text = "Drink everything from the King's mug";
                }
                break;
            }
        }
    }


    int KingsINPulledCard()
    {
        int value = 0;
        foreach (var card in pulledCards)
        {
            if (card.value == 13)
                value++;
        }

        return value;
    }
    public void NextCard()
    {
        if (pulledCards.Count==52)
        {
            for (int i = 0; i < 49; i++)
            {
                pulledCards.RemoveAt(0);
            }
        }
        
        int index = Random.Range(0, 52);
        while (pulledCards.Contains(CardManager._instance.cards[index]))
        {
            index = Random.Range(0, 52);
        }

        if (pulledCards.Count > 0)
        {
            if (pulledCards.Count == 1)
            {
                pulledCards[pulledCards.Count-1].transform.position=new Vector3(-1, -2, 0);
                pulledCards[pulledCards.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = -11;
            }
            else
            {
                pulledCards[pulledCards.Count-2].gameObject.SetActive(false);
                pulledCards[pulledCards.Count-2].transform.position=new Vector3(0, 0, 0); 
                pulledCards[pulledCards.Count-2].GetComponent<SpriteRenderer>().sortingOrder = -10;
                
                pulledCards[pulledCards.Count-1].transform.position=new Vector3(-1, -2, 0); 
                pulledCards[pulledCards.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = -11;
            }
            
        }
        
        pulledCards.Add(CardManager._instance.cards[index]);
        CardManager._instance.cards[index].gameObject.SetActive(true);
        CardManager._instance.cards[index].transform.position=new Vector3(0, -1, 0);
        
        NewTask(CardManager._instance.cards[index]);
        
        NextPlayertext();
    }

    void NextPlayertext()
    {
        if (PlayerManager._instance.currentPlayer < GameManager._instance.playersNum - 1)
        {
            actualPlayerText.text = PlayerManager._instance.names[PlayerManager._instance.currentPlayer];
            PlayerManager._instance.currentPlayer++;
            nextPlayer.text = "next player: "+PlayerManager._instance.names[PlayerManager._instance.currentPlayer];
        }
        else
        {
            actualPlayerText.text =PlayerManager._instance.names[PlayerManager._instance.currentPlayer];
            PlayerManager._instance.currentPlayer = 0;
            nextPlayer.text = "next player: "+PlayerManager._instance.names[PlayerManager._instance.currentPlayer];
        }
            
    }
}
