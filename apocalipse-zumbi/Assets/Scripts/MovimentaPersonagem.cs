using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentaPersonagem : MonoBehaviour
{
    private Rigidbody meuRigidbody;

    private void Awake()
    {
        meuRigidbody = GetComponent<Rigidbody>();
    }

    public void Movimentar(Vector3 direcao, float velocidade)
    {
        // Normalizando o vetor posição para virar um vetor unitário

        // Soma a posição atual da física do jogador + a direção onde quero ir. Atualizando frame por frame (update)
        // Velocidade = quadradinhos por frame ; Time.deltatime faz ser quadrados por segundo * direcao que é Vector3
        // Considerando 80 fps fica: (0,0,0) + (0,0,1) * 10 * (1/80)
        meuRigidbody.MovePosition (meuRigidbody.position + direcao.normalized * velocidade * Time.deltaTime);
    }

    public void Rotacionar(Vector3 direcao)
    {
        // A direção calculada vai ser pra onde o personagem vai olhar.
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        meuRigidbody.MoveRotation(novaRotacao);
    }

}
