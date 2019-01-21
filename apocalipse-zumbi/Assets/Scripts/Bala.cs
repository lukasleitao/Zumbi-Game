using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {

    public float Velocidade = 30;

	void FixedUpdate () {
        GetComponent<Rigidbody>().MovePosition
            (GetComponent<Rigidbody>().position + transform.forward * Velocidade * Time.deltaTime);		
	}

    // Quando a opção "is trigger" estiver ligada, ela entrará nesse método quando colidir
    // O Objeto de colisão será dado pelo jogo... Onde a bala colidir
    // Se colidir com zumbi, some a bala e o zumbi. Se não for zumbi, só some a bala
    private void OnTriggerEnter(Collider objetoDeColisao)
    {
        if (objetoDeColisao.tag == "Inimigo")
        {
            Destroy(objetoDeColisao.gameObject);
        }

        Destroy(gameObject);
    }
}
