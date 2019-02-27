using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel
{
    private Vector3 direcao;
    public LayerMask MascaraChao;
    // Fui no jogador, achei esse script na interface e joguei o script do canvas nele
    // No script ControlaInterface está privado e não público
    public ControlaInterface scriptControlaInterface;
    public AudioClip SomDeDano;
    private MovimentoJogador meuMovimentaJogador;
    private AnimacaoPersonagem minhaAnimacaoPersonagem;
    // statusJogador é public por causa do script ControlaInterface
    public Status statusJogador;

    // Quando recomeçar o jogo não recomeçar congelado
    private void Start()
    {
        meuMovimentaJogador = GetComponent<MovimentoJogador>();
        minhaAnimacaoPersonagem = GetComponent<AnimacaoPersonagem>();
        statusJogador = GetComponent<Status>();
    }

    // Update roda a cada frame
    void Update () {

        // Horizontal e Vertical era o nome já no unity
        // Esses dois já tem gravado neles as teclas W,A,S,D e as setas
        // Dá pra editar lá no unity
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        //direcao = (eixoX, 0, eixoZ);
        direcao = new Vector3(eixoX, 0, eixoZ);

        minhaAnimacaoPersonagem.Movendo(direcao.magnitude);

	}

    // FixedUpdate roda a cada 0.02s
    void FixedUpdate()
    {
        meuMovimentaJogador.Movimentar(direcao, statusJogador.Velocidade);
        
        meuMovimentaJogador.RotacaoJogador(MascaraChao);
    }


    public void TomarDano(int dano)
    {
        statusJogador.Vida -= dano;
        scriptControlaInterface.AtualizarSliderVidaJogador();
        // instancia é uma variável public static no script ControlaAudio. Instancia do AudioSource
        ControlaAudio.instancia.PlayOneShot(SomDeDano);

        if (statusJogador.Vida <= 0)
        {
            Morreu();
        }
    }

    public void Morreu()
    {
        // Está no Canvas
        scriptControlaInterface.GameOver();
    }

    public void CurarVida(int quantidadeDeCura)
    {
        statusJogador.Vida += quantidadeDeCura;

        // Mathf.Clamp() seria um método da Unity para tratar isso
        if(statusJogador.Vida > statusJogador.VidaInicial)
        {
            statusJogador.Vida = statusJogador.VidaInicial;
        }

        scriptControlaInterface.AtualizarSliderVidaJogador();
    }
}

