using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Update()
    {
        // Nesse caso é "+" porque a barra de vida funciona dos dois lados
        // Geralmente seria necessário colocar "- Camera..."
        transform.LookAt(transform.position + Camera.main.transform.forward);   
    }
}
