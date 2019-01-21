using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour {

    public GameObject Jogador;
    public float Velocidade = 5;

	// Use this for initialization
	void Start () {
        // Quando o gerador cria um zumbi, ele procura pela tag Jogador nos objetos
        Jogador = GameObject.FindGameObjectWithTag("Jogador");
        int geraTipoZumbi = Random.Range(1, 28);
        // Entra no zumbi, escolhe 1 aleatório de 27, entra nas opções do objeto e ativa o quadradinho dele
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
	}

    // Update is called once per frame
    void Update()
    {

    }

    // FixedUpdate is called once per 0.02s
    private void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);

        // Direção é a posição do jogador menos a minha
        Vector3 direcao = Jogador.transform.position - transform.position;

        // A direção calculada vai ser pra onde o zumbi vai olhar.
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        GetComponent<Rigidbody>().MoveRotation(novaRotacao);

        if (distancia > 2.5)
        {
            // Normalizando o vetor posição para virar um vetor unitário
            GetComponent<Rigidbody>().MovePosition
                (GetComponent<Rigidbody>().position + direcao.normalized * Velocidade * Time.deltaTime);

            GetComponent<Animator>().SetBool("Atacando", false);
        }
        else  // Atacando quando estiver perto
        {
            GetComponent<Animator>().SetBool("Atacando", true);
        }

    }

    // Para o jogo quando eu for atacado
    void AtacaJogador ()
    {
        Time.timeScale = 0;
        Jogador.GetComponent<ControlaJogador>().textoGameOver.SetActive(true);
        Jogador.GetComponent<ControlaJogador>().Vivo = false;
    }

}
