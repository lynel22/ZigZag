using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fin : MonoBehaviour
{   public Button boton;
    // Start is called before the first frame update
    void Start()
    {   audioManager.instance.Play("Victory");
        boton.onClick.AddListener(Reiniciar);
    }

    void Reiniciar(){
        SceneManager.LoadScene("Inicio");
        audioManager.instance.Stop("Victory");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
