using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    
    public GameMode[] gameModes;
    public GameMode CurrentGameMode;

    public int playersNum = 0;
    void Awake()
    {
        _instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGameMode(int index)
    {
        if (CurrentGameMode != null)
        {
            Destroy(CurrentGameMode.gameObject);
        }
        
        if(index>gameModes.Length-1)
            return;

        CurrentGameMode=Instantiate(gameModes[index], Vector3.zero, Quaternion.identity);
    }
   
}
