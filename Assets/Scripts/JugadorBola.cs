using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JugadorBola : MonoBehaviour
{
    //PUBLICAS
    public Camera camara;
    public GameObject sueloVerde;
    public GameObject sueloRojo;
    public GameObject booster;
    public GameObject sueloPunto;
    public GameObject sueloPinchosFor;
    public GameObject sueloSalto;
    public float velocidad = 5.0f;
    public float velocidadMax = 20.0f;

    public float velocidadMin = 7.5f;
    public ProgressBar barraProgreso;
    public int puntosMax = 15;
    //PRIVADAS
    private Vector3 offset;
    private float ValX, ValZ;
    private Vector3 direccionActual;
    private int puntos=0;
    private int cont=3;
    private bool acelerado = false;
    private int rojos=5;
    private int salto=4;
    private bool derecha=false;
    private int Pincho=0;
    private object lockObject = new object();

    public Transform background;
    
    // Start is called before the first frame update
    void Start()
    {   
        barraProgreso.BarValue = 0;
        offset = camara.transform.position;
        CrearSueloIni();
        direccionActual = Vector3.forward;
        background.transform.position = new Vector3(background.transform.position.x, background.transform.position.y, 10);
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

        if (transform.position.y < -5.0f) // Puedes ajustar este valor según la altura del vacío en tu escena
        {   
            audioManager.instance.Stop("Level"+PlayerPrefs.GetInt("level").ToString());
            SceneManager.LoadScene("Perder"); // Cargar la escena de perder
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Punto")
        {   
            puntos++;
            barraProgreso.BarValue = (float)Math.Round((double)puntos / puntosMax * 100, 0);
            if (puntos == puntosMax)
            {
                audioManager.instance.Play("Levelup");
                audioManager.instance.Stop("Level" + PlayerPrefs.GetInt("level").ToString());
                switch (PlayerPrefs.GetInt("level"))
                {
                    case 1:
                        SceneManager.LoadScene("Nivel2");
                        break;
                    case 2:
                        SceneManager.LoadScene("Fin");
                        break;
                }
            }
            else
            {
                audioManager.instance.Play("Punto");
            }
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Pincho")
        {
            audioManager.instance.Stop("Level"+PlayerPrefs.GetInt("level").ToString());
            SceneManager.LoadScene("Perder");
        }

        if(other.gameObject.tag == "Salto")
        {
            StartCoroutine(Salto());
            
        }
    }

    IEnumerator Salto()
    {   
        GetComponent<Rigidbody>().AddForce(Vector3.up * 6, ForceMode.Impulse);
        velocidad = 5.0f;
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(ReiniciarVelocidad(10,velocidad));
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
            
            StartCoroutine(ReiniciarVelocidad(5,velocidad));
        }
    
        if (collision.gameObject.name.Contains("SueloRojo"))
        {
            velocidad = velocidadMin;
            
            StartCoroutine(ReiniciarVelocidad(3,velocidad));
        }
    
        
    }


    IEnumerator ReiniciarVelocidad(int nSeconds,float velAsig)
    {   yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < nSeconds; i++)
        {   if(velocidad > 15.0f ){
                lock(lockObject){velocidad -= (15.0f-velAsig)/nSeconds;}
            }
            else if (velocidad < 15.0f )
            {   lock(lockObject){velocidad +=(15.0f-velAsig)/nSeconds;}
            }
            yield return new WaitForSeconds(1.0f);
        }
    }


    IEnumerator BorrarSuelo(GameObject suelo)
    {   
        float aleatorio = UnityEngine.Random.Range(0.0f, 1.0f);
        float especial = UnityEngine.Random.Range(0.0f, 1.0f);
        Quaternion rotacion;
        if(aleatorio > 0.5f ) //derecha
        {   
            if((Pincho ==1||salto ==3) && derecha){
                    ValX += 6f;
                    derecha=true;
            }
            else
            {
                    ValZ += 6f;
                    derecha=false;
            }
        }
        else //adelante
        {   if((Pincho ==1||salto ==3) && !derecha){
            
                ValZ += 6f;
                derecha=false;
            }
            else
            {
                ValX += 6f;
                derecha=true;
            }
        
        }

        if (especial > 0.85f && cont <= 0)
        {   if(derecha){
                rotacion = Quaternion.Euler(0, -90, 0);
            }
            else{
                rotacion = Quaternion.Euler(0, 180, 0);
            }
            cont=4;
            Instantiate(booster, new Vector3(ValX, 0, ValZ), rotacion);
        }
        else // es especial
        {   if(especial > 0.75f && rojos <= 0){
                rojos = 6;
                Instantiate(sueloRojo, new Vector3(ValX, 0, ValZ), Quaternion.identity);
            }
            else{
                if(especial > 0.65f && PlayerPrefs.GetInt("level") == 2){ // es pinchos
                    Pincho=2;
                    if(derecha){//pinchos derecha
                        Instantiate(sueloPinchosFor, new Vector3(ValX, 0, ValZ), Quaternion.Euler(0, -90, 0));
                        
                    }
                    else{//pinchos adelante
                        Instantiate(sueloPinchosFor, new Vector3(ValX, 0, ValZ), Quaternion.identity);
                        
                    }
                }
                else{
                    if(especial > 0.5f && salto<=0 && PlayerPrefs.GetInt("level")==3){ // es salto
                        salto=4;
                        if(derecha){//salto derecha
                            Instantiate(sueloSalto, new Vector3(ValX, 0, ValZ), Quaternion.Euler(0, 90, 0));
                            
                        }
                        else{//salto adelante
                            Instantiate(sueloSalto, new Vector3(ValX, 0, ValZ), Quaternion.identity);
                        }
                    }
                    else{
                        if(especial < 0.2f){ // es punto
                            Instantiate(sueloPunto, new Vector3(ValX, 0, ValZ), Quaternion.identity);
                        }
                        else{
                            Instantiate(sueloVerde, new Vector3(ValX, 0, ValZ), Quaternion.identity);
                        }
                    }
                    
                }
                
            }
            Pincho--;
            cont--;
            rojos--;
            salto--;
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
            Instantiate(sueloVerde, new Vector3(ValX, 0, ValZ), Quaternion.identity);
        }
    }
}
