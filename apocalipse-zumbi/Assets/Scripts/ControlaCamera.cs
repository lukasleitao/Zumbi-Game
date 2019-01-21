using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaCamera : MonoBehaviour {

    public GameObject Jogador;
    private Vector3 distCompensar;


	// Use this for initialization
	void Start () {
        // Pega a posição da câmera em relação ao (0,0,0) do mundo e diminui pela posição do jogador
        // também em relação ao (0,0,0) do mundo. Exemplo: (20,35,60) - (15, 35, 50) = (5,0,10)
        // Ficando nessa distância com relação ao jogador sempre
        distCompensar = transform.position - Jogador.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        // Exemplo: jogador = (10,15,20), Câmera = (15,15,30)
        transform.position = Jogador.transform.position + distCompensar;
	}
}
