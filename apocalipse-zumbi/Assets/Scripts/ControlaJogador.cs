using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// SceneManagement para poder reiniciar o jogo
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour {

    public float Velocidade = 10;
    private Vector3 direcao;
    public LayerMask MascaraChao;
    public GameObject textoGameOver;
    public int Vida = 100;
    // Fui no jogador, achei esse script na interface e joguei o script do canvas nele
    // No script ControlaInterface está privado e não público
    public ControlaInterface scriptControlaInterface;
    // Assim com animator tbm e etc...
    private Rigidbody jogadorRigidBody;
    public AudioClip SomDeDano;

    // Quando recomeçar o jogo não recomeçar congelado
    private void Start()
    {
        Time.timeScale = 1;
        jogadorRigidBody = GetComponent<Rigidbody>();
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

        // Máquinas da estado das animações
        if (direcao != Vector3.zero){
            GetComponent<Animator>().SetBool("Movendo", true);
        }
        else {
            GetComponent<Animator>().SetBool("Movendo", false);
        }

        if (Vida <= 0)
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
        // Soma a posição atual da física do jogador + a direção onde quero ir. Atualizando frame por frame
        // Velocidade = quadradinhos por frame ; Time.deltatime faz ser quadrados por segundo * direcao que é Vector3
        // Considerando 80 fps fica: (0,0,0) + (0,0,1) * 10 * (1/80)
        jogadorRigidBody.MovePosition(jogadorRigidBody.position + direcao * Velocidade * Time.deltaTime);

        // Raio da origem da câmera até onde o mouse tá apontando
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Pega a posição do raio que ele toca em algo ( já tem valor implícito )
        RaycastHit impacto;

        // O "out" desse if é só para não dar erro porque impacto não tem valor nenhum explícito
        // A mascara no chao é pra contar só o chão como algo que o raio pode tocar
        if(Physics.Raycast(raio,out impacto, 100, MascaraChao))
        {
            // "- transform.position" para ser relativo ao jogador
            Vector3 posicaoMiraJogador = impacto.point - transform.position;

            // Para ficar na altura do jogador
            posicaoMiraJogador.y = transform.position.y;

            // Pensou em rotação pensou na porra do "Quaternion"
            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);

            GetComponent<Rigidbody>().MoveRotation(novaRotacao);
        }
    }


    public void TomarDano(int dano)
    {
        Vida -= dano;
        scriptControlaInterface.AtualizarSliderVidaJogador();
        ControlaAudio.instancia.PlayOneShot(SomDeDano);

        if (Vida <= 0)
        {
            Time.timeScale = 0;
            // Está no Canvas
            GetComponent<ControlaJogador>().textoGameOver.SetActive(true);
        }
    }
    
}

