using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    public static GameMode _instance;

    [SerializeField] private GameObject Intro;
    [SerializeField] private GameObject GamePlay;
    
    // Start is called before the first frame update
    IEnumerator Start() {
        yield return new WaitForEndOfFrame();
    }

    public virtual void Init()
    {
        CardManager._instance.SetCards();
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
