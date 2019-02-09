using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaAudio : MonoBehaviour
{
    private AudioSource meuAudioSource;
    public static AudioSource instancia;

    // Awake é como se fosse start porém vem antes (foi o que entendi)
    void Awake()
    {
        meuAudioSource = GetComponent<AudioSource>();
        instancia = meuAudioSource;
    }
}
