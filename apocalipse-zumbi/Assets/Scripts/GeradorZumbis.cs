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

    private void Start()
    {
        jogador = GameObject.FindGameObjectWithTag(Tags.Jogador);    
    }
    
    void Update () {

        if (Vector3.Distance(transform.position,jogador.transform.position) >= distanciaDoJogadorGerarZumbi)
        {
            contadorTempo += Time.deltaTime;

            if (contadorTempo > TempoGerarZumbi)
            {
                StartCoroutine(GerarZumbi());
                contadorTempo = 0;
            }
        }

    }

    // IEnumerator faz com que o frame não seja atrasado. Sai do while e tenta no próximo fram
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

        Instantiate(Zumbi, novaPosicaoCriacao, transform.rotation);
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
