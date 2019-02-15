using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//tem que falar que vamos usar as partes da interface (Slide que queremos)
using UnityEngine.UI;

public class ControlaInterface : MonoBehaviour
{
    private ControlaJogador scriptControlaJogador;
    // Só queremos o Slider do GameObject
    public Slider SliderVidaJogador;

    // Start is called before the first frame update
    void Start()
    {
        scriptControlaJogador = GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>();
        // Iniciar o jogo com o valor máximo do slider igual ao da vida
        SliderVidaJogador.maxValue = scriptControlaJogador.statusJogador.Vida;
        AtualizarSliderVidaJogador();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AtualizarSliderVidaJogador ()
    {
        SliderVidaJogador.value = scriptControlaJogador.statusJogador.Vida;
    }
}
