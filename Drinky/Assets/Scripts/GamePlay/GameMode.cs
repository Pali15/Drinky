using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    public static GameMode _instance;

    [SerializeField] private GameObject Intro;
    [SerializeField] private GameObject GamePlay;

    [SerializeField]protected Deck deck;

    [SerializeField] protected Text nameText;

    // Start is called before the first frame update
    IEnumerator Start() {
        yield return new WaitForEndOfFrame();
    }

    public virtual void Init()
    {
        deck.Init();
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

    public void HideCards()
    {
        deck.HideCards();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
