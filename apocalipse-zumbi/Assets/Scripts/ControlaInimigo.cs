using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel
{
    public AudioClip SomDeMorte;
    private StatusInimigo statusInimigo;
    private GameObject Jogador;
    private MovimentaPersonagem movimentaInimigo;
    private AnimacaoPersonagem animaInimigo;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float contadorVagar;
    public GameObject KitMedicoPrefab;
    private float porcentagemGerarKitMedico = 0.1f;
 
	// Use this for initialization
	void Start ()
    {
        // Quando o gerador cria um zumbi, ele procura pela tag Jogador nos objetos
        Jogador = GameObject.FindGameObjectWithTag(Tags.Jogador);
        movimentaInimigo = GetComponent<MovimentaPersonagem>();
        animaInimigo = GetComponent<AnimacaoPersonagem>();
        statusInimigo = GetComponent<StatusInimigo>();
        AleatorizarZumbi();
    }


    // FixedUpdate is called once per 0.02s
    private void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);

        movimentaInimigo.Rotacionar(direcao);
        animaInimigo.Movendo(direcao.magnitude);

        // vaga
        if (distancia > 15)
        {
            Vagar();
        }
        // persegue
        else if (distancia > 2.5)
        {
            direcao = movimentaInimigo.Perseguir(Jogador, statusInimigo.Velocidade);
            animaInimigo.Atacar(false);
        }
        // ataca
        else 
        {
            direcao = movimentaInimigo.Perseguir(Jogador, statusInimigo.Velocidade);

            animaInimigo.Atacar(true);
        }

    }
   

    private void Vagar()
    {
        contadorVagar -= Time.deltaTime;

        if (contadorVagar <= 0)
        {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar = statusInimigo.contadorTempoNovaPosicaoAleatoria;
        }

        bool FicouPertoOSuficiente = Vector3.Distance(posicaoAleatoria, transform.position) <= 0.05;
        if (FicouPertoOSuficiente == false)
        {
            direcao = posicaoAleatoria - transform.position;
            movimentaInimigo.Movimentar(direcao, statusInimigo.Velocidade);
        }
    }

    Vector3 AleatorizarPosicao ()
    {
        int raioDaEsfera = 10;
        Vector3 posicao = Random.insideUnitSphere * raioDaEsfera;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }
  
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

    public void TomarDano(int dano)
    {
        statusInimigo.Vida -= dano;

        if (statusInimigo.Vida <= 0)
        {
            Morreu();
        }
    }

    public void Morreu()
    {
        Destroy(gameObject);
        ControlaAudio.instancia.PlayOneShot(SomDeMorte);
        VerificarGeracaoKitMedico(porcentagemGerarKitMedico);
    }

    private void VerificarGeracaoKitMedico (float porcentagemDeGeracaoDoKitMedico)
    {
        if(Random.value <= porcentagemDeGeracaoDoKitMedico)
        {
            Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity);
        }
    }
}
