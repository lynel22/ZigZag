using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class NivelCarga : MonoBehaviour
{

    public Text Contador;
    // Start is called before the first frame update
    void Start()
    {
        level1.escena++;
        Contador.text = ""+level1.escena;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
