using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorBola : MonoBehaviour
{
    //PUBLICAS
    public Camera camara;
    public GameObject suelo;
    public float velocidad = 5.0f;
    //PRIVADAS
    private Vector3 offset;
    private float ValX, ValZ;
    private Vector3 direccionActual;
    // Start is called before the first frame update
    void Start()
    {
        offset = camara.transform.position;
        CrearSueloIni();
        direccionActual = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        camara.transform.position = transform.position + offset;
        if (Input.GetKeyUp(KeyCode.Space)) // Barra espaciadora
        {
            CambiarDireccion();
        }

        transform.Translate(direccionActual * velocidad * Time.deltaTime);
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            StartCoroutine(BorrarSuelo(collision.gameObject));
        }
    }

    IEnumerator BorrarSuelo(GameObject suelo)
    {
        float aleatorio = Random.Range(0.0f, 1.0f);
        if (aleatorio > 0.5f)
        {
            ValX += 6f;
        }
        else
        {
            ValZ += 6f;
        }
        Instantiate(suelo, new Vector3(ValX, 0, ValZ), Quaternion.identity);
        yield return new WaitForSeconds(2);
        suelo.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        suelo.gameObject.GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(2);
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
        for (int i = 0; i < 10; i++)
        {
            ValZ += 6f;
            Instantiate(suelo, new Vector3(ValX, 0, ValZ), Quaternion.identity);
            
        }
    }
}
