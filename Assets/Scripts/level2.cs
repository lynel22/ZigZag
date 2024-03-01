using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2 : MonoBehaviour
{   public Material MatSuelo;
    public Material Morado;
    // Start is called before the first frame update
    void Start()
    {   MatSuelo.color= HexToColor("00FF22");
        PlayerPrefs.SetInt("level", 2);
        audioManager.instance.Play("Level2");
    }

    private Color HexToColor(object colorHexadecimal)
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
