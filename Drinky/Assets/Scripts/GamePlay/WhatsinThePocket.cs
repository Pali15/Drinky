using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class WhatsinThePocket : GameMode
{
    /// <summary>
    /// number of kings which got pulled from the deck
    /// </summary>
    private int kingPulled = 0;

    /// <summary>
    /// task text
    /// </summary>
    [SerializeField] private Text taskText;

    
    public override void Init(bool b)
    {
        base.Init(b);
    }

    // Start is called before the first frame update
    public override void StartGame()
    {
        base.StartGame();
        Init(false);
        nameText.text = PlayerManager._instance.getCurrentPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sets the task text
    /// </summary>
    /// <param name="c">Card which got pulled</param>
    void NewTask(Card c)
    {
        switch (c.value)
        {
            case 1:
            {
                    taskText.text = PlayerManager._instance.getCurrentPlayer() + " Válassz ki egy embert és egy italt. A kiválasztott embernek el kell kezdenie innia az italt amit kiválasztottál addig, amíg nem mondod azt hogy: STOP.";
                    break;
            }

            case 2:
            {
                    taskText.text = "A 2 szomszédod igyon 1 kortyot";
                    break;
            }
            case 3:
            {
                    taskText.text = "Igyál három kortyot!";
                    break;
            }
            case 4:
            {
                    taskText.text = "Válassz 1 embert aki iszik 4 kortyot.";
                    break;
            }

            case 5:
            {
                    taskText.text = "A fiúk igyanak 1 kortyot!";
                    break;
            }
            case 6:
            {
                    taskText.text="A lányok igyanak 1 kortyot!";
                    break;
            }
            case 7:
            {
                    taskText.text = "Felelsz vagy mersz. Ha az ember nem válaszol a kérdésedre, vagy nem csinálja meg a feladatot amit adtál neki annak innia kell 4 kortyot.";
                    break;
            }

            case 8:
            {
                    taskText.text = "Válaszd ki a szolgád. A szolgádnak addig kell innia amikor neked, ameddig nem lesz neki is szolgája.";
                    break;
            }
            case 9:
            {
                    taskText.text = "Ossz ki 9 kortyot. Legalább 2 embert kell választanod.";
                    break;
            }
            case 10:
            {
                    taskText.text = "Mindenki tegye le a kisújját az asztalra. Az utolsó aki letette igyon 2 kortyot.";
                    break;
            }

            case 11:
            {
                    taskText.text = "Válassz ki egy témát. Mindenkinek mondania kell egy márkát ami kapcsolódik a témához. Egy márkát nem lehet kétszer mondani. Aki elsőnek nem tud új márkát mondani annak innia kell 2 kortyot.";
                    break;
            }
            case 12:
            {
                    taskText.text = "Never have I ever. Akik még sosem csinálták, annak innia kell 2 kortyot.";
                    break;
            }
            case 13:
            {
                    if (kingPulled == 1)
                    {
                        taskText.text = "Válasszatok ki egy poharat a Király poharaként. Töltsél bele az italodból.";
                    }
                    else if (kingPulled < 4)
                        taskText.text = "Töltsél az italodból a Király poharába.";
                    else
                    {
                        taskText.text = "Igyál meg mindent a király poharából.";
                        kingPulled = 0;
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// pulls a card
    /// </summary>
    /// <param name="currentCardPos">Position where the new card will be placed</param>
    protected override void PullCard(Vector3 currentCardPos)
    {
        base.PullCard(currentCardPos);

        if (deck.currentCard.value == 13)
        {
            kingPulled++;
        }
    }

    /// <summary>
    /// Pulls a card and sets the task
    /// </summary>
    public void NextCard()
    {
        PullCard(new Vector3(0, -0.5f, 0));

        NewTask(deck.currentCard);

        taskText.gameObject.SetActive(true);

        SwitchInGameContinueButtons();

    }

    /// <summary>
    /// handles if the continue button is pressed
    /// </summary>
    public override void ContinuePressed()
    {
        base.ContinuePressed();

        taskText.gameObject.SetActive(false);
    }
}
