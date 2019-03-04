using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefeFase : MonoBehaviour
{
    public GameObject ChefeDaFase;
    private float tempoProximaGeracao = 0;
    private float tempoEntreGeracoes = 20;

    private void Start()
    {
        tempoProximaGeracao = tempoEntreGeracoes;
    }

    private void Update()
    {
        if(Time.timeSinceLevelLoad > tempoProximaGeracao)
        {
            Instantiate(ChefeDaFase, transform.position, Quaternion.identity);
            tempoProximaGeracao = Time.timeSinceLevelLoad + tempoEntreGeracoes;
        }
    }
}
