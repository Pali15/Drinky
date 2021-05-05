using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public static CardManager _instance;

    public Sprite[] sprites;

    [SerializeField] Card _card;
    
    public Card[] cards;

    public void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetCards()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                cards[j+(i*13)] = Instantiate(_card, transform);
                cards[j+(i*13)].GetComponent<SpriteRenderer>().sprite = sprites[j + (i * 13)];

                cards[j + (i * 13)].value = j + 1;
               
                if (i % 2 != 1)
                {
                    cards[j + (i * 13)].isRed = true;
                }
                
                cards[j+(i*13)].gameObject.SetActive(false);
            }
        }
    }

    public void HideCards()
    {
        for (int i = 0; i < 52; i++)
        {
            cards[i].gameObject.SetActive(false);
        }
    }
}
