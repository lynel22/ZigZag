using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level1 : MonoBehaviour
{   
    // Start is called before the first frame update

    public static int escena = 0;

    void Start()

    {   //encuentra el material verde
        
        audioManager.instance.Play("Level1");
        PlayerPrefs.SetInt("level", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
