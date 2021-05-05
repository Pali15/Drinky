using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RussianRoulette : GameMode
{
    [SerializeField] private Card[] cards;
    [SerializeField] private int currentIndex = 0;
    [SerializeField] private Text nameText;
    [SerializeField] private Text gameEndText;
    [SerializeField] private Text cardsText;
    [SerializeField] private Text shotsText;
    [SerializeField] private Text cardNum;

    [SerializeField] private bool stop;
    [SerializeField] private bool gameover;

    [SerializeField] private int bullets = 6;
    
    public void Start()
    {
        cardNum.text = bullets.ToString();
    }

    public void StartGame()
    {
        base.StartGame();
        Init();
    }

    public override void Init()
    {
        base.Init();
        cards=new Card[bullets];
        SetCards();
        
        nameText.text = PlayerManager._instance.names[0];
        
        cards[0].gameObject.SetActive(true);
        if (!cards[0].isRed)
        {
            gameover = true;
            CheckGameEnd();
        }else
        {
            CurrentPlayerIncrease();
            nameText.text = PlayerManager._instance.names[PlayerManager._instance.currentPlayer];
        }
           
        cardsText.text = bullets.ToString()+" / "+(currentIndex + 1).ToString();;
        shotsText.text = (currentIndex + 1).ToString();
    }
    
    void SetCards()
    {
        int randidx = Random.Range(0, 52);
        
        while (CardManager._instance.cards[randidx].isRed)
        {
            randidx = Random.Range(0, 52);
        }

        int blackIndex = Random.Range(0, bullets);
        Debug.Log(blackIndex.ToString());

        cards[blackIndex] = CardManager._instance.cards[randidx];

        for (int i = 0; i < bullets; i++)
        {
            if (i != blackIndex)
            {
                randidx = Random.Range(0, 52);
                while (!CardManager._instance.cards[randidx].isRed || cards.Contains(CardManager._instance.cards[randidx])) 
                {
                    randidx = Random.Range(0, 52);
                }
                cards[i] = CardManager._instance.cards[randidx];
            }
            
        }

    }

    public override void CheckGameEnd()
    {
        base.CheckGameEnd();
        
        if (gameover)
        {
            gameEndText.text = PlayerManager._instance.names[PlayerManager._instance.currentPlayer] + " drink " + (currentIndex + 1).ToString();
            StartCoroutine(TextAnimation(2f, gameEndText));
            StartCoroutine(Restart(2.2f));
        }

        if (stop)
        {
            gameEndText.text = PlayerManager._instance.names[PlayerManager._instance.currentPlayer] + " drink 2";
            StartCoroutine(TextAnimation(2f, gameEndText));
            StartCoroutine(Restart(2.2f));
        }
    }
    

    public void Stop()
    {
        if((gameover || stop) || currentIndex==bullets-2)
            return;
        
        stop = true;
        CheckGameEnd();
    }

    public void Go()
    {
        if(gameover || stop)
            return;

        cards[currentIndex].gameObject.SetActive(false);
        currentIndex++;
        
        cards[currentIndex].gameObject.SetActive(true);
        
        shotsText.text = (currentIndex +1 ).ToString();
        cardsText.text = bullets.ToString()+" / "+(currentIndex + 1).ToString();;
        
        if (!cards[currentIndex].isRed)
        {
            gameover = true;
            CheckGameEnd();
        }
        else
        {
            ShowNextPlayer();
        }
    }
    
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

    IEnumerator Restart(float t)
    {
        yield return new WaitForSeconds(t);
        
        CurrentPlayerIncrease();
        
        gameover = false;
        stop = false;

        for (int i = 0; i < currentIndex + 1; i++)
        {
            cards[i].gameObject.SetActive(false);
        }
        SetCards();
        
        cards[0].gameObject.SetActive(true);
        nameText.text = PlayerManager._instance.names[PlayerManager._instance.currentPlayer];

        currentIndex = 0;
        shotsText.text = (currentIndex + 1).ToString();
        cardsText.text = bullets.ToString()+" / "+(currentIndex + 1).ToString();;
        
        if (!cards[0].isRed)
        {
            gameover = true;
            CheckGameEnd();
        }
        else
        {
            nameText.text = PlayerManager._instance.names[PlayerManager._instance.currentPlayer];
        }
        
    }
    
    public void ShowNextPlayer()
    {
        CurrentPlayerIncrease();
        nameText.text = PlayerManager._instance.names[PlayerManager._instance.currentPlayer];
    }

    public void CurrentPlayerIncrease()
    {
        if (PlayerManager._instance.currentPlayer < GameManager._instance.playersNum-1)
        {
            PlayerManager._instance.currentPlayer++;
        }
        else
        {
            PlayerManager._instance.currentPlayer = 0;
        }
    }

    public void plus()
    {
        bullets++;
        cardNum.text = bullets.ToString();
    }

    public void minus()
    {
        if (bullets > 6)
        {
            bullets--;
            cardNum.text = bullets.ToString();
        }
           
    }
    
    
}
