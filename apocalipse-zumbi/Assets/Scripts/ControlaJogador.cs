using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// SceneManagement para poder reiniciar o jogo
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour {

    private Vector3 direcao;
    public LayerMask MascaraChao;
    public GameObject textoGameOver;
    // Fui no jogador, achei esse script na interface e joguei o script do canvas nele
    // No script ControlaInterface está privado e não público
    public ControlaInterface scriptControlaInterface;
    public AudioClip SomDeDano;
    private MovimentoJogador meuMovimentaJogador;
    private AnimacaoPersonagem minhaAnimacaoPersonagem;
    public Status statusJogador;

    // Quando recomeçar o jogo não recomeçar congelado
    private void Start()
    {
        Time.timeScale = 1;
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

        if (statusJogador.Vida <= 0)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("game4");
            }
        }

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
        ControlaAudio.instancia.PlayOneShot(SomDeDano);

        if (statusJogador.Vida <= 0)
        {
            Time.timeScale = 0;
            // Está no Canvas
            GetComponent<ControlaJogador>().textoGameOver.SetActive(true);
        }
    }
    
}

