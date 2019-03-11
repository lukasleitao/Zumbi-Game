using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefeFase : MonoBehaviour
{
    public GameObject ChefeDaFase;
    private float tempoProximaGeracao = 0;
    public float tempoEntreGeracoes = 20;
    private ControlaInterface scripControlaInterface;
    public Transform[] PosicoesDeCriacaoChefe;
    private Transform jogador;

    private void Start()
    {
        tempoProximaGeracao = tempoEntreGeracoes;
        scripControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
        jogador = GameObject.FindGameObjectWithTag(Tags.Jogador).transform;
    }

    private void Update()
    {
        if(Time.timeSinceLevelLoad > tempoProximaGeracao)
        {
            Vector3 posicaoDeCriacao = CalcularPosicaoGerarChefe();
            Instantiate(ChefeDaFase, posicaoDeCriacao, Quaternion.identity);
            scripControlaInterface.TextoApareceuZumbi();
            tempoProximaGeracao = Time.timeSinceLevelLoad + tempoEntreGeracoes;
        }
    }

    Vector3 CalcularPosicaoGerarChefe ()
    {
        Vector3 posicaoMaisDistanteDoJogador = Vector3.zero;
        float maisDistante = 0;

        foreach(Transform posicao in PosicoesDeCriacaoChefe)
        {
            float distanciaEntreOJogador = Vector3.Distance(jogador.position, posicao.position);

            if(distanciaEntreOJogador > maisDistante)
            {
                maisDistante = distanciaEntreOJogador;
                posicaoMaisDistanteDoJogador = posicao.position;
            }
        }

        return posicaoMaisDistanteDoJogador;
    }
}
