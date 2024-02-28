using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CuentaAtras : MonoBehaviour
{
    private Button boton;
    public Image imagen;
    public Sprite[] numeros;

    // Start is called before the first frame update
    void Start()
    {
        boton = GameObject.FindObjectOfType<Button>();
        boton.onClick.AddListener(Empezar);

       //boton = GameObject.FindWithTag("botonSalir").GetComponent<Button>();
    }

    void Empezar()
    {
        imagen.gameObject.SetActive(true);
        boton.gameObject.SetActive(false);

        StartCoroutine(CuentaAtra());
    }

    IEnumerator CuentaAtra()
    {
        for (int i = 0; i < numeros.Length; i++)
        {
            imagen.sprite = numeros[i];
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene("Nivel1");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
