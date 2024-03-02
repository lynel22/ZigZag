using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInicial : MonoBehaviour
{

    public int pantalla = 1;
    public Button boton;
    // Start is called before the first frame update
    public void Jugar(){
        LevelLoader.LoadNextLevel("Nivel1");
        audioManager.instance.Stop("Menu");
    }
    
    
    
    void Start()
    {   boton.onClick.AddListener(Jugar);
        audioManager.instance.Play("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
