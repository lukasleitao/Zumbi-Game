using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour {

    public GameObject Zumbi;
    private float contadorTempo = 0;
    public float TempoGerarZumbi = 4;
    public LayerMask LayerZumbi;

	// Update is called once per frame
	void Update () {

        contadorTempo += Time.deltaTime;

        if (contadorTempo > TempoGerarZumbi)
        {
            int raioDaEsferaDeColisao = 1;
            Collider[] colisores = Physics.OverlapSphere(AleatorizarPosicao(), raioDaEsferaDeColisao, LayerZumbi);
            
            // while bugga
            //while (colisores.Length > 0)
            //{
            //    colisores = Physics.OverlapSphere(transform.position, raioDaEsferaDeColisao, LayerZumbi);
            //}
            GerarZumbi();
            contadorTempo = 0;
        }
    }

    void GerarZumbi()
    {
        Instantiate(Zumbi, transform.position, transform.rotation);
    }

    Vector3 AleatorizarPosicao()
    {
        int raioDaEsferaDePosicao = 3;
        Vector3 posicao = Random.insideUnitSphere * raioDaEsferaDePosicao;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }
}
