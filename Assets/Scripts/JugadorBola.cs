using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JugadorBola : MonoBehaviour
{
    //PUBLICAS
    public Camera camara;
    public GameObject suelo;
    public GameObject booster;
    public GameObject sueloPunto;
    public float velocidad = 5.0f;
    public float velocidadMax = 30.0f;
    public ProgressBar barraProgreso;
    public int puntosMax = 25;
    //PRIVADAS
    private Vector3 offset;
    private float ValX, ValZ;
    private Vector3 direccionActual;
    private int puntos=0;
    private int cont=3;
    
    // Start is called before the first frame update
    void Start()
    {   barraProgreso.BarValue = 0;
        offset = camara.transform.position;
        CrearSueloIni();
        direccionActual = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        camara.transform.position = transform.position + offset;
        if (Input.GetKeyDown(KeyCode.Space)) // Barra espaciadora
        {
            CambiarDireccion();
        }

        transform.Translate(direccionActual * velocidad * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Punto")
        {   
            puntos++;
            barraProgreso.BarValue = (float)puntos/puntosMax*100;
            if(puntos == puntosMax){
                audioManager.instance.Play("Levelup");
                SceneManager.LoadScene("Fin");
            }
            else{
                audioManager.instance.Play("Punto");
            }
            Destroy(other.gameObject);
        }
    
    }


    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {   
            StartCoroutine(BorrarSuelo(collision.gameObject));
        }
    }

    
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Booster"))
        {
            velocidad = velocidadMax;
            StartCoroutine(ReiniciarVelocidad());
        }
    }


    IEnumerator ReiniciarVelocidad()
    {
        for (int i = 0; i < 5; i++)
        {   if(velocidad > 15.0f){
                velocidad -= 0.5f;
            }
            
            yield return new WaitForSeconds(1.0f);
        }
    }


    IEnumerator BorrarSuelo(GameObject suelo)
    {   
        float aleatorio = Random.Range(0.0f, 1.0f);
        float especial = Random.Range(0.0f, 1.0f);
        Quaternion rotacion;
        if (aleatorio > 0.5f)
        {
            ValX += 6f;
            rotacion=Quaternion.Euler(0, -90, 0);
        }
        else
        {
            ValZ += 6f;
            rotacion=Quaternion.Euler(0, 180, 0);
        }

        if (especial > 0.95f && cont <= 0)
        {   
            cont=3;
            Instantiate(booster, new Vector3(ValX, 0, ValZ), rotacion);
        }
        else // es especial
        {   
            if(especial < 0.2f){ // es punto
                Instantiate(sueloPunto, new Vector3(ValX, 0, ValZ), Quaternion.identity);
            }
            else{
                Instantiate(suelo, new Vector3(ValX, 0, ValZ), Quaternion.identity);
            }
            cont--;
        }
        
        yield return new WaitForSeconds(2.5f);
        suelo.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        suelo.gameObject.GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(2.5f);
        Destroy(suelo);
    }

    void CambiarDireccion()
    {
        if (direccionActual == Vector3.forward)
        {
            direccionActual = Vector3.right;
        }
        else
        {
            direccionActual = Vector3.forward;
        }
    }

    void CrearSueloIni()
    {
        for (int i = 0; i < 3; i++)
        {
            ValZ += 6f;
            Instantiate(suelo, new Vector3(ValX, 0, ValZ), Quaternion.identity);
            
        }
    }
}
