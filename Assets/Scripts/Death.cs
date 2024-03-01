using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Death : MonoBehaviour
{   public Button boton;
    // Start is called before the first frame update
    void Start()
    {   boton.onClick.AddListener(Reiniciar);
        audioManager.instance.Play("Death");
    }

    public void Reiniciar(){
        SceneManager.LoadScene("Nivel"+PlayerPrefs.GetInt("level").ToString());
        audioManager.instance.Stop("Death");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
