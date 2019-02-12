using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour {

    public GameObject Jogador;
    public float Velocidade = 5;
    private MovimentaPersonagem movimentaInimigo;
    private AnimacaoPersonagem animaInimigo;

	// Use this for initialization
	void Start ()
    {
        // Quando o gerador cria um zumbi, ele procura pela tag Jogador nos objetos
        Jogador = GameObject.FindGameObjectWithTag("Jogador");
        movimentaInimigo = GetComponent<MovimentaPersonagem>();
        animaInimigo = GetComponent<AnimacaoPersonagem>();
        AleatorizarZumbi();
    }


    // FixedUpdate is called once per 0.02s
    private void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);

        // Direção é a posição do jogador menos a minha
        Vector3 direcao = Jogador.transform.position - transform.position;

        movimentaInimigo.Rotacionar(direcao);

        if (distancia > 2.5)
        {
            movimentaInimigo.Movimentar(direcao, Velocidade);

            animaInimigo.Atacar(false);
        }
        else  // Atacando quando estiver perto
        {
            animaInimigo.Atacar(true);
        }

    }

    // Para o jogo quando eu for atacado
    void AtacaJogador ()
    {
        int dano = Random.Range(15, 30);
        Jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    void AleatorizarZumbi ()
    {
        int geraTipoZumbi = Random.Range(1, 28);
        // Entra no zumbi, escolhe 1 aleatório de 27, entra nas opções do objeto e ativa o quadradinho dele
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }
}
