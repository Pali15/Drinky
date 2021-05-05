using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BusDriver : GameMode
{
    public int cardNum;
    private int index = 0;
    private List<Card> Cards;

    [SerializeField]private bool gameOver;
    [SerializeField]private bool cleared;

    public int shots=0;

    [SerializeField] private Text gameEndText;
    [SerializeField] private Text cardsClearedText;
    [SerializeField] private Text shotsText;
    [SerializeField] private Text nameText;
    [SerializeField] private Text cardNumText;
    [SerializeField] private Text winText;

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
        
        Cards=new List<Card>();
        SetList();
        Cards[0].gameObject.SetActive(true);

        shotsText.text = shots.ToString();
        cardsClearedText.text = cardNum.ToString() + " / 1";

        nameText.text = PlayerManager._instance.names[PlayerManager._instance.currentPlayer];

    }

    void SetList()
    {
        for (int i = 0; i < cardNum; i++)
        {
            int randIDX = Random.Range(0, 52);
            while (Cards.Contains(CardManager._instance.cards[randIDX]))
            {
                randIDX = Random.Range(0, 52);
            }
            Cards.Add(CardManager._instance.cards[randIDX]);
        }
    }

    IEnumerator  GameOver(float t)
    {
        yield return new WaitForSeconds(t);
        
        shots += index+1;
        shotsText.text = shots.ToString();
        gameOver = true;
        newRound();
    }

    void Win()
    {
        if (shots > 0)
        {
           winText.text = PlayerManager._instance.names[PlayerManager._instance.currentPlayer] + " drink " + shots.ToString();
        }
        else
        {
           winText.text = "Congratulation " + PlayerManager._instance.names[PlayerManager._instance.currentPlayer] + "!";
        }

        gameOver = false;
        
        inGameButtons.SetActive(false);
        continueButton.SetActive(true);
    }
    
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

    public void Higher()
    {
        if (gameOver || cleared)
        {
            return;
        }
        
        if (index > 0)
        {
            Cards[index-1].gameObject.SetActive(false);
        }
        
        Cards[index].transform.position=new Vector3(-1, -1, 0);
        Cards[index].GetComponent<SpriteRenderer>().sortingOrder = -11;
        
        Cards[index+1].gameObject.SetActive(true);

        if (Cards[index].value != 1 && Cards[index + 1].value != 1)
        {
            if (Cards[index].value > Cards[index + 1].value)
            {
                gameOver = true;
                CheckGameEnd();
                return;
            }
        }
        
        
        index++;
        
        if (!gameOver)
        {
            cardsClearedText.text = cardNum.ToString() + " / "+(index+1).ToString();
        }

        if (index == cardNum - 1)
        {
            cleared = true;
        }
        
        CheckGameEnd();
        
    }
    
    public void Lower()
    {
        if(gameOver ||cleared)
            return;

        if (index > 0)
        {
            Cards[index-1].gameObject.SetActive(false);
        }
        
        Cards[index].transform.position=new Vector3(-1, -1, 0);
        Cards[index].GetComponent<SpriteRenderer>().sortingOrder = -11;
        
        Cards[index+1].gameObject.SetActive(true);
        
        if (Cards[index].value != 1 && Cards[index + 1].value != 1)
        {
            if (Cards[index].value < Cards[index + 1].value)
            {
                gameOver = true;
                CheckGameEnd();
                return;
            }
        }
        
        index++;

        if (!gameOver)
        {
            cardsClearedText.text = cardNum.ToString() + " / "+(index+1).ToString();
        }
        
        if (index == cardNum - 1)
        {
            cleared = true;
        }
        
        CheckGameEnd();
    }

    void ClearList()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            Cards[i].transform.position=new Vector3(0, 0, 0);
            Cards[i].GetComponent<SpriteRenderer>().sortingOrder = -10;
            Cards[i].gameObject.SetActive(false);
        }
        
        Cards.Clear();
    }

    public void newRound()
    {
        ClearList();
        index = 0;
        SetList();
        Cards[0].gameObject.SetActive(true);

        cardsClearedText.text = cardNum.ToString() + " / "+(index+1).ToString();      
        
        if (!gameOver)
        {
            shots = 0;
            shotsText.text = shots.ToString();
            if (PlayerManager._instance.currentPlayer < GameManager._instance.playersNum-1 && GameManager._instance.playersNum!=1)
            {
                PlayerManager._instance.currentPlayer++;
                nameText.text = PlayerManager._instance.names[PlayerManager._instance.currentPlayer];
            }
            else
            {
                PlayerManager._instance.currentPlayer=0;
                nameText.text = PlayerManager._instance.names[PlayerManager._instance.currentPlayer];
            }
            
            winText.gameObject.SetActive(false);
            cleared = false;
        }
        else
        {
            gameEndText.gameObject.SetActive(false);
            gameOver = false;
        }
        
        inGameButtons.SetActive(true);
        continueButton.SetActive(false);
    }

    

    public void AddCard()
    {
        cardNum++;
        cardNumText.text = cardNum.ToString();
    }

    public void RemoveCard()
    {
        if(cardNum>2)
            cardNum--;
        cardNumText.text = cardNum.ToString();
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

}
