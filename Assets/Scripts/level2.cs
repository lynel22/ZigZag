using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2 : MonoBehaviour
{   
    // Start is called before the first frame update
    void Start()
    {   
        PlayerPrefs.SetInt("level", 2);
        audioManager.instance.Play("Level2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
