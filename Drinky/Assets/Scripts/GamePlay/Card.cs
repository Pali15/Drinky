using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
   
    public int value;

    public bool isRed;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Copy(Card card)
    {
        value = card.value;
        isRed = card.isRed;
        GetComponent<SpriteRenderer>().sprite = card.GetComponent<SpriteRenderer>().sprite;
    }
}
