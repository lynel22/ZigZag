using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("level", 3);
        audioManager.instance.Play("Level3");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
