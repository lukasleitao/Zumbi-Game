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

    public Vector3 Perseguir(GameObject ObjetoSeguido, float velocidade)
    {
        // Direção é a posição do jogador menos a minha
        Vector3 direcao = ObjetoSeguido.transform.position - transform.position;
        Movimentar(direcao, velocidade);
        // Retorna direção para atualizar a rotação
        return direcao;
    }

    public void Morrer()
    {
        // meuRigidbody.constraints = RigidbodyConstraints.None; -- Não me interessa agora -- Ajuda se eu quisesse que ele sumisse no chao
        GetComponent<Collider>().enabled = false;
        meuRigidbody.isKinematic = false;
        meuRigidbody.velocity = Vector3.zero;
    }
        
}
