using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour {

    public GameObject Zumbi;
    public float TempoGerarZumbi = 4;
    public LayerMask LayerZumbi;
    private float contadorTempo = 0;
    private float raioDaEsferaDeGeracao = 3;
    private float distanciaDoJogadorGerarZumbi = 20;
    private GameObject jogador;
    private int quantidadeMaximaZumbisVivos = 2;
    private int quantidadeZumbisVivos;
    private float tempoProximaDificuldade = 5;
    private float contadorTempoDificuldade;

    private void Start()
    {
        jogador = GameObject.FindGameObjectWithTag(Tags.Jogador);
        contadorTempoDificuldade = tempoProximaDificuldade;

        for(int i = 0 ;i < quantidadeMaximaZumbisVivos; i++)
        {
            StartCoroutine(GerarZumbi());
        }

    }
    
    void Update () {

        bool possoGerarZumbiPelaDistancia = Vector3.Distance(transform.position, jogador.transform.position)
                                                            >= distanciaDoJogadorGerarZumbi;

        bool possoGerarZumbiPelaQuantidade = quantidadeZumbisVivos < quantidadeMaximaZumbisVivos;

        if (possoGerarZumbiPelaDistancia && possoGerarZumbiPelaQuantidade)
        {
            contadorTempo += Time.deltaTime;

            if (contadorTempo > TempoGerarZumbi)
            {
                StartCoroutine(GerarZumbi());
                contadorTempo = 0;
            }
        }

        if(Time.timeSinceLevelLoad > contadorTempoDificuldade)
        {
            quantidadeMaximaZumbisVivos++;
            contadorTempoDificuldade = Time.timeSinceLevelLoad + tempoProximaDificuldade;
        }
    }

    // IEnumerator faz com que o frame não seja atrasado. Sai do while e tenta no próximo frame
    IEnumerator GerarZumbi()
    {
        int raioDaEsferaDeColisao = 1;
        Vector3 novaPosicaoCriacao = AleatorizarPosicao();
        Collider[] colisores = Physics.OverlapSphere(novaPosicaoCriacao, raioDaEsferaDeColisao, LayerZumbi);

        while (colisores.Length > 0)
        {
            novaPosicaoCriacao = AleatorizarPosicao();
            colisores = Physics.OverlapSphere(novaPosicaoCriacao, raioDaEsferaDeColisao, LayerZumbi);
            yield return null;
        }

        ControlaInimigo zumbi = Instantiate(Zumbi, novaPosicaoCriacao, transform.rotation).GetComponent<ControlaInimigo>();
        zumbi.MeuGerador = this;

        quantidadeZumbisVivos++;
    
    }

    public void DiminuirQuantidadeZumbi()
    {
        quantidadeZumbisVivos--;
    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * raioDaEsferaDeGeracao;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioDaEsferaDeGeracao);
    }
}
