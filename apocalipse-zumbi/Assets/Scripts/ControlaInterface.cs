using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// tem que falar que vamos usar as partes da interface (Slide que queremos)
using UnityEngine.UI;
// SceneManagement para poder reiniciar o jogo
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour
{
    // Só queremos o Slider do GameObject
    public Slider SliderVidaJogador;
    public GameObject PainelGameOver;
    private float tempoMaximoSobrevivencia;
    private int quantidadeDeZumbisMortos;
    private ControlaJogador scriptControlaJogador;
    // Textos
    public Text TextoQuantidadeZumbisMortos;
    public Text TextoTempoDeSobrevivencia;
    public Text TextoTempoMaximoSobrevivenciaSalvo;
    public Text TextoChefeApareceu;

    // Start is called before the first frame update
    void Start()
    {
        scriptControlaJogador = GameObject.FindWithTag(Tags.Jogador).GetComponent<ControlaJogador>();
        // Iniciar o jogo com o valor máximo do slider igual ao da vida
        SliderVidaJogador.maxValue = scriptControlaJogador.statusJogador.Vida;
        AtualizarSliderVidaJogador();
        Time.timeScale = 1;
        tempoMaximoSobrevivencia = PlayerPrefs.GetFloat(Tags.TempoPontuacaoMaximo);
    }

    public void AtualizarSliderVidaJogador ()
    {
        SliderVidaJogador.value = scriptControlaJogador.statusJogador.Vida;
    }
    
    public void GameOver ()
    {
        Time.timeScale = 0;
        PainelGameOver.SetActive(true);

        int minutos = FloatToMinutes(Time.timeSinceLevelLoad);
        int segundos = FloatToSeconds(Time.timeSinceLevelLoad);

        TextoTempoDeSobrevivencia.text = "You survived for " + minutos + " minutes e " + segundos + " seconds!";

        AjustarTempoMaximo(minutos, segundos);
    }

    private void AjustarTempoMaximo(int min, int seg)
    {
        if(Time.timeSinceLevelLoad > tempoMaximoSobrevivencia)
        {
            tempoMaximoSobrevivencia = Time.timeSinceLevelLoad;
            TextoTempoMaximoSobrevivenciaSalvo.text = textoMelhorTempo(min, seg);

            PlayerPrefs.SetFloat(Tags.TempoPontuacaoMaximo, tempoMaximoSobrevivencia);
        }

        if(TextoTempoMaximoSobrevivenciaSalvo.text == "")
        {
            int minutos = FloatToMinutes(tempoMaximoSobrevivencia);
            int segundos = FloatToSeconds(tempoMaximoSobrevivencia);

            TextoTempoMaximoSobrevivenciaSalvo.text = textoMelhorTempo(minutos, segundos);
        }
    }

    public void AtualizarQuantidadeZumbiMorto()
    {
        quantidadeDeZumbisMortos++;
        TextoQuantidadeZumbisMortos.text = string.Format("x {0}", quantidadeDeZumbisMortos);
    }

    public void TextoApareceuZumbi()
    {
        float tempoAteSumir = 2;
        StartCoroutine(DesapareceTextoApareceuChefe(tempoAteSumir, TextoChefeApareceu));
    }

    IEnumerator DesapareceTextoApareceuChefe(float tempoParaSumir, Text TextoQueVaiSumir)
    {
        TextoQueVaiSumir.gameObject.SetActive(true);
        Color corDoTexto = TextoQueVaiSumir.color;
        corDoTexto.a = 1;
        TextoQueVaiSumir.color = corDoTexto;
        yield return new WaitForSeconds(1);
        float contador = 0;

        while(corDoTexto.a > 0)
        {
            contador += Time.deltaTime / tempoParaSumir;
            corDoTexto.a = Mathf.Lerp(1, 0, contador);
            TextoQueVaiSumir.color = corDoTexto;
            yield return null;
        }

        if(corDoTexto.a == 0)
        {
            TextoQueVaiSumir.gameObject.SetActive(false);
        }
    }

    private string textoMelhorTempo(int min, int seg)
    {
        string texto = string.Format("Best time: {0} minutes and {1} seconds.", min, seg);

        return texto;
    }
    public int FloatToMinutes(float tempo)
    {
        int minutos = (int)(tempo / 60);

        return minutos;
    }

    public int FloatToSeconds(float tempo)
    {
        int segundos = (int)(tempo % 60);

        return segundos;
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(Tags.Game);
    }
}

