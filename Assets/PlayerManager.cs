using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager _instance;

    public List<string> names;
    public int currentPlayer=0;

    public int player=0;
    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        names=new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
