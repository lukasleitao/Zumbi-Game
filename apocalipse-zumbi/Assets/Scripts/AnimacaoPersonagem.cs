using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPersonagem : MonoBehaviour
{
    private Animator meuAnimator;

    private void Awake()
    {
        meuAnimator = GetComponent<Animator>();
    }

    public void Atacar (bool estado)
    {
        meuAnimator.SetBool(Tags.Atacando, estado);
    }

    public void Movendo (float velocidade)
    {
        meuAnimator.SetFloat(Tags.Movendo, velocidade);
    }

    public void Morreu()
    {
        meuAnimator.SetTrigger(Tags.Morreu);
    }
}