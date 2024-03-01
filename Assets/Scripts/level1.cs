using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level1 : MonoBehaviour
{   public Material Verde;
    public Material MatSuelo;
    // Start is called before the first frame update
    void Start()

    {   //encuentra el material verde
        MatSuelo = Verde; 
        MatSuelo.CopyPropertiesFromMaterial(Verde);
        audioManager.instance.Play("Level1");
        PlayerPrefs.SetInt("level", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
