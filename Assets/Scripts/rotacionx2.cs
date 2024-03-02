using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotacionx2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotar en el eje x e y
        transform.Rotate(1, 2, 0);
    }
}
