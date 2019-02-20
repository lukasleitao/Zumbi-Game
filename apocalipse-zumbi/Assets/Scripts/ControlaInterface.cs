using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// tem que falar que vamos usar as partes da interface (Slide que queremos)
using UnityEngine.UI;
// SceneManagement para poder reiniciar o jogo
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour
{
    private ControlaJogador scriptControlaJogador;
    // Só queremos o Slider do GameObject
    public Slider SliderVidaJogador;
    public GameObject PainelGameOver; 

    // Start is called before the first frame update
    void Start()
    {
        scriptControlaJogador = GameObject.FindWithTag(Tags.Jogador).GetComponent<ControlaJogador>();
        // Iniciar o jogo com o valor máximo do slider igual ao da vida
        SliderVidaJogador.maxValue = scriptControlaJogador.statusJogador.Vida;
        AtualizarSliderVidaJogador();
        Time.timeScale = 1;
    }

    public void AtualizarSliderVidaJogador ()
    {
        SliderVidaJogador.value = scriptControlaJogador.statusJogador.Vida;
    }
    
    public void GameOver ()
    {
        Time.timeScale = 0;
        PainelGameOver.SetActive(true);
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(Tags.Game);
    }
}

