using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ControlaChefe : MonoBehaviour, IMatavel
{
    private GameObject jogador;
    private NavMeshAgent agente;
    private StatusInimigo statusChefe;
    private AnimacaoPersonagem animaChefe;
    private MovimentaPersonagem movimentaChefe;
    public KitMedico KitMedicoPrefab;
    public Slider sliderVidaChefe;
    public Color CorDaVidaMaxima, CorDaVidaMinima;
    public Image BarraVidaChefe;


    // Start is called before the first frame update
    void Start()
    {
        // Movimentacao
        jogador = GameObject.FindWithTag(Tags.Jogador);
        movimentaChefe = GetComponent<MovimentaPersonagem>();
        agente = GetComponent<NavMeshAgent>();
        // Status
        statusChefe = GetComponent<StatusInimigo>();
        agente.speed = statusChefe.Velocidade;
        // Status - interface
        sliderVidaChefe.maxValue = statusChefe.VidaInicial;
        AtualizarSlideVidaChefe();
        // Animacao
        animaChefe = GetComponent<AnimacaoPersonagem>();
    }

    // Update is called once per frame
    void Update()
    {
        agente.SetDestination(jogador.transform.position);
        animaChefe.Movendo(agente.velocity.magnitude);

        if(agente.hasPath == true)
        {
            float porcentagemVidaChefe = (float)statusChefe.Vida / statusChefe.VidaInicial;
            Color corDaVida = Color.Lerp(CorDaVidaMinima, CorDaVidaMaxima, porcentagemVidaChefe);
            BarraVidaChefe.color = corDaVida;

            bool estaPertoDoJogador = agente.remainingDistance <= agente.stoppingDistance;

            if (estaPertoDoJogador)
            {
                animaChefe.Atacar(true);
                Vector3 direcao = jogador.transform.position - transform.position;
                movimentaChefe.Rotacionar(direcao);
            }
            else
            {
                animaChefe.Atacar(false);
            }
        }

    }

    void AtacaJogador()
    {
        int dano = Random.Range(30, 40);
        jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    public void TomarDano(int dano)
    {
        statusChefe.Vida -= dano;
        AtualizarSlideVidaChefe();
        if(statusChefe.Vida <= 0)
        {
            Morreu();
        }
    }

    public void Morreu()
    {
        animaChefe.Morreu();
        movimentaChefe.Morrer();

        this.enabled = false;
        agente.enabled = false;

        float tempoAteZumbiDesaparecer = 2;
        Destroy(gameObject, tempoAteZumbiDesaparecer);

        // Lembrar do script da bala e porque a rotação lá é diferente
        Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity);
    }

    public void AtualizarSlideVidaChefe()
    {
        sliderVidaChefe.value = statusChefe.Vida;
    }
}
