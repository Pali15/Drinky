using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{

    public void onEdit()
    {
       int i=GetIndex();
       PlayerManager._instance.names[i] = this.GetComponent<InputField>().text;
    }

    int GetIndex()
    {
        int index = 0;
        foreach (var b in PlayersInput._instance.inputfields)
        {
            if (b == this.GetComponent<InputField>())
                break;
            index++;
        }
    
        Debug.Log(index.ToString());
        return index;
    }

    public void Remove()
    {
        int i = GetIndex();
        PlayerManager._instance.names.RemoveAt(i);
        PlayersInput._instance.inputfields.RemoveAt(i);
        GameManager._instance.playersNum--;
        Destroy(gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {    
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
